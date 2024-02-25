# BetterShotgun

<details>
<summary><strong>English</strong></summary>

Should support custom moons (I haven't tested)

## Config

All available mod settings can be found in the config ```Hypick.BetterShotgun.cfg```

! If the Rarity parameter is enabled, the item will appear in gifts

Available settings in the config:

- Shotgun
  - Price (default = 700, disable = -1)
    - Cost of a shotgun in the store
  - MinValueScrap (default = 40) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)
    - Minimum scrap cost
  - MaxValueScrap (default = 70) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)
    - Maximum scrap cost
  - Rarity (default = -1, disable = -1)
    - Rarity of shotgun spawn on moons (higher = more often)
  - **[BETA]** Weight
    - Scrap weight
  - MaxDiscount (default = 80, vanilla = 80)
    - Maximum discount in the store
- Shotgun Tweaks
  - MisfireOff (default = true, vanilla = false)
    - Disables misfire
  - InfiniteAmmo (default = false)
    - Endless ammo
  - ShowAmmoCount (default = true)
    - The number of cartridges in the shotgun will be displayed at the top right
  - **[BETA]** AmmoCheckAnimation (default = false)
    - Adds ammo check animation to the reload button
  - ReloadKeybind (default = false, vanilla = E)
    - Changes the reload key to the one you set
  - ReloadNoLimit (default = false)
    - Allows you to endlessly reload your shotgun
  - SkipReloadAnimation (default = false)
    - Skips reload animation
  - DisableFriendlyFire (default = false)
    - Turns off friendly fire
- Shell
  - Price (default: 50, disable = -1)
    - Cost of a cartridge in the store
  - MinValueScrap (default = 15) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)
    - Minimum scrap cost
  - MaxValueScrap (default = 25) (In the game, the value is scaled down, so it is calculated using the formula value * 100 / 40)
    - Maximum scrap cost
  - Rarity (default = 2, disable = -1)
    - Rarity of the appearance of cartridges on moons (higher = more often)
  - MaxDiscount (default = 80, vanilla = 80)
    - Maximum discount in the store

</details>

<details>
<summary><strong>Русский</strong></summary>

Должен поддерживать кастомные луны (я не тестировал)

## Конфиг

Все доступные настройки мода можно найти в конфиге ```Hypick.BetterShotgun.cfg```

! Если параметр Rarity включен, то предмет будет появляться еще в подарах

Доступные настройки в конфиге:

- Shotgun
  - Price (по умолчанию = 700, отключить = -1)
    - Стоимость дробовика в магазине
  - MinValueScrap (по умолчанию = 40) (В игре значение масштабируется в меньшую сторону, поэтому высчитывается по формуле value * 100 / 40)
    - Минимальная стоимость лома
  - MaxValueScrap (по умолчанию = 70) (В игре значение масштабируется в меньшую сторону, поэтому высчитывается по формуле value * 100 / 40)
    - Максимальная стоимость лома
  - Rarity (по умолчанию = -1, отключить = -1)
    - Редкость появления дробовика на лунах (выше = чаще)
  - **[BETA]** Weight (по умолчанию = 16)
    - Вес лома
  - MaxDiscount (по умолчанию = 80, ванилла = 80)
    - Максимальная скидка в магазине
- Shotgun Tweaks
  - MisfireOff (по умолчанию = true, ванилла = false)
    - Отключает осечку
  - InfiniteAmmo (по умолчанию = false)
    - Бесконечные патроны
  - ShowAmmoCount (по умолчанию = true)
    - Справа сверху будет отображаться кол-во патронов в дробовике
  - **[BETA]** AmmoCheckAnimation (по умолчанию = false)
    - Добавляет анимацию проверки патронов на кнопку перезарядки
  - ReloadKeybind (по умолчанию = false, ванилла = E)
    - Меняет клавишу перезарядки на установленную вами
  - ReloadNoLimit (по умолчанию = false)
    - Позволяет бесконечно перезаряжать дробовик
  - SkipReloadAnimation (по умолчанию = false)
    - Пропускает анимацию перезарядки
  - DisableFriendlyFire (по умолчанию = false)
    - Отключает огонь по своим
- Shell
  - Price (по умолчанию: 50, отключить = -1)
    - Стоимость патрона в магазине
  - MinValueScrap (по умолчанию = 15) (В игре значение масштабируется в меньшую сторону, поэтому высчитывается по формуле value * 100 / 40)
    - Минимальная стоимость найденного на луне патрона
  - MaxValueScrap (по умолчанию = 25) (В игре значение масштабируется в меньшую сторону, поэтому высчитывается по формуле value * 100 / 40)
    - Максимальная стоимость найденного на луне патрона
  - Rarity (по умолчанию = 2, отключить = -1)
    - Редкость появления патронов на лунах (выше = чаще)
  - MaxDiscount (default = 80, vanilla = 80)
    - Maximum discount in the store

</details>

## Recommended Mods

- NutcrackerFixes
- LCAmmoCheck (this mod is better than the built-in AmmoCheckAnimation)

## Contributing

If you have an idea for a mod or find a problem, you can open an [issue](https://github.com/Hypick122/BetterShotgun/issues) or [pull request](https://github.com/Hypick122/BetterShotgun/pulls) on Github.

## License

This project is licensed under the [MIT License](https://github.com/Hypick122/BetterShotgun?tab=MIT-1-ov-file).

## Latest version

### [1.4.2](https://github.com/Hypick122/BetterShotgun/compare/v1.4.1...v1.4.2) (2024-02-26)

- AmmoCheckAnimation changed from "true" to "false" by default
- Changed the name of shotgun ammo in the store from "Ammo" to "Shell"
- Added more logging

