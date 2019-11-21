using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTreeGrowth_Script_Hour2 : MonoBehaviour
{
    PlantGrowthController_Script tree;
    public float timelinePosition;
    public ParticleSystem growEffect;
    void Start()
    {
        tree = GetComponent<PlantGrowthController_Script>();
    }
    public void EnableTree()
    {
        tree.enabled = true;
        ParticleSystem clone = Instantiate(growEffect, this.transform.position, Quaternion.identity);
        //Potentielt destroy
        clone.Play();
        print(this.gameObject.name + " enabled");
    }

    public float GetTimelinePosition()
    {
        return timelinePosition;
    }


}
