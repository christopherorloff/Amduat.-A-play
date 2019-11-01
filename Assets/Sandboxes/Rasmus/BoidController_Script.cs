using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController_Script : MonoBehaviour
{
    BoidIndividual_Script[] flock;

    void Start()
    {
        flock = GetComponentsInChildren<BoidIndividual_Script>();

        for (int i = 0; i < flock.Length; i++)
        {
            //Get local boids
            //
        }
    }

}
