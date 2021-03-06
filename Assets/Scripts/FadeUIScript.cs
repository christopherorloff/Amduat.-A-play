using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScrollManager;

public class FadeUIScript : MonoBehaviour
{

    public Text textObject;
    public CanvasGroup FadeSprite;
    public GameObject canvas;
    public bool sceneReady = false;
    public bool sceneEnd = false;

    void Start()
    {
        // print("Start called");
        canvas.SetActive(true);
        FadeSprite = canvas.GetComponent<CanvasGroup>();
        StartCoroutine(FadeSpriteCoroutineDown(0, 4f));
    }

    public void StartFadeOut()
    {
        // print("Start fade out func");
        StartCoroutine(FadeSpriteCoroutineUp(1, 2));
    }

    public IEnumerator FadeSpriteCoroutineDown(float value, float time)
    {
        StartCoroutine(FadeTextCoroutine(1, 1));

        Scroll.LockInput();
        yield return new WaitForSeconds(2.5f);
        float startValue = 1;
        float startTime = Time.time;

        while (FadeSprite.alpha > value)
        {
            float t = (Time.time - startTime) / time;
            FadeSprite.alpha = Mathf.Lerp(startValue, value, t);
            if (Scroll.InputLockState() && t > 0.2f)
            {
                Scroll.UnlockInput();
            }
            yield return null;
        }
    }


    public IEnumerator FadeSpriteCoroutineUp(float value, float time)
    {
        float startValue = 0;
        float startTime = Time.time;

        while (FadeSprite.alpha < value)
        {
            float t = (Time.time - startTime) / time;
            t = Mathf.Clamp(t, 0, 1);
            FadeSprite.alpha = Mathf.Lerp(startValue, value, t);
            yield return null;
        }

        GameManager.Instance.StartChangeToNextScene();
    }




    public IEnumerator FadeTextCoroutine(float value, float time)
    {


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

        sceneReady = true;
    }


}
