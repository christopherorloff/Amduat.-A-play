using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class LimbMovement : MonoBehaviour
{
    //SCROLL VARIABLES
    bool lastDirectionUp = true;

    //LIMB VARIABLES
    public GameObject[] limbs;
    public int counter;

    //ROTATION VARIABLES
    public Vector3[] rotationTargets;
    public float speed = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        //Setting first limb from limbs array to be the active limb
        limbs[counter].GetComponent<Limb>().isActive = true;
    }

    void Update()
    {
        CheckIfMoving();

        //Rotate camera
        float step = speed * Time.deltaTime;

        if(counter >= 1) {
            Quaternion rotation = Quaternion.Euler(rotationTargets[counter - 1]);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, step);
        }
    }

    void Move()
    {
        counter++;
        if(counter-1 < limbs.Length) {
            //Setting the correct limb to move
            limbs[counter - 1].GetComponent<Limb>().isMoving = true;
            limbs[counter].GetComponent<Limb>().isActive = true;
        }
    }

    //Checking if last direction is opposite of the previous one - if it is, then a limb will move
    void CheckIfMoving() {
        if (lastDirectionUp) {
            if (Scroll.scrollValue() < -0.05f) {
                //Setting current limb from limbs array to be not active limb
                limbs[counter].GetComponent<Limb>().isActive = false;

                Move();
                lastDirectionUp = false;
            }
        }
        else if (!lastDirectionUp) {
            if (Scroll.scrollValue() > 0.05f) {
                //Setting current limb from limbs array to be not active limb
                limbs[counter].GetComponent<Limb>().isActive = false;

                Move();
                lastDirectionUp = true;
            }
        }
    }
}
