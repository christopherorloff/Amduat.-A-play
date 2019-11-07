using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTreeGrowth_Script_Hour2 : MonoBehaviour
{
    PlantGrowthController_Script tree;
    void Start()
    {
        tree = GetComponent<PlantGrowthController_Script>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        tree.enabled = true;
    }
}
