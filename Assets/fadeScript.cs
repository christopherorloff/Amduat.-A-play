using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeScript : MonoBehaviour
{
    public SpriteRenderer image1;
    public SpriteRenderer image2;
    public SpriteRenderer image3;
    public float colorChangeDuration;

    private float t = 0f;

    private float min1 = 1f;
    private float max1 = 0f;
    private float min2 = 0f;
    private float max2 = 1f;
    /*private float min3 = 100f;
    private float max3 = 255f;*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image1.color = new Color(1,1,1,Mathf.Lerp(min1,max1,t)); 
        image2.color = new Color(1,1,1,Mathf.Lerp(min2,max2,t)); 

        if (t < 1f)
        { 
            t += Time.deltaTime/colorChangeDuration;
            //Debug.Log(t);
        }

        if (t >= 1.0f)
        {
            float temp1 = max1;
            max1 = min1;
            min1 = temp1;

            float temp2 = max2;
            max2 = min2;
            min2 = temp2;

            t = 0.0f;
        }

    }
}
