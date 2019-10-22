using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using ScrollManager;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    FMOD.Studio.System system;

    [SerializeField]
    private int hour;
    private string activeScene;

    //AMBIENCE
    private string oceanAmbPath = "event:/HOUR 7/Ocean";
    public FMOD.Studio.EventInstance oceanAmbInstance;

    //MUSIC
    private string showdownMuPath = "event:/MUSIC/Showdown";
    public FMOD.Studio.EventInstance showdownMuInstance;

    //Hour 6 Sounds
    private string knifeSpawnPath = "event:/HOUR 6/KnifeSpawn";
    public FMOD.Studio.EventInstance knifeSpawnInstance;
    private string knifeThrowPath = "event:/HOUR 6/KnifeThrow";
    public FMOD.Studio.EventInstance knifeThrowInstance;
    private string knifeHitPath = "event:/HOUR 6/KnifeHit";
    public FMOD.Studio.EventInstance knifeHitInstance;
    private string knifeClangPath = "event:/HOUR 6/KnifeClang";
    public FMOD.Studio.EventInstance knifeClangInstance;

    private string apopisAppearPath = "event:/HOUR 6/ApopisAppear";
    public FMOD.Studio.EventInstance apopisAppearInstance;

    //Hour 7 Sounds
    private string apopisIdlePath = "event:/HOUR 7/ApopisTiredIdle";
    public FMOD.Studio.EventInstance apopisIdleInstance;
   
    private string spearReadyPath = "event:/HOUR 7/SpearReady";
    public FMOD.Studio.EventInstance spearReadyInstance;
    private string spearHitPath = "event:/HOUR 7/SpearHit";
    public FMOD.Studio.EventInstance spearHitInstance;
    private string spearMissPath = "event:/HOUR 7/SpearMiss";
    public FMOD.Studio.EventInstance spearMissInstance;
    FMOD.Studio.PLAYBACK_STATE spearMissPlaybackState;
    bool spearIsNotPlaying;
    private string spearChargePath = "event:/HOUR 7/SpearCharge";
    public FMOD.Studio.EventInstance spearChargeInstance;

    private void Awake()
    {
        CheckInstance();
        activeScene = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        //Initialize system. Create sound instances.
        system = FMODUnity.RuntimeManager.StudioSystem;
        CreateSoundInstances();
        
        HourInitialSounds(hour);
    }

    private void Update()
    {
        //INPUTS FOR TESTING
        if (Input.GetKeyDown(KeyCode.A)) {
        }

        if (Input.GetKeyDown(KeyCode.S)) {
        }


        //UPDATES FOR SPECIFIC HOURS

        //HOUR 7
        if(GetHour() == 7) {
            spearMissInstance.getPlaybackState(out spearMissPlaybackState);
            spearIsNotPlaying = spearMissPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
        }

        //SKAL VIRKE MED GAME MANAGER
        if(activeScene != SceneManager.GetActiveScene().name) { }
    }

    public int GetHour() {
        return hour;
    }

    public void SetHour(int currentHour) {
        hour = currentHour - 1;
    }

    void CreateSoundInstances() { 
        //AMBIENCE INSTANCES
        oceanAmbInstance = FMODUnity.RuntimeManager.CreateInstance(oceanAmbPath);

        //MUSIC INSTANCES
        showdownMuInstance = FMODUnity.RuntimeManager.CreateInstance(showdownMuPath);

        //HOUR 6 SFX INSTANCES
        knifeSpawnInstance = FMODUnity.RuntimeManager.CreateInstance(knifeSpawnPath);
        knifeThrowInstance = FMODUnity.RuntimeManager.CreateInstance(knifeThrowPath);
        knifeHitInstance = FMODUnity.RuntimeManager.CreateInstance(knifeHitPath);
        knifeClangInstance = FMODUnity.RuntimeManager.CreateInstance(knifeClangPath);

        apopisAppearInstance = FMODUnity.RuntimeManager.CreateInstance(apopisAppearPath);

        //HOUR 7 SFX INSTANCES
        apopisIdleInstance = FMODUnity.RuntimeManager.CreateInstance(apopisIdlePath);
        spearReadyInstance = FMODUnity.RuntimeManager.CreateInstance(spearReadyPath);
        spearHitInstance = FMODUnity.RuntimeManager.CreateInstance(spearHitPath);
        spearMissInstance = FMODUnity.RuntimeManager.CreateInstance(spearMissPath);
        spearChargeInstance = FMODUnity.RuntimeManager.CreateInstance(spearChargePath);
    }

    void CheckInstance()
    {
        //Checking that only one instance exists
        if (Instance == null)
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
    }

    public void PlaySpearMiss(float _charge) {
        //Only one spear miss sound at a time
        if (spearIsNotPlaying)
        {
            spearMissInstance.setParameterByName("Pitch", _charge);
            spearMissInstance.start();
        }
    }

    public void HourInitialSounds(int _hour) { 
        if (_hour == 1) { }
        if (_hour == 2) { }
        if (_hour == 3) { }
        if (_hour == 4) { }
        if (_hour == 5) { }

        if (_hour == 6) {
            showdownMuInstance.start();

            apopisAppearInstance.start();

            oceanAmbInstance.start();
            oceanAmbInstance.setParameterByName("Intensity", 0.8f);
        }

        if (_hour == 7) {
            showdownMuInstance.setParameterByName("End", 1f);

            oceanAmbInstance.start();
            oceanAmbInstance.setParameterByName("Intensity", 0f);
            apopisIdleInstance.start();
            spearChargeInstance.start();
        }

        if (_hour == 8) { }
        if (_hour == 9) { }
        if (_hour == 10) { }
        if (_hour == 11) { }
        if (_hour == 12) { }
    }
}
