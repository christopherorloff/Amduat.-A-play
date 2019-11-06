using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidIndividual_Script : MonoBehaviour
{
    private List<Collider2D> localFlockColliders;
    private ContactFilter2D contactFilter2D;

    void Start()
    {
        localFlockColliders = new List<Collider2D>();
        contactFilter2D.useTriggers = true;
    }

    public List<Collider2D> GetLocalFlock(float radius)
    {
        localFlockColliders.Clear();
        Physics2D.OverlapCircle(this.transform.position, radius, contactFilter2D, localFlockColliders);
        return localFlockColliders;
    }
}
