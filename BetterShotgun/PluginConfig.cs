using System.Security.Cryptography.X509Certificates;
using BepInEx.Configuration;

namespace Hypick;

public static class Categories
{
	public const string Shotgun = nameof(Shotgun);
	public const string ShotgunTweaks = "Shotgun.Tweaks";
	public const string Shell = nameof(Shell);
}

public class PluginConfig
{
	public int ShotgunPrice { get; }
	public int ShotgunMaxDiscount { get; }
	public int ShotgunMinValue { get; }
	public int ShotgunMaxValue { get; }
	public int ShotgunWeight { get; }
	public int ShotgunRarity { get; }

	public bool MisfireOff { get; }
	public bool InfiniteAmmo { get; }
	public string ReloadKeybind { get; }
	public bool ShowAmmoCount { get; }

	// public bool ShowAmmoCountUI { get; }
	public bool AmmoCheckAnimation { get; }
	public bool ReloadNoLimit { get; }

	// public bool DisableFriendlyFire { get; }
	public bool SkipReloadAnimation { get; }

	// public bool DoorBreach { get; }
	// public bool KillMine { get; }
	// public bool DisableTurret { get; }

	public int ShotgunShellPrice { get; }
	public int ShotgunShellMaxDiscount { get; }
	public int ShotgunShellMinValue { get; }
	public int ShotgunShellMaxValue { get; }
	public int ShotgunShellRarity { get; }

	public PluginConfig(ConfigFile cfg)
	{
		ShotgunPrice = cfg.Bind<int>(Categories.Shotgun, "Price", 700, "Cost of a shotgun in a store. (-1 = remove from sale)").Value;
		ShotgunMaxDiscount = cfg.Bind<int>(Categories.Shotgun, "MaxDiscount", 80, new ConfigDescription("Maximum discount percentage in store (vanilla = 80)", new AcceptableValueRange<int>(0, 90))).Value;
		ShotgunMinValue = cfg.Bind<int>(Categories.Shotgun, "MinValueScrap", 40, "Minimum shotgun value (must be >= 0)").Value;
		ShotgunMaxValue = cfg.Bind<int>(Categories.Shotgun, "MaxValueScrap", 70, "Maximum shotgun value (must be >= min value)").Value;
		ShotgunWeight = cfg.Bind<int>(Categories.Shotgun, "Weight", 16, new ConfigDescription("Scrap weight", new AcceptableValueRange<int>(0, 100))).Value;

		ShotgunRarity = cfg.Bind<int>(Categories.Shotgun, "Rarity", -1, "Rarity of shotgun spawn on moons (higher = more common). A shotgun will also appear in gifts. (-1 = disable)").Value;
		MisfireOff = cfg.Bind<bool>(Categories.ShotgunTweaks, nameof(MisfireOff), true, "If set to true, disables shotgun misfire (vanilla = false)").Value;
		InfiniteAmmo = cfg.Bind<bool>(Categories.ShotgunTweaks, nameof(InfiniteAmmo), false, "If set to true, the shotgun will have infinite ammo").Value;
		ReloadKeybind = cfg.Bind<string>(Categories.ShotgunTweaks, nameof(ReloadKeybind), "R", "Changes the reload key to the one you specify (vanilla = E)").Value;
		ShowAmmoCount = cfg.Bind<bool>(Categories.ShotgunTweaks, nameof(ShowAmmoCount), true, "If set to true, the number of cartridges in the shotgun will be displayed in the upper right text").Value;
		AmmoCheckAnimation = cfg.Bind<bool>(Categories.ShotgunTweaks, nameof(AmmoCheckAnimation), true, "Enables animation of checking cartridges in a shotgun on the reload key (Does not work with InfiniteAmmo = true)").Value;
		ReloadNoLimit = cfg.Bind<bool>(Categories.ShotgunTweaks, nameof(ReloadNoLimit), false, "The shotgun can be loaded with an infinite number of cartridges").Value;
		// DisableFriendlyFire = cfg.Bind<bool>(Categories.ShotgunTweaks, nameof(DisableFriendlyFire), true, "").Value;
		SkipReloadAnimation = cfg.Bind<bool>(Categories.ShotgunTweaks, nameof(SkipReloadAnimation), false, "Skips shotgun reload animation").Value;

		ShotgunShellPrice = cfg.Bind<int>(Categories.Shell, "Price", 50, "Cost of a shotgun shell in a store. (-1 = remove from sale)").Value;
		ShotgunShellMaxDiscount = cfg.Bind<int>(Categories.Shell, "MaxDiscount", 80, new ConfigDescription("Maximum discount percentage in store (vanilla = 80)", new AcceptableValueRange<int>(0, 90))).Value;
		ShotgunShellMinValue = cfg.Bind<int>(Categories.Shell, "MinValueScrap", 15, "Minimum shotgun shell value (must be > 0)").Value;
		ShotgunShellMaxValue = cfg.Bind<int>(Categories.Shell, "MaxValueScrap", 25, "Maximum shotgun shell value (must be > min value)").Value;
		ShotgunShellRarity = cfg.Bind<int>(Categories.Shell, "Rarity", 2, "Rarity of shotgun shell spawns on moons (higher = more common). Shell will also appear in gifts. (-1 = disable)").Value;
	}
}
