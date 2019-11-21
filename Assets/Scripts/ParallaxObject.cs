using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
    public ParallaxBackground parallaxLayer;
    private Transform cameraTransform;
    private Transform obj;

    //private float viewZone = 10;
    private float lastCameraX;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("2DCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * parallaxLayer.paralexSpeed);
        lastCameraX = cameraTransform.position.x;
    }
}