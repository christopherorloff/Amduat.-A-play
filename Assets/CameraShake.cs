using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public SpearAnimation SA;

    public Camera mainCam;

    private float shakeAmount = 0;

    void Awake()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }
    }

    public void Start()
    {
        SA = FindObjectOfType<SpearAnimation>();
    }

    public void Shake(float amt, float lenght)
    {
        shakeAmount = amt;
        InvokeRepeating("BeginShake", 0, 0.001f);
        Invoke("StopShake", lenght);
    }

    public void EarlyShake(float amt, float lenght)
    {
        shakeAmount = amt;
        InvokeRepeating("BeginShake", 0, 0.01f);
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
        mainCam.transform.localPosition = SA.cameraObject.transform.position;

    }
    public void StopShake2()
    {
        CancelInvoke("BeginShake");
        mainCam.transform.localPosition = SA.cameraObject.transform.position;


    }

}
