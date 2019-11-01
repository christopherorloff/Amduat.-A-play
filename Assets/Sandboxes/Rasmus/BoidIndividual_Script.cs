using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidIndividual_Script : MonoBehaviour
{
    private List <Collider2D> localFlockColliders;
    private List <BoidIndividual_Script> localFlock;
    private ContactFilter2D contactFilter2D;
    private Collider2D collider2D;


    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        localFlock = new List<BoidIndividual_Script>();
        localFlockColliders = new List<Collider2D>();
        contactFilter2D.useTriggers = true;
    }

    public void GetLocalFlock(float radius)
    {
        localFlock.Clear();
        Physics2D.OverlapCircle(this.transform.position, radius,contactFilter2D,localFlockColliders);
        print(this.name);
        foreach (var item in localFlockColliders)
        {
            if (item.name == this.name)
            {
                continue;
            }
            print("-- " + item.name);
        }

    }
}
