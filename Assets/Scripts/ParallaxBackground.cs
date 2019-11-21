using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float backgroundSize;
    public float paralexSpeed;
    public float scale;
    private Transform cameraTransform;
    public Transform[] layers;
    public bool scrolling, parallax;

    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;

    // Start is called before the first frame update
    void Start()
    {
        backgroundSize = backgroundSize * scale;
        leftIndex = 0;
        rightIndex = layers.Length - 1;
        cameraTransform = GameObject.FindGameObjectWithTag("2DCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parallax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * paralexSpeed);
        }
        lastCameraX = cameraTransform.position.x;

        if (scrolling)
        {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
            {
                ScrollLeft();
            }

            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
            {
                ScrollRight();
            }
        }
    }

    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        //layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backgroundSize, 9.565579f, 4.4f);
        layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backgroundSize, layers[leftIndex].position.y, layers[leftIndex].position.z);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = new Vector3((layers[rightIndex].position.x + backgroundSize), layers[rightIndex].position.y, layers[rightIndex].position.z);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }

}
