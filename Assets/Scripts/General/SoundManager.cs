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

    public TimelineManager_Script_Hour1 timelineHour1;
    public TimelineManager_Script_Hour2 timelineHour2;


    //AMBIENCE
    private string waterAmbPath = "event:/AMBIENCE/Water";
    public FMOD.Studio.EventInstance waterAmbInstance;
    FMOD.Studio.PLAYBACK_STATE waterAmbPlaybackState;
    bool waterAmbIsNotPlaying;

    private string jungleAmbPath = "event:/AMBIENCE/Hour2_Jungle";
    public FMOD.Studio.EventInstance jungleAmbInstance;

    private string seaCaveAmbPath = "event:/AMBIENCE/Hour4_SeaCave";
    public FMOD.Studio.EventInstance seaCaveAmbInstance;

    //General Sounds
    private string blessedDeadAppearPath = "event:/GENERAL SOUNDS/BlessedDeadAppear";

    private string boatPaddlePath = "event:/GENERAL SOUNDS/BoatPaddle";

    //Hour 1 Sounds
    private string solarBaboonsAppearPath = "event:/HOUR 1/SolarBaboonsAppear";
    public FMOD.Studio.EventInstance solarBaboonsAppearInstance;

    private string sunMovePath = "event:/HOUR 1/SunMove";
    public FMOD.Studio.EventInstance sunMoveInstance;

    //Hour 2 Sounds
    private string grainGodSpewPath = "event:/HOUR 2/GrainGodSpew";
    public FMOD.Studio.EventInstance grainGodSpewInstance;

    private string growAttackPath = "event:/HOUR 2/GrowAttack";
    public bool growAttackReady = true;

    private string growLoopPath = "event:/HOUR 2/GrowLoop";
    public FMOD.Studio.EventInstance growLoopInstance;

    //Hour 3 Sounds
    private string osirisLimbPath = "event:/HOUR 3/OsirisLimbCollected";
    public FMOD.Studio.EventInstance osirisLimbInstance;

    //Hour 4 Sounds
    private string collectEnergyPath = "event:/HOUR 4/CollectEnergy";

    private string towBoatPath = "event:/HOUR 4/TowBoat";

    private string goddessesAppearingPath = "event:/HOUR 4/GoddessesAppearing";
    public FMOD.Studio.EventInstance goddessesAppearingInstance;


    //Hour 5 Sounds


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

    //MUSIC
    private string showdownMuPath = "event:/MUSIC/Showdown";
    public FMOD.Studio.EventInstance showdownMuInstance;

    //private bool nextScene;

    private void Awake()
    {
        CheckInstance();
        CreateSoundInstances();
        activeScene = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        //Initialize system. Create sound instances.
        system = FMODUnity.RuntimeManager.StudioSystem;
        //HourInitialSounds(hour);
    }

    void OnEnable()
    {
        EventManager.sceneChange += SceneChanged;
    }

    void OnDisable()
    {
        EventManager.sceneChange -= SceneChanged;
    }

    private void Update()
    {
        //INPUTS FOR TESTING
        if (Input.GetKeyDown(KeyCode.A)) {
            knifeHitInstance.start();
        }

        if (Input.GetKeyDown(KeyCode.S)) {
        }

        if(GetHour() == 1) {
            if(timelineHour1.GetTimeline() < 1) {
                float input = Scroll.scrollValueAccelerated();

                sunMoveInstance.setParameterByName("Scroll", input * 10);
                sunMoveInstance.setParameterByName("Progrss", timelineHour1.GetTimeline());
            } else {
                sunMoveInstance.setParameterByName("Scroll", 0);
            }
        }

        if(GetHour() == 2) {

            float input = -Scroll.scrollValueAccelerated();
            print(input);

            if(timelineHour2.GetTimeline() < 0.9f) {
                if (input > 0.01)
                {
                    if (growAttackReady)
                    {
                        FMOD.Studio.EventInstance growAttackInstance = FMODUnity.RuntimeManager.CreateInstance(growAttackPath);
                        growAttackInstance.start();
                        growAttackReady = false;
                    }
                    growLoopInstance.setParameterByName("Scroll", 1);
                }
                else if (input < 0.01f)
                {
                    growLoopInstance.setParameterByName("Scroll", 0);
                    growAttackReady = true;
                }
            } else {
                growLoopInstance.setParameterByName("Scroll", 0);
            }
        }
    }

    private void SceneChanged()
    {
        // Her kan ting ske når scenen er skiftet... Din nye start() Jacob
        //print("Scene changed [Sound Manager]");
        //HourInitialSounds(7);

        HourInitialSounds(hour);
        print("SCENE CHANGED");
    }

    // Flot kodestil Jacob!
    public int GetHour() {
        return hour;
    }

    public void SetHour(int currentHour) {
        hour = currentHour;
    }

    void CreateSoundInstances() { 
        //AMBIENCE INSTANCES
        waterAmbInstance = FMODUnity.RuntimeManager.CreateInstance(waterAmbPath);
        jungleAmbInstance = FMODUnity.RuntimeManager.CreateInstance(jungleAmbPath);
        seaCaveAmbInstance = FMODUnity.RuntimeManager.CreateInstance(seaCaveAmbPath);

        //MUSIC INSTANCES
        showdownMuInstance = FMODUnity.RuntimeManager.CreateInstance(showdownMuPath);

        //HOUR 1 SFX INSTANCES
        solarBaboonsAppearInstance = FMODUnity.RuntimeManager.CreateInstance(solarBaboonsAppearPath);
        sunMoveInstance = FMODUnity.RuntimeManager.CreateInstance(sunMovePath);

        //HOUR 2 SFX INSTANCES
        grainGodSpewInstance = FMODUnity.RuntimeManager.CreateInstance(grainGodSpewPath);
        growLoopInstance = FMODUnity.RuntimeManager.CreateInstance(growLoopPath);

        //HOUR 3 SFX INSTANCES
        osirisLimbInstance = FMODUnity.RuntimeManager.CreateInstance(osirisLimbPath);

        //HOUR 4 SFX INSTANCES


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

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySpearMiss(float _charge) {
        //Only one spear miss sound at a time
        spearMissInstance.getPlaybackState(out spearMissPlaybackState);
        spearIsNotPlaying = spearMissPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;

        if (spearIsNotPlaying)
        {
            spearMissInstance.setParameterByName("Pitch", _charge);
            spearMissInstance.start();
        }
    }

    public void HourInitialSounds(int _hour) { 
        if (_hour == 1) {
            waterAmbInstance.start();
            sunMoveInstance.start();
        }
        if (_hour == 2) {
            waterAmbInstance.getPlaybackState(out waterAmbPlaybackState);
            waterAmbIsNotPlaying = waterAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (waterAmbIsNotPlaying) {
                waterAmbInstance.start();
            }
            jungleAmbInstance.start();

            growLoopInstance.start();
        }
        if (_hour == 3) {
            waterAmbInstance.getPlaybackState(out waterAmbPlaybackState);
            waterAmbIsNotPlaying = waterAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (waterAmbIsNotPlaying)
            {
                waterAmbInstance.start();
            }
            jungleAmbInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        if (_hour == 4) {
            waterAmbInstance.getPlaybackState(out waterAmbPlaybackState);
            waterAmbIsNotPlaying = waterAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (waterAmbIsNotPlaying)
            {
                waterAmbInstance.start();
            }
            seaCaveAmbInstance.start();
        }
        if (_hour == 5) { }

        if (_hour == 6) {
        }

        if (_hour == 7) {
            spearChargeInstance.start();
        }

        if (_hour == 8) { }
        if (_hour == 9) { }
        if (_hour == 10) { }
        if (_hour == 11) { }
        if (_hour == 12) { }
    }
}
