using UnityEngine;
using HarmonyLib;

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