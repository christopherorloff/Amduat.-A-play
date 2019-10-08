using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class TakeInput : MonoBehaviour
{
    
    void Update()
    {
        print (Scroll.isScrolling());
    }
}
