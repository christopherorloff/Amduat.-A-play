using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ScrollManager;

public class SoundTest : MonoBehaviour
{
    FMOD.Studio.System system;

    //Event variables
    public string eventPath;
    FMOD.Studio.EventInstance eventInstance;

    //Parameter variables
    public float intensity;
    public float scale = 0.5f;

    //Knife throw test sounds
    public string knifeThrowPath;
    FMOD.Studio.EventInstance knifeThrowInstance;

    public string knifeHitPath;
    FMOD.Studio.EventInstance[] knifeHitInstance = new FMOD.Studio.EventInstance[6];

    public string knifeClangPath;
    FMOD.Studio.EventInstance knifeClangInstance;

    FMOD.Studio.PLAYBACK_STATE playbackState;


    //Spear Charge sounds
    public string spearChargePath;
    FMOD.Studio.EventInstance spearChargeInstance;
    public float spearCharge;
    public float spearScroll;


    void Start()
    {
        system = FMODUnity.RuntimeManager.StudioSystem;

        //Creating event instance and playing sound
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        eventInstance.start();

        //Knife event instances
        knifeThrowInstance = FMODUnity.RuntimeManager.CreateInstance(knifeThrowPath);
        knifeClangInstance = FMODUnity.RuntimeManager.CreateInstance(knifeClangPath);

        for(int i = 0; i < knifeHitInstance.Length; i++) {
            knifeHitInstance[i] = FMODUnity.RuntimeManager.CreateInstance(knifeHitPath);
        }

        //Spear charge event instance
        spearChargeInstance = FMODUnity.RuntimeManager.CreateInstance(spearChargePath);
        spearChargeInstance.start();

    }

    void Update()
    {
        eventInstance.setParameterByName("Intensity", intensity);
        if (Input.GetKey(KeyCode.UpArrow)) {
            intensity += Time.deltaTime * scale;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            intensity -= Time.deltaTime * scale;
        }
        intensity = Mathf.Clamp(intensity, 0, 1);

        if (Input.GetKeyDown(KeyCode.A)) knifeThrowInstance.start();

        if (Input.GetKeyDown(KeyCode.S)) {
            for(int i = 0; i < knifeHitInstance.Length; i++) {
                //Checking if a sound is playing in an array of instances. If not playing, then checking next sound
                FMOD.Studio.PLAYBACK_STATE playbackState;
                knifeHitInstance[i].getPlaybackState(out playbackState);
                if(playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED) {
                    knifeHitInstance[i].start();
                    knifeHitInstance[i].release();
                    return;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.D)) knifeClangInstance.start();

        SpearCharge();
    }

    void SpearCharge() {
        spearChargeInstance.setParameterByName("Charge", spearCharge);
        spearChargeInstance.setParameterByName("Scroll", spearScroll);

        spearScroll = Scroll.scrollValue();
        //spearCharge += Input.GetAxis("Mouse ScrollWheel") / 10000;
    }
}
