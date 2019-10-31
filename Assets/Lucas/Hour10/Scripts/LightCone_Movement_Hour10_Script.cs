using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class LightCone_Movement_Hour10_Script : MonoBehaviour
{

    public GameObject FirstGoddessCone;
    public GameObject SecondGoddessCone;
    public GameObject ThirdGoddessCone;
    public GameObject FourthGoddessCone;

    public float speed = 50.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Scroll.scrollDirection() == "Down")
        {
            transform.Rotate(Vector3.forward * -speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
