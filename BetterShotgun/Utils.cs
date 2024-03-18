using UnityEngine.InputSystem;

namespace Hypick;

public class Utils
{
	public static void CheckInfiniteAmmo(ShotgunItem __instance)
	{
		if (SyncConfig.Instance.InfiniteAmmo.Value)
			__instance.shellsLoaded = 3;
	}

	public static bool CheckFriendly(ShotgunItem __instance)
	{
		return __instance.playerHeldBy != null;
	}

	public static string GetCustomTooltip(ShotgunItem __instance)
	{
		var newToolTips = SyncConfig.Default.AmmoCheckAnimation.Value ? "Reload / Check" : "Reload";

		if (!SyncConfig.Default.ShowAmmoCount.Value)
			return $"{newToolTips}: [{Plugin.InputActionsInstance.ReloadKey.GetBindingDisplayString()}]";

		var maxAmmo = SyncConfig.Instance.ReloadNoLimit.Value ? "∞" : "2";
		var ammoInfo = SyncConfig.Instance.InfiniteAmmo.Value ? "∞" : $"{__instance.shellsLoaded}/{maxAmmo}";

		return $"{newToolTips} ({ammoInfo}): [{Plugin.InputActionsInstance.ReloadKey.GetBindingDisplayString()}]";
	}
}