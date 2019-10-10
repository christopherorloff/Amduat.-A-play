using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class TakeInput : MonoBehaviour
{
    float highest = 0;
    void Update()
    {
        highest = (Mathf.Abs(Scroll.scrollValue()) > highest) ? Mathf.Abs(Scroll.scrollValue()) : highest;
        print ("Highest: " + highest);
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            highest = 0;
        }
    }
}
