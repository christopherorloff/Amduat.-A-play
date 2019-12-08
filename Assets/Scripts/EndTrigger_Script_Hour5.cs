using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger_Script_Hour5 : MonoBehaviour
{
    public FadeUIScript fadeUIScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boat")
        {
            fadeUIScript.StartFadeOut();
        }
    }
}
