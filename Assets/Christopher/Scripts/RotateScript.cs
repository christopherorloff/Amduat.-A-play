using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    [Range (-10,10)]
    public float turnSpeed;

    [Range (-10,10)]
    public float intensity;

    public GameObject wheel;
    public bool wheelControlled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(wheelControlled)
        {
            transform.Rotate(new Vector3(0, 0, /*wheel.input **/ intensity));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, turnSpeed));
        }
        
    }
}
