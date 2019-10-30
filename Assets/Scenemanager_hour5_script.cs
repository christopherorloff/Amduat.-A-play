using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenemanager_hour5_script : MonoBehaviour
{

    public GameObject Boat;
    public CheckCollisonBoat_Script TriggerScript;
    int numberOfSnakes;
    float boatMoveSpeedX;

    void Start()
    {
        boatMoveSpeedX = 0.7f;
    }

    void Update()
    {

        Boat.transform.position += new Vector3(boatMoveSpeedX * Time.deltaTime, 0, 0);


        if (TriggerScript.numberOfSnakes > 3)
        {
            boatMoveSpeedX = 0.5f;
        }
        if (TriggerScript.numberOfSnakes > 7)
        {
            boatMoveSpeedX = 0.4f;
        }
        if (TriggerScript.numberOfSnakes > 10)
        {
            boatMoveSpeedX = 0.2f;
        }
        if (TriggerScript.numberOfSnakes > 14)
        {
            boatMoveSpeedX = 0.1f;
        }
        if (TriggerScript.numberOfSnakes > 18)
        {
            boatMoveSpeedX = 0.0f;
        }
    }



    
}
