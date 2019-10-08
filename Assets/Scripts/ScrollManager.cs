using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScrollManager
{
    public class Scroll : MonoBehaviour
    {
        //Notes
        //Remember to have a variable to invert direction depending on scroll direction
        

        // Detect beginning of input - Event maybe?

        // Bool - Input or not
        public static bool isScrolling()
        {
            bool status = false;
            status = (Input.mouseScrollDelta.y != 0)?true:false;
            return status;
        }
        // Direction - Are we receiving input, and is it up or down

        // Input with acceleration (artificial)

        // Input with acceleration (system based)
    }

}
