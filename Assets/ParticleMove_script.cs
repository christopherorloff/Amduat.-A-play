using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class ParticleMove_script : MonoBehaviour
{

    public ParticleSystem p;
    public ParticleSystem.Particle[] particles;
    public Transform Target;
    public float affectDistance;
    float sqrDist;
    Transform thisTransform;
    float speed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<ParticleSystem>();
        sqrDist = affectDistance * affectDistance;
    }

    // Update is called once per frame
    void Update()
    {

        if (Scroll.scrollValueAccelerated() > 0)
        {
            particles = new ParticleSystem.Particle[p.particleCount];
            p.GetParticles(particles);
            for (int i = 0; i < particles.GetUpperBound(0); i++)
            {
                float ForceToAdd = (particles[i].startLifetime - particles[i].remainingLifetime) * (10 * Vector3.Distance(Target.position, particles[i].position));
                particles[i].position = Vector3.Lerp(particles[i].position, Target.position, Time.deltaTime / 2.0f);


            }
            p.SetParticles(particles, particles.Length);

        }
        if (Scroll.scrollValueAccelerated() < 0)
        {
            print("scroller anden vej");
            particles = new ParticleSystem.Particle[p.particleCount];
            p.GetParticles(particles);
            for (int i = 0; i < particles.GetUpperBound(0); i++)
            {
                Vector3 directionAwayFromTarget = (particles[i].position - Target.position).normalized;
                particles[i].position += directionAwayFromTarget * speed * Time.deltaTime;
                //float ForceToAdd = (particles[i].startLifetime - particles[i].remainingLifetime) * (10 * Vector3.Distance(Target.position, particles[i].position));
                //particles[i].position = Vector3.Lerp(particles[i].position, Target.position, Time.deltaTime / 2.0f);

            }
            p.SetParticles(particles, particles.Length);

        }





    }
    }

