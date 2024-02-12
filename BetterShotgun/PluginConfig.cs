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
    public bool MisfireOff { get; private set; }
    public bool InfiniteAmmo { get; private set; }
    public string ReloadKeybind { get; private set; }
    public bool ShowAmmoCount { get; private set; }
    public bool AmmoCheckAnimation { get; private set; }

    public int ShotgunShellPrice { get; private set; }
    public int ShotgunShellMinValue { get; private set; }
    public int ShotgunShellMaxValue { get; private set; }
    public int ShotgunShellRarity { get; private set; }

    public PluginConfig(ConfigFile cfg)
    {
        ShotgunPrice = cfg.Bind<int>(Categories.Shotgun, "Price", 700, "Cost of a shotgun in a store. (-1 = remove from sale)").Value;
        ShotgunMinValue = cfg.Bind<int>(Categories.Shotgun, "MinValueScrap", 40, "Minimum shotgun value (must be > 0)").Value;
        ShotgunMaxValue = cfg.Bind<int>(Categories.Shotgun, "MaxValueScrap", 70, "Maximum shotgun value (must be > min value)").Value;
        ShotgunRarity = cfg.Bind<int>(Categories.Shotgun, "Rarity", -1, "Rarity of shotgun spawn on moons (higher = more common). A shotgun will also appear in gifts. (-1 = disable)").Value;
        MisfireOff = cfg.Bind<bool>(Categories.Shotgun, "MisfireOff", true, "If set to true, disables shotgun cutoff (vanilla = false)").Value;
        InfiniteAmmo = cfg.Bind<bool>(Categories.Shotgun, nameof(InfiniteAmmo), false, "If set to true, the shotgun will have infinite ammo").Value;
        ReloadKeybind = cfg.Bind<string>(Categories.Shotgun, nameof(ReloadKeybind), "R", "Changes the reload key to the one you specify (vanilla = E)").Value;
        ShowAmmoCount = cfg.Bind<bool>(Categories.Shotgun, nameof(ShowAmmoCount), true, "If set to true, the number of cartridges in the shotgun will be displayed in the upper right text").Value;
        AmmoCheckAnimation = cfg.Bind<bool>(Categories.Shotgun, nameof(AmmoCheckAnimation), true, "Adds an ammo check animation to the reload button").Value;

        ShotgunShellPrice = cfg.Bind<int>(Categories.Shell, "Price", 50, "Cost of a shotgun shell in a store. (-1 = remove from sale)").Value;
        ShotgunShellMinValue = cfg.Bind<int>(Categories.Shell, "MinValueScrap", 15, "Minimum shotgun shell value (must be > 0)").Value;
        ShotgunShellMaxValue = cfg.Bind<int>(Categories.Shell, "MaxValueScrap", 25, "Maximum shotgun shell value (must be > min value)").Value;
        ShotgunShellRarity = cfg.Bind<int>(Categories.Shell, "Rarity", 2, "Rarity of shotgun shell spawns on moons (higher = more common). Shell will also appear in gifts. (-1 = disable)").Value;
    }
}
