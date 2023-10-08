using UnityEngine;
using HarmonyLib;

public class EntryPoint{
    public static void Load(GameObject consoleGameObject){
        Debug.Log("Loaded Infinite Worm Abilites!");
        Harmony harmony = new Harmony("dev.gwogloader.infWormAbilities");
        harmony.PatchAll();
    }

    public static void UnLoad(GameObject consoleGameObject){
        Debug.Log("Unloaded Infinite Worm Abilites!");
        Harmony harmony = new Harmony("dev.gwogloader.infWormAbilities");
        harmony.UnpatchAll(harmonyID: "dev.gwogloader.infWormAbilities");
    }
}

[HarmonyPatch(typeof(WormAbility))]
[HarmonyPatch("UseAbility")] // if possible use nameof() here
class DontStartCDPatch
{
    static bool Prefix(WormAbility __instance)
    {
        return false;
    }
}