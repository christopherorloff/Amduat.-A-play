using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ScrollManager;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    FMOD.Studio.System system;

    [SerializeField]
    private int hour;

    //AMBIENCE
    private string oceanAmbPath = "event:/HOUR 7/Ocean";
    FMOD.Studio.EventInstance oceanAmbInstance;

    //MUSIC
    private string showdownMuPath = "event:/MUSIC/Showdown";
    FMOD.Studio.EventInstance showdownMuInstance;

    //Hour 7 Sounds
    private string apopisIdlePath = "event:/HOUR 7/ApopisTiredIdle";
    FMOD.Studio.EventInstance apopisIdleInstance;

    private void Awake()
    {
        //CheckInstance();
    }

    private void Start()
    {
        system = FMODUnity.RuntimeManager.StudioSystem;
        CreateSoundInstances();

        SetHour(6);

        if(hour == 6) {
            oceanAmbInstance.start();
            showdownMuInstance.start();
            apopisIdleInstance.start();
        }
    }

    public int GetHour() {
        return hour;
    }

    public void SetHour(int currentHour) {
        hour = currentHour;
    }

    void CreateSoundInstances() { 
        //AMBIENCE INSTANCES
        oceanAmbInstance = FMODUnity.RuntimeManager.CreateInstance(oceanAmbPath);

        //MUSIC INSTANCES
        showdownMuInstance = FMODUnity.RuntimeManager.CreateInstance(showdownMuPath);

        //HOUR 7 SFX INSTANCES
        apopisIdleInstance = FMODUnity.RuntimeManager.CreateInstance(apopisIdlePath);
    }

    void CheckInstance()
    {
        //Checking that only one instance exists
        if (Instance == null)
        {
            Instance = FindObjectOfType<SoundManager>();

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }

}
