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

            Shotgun.itemName = "Shotgun";
            Shotgun.minValue = 50;
            Shotgun.maxValue = 100;
            
            ShotgunShell.itemName = "Shells";
            ShotgunShell.minValue = 25;
            ShotgunShell.maxValue = 50;

            if (Config.ShotgunPrice != -1) Items.RegisterShopItem(Shotgun, Config.ShotgunPrice);
            if (Config.ShellPrice != -1) Items.RegisterShopItem(ShotgunShell, Config.ShellPrice);

            if (Config.ShotgunRarity != 0) Items.RegisterScrap(Shotgun, Config.ShotgunRarity, Levels.LevelTypes.All);
            if (Config.ShellRarity != 0) Items.RegisterScrap(ShotgunShell, Config.ShellRarity, Levels.LevelTypes.All);
		}
	}
}
