using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class PlantGrowthController_Script : MonoBehaviour
{
    public GameObject branch;
    public GameObject leaf;
    public float branchAngle = 15;
    public int numberOfGenerations = 3;
    [Range(1.0f,3.0f)]
    public float speedMultiplier = 1;
    [Range(0.9f,1.0f)]
    public float minimizationPerGeneration = 1;
    public bool AddRandomization = false;

    void Start()
    {
        // StartCoroutine(InstantiateAndGrow(this.transform, transform.position,0));
        StartCoroutine(StartGrowth());
 
    }

    IEnumerator StartGrowth()
    {
        GameObject clone = Instantiate(branch, transform.position, Quaternion.identity);
        StartCoroutine(GrowNewBranch(clone.transform, branchAngle,0));
        StartCoroutine(GrowNewBranch(clone.transform, -branchAngle,0));

        yield return null;
    }

    IEnumerator GrowNewBranch(Transform parent, float angle, int generation)
    {
        Vector3 branchSpawnPosition = parent.Find("Top").transform.position;
        
        if (AddRandomization)
        {
            float random = Random.Range(0.2f,1.5f);
            angle *= random;
        }

        GameObject clone = Instantiate(branch, branchSpawnPosition, parent.rotation);
        clone.transform.parent = parent;
        clone.transform.Rotate(0,0,angle);
        clone.transform.localScale = minimizationPerGeneration * parent.localScale;

        Vector3 originalScale = clone.transform.localScale;
        clone.transform.localScale = new Vector3 (originalScale.x,0,0);
        float t = 0;

        while (clone.transform.localScale.y < originalScale.y)
        {
            if (Scroll.isScrolling())
            {
                t += (Time.deltaTime * speedMultiplier);
            }
            clone.transform.localScale = new Vector3(clone.transform.localScale.x, Mathf.SmoothStep(0, originalScale.y, t), 0);
            yield return null;
        }

        clone.transform.localScale = originalScale;

        if (generation < numberOfGenerations)
        {
            int thisGeneration = generation + 1;
            StartCoroutine(GrowNewBranch(clone.transform, branchAngle, thisGeneration));
            StartCoroutine(GrowNewBranch(clone.transform, -branchAngle, thisGeneration));
        } else
        {
            if (leaf != null)
            {
                Vector3 leafSpawnPosition = clone.transform.Find("Top").transform.position;
                GameObject leafClone = Instantiate(leaf, leafSpawnPosition, clone.transform.rotation) as GameObject;
                leafClone.transform.parent = clone.transform;
                leafClone.transform.localScale *= minimizationPerGeneration * 0.5f;
            }
        }
        yield return null;
    }
}
