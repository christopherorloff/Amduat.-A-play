using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessedDead_NakedDetection_Hour8_Script : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlessedDead")
        {
            collision.GetComponent<BlessedDead_Transformation_Hour8_Script>().isNaked = true;
        }
    }
}
