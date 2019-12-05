using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMoveTowardsBoat_script : MonoBehaviour
{
    public Scenemanager_hour5_script SceneManager;

    public GameObject boat;
    float speed;
    float awaySpeed = 2f;
    float distance = 1f;

    void Start()
    {
        speed = Random.Range(0.3f, 0.8f);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, boat.transform.position, speed * Time.deltaTime);
        transform.right = boat.transform.position - transform.position;
        if (SceneManager.pushSnakesAway)
        {
            transform.position = Vector2.MoveTowards(transform.position, boat.transform.position, -1 * awaySpeed * Time.deltaTime);
        }
        if (Vector3.Distance(boat.transform.position, transform.position) <= 1)
        {
            transform.position = (transform.position - boat.transform.position).normalized * distance + boat.transform.position;

        }
    }
}
