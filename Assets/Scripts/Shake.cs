using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float magnitude;

    void Update()
    {
        float x = Random.Range(-1, 1) * magnitude;
        float y = Random.Range(-1, 1) * magnitude;
        transform.localPosition = new Vector3(x, y, transform.position.z);
    }
}
