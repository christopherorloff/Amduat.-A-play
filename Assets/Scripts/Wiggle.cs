using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class Wiggle : MonoBehaviour
{
    Vector3 originalPosition;
    public float wiggleMagnitude = 25;
    public float speed = 1f;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(originalPosition, new Vector3(transform.position.x, transform.position.y + Scroll.scrollValueAccelerated() * wiggleMagnitude, transform.position.z), ref velocity, speed);
    }
}
