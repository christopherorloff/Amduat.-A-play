using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hour1_particleLogic_Script : MonoBehaviour
{

    public ParticleSystem p;
    public ParticleSystem.Particle[] particles;
    public Transform Target;
    float sqrDist;
    public float affectDistance;
    Transform thisTrans;
    float speed = 0.5f;
    public bool sceneDone = false;


    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<ParticleSystem>();
        sqrDist = affectDistance * affectDistance;
    }

    // Update is called once per frame
    void Update()
    {

        if (sceneDone)
        {
            speed = 5f;
            p.Stop();
        }
            particles = new ParticleSystem.Particle[p.particleCount];
            p.GetParticles(particles);
            for (int i = 0; i < particles.GetUpperBound(0); i++)
            {
                particles[i].position = Vector3.MoveTowards(particles[i].position, Target.position, speed * Time.deltaTime);
            }
        
       
        p.SetParticles(particles, particles.Length);


    }
}
