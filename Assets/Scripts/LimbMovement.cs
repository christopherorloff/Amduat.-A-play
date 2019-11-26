using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class LimbMovement : MonoBehaviour
{
    //SCROLL VARIABLES
    bool lastDirectionUp = true;
    public int scrolledCounter;

    //LIMB VARIABLES
    public GameObject[] limbs;
    private int limbsDone;

    public float limbSpeed = 1;

    //ROTATION VARIABLES
    public Vector3[] rotationTargets;
    public float rotationSpeed = 0.3F;
    public float scrollThreshold = 0.8f;
    private Vector3 velocity = Vector3.zero;

    //END VARIABLES
    private bool isDone;
    private float endSpeed = 0.1f;
    public GameObject torso;
    private Vector3 torsoVelocity;
    private float torsoEndSpeed = 3f;

    public GameObject osirisCollectedGameObject;
    private Vector3 collectedOsirisGameObjectVelocity;
    private float collectedOsirisGameObjectSpeed = 300f;
    private float countdownToDestroy;

    public GameObject osirisCollectedParticleSystem;
    private bool osirisCollectedParticleSystemRunning;

    public PulseLight_Script PulseRed;
    public PulseGreenLight_script pulseGreen;

    public Sprite greenTorso;


    private void Start()
    {
        //Setting first limb from limbs array to be the active limb
        limbs[scrolledCounter].GetComponent<Limb>().isActive = true;
        for(int i = 0; i < limbs.Length; i++) {
            limbs[i].GetComponent<Limb>().smoothTime = limbSpeed;
        }
    }

    void Update()
    {
        CheckIfMoving();
        WrongDirection();

        //Rotate entire object
        float step = rotationSpeed * Time.deltaTime;

        if(scrolledCounter > 0 && scrolledCounter < rotationTargets.Length) {
            Quaternion rotation = Quaternion.Euler(rotationTargets[scrolledCounter]);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, step);
        }

        //Initiate final animation of Osiris rising, when all limbs collected
        //Instantiating particle system
        if (isDone) {
            torso.GetComponent<SpriteRenderer>().sprite = greenTorso;

            Transform[] targetTransforms = new Transform[limbs.Length];
            float moveStep = endSpeed * Time.deltaTime;

            //Moving individual limbs
            for(int i = 0; i < limbs.Length; i++) {
                targetTransforms[i] = limbs[i].GetComponent<Limb>().target.transform;
                targetTransforms[i].position = Vector3.MoveTowards(targetTransforms[i].position, new Vector3(0,0,0), moveStep);
            }

            //Moving entire prefab
            torso.transform.position = Vector3.SmoothDamp(torso.transform.position, new Vector3(0, 10, 0), ref torsoVelocity, torsoEndSpeed);
            osirisCollectedGameObject.transform.position = Vector3.SmoothDamp(osirisCollectedGameObject.transform.position, new Vector3(0, -2, 0), ref collectedOsirisGameObjectVelocity, collectedOsirisGameObjectSpeed * Time.deltaTime);
            countdownToDestroy += Time.deltaTime;

            //Instantiating particle system
            if (!osirisCollectedParticleSystemRunning) {
                Instantiate(osirisCollectedParticleSystem, new Vector3(0, 0, 0), Quaternion.identity);
                osirisCollectedParticleSystemRunning = true;
            }

            //Hiding limbs when out of screen
            if (countdownToDestroy >= collectedOsirisGameObjectSpeed) {
                for(int i = 0; i < limbs.Length; i++) {
                    limbs[i].SetActive(false);
                }
            }
        }
    }

    void Move()
    {
        scrolledCounter++;
        if(scrolledCounter - 1 < limbs.Length) {
            //Setting the correct limb to move
            limbs[scrolledCounter - 1].GetComponent<Limb>().isMoving = true;

            FMOD.Studio.EventInstance sweepSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR 3/OsirisLimbSweep");
            sweepSoundInstance.start();

            //If there is another limb, it is set to become active here
            if (scrolledCounter < limbs.Length) {
                print("SETTING LIMB ACTIVE");
                limbs[scrolledCounter].GetComponent<Limb>().isActive = true;
            }
        }
    }


    void WrongDirection()
    {

        float input = Scroll.scrollValue();
        if (lastDirectionUp) 
        {
            if (input > scrollThreshold && !PulseRed.running)
            {
                PulseRed.StartPulse = true;
            }
        }
        else if (!lastDirectionUp)
        {
            if (input < -scrollThreshold && !PulseRed.running)
            {
                PulseRed.StartPulse = true;
            }
        }


    }

    //Checking if last direction is opposite of the previous one - if it is, then a limb will move
    void CheckIfMoving() {

        float input = Scroll.scrollValue();
        if (lastDirectionUp) {
            if (input < -scrollThreshold) {
                //Setting current limb from limbs array to be not active limb
                SetLimbNotActive();
                Move();
                lastDirectionUp = false;
                print("DOWN");

                if (!pulseGreen.running)
                {
                    pulseGreen.StartPulse = true;

                }
            }
        }
        else if (!lastDirectionUp) {
            if (input > scrollThreshold) {
                //Setting current limb from limbs array to be not active limb
                SetLimbNotActive();

                Move();
                lastDirectionUp = true;
                print("UP");

                if (!pulseGreen.running)
                {
                    pulseGreen.StartPulse = true;

                }
            }
        }
    }

    void SetLimbNotActive() {
        if(scrolledCounter < limbs.Length) limbs[scrolledCounter].GetComponent<Limb>().isActive = false;
    }

    public void LimbDone() {
        limbsDone++;
        if(limbsDone == limbs.Length) {
            print("READY FOR FINAL ANIMATION");
            isDone = true;
        }
    }
}
