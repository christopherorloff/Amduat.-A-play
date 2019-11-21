using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour
{
    public ParticleSystem BoatParticles;

    
    public TowBoatLogic TowScript;
    float moveSpeed = 1f;

    void Start()
    {
        TowScript = FindObjectOfType<TowBoatLogic>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (TowScript.pulling)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);

        }


        if (TowScript.startEffect)
        {
            StartCoroutine(BoatEffect());
        }
    }





    IEnumerator BoatEffect()
    {
        TowScript.startEffect = false;
        print("startparticle");
        BoatParticles.Play();
        yield return new WaitForSeconds(1.5f);
        BoatParticles.Stop();

    }
}
