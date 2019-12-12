using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollowScript_Hour12 : MonoBehaviour
{

    public Transform target;
    public int numberOfBlessedDead = 9;
    Vector3[] velocities;
    public Transform[] BDTransforms;
    private Vector3[] offsets;
    private float[] smoothTime;
    public Hour12_SceneManager timeline;



    // Start is called before the first frame update
    void Start()
    {
        
        velocities = new Vector3[numberOfBlessedDead];
        offsets = new Vector3[numberOfBlessedDead];
        smoothTime = new float[numberOfBlessedDead];


        for (int i = 0; i < numberOfBlessedDead; i++)
        {
            offsets[i] = BDTransforms[i].transform.position - target.transform.position;
            smoothTime[i] = UnityEngine.Random.Range(1.0f, 1.5f);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numberOfBlessedDead; i++)
        {
            BDTransforms[i].position = Vector3.SmoothDamp(BDTransforms[i].position, (target.position + offsets[i]), ref velocities[i], smoothTime[i]);
        } 
    }
}
