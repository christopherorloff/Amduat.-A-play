using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class BlessedDead_ClothingDetection_Hour8_Script : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlessedDead" && Scroll.isScrolling())
        {
            collision.GetComponent<BlessedDead_Transformation_Hour8_Script>().isNaked = false;
        }
    }
}
