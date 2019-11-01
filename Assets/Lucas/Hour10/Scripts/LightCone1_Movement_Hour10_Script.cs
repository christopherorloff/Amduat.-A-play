using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class LightCone1_Movement_Hour10_Script : MonoBehaviour
{
    public GameObject StaticCone;
    public GameObject FirstGoddessCone;
    public GameObject SecondGoddessCone;

    public float speed = 50.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StaticCone.SetActive(true);
        FirstGoddessCone.SetActive(true);
        SecondGoddessCone.SetActive(false);
        //ThirdGoddessCone.SetActive(false);
        //FourthGoddessCone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Scroll.scrollDirection() == "Down")
        {
            FirstGoddessCone.transform.Rotate(Vector3.forward * -speed * Time.deltaTime);
            //ThirdGoddessCone.transform.Rotate(Vector3.forward * -speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ConeZone")
        {
            SecondGoddessCone.SetActive(true);
            StaticCone.SetActive(false);
        }
    }
}
