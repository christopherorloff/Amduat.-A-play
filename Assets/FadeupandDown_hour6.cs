using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeupandDown_hour6 : MonoBehaviour
{
    public SpriteRenderer Sprite;

    // Start is called before the first frame update
    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(pulseEffect(0.6f, 1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator pulseEffect(float value, float time)
    {
        float startValue = 0;
        float startTime = Time.time;
        while (Sprite.color.a < value)
        {
            float t = (Time.time - startTime) / time;
            Color newColor = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, Mathf.Lerp(startValue, value, t));
            Sprite.color = newColor;
            yield return null;
        }

        float midTime = Time.time;
        while (Sprite.color.a > 0)
        {
            float t = (Time.time - midTime) / time;
            Color newColor = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, Mathf.Lerp(value, startValue, t));
            Sprite.color = newColor;
            yield return null;
        }

        StartCoroutine(pulseEffect(0.6f, 1.5f));
    }


}
