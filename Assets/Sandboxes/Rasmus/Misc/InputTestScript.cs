using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class InputTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
<<<<<<< HEAD

=======
        Scroll.OnScrollEnter += OnEnter;
        Scroll.OnScrollExit += OnExit;
>>>>>>> 9ca83829bde308d5d83ae68ef926657928314474
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        print("InputValue: " + Scroll.scrollValue());
=======
       // print("InputValue: " + Scroll.scrollValue());
>>>>>>> 9ca83829bde308d5d83ae68ef926657928314474
    }

    void OnEnter() { print("OnEnter"); }
    void OnExit() { print("OnExit"); }
}
