using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMoveTowardsBoat_script : MonoBehaviour
{
    public Scenemanager_hour5_script SceneManager;

    public GameObject Boat;
    float speed;
    float awaySpeed = 2f;
    float distance = 1f;

    void Start()
    {
        speed = Random.Range(0.3f, 0.8f);
        Boat = GameObject.FindGameObjectWithTag("Boat");
        SceneManager = FindObjectOfType<Scenemanager_hour5_script>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Boat.transform.position, speed * Time.deltaTime);

        if (SceneManager.pushSnakesAway)
        {
            transform.position = Vector2.MoveTowards(transform.position, Boat.transform.position, -1 * awaySpeed * Time.deltaTime);
        }
        if (Vector3.Distance(Boat.transform.position, transform.position) <=1)
        {
            transform.position = (transform.position - Boat.transform.position).normalized * distance + Boat.transform.position;

        }
    }
}
