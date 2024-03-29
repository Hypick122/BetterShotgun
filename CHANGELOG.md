# Changelog

## [1.4.7](https://github.com/Hypick122/BetterShotgun/compare/v1.4.6...v1.4.7) (2024-03-20)


### Bug Fixes

* change compatibility with ReservedWeaponSlot ([#38](https://github.com/Hypick122/BetterShotgun/issues/38)) ([98efc50](https://github.com/Hypick122/BetterShotgun/commit/98efc50ffc04cefa7631cd55ffed7ed862185245))


### Miscellaneous Chores

* improve AmmoCheckAnimation ([abec62f](https://github.com/Hypick122/BetterShotgun/commit/abec62fc66f18e9c4269559aa51b5307d1d395cc))
* improve InfiniteAmmo ([#35](https://github.com/Hypick122/BetterShotgun/issues/35)) ([d9be920](https://github.com/Hypick122/BetterShotgun/commit/d9be920be0368b71320ba8afd185102201f2a6b8))
* prepare to 1.4.7 ([8066a03](https://github.com/Hypick122/BetterShotgun/commit/8066a03c663865be42ac05bdc69967473d1089f2))

## [1.4.6](https://github.com/Hypick122/BetterShotgun/compare/v1.4.5...v1.4.6) (2024-03-16)

### Bug Fixes

* Fixed a problem with extracting values from the
  config ([45bd1da](https://github.com/Hypick122/BetterShotgun/commit/45bd1da736bfd421411c732d19698b497d80ee82))

### Miscellaneous Chores

* Changed the setup of the ReloadKeybind key and added an error
  exception ([#30](https://github.com/Hypick122/BetterShotgun/issues/30)) ([8bedb4f](https://github.com/Hypick122/BetterShotgun/commit/8bedb4f49641d7893963dfeed4539be1e8826dfb))
* Bump LethalLib from 1.4.2 to 1.4.3
* Bump LethalCompany_InputUtils from 0.6.3 to 0.7.1

## [1.4.5](https://github.com/Hypick122/BetterShotgun/compare/v1.4.4...v1.4.5) (2024-03-13)

### Bug Fixes

* Fixed crash due to
  ReloadKeybind ([#30](https://github.com/Hypick122/BetterShotgun/issues/30)) ([f18f53f](https://github.com/Hypick122/BetterShotgun/commit/f18f53f8f0cdd491bb338a5738531fd38bd32895))

### Miscellaneous Chores

* Synchronization has been replaced with [CSync](https://thunderstore.io/c/lethal-company/p/Sigurd/CSync/), since the
  last synchronization did not
  work ([142cf52](https://github.com/Hypick122/BetterShotgun/commit/142cf5224ac6fef035b497af79ab4a1b6cf27fb3))

## [1.4.4](https://github.com/Hypick122/BetterShotgun/compare/v1.4.3...v1.4.4) (2024-03-04)

### Miscellaneous Chores

* Added configuration synchronization between clients and
  host ([#28](https://github.com/Hypick122/BetterShotgun/issues/28)) ([12a50d4](https://github.com/Hypick122/BetterShotgun/commit/12a50d4f7fb639f98f3f2e7385e23bb9cf3fdc20))
* Removed the check for ReservedItemSlotCore in
  ReloadKeybind ([b292bae](https://github.com/Hypick122/BetterShotgun/commit/b292baec7c2d9a4af49291259327c5d38eeb627f))
* Fixed compatibility with ReservedWeaponSlot: the name of the cartridges automatically changes to "
  Ammo" ([#22](https://github.com/Hypick122/BetterShotgun/issues/22)) ([6a6d347](https://github.com/Hypick122/BetterShotgun/commit/6a6d347868522c8eb4e392ea6f313645b95aa0db))

## [1.4.3](https://github.com/Hypick122/BetterShotgun/compare/v1.4.2...v1.4.3) (2024-02-28)

### Miscellaneous Chores

* Added check for the presence of
  ReservedItemSlotCore ([17f556a](https://github.com/Hypick122/BetterShotgun/commit/17f556aeebe41b15260742ddf32aec82cc2b97f5))

## [1.4.2](https://github.com/Hypick122/BetterShotgun/compare/v1.4.1...v1.4.2) (2024-02-25)

### Miscellaneous Chores

* AmmoCheckAnimation changed from "true" to "false" by default
* Changed the name of shotgun ammo in the store from "Ammo" to "Shell"
* Added more logging

## [1.4.1](https://github.com/Hypick122/BetterShotgun/compare/v1.4.0...v1.4.1) (2024-02-18)

### Bug Fixes

* Fixed an issue with key bindings not changing ([#17](https://github.com/Hypick122/BetterShotgun/issues/17))

## [1.4.0](https://github.com/Hypick122/BetterShotgun/compare/v1.3.0...v1.4.0) (2024-02-17)

### Miscellaneous Chores

* The structure of the configuration file has been changed once again
* Added LethalCompany_InputUtils to dependencies to improve the key binding change function

### Features

* DisableFriendlyFire (default = false)
    * Turns off friendly fire

### Bug Fixes

* Fixed issue [#15](https://github.com/Hypick122/BetterShotgun/issues/15) (it seems)
* Fixed an issue where when one of the players reloads a shotgun, ShowAmmoCount would incorrectly display the number of
  ammo for all shotguns

## [1.3.0](https://github.com/Hypick122/BetterShotgun/compare/v1.2.0...v1.3.0) (2024-02-15)

### Miscellaneous Chores

* Changed the priority of ShootGunPrefix (by [@JuanCalle1606](https://github.com/JuanCalle1606)
  in [#13](https://github.com/Hypick122/BetterShotgun/pull/13)), thereby making it more compatible with mods like
  HexiBetterShotgun
* Changed the calculation of MinValueScrap and MaxValueScrap (now using the formula value * 100 / 40)
* The structure of the configuration file has been slightly changed

### Features

* **[BETA]** Weight (default = 16) (shotgun only)
    * Scrap weight
* MaxDiscount (default = 80, vanilla = 80)
    * Maximum discount in the store

## [1.2.0](https://github.com/Hypick122/BetterShotgun/compare/v1.1.1...v1.2.0) (2024-02-13)

### Miscellaneous Chores

* Finally fixed AmmoCheckAnimation (most likely :))
* Removed the shotgun loading sound when viewing ammo

### Features

* ReloadNoLimit (default = false)
    * Allows you to endlessly reload your shotgun
* SkipReloadAnimation (default = false)
    * Skips reload animation

## [1.1.1](https://github.com/Hypick122/BetterShotgun/compare/v1.1.0...v1.1.1) (2024-02-13)

### Bug Fixes ([d78ef12](https://github.com/Hypick122/BetterShotgun/commit/d78ef1249c18c95a8d66f1a4cb75b5acd51f388a))

* Fixed an issue where AmmoCheckAnimation still worked even if it was disabled in the
  config ([#7](https://github.com/Hypick122/BetterShotgun/issues/7))
* Fixed an issue where the shotgun would misfire when falling to the ground with MisfireOff enabled in the
  config ([#8](https://github.com/Hypick122/BetterShotgun/issues/8))

## [1.1.0](https://github.com/Hypick122/BetterShotgun/compare/v1.0.3...v1.1.0) (2024-02-12)

### Features

* MisfireOff (default = true, vanilla = false)
    * Disables misfire
* InfiniteAmmo (default = false)
    * Endless ammo
* ShowAmmoCount (default = true)
    * The number of cartridges in the shotgun will be displayed at the top right
* **[BETA]** AmmoCheckAnimation
    * Enables animation of checking cartridges on the reload button
* ReloadKeybind
    * Changes the reload key to the one you set

## [1.0.3](https://github.com/Hypick122/BetterShotgun/compare/v1.0.2...v1.0.3) (2024-01-29)

### Miscellaneous Chores

* LethalLib 0.14.1 -> 0.14.2
* Descriptions in the config have been corrected

## [1.0.2](https://github.com/Hypick122/BetterShotgun/compare/v1.0.1...v1.0.2) (2024-01-29)

### Miscellaneous Chores

* LethalLib 0.13.2 -> 0.14.1
* Minor changes to the
  code ([f96f05b](https://github.com/Hypick122/BetterShotgun/commit/f96f05b9ceeccac01f6912f6731790605f55c507))

## [1.0.1](https://github.com/Hypick122/BetterShotgun/compare/v1.0.0...v1.0.1) (2024-01-25)

### Miscellaneous Chores

* The default parameters in the config have been slightly changed
* Added parameters for the cost of scrap metal on the moons in the config

## 1.0.0 (2024-01-22)

### Miscellaneous Chores

* release ([4fb9b6d](https://github.com/Hypick122/BetterShotgun/commit/4fb9b6d1e632651fa9c1dceb8abd329ba81a1833))
