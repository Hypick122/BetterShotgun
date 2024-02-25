using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using LethalLib.Modules;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Hypick;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency(LethalLib.Plugin.ModGUID)]
[BepInDependency("com.rune580.LethalCompanyInputUtils")]
public class Plugin : BaseUnityPlugin
{
	private static Plugin Instance { get; set; }

	public static ManualLogSource Log => Instance.Logger;

	public new static PluginConfig Config;

	private readonly Harmony _harmony = new(PluginInfo.PLUGIN_GUID);

	private static Keybinds InputActionsInstance = new Keybinds();

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
		Config = new PluginConfig(base.Config);

		SceneManager.sceneLoaded += OnSceneLoaded;

		Log.LogInfo($"Applying patches...");
		_harmony.PatchAll();
		Log.LogInfo($"Patches applied");

		SetupKeybindCallbacks();

		Logger.LogInfo($"{PluginInfo.PLUGIN_GUID} is fully loaded!");
	}

	public void SetupKeybindCallbacks()
	{
		InputActionsInstance.ReloadKey.AddBinding($"<keyboard>/{Config.ReloadKeybind}");
		if (Config.ReloadKeybind.Replace("<keyboard>/", "") != "e")
		{
			Log.LogInfo($"Start ReloadKeybind with key {InputActionsInstance.ReloadKey.GetBindingDisplayString()}");
			InputActionsInstance.ReloadKey.performed += OnReloadKeyPressed;
		}
	}

	public void OnReloadKeyPressed(InputAction.CallbackContext context)
	{
		if (!context.performed || GameNetworkManager.Instance == null || GameNetworkManager.Instance.localPlayerController == null)
			return;

		PlayerControllerB player = GameNetworkManager.Instance.localPlayerController;
		if (!player.IsOwner)
			return;

		GrabbableObject currentItem = player.ItemSlots[player.currentItemSlot];
		if (currentItem != null && currentItem is ShotgunItem item && !item.isReloading)
			item.StartReloadGun();
	}

	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (_isLoaded || scene.name != "MainMenu")
			return;

		_isLoaded = true;
		RegisterItem(Shotgun, "Shotgun", Config.ShotgunMaxDiscount, Config.ShotgunMinValue, Config.ShotgunMaxValue,
			Config.ShotgunWeight, Config.ShotgunPrice, Config.ShotgunRarity);
		RegisterItem(ShotgunShell, "Shell", Config.ShellMaxDiscount, Config.ShellMinValue, Config.ShellMaxValue, 0,
			Config.ShellPrice, Config.ShellRarity);
	}

	private static void RegisterItem(Item item, string name, int maxDiscount, int minValue, int maxValue, int weight,
		int price, int rarity)
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