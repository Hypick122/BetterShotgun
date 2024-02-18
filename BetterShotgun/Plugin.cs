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
[BepInDependency("com.rune580.LethalCompanyInputUtils", BepInDependency.DependencyFlags.HardDependency)]
public class Plugin : BaseUnityPlugin
{
	public static Plugin Instance { get; set; }

	public static ManualLogSource Log => Instance.Logger;

	public static new PluginConfig Config;

	private readonly Harmony _harmony = new(PluginInfo.PLUGIN_GUID);

	internal static Keybinds InputActionsInstance = new Keybinds();

	public List<Item> AllItems => Resources.FindObjectsOfTypeAll<Item>().Concat(FindObjectsByType<Item>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID)).ToList();
	public Item Shotgun => AllItems.FirstOrDefault(item => item.name == "Shotgun");
	public Item ShotgunShell => AllItems.FirstOrDefault(item => item.name == "GunAmmo");

	public static AnimationClip ShotgunInspectAnimation;
	public static AudioClip ShotgunInspectSFX;

	private bool isLoaded;

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
		if (Config.ReloadKeybind.ToLower().Replace("<keyboard>/", "") != "e")
		{
			Log.LogInfo($"Start ReloadKeybind with key {InputActionsInstance.ReloadKey.GetBindingDisplayString()}");
			InputActionsInstance.ReloadKey.performed += OnReloadKeyPressed;
		}
	}

	public void OnReloadKeyPressed(InputAction.CallbackContext context)
	{
		if (!context.performed)
			return;

		if (GameNetworkManager.Instance != null && GameNetworkManager.Instance.localPlayerController != null)
		{
			PlayerControllerB player = GameNetworkManager.Instance.localPlayerController;
			if (player.IsOwner)
			{
				GrabbableObject currentItem = player.ItemSlots[player.currentItemSlot];
				if (currentItem != null && currentItem is ShotgunItem item && !item.isReloading)
					item.StartReloadGun();
			}
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (!isLoaded && scene.name == "MainMenu")
		{
			isLoaded = true;

			RegisterItem(Shotgun, Config.ShotgunMaxDiscount, Config.ShotgunMinValue, Config.ShotgunMaxValue, Config.ShotgunWeight, Config.ShotgunPrice, Config.ShotgunRarity);
			RegisterItem(ShotgunShell, Config.ShotgunShellMaxDiscount, Config.ShotgunShellMinValue, Config.ShotgunShellMaxValue, 0, Config.ShotgunShellPrice, Config.ShotgunShellRarity);
		}
	}

	private void RegisterItem(Item item, int maxDiscount, int minValue, int maxValue, int weight, int price, int rarity)
	{
		item.highestSalePercentage = maxDiscount;
		item.minValue = Mathf.Max(minValue, 0) * 100 / 40;
		item.maxValue = Mathf.Max(maxValue, item.minValue) * 100 / 40;
		item.weight = weight <= 9 ? (weight + 100) / 100f : (weight + 99) / 100f; // (weight - 1) / 100f + 1f // TODO: to correct
		// item.weight = (weight / 100f) + 1f;

		if (price != -1)
			Items.RegisterShopItem(item, price);
		if (rarity != -1)
			Items.RegisterScrap(item, rarity, Levels.LevelTypes.All);

		Log.LogInfo($"Loaded {item}");
	}
}
