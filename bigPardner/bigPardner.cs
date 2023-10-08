using UnityEngine;
using HarmonyLib;

class EntryPoint{
    public static void Load(GameObject consoleGameObject){
        Debug.Log("Loaded Big Pardner!");
        var harmony = new Harmony("dev.gwogloader.bigPardner");
        harmony.PatchAll();
    }

    public static void UnLoad(GameObject consoleGameObject){
        Debug.Log("Unloaded Big Pardner!");
        var harmony = new Harmony("dev.gwogloader.bigPardner");
        harmony.UnpatchAll(harmonyID: "dev.gwogloader.bigPardner");
    }
}

[HarmonyPatch(typeof(Player))]
[HarmonyPatch("OnNetworkSpawn")]
class bigPardner
{
    static void Postfix(Player __instance)
    {
        __instance.gameObject.transform.localScale = new Vector3(10, 10, 10);
    }
}