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
    public int ShotgunRarity { get; private set; }
    public int ShellPrice { get; private set; }
    public int ShellRarity { get; private set; }

    public PluginConfig(ConfigFile cfg)
    {
        ShotgunPrice = cfg.Bind<int>(Categories.Shotgun, "Price", 700, "Cost of a shotgun in a store. Set -1 to remove shotgun from sale").Value;
        ShotgunRarity = cfg.Bind<int>(Categories.Shotgun, "Rarity", 2, "Rare appearance of the shotgun on moons. (Higher = More Common). Set to 0 to disable it from appearing").Value;
        ShellPrice = cfg.Bind<int>(Categories.Shell, "Price", 50, "Cost of a shotgun shell in a store. Set -1 to remove shotgun shell from sale").Value;
        ShellRarity = cfg.Bind<int>(Categories.Shell, "Rarity", 5, "Rare appearance of shotgun shells on moons. (Higher = More Common). Set to 0 to disable their spawning").Value;
    }
}