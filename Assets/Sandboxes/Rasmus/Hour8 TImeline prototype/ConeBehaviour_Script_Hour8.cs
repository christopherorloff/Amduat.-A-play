using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class ConeBehaviour_Script_Hour8 : MonoBehaviour
{
    // public
    public Transform coneTop, coneBottom, sunOrb, followStart;
    public Vector2 queueOffsetDimensions;
    public float smoothTime;


    public float inputStep = 0.2f;
    public float inputDownStep = 0.1f;
    // private
    private Vector3 orb, mid, top, bottom, topQuarter, bottomQuarter;
    private Vector3[] rayVectors;

    private float min = 0.02f;
    private float max = 0.5f;

    private Vector3 minConeScale;
    private Vector3 maxConeScale;

    private float input;
    private float velocity;
    private float coneLimit = 0.5f;

    void Start()
    {
        rayVectors = new Vector3[] { top, topQuarter, mid, bottomQuarter, bottom };
        for (int i = 0; i < rayVectors.Length; i++)
        {
            rayVectors[i] = new Vector3();
        }
        minConeScale = new Vector3(transform.localScale.x, min, transform.localScale.z);
        maxConeScale = new Vector3(transform.localScale.x, max, transform.localScale.z);

        transform.localScale = minConeScale;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        input = Scroll.scrollValue();

        if (input > 0)
        {
            velocity += inputStep * Time.deltaTime;
        }
        else
        {
            velocity -= inputDownStep * Time.deltaTime;
        }

        velocity = Mathf.Clamp(velocity, 0, 1);
        transform.localScale = Vector3.Lerp(minConeScale, maxConeScale, velocity);
    }

    void FixedUpdate()
    {
        // Vectors
        UpdateVectorPositions();
        // Drawing rays to test
        DrawRays();
        // Ray the casting
        RayCasting();
    }

    private void RayCasting()
    {
        for (int i = 0; i < rayVectors.Length; i++)
        {
            float distance = Vector3.Distance(orb, rayVectors[i]);
            RaycastHit2D hit = Physics2D.Raycast(orb, (rayVectors[i] - orb).normalized, distance);
            if (hit.collider != null && velocity > coneLimit && hit.collider.tag == "BlessedDead")
            {
                SetFollow(hit.transform.gameObject);
                hit.collider.enabled = false;
            }
        }
    }

    private void SetFollow(GameObject go)
    {
        go.GetComponent<BlessedDeadBehaviour_Script_Hour8>().StartFollowing(followStart, queueOffsetDimensions, smoothTime);
    }

    private void DrawRays()
    {
        Debug.DrawRay(orb, top - orb, Color.green);
        Debug.DrawRay(orb, topQuarter - orb, Color.yellow);
        Debug.DrawRay(orb, mid - orb, Color.cyan);
        Debug.DrawRay(orb, bottomQuarter - orb, Color.magenta);
        Debug.DrawRay(orb, bottom - orb, Color.red);
    }

    private void UpdateVectorPositions()
    {
        // GameObjects
        orb = sunOrb.position;
        top = coneTop.position;
        bottom = coneBottom.position;

        // Compound vectors
        mid = ((top + bottom) * 0.5f);
        topQuarter = ((top + mid) * 0.5f);
        bottomQuarter = ((bottom + mid) * 0.5f);

        rayVectors[0] = top;
        rayVectors[1] = topQuarter;
        rayVectors[2] = mid;
        rayVectors[3] = bottomQuarter;
        rayVectors[4] = bottom;
    }
}
