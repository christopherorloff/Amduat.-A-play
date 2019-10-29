using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour
{

    public TowBoatLogic TowScript;
    float moveSpeed = 1f;

    void Start()
    {
        TowScript = FindObjectOfType<TowBoatLogic>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (TowScript.pulling)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);

        }
    }
}
