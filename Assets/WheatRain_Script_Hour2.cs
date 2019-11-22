using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatRain_Script_Hour2 : MonoBehaviour
{

    private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    float delayBeforeSpray = 2;

    void Start()
    {
        GetComponentsInChildren<ParticleSystem>(true, particleSystems);
        foreach (var item in particleSystems)
        {
            item.Stop();
        }
    }

    public void StartSpraying() { StartCoroutine(startSprayWithDelay()); }

    IEnumerator startSprayWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeSpray);
        foreach (var item in particleSystems)
        {
            item.Play();
        }

    }
}
