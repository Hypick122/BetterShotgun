using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hypick.Patches;

[HarmonyPatch(typeof(ShotgunItem))]
internal class ShotgunItemPatch
{
	# region MisfireOff

	[HarmonyPatch("Update")]
	[HarmonyPrefix]
	public static void Update(ShotgunItem __instance)
	{
		if (SyncConfig.Instance.MisfireOff.Value && !__instance.safetyOn)
		{
			__instance.hasHitGroundWithSafetyOff = true;
			__instance.misfireTimer = float.MaxValue;
		}
	}

	# endregion

	# region InfiniteAmmo

	[HarmonyPatch("ItemActivate")]
	[HarmonyPrefix]
	public static void ItemActivate(ShotgunItem __instance)
	{
		if (SyncConfig.Instance.InfiniteAmmo.Value)
			__instance.shellsLoaded = int.MaxValue;
	}

	# endregion

	# region ShowAmmoCount

	[HarmonyPatch("SetControlTipsForItem")]
	[HarmonyPrefix]
	public static void SetControlTipsForItem(ShotgunItem __instance)
	{
		__instance.itemProperties.toolTips[1] = GetCustomTooltip(__instance);
	}

	[HarmonyPatch("SetSafetyControlTip")]
	[HarmonyPrefix]
	public static void SetSafetyControlTip(ShotgunItem __instance)
	{
		if (__instance.IsOwner)
			HUDManager.Instance.ChangeControlTip(2, GetCustomTooltip(__instance));
	}

	[HarmonyPatch("ReloadGunEffectsClientRpc")]
	[HarmonyPatch("ShootGun")]
	[HarmonyPostfix]
	public static void UpdateControlTipsForItem(ShotgunItem __instance)
	{
		if (SyncConfig.Default.ShowAmmoCount.Value)
			__instance.SetSafetyControlTip();
	}

	private static string GetCustomTooltip(ShotgunItem item)
	{
		var newToolTips = SyncConfig.Default.AmmoCheckAnimation.Value ? "Reload / Check" : "Reload";

		if (!SyncConfig.Default.ShowAmmoCount.Value)
			return $"{newToolTips}: [{Plugin.InputActionsInstance.ReloadKey.GetBindingDisplayString()}]";

		var maxAmmo = SyncConfig.Instance.ReloadNoLimit.Value ? "∞" : "2";
		var ammoInfo = SyncConfig.Instance.InfiniteAmmo.Value ? "∞" : $"{item.shellsLoaded}/{maxAmmo}";

		return $"{newToolTips} ({ammoInfo}): [{Plugin.InputActionsInstance.ReloadKey.GetBindingDisplayString()}]";
	}

	# endregion

	# region ReloadNoLimit

	[HarmonyPatch("ShootGun")]
	[HarmonyPrefix]
	[HarmonyPriority(Priority.High)]
	public static void ShootGunPrefix(ShotgunItem __instance, out int __state)
	{
		__state = __instance.shellsLoaded;
	}

	[HarmonyPatch("ShootGun")]
	[HarmonyPostfix]
	public static void ShootGunPostfix(ShotgunItem __instance, ref int __state)
	{
		__instance.shellsLoaded = Mathf.Max(0, __state - 1);
		__instance.SetSafetyControlTip();
	}

	[HarmonyPatch("reloadGunAnimation")]
	[HarmonyPrefix]
	public static bool ReloadGunAnimation(ShotgunItem __instance, ref IEnumerator __result)
	{
		__result = ReloadGunAnimationCustom(__instance);
		return false;
	}

	# endregion

	# region DisableFriendlyFire

	[HarmonyPatch("ShootGun")]
	public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
	{
		var found = false;
		foreach (var instruction in instructions)
		{
			if (SyncConfig.Instance.DisableFriendlyFire.Value && !found && instruction.ToString().Contains("playerHeldBy"))
			{
				found = true;
				yield return new CodeInstruction(OpCodes.Ldarg_0);
				yield return new CodeInstruction(OpCodes.Call,
					AccessTools.Method(typeof(ShotgunItemPatch), nameof(CheckFriendly)));
				yield return new CodeInstruction(OpCodes.Stloc_0);
			}

			yield return instruction;
		}
	}

	private static bool CheckFriendly(ShotgunItem __instance)
	{
		return __instance.playerHeldBy != null;
	}

	# endregion

	[HarmonyPatch("ItemInteractLeftRight")]
	[HarmonyPrefix]
	public static bool ItemInteractLeftRight(ShotgunItem __instance, bool right)
	{
		if (SyncConfig.Default.ReloadKeybind.Value.ToLower() != "e" && right)
			return false;

		if (SyncConfig.Instance.ReloadNoLimit.Value && right && !__instance.isReloading)
		{
			__instance.StartReloadGun();
			return false;
		}

		return true;
	}

	[HarmonyPatch("StartReloadGun")]
	[HarmonyPrefix]
	public static bool StartReloadGun(ShotgunItem __instance)
	{
		if ((SyncConfig.Default.AmmoCheckAnimation.Value && !__instance.ReloadedGun()) || (SyncConfig.Default.AmmoCheckAnimation.Value &&
			    !SyncConfig.Instance.ReloadNoLimit.Value && !SyncConfig.Instance.InfiniteAmmo.Value && __instance.shellsLoaded >= 2))
		{
			if (__instance.gunCoroutine != null)
				__instance.StopCoroutine(__instance.gunCoroutine);

			__instance.gunCoroutine = __instance.StartCoroutine(CheckAmmoAnimation(__instance));
			return false;
		}

		if (SyncConfig.Instance.InfiniteAmmo.Value && __instance.ReloadedGun())
			return false;

		return __instance.IsOwner;
	}

