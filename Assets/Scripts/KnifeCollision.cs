using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCollision : MonoBehaviour
{
    Transform parent;
    void OnEnable()
    {
        parent = transform.parent;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        parent.parent = other.transform;
        Destroy(parent.GetComponent<Rigidbody2D>());
        parent.GetComponent<ThrowScript>().KnifeHit();
    }
}
