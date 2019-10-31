using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMeasure : MonoBehaviour
{
    public Transform target;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
    }
}
