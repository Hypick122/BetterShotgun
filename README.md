# BetterShotgun

<details>
<summary><strong>English</strong></summary>

Should support custom moons (I haven't tested)

## Config

All available mod settings can be found in the config ```Hypick.BetterShotgun.cfg```

! If the Rarity parameter is enabled, the item will appear in gifts

Available settings in the config:

- Shotgun
  - Price (default = 700)
    - Cost of a shotgun in the store
  - (Not working) MinValueScrap (default = 40)
    - Minimum cost of a shotgun found on the moon
  - (Not working) MaxValueScrap (default = 70)
    - Maximum cost of a shotgun found on the moon
  - Rarity (default = -1)
    - Rarity of shotgun spawn on moons (higher = more often)
  - MisfireOff (default = true, vanilla = false)
    - Disables misfire
  - InfiniteAmmo (default = false)
    - Endless ammo
  - ShowAmmoCount (default = true)
    - The number of cartridges in the shotgun will be displayed at the top right
  - [BETA] AmmoCheckAnimation (default = true)
    - Adds ammo check animation to the reload button
  - ReloadKeybind (default = false, vanilla = E)
    - Changes the reload key to the one you set
  - ReloadNoLimit (default = false)
    - Allows you to endlessly reload your shotgun
  - SkipReloadAnimation (default = false)
    - Skips reload animation
- Shell
  - Price (default: 50)
    - Cost of a cartridge in the store
  - (Not working) MinValueScrap (default = 15)
    - Minimum cost of a cartridge found on the moon
  - (Not working) MaxValueScrap (default = 25)
    - Maximum cost of a cartridge found on the moon
  - Rarity (default = 2)
    - Rarity of the appearance of cartridges on moons (higher = more often)

</details>

<details>
<summary><strong>Русский</strong></summary>

Должен поддерживать кастомные луны (я не тестировал)

## Конфиг

Все доступные настройки мода можно найти в конфиге ```Hypick.BetterShotgun.cfg```

! Если параметр Rarity включен, то предмет будет появляться еще в подарах

Доступные настройки в конфиге:

- Shotgun
  - Price (по умолчанию = 700)
    - Стоимость дробовика в магазине
  - (Не работает) MinValueScrap (по умолчанию = 40)
    - Минимальная стоимость найденного на луне дробовика
  - (Не работает) MaxValueScrap (по умолчанию = 70)
    - Максимальная стоимость найденного на луне дробовика
  - Rarity (по умолчанию = -1)
    - Редкость появления дробовика на лунах (выше = чаще)
  - MisfireOff (по умолчанию = true, ванилла = false)
    - Отключает осечку
  - InfiniteAmmo (по умолчанию = false)
    - Бесконечные патроны
  - ShowAmmoCount (по умолчанию = true)
    - Справа сверху будет отображаться кол-во патронов в дробовике
  - [BETA] AmmoCheckAnimation (по умолчанию = true)
    - Добавляет анимацию проверки патронов на кнопку перезарядки
  - ReloadKeybind (по умолчанию = false, ванилла = E)
    - Меняет клавишу перезарядки на установленную вами
  - ReloadNoLimit (по умолчанию = false)
    - Позволяет бесконечно перезаряжать дробовик
  - SkipReloadAnimation (по умолчанию = false)
    - Пропускает анимацию перезарядки
- Shell
  - Price (по умолчанию: 50)
    - Стоимость патрона в магазине
  - (Не работает) MinValueScrap (по умолчанию = 15)
    - Минимальная стоимость найденного на луне патрона
  - (Не работает) MaxValueScrap (по умолчанию = 25)
    - Максимальная стоимость найденного на луне патрона
  - Rarity (по умолчанию = 2)
    - Редкость появления патронов на лунах (выше = чаще)

</details>

## Known issues

- MinValueScrap and MaxValueScrap do not work

## Changelog

## [1.2.0] - 14.02.2024 | Current version

- Finally fixed AmmoCheckAnimation (most likely :)
- Removed the shotgun loading sound when viewing ammo
- Added two new features:
  - ReloadNoLimit
    - Allows you to endlessly reload your shotgun
  - SkipReloadAnimation
    - Skips reload animation

## [1.1.1] - 13.02.2024

- Fixed an issue where AmmoCheckAnimation still worked even if it was disabled in the config ([#7](https://github.com/Hypick122/BetterShotgun/issues/7))
- Fixed an issue where the shotgun would misfire when falling to the ground with MisfireOff enabled in the config ([#8](https://github.com/Hypick122/BetterShotgun/issues/8))

## [1.1.0] - 12.02.2024

- Added 5 new features:
  - MisfireOff (default: true, vanilla: false)
    - Disables misfire
  - InfiniteAmmo (default: false)
    - Endless ammo
  - ShowAmmoCount (default: true)
    - The number of cartridges in the shotgun will be displayed at the top right
  - [BETA] AmmoCheckAnimation
    - Enables animation of checking cartridges on the reload button
  - ReloadKeybind
    - Changes the reload key to the one you set

## [1.0.3] - 29.01.2024

- LethalLib 0.14.1 -> 0.14.2
- Descriptions in the config have been corrected

## [1.0.2] - 29.01.2024

- LethalLib 0.13.2 -> 0.14.1
- Minor changes to the code

## [1.0.1] - 25.01.2024

- The default parameters in the config have been slightly changed
- Added parameters for the cost of scrap metal on the moons in the config
