using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeBehaviour_Script_Hour8 : MonoBehaviour
{
    // public
    public Transform coneTop, coneBottom, sunOrb, followStart;
    public Vector2 queueOffsetDimensions;
    public float smoothTime;

    // private
    private Vector3 orb, mid, top, bottom, topQuarter, bottomQuarter;
    private Vector3[] rayVectors;

    private float min = 0.02f;
    private float max = 0.5f;



    void Start()
    {
        rayVectors = new Vector3[] { top, topQuarter, mid, bottomQuarter, bottom };
        for (int i = 0; i < rayVectors.Length; i++)
        {
            rayVectors[i] = new Vector3();
        }
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
            if (hit.collider != null)
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
