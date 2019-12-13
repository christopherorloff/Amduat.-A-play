using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class InputTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Scroll.OnScrollEnter += OnEnter;
        Scroll.OnScrollExit += OnExit;
    }

    // Update is called once per frame
    void Update()
    {
       // print("InputValue: " + Scroll.scrollValue());
    }

    void OnEnter() { print("OnEnter"); }
    void OnExit() { print("OnExit"); }
}
