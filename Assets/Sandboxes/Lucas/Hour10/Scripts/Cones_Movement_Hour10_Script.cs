using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class Cones_Movement_Hour10_Script : MonoBehaviour
{
    public GameObject FirstGoddessCone; //Movement direction = "Down"
    public GameObject SecondGoddessCone; //Movement direction = "Up"
    public GameObject ThirdGoddessCone; //Movement direction = "Down"
    public GameObject FourthGoddessCone; //Movement direction = "Up"

    public GameObject Boat;

    public Transform FirstTarget;
    public Transform SecondTarget;
    public Transform ThirdTarget;
    public Transform FourthTarget;

    public float ConeSpeed = 50.0f;
    public float BoatSpeed = 5.0f;

    public bool coneOneCanMove = true;
    public bool coneTwoCanMove = false;
    public bool coneThreeCanMove = false;
    public bool coneFourCanMove = false;

    public Cone1_CollisionDetection_Hour10_Script ConeOneCollision;
    public Cone2_CollisionDetection_Hour10_Script ConeTwoCollision;
    public Cone3_CollisionDetection_Hour10_Script ConeThreeCollision;
    public Cone4_CollisionDetection_Hour10_Script ConeFourCollision;


    // Start is called before the first frame update
    void Start()
    {
        FirstGoddessCone.GetComponent<SpriteRenderer>().enabled = true;
        SecondGoddessCone.GetComponent<SpriteRenderer>().enabled = false;
        ThirdGoddessCone.GetComponent<SpriteRenderer>().enabled = false;
        FourthGoddessCone.GetComponent<SpriteRenderer>().enabled = false;

        ConeOneCollision = FindObjectOfType<Cone1_CollisionDetection_Hour10_Script>();
        ConeTwoCollision = FindObjectOfType<Cone2_CollisionDetection_Hour10_Script>();
        ConeThreeCollision = FindObjectOfType<Cone3_CollisionDetection_Hour10_Script>();
        ConeFourCollision = FindObjectOfType<Cone4_CollisionDetection_Hour10_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        ConeRotation();
        BoatMovement();
    }


    void ConeRotation()
    {
        //ROTATING THE FIRST CONE
        if (Scroll.scrollDirection() == "Down" && coneOneCanMove == true)
        {
            FirstGoddessCone.transform.Rotate(Vector3.forward * -ConeSpeed * Time.deltaTime);
        }

        //ROTATING THE SECOND CONE
        if (Scroll.scrollDirection() == "Up" && coneTwoCanMove == true)
        {
            SecondGoddessCone.GetComponent<SpriteRenderer>().enabled = true;
            FirstGoddessCone.GetComponent<BoxCollider2D>().enabled = false;
            SecondGoddessCone.transform.Rotate(Vector3.forward * -ConeSpeed * Time.deltaTime);
        }

        //ROTATING THE THIRD CONE
        if (Scroll.scrollDirection() == "Down" && coneThreeCanMove == true)
        {
            ThirdGoddessCone.GetComponent<SpriteRenderer>().enabled = true;
            SecondGoddessCone.GetComponent<BoxCollider2D>().enabled = false;
            ThirdGoddessCone.transform.Rotate(Vector3.forward * -ConeSpeed * Time.deltaTime);
        }

        //ROTATING THE FOURTH CONE
        if (Scroll.scrollDirection() == "Up" && coneFourCanMove == true)
        {
            FourthGoddessCone.GetComponent<SpriteRenderer>().enabled = true;
            ThirdGoddessCone.GetComponent<BoxCollider2D>().enabled = false;
            FourthGoddessCone.transform.Rotate(Vector3.forward * -ConeSpeed * Time.deltaTime);
        }
    }
    void BoatMovement()
    {
        float step = BoatSpeed * Time.deltaTime;

        //MOVING THE BOAT TO FIRST TARGET
        if (ConeOneCollision.firstConeCollision == true)
        { 
            Boat.transform.position = Vector3.MoveTowards(Boat.transform.position, FirstTarget.position, step);
            if (Boat.transform.position.x >= FirstTarget.position.x)
            {
                ConeOneCollision.firstConeCollision = false;
            }
        }

        //MOVING THE BOAT TO SECOND TARGET
        if (ConeTwoCollision.secondConeCollision == true)
        {
            Boat.transform.position = Vector3.MoveTowards(Boat.transform.position, SecondTarget.position, step);
            if (Boat.transform.position.x >= SecondTarget.position.x)
            {
                ConeTwoCollision.secondConeCollision = false;
            }
        }

        //MOVING THE BOAT TO THIRD TARGET
        if (ConeThreeCollision.thirdConeCollision == true)
        {
            Boat.transform.position = Vector3.MoveTowards(Boat.transform.position, ThirdTarget.position, step);
            if (Boat.transform.position.x >= ThirdTarget.position.x)
            {
                ConeThreeCollision.thirdConeCollision = false;
            }

        }

        //MOVING THE BOAT TO FOURTH TARGET
        if (ConeFourCollision.fourthConeCollision == true)
        {
            Boat.transform.position = Vector3.MoveTowards(Boat.transform.position, FourthTarget.position, step);
            if(Boat.transform.position.x >= FourthTarget.position.x)
            {
                ConeFourCollision.fourthConeCollision = false;
            }
        }
    }
}