	private static IEnumerator CheckAmmoAnimation(ShotgunItem __instance)
	{
		__instance.isReloading = true;
		__instance.playerHeldBy.playerBodyAnimator.SetBool("ReloadShotgun", true);
		__instance.shotgunShellLeft.enabled = __instance.shellsLoaded > 0;
		__instance.shotgunShellRight.enabled = __instance.shellsLoaded > 1;

		yield return new WaitForSeconds(0.3f);

		// __instance.gunAudio.PlayOneShot(__instance.gunReloadSFX);
		__instance.gunAnimator.SetBool("Reloading", true);
		__instance.ReloadGunEffectsServerRpc();

		yield return new WaitForSeconds(2.35f);

		// __instance.gunAudio.PlayOneShot(__instance.gunReloadFinishSFX);
		__instance.gunAnimator.SetBool("Reloading", false);
		__instance.playerHeldBy.playerBodyAnimator.SetBool("ReloadShotgun", false);
		__instance.isReloading = false;
		__instance.ReloadGunEffectsServerRpc(start: false);
	}

	private static IEnumerator ReloadGunAnimationCustom(ShotgunItem __instance)
	{
		if (!SyncConfig.Instance.SkipReloadAnimation.Value)
		{
			__instance.isReloading = true;
			if (__instance.shellsLoaded <= 0)
			{
				__instance.playerHeldBy.playerBodyAnimator.SetBool("ReloadShotgun", true);
				__instance.shotgunShellLeft.enabled = false;
				__instance.shotgunShellRight.enabled = false;
			}
			else
			{
				__instance.playerHeldBy.playerBodyAnimator.SetBool("ReloadShotgun2", true);
				__instance.shotgunShellRight.enabled = false;
			}

			yield return new WaitForSeconds(0.3f);

			__instance.gunAudio.PlayOneShot(__instance.gunReloadSFX);
			__instance.gunAnimator.SetBool("Reloading", true);
			__instance.ReloadGunEffectsServerRpc(true);

			yield return new WaitForSeconds(0.95f);

			__instance.shotgunShellInHand.enabled = true;
			__instance.shotgunShellInHandTransform.SetParent(__instance.playerHeldBy.leftHandItemTarget);
			__instance.shotgunShellInHandTransform.localPosition = new Vector3(-0.0555f, 0.1469f, -0.0655f);
			__instance.shotgunShellInHandTransform.localEulerAngles = new Vector3(-1.956f, 143.856f, -16.427f);

			yield return new WaitForSeconds(0.95f);

			__instance.playerHeldBy.DestroyItemInSlotAndSync(__instance.ammoSlotToUse);
			__instance.ammoSlotToUse = -1;

			if (SyncConfig.Instance.ReloadNoLimit.Value)
				__instance.shellsLoaded++;
			else
				__instance.shellsLoaded = Mathf.Clamp(__instance.shellsLoaded + 1, 0, 2);

			__instance.shotgunShellLeft.enabled = true;
			if (__instance.shellsLoaded >= 2)
				__instance.shotgunShellRight.enabled = true;
			__instance.shotgunShellInHand.enabled = false;
			__instance.shotgunShellInHandTransform.SetParent(__instance.transform);

			yield return new WaitForSeconds(0.45f);

			__instance.gunAudio.PlayOneShot(__instance.gunReloadFinishSFX);
			__instance.gunAnimator.SetBool("Reloading", false);
			__instance.playerHeldBy.playerBodyAnimator.SetBool("ReloadShotgun", false);
			__instance.playerHeldBy.playerBodyAnimator.SetBool("ReloadShotgun2", false);
			__instance.isReloading = false;
			__instance.ReloadGunEffectsServerRpc(start: false);
		}
		else
		{
			Plugin.Log.LogInfo("Skip reload animation");
			__instance.isReloading = true;

			__instance.playerHeldBy.DestroyItemInSlotAndSync(__instance.ammoSlotToUse);
			__instance.ammoSlotToUse = -1;

			if (SyncConfig.Instance.ReloadNoLimit.Value)
				__instance.shellsLoaded++;
			else
				__instance.shellsLoaded = Mathf.Clamp(__instance.shellsLoaded + 1, 0, 2);

			__instance.shotgunShellLeft.enabled = true;
			if (__instance.shellsLoaded >= 2)
				__instance.shotgunShellRight.enabled = true;
			__instance.shotgunShellInHand.enabled = false;
			__instance.shotgunShellInHandTransform.SetParent(__instance.transform);

			// __instance.gunAudio.PlayOneShot(__instance.gunReloadFinishSFX);
			// __instance.gunAnimator.SetBool("Reloading", false);
			// __instance.playerHeldBy.playerBodyAnimator.SetBool("ReloadShotgun", false);
			// __instance.playerHeldBy.playerBodyAnimator.SetBool("ReloadShotgun2", false);
			__instance.isReloading = false;
			// __instance.ReloadGunEffectsServerRpc(start: false);

			__instance.SetSafetyControlTip();
		}
	}
}