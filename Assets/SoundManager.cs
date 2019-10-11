using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ScrollManager;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private void Awake()
    {
        //Checking that only one instance exists
        if (Instance == null) {
            Instance = FindObjectOfType<SoundManager>(); 

            if(Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        } 
    }
}
