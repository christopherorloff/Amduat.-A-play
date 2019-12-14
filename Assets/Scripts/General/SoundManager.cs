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
    private int hour = 0;
    private string activeScene;

    [Header("Timelines")]
    public TimelineManager_Script_Hour1 timelineHour1;
    public TimelineManager_Script_Hour2 timelineHour2;

    [Header("Hour 5 Specific Variables")]
    public Transform[] hour5Snakes;
    public Transform hour5Boat;
    public float hour5SnakesDistanceToBoat;

    [Header("Hour 8 Specific Variables")]
    public SceneManagerScript_Hour8 sceneManager8;
    public ConeBehaviour_Script_Hour8 coneBehaviour;


    //AMBIENCE
    private string waterAmbPath = "event:/AMBIENCE/Water";
    public FMOD.Studio.EventInstance waterAmbInstance;
    FMOD.Studio.PLAYBACK_STATE waterAmbPlaybackState;
    bool waterAmbIsNotPlaying;

    private string jungleAmbPath = "event:/AMBIENCE/Hour2_Jungle";
    public FMOD.Studio.EventInstance jungleAmbInstance;

    private string seaCaveAmbPath = "event:/AMBIENCE/Hour4_SeaCave";
    public FMOD.Studio.EventInstance seaCaveAmbInstance;

    private string caveAmbPath = "event:/AMBIENCE/Cave";
    public FMOD.Studio.EventInstance caveAmbInstance;
    FMOD.Studio.PLAYBACK_STATE caveAmbPlaybackState;
    bool caveAmbIsNotPlaying;

    private string caveWaterAmbPath = "event:/AMBIENCE/CaveWater";
    public FMOD.Studio.EventInstance caveWaterAmbInstance;
    FMOD.Studio.PLAYBACK_STATE caveWaterAmbPlaybackState;
    bool caveWaterAmbIsNotPlaying;

    //General Sounds
    private string blessedDeadAppearPath = "event:/GENERAL SOUNDS/BlessedDeadAppear";

    private string boatPaddlePath = "event:/GENERAL SOUNDS/BoatPaddle";

    private string boatPaddleContinuousPath = "event:/GENERAL SOUNDS/BoatPaddleContinuous";
    public FMOD.Studio.EventInstance boatPaddleContinuousInstance;
    FMOD.Studio.PLAYBACK_STATE boatPaddleContinuousPlaybackState;
    bool boatPaddleContinuousIsNotPlaying;

    private string dustballRollingPath = "event:/GENERAL SOUNDS/DustballRolling";
    public FMOD.Studio.EventInstance dustballRollingInstance;
    FMOD.Studio.PLAYBACK_STATE dustballRollingPlaybackState;
    bool dustballRollingIsNotPlaying;

    private string titleSoundPath = "event:/GENERAL SOUNDS/Title";
    public FMOD.Studio.EventInstance titleSoundInstance;

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

    private string boatAgroundPath = "event:/HOUR 4/BoatAground";
    public FMOD.Studio.EventInstance boatAgroundInstance;


    //Hour 5 Sounds
    private string snakesHissPath = "event:/HOUR 5/SnakesHiss";
    public FMOD.Studio.EventInstance snakesHissInstance;

    private string wingedChargePath = "event:/HOUR 5/WingedCharge";

    private string wingedThrustPath = "event:/HOUR 5/WingedThrust";

    //Hour 6 Sounds
    private string knifeSpawnPath = "event:/HOUR 6/KnifeSpawn";
    public FMOD.Studio.EventInstance knifeSpawnInstance;

    private string knifeThrowPath = "event:/HOUR 6/KnifeThrow";

    private string knifeHitPath = "event:/HOUR 6/KnifeHit";

    private string knifeClangPath = "event:/HOUR 6/KnifeClang";

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

    //Hour 8 Sounds
    private string sunShinePath = "event:/HOUR 8/SunShine";
    public FMOD.Studio.EventInstance sunShineInstance;

    private string blessedDeadRunningPath = "event:/HOUR 8/BlessedDeadRunning";
    public FMOD.Studio.EventInstance blessedDeadRunningInstance;

    private string boatPushLoopPath = "event:/HOUR 8/BoatPushLoop";
    public FMOD.Studio.EventInstance boatPushLoopInstance;

    //Hour 9 Sounds
    private string blessedDeadDropPath = "event:/HOUR 9/BlessedDeadDrop";
    public FMOD.Studio.EventInstance blessedDeadDropInstance;

    //HOUR 10 Sounds
    private string dustballAppearsPath = "event:/HOUR 10/DustballAppears";
    public FMOD.Studio.EventInstance dustballAppearsInstance;

    private string statueDonePath = "event:/HOUR 10/StatueDone";
    public FMOD.Studio.EventInstance statueDoneInstance;

    private string statueMovePath = "event:/HOUR 10/StatueMove";
    public FMOD.Studio.EventInstance statueMoveInstance;

    //MUSIC
    private string themeMuPath = "event:/MUSIC/Theme";
    public FMOD.Studio.EventInstance themeMuInstance;

    private string apopisThemeMuPath = "event:/MUSIC/ApopisTheme";
    public FMOD.Studio.EventInstance apopisThemeMuInstance;

    private string ritualThemeMuPath = "event:/MUSIC/RitualTheme";
    public FMOD.Studio.EventInstance ritualThemeMuInstance;

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
        if (Input.GetKeyDown(KeyCode.A))
        {
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
        }

        if (GetHour() == 1)
        {
            if(timelineHour1 == null) {
                FindObjectOfType<TimelineManager_Script_Hour1>();
            }

            if (timelineHour1.GetTimeline() < 1)
            {
                float input = Scroll.scrollValueAccelerated();

                sunMoveInstance.setParameterByName("Scroll", input * 10);
                sunMoveInstance.setParameterByName("Progrss", timelineHour1.GetTimeline());
            }
            else
            {
                sunMoveInstance.setParameterByName("Scroll", 0);
            }
        }

        if (GetHour() == 2)
        {
            if(timelineHour2 == null) {
                FindObjectOfType<TimelineManager_Script_Hour2>();
            }

            float input = -Scroll.scrollValueAccelerated();
            print(input);

            if (timelineHour2.GetTimeline() < 0.9f)
            {
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
            }
            else
            {
                growLoopInstance.setParameterByName("Scroll", 0);
            }
        }

        if(GetHour() == 5) {

            if(hour5Boat == null) {
                hour5Boat = GameObject.Find("Boat_Parent").transform;
            }

            if(hour5Snakes.Length == 0) {
                hour5Snakes = new Transform[12];
                SnakeMoveTowardsBoat_script[] snakes = new SnakeMoveTowardsBoat_script[12];
                snakes = FindObjectsOfType<SnakeMoveTowardsBoat_script>();
                print(snakes);

                for(int i = 0; i < snakes.Length; i++) {
                    hour5Snakes[i] = snakes[i].GetComponent<Transform>();
                }

            }

            //Updating snakes distance variable so it goes from 0-1, and updates according to snakes distance to boat
            Vector3[] snakePositions = new Vector3[hour5Snakes.Length];
            for (int i = 0; i < hour5Snakes.Length; i++) {
                    snakePositions[i] = hour5Snakes[i].position;
                }
            hour5SnakesDistanceToBoat = Mathf.Clamp(Vector3.Distance(hour5Boat.position, GetMeanVector3(snakePositions)), 1, 6);
            hour5SnakesDistanceToBoat = hour5SnakesDistanceToBoat / 6;
            snakesHissInstance.setParameterByName("Intensity", hour5SnakesDistanceToBoat);
        }

        if(GetHour() == 8) {
            if(sceneManager8 == null) {
                sceneManager8 = FindObjectOfType<SceneManagerScript_Hour8>();
                print("HALLO");
            }

            if(coneBehaviour == null) {
                coneBehaviour = FindObjectOfType<ConeBehaviour_Script_Hour8>();
            }

            boatPushLoopInstance.setParameterByName("Speed", sceneManager8.GetBlessedDeadSpeed());
            sunShineInstance.setParameterByName("Scroll", coneBehaviour.GetConeSize());
            blessedDeadRunningInstance.setParameterByName("Amount", 1 - sceneManager8.GetBlessedDeadSpeed());
            if(1 - sceneManager8.GetBlessedDeadSpeed() <= 0) {
                blessedDeadRunningInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
    }

    private Vector3 GetMeanVector3(Vector3[] positions) {
        float x = 0;
        float y = 0;
        float z = 0;

        foreach(Vector3 pos in positions) {
            x += pos.x;
            y += pos.y;
            z += pos.z;
        }

        return new Vector3(x / positions.Length, y / positions.Length, z / positions.Length);
    }

    public void PlayKnifeHit() {
        FMOD.Studio.EventInstance knifeHitInstance;
        knifeHitInstance = FMODUnity.RuntimeManager.CreateInstance(knifeHitPath);
        knifeHitInstance.start();
    }

    public void PlayKnifeBounce() {
        FMOD.Studio.EventInstance knifeClangInstance;
        knifeClangInstance = FMODUnity.RuntimeManager.CreateInstance(knifeClangPath);
        knifeClangInstance.start();
    }

    public void PlayKnifeThrow() {
        FMOD.Studio.EventInstance knifeThrowInstance;
        knifeThrowInstance = FMODUnity.RuntimeManager.CreateInstance(knifeThrowPath);
        knifeThrowInstance.start();
    }

    public void PlayGodAppear() {
        FMOD.Studio.EventInstance osirisAppear;
        osirisAppear = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR 5/OsirisAppear");
        osirisAppear.start();
    }

    public void PlayBoatPaddle() {
        FMOD.Studio.EventInstance sound;
        sound = FMODUnity.RuntimeManager.CreateInstance("event:/GENERAL SOUNDS/BoatPaddle");
        sound.start();
    }

    public void PlayBlessedDeadAppear()
    {
        FMOD.Studio.EventInstance sound;
        sound = FMODUnity.RuntimeManager.CreateInstance("event:/GENERAL SOUNDS/BlessedDeadAppear");
        sound.setParameterByName("Water", 0);
        sound.start();
    }

    public void PlayBlessedDeadAppearBoat() {
        FMOD.Studio.EventInstance sound;
        sound = FMODUnity.RuntimeManager.CreateInstance("event:/GENERAL SOUNDS/BlessedDeadAppear");
        sound.setParameterByName("Water", 1);
        sound.start();
    }

    public void PlayBlessedDeadBoatMove() {
        FMOD.Studio.EventInstance sound;
        sound = FMODUnity.RuntimeManager.CreateInstance("event:/GENERAL SOUNDS/BlessedDeadBoatMove");
        sound.start();
    }

    public void PlayBlessedDeadAground() {
        FMOD.Studio.EventInstance sound;
        sound = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR11/BlessedDeadAground");
        sound.start();
    }

    public void PlayTheme() {
        themeMuInstance.start();
    }

    public void PlayRitualTheme() {
        ritualThemeMuInstance.start();
    }

    public void PlayApopisTheme() {
        apopisThemeMuInstance.start();
    }

    public void PlayDustballTheme() {
        FMOD.Studio.EventInstance sound;
        sound = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR 12/DustballTheme");
        sound.start();
    }

    public void EndTheme() {
        themeMuInstance.setParameterByName("End", 1);
    }

    public void EndRitualTheme() {
        ritualThemeMuInstance.setParameterByName("RitualThemeEnd", 1);
    }

    public void EndApopisTheme() {
        apopisThemeMuInstance.setParameterByName("ApopisThemeEnd", 1);
    }

    private void SceneChanged()
    {
        // Her kan ting ske når scenen er skiftet... Din nye start() Jacob
        //print("Scene changed [Sound Manager]");
        //HourInitialSounds(7);
        hour = GameManager.Instance.GetActiveSceneIndex() + 1;
        HourInitialSounds(hour);
        print("SCENE CHANGED");
    }

    // Flot kodestil Jacob!
    public int GetHour()
    {
        return hour;
    }

    public void SetHour(int currentHour)
    {
        hour = currentHour;
    }

    void CreateSoundInstances()
    {
        //GENERAL SOUND INSTANCES
        titleSoundInstance = FMODUnity.RuntimeManager.CreateInstance(titleSoundPath);
        boatPaddleContinuousInstance = FMODUnity.RuntimeManager.CreateInstance(boatPaddleContinuousPath);
        dustballRollingInstance = FMODUnity.RuntimeManager.CreateInstance(dustballRollingPath);

        //AMBIENCE INSTANCES
        waterAmbInstance = FMODUnity.RuntimeManager.CreateInstance(waterAmbPath);
        jungleAmbInstance = FMODUnity.RuntimeManager.CreateInstance(jungleAmbPath);
        seaCaveAmbInstance = FMODUnity.RuntimeManager.CreateInstance(seaCaveAmbPath);
        caveAmbInstance = FMODUnity.RuntimeManager.CreateInstance(caveAmbPath);
        caveWaterAmbInstance = FMODUnity.RuntimeManager.CreateInstance(caveWaterAmbPath);

        //HOUR 1 SFX INSTANCES
        solarBaboonsAppearInstance = FMODUnity.RuntimeManager.CreateInstance(solarBaboonsAppearPath);
        sunMoveInstance = FMODUnity.RuntimeManager.CreateInstance(sunMovePath);

        //HOUR 2 SFX INSTANCES
        grainGodSpewInstance = FMODUnity.RuntimeManager.CreateInstance(grainGodSpewPath);
        growLoopInstance = FMODUnity.RuntimeManager.CreateInstance(growLoopPath);

        //HOUR 3 SFX INSTANCES
        osirisLimbInstance = FMODUnity.RuntimeManager.CreateInstance(osirisLimbPath);

        //HOUR 4 SFX INSTANCES
        goddessesAppearingInstance = FMODUnity.RuntimeManager.CreateInstance(goddessesAppearingPath);
        boatAgroundInstance = FMODUnity.RuntimeManager.CreateInstance(boatAgroundPath);

        //Hour 5 SFX INSTANCES
        snakesHissInstance = FMODUnity.RuntimeManager.CreateInstance(snakesHissPath);

        //HOUR 6 SFX INSTANCES
        knifeSpawnInstance = FMODUnity.RuntimeManager.CreateInstance(knifeSpawnPath);

        apopisAppearInstance = FMODUnity.RuntimeManager.CreateInstance(apopisAppearPath);

        //HOUR 8 SFX INSTANCES
        sunShineInstance = FMODUnity.RuntimeManager.CreateInstance(sunShinePath);
        blessedDeadRunningInstance = FMODUnity.RuntimeManager.CreateInstance(blessedDeadRunningPath);
        boatPushLoopInstance = FMODUnity.RuntimeManager.CreateInstance(boatPushLoopPath);

        //HOUR 7 SFX INSTANCES
        apopisIdleInstance = FMODUnity.RuntimeManager.CreateInstance(apopisIdlePath);
        spearReadyInstance = FMODUnity.RuntimeManager.CreateInstance(spearReadyPath);
        spearHitInstance = FMODUnity.RuntimeManager.CreateInstance(spearHitPath);
        spearMissInstance = FMODUnity.RuntimeManager.CreateInstance(spearMissPath);
        spearChargeInstance = FMODUnity.RuntimeManager.CreateInstance(spearChargePath);

        //HOUR 8 SFX INSTANCE

        //HOUR 9 SFX INSTANCE
        blessedDeadDropInstance = FMODUnity.RuntimeManager.CreateInstance(blessedDeadDropPath);

        //HOUR 10 SFX INSTANCE
        dustballAppearsInstance = FMODUnity.RuntimeManager.CreateInstance(dustballAppearsPath);
        statueDoneInstance = FMODUnity.RuntimeManager.CreateInstance(statueDonePath);
        statueMoveInstance = FMODUnity.RuntimeManager.CreateInstance(statueMovePath);

        //MUSIC INSTANCES
        apopisThemeMuInstance = FMODUnity.RuntimeManager.CreateInstance(apopisThemeMuPath);
        ritualThemeMuInstance = FMODUnity.RuntimeManager.CreateInstance(ritualThemeMuPath);
        themeMuInstance = FMODUnity.RuntimeManager.CreateInstance(themeMuPath);
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

    public void PlaySpearMiss(float _charge)
    {
        //Only one spear miss sound at a time
        spearMissInstance.getPlaybackState(out spearMissPlaybackState);
        spearIsNotPlaying = spearMissPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;

        if (spearIsNotPlaying)
        {
            spearMissInstance.setParameterByName("Pitch", _charge);
            spearMissInstance.start();
        }
    }

    public void PlayWingCharge() {
        float value = Random.Range(0f, 1f);
        FMOD.Studio.EventInstance wingChargeInstance = FMODUnity.RuntimeManager.CreateInstance(wingedChargePath);
        wingChargeInstance.start();
        wingChargeInstance.setParameterByName("Progress", value);
    }

    public void PlayWingThrust() {
        FMOD.Studio.EventInstance wingThrustInstance = FMODUnity.RuntimeManager.CreateInstance(wingedThrustPath);
        wingThrustInstance.start();
    }

    public void HourInitialSounds(int _hour)
    {
        if (_hour == 1)
        {
            //STARTING SOUNDS
            timelineHour1 = FindObjectOfType<TimelineManager_Script_Hour1>();
            waterAmbInstance.start();
            sunMoveInstance.start();
        }

        if (_hour == 2)
        {
            timelineHour2 = FindObjectOfType<TimelineManager_Script_Hour2>();
            //STOPPING SOUNDS
            sunMoveInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            //STARTING SOUNDS
            waterAmbInstance.getPlaybackState(out waterAmbPlaybackState);
            waterAmbIsNotPlaying = waterAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (waterAmbIsNotPlaying)
            {
                waterAmbInstance.start();
            }
            jungleAmbInstance.start();
            growLoopInstance.start();
        }

        if (_hour == 3)
        {
            //STOPPING SOUNDS
            jungleAmbInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            growLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            //STARTING SOUNDS
            waterAmbInstance.getPlaybackState(out waterAmbPlaybackState);
            waterAmbIsNotPlaying = waterAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (waterAmbIsNotPlaying)
            {
                waterAmbInstance.start();
            }
        }

        if (_hour == 4)
        {
            //STOPPING SOUNDS

            //STARTING SOUNDS
            waterAmbInstance.getPlaybackState(out waterAmbPlaybackState);
            waterAmbIsNotPlaying = waterAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (waterAmbIsNotPlaying)
            {
                waterAmbInstance.start();
            }
            seaCaveAmbInstance.start();
            boatAgroundInstance.start();
            EndTheme();
            EndRitualTheme();
        }

        if (_hour == 5) {
            //STOPPING SOUNDS
            waterAmbInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            seaCaveAmbInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            //STARTING SOUNDS
            caveAmbInstance.start();
            caveWaterAmbInstance.start();
            snakesHissInstance.start();
            boatPaddleContinuousInstance.start();
        }

        if (_hour == 6)
        {
            snakesHissInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            boatPaddleContinuousInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            apopisAppearInstance.start();

            caveWaterAmbInstance.getPlaybackState(out caveWaterAmbPlaybackState);
            caveWaterAmbIsNotPlaying = caveWaterAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (caveWaterAmbIsNotPlaying) {
                caveWaterAmbInstance.start();
            }
            caveAmbInstance.getPlaybackState(out caveAmbPlaybackState);
            caveAmbIsNotPlaying = caveAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (caveAmbIsNotPlaying) {
                caveAmbInstance.start();
            }
        }

        if (_hour == 7)
        {
            spearChargeInstance.start();

            waterAmbInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            caveWaterAmbInstance.getPlaybackState(out caveWaterAmbPlaybackState);
            caveWaterAmbIsNotPlaying = caveWaterAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (caveWaterAmbIsNotPlaying)
            {
                caveWaterAmbInstance.start();
            }
            caveAmbInstance.getPlaybackState(out caveAmbPlaybackState);
            caveAmbIsNotPlaying = caveAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (caveAmbIsNotPlaying)
            {
                caveAmbInstance.start();
            }
        }

        if (_hour == 8) {
            caveAmbInstance.start();
            blessedDeadRunningInstance.start();
            boatPushLoopInstance.start();
            sunShineInstance.start();
        }

        if (_hour == 9) {
            caveAmbInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            blessedDeadRunningInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            boatPushLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            sunShineInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            waterAmbInstance.start();
            blessedDeadDropInstance.start();

        }

        if (_hour == 10) {
            waterAmbInstance.getPlaybackState(out waterAmbPlaybackState);
            waterAmbIsNotPlaying = waterAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (waterAmbIsNotPlaying)
            {
                waterAmbInstance.start();
            }

            statueMoveInstance.start();
        }

        if (_hour == 11) {
            statueMoveInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            waterAmbInstance.getPlaybackState(out waterAmbPlaybackState);
            waterAmbIsNotPlaying = waterAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (waterAmbIsNotPlaying)
            {
                waterAmbInstance.start();
            }

            boatPaddleContinuousInstance.start();
            dustballRollingInstance.start();

        }

        if (_hour == 12) {
            //STOPPING SOUNDS
            EndRitualTheme();

            //STARTING SOUNDS
            waterAmbInstance.getPlaybackState(out waterAmbPlaybackState);
            waterAmbIsNotPlaying = waterAmbPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if (waterAmbIsNotPlaying)
            {
                waterAmbInstance.start();
            }

            boatPaddleContinuousInstance.getPlaybackState(out boatPaddleContinuousPlaybackState);
            boatPaddleContinuousIsNotPlaying = boatPaddleContinuousPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if(boatPaddleContinuousIsNotPlaying) boatPaddleContinuousInstance.start();

            dustballRollingInstance.getPlaybackState(out dustballRollingPlaybackState);
            dustballRollingIsNotPlaying = dustballRollingPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            if(dustballRollingIsNotPlaying) dustballRollingInstance.start();
        }
    }
}
