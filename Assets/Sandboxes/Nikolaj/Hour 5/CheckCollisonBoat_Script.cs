using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisonBoat_Script : MonoBehaviour
{


    public int numberOfSnakes;

    void Start()
    {
        numberOfSnakes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snake")
        {
            numberOfSnakes++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snake")
        {
            numberOfSnakes--;
        }
    }
}
