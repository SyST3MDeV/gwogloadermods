using System;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

class EntryPoint{
    public static void Load(GameObject consoleGameObject){
        Debug.Log("Loaded Debug Menu!");
        //var harmony = new Harmony("dev.gwogloader.debugMenu");
        //harmony.PatchAll();
        GameManager.Singleton.gameObject.AddComponent<DebugMenu>();
    }

    public static void UnLoad(GameObject consoleGameObject){
        Debug.Log("Unloaded Debug Menu!");
        //var harmony = new Harmony("dev.gwogloader.debugMenu");
        //harmony.UnpatchAll(harmonyID: "dev.gwogloader.debugMenu");
        if(GameManager.Singleton != null){
            if(GameManager.Singleton.gameObject.GetComponent<DebugMenu>() != null){
                GameManager.Singleton.gameObject.GetComponent<DebugMenu>().destroyMe();
            }
        }
    }
}

class DebugMenu: MonoBehaviour{
    private bool menuEnabled = false;
    private Rect windowPosition = new Rect(100, 100, 200, 300);

    public void Update(){
        if(Input.GetKeyDown(KeyCode.O)){
            menuEnabled = !menuEnabled;
        }
    }

    public void OnGUI(){
        if(menuEnabled){
            windowPosition = GUI.Window(99, windowPosition, windowFunc, "Debug Menu");
        }
    }

    private void windowFunc(int id)
    {
        if(GUI.Button(new Rect(5, 20, 190, 20), "Spawn Cactus Fruit")){
            GameManager.Singleton.DebugSpawnACactusFruitAtOurFeet_ServerRPC(GameManager.Singleton.localPlayerObject.transform.position);
        }
        if(GUI.Button(new Rect(5, 40, 190, 20), "Spawn Dynamite")){
            GameManager.Singleton.DebugSpawnADynamiteAtOurFeet_ServerRPC(GameManager.Singleton.localPlayerObject.transform.position);
        }
        if(GUI.Button(new Rect(5, 60, 190, 20), "Tactical Nuke Map")){
            GameManager.Singleton.CreateExplosionAtPosition_ServerRPC(new Vector3(0, 0, 0), 512);
        }
        if(GUI.Button(new Rect(5, 80, 190, 20), "Revive Self")){
            NetworkActions.Singleton.RevivePlayer_ServerRPC(NetworkManager.Singleton.LocalClientId);
        }
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

    private bool localPlayerScript(Player player)
    {
        return player.OwnerClientId == NetworkManager.Singleton.LocalClientId;
    }

    public void destroyMe(){
        Destroy(this);
    }
}