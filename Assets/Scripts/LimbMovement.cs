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

    public float limbSpeed = 1;

    //ROTATION VARIABLES
    public Vector3[] rotationTargets;
    public float rotationSpeed = 0.3F;
    public float scrollThreshold = 0.8f;
    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        //Setting first limb from limbs array to be the active limb
        limbs[counter].GetComponent<Limb>().isActive = true;
        for(int i = 0; i < limbs.Length; i++) {
            limbs[i].GetComponent<Limb>().smoothTime = limbSpeed;
        }
    }

    void Update()
    {
        CheckIfMoving();

        //Rotate entire object
        float step = rotationSpeed * Time.deltaTime;

        if(counter > 0 && counter < rotationTargets.Length) {
            Quaternion rotation = Quaternion.Euler(rotationTargets[counter]);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, step);
        }
    }

    void Move()
    {
        counter++;
        if(counter - 1 < limbs.Length) {
            //Setting the correct limb to move
            limbs[counter - 1].GetComponent<Limb>().isMoving = true;

            //If there is another limb, it is set to become active here
            if(counter < limbs.Length) {
                limbs[counter].GetComponent<Limb>().isActive = true;
            }
        }
    }

    //Checking if last direction is opposite of the previous one - if it is, then a limb will move
    void CheckIfMoving() {
        if (lastDirectionUp) {
            if (Scroll.scrollValue() < -scrollThreshold) {
                //Setting current limb from limbs array to be not active limb
                SetLimbNotActive();

                Move();
                lastDirectionUp = false;
            }
        }
        else if (!lastDirectionUp) {
            if (Scroll.scrollValue() > scrollThreshold) {
                //Setting current limb from limbs array to be not active limb
                SetLimbNotActive();

                Move();
                lastDirectionUp = true;
            }
        }
    }

    void SetLimbNotActive() {
        if(counter < limbs.Length) limbs[counter].GetComponent<Limb>().isActive = false;
    }
}
