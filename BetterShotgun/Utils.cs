using LethalLib.Modules;
using UnityEngine;
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

	public static void RegisterItem(
		Item item, string name, int maxDiscount, int minValue, int maxValue, float weight, int price, int rarity
	)
	{
		item.itemName = name;
		item.highestSalePercentage = maxDiscount;
		item.minValue = Mathf.Max(minValue, 0) * 100 / 40;
		item.maxValue = Mathf.Max(maxValue, item.minValue) * 100 / 40;
		item.weight = weight <= 9 ? (weight + 100) / 100f : (weight + 99) / 100f; // TODO: to correct

		if (price != -1)
		{
			Items.RegisterShopItem(item, price);
			Plugin.Log.LogInfo($"{item.itemName} added to the store");
		}

		if (rarity != -1)
		{
			Items.RegisterScrap(item, rarity, Levels.LevelTypes.All);
			Plugin.Log.LogInfo($"{item.itemName} added as scrap");
		}

		Plugin.Log.LogInfo($"Loaded {item.itemName}");
	}
}