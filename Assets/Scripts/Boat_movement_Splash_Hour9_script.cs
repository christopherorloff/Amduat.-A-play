using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class Boat_movement_Splash_Hour9_script : MonoBehaviour
{

    float boatMoveSpeedX = 0.8f;
    public bool waterSplash = false;
    bool corutineRunning = false;

    void Start()
    {
        
    }

    void Update()
    {

        print(Scroll.scrollValueAccelerated());

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {

            
            transform.position += new Vector3(boatMoveSpeedX * Time.deltaTime, 0, 0);


            if (!corutineRunning)
            {
                waterSplash = true;
                StartCoroutine(ToggleWaterSplash());

            }



        }
        



    }


    public IEnumerator ToggleWaterSplash()
    {
        corutineRunning = true;
        yield return new WaitForSeconds(0.2f);
        waterSplash = false;
        corutineRunning = false;

    }

}
