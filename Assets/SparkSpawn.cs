using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkSpawn : MonoBehaviour
{

    public bool NewKnife = false;
    bool makeSmall = false;
    float rotateSpeed = 300;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {

        if (NewKnife)
        {
            transform.localScale += new Vector3(0.6f * Time.deltaTime, 0.6f * Time.deltaTime, 0);
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            if (transform.localScale.x >= 1)
            {
                NewKnife = false;
                makeSmall = true;
            }
        }

        if (makeSmall)
        {
            transform.localScale -= new Vector3(0.6f * Time.deltaTime, 0.6f * Time.deltaTime, 0);
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

            if (transform.localScale.x <= 0)
            {
                makeSmall = false;
            }
        }

    }
}
