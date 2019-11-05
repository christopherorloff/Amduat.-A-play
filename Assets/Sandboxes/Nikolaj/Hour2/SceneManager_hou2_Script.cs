using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class SceneManager_hou2_Script : MonoBehaviour
{

    public Transform LeafSpawnPoint;
    public GameObject Leaf;
    

    void Start()
    {
        
    }

    void Update()
    {
        float input = Scroll.scrollValueAccelerated();

        if (input < 0)
        {
            Instantiate(Leaf, LeafSpawnPoint.position, Quaternion.identity);
        }
        
    }
}
