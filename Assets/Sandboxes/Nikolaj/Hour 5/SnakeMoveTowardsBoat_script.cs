using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMoveTowardsBoat_script : MonoBehaviour
{
    public Scenemanager_hour5_script SceneManager;

    public GameObject boat;
    float speed;
    float awaySpeed = 3f;
    float distance = 1f;
    Vector2 velocity;
    private float smoothTime = 2;

    void Start()
    {
        speed = Random.Range(0.3f, 0.8f) + 1;
    }

    void Update()
    {
        transform.right = boat.transform.position - transform.position;
        if (SceneManager.pushSnakesAway)
        {
            Vector3 opposite = -(boat.transform.position - transform.position);
            transform.position = Vector2.SmoothDamp(transform.position, opposite, ref velocity, smoothTime);

        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, boat.transform.position, speed * Time.deltaTime);
        }
        if (Vector3.Distance(boat.transform.position, transform.position) <= 1)
        {
            transform.position = (transform.position - boat.transform.position).normalized * distance + boat.transform.position;

        }
    }
}
