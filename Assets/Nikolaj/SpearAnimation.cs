using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpearAnimation : MonoBehaviour
{

    float animspeed = 0.6f;
    float wrongStapSpeed = -4f;


    public AnimationClip lift;
    public AnimationClip stap;
    Animation anim;
    Animator animator;


    bool readyToStap = false;
    bool stapDone = false;
    bool tryToStap = false;
    bool bounce = false;
    bool idle = true;
    bool animationDone = false;
    bool growX = false;
    bool growY = false;
    bool startEffect = false;

    public GameObject light1;
    public GameObject light2;
    public GameObject Effect;

    public SpriteRenderer Snake;
    public SpriteRenderer Panel;


    void Start()
    {
        anim = GetComponent<Animation>();
        animator = GetComponent<Animator>();

        light1.transform.localScale = new Vector3(0f, 0, 1);
        light2.transform.localScale = new Vector3(0f, 0, 1);


    }

    void FixedUpdate()
    {
        if (!readyToStap && Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {

            anim.clip = lift;
            anim["SpearUP"].speed = animspeed;
            anim.Play();
            idle = false;

        }
       if (!readyToStap && Input.GetAxisRaw("Mouse ScrollWheel") == 0 && !idle)
        {
            anim.clip = lift;
            anim["SpearUP"].speed = -0.2f;
            anim.Play();
            idle = true;
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && !readyToStap)
        {
            anim.clip = lift;
            anim["SpearUP"].speed = wrongStapSpeed;
            anim.Play();
        }


        if (anim["SpearUP"].normalizedTime >= 0.99f)
        {
            print("readytostap");
            readyToStap = true;
            anim.clip = lift;
            anim.enabled = false;
        }


        if (readyToStap && Input.GetAxisRaw("Mouse ScrollWheel") > 0f && !animationDone)
        {
            anim.enabled = true;
            print("sidstestap");
            anim.clip = stap;
            anim["SpearDOWN"].speed = 3f;
            if (anim["SpearDOWN"].normalizedTime >= 0.68f)
            {
                print("stapdone");
                anim.enabled = false;
                animationDone = true;
                growX = true;
                growY = true;
                startEffect = true;

            }
            anim.Play();

        }

        if (startEffect)
        {
            Instantiate(Effect, light1.transform.position, Quaternion.identity);
            startEffect = false;
        }

        if (animationDone == true && growX == true)
        {
            print("light!!!");
            light1.transform.localScale += new Vector3( 0.35f * Time.fixedDeltaTime, 0, 0);
            light2.transform.localScale += new Vector3( 0.35f * Time.fixedDeltaTime, 0, 0);


            if (light1.transform.localScale.x >= 1.5f)
            {
                growX = false;

            }
        }

        if (animationDone == true && growY == true)
        {
            print("light!!!");
            light1.transform.localScale += new Vector3(0, 0.70f * Time.fixedDeltaTime, 0);
            light2.transform.localScale += new Vector3(0, 0.70f * Time.fixedDeltaTime, 0);


            if (light1.transform.localScale.y >= 1.0f)
            {
                growY = false;

            }
        }

        if (!growY && !growX && animationDone == true) 
        {
            Snake.color = new Color(Snake.color.r + 0.005f, Snake.color.g, Snake.color.b - 0.005f);
        }
            
        
    }
    
}
