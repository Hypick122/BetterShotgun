using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using LethalLib.Modules;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Hypick;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(LethalLib.Plugin.ModGUID)]
[BepInDependency("com.rune580.LethalCompanyInputUtils")]
[BepInDependency("com.sigurd.csync")]
public class Plugin : BaseUnityPlugin
{
	private static Plugin Instance { get; set; }

	public static ManualLogSource Log => Instance.Logger;

	public static SyncConfig SyncConfig;

	private readonly Harmony _harmony = new(MyPluginInfo.PLUGIN_GUID);

	public static readonly Keybinds InputActionsInstance = new();

	private List<Item> AllItems => Resources.FindObjectsOfTypeAll<Item>()
		.Concat(FindObjectsByType<Item>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID)).ToList();

	private Item Shotgun => AllItems.FirstOrDefault(item => item.name == "Shotgun");
	private Item ShotgunShell => AllItems.FirstOrDefault(item => item.name == "GunAmmo");

	private static AnimationClip ShotgunInspectAnimation;
	private static AudioClip ShotgunInspectSFX;

	private bool _isLoaded;

	public Plugin() => Instance = this;

	private void Awake()
	{
		SyncConfig = new SyncConfig(Config);

		SceneManager.sceneLoaded += OnSceneLoaded;

		_harmony.PatchAll();
		Log.LogInfo($"Patches applied");

		SetupKeybindCallbacks();

		Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} is fully loaded!");
	}

	public void SetupKeybindCallbacks()
	{
		try
		{
			if (SyncConfig.Default.ReloadKeybind.Value.ToLower() != "e")
			{
				InputActionsInstance.ReloadKey.AddBinding($"<keyboard>/{SyncConfig.Default.ReloadKeybind.Value}");
				InputActionsInstance.ReloadKey.performed += OnReloadKeyPressed;
				Log.LogInfo(
					$"ReloadKeybind is started using the {InputActionsInstance.ReloadKey.GetBindingDisplayString()} key");
			}
		}
		catch (Exception e)
		{
			Log.LogError("An error occurred while binding ReloadKeybind");
			Log.LogError(e);
		}
	}

	public void OnReloadKeyPressed(InputAction.CallbackContext context)
	{
		if (!context.performed || GameNetworkManager.Instance == null ||
		    GameNetworkManager.Instance.localPlayerController == null)
			return;

		PlayerControllerB player = GameNetworkManager.Instance.localPlayerController;
		if (player.IsOwner)
		{
			GrabbableObject currentItem = player.ItemSlots[player.currentItemSlot];

			if (currentItem != null && currentItem is ShotgunItem item && !item.isReloading)
				item.StartReloadGun();
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (!_isLoaded && scene.name == "MainMenu")
		{
			_isLoaded = true;

			var ammoName = "Shell";
			if (Chainloader.PluginInfos.ContainsKey("FlipMods.ReservedWeaponSlot"))
			{
				ammoName = "Ammo";
				Log.LogWarning(
					"ReservedWeaponSlot detected, the name of the cartridges changes from \"Shell\" to \"Ammo\"");
			}

			RegisterItem(Shotgun, "Shotgun", SyncConfig.Instance.ShotgunMaxDiscount.Value,
				SyncConfig.Instance.ShotgunMinValue.Value, SyncConfig.Instance.ShotgunMaxValue.Value,
				SyncConfig.Instance.ShotgunWeight.Value, SyncConfig.Instance.ShotgunPrice.Value,
				SyncConfig.Instance.ShotgunRarity.Value);
			RegisterItem(ShotgunShell, ammoName, SyncConfig.Instance.ShellMaxDiscount.Value,
				SyncConfig.Instance.ShellMinValue.Value, SyncConfig.Instance.ShellMaxValue.Value, 0,
				SyncConfig.Instance.ShellPrice.Value, SyncConfig.Instance.ShellRarity.Value);
		}
	}

	private static void RegisterItem(
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
			Log.LogInfo($"{item.itemName} added to the store");
		}

		if (rarity != -1)
		{
			Items.RegisterScrap(item, rarity, Levels.LevelTypes.All);
			Log.LogInfo($"{item.itemName} added as scrap");
		}

		Log.LogInfo($"Loaded {item.itemName}");
	}
}