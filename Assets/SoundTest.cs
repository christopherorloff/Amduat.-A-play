using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ScrollManager;

public class SoundTest : MonoBehaviour
{
    FMOD.Studio.System system;


    //Knife throw test sounds
    public string knifeThrowPath;
    FMOD.Studio.EventInstance knifeThrowInstance;

    public string knifeHitPath;
    FMOD.Studio.EventInstance[] knifeHitInstance = new FMOD.Studio.EventInstance[6];

    public string knifeClangPath;
    FMOD.Studio.EventInstance knifeClangInstance;

    FMOD.Studio.PLAYBACK_STATE playbackState;


    void Awake()
    {
        system = FMODUnity.RuntimeManager.StudioSystem;

        //Creating event instance and playing sound
        //eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        //eventInstance.start();

        if (FMODUnity.RuntimeManager.HasBankLoaded("Master"))
        {
            Debug.Log("Master Bank Loaded");
            //Knife event instances
            knifeThrowInstance = FMODUnity.RuntimeManager.CreateInstance(knifeThrowPath);
            knifeClangInstance = FMODUnity.RuntimeManager.CreateInstance(knifeClangPath);

            for (int i = 0; i < knifeHitInstance.Length; i++)
            {
                knifeHitInstance[i] = FMODUnity.RuntimeManager.CreateInstance(knifeHitPath);
            }
        }
    }

    void Update()
    {
        /*eventInstance.setParameterByName("Intensity", intensity);
        if (Input.GetKey(KeyCode.UpArrow)) {
            intensity += Time.deltaTime * scale;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            intensity -= Time.deltaTime * scale;
        }*/

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
    }
}
