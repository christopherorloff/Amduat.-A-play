using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class Hour9_blessedDeadLogic : MonoBehaviour
{

    public Transform[] blessedDead;
    public ParticleSystem[] showSpawnsParticle;

    public int currentBlessed = 0;
    public float animationDuration = 1.5f;
    public float yMax = 1;
    private int maxBoatsSpawned = 8;
    public bool running = false;
    public Transform Target;

    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!running && Scroll.scrollValue() > 0 && currentBlessed < blessedDead.Length)
        {
           
            StartCoroutine(RaiseBlessedDead(blessedDead[currentBlessed]));

            
        }
    }


    private IEnumerator RaiseBlessedDead(Transform BlessedGO)
    {
        running = true;


        float offset = BlessedGO.GetComponent<SpriteRenderer>().bounds.max.y;

        Instantiate(particle, new Vector3(BlessedGO.position.x, offset), particle.transform.rotation);
        showSpawnsParticle[currentBlessed].Stop();        
        float startTime = Time.time;
        while (BlessedGO.localPosition.y < yMax)
        {
            float t = (Time.time - startTime) / animationDuration;
            BlessedGO.localPosition = new Vector3(BlessedGO.transform.localPosition.x, Mathf.SmoothStep(BlessedGO.localPosition.x, yMax, t), 0);
            yield return null;
        }

        currentBlessed++;
        if (currentBlessed >= maxBoatsSpawned)
        {
            StartMovingBoats();
        }
        running = false;
        
        

    }

    void StartMovingBoats()
    {
      
        

    }


}
