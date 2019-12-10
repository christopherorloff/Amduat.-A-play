using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSpawnEffect : MonoBehaviour
{

    public Hour9_blessedDeadLogic BlessedLogic;

    // Start is called before the first frame update
    void Start()
    {
        BlessedLogic = FindObjectOfType<Hour9_blessedDeadLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BlessedLogic.stopEffect == true)
        {
            GetComponent<ParticleSystem>().Stop();
        }
    }
}
