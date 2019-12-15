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
    [Range(1.0f, 7.0f)]
    public float speedMultiplier = 1;
    [Range(0.7f, 1.0f)]
    public float minimizationPerGeneration = 1;
    public bool AddRandomization = false;
    public bool spawnLeafs = false;
    public Color branchColor;
    // public int spriteLayer = -1;
    private SpriteRenderer branchSprite;

    void OnEnable()
    {

        branchSprite = branch.GetComponentInChildren<SpriteRenderer>();
        branchSprite.color = branchColor;

        // if (spriteLayer != -1)
        //     branchSprite.sortingOrder = spriteLayer;

        StartCoroutine(StartGrowth());
    }


    IEnumerator StartGrowth()
    {
        GameObject clone = Instantiate(branch, transform.position, transform.rotation);
        clone.transform.parent = this.transform;
        clone.transform.localScale = Vector3.one;


        //Save original size for later
        Vector3 originalScale = clone.transform.localScale;

        //Make clone invisible to prepare for "growing"
        clone.transform.localScale = new Vector3(originalScale.x, 0, 0);
        float t = 0;

        while (clone.transform.localScale.y < originalScale.y)
        {
            if (Scroll.scrollValue() < 0)
            {
                t += (Time.deltaTime * speedMultiplier);
            }
            clone.transform.localScale = new Vector3(clone.transform.localScale.x, Mathf.SmoothStep(0, originalScale.y, t), 0);
            yield return null;
        }


        StartCoroutine(GrowNewBranch(clone.transform, branchAngle, 0));
        StartCoroutine(GrowNewBranch(clone.transform, -branchAngle, 0));
    }

    IEnumerator GrowNewBranch(Transform parent, float angle, int generation)
    {
        //Position of the top of the gameobject, aka where we spawn the next set
        Vector3 branchSpawnPosition = parent.Find("Top").transform.position;

        //Add randomaization to angle if so chosen
        angle = (AddRandomization) ? angle * Random.Range(0.2f, 1.5f) : angle;

        //Instantiation and practical matters related to it
        GameObject clone = Instantiate(branch, branchSpawnPosition, parent.rotation);
        clone.transform.parent = parent;
        clone.transform.localScale = Vector3.one * minimizationPerGeneration;

        clone.transform.Rotate(0, 0, angle);

        //Save original size for later
        Vector3 originalScale = clone.transform.localScale;

        //Make clone invisible to prepare for "growing"
        clone.transform.localScale = new Vector3(originalScale.x, 0, 0);
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

        //Make sure the scale is back to exactly the original size
        clone.transform.localScale = originalScale;

        //To avoid infinite loop
        if (generation < numberOfGenerations)
        {
            int thisGeneration = generation + 1;
            StartCoroutine(GrowNewBranch(clone.transform, branchAngle, thisGeneration));
            StartCoroutine(GrowNewBranch(clone.transform, -branchAngle, thisGeneration));
        }
        else // only put leaf on last branch and only if a leaf prefab is chosen
        {
            if (leaf != null && spawnLeafs)
            {
                Vector3 leafSpawnPosition = clone.transform.Find("Top").transform.position;
                GameObject leafClone = Instantiate(leaf, leafSpawnPosition, clone.transform.rotation) as GameObject;
                leafClone.transform.parent = clone.transform;
                leafClone.transform.localScale = Vector3.one * minimizationPerGeneration;
                leafClone.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Random.ColorHSV(0.2f, 0.3f, 0.8f, 0.9f, 0.6f, 0.7f);
            }
        }
    }
}
