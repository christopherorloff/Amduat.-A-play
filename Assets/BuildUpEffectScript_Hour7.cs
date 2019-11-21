using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class BuildUpEffectScript_Hour7 : MonoBehaviour
{

    ParticleSystem PS;
    ParticleSystem.EmissionModule emmision;
    public float energy = 0;

    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<ParticleSystem>();
        emmision = PS.emission;
    }

    // Update is called once per frame
    void Update()
    {

        if (ScrollManager.Scroll.scrollValueAccelerated() < 0)
        {
            energy += 0.05f;
        }
        ParticleSystem.MinMaxCurve tempCurve = emmision.rateOverTime;
        tempCurve.constant = energy;
        emmision.rateOverTime = tempCurve;

       
    }
}
