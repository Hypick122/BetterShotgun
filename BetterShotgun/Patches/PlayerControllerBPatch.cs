using BepInEx;
using GameNetcodeStuff;
using HarmonyLib;

namespace Hypick.Patches;

[HarmonyPatch(typeof(PlayerControllerB))]
internal class PlayerControllerBPatch
{
    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    static void Update(PlayerControllerB __instance)
    {
        if (Plugin.Config.ReloadKeybind.ToLower() != "e" && UnityInput.Current.GetKeyDown(Plugin.Config.ReloadKeybind.ToLower()))
        {
            ShotgunItem shotgun = __instance.ItemSlots[__instance.currentItemSlot] as ShotgunItem;
            if (shotgun != null && !shotgun.isReloading && shotgun.shellsLoaded < 2)
                shotgun.StartReloadGun();
        }
    }
}
