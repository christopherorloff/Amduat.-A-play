using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMoveTowardsBoat_script : MonoBehaviour
{

    public GameObject Boat;
    float speed;

    void Start()
    {
        speed = Random.Range(0.3f, 0.8f);
        Boat = GameObject.FindGameObjectWithTag("Boat");
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Boat.transform.position, speed * Time.deltaTime);
    }
}
