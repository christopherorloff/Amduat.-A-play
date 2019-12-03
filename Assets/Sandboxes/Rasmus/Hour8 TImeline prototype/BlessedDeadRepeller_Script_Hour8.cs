using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlessedDeadRepeller_Script_Hour8 : MonoBehaviour
{
    public float overlapRadius = 1;
    private ContactFilter2D contactFilter2D;
    private List<Collider2D> colliders;

    void Start()
    {
        colliders = new List<Collider2D>();

    }

    void Update()
    {
        colliders.Clear();
        Physics2D.OverlapCircle((Vector2)transform.position, overlapRadius, contactFilter2D, colliders);
        Check
    }
}
