using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KnifeSpawner : MonoBehaviour
{
    public GameObject knife;
    public Transform spawnPos;

    float delay = 2;

    void OnEnable()
    {
        EventManager.KnifeHitEvent += SpawnKnife;
    }

    void OnDisable()
    {
        EventManager.KnifeHitEvent -= SpawnKnife;
    }

    public void SpawnKnife()
    {
        StartCoroutine(spawnDelay());
    }

    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(delay);
        GameObject go = Instantiate(knife);
        go.transform.parent = spawnPos.parent;
        go.transform.position = spawnPos.transform.position;
        go.transform.rotation = spawnPos.transform.rotation;
        go.transform.localScale = spawnPos.transform.localScale;

    }

}
