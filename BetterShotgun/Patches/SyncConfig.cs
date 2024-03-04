using GameNetcodeStuff;
using HarmonyLib;

namespace Hypick.Patches;

[HarmonyPatch]
public class SyncConfig
{
	[HarmonyPatch(typeof(PlayerControllerB), "ConnectClientToPlayerObject")]
	[HarmonyPostfix]
	public static void InitializeLocalPlayer() {
		if (Config.IsHost) {
			Config.MessageManager.RegisterNamedMessageHandler("BetterShotgun_OnRequestConfigSync", Config.OnRequestSync);
			Config.Synced = true;

			return;
		}

		Config.Synced = false;
		Config.MessageManager.RegisterNamedMessageHandler("BetterShotgun_OnReceiveConfigSync", Config.OnReceiveSync);
		Config.RequestSync();
	}
	
	[HarmonyPostfix]
	[HarmonyPatch(typeof(GameNetworkManager), "StartDisconnect")]
	public static void PlayerLeave() {
		Config.RevertSync();
	}
}