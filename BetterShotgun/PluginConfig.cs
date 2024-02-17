using BepInEx.Configuration;

namespace Hypick;

public static class Category
{
	public const string Shotgun = "1 >> Shotgun << 1";
	public const string ShotgunTweaks = "3 >> Shotgun Tweaks << 3";
	public const string Shell = "2 >> Shell << 2";
}

public class PluginConfig
{
	# region Shotgun
	public int ShotgunPrice { get; }
	public int ShotgunMaxDiscount { get; }
	public int ShotgunMinValue { get; }
	public int ShotgunMaxValue { get; }
	public int ShotgunWeight { get; }
	public int ShotgunRarity { get; }
	# endregion

	# region Shotgun Tweaks
	public bool MisfireOff { get; }
	public bool InfiniteAmmo { get; }
	public string ReloadKeybind { get; }
	public bool ShowAmmoCount { get; }

	// public bool ShowAmmoCountUI { get; }
	public bool AmmoCheckAnimation { get; }
	public bool ReloadNoLimit { get; }

	public bool DisableFriendlyFire { get; }
	public bool SkipReloadAnimation { get; }
	// public bool DoorBreach { get; }
	// public bool KillMine { get; }
	// public bool DisableTurret { get; }
	# endregion

	# region Shotgun Shell
	public int ShotgunShellPrice { get; }
	public int ShotgunShellMaxDiscount { get; }
	public int ShotgunShellMinValue { get; }
	public int ShotgunShellMaxValue { get; }
	public int ShotgunShellRarity { get; }
	# endregion

	public PluginConfig(ConfigFile cfg)
	{
		# region Shotgun
		ShotgunPrice = cfg.Bind<int>(Category.Shotgun, "Price", 700, "Cost of a shotgun in a store. (-1 = remove from sale)").Value;
		ShotgunMaxDiscount = cfg.Bind<int>(Category.Shotgun, "MaxDiscount", 80, new ConfigDescription("Maximum discount percentage in store (vanilla = 80)", new AcceptableValueRange<int>(0, 90))).Value;
		ShotgunMinValue = cfg.Bind<int>(Category.Shotgun, "MinValueScrap", 40, "Minimum scrap value (must be >= 0) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)").Value;
		ShotgunMaxValue = cfg.Bind<int>(Category.Shotgun, "MaxValueScrap", 70, "Maximum scrap value (must be >= min value) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)").Value;
		ShotgunWeight = cfg.Bind<int>(Category.Shotgun, "Weight", 16, new ConfigDescription("[BETA] Scrap weight", new AcceptableValueRange<int>(0, 100))).Value;
		ShotgunRarity = cfg.Bind<int>(Category.Shotgun, "Rarity", -1, "Rarity of shotgun spawn on moons (higher = more common). A shotgun will also appear in gifts. (-1 = disable)").Value;
		# endregion

		# region Shotgun Tweaks
		MisfireOff = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(MisfireOff), true, "If set to true, disables shotgun misfire (vanilla = false)").Value;
		InfiniteAmmo = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(InfiniteAmmo), false, "If set to true, the shotgun will have infinite ammo").Value;
		ReloadKeybind = cfg.Bind<string>(Category.ShotgunTweaks, nameof(ReloadKeybind), "R", "Changes the reload key to the one you specify (vanilla = E)").Value;
		Manager.ReloadShotgunKey = ReloadKeybind.ToLower().StartsWith("<keyboard>") ? ReloadKeybind : $"<Keyboard>/{ReloadKeybind}";
		ShowAmmoCount = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(ShowAmmoCount), true, "If set to true, the number of cartridges in the shotgun will be displayed in the upper right text").Value;
		AmmoCheckAnimation = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(AmmoCheckAnimation), true, "[BETA] Enables animation of checking cartridges in a shotgun on the reload key (Does not work with InfiniteAmmo = true)").Value;
		ReloadNoLimit = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(ReloadNoLimit), false, "The shotgun can be loaded with an infinite number of cartridges").Value;
		DisableFriendlyFire = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(DisableFriendlyFire), false, "Turns off friendly fire").Value;
		SkipReloadAnimation = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(SkipReloadAnimation), false, "Skips shotgun reload animation").Value;
		# endregion

		# region Shotgun Shell
		ShotgunShellPrice = cfg.Bind<int>(Category.Shell, "Price", 50, "Cost of a shotgun shell in a store. (-1 = remove from sale)").Value;
		ShotgunShellMaxDiscount = cfg.Bind<int>(Category.Shell, "MaxDiscount", 80, new ConfigDescription("Maximum discount percentage in store (vanilla = 80)", new AcceptableValueRange<int>(0, 90))).Value;
		ShotgunShellMinValue = cfg.Bind<int>(Category.Shell, "MinValueScrap", 15, "Minimum scrap value (must be >= 0) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)").Value;
		ShotgunShellMaxValue = cfg.Bind<int>(Category.Shell, "MaxValueScrap", 25, "Maximum scrap value (must be >= min value) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)").Value;
		ShotgunShellRarity = cfg.Bind<int>(Category.Shell, "Rarity", 2, "Rarity of shotgun shell spawns on moons (higher = more common). Shell will also appear in gifts. (-1 = disable)").Value;
		# endregion
	}
}
