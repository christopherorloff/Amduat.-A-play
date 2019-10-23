using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Game Manager events
    public delegate void GM();
    
    public static GM sceneChange;

    // HOUR 6 events
    public delegate void Hour6();

    public static Hour6 turnOffInputEvent;
    public static Hour6 turnOnInputEvent;
    public static Hour6 knifeHitEvent;
    public static Hour6 snakeDeadEvent;
}
