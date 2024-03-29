# BetterShotgun

<details>
<summary><strong>English</strong></summary>

The mod should support custom moons (I haven't tested it)

## Config

All available mod settings can be found in the config ```Hypick.BetterShotgun.cfg```

**Parameters marked as ``[Sync]`` are synchronized with the host**

! If the ```Rarity``` parameter is enabled, the item will appear in gifts

```MinValueScrap```, ```MaxValueScrap```: In the game, the value is scaled down, so it is calculated using the formula
value * 100 / 40

Available settings in the config:

* **Shotgun**
    * ```[Sync]``` **Price** (default = 700, disable = -1)
        * The cost of a shotgun in the store
    * ```[Sync]``` **MinValueScrap** (default = 40)
        * Minimum scrap cost
    * ```[Sync]``` **MaxValueScrap** (default = 70)
        * Maximum scrap cost
    * ```[Sync]``` **Rarity** (default = -1, disable = -1)
        * The rarity of the appearance of a shotgun on the moons (higher = more often)
    * ```[Sync]``` ```[BETA]``` **Weight** (default = 16)
        * Scrap weight
    * ```[Sync]``` **MaxDiscount** (default = 80, vanilla = 80)
        * Maximum discount in the store
* **Shotgun Tweaks**
    * ```[Sync]``` **MisfireOff** (default = true, vanilla = false)
        * Disables the misfire
    * ```[Sync]``` **InfiniteAmmo** (default = false)
        * Endless ammo
    * **ShowAmmoCount** (default = true)
        * The number of loaded ammo will be displayed in the tooltip
    * ```[BETA]``` **AmmoCheckAnimation** (default = false)
        * Adds ammo check animation to the reload button
    * **ReloadKeybind** (default = false, vanilla = E)
        * Changes the reload keybind to the one you set
    * ```[Sync]``` **ReloadNoLimit** (default = false)
        * Allows you to infinitely reload the shotgun
    * ```[Sync]``` **SkipReloadAnimation** (default = false)
        * Skips the reload animation
    * ```[Sync]``` **DisableFriendlyFire** (default = false)
        * Turns off friendly fire
* **Shell**
    * ```[Sync]``` **Price** (default: 50, disable = -1)
        * The cost of the cartridge in the store
    * ```[Sync]``` **MinValueScrap** (default = 15)
        * Minimum scrap cost
    * ```[Sync]``` **MaxValueScrap** (default = 25)
        * Maximum scrap cost
    * ```[Sync]``` **Rarity** (default = 2, disable = -1)
        * The rarity of the appearance of cartridges on the moons (higher = more often)
    * ```[Sync]``` **MaxDiscount** (default = 80, vanilla = 80)
        * Maximum discount in the store

</details>

<details>
<summary><strong>Русский</strong></summary>

Мод должен поддерживать кастомные луны (я не тестировал)

## Конфиг

Все доступные настройки мода можно найти в конфиге ```Hypick.BetterShotgun.cfg```

**Параметры, помеченные как ```[Sync]```, синхронизируются с хостом**

! Если параметр ```Rarity``` включен, то предмет будет появляться еще в подарах

```MinValueScrap```, ```MaxValueScrap```: В игре значение лома масштабируется в меньшую сторону, поэтому высчитывается
по формуле value * 100 / 40

Доступные настройки в конфиге:

* **Shotgun**
    * ```[Sync]``` **Price** (по умолчанию = 700, отключить = -1)
        * Стоимость дробовика в магазине
    * ```[Sync]``` **MinValueScrap** (по умолчанию = 40)
        * Минимальная стоимость лома
    * ```[Sync]``` **MaxValueScrap** (по умолчанию = 70)
        * Максимальная стоимость лома
    * ```[Sync]``` **Rarity** (по умолчанию = -1, отключить = -1)
        * Редкость появления дробовика на лунах (выше = чаще)
    * ```[Sync]``` ```[BETA]``` **Weight** (по умолчанию = 16)
        * Вес лома
    * **MaxDiscount** (по умолчанию = 80, ванилла = 80)
        * Максимальная скидка в магазине
* **Shotgun Tweaks**
    * ```[Sync]``` **MisfireOff** (по умолчанию = true, ванилла = false)
        * Отключает осечку
    * ```[Sync]``` **InfiniteAmmo** (по умолчанию = false)
        * Бесконечные патроны
    * **ShowAmmoCount** (по умолчанию = true)
        * Во всплывающей подсказке будет отображаться количество заряженных патронов
    * ```[BETA]``` **AmmoCheckAnimation** (по умолчанию = false)
        * Добавляет анимацию проверки патронов на кнопку перезарядки
    * **ReloadKeybind** (по умолчанию = false, ванилла = E)
        * Меняет клавишу перезарядки на установленную вами
    * ```[Sync]``` **ReloadNoLimit** (по умолчанию = false)
        * Позволяет бесконечно перезаряжать дробовик
    * ```[Sync]``` **SkipReloadAnimation** (по умолчанию = false)
        * Пропускает анимацию перезарядки
    * ```[Sync]``` **DisableFriendlyFire** (по умолчанию = false)
        * Отключает огонь по своим
* **Shell**
    * ```[Sync]``` **Price** (по умолчанию: 50, отключить = -1)
        * Стоимость патрона в магазине
    * ```[Sync]``` **MinValueScrap** (по умолчанию = 15)
        * Минимальная стоимость найденного на луне патрона
    * ```[Sync]``` **MaxValueScrap** (по умолчанию = 25)
        * Максимальная стоимость найденного на луне патрона
    * ```[Sync]``` **Rarity** (по умолчанию = 2, отключить = -1)
        * Редкость появления патронов на лунах (выше = чаще)
    * ```[Sync]``` **MaxDiscount** (default = 80, vanilla = 80)
        * Максимальная скидка в магазине

</details>

## Known issues

* The price, weight, etc. of the item are not synchronized between the host and the clients

## Recommended Mods

* NutcrackerFixes

## Contributing

If you have an idea for a mod or find a problem, you can open
an [issue](https://github.com/Hypick122/BetterShotgun/issues)
or [pull request](https://github.com/Hypick122/BetterShotgun/pulls) on Github.

## License

This project is licensed under the [MIT License](https://github.com/Hypick122/BetterShotgun?tab=MIT-1-ov-file).

## Latest version

### [1.4.7](https://github.com/Hypick122/BetterShotgun/compare/v1.4.6...v1.4.7) (2024-03-20)

* Improved animation checking shells (actually LCAmmoCheck_OLD).
* InfiniteAmmo ([#35](https://github.com/Hypick122/BetterShotgun/issues/35)):
  * Changed the number of shells from 2147483647 to 3 so that the shotgun has two rounds if InfiniteAmmo mode is disabled.
  * Now, when checking the shells in the shotgun from the very beginning, it will be filled with two shells, not one.
* The ReservedWeaponSlot check has been removed and the name of the shells has been replaced with "Shells".