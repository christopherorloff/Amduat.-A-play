using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles_moveTowards_object : MonoBehaviour
{

    public ParticleSystem p;
    public ParticleSystem.Particle[] particles;
    public Transform Target;
    public float affectDistance;
    float sqrDist;
    Transform thisTransform;


    void Start()
    {
        p = GetComponent<ParticleSystem>();
        sqrDist = affectDistance * affectDistance;
    }

    // Update is called once per frame
    void Update()
    {

    
        particles = new ParticleSystem.Particle[p.particleCount];
        p.GetParticles(particles);
        for (int i = 0; i < particles.GetUpperBound(0); i++)
        {
            float ForceToAdd = (particles[i].startLifetime - particles[i].remainingLifetime) * (20 * Vector3.Distance(Target.position, particles[i].position));
            particles[i].position = Vector3.Lerp(particles[i].position, Target.position, Time.deltaTime / 2.0f);
          //  particles[i].position = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime / 2f);

        }
        p.SetParticles(particles, particles.Length);
    }
}
