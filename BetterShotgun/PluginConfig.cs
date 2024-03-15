using System.Runtime.Serialization;
using BepInEx.Configuration;
using CSync.Lib;
using CSync.Util;

namespace Hypick;

public static class Category
{
	public const string Shotgun = "1 >> Shotgun << 1";
	public const string ShotgunTweaks = "3 >> Shotgun Tweaks << 3";
	public const string Shell = "2 >> Shell << 2";
}

[DataContract]
public class SyncConfig : SyncedConfig<SyncConfig>
{
	# region Shotgun

	[DataMember] public SyncedEntry<int> ShotgunPrice { get; private set; }
	[DataMember] public SyncedEntry<int> ShotgunMaxDiscount { get; private set; }
	[DataMember] public SyncedEntry<int> ShotgunMinValue { get; private set; }
	[DataMember] public SyncedEntry<int> ShotgunMaxValue { get; private set; }
	[DataMember] public SyncedEntry<float> ShotgunWeight { get; private set; }
	[DataMember] public SyncedEntry<int> ShotgunRarity { get; private set; }

	# endregion

	# region Shotgun Tweaks

	[DataMember] public SyncedEntry<bool> MisfireOff { get; private set; }
	[DataMember] public SyncedEntry<bool> InfiniteAmmo { get; private set; }
	[DataMember] public SyncedEntry<string> ReloadKeybind { get; private set; }
	[DataMember] public SyncedEntry<bool> ShowAmmoCount { get; private set; }
	[DataMember] public SyncedEntry<bool> AmmoCheckAnimation { get; private set; }
	[DataMember] public SyncedEntry<bool> ReloadNoLimit { get; private set; }
	[DataMember] public SyncedEntry<bool> DisableFriendlyFire { get; private set; }
	[DataMember] public SyncedEntry<bool> SkipReloadAnimation { get; private set; }

	# endregion

	# region Shotgun Shell

	[DataMember] public SyncedEntry<int> ShellPrice { get; private set; }
	[DataMember] public SyncedEntry<int> ShellMaxDiscount { get; private set; }
	[DataMember] public SyncedEntry<int> ShellMinValue { get; private set; }
	[DataMember] public SyncedEntry<int> ShellMaxValue { get; private set; }
	[DataMember] public SyncedEntry<int> ShellRarity { get; private set; }

	# endregion

	public SyncConfig(ConfigFile cfg) : base(MyPluginInfo.PLUGIN_GUID)
	{
		ConfigManager.Register(this);

		# region Shotgun

		ShotgunPrice = cfg.BindSyncedEntry(
			Category.Shotgun,
			"Price",
			700,
			"Cost of a shotgun in a store. (-1 = remove from sale)"
		);
		ShotgunMaxDiscount = cfg.BindSyncedEntry(
			Category.Shotgun,
			"MaxDiscount",
			80,
			new ConfigDescription("Maximum discount percentage in store (vanilla = 80)",
				new AcceptableValueRange<int>(0, 90))
		);
		ShotgunMinValue = cfg.BindSyncedEntry(
			Category.Shotgun,
			"MinValueScrap",
			40,
			"Minimum scrap cost (must be >= 0) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)"
		);
		ShotgunMaxValue = cfg.BindSyncedEntry(
			Category.Shotgun,
			"MaxValueScrap",
			70,
			"Maximum scrap cost (must be >= min value) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)"
		);
		ShotgunWeight = cfg.BindSyncedEntry(
			Category.Shotgun,
			"Weight",
			16f,
			new ConfigDescription("[BETA] Scrap weight", new AcceptableValueRange<float>(0f, 100f))
		);
		ShotgunRarity = cfg.BindSyncedEntry(
			Category.Shotgun,
			"Rarity",
			-1,
			"Rarity of shotgun spawn on moons (higher = more common). A shotgun will also appear in gifts. (-1 = disable)"
		);

		# endregion

		# region Shotgun Tweaks

		MisfireOff = cfg.BindSyncedEntry(
			Category.ShotgunTweaks,
			nameof(MisfireOff),
			true,
			"If set to true, it disables the shotgun misfire (vanilla = false)"
		);
		InfiniteAmmo = cfg.BindSyncedEntry(
			Category.ShotgunTweaks,
			nameof(InfiniteAmmo),
			false,
			"If set to true, there will be endless rounds in the shotgun");
		ReloadKeybind = cfg.BindSyncedEntry(
			Category.ShotgunTweaks,
			nameof(ReloadKeybind),
			"R",
			"Changes the reload key to the one you specify (vanilla = E)");
		ShowAmmoCount = cfg.BindSyncedEntry(
			Category.ShotgunTweaks,
			nameof(ShowAmmoCount),
			true,
			"If set to true, the number of loaded cartridges will be displayed in the tooltip"
		);
		AmmoCheckAnimation = cfg.BindSyncedEntry(
			Category.ShotgunTweaks,
			nameof(AmmoCheckAnimation),
			false,
			"[BETA] Enables animation of checking cartridges in a shotgun by pressing the reload key (Does not work when InfiniteAmmo = true)"
		);
		ReloadNoLimit = cfg.BindSyncedEntry(
			Category.ShotgunTweaks,
			nameof(ReloadNoLimit),
			false,
			"If set to true, there will be no restrictions on the number of rounds in the shotgun"
		);
		DisableFriendlyFire = cfg.BindSyncedEntry(
			Category.ShotgunTweaks,
			nameof(DisableFriendlyFire),
			false,
			"Turns off friendly fire"
		);
		SkipReloadAnimation = cfg.BindSyncedEntry(
			Category.ShotgunTweaks,
			nameof(SkipReloadAnimation),
			false,
			"Skips the shotgun reload animation"
		);

		# endregion

		# region Shotgun Shell

		ShellPrice = cfg.BindSyncedEntry(
			Category.Shell,
			"Price",
			50,
			"Cost of a shotgun shell in a store. (-1 = remove from sale)"
		);
		ShellMaxDiscount = cfg.BindSyncedEntry(
			Category.Shell,
			"MaxDiscount",
			80,
			new ConfigDescription("Maximum discount percentage in store (vanilla = 80)",
				new AcceptableValueRange<int>(0, 90))
		);
		ShellMinValue = cfg.BindSyncedEntry(
			Category.Shell,
			"MinValueScrap",
			15,
			"Minimum scrap cost (must be >= 0) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)"
		);
		ShellMaxValue = cfg.BindSyncedEntry(
			Category.Shell,
			"MaxValueScrap",
			25,
			"Maximum scrap cost (must be >= min value) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)"
		);
		ShellRarity = cfg.BindSyncedEntry(
			Category.Shell,
			"Rarity",
			2,
			"Rarity of shotgun shell spawns on moons (higher = more common). Shell will also appear in gifts. (-1 = disable)"
		);

		# endregion
	}
}