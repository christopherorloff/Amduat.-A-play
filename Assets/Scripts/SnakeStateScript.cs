using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeStateScript : MonoBehaviour
{
    Vector3 initialPosition;
    Vector3 secondaryPosition;
    float yMax;
    float yMin;

    float startTime;
    public float duration = 3.0f;

    int knifeHits = 0;
    public int knifeHitLimit = 6;

    BgMovement bgMovement;

    void Awake()
    {
        initialPosition = this.transform.position;
        secondaryPosition = new Vector3(initialPosition.x, -8, 0);
        yMax = initialPosition.y;
        yMin = secondaryPosition.y;
        bgMovement = GetComponent<BgMovement>();
        bgMovement.enabled = false;
        StartCoroutine(SnakeIntro());
    }



    IEnumerator SnakeIntro()
    {
        startTime = Time.time;
        transform.position = secondaryPosition;
        print((Time.time - startTime));
        while ((Time.time - startTime) <= duration)
        {
            print((Time.time - startTime));
            float t = (Time.time - startTime) / duration;
            transform.position = new Vector3(initialPosition.x, Mathf.SmoothStep(yMin, yMax, t), 0);
            yield return null;
        }
        print("Fade in snake done.");
        bgMovement.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Knife"))
        {
            knifeHits++;
            if(knifeHits >= knifeHitLimit)
            {
                print("den ær døj");
            }
        }
    }
}
