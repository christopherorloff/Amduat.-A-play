﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone1_CollisionDetection_Hour10_Script : MonoBehaviour
{
    public bool firstConeCollision = false;

    public Cones_Movement_Hour10_Script coneManagement;

    private void Start()
    {
        coneManagement = FindObjectOfType<Cones_Movement_Hour10_Script>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ConeZone")
        {
            firstConeCollision = true;
            coneManagement.coneOneCanMove = false;
            coneManagement.coneTwoCanMove = true;
        }
    }

}
