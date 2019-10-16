using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class MoveWithScroll : MonoBehaviour
{
    float minx, maxx, miny, maxy;
    float webScalar = 0;
    void Start()
    {
        minx = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        maxx = Camera.main.ViewportToWorldPoint(Vector3.one).x;
        miny = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
        maxy = Camera.main.ViewportToWorldPoint(Vector3.one).y;
    }
    
    // Update is called once per frame
    void Update()
    {
        float move = transform.position.y + (Scroll.scrollValueAccelerated() * webScalar);
        Vector3 vel = new Vector3 (0,move, 0);
        transform.position = vel;

        // Keep cube on screen
        if (transform.position.y > maxy)
        {
            transform.position = new Vector3(transform.position.x, miny);
        } else if (transform.position.y < miny)
        {
            transform.position = new Vector3(transform.position.x, maxy);
        }

    }

    public void UpdateScalar(float input)
    {
        webScalar = input;
    }
}
