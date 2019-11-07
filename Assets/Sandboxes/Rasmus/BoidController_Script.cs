using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController_Script : MonoBehaviour
{
    //Individual Boid Specs
    Transform[] flock;
    int[] instanceIDs;
    List<Collider2D>[] localFlock;
    Vector3[] velocities;


    //Interface
    public float overlapRadius;
    public float speed = 1;

    private ContactFilter2D contactFilter2D;
    private int flocksize;

    void Start()
    {
        // References to boid specs
        flocksize = this.transform.childCount;
        flock = new Transform[flocksize];
        instanceIDs = new int[flocksize];
        localFlock = new List<Collider2D>[flocksize];
        velocities = new Vector3[flocksize];

        for (int i = 0; i < flocksize; i++)
        {
            flock[i] = transform.GetChild(i);
            instanceIDs[i] = flock[i].GetInstanceID();
            velocities[i] = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f).normalized;
            print("V: " + Vector3.Magnitude(velocities[i]));
        }
    }

    void Update()
    {
        for (int i = 0; i < flock.Length; i++)
        {
            //Cleanup

            //Get local boids
            // Physics2D.OverlapCircle(this.transform.position, overlapRadius, contactFilter2D, localFlock[i]);

            // //Get seperation
            // Vector3 seperation = GetSeperation();
            // //Get alignment
            // Vector3 alignment = GetAlignment();
            // //Get cohesion
            // Vector3 cohesion = GetCohesion();
            //Get softBounds
            //Goal

            //Add it all together
            //Move the boids
            flock[i].position += velocities[i] * speed * Time.deltaTime;
            flock[i].rotation = Quaternion.FromToRotation(Vector3.up, velocities[i]);
        }
    }

    private Vector3 GetSeperation()
    {
        Vector3 output = new Vector3();

        return output;
    }

    private Vector3 GetAlignment()
    {
        Vector3 output = new Vector3();

        return output;
    }

    private Vector3 GetCohesion()
    {
        Vector3 output = new Vector3();

        return output;
    }


}
