using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseLight_Script : MonoBehaviour
{

    public SpriteRenderer Sprite;

    public bool StartPulse= false;
    public bool running = false;

    void Start()
    {

        Sprite = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        if (StartPulse && !running)
        {
            print("startpulse");
            StartCoroutine(FadeUp(0.2f, 0.2f));
        }

    }

    public IEnumerator FadeUp(float value, float time)
    {
            float startValue = 0;
            float startTime = Time.time;
            running = true;
            while (Sprite.color.a < value)
            {
                float t = (Time.time - startTime) / time;
                Color newColor = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, Mathf.Lerp(startValue, value, t));
                Sprite.color = newColor;
                yield return null;
            }

        float midtime = Time.time;

        while (Sprite.color.a > 0)
        {
            float t = (Time.time - midtime) / time;
            Color newColor = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, Mathf.Lerp(value, startValue, t));
            Sprite.color = newColor;
            StartPulse = false;
            yield return null;

        }
        yield return new WaitForSeconds(1.5f);
        running = false;



    }
}
