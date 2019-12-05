using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class Sandbox_Chris_RubberBand_Script : MonoBehaviour
{
    public float timelineScalar = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float input = Scroll.scrollValueAccelerated();

        ConvertInputToProgress(input);
        
    }

    private void ConvertInputToProgress(float input)
    {
        if (input > 0)
        {
            float speed = Scroll.scrollValueAccelerated(0.99999f) * timelineScalar * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 0.001f);
            Debug.Log("good");

        }
        else if(input < 0)
        {
            Debug.Log("wrong");
        }
    }
}
