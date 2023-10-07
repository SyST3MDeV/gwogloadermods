using UnityEngine;
using HarmonyLib;

class EntryPoint{
    public static void Load(GameObject consoleGameObject){
        Debug.Log("Loaded Infinite Kits!");
        var harmony = new Harmony("dev.gwogloader.infKits");
        harmony.PatchAll();
    }

    public static void UnLoad(GameObject consoleGameObject){
        Debug.Log("Unloaded Infinite Kits!");
        var harmony = new Harmony("dev.gwogloader.infKits");
        harmony.UnpatchAll(harmonyID: "dev.gwogloader.infKits");
    }
}

[HarmonyPatch(typeof(PardnerEquipment))]
[HarmonyPatch("OnUse")]
class InfKits
{
    static bool Prefix(PardnerEquipment __instance)
    {
        return false;
    }
}