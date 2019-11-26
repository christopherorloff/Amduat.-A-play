using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hour11_BlessedDeath_MoveToShore_Script : MonoBehaviour
{
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    private SpriteRenderer spriteRenderer;
    public Transform target;
    public Transform starter;
    public Transform boat;
    public bool going;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveTowardShore();
    }

    public void moveToShore()
    {
        print("BUBBER!!");
        starter.position = transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(starter.position, target.position);
        going = true;
    }

    private void moveTowardShore()
    {
        if(going)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(starter.position, target.position, fractionOfJourney);
            print(fractionOfJourney);
            if(fractionOfJourney >= 1f)
            {
                spriteRenderer.color = new Color(1f,1f,1f,0.5f);
            }
        }
    }
}
