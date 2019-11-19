using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Change_Direction_Script : MonoBehaviour
{

    public bool DirectionDown = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (DirectionDown)
        {
            gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        if (!DirectionDown)
        {
            gameObject.transform.rotation = Quaternion.Euler(270, 0, 0);

        }
    }
}
