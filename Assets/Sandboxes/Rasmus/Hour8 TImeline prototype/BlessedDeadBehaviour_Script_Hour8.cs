using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessedDeadBehaviour_Script_Hour8 : MonoBehaviour
{
    //Behaviour
    private Vector3 velocity;

    //Screen stats

    Vector2 screenSizeMax = new Vector2(Screen.width, Screen.height);
    Vector2 screenSizeMin = Vector2.zero;

    private Vector2 screenBoundariesMax;
    private Vector2 screenBoundariesMin;
    private Vector2 screenWorldSize;

    //Interface --> move to parent object
    public float clampedYPosition = 1 / 3;
    public float speed = 1;

    void Start()
    {

        //Setup for constant variables
        screenBoundariesMax = (Vector2)Camera.main.ScreenToWorldPoint((Vector3)screenSizeMax);
        screenBoundariesMin = (Vector2)Camera.main.ScreenToWorldPoint((Vector3)screenSizeMin);
        screenWorldSize.x = screenBoundariesMax.x - screenBoundariesMin.x;
        screenWorldSize.y = screenBoundariesMax.y - screenBoundariesMin.y;
        Init();
    }

    void Update()
    {
        Move();
        CheckForBoundaries();
    }

    private void Move()
    {
        transform.position += velocity * speed * Time.deltaTime;
    }

    private void Move(Vector3 input)
    {
        transform.position += input * speed * Time.deltaTime;
    }

    private void Init()
    {
        velocity = Vector3.left;
        transform.position = new Vector3(screenBoundariesMax.x + UnityEngine.Random.Range(0.0f, 2.0f), GetRandomYPosition(), 0);
    }

    private void CheckForBoundaries()
    {

        if (transform.position.x < screenBoundariesMin.x)
        {
            transform.position = new Vector3(screenBoundariesMax.x, GetRandomYPosition(), 0);
        }
    }


    private float GetRandomYPosition()
    {
        float clamp = screenWorldSize.y / 2 * clampedYPosition;
        float y = UnityEngine.Random.Range(-clamp, clamp);
        return y;
    }
}
