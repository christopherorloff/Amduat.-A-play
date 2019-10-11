using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

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

            }

        }
        if (Input.GetKeyDown(KeyCode.D)) knifeClangInstance.start();
    }
}
