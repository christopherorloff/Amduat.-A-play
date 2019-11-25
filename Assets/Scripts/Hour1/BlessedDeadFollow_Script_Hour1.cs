using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessedDeadFollow_Script_Hour1 : MonoBehaviour
{
    public Transform boatTarget;
    public GameObject blessedDeadPrefab;
    public int numberOfBlessedDead = 10;

    private Transform[] BDTransforms;
    private Vector3[] BDVelocities;
    private float[] BDXOffset;
    private float[] BDSmoothTimes;
    private SpriteRenderer[] blessedDeadSprites;

    public TimelineManager_Script_Hour1 timeline;

    // Start is called before the first frame update
    void Awake()
    {
        BDTransforms = new Transform[numberOfBlessedDead];
        BDVelocities = new Vector3[numberOfBlessedDead];
        BDSmoothTimes = new float[numberOfBlessedDead];
        BDXOffset = new float[numberOfBlessedDead];
        blessedDeadSprites = new SpriteRenderer[numberOfBlessedDead];

        //Initiate blessed dead
        for (int i = 0; i < numberOfBlessedDead; i++)
        {
            Vector3 spawnOffset = new Vector3(UnityEngine.Random.Range(0.5f, 2.5f), UnityEngine.Random.Range(-1f, 3f), 0);
            BDXOffset[i] = spawnOffset.x;
            GameObject clone = Instantiate(blessedDeadPrefab, boatTarget.position - spawnOffset, Quaternion.identity);
            BDTransforms[i] = clone.transform;
            BDSmoothTimes[i] = UnityEngine.Random.Range(1.0f,1.5f);
            BDVelocities[i] = Vector3.zero;
            blessedDeadSprites[i] = clone.GetComponent<SpriteRenderer>();
            blessedDeadSprites[i].color = new Color(blessedDeadSprites[i].color.r, blessedDeadSprites[i].color.g, blessedDeadSprites[i].color.b, 0);
        }
    }

    void Update()
    {
        UpdatePositionOfBlessedDead();
    }

    private void UpdatePositionOfBlessedDead()
    {
        for (int i = 0; i < numberOfBlessedDead; i++)
        {
            BDTransforms[i].position = Vector3.SmoothDamp(BDTransforms[i].position, new Vector3(boatTarget.position.x - BDXOffset[i], Mathf.Lerp(BDTransforms[i].position.y, boatTarget.transform.position.y, timeline.GetTimeline() - 0.5f), 0), ref BDVelocities[i], BDSmoothTimes[i]);
        }
    }

    public SpriteRenderer[] GetBlessedDeadSprites()
    {
        return blessedDeadSprites;
    }
}
