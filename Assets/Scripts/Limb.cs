using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    //TARGET VARIABLES
    public Transform target;
    public float smoothTime = 0.8f;
    private float origitalSmoothTime;
    private Vector3 velocity = Vector3.zero;

    public bool isMoving;
    public bool isActive;
    public bool isDone;

    private Shake shake;
    private float shakeValue;
    private float increaseSpeed = 0.01f;

    private Wiggle wiggle;

    public GameObject snapParticle;
    private bool particleRunning;
    private LimbMovement limbMovement;

    public GameObject directionParticle;
    private Transform directionParticlePosition;
    private bool directionParticleRunning;
    private GameObject particle;

    private void Start()
    {
        shake = GetComponentInParent<Shake>();
        wiggle = GetComponentInParent<Wiggle>();

        origitalSmoothTime = smoothTime - Random.Range(smoothTime, smoothTime/2);

        limbMovement = FindObjectOfType<LimbMovement>();
    }

    private void Update()
    {
        if (isMoving && transform.position != target.position) {
            //Move limb to correct position of target
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);

            //If shake script is on parent, then increase shake
            if (shake != null)
            {
                shakeValue += Time.deltaTime * increaseSpeed;
                if (shakeValue > 0.025f) shakeValue = 0.025f;
                shake.magnitude = shakeValue;
            }

            //Snap (i.e. make smooth time 0) when close to target
            if (!isDone) {
                float dist = Vector3.Distance(transform.position, target.position);
                if (dist <= 0.05f)
                {
                    smoothTime = 0;
                    isDone = true;
                    //isMoving = false;

                    if (!particleRunning)
                    {
                        Instantiate(snapParticle, transform.position, Quaternion.identity);

                        limbMovement.LimbDone();
                        
                        if(limbMovement.scrolledCounter >= limbMovement.limbs.Length) {
                            FMOD.Studio.EventInstance collectedSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR 3/OsirisLimbCollectedFinal");
                            collectedSoundInstance.start();
                        }
                        else {
                            FMOD.Studio.EventInstance collectedSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR 3/OsirisLimbCollected");
                            collectedSoundInstance.start();
                        }

                        particleRunning = true;
                    }
                }
            } else if (isDone && isMoving) {
                smoothTime = origitalSmoothTime;
            }

            //print("DISTANCE TO TARGET IS : " + dist);
        }


        if (isActive) {
            if(wiggle != null) wiggle.enabled = true;
            if (shake != null) shake.enabled = false;
            if (!directionParticleRunning) {
                directionParticlePosition = transform.Find("ParticleTarget");
                particle = Instantiate(directionParticle, directionParticlePosition.position, Quaternion.identity);
                if(limbMovement.scrolledCounter % 2 == 0) {
                    particle.GetComponent<Particle_Change_Direction_Script>().DirectionDown = true;
                } else if (limbMovement.scrolledCounter % 2 == 1) {
                    particle.GetComponent<Particle_Change_Direction_Script>().DirectionDown = false;
                }
                particle.transform.parent = transform;
                directionParticleRunning = true;
            }
        } else {
            if (directionParticleRunning) {
                particle.GetComponent<ParticleSystem>().Stop();
            }
            if (wiggle != null) wiggle.enabled = false;
            if (shake != null) shake.enabled = true;
        }
    }
}
