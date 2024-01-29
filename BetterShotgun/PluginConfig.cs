using BepInEx.Configuration;

namespace Hypick;

public static class Categories
{
    public const string Shotgun = nameof(Shotgun);
    public const string Shell = nameof(Shell);
}

public class PluginConfig
{
    public int ShotgunPrice { get; private set; }
    public int ShotgunMinValue { get; private set; }
    public int ShotgunMaxValue { get; private set; }
    public int ShotgunRarity { get; private set; }
    // public float ShotgunPresentChance { get; private set; }

    public int ShotgunShellPrice { get; private set; }
    public int ShotgunShellMinValue { get; private set; }
    public int ShotgunShellMaxValue { get; private set; }
    public int ShotgunShellRarity { get; private set; }
    // public float ShotgunShellPresentChance { get; private set; }

    public PluginConfig(ConfigFile cfg)
    {
        ShotgunPrice = cfg.Bind<int>(Categories.Shotgun, "Price", 700, "Cost of a shotgun in a store. Set -1 to remove from sale").Value;
        ShotgunMinValue = cfg.Bind<int>(Categories.Shotgun, "MinValueScrap", 40, "Minimum shotgun value (must be > 0)").Value;
        ShotgunMaxValue = cfg.Bind<int>(Categories.Shotgun, "MaxValueScrap", 70, "Maximum shotgun value (must be > min value)").Value;
        ShotgunRarity = cfg.Bind<int>(Categories.Shotgun, "Rarity", -1, "Rarity of shotgun spawn on moons (higher = more common). A shotgun will also appear in gifts. Set to -1 to disable").Value;
        // ShotgunPresentChance = cfg.Bind<float>(Categories.Shotgun, "PresentChance", 0.5f, new ConfigDescription("[BETA]", new AcceptableValueRange<float>(0f, 100f))).Value;

        ShotgunShellPrice = cfg.Bind<int>(Categories.Shell, "Price", 50, "Cost of a shotgun shell in a store. Set -1 to remove from sale").Value;
        ShotgunShellMinValue = cfg.Bind<int>(Categories.Shell, "MinValueScrap", 15, "Minimum shotgun shell value (must be > 0)").Value;
        ShotgunShellMaxValue = cfg.Bind<int>(Categories.Shell, "MaxValueScrap", 25, "Maximum shotgun shell value (must be > min value)").Value;
        ShotgunShellRarity = cfg.Bind<int>(Categories.Shell, "Rarity", 2, "Rarity of shotgun shell spawns on moons (higher = more common). Shell will also appear in gifts. Set to -1 to disable").Value;
        // ShotgunShellPresentChance = cfg.Bind<float>(Categories.Shell, "PresentChance", 1f, new ConfigDescription("[BETA]", new AcceptableValueRange<float>(0f, 100f))).Value;
    }
}
