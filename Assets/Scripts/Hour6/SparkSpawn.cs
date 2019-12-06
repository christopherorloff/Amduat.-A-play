using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkSpawn : MonoBehaviour
{

    public bool NewKnife = false;
    bool makeSmall = false;
    float rotateSpeed = 300;
    bool running = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {

        if (NewKnife  && !running)
        {
            StartCoroutine(ScaleUpAndDown());
        }


    }


    IEnumerator ScaleUpAndDown()
        {

            running = true;

            while (transform.localScale.x <= 1)
            {
                transform.localScale += new Vector3(0.6f * Time.deltaTime, 0.6f * Time.deltaTime, 0);
                transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
                yield return null;
            }

            while (transform.localScale.x >= 0)
            {
                transform.localScale -= new Vector3(0.6f * Time.deltaTime, 0.6f * Time.deltaTime, 0);
                transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
                yield return null;

            }
            NewKnife = false;
            running = false;

        }
        

    


}
