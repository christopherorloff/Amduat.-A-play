using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlesFade : MonoBehaviour
{


    public CanvasGroup background;
    public CanvasGroup tilte1;
    public CanvasGroup tilte2;
    public CanvasGroup tilte3;
    public CanvasGroup BlackBars;

    public void startLogic() {

        StartCoroutine(startFadeTitles());
    }



    public IEnumerator startFadeTitles()
    {

        float startTime = Time.time;

        while (background.alpha < 1)
        {
            float t = (Time.time - startTime) / 3;
            t = Mathf.Clamp(t, 0, 1);
            background.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.7f);

        float blackBarsTime = Time.time;
        while (BlackBars.alpha > 0)
        {
            float t = (Time.time - blackBarsTime) / 3;
            t = Mathf.Clamp(t, 0, 1);
            BlackBars.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        float secondTime = Time.time;

        while (tilte1.alpha < 1)
        {
            float t = (Time.time - secondTime) / 3;
            t = Mathf.Clamp(t, 0, 1);
            tilte1.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        yield return new WaitForSeconds(5f);

        float midTime = Time.time;

        while (tilte1.alpha > 0)
        {
            float t = (Time.time - midTime) / 3;
            t = Mathf.Clamp(t, 0, 1);
            tilte1.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }

        float secondMidTime = Time.time;

        while (tilte2.alpha < 1)
        {
            float t = (Time.time - secondMidTime) / 3;
            t = Mathf.Clamp(t, 0, 1);
            tilte2.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        yield return new WaitForSeconds(5f);

        float endTime = Time.time;
        while (tilte2.alpha > 0)
        {
            float t = (Time.time - endTime) / 3;
            t = Mathf.Clamp(t, 0, 1);
            tilte2.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }


        float secondEndTime = Time.time;

        while (tilte3.alpha < 1)
        {
            float t = (Time.time - secondEndTime) / 3;
            t = Mathf.Clamp(t, 0, 1);
            tilte3.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        yield return new WaitForSeconds(5);

        float lastTime = Time.time;

        while (tilte3.alpha > 0)
        {
            float t = (Time.time - lastTime) / 3;
            t = Mathf.Clamp(t, 0, 1);
            tilte3.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }
    }

}
