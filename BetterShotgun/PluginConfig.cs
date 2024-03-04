using System;
using BepInEx.Configuration;
using Unity.Collections;
using Unity.Netcode;

namespace Hypick;

public static class Category
{
	public const string Shotgun = "1 >> Shotgun << 1";
	public const string ShotgunTweaks = "3 >> Shotgun Tweaks << 3";
	public const string Shell = "2 >> Shell << 2";
}

[Serializable]
public class Config : SyncedInstance<Config>
{
	# region Shotgun

	public int ShotgunPrice { get; }
	public int ShotgunMaxDiscount { get; }
	public int ShotgunMinValue { get; }
	public int ShotgunMaxValue { get; }
	public float ShotgunWeight { get; }
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

	public int ShellPrice { get; }
	public int ShellMaxDiscount { get; }
	public int ShellMinValue { get; }
	public int ShellMaxValue { get; }
	public int ShellRarity { get; }

	# endregion

	public Config(ConfigFile cfg)
	{
		InitInstance(this);

		# region Shotgun

		ShotgunPrice = cfg.Bind<int>(Category.Shotgun, "Price", 700,
			"Cost of a shotgun in a store. (-1 = remove from sale)").Value;
		ShotgunMaxDiscount = cfg.Bind<int>(Category.Shotgun, "MaxDiscount", 80,
			new ConfigDescription("Maximum discount percentage in store (vanilla = 80)",
				new AcceptableValueRange<int>(0, 90))).Value;
		ShotgunMinValue = cfg.Bind<int>(Category.Shotgun, "MinValueScrap", 40,
				"Minimum scrap cost (must be >= 0) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)")
			.Value;
		ShotgunMaxValue = cfg.Bind<int>(Category.Shotgun, "MaxValueScrap", 70,
				"Maximum scrap cost (must be >= min value) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)")
			.Value;
		ShotgunWeight = cfg.Bind<float>(Category.Shotgun, "Weight", 16f,
			new ConfigDescription("[BETA] Scrap weight", new AcceptableValueRange<float>(0f, 100f))).Value;
		ShotgunRarity = cfg.Bind<int>(Category.Shotgun, "Rarity", -1,
				"Rarity of shotgun spawn on moons (higher = more common). A shotgun will also appear in gifts. (-1 = disable)")
			.Value;

		# endregion

		# region Shotgun Tweaks

		MisfireOff = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(MisfireOff), true,
			"If set to true, it disables the shotgun misfire (vanilla = false)").Value;
		InfiniteAmmo = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(InfiniteAmmo), false,
			"If set to true, there will be endless rounds in the shotgun").Value;
		ReloadKeybind = cfg.Bind<string>(Category.ShotgunTweaks, nameof(ReloadKeybind), "R",
			"Changes the reload key to the one you specify (vanilla = E)").Value;
		ShowAmmoCount = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(ShowAmmoCount), true,
			"If set to true, the number of loaded cartridges will be displayed in the tooltip").Value;
		AmmoCheckAnimation = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(AmmoCheckAnimation), false,
				"[BETA] Enables animation of checking cartridges in a shotgun by pressing the reload key (Does not work when InfiniteAmmo = true)")
			.Value;
		ReloadNoLimit = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(ReloadNoLimit), false,
			"If set to true, there will be no restrictions on the number of rounds in the shotgun").Value;
		DisableFriendlyFire = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(DisableFriendlyFire), false,
			"Turns off friendly fire").Value;
		SkipReloadAnimation = cfg.Bind<bool>(Category.ShotgunTweaks, nameof(SkipReloadAnimation), false,
			"Skips the shotgun reload animation").Value;

		# endregion

		# region Shotgun Shell

		ShellPrice = cfg.Bind<int>(Category.Shell, "Price", 50,
			"Cost of a shotgun shell in a store. (-1 = remove from sale)").Value;
		ShellMaxDiscount = cfg.Bind<int>(Category.Shell, "MaxDiscount", 80,
			new ConfigDescription("Maximum discount percentage in store (vanilla = 80)",
				new AcceptableValueRange<int>(0, 90))).Value;
		ShellMinValue = cfg.Bind<int>(Category.Shell, "MinValueScrap", 15,
				"Minimum scrap cost (must be >= 0) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)")
			.Value;
		ShellMaxValue = cfg.Bind<int>(Category.Shell, "MaxValueScrap", 25,
				"Maximum scrap cost (must be >= min value) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)")
			.Value;
		ShellRarity = cfg.Bind<int>(Category.Shell, "Rarity", 2,
				"Rarity of shotgun shell spawns on moons (higher = more common). Shell will also appear in gifts. (-1 = disable)")
			.Value;

		# endregion
	}

	public static void RequestSync()
	{
		if (!IsClient) return;

		using FastBufferWriter stream = new(IntSize, Allocator.Temp);
		MessageManager.SendNamedMessage("ModName_OnRequestConfigSync", 0uL, stream);
	}

	public static void OnRequestSync(ulong clientId, FastBufferReader _)
	{
		if (!IsHost) return;

		Plugin.Log.LogInfo($"Config sync request received from client: {clientId}");

		byte[] array = SerializeToBytes(Instance);
		int value = array.Length;

		using FastBufferWriter stream = new(value + IntSize, Allocator.Temp);

		try
		{
			stream.WriteValueSafe(in value, default);
			stream.WriteBytesSafe(array);

			MessageManager.SendNamedMessage("BetterShotgun_OnReceiveConfigSync", clientId, stream);
		}
		catch (Exception e)
		{
			Plugin.Log.LogInfo($"Error occurred syncing config with client: {clientId}\n{e}");
		}
	}

	public static void OnReceiveSync(ulong _, FastBufferReader reader)
	{
		if (!reader.TryBeginRead(IntSize))
		{
			Plugin.Log.LogError("Config sync error: Could not begin reading buffer.");
			return;
		}

		reader.ReadValueSafe(out int val, default);
		if (!reader.TryBeginRead(val))
		{
			Plugin.Log.LogError("Config sync error: Host could not sync.");
			return;
		}

		byte[] data = new byte[val];
		reader.ReadBytesSafe(ref data, val);

		SyncInstance(data);

		Plugin.Log.LogInfo("Successfully synced config with host.");
	}
}