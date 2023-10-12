using UnityEngine;
using HarmonyLib;
using Unity.Netcode;

class EntryPoint{
    public static void Load(GameObject consoleGameObject){
        Debug.Log("Loaded Big Woim!");
        var harmony = new Harmony("dev.gwogloader.bigWoim");
        harmony.PatchAll();
    }

    public static void UnLoad(GameObject consoleGameObject){
        Debug.Log("Unloaded Big Woim!");
        var harmony = new Harmony("dev.gwogloader.bigWoim");
        harmony.UnpatchAll(harmonyID: "dev.gwogloader.bigWoim");
    }
}

[HarmonyPatch(typeof(WormPlayer))]
[HarmonyPatch("OnNetworkSpawn")]
class BigWoim
{
    static void Postfix(WormPlayer __instance)
    {
        __instance.gameObject.transform.localScale = new Vector3(10, 10, 10);
    }
}

[HarmonyPatch(typeof(WormPlayer))]
[HarmonyPatch("FixedUpdate")]
class FakeAlwaysOnGround
{
    static AccessTools.FieldRef<WormPlayer, bool> touchingGroundRef =
        AccessTools.FieldRefAccess<WormPlayer, bool>("touchingGround");

    static void Postfix(WormPlayer __instance)
    {
        touchingGroundRef(__instance) = true;
    }
}

[HarmonyPatch(typeof(WormPlayer))]
[HarmonyPatch("Update")]
class InstaDestroy
{
    static AccessTools.FieldRef<WormPlayer, NetworkVariable<bool>> willDestroyBuildingInOneHitRef =
        AccessTools.FieldRefAccess<WormPlayer, NetworkVariable<bool>>("willDestroyBuildingInOneHit");

    static void Postfix(WormPlayer __instance)
    {
        if(willDestroyBuildingInOneHitRef(__instance).CanClientWrite(NetworkManager.Singleton.LocalClientId)){
            willDestroyBuildingInOneHitRef(__instance).Value = true;
        }
    }
}

[HarmonyPatch(typeof(WormMovement))]
[HarmonyPatch("Dive")]
class DiveModifier
{
    static void Prefix(ref float _diveForce)
    {
        _diveForce = _diveForce * 2f;
    }
}

/*
[HarmonyPatch(typeof(WormMovement))]
[HarmonyPatch("WhatSpeedShouldWeBeMovingAt")]
class MovementModifier
{
    static AccessTools.FieldRef<WormMovement, WormPlayer> wormPlayerRef =
        AccessTools.FieldRefAccess<WormMovement, WormPlayer>("wormPlayer");

    static AccessTools.FieldRef<WormMovement, float> currMoveSpeedRef =
        AccessTools.FieldRefAccess<WormMovement, float>("currMoveSpeed");

    static AccessTools.FieldRef<WormMovement, float> surfaceSpeedRef =
        AccessTools.FieldRefAccess<WormMovement, float>("surfaceSpeed");

    static AccessTools.FieldRef<WormMovement, float> burrowSpeedRef =
        AccessTools.FieldRefAccess<WormMovement, float>("burrowSpeed");

    static void Postfix(WormMovement __instance)
    {
        if (!wormPlayerRef(__instance))
		{
			return;
		}
		if (wormPlayerRef(__instance).wormMovementState == WormPlayer.WormMovementState.Surface)
		{
			currMoveSpeedRef(__instance) = surfaceSpeedRef(__instance) * 10f;
			return;
		}
		if (wormPlayerRef(__instance).wormMovementState == WormPlayer.WormMovementState.Burrowed)
		{
			currMoveSpeedRef(__instance) = burrowSpeedRef(__instance) * 10f;
			return;
		}
    }
}
*/