using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEffect : MonoBehaviour
{

    public SpearAnimation SA;

    // Start is called before the first frame update
    void Start()
    {
        SA = FindObjectOfType<SpearAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SA.stopEffect2)
        {
            GetComponent<ParticleSystem>().Stop();
            SA.stopEffect2 = false;
        }



    }
}
