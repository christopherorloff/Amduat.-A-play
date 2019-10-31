using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class LightCone2_Movement_Hour10_Script : MonoBehaviour
{
    public GameObject SecondGoddessCone;
    public GameObject ThirdGoddessCone;

    public float speed = 50.0f;


    // Start is called before the first frame update
    void Start()
    {
        SecondGoddessCone.SetActive(true);
        ThirdGoddessCone.SetActive(false);
        //ThirdGoddessCone.SetActive(false);
        //FourthGoddessCone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Scroll.scrollDirection() == "Up")
        {
            SecondGoddessCone.transform.Rotate(Vector3.forward * -speed * Time.deltaTime);
            //FourthGoddessCone.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ConeZone")
        {
            ThirdGoddessCone.SetActive(true);
            SecondGoddessCone.SetActive(false);
        }
    }
}
