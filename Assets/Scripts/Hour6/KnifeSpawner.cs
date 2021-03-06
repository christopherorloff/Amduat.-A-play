using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KnifeSpawner : MonoBehaviour
{
    public GameObject knife;
    public Transform spawnPos;
    public SparkSpawn SS;
    public GameObject KnifeSpawnEffect;

    float delay = 3f;


    private void Start()
    {
        SS = FindObjectOfType<SparkSpawn>();
    }

    void OnEnable()
    {
        EventManager.knifeHitEvent += SpawnKnife;
    }

    void OnDisable()
    {
        EventManager.knifeHitEvent -= SpawnKnife;
    }

    public void SpawnKnife()
    {
        StartCoroutine(spawnDelay());
        SoundManager.Instance.knifeSpawnInstance.start();

    }

    IEnumerator spawnDelay()
    {
        //sparkeffect bool
        SS.NewKnife = true;
        Instantiate(KnifeSpawnEffect, spawnPos.transform.position, KnifeSpawnEffect.transform.rotation);

        yield return new WaitForSeconds(delay);
        GameObject go = Instantiate(knife);
        go.transform.parent = spawnPos.parent;
        go.transform.position = spawnPos.transform.position;
        go.transform.rotation = spawnPos.transform.rotation;
        go.transform.localScale = spawnPos.transform.localScale;
        Destroy(GameObject.Find("Hour6_KnifeSpawnParticle(Clone)"), 6f);

    }

}
