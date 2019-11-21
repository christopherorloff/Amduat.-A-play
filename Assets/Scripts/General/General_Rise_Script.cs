using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General_Rise_Script : MonoBehaviour
{
    public bool riseStart = false;
    public bool rising = false;
    public GameObject riseObject;
    public float riseDuration;
    public float riseDistance;
    public float riseDelay;
    public bool shake = false;
    public float shakeMagnitude;

    private float initialStart;
    private float initialEnd;
    private float initialX;
    private float t = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        initialStart = riseObject.transform.position.y;
        initialEnd = initialStart + riseDistance;
        initialX = riseObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(riseStart == true)
        {
            StartCoroutine(startRiseTimer(riseDelay));
            rising = true;
        }
    }

    private IEnumerator startRiseTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(shake && rising)
        {
            float x = (Random.Range(-1, 1) * shakeMagnitude) + initialX;
            riseObject.transform.position = new Vector3(x,Mathf.Lerp(initialStart, initialEnd, t) , transform.position.z);
        }
        else if (rising)
        {
            riseObject.transform.position = new Vector3(transform.position.x,Mathf.Lerp(initialStart, initialEnd, t) , transform.position.z);
        }

        if (t < 1f)
        { 
            t += Time.deltaTime/riseDuration;
            Debug.Log(t);
        }
        else
        {
            Debug.Log("stop");
            shake = false;
        }
    }
}
