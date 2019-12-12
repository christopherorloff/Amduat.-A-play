using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public SpearAnimation SA;

    public Camera mainCam;

    private float shakeAmount = 0;
    private Vector3 initalPos;

    void Awake()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }
    }

    public void Start()
    {
        initalPos = mainCam.transform.position;
        SA = FindObjectOfType<SpearAnimation>();
    }

    public void Shake(float amt, float lenght)
    {
        shakeAmount = amt;
        initalPos = mainCam.transform.position;
        InvokeRepeating("BeginShake", 0, 0.001f);
        Invoke("StopShake", lenght);
    }

    public void EarlyShake(float amt, float lenght)
    {
        shakeAmount = amt;
        InvokeRepeating("BeginShake", 0, 0.001f);
        Invoke("StopShake2", lenght);


    }

    void BeginShake()
    {
        if (shakeAmount > 0)
        {

            Vector3 camPos = mainCam.transform.position;
            float offsetX = Random.value * shakeAmount * 2f - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2f - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }


    }


    public void StopShake()
    {
        CancelInvoke("BeginShake");
        //mainCam.transform.localPosition = SA.cameraObject.transform.position;
        mainCam.transform.localPosition = new Vector3(0f,0,-10);
    }
    public void StopShake2()
    {
        CancelInvoke("BeginShake");
        mainCam.transform.localPosition = SA.cameraObject.transform.position;


    }

}
