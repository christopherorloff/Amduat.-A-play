using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveTrigger_Script_Hour5 : MonoBehaviour
{
    public GraveFadeHover_Script_hour5 grave;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boat")
        {
            grave.StartFadeIn(5);
        }
    }
}
