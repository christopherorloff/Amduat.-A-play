using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpearAnimation : MonoBehaviour
{

    float animspeed = 0.06f;
    float wrongStapSpeed = -0.4f;
    float cameraDrag = 0.25f;
    float drag = 0.29f;


    public AnimationClip lift;
    public AnimationClip stap;
    Animation anim;
    Animator animator;


    bool readyToStap = false;
    bool stapDone = false;
    bool tryToStap = false;
    public bool bounce = false;
    bool idle = true;
    public bool animationDone = false;
    public bool growX = false;
    public bool growY = false;
    public bool startEffect = false;


    public Camera cam;
    public GameObject cameraObject;

    public CameraShake CS;

    void Start()
    {
        anim = GetComponent<Animation>();
        animator = GetComponent<Animator>();

        CS = FindObjectOfType<CameraShake>();
    }

    void Update() { 
   
        //Pitching up charge sound according to animation
        SoundManager.Instance.spearChargeInstance.setParameterByName("Charge", anim["SpearAnimationUp"].normalizedTime);

        //If statement to start the lift animation, if scrollwheel up input is true
        if (!readyToStap && Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            SoundManager.Instance.spearChargeInstance.setParameterByName("Scroll", 1);

            anim.clip = lift;
            anim["SpearAnimationUp"].speed = animspeed;
            anim.Play();

            //camera zooms in and moves
            cam.orthographicSize -= 0.003f;
            cameraObject.transform.Translate(new Vector3(-cameraDrag * Time.fixedDeltaTime, 0, 0));

        }else if (!readyToStap)
        {
            cam.orthographicSize += 0.003f * drag;
            cameraObject.transform.Translate(new Vector3(cameraDrag * drag * Time.fixedDeltaTime, 0, 0));

            if (cam.orthographicSize >= 5)
            {
                cam.orthographicSize = 5;
            }

            if (cameraObject.transform.position.x >= 0)
            {
                cameraObject.transform.position = new Vector3(0, 0, -10);
            }
            print(animspeed);
        }
        

        //if play is not scrolling the animation will slowly play backwards to lower the spear again
       if (!readyToStap && Input.GetAxisRaw("Mouse ScrollWheel") == 0)
        {
            SoundManager.Instance.spearChargeInstance.setParameterByName("Scroll", 0);
            anim.clip = lift;
            anim["SpearAnimationUp"].speed = -0.02f;
            anim.Play();
        }

        //If player tries to stap when spear is not completely lifted, then the lift animation is 
        //played backswards to show wrong stap
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && !readyToStap && anim["SpearAnimationUp"].normalizedTime > 0.01)
        {
            anim.clip = lift;
            anim["SpearAnimationUp"].speed = wrongStapSpeed;
            anim.Play();
            tryToStap = true;
            if (tryToStap)
            {
                cam.orthographicSize += 0.05f * drag;
                cameraObject.transform.Translate(new Vector3(2f * Time.fixedDeltaTime, 0, 0));
                if (anim["SpearAnimationUp"].normalizedTime <= 0.1f)
                {
                    CS.EarlyShake(0.005f, 0.4f);
                }

                    if (cam.orthographicSize >= 5)
                {
                    cam.orthographicSize = 5;
                }

                if (cameraObject.transform.position.x >= 0)
                {
                    cameraObject.transform.position = new Vector3(0, 0, -10);
                }

                SoundManager.Instance.PlaySpearMiss(anim["SpearAnimationUp"].normalizedTime);

            }

        }

        //If the lift animation is done, then we stop animating, so SETH will hold his spear above his head
        if (anim["SpearAnimationUp"].normalizedTime >= 0.99f && !readyToStap)
        {
            //Starting spear ready sound. Stopping other sounds.
            SoundManager.Instance.spearReadyInstance.start();
            SoundManager.Instance.spearChargeInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            SoundManager.Instance.oceanAmbInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            SoundManager.Instance.apopisIdleInstance.setParameterByName("Stop", 1);

            print("readytostap");
            anim.clip = lift;
            anim.enabled = false;
            CS.Shake(0.02f, 10f);
            readyToStap = true;

        }

        //If we are ready for the final stap, and scroll down, we start speardown animation
        if (readyToStap && Input.GetAxisRaw("Mouse ScrollWheel") > 0f && !animationDone && !stapDone)
        {
            SoundManager.Instance.spearHitInstance.start();
            SoundManager.Instance.spearReadyInstance.setParameterByName("Stop", 1);

            anim.enabled = true;
            print("sidstestap");

            anim.clip = stap;
            anim["SpearAnimationDown"].speed = 0.5f;
            CS.StopShake();
            anim.Play();
            stapDone = true;

            //if the speardown animation is done, we stop animating and start the growing of light and particle effect
        }
        if (anim["SpearAnimationDown"].normalizedTime >= 0.8f && stapDone)
        {
            growX = true;
            growY = true;
            startEffect = true;
            print("stapdone");
            anim.enabled = false;
            animationDone = true;
            stapDone = false;
            CS.Shake(0.001f, 0.8f);


        }

    }
    
}
