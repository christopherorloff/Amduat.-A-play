using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class TakeInput : MonoBehaviour
{
    Event e;
    void Start()
    {
        
    }
    void Update()
    {
        //print ("Var: " + Scroll.scrollValueFilteredVar());
        print("Axis: "+ Scroll.scrollValueMean(10));
    }
}
