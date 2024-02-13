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
            GrabbableObject currentItem = __instance.ItemSlots[__instance.currentItemSlot];

            if (currentItem != null && currentItem is ShotgunItem)
            {
                ShotgunItem item = (ShotgunItem)currentItem;
                if (!item.isReloading)
                    item.StartReloadGun();
            }
        }
    }
}
