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

    void Start()
    {
        system = FMODUnity.RuntimeManager.StudioSystem;

        //Creating event instance and playing sound
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        eventInstance.start();
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
    }
}
