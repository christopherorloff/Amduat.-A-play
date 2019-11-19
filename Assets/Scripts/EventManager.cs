using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    // public static EventManager Instance;

    // Game Manager events
    public delegate void GM();
    public static GM sceneChange;

    // HOUR 6 events
    public delegate void Hour6();

    public static Hour6 turnOffInputEvent;
    public static Hour6 turnOnInputEvent;
    public static Hour6 knifeHitEvent;
    public static Hour6 snakeDeadEvent;

    // private void Awake()
    // {
    //     // if the singleton hasn't been initialized yet
    //     if (Instance != null && Instance != this)
    //     {
    //         Destroy(this.gameObject);
    //         return;//Avoid doing anything else
    //     }
    //     Instance = this;
    //     DontDestroyOnLoad(this.gameObject);
    // }
}
