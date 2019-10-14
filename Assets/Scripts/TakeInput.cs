using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class TakeInput : MonoBehaviour
{
    void Awake()
    {
        print("OS: " + SystemInfo.operatingSystemFamily);
    }

    float highest = 0;
    void Update()
    {
        highest = (Mathf.Abs(Scroll.scrollValue()) > highest) ? Mathf.Abs(Scroll.scrollValue()) : highest;
        //print("Highest: " + highest);
        //print(Scroll.scrollValue());

        if (Input.GetKeyDown(KeyCode.R))
        {
            highest = 0;
        }
    }
}
