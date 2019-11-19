using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseStatue_Script_Hour1 : MonoBehaviour
{
    public Transform statue1;
    public Transform statue2;

    public float durationOfAnimation = 5;
    public float delayBeforeSecondStatue = 4;
    public float yMax = 1;

    public void StartRaisingStatues()
    {
        StartCoroutine(RaisingStatue(statue1, -1));
        StartCoroutine(RaisingStatue(statue2, delayBeforeSecondStatue));
    }

    private IEnumerator RaisingStatue(Transform statueGO, float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        float startTime = Time.time;
        while (statueGO.localPosition.y < yMax)
        {
            float t = (Time.time - startTime) / durationOfAnimation;
            statueGO.localPosition = new Vector3(0, Mathf.SmoothStep(0, yMax, t), 0);
            yield return null;
        }

        //Small bounce upon reaching full height?

    }
}
