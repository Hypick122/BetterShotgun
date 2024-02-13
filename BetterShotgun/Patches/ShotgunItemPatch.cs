using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace Hypick.Patches;

[HarmonyPatch(typeof(ShotgunItem))]
internal class ShotgunItemPatch
{
    [HarmonyPatch("Update")]
    [HarmonyPrefix]
    static void Update(ShotgunItem __instance)
    {
        if (Plugin.Config.MisfireOff && !__instance.safetyOn)
        {
            __instance.hasHitGroundWithSafetyOff = true;
            __instance.misfireTimer = float.MaxValue;
        }
    }

    [HarmonyPatch("ItemActivate")]
    [HarmonyPrefix]
    static void ItemActivate(ShotgunItem __instance)
    {
        if (Plugin.Config.InfiniteAmmo)
            __instance.shellsLoaded = int.MaxValue;
    }

    [HarmonyPatch("SetControlTipsForItem")]
    [HarmonyPrefix]
    static void SetControlTipsForItem(ShotgunItem __instance)
    {
        var newToolTips = Plugin.Config.AmmoCheckAnimation ? "Reload / Check" : "Reload";
        var keybindReload = $"[{Plugin.Config.ReloadKeybind}]";

        if (Plugin.Config.ShowAmmoCount)
        {
            string ammoInfo = Plugin.Config.InfiniteAmmo ? "∞" : $"{__instance.shellsLoaded}/2";
            __instance.itemProperties.toolTips[1] = newToolTips + $" ({ammoInfo}): {keybindReload}";
        }
        else
            __instance.itemProperties.toolTips[1] = newToolTips + $": {keybindReload}";
    }

    [HarmonyPatch("SetSafetyControlTip")]
    [HarmonyPrefix]
    static void SetSafetyControlTip(ShotgunItem __instance)
    {
        string newToolTips = Plugin.Config.AmmoCheckAnimation ? "Reload / Check" : "Reload";
        string keybindReload = $"[{Plugin.Config.ReloadKeybind}]";

        if (__instance.IsOwner && Plugin.Config.ShowAmmoCount)
        {
            string ammoInfo = Plugin.Config.InfiniteAmmo ? "∞" : $"{__instance.shellsLoaded}/2";
            newToolTips += $" ({ammoInfo}): {keybindReload}";
            HUDManager.Instance.ChangeControlTip(2, newToolTips);
        }
        else
            HUDManager.Instance.ChangeControlTip(2, newToolTips + $": {keybindReload}");
    }

    [HarmonyPatch("ReloadGunEffectsClientRpc")]
    [HarmonyPatch("ShootGun")]
    [HarmonyPostfix]
    static void UpdateControlTips(ShotgunItem __instance)
    {
        if (Plugin.Config.ShowAmmoCount)
            __instance.SetSafetyControlTip();
    }

    [HarmonyPatch("ItemInteractLeftRight")]
    [HarmonyPrefix]
    static bool ItemInteractLeftRight(bool right)
    {
        if (Plugin.Config.ReloadKeybind.ToLower() != "e" && right)
            return false;

        return true;
    }

    [HarmonyPatch("StartReloadGun")]
    [HarmonyPrefix]
    static bool StartReloadGun(ShotgunItem __instance)
    {
        if (Plugin.Config.AmmoCheckAnimation && !__instance.ReloadedGun())
        {
            if (__instance.gunCoroutine != null)
                __instance.StopCoroutine(__instance.gunCoroutine);

            __instance.gunCoroutine = __instance.StartCoroutine(CheckAmmoAnimation(__instance));
            return false;
        }

        if (Plugin.Config.InfiniteAmmo && __instance.ReloadedGun())
            return false;

        return true;
    }

    private static IEnumerator CheckAmmoAnimation(ShotgunItem __instance)
    {
        __instance.isReloading = true;
        __instance.playerHeldBy.playerBodyAnimator.SetBool("ReloadShotgun", true);
        __instance.shotgunShellLeft.enabled = __instance.shellsLoaded > 0;
        __instance.shotgunShellRight.enabled = __instance.shellsLoaded > 1;

        yield return new WaitForSeconds(0.3f);

        __instance.gunAudio.PlayOneShot(__instance.gunReloadSFX);
        __instance.gunAnimator.SetBool("Reloading", true);
        __instance.ReloadGunEffectsServerRpc();

        yield return new WaitForSeconds(2.35f);

        __instance.gunAudio.PlayOneShot(__instance.gunReloadFinishSFX);
        __instance.gunAnimator.SetBool("Reloading", false);
        __instance.playerHeldBy.playerBodyAnimator.SetBool("ReloadShotgun", false);
        __instance.isReloading = false;
        __instance.ReloadGunEffectsServerRpc(start: false);
    }
}
