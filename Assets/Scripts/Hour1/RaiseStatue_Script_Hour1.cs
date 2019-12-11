using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseStatue_Script_Hour1 : MonoBehaviour
{
    public Transform statue1;
    public Transform statue2;
    public ParticleSystem particle;
    private Vector3 statueOffset1;
    private Vector3 statueOffset2;


    public float durationOfAnimation = 5;
    public float delayBeforeSecondStatue = 4;
    public float yMax = 1;

    public float shakeMagnitude;

    void Start()
    {
        float offset1 = statue1.GetComponentInChildren<SpriteRenderer>().bounds.max.y;
        statueOffset1 = new Vector3(0, offset1);

        float offset2 = statue2.GetComponentInChildren<SpriteRenderer>().bounds.max.y;
        statueOffset2 = new Vector3(0, offset2);
    }

    public void StartRaisingStatues()
    {
        StartCoroutine(RaisingStatue(statue1, -1, statueOffset1));
        StartCoroutine(RaisingStatue(statue2, delayBeforeSecondStatue, statueOffset2));
    }

    private IEnumerator RaisingStatue(Transform statueGO, float delay, Vector3 offset)
    {
        SoundManager.Instance.solarBaboonsAppearInstance.start();
        Instantiate(particle, new Vector3(statueGO.position.x, offset.y), particle.transform.rotation);
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        float startTime = Time.time;
        while (statueGO.localPosition.y < yMax)
        {
            float x = (Random.Range(-1, 1) * shakeMagnitude);
            float t = (Time.time - startTime) / durationOfAnimation;
            statueGO.localPosition = new Vector3(x, Mathf.SmoothStep(0, yMax, t), 0);
            yield return null;
        }

        //Small bounce upon reaching full height?

    }
}
