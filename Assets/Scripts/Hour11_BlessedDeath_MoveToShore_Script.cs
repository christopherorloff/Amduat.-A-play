using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hour11_BlessedDeath_MoveToShore_Script : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool going;
    public bool shoreHit;
    public Sprite onShoreSprite;

    public Transform boatTarget;
    private Vector3 BDVelocities;
    private float BDXOffset;
    private float BDSmoothTimes;
    public float speed = -0.2f;

    public TimelineManager_Script_Hour11 timeline;
    public float myTimeLine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        BDXOffset = Vector3.Distance(boatTarget.position, transform.position) - UnityEngine.Random.Range(0.2f, 0.4f);
        BDSmoothTimes = UnityEngine.Random.Range(1.0f, 1.5f);
        BDVelocities = Vector3.zero;
    }

    void Update()
    {
        myTimeLine = timeline.GetTimeline();
        moveTowardShore();
    }

    public void moveToShore()
    {
        
        going = true;
    }

    private void moveTowardShore()
    {
        if(going)
        {
            transform.parent = null;
            transform.position += new Vector3(0,  Time.deltaTime*speed, 0);
        }
        else if (!shoreHit)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(boatTarget.position.x - BDXOffset, Mathf.Lerp(transform.position.y, boatTarget.transform.position.y - 0.4f, myTimeLine - 0.5f), 0), ref BDVelocities, BDSmoothTimes);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("HIT");
        if (col.gameObject.tag == "Shore")
        {
                Debug.Log("HIT");
            //If the GameObject has the same tag as specified, output this message in the console
                spriteRenderer.color = new Color(1f,1f,1f,0.5f);
                shoreHit = true;
                going = false;
        }
    }
}
