using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowBoatLogic : MonoBehaviour
{

    float energy;
    float scroll;
    float drag = 1.5f;
    float rotSpeed = 100f;
    float pullRotSpeed = 20;
    float maxEnergy = -20f;
    float startPos = 10f;
    float moveSpeed = 1f;


    public bool startEffect = false;
    public bool readyToPull = false;
    public bool pulling = false;


    void Start()
    {
        energy = startPos;


    }

    void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");

        transform.rotation = Quaternion.Euler(0, 180, energy);


        if (scroll < 0 && !readyToPull)
        {
            energy -= (Mathf.Abs(scroll) * rotSpeed * Time.deltaTime);
        }

        if (energy <= maxEnergy && !readyToPull)
        {
            print("readytopull");
            readyToPull = true;
           
        }
        if (scroll > 0.1f && readyToPull)
        {
            print("Pulling");
            pulling = true;
            startEffect = true;
            
            
        }
        if (pulling)
        {
            energy += pullRotSpeed * Time.deltaTime;
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);

            if (energy >= startPos)
            {
                energy = startPos;
                readyToPull = false;
                pulling = false;
                print("startpos");
            }
        }



        if (scroll == 0 && !readyToPull)
        {
            energy += drag * Time.deltaTime;
            if (energy >= startPos)
            {
                energy = startPos;
            }
        }
  
    }

}
