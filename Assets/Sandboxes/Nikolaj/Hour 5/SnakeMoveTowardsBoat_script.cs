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
                Debug.DrawRay(transform.position, opposite, Color.yellow, 1000);
                StartCoroutine(PushSnakes(transform.position, opposite, 1.5f));
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
        while (t < 0.99f)
        {
            t = (Time.time - startTime) / duration;

            //easing function: inverse power --> ease out
            t = 1 - (1 - t) * (1 - t);
            t = Mathf.Clamp(t, 0, 1);
            transform.position = Vector3.Lerp(start, (direction * awaySpeed) + start, t);
            yield return null;
        }

        coroutineRunning = false;
    }
}
