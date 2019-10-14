using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpearAnimation : MonoBehaviour
{

    float animspeed = 0.6f;
    float wrongStapSpeed = -4f;
    float cameraDrag = 0.25f;
    float drag = 0.29f;


    public AnimationClip lift;
    public AnimationClip stap;
    Animation anim;
    Animator animator;


    bool readyToStap = false;
    bool stapDone = false;
    bool tryToStap = false;
    bool bounce = false;
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

    void FixedUpdate()
    {
        //If statement to start the lift animation, if scrollwheel up input is true
        if (!readyToStap && Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {

            anim.clip = lift;
            anim["SpearUP"].speed = animspeed;
            anim.Play();
            idle = false;

            //camera zooms in and moves
            cam.orthographicSize -= 0.003f;
            cameraObject.transform.Translate(new Vector3(-cameraDrag * Time.fixedDeltaTime, 0, 0));

        }

        //if play is not scrolling the animation will slowly play backwards to lower the spear again
       if (!readyToStap && Input.GetAxisRaw("Mouse ScrollWheel") == 0 && !idle)
        {
            anim.clip = lift;
            anim["SpearUP"].speed = -0.2f;
            anim.Play();
            //idle = true;

            //camera slowly zooms out
            cam.orthographicSize += 0.003f * drag;
            cameraObject.transform.Translate(new Vector3(cameraDrag * drag * Time.fixedDeltaTime, 0, 0));

            //when ortographic size hits 5, it stays 5 and wont zoom out more
            if (cam.orthographicSize >= 5)
            {
                cam.orthographicSize = 5;
            }

            if (cameraObject.transform.position.x >= 0)
            {
                cameraObject.transform.position = new Vector3(0, 0, -10);
            }


        }

        //If player tries to stap when spear is not completely lifted, then the lift animation is 
        //played backswards to show wrong stap
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && !readyToStap)
        {
            anim.clip = lift;
            anim["SpearUP"].speed = wrongStapSpeed;
            anim.Play();

            //Camera zooms out quickly
            cam.orthographicSize += 0.05f * drag;
            cameraObject.transform.Translate(new Vector3(2f * Time.fixedDeltaTime, 0, 0));
            if (cam.gameObject.transform.position.x >= 0)
            {
                cam.transform.position = cameraObject.transform.position;

            }

            if (cam.orthographicSize >= 5)
            {
                cam.orthographicSize = 5;
            }

            if (anim["SpearUP"].normalizedTime <= 0.1f)
            {
                CS.EarlyShake(0.002f, 0.3f);
            }
        }

        //If the lift animation is done, then we stop animating, so SETH will hold his spear above his head
        if (anim["SpearUP"].normalizedTime >= 0.99f)
        {
            print("readytostap");
            readyToStap = true;
            anim.clip = lift;
            anim.enabled = false;
            CS.Shake(0.001f, 10f);
        }

        //If we are ready for the final stap, and scroll down, we start speardown animation
        if (readyToStap && Input.GetAxisRaw("Mouse ScrollWheel") > 0f && !animationDone)
        {
            anim.enabled = true;
            print("sidstestap");
            anim.clip = stap;
            anim["SpearDOWN"].speed = 3f;
            CS.StopShake();

            //if the speardown animation is done, we stop animating and start the growing of light and particle effect
            if (anim["SpearDOWN"].normalizedTime >= 0.68f)
            {
                print("stapdone");
                anim.enabled = false;
                animationDone = true;
                growX = true;
                growY = true;
                startEffect = true;
                CS.Shake(0.01f, 0.8f);

            }
            anim.Play();

        }

    }
    
}
