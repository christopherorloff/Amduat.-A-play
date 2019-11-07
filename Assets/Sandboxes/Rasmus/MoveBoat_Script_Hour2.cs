using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class MoveBoat_Script_Hour2 : MonoBehaviour
{
    public float speed = 3;
    public float drag = 0.99f;
    void Update()
    {
        if (Scroll.scrollValueAccelerated(drag) > 0)
        {
            print("Input: " + Scroll.scrollValueAccelerated(drag));
            transform.position += new Vector3(Scroll.scrollValueAccelerated(drag) * Time.deltaTime * speed, 0, 0);
        }
    }
}
