using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMovement : MonoBehaviour
{

    [SerializeField]
    [Range(-10, 10)]
    private float rotateSpeed;

    [SerializeField]
    [Range(0, 10)]
    private float amplitudeX;

    [SerializeField]
    [Range(0, 10)]
    private float frequenzyX;

    [SerializeField]
    [Range(0, 10)]
    private float amplitudeY;

    [SerializeField]
    [Range(0, 10)]
    private float frequenzyY;

    [SerializeField]
    private GameObject wheel;

    [SerializeField]
    private bool wheelControlled;

    [SerializeField]
    private bool rotating;

    [SerializeField]
    private bool floating;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        if (rotating)
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed));
        }
        if(floating)
        {
            x = Mathf.Cos(Time.time * frequenzyX) * amplitudeX;
            y = Mathf.Sin(Time.time * frequenzyY) * amplitudeY;
            transform.position = new Vector3(x, y, z) + initialPosition;
        }
    }
}
