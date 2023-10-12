using UnityEngine;
using HarmonyLib;
using Unity.Netcode;
using System.Collections;

class EntryPoint{
    public static void Load(GameObject consoleGameObject){
        Debug.Log("Loaded the Apocalypse!");
        //var harmony = new Harmony("dev.gwogloader.apoc");
        //harmony.PatchAll();
        GameManager.Singleton.gameObject.AddComponent<ApocManager>();
    }

    public static void UnLoad(GameObject consoleGameObject){
        Debug.Log("Unloaded the Apocalypse!");
        //var harmony = new Harmony("dev.gwogloader.apoc");
        //harmony.UnpatchAll(harmonyID: "dev.gwogloader.apoc");
        if(GameManager.Singleton != null){
            if(GameManager.Singleton.gameObject.GetComponent<ApocManager>() != null){
                GameManager.Singleton.gameObject.GetComponent<ApocManager>().destroyMe();
            }
        }
    }
}

class ApocManager: MonoBehaviour{
    private GameManager gameManager;

    public void Start(){
        gameManager = GameManager.Singleton;

        if(NetworkManager.Singleton.IsHost){
            StartCoroutine("doBoom");
            StartCoroutine("doHot");
        }
    }

    IEnumerator doBoom(){
        yield return new WaitForSeconds(Random.Range(0f, 1.5f));
        if(gameManager.gameState == GameManager.GameState.playing){
            gameManager.CreateExplosionAtPosition_ServerRPC(new Vector3(Random.Range(-256f, 256f), Random.Range(-20f, 20f), Random.Range(-256f, 256f)), 5f);
        }
        StartCoroutine("doBoom");
    }

    IEnumerator doHot(){
        yield return new WaitForSeconds(Random.Range(0f, 20f));
        if(gameManager.gameState == GameManager.GameState.playing){
            gameManager.AddHotSandTime_ServerRPC(Random.Range(1f, 5f));
        }
        StartCoroutine("doHot");
    }

    public void destroyMe(){
        Destroy(this);
    }
}