using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setParent : MonoBehaviour
{

    public Transform snake; 


    void Start()
    {

        this.transform.SetParent(snake);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
