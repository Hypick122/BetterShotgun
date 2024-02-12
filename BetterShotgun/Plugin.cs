using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using LethalLib.Modules;

namespace Hypick;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static Plugin Instance { get; set; }

    public static ManualLogSource Log => Instance.Logger;

    public new static PluginConfig Config;

    private readonly Harmony _harmony = new(PluginInfo.PLUGIN_GUID);

    public List<Item> AllItems => Resources.FindObjectsOfTypeAll<Item>().Concat(FindObjectsByType<Item>((FindObjectsInactive)1, (FindObjectsSortMode)1)).ToList();
    public Item Shotgun => AllItems.FirstOrDefault(item => item.name == "Shotgun");
	public Item ShotgunShell => AllItems.FirstOrDefault(item => item.name == "GunAmmo");

    private bool isLoaded;

    public Plugin() => Instance = this;

    private void Awake()
    {
        Config = new PluginConfig(base.Config);

        SceneManager.sceneLoaded += OnSceneLoaded;

        Log.LogInfo($"Applying patches...");
        _harmony.PatchAll();
        Log.LogInfo($"Patches applied");

        Logger.LogInfo($"{PluginInfo.PLUGIN_GUID} is fully loaded!");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (!isLoaded && scene.name == "MainMenu")
		{
			isLoaded = true;

            RegisterItem(Shotgun, "Shotgun", Config.ShotgunMinValue, Config.ShotgunMaxValue, Config.ShotgunPrice, Config.ShotgunRarity);
            RegisterItem(ShotgunShell, "Shell", Config.ShotgunShellMinValue, Config.ShotgunShellMaxValue, Config.ShotgunShellPrice, Config.ShotgunShellRarity);
		}
	}

    private void RegisterItem(Item item, string name, int min, int max, int price, int rarity)
    {
        item.itemName = name;
        item.minValue = Mathf.Max(min, 1);
        item.maxValue = Mathf.Max(max, item.minValue);

        if (price != -1) Items.RegisterShopItem(item, price);
        if (rarity != -1) Items.RegisterScrap(item, rarity, Levels.LevelTypes.All);

        Log.LogInfo($"Loaded {item}");
    }
}
