using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlessedDeadRepeller_Script_Hour8 : MonoBehaviour
{
    public float overlapRadius = 1;
    public float speed = 2;
    public float forwardOffsetOverlapCircle = 0.5f;
    private Vector3 offsetVector;
    private ContactFilter2D contactFilter2D;
    private List<Collider2D> colliders;

    void Start()
    {
        colliders = new List<Collider2D>();
    }

    void Update()
    {
        colliders.Clear();
        Physics2D.OverlapCircle((Vector2)transform.position + (forwardOffsetOverlapCircle * Vector2.right), overlapRadius, contactFilter2D, colliders);
        CheckResults();
    }

    private void CheckResults()
    {
        if (colliders.Count > 0)
        {
            foreach (var item in colliders)
            {
                if (item.transform.position.y >= transform.position.y)
                {
                    item.transform.position += Vector3.up * speed * Time.deltaTime;
                }
                else
                {
                    item.transform.position += Vector3.down * speed * Time.deltaTime;
                }
            }
        }
    }
}
