using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMoveTowardsBoat_script : MonoBehaviour
{
    public Scenemanager_hour5_script SceneManager;

    public GameObject boat;
    float speed;
    float awaySpeed = 5f;
    float distance = 1f;
    Vector2 velocity;
    private float smoothTime = 2;

    private bool coroutineRunning = false;

    void Start()
    {
        speed = Random.Range(0.3f, 0.8f) + 1;
    }

    void Update()
    {
        transform.right = boat.transform.position - transform.position;

        if (SceneManager.pushSnakesAway)
        {

            if (!coroutineRunning)
            {
                Vector3 opposite = -(boat.transform.position - transform.position).normalized;
                StartCoroutine(PushSnakes(transform.position, opposite, 2));
            }
        }
        else
        {
            if (Vector3.Distance(boat.transform.position, transform.position) <= 1)
            {
                transform.position = (transform.position - boat.transform.position).normalized * distance + boat.transform.position;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, boat.transform.position, speed * Time.deltaTime);
            }
        }
    }

    IEnumerator PushSnakes(Vector3 start, Vector3 direction, float duration)
    {
        coroutineRunning = true;
        float startTime = Time.time;
        float t = 0;
        while (t < 1)
        {
            t = (Time.time - startTime) / duration;
            t = (1 - t) * (1 - t) * (1 - t);
            t = Mathf.Clamp(t, 0, 1);
            print(t);
            transform.position += direction * awaySpeed * Time.deltaTime;
            yield return null;
        }

        coroutineRunning = false;
    }
}
