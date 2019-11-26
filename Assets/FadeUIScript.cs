using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUIScript : MonoBehaviour
{

    public Text textObject;
    public CanvasGroup FadeSprite;
    public GameObject canvas;

    public bool sceneEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(true);
        FadeSprite = canvas.GetComponent<CanvasGroup>();
        StartCoroutine(FadeSpriteCoroutineDown(0, 4f));
    }

    // Update is called once per frame
    public void StartFadeOut()
    {
        StartCoroutine(FadeSpriteCoroutineUp(1, 2));
    }

    public IEnumerator FadeSpriteCoroutineDown(float value, float time)
    {

        yield return new WaitForSeconds(1.5f);
        float startValue = 1;
        float startTime = Time.time;


        while (FadeSprite.alpha > value)
        {
            float t = (Time.time - startTime) / time;
            FadeSprite.alpha = Mathf.Lerp(startValue, value, t);
            yield return null;


        }
        StartCoroutine(FadeTextCoroutine(1, 1));


    }


    public IEnumerator FadeSpriteCoroutineUp(float value, float time)
    {
        float startValue = 0;
        float startTime = Time.time;

        while (FadeSprite.alpha < value)
        {
            float t = (Time.time - startTime) / time;
            print("Coroutine while, t: " + t);
            t = Mathf.Clamp(t, 0, 1);
            FadeSprite.alpha = Mathf.Lerp(startValue, value, t);
            yield return null;
        }
    }




    public IEnumerator FadeTextCoroutine(float value, float time)
    {


        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.titleSoundInstance.start();
        float startValue = 0;
        float startTime = Time.time;


        while (textObject.color.a < value)
        {

            float t = (Time.time - startTime) / time;
            Color newColor = new Color(textObject.color.r, textObject.color.g, textObject.color.b, Mathf.Lerp(startValue, value, t));
            textObject.color = newColor;
            yield return null;


        }
        yield return new WaitForSeconds(3f);
        float midtime = Time.time;

        while (textObject.color.a > 0)
        {
            float t = (Time.time - midtime) / time;
            Color newColor = new Color(textObject.color.r, textObject.color.g, textObject.color.b, Mathf.Lerp(value, startValue, t));
            textObject.color = newColor;
            yield return null;
        }
    }


}
