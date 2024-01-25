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

    public Plugin()
    {
        Instance = this;
    }

    private void Awake()
    {
        Config = new PluginConfig(base.Config);

        SceneManager.sceneLoaded += OnSceneLoaded;

        Log.LogInfo($"Applying patches...");
        _harmony.PatchAll();
        Log.LogInfo($"Patches applied");

        Logger.LogInfo($"{PluginInfo.PLUGIN_GUID} is fully loaded!");
    }

    private bool isLoaded;
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (!isLoaded && scene.name == "MainMenu")
		{
			isLoaded = true;

            SetItemValues(Shotgun, "Shotgun", Config.ShotgunMinValue, Config.ShotgunMaxValue);
            SetItemValues(ShotgunShell, "Shell", Config.ShotgunShellMinValue, Config.ShotgunShellMaxValue);

            if (Config.ShotgunPrice != -1) Items.RegisterShopItem(Shotgun, Config.ShotgunPrice);
            if (Config.ShotgunShellPrice != -1) Items.RegisterShopItem(ShotgunShell, Config.ShotgunShellPrice);

            if (Config.ShotgunRarity != -1) Items.RegisterScrap(Shotgun, Config.ShotgunRarity, Levels.LevelTypes.All);
            if (Config.ShotgunShellRarity != -1) Items.RegisterScrap(ShotgunShell, Config.ShotgunShellRarity, Levels.LevelTypes.All);
		}
	}

    private void SetItemValues(Item item, string name, int min, int max)
    {
        item.itemName = name;
        item.minValue = min;
        item.maxValue = min < max ? max : max * 2;
    }
}
