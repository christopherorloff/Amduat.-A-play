using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController_Script : MonoBehaviour
{
    //Individual Boid Specs
    Transform[] flockTransforms;
    int[] instanceIDs;
    List<Collider2D>[] localFlock;
    Vector3[] velocities;


    //Interface
    public float overlapRadius;
    public float speed = 1;
    public float seperationDistance = 0.1f;
    private float seperationDistanceSquared;


    private ContactFilter2D contactFilter2D;
    private int flocksize;

    void Start()
    {
        // References to boid specs
        flocksize = this.transform.childCount;
        flockTransforms = new Transform[flocksize];
        instanceIDs = new int[flocksize];
        localFlock = new List<Collider2D>[flocksize];
        velocities = new Vector3[flocksize];

        seperationDistanceSquared = seperationDistance * seperationDistance;

        // Setting up references and initial values for boids
        for (int i = 0; i < flocksize; i++)
        {
            flockTransforms[i] = transform.GetChild(i);
            // flockTransforms[i].position = new Vector3(
            //     flockTransforms[i].position.x,
            //     flockTransforms[i].position.y, 0);
            localFlock[i] = new List<Collider2D>();
            instanceIDs[i] = flockTransforms[i].GetInstanceID();
            velocities[i] = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f).normalized;
        }
    }

    // All vectors must be collected from flock before any movement happens
    void Update()
    {
        GetDataFromFlock();
        MoveFlock();
    }

    private void GetDataFromFlock()
    {
        for (int i = 0; i < flockTransforms.Length; i++)
        {
            //Cleanup
            localFlock[i].Clear();

            //Get local boids
            Physics2D.OverlapCircle((Vector2)flockTransforms[i].position, overlapRadius, contactFilter2D, localFlock[i]);
            print("flocksize: " + localFlock[i].Count);
            // //Get seperation
            Vector3 seperation = GetSeperation(i);
            // //Get alignment
            // Vector3 alignment = GetAlignment();
            // //Get cohesion
            // Vector3 cohesion = GetCohesion();
            //Get softBounds
            //Goal
            //Add it all together
            velocities[i] += seperation;

        }
    }

    private void MoveFlock()
    {
        for (int i = 0; i < flockTransforms.Length; i++)
        {
            //Move the boids
            flockTransforms[i].position += velocities[i] * speed * Time.deltaTime;
            flockTransforms[i].rotation = Quaternion.FromToRotation(Vector3.up, velocities[i]);
        }
    }

    private Vector3 GetSeperation(int id)
    {
        Vector3 output = Vector3.zero;

        foreach (var item in localFlock[id])
        {
            print("Getting seperation");
            if (item.GetInstanceID() == instanceIDs[id]) { continue; }
            Vector3 vectorBetween = item.transform.position - flockTransforms[id].position;
            float squaredMagnitude = vectorBetween.sqrMagnitude;
            if (squaredMagnitude <= seperationDistanceSquared)
            {
                print("Inside: " + flockTransforms[id].name);
                output -= vectorBetween;
            }
        }


        return output;
    }

    private Vector3 GetAlignment(int id)
    {
        Vector3 output = Vector3.zero;

        return output;
    }

    private Vector3 GetCohesion(int id)
    {
        Vector3 output = Vector3.zero;

        return output;
    }


}
