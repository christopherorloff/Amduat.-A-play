using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Simple_hover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.up * Mathf.Cos(Time.deltaTime);
    }
}
