using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingAudio : MonoBehaviour
{
    public string openingSceneName;
    public Button startButton;

    void Awake()
    {
        //HER LOADER VI BANKS INDEN SPILLET GÅR IGANG
        FMODUnity.RuntimeManager.LoadBank("Master");
        FMODUnity.RuntimeManager.LoadBank("Master.strings");
    }

    private void Start()
    {
        startButton.GetComponentInChildren<Text>().text = "BEGIN SCENE: " + openingSceneName;
    }

    //DENNE FUNKTION KALDES NÅR MAN TRYKKER PÅ KNAPPEN TIL AT STARTE SPILLET.
    //KAN KUN STARTES NÅR LYDBANKE ER LOADET
    //SKAL OGSÅ GÆLDE FOR RESTEN AF SPILLETS ASSETS
    public void ProceedToGame() {
        if (FMODUnity.RuntimeManager.HasBankLoaded("Master")) {
            Debug.Log("AUDIO: Master has loaded");
            if (FMODUnity.RuntimeManager.HasBankLoaded("Master.strings")) {
                Debug.Log("AUDIO: Master.strings has loaded");
                //SÆTTER DEN NÆSTE SCENE I INSPECTOREN SÅ DET ER LET AT TESTE I WEB BUILD
                //FINAL VERSION SKAL VÆRE INTRO SCENEN TIL SPILLET
                SceneManager.LoadScene(openingSceneName);
            }
        }
    }
}
