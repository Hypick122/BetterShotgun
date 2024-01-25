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
        ShotgunPrice = cfg.Bind<int>(Categories.Shotgun, "Price", 700, "Cost of a shotgun in a store. Set -1 to remove shotgun from sale").Value;
        ShotgunMinValue = cfg.Bind<int>(Categories.Shotgun, "MinValueScrap", 40, "Minimum cost of scrap metal.").Value;
        ShotgunMaxValue = cfg.Bind<int>(Categories.Shotgun, "MaxValueScrap", 70, "Maximum value of scrap metal. The parameter must be greater than the minimum").Value;
        ShotgunRarity = cfg.Bind<int>(Categories.Shotgun, "Rarity", -1, "Rare appearance of the shotgun on moons (Higher = More Common). A shotgun will also appear in gifts. Set to -1 to disable it from appearing").Value; // -1
        // ShotgunPresentChance = cfg.Bind<float>(Categories.Shotgun, "PresentChance", 0.5f, new ConfigDescription("[BETA]", new AcceptableValueRange<float>(0f, 100f))).Value;


        ShotgunShellPrice = cfg.Bind<int>(Categories.Shell, "Price", 50, "Cost of a shotgun shell in a store. Set -1 to remove shotgun shell from sale").Value;
        ShotgunShellMinValue = cfg.Bind<int>(Categories.Shell, "MinValueScrap", 15, "Minimum cost of scrap metal.").Value;
        ShotgunShellMaxValue = cfg.Bind<int>(Categories.Shell, "MaxValueScrap", 25, "Maximum value of scrap metal. The parameter must be greater than the minimum").Value;
        ShotgunShellRarity = cfg.Bind<int>(Categories.Shell, "Rarity", 2, "Rare appearance of shotgun shells on moons (Higher = More Common). Ammo will also appear in gifts. Set to -1 to disable their spawning").Value; // 3
        // ShotgunShellPresentChance = cfg.Bind<float>(Categories.Shell, "PresentChance", 1f, new ConfigDescription("[BETA]", new AcceptableValueRange<float>(0f, 100f))).Value;
    }
}