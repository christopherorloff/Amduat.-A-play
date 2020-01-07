using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingAudio : MonoBehaviour
{
    public Button startButton;
    private bool hasStarted = false;

    //DENNE FUNKTION KALDES NÅR MAN TRYKKER PÅ KNAPPEN TIL AT STARTE SPILLET.
    //KAN KUN STARTES NÅR LYDBANKE ER LOADET
    //SKAL OGSÅ GÆLDE FOR RESTEN AF SPILLETS ASSETS
    public void ProceedToGame()
    {
        Debug.Log("Button clicked");
        FMODUnity.RuntimeManager.LoadBank("Master");
        FMODUnity.RuntimeManager.LoadBank("Master.strings");
    }

    void Update()
    {
        Debug.Log("UPDATE");
        if (FMODUnity.RuntimeManager.HasBankLoaded("Master"))
        {

            Debug.Log("AUDIO: Master has loaded");
            if (FMODUnity.RuntimeManager.HasBankLoaded("Master.strings"))
            {
                if (!hasStarted)
                {
                    Debug.Log("AUDIO: Master.strings has loaded");
                    SoundManager.Instance.CreateSoundInstances();
                    //SÆTTER DEN NÆSTE SCENE I INSPECTOREN SÅ DET ER LET AT TESTE I WEB BUILD
                    //FINAL VERSION SKAL VÆRE INTRO SCENEN TIL SPILLET
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    hasStarted = true;
                }
            }
        }
    }
}
