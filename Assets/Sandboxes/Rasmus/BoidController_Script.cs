using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController_Script : MonoBehaviour
{
    BoidIndividual_Script[] flock;

    //Interface
    public float overlapsRadius;

    void Start()
    {
        flock = GetComponentsInChildren<BoidIndividual_Script>();

        for (int i = 0; i < flock.Length; i++)
        {
            //Get local boids
            //Get seperation
            //Get alignment
            //Get cohesion
            //Get softBounds
            //Goal
            
            //Add it all together
            flock[i].GetLocalFlock(overlapsRadius);
        }
    }

}
