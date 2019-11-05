using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_HitZone_Script : MonoBehaviour
{
    bool hit = false;
    public GameObject hitZoneRed;
    public GameObject SnakeState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hit == true)
        {
            Destroy(GetComponent<Collider2D>(), 1.5f);
            Destroy(hitZoneRed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        SnakeState.GetComponent<SnakeStateScript>().hitPlus();
    }

}
