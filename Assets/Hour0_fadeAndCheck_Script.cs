using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;
using UnityEngine.UI;

public class Hour0_fadeAndCheck_Script : Timeline_BaseClass
{
    public GameObject ImageToFade1;
    public GameObject ImageToFade2;
    public GameObject TextToFade;
    public Image FadeToBlack;
    public float InputScalar = 1;

    public float scrollScalar = 1;
    public float CheckTime = 0.5f;
    public bool Inverted;

    
    private float inputFixed;
    private float speed;
    private float input;
    private bool Checker;
    private float startTime;
    private float combinedInput;
    private bool Checked;
    // Start is called before the first frame update
    void Start()
    {
        Checker = false;
        Checked = false;
        AddTimelineEvent(0.8f, fade);
        HandleKeys();
    }

    // Update is called once per frame
    void Update()
    {
        input = Scroll.scrollValueAccelerated();
        ConvertInput(input);
        
    }

    private void ConvertInput(float inputCheck)
    {
        speed = Scroll.scrollValueAccelerated(0.99999f) * InputScalar * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0, 0.0006f);
        inputFixed += speed;
        inputFixed = Mathf.Clamp(inputFixed, 0, 1);

        if(Checked == false){
            if(inputCheck != 0 && Checker == false)
            {
                startTime = Time.time;
                Debug.Log("Checker started add: " + startTime);
                Checker = true;
            }
            if(Time.time < startTime+CheckTime && Checker)
            {
                combinedInput += input;
                Debug.Log("Checker " + combinedInput + " " + Time.time);
            }
            else if(combinedInput < 0)
            {
                Inverted = true;
                Debug.Log("Inverted");
                Checked = true;
            }
            else if(combinedInput > 0)
            {
                Inverted = false;
                Debug.Log("Natural");
                Checked = true;
            }
        }
        else if(Inverted && input < 0)
        {
            speed = Scroll.scrollValueAccelerated(0.99999f) * InputScalar * Time.deltaTime;
            speed = Mathf.Clamp(speed, -0.006f, 0);
            speed = Mathf.Abs(speed);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
            Debug.Log("Timeline: " +Timeline);
            FadeScroll(speed);
        }
        else if (!Inverted && input > 0)
        {
            speed = Scroll.scrollValueAccelerated(0.99999f) * InputScalar * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 0.0006f);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
            Debug.Log("Timeline: " +Timeline);
            FadeScroll(speed);
        }
    }

    private void FadeScroll(float scrollValue)
    {
        ImageToFade2.transform.position += new Vector3(0,scrollValue * scrollScalar,0);
        ImageToFade1.transform.position += new Vector3(0,scrollValue * scrollScalar,0);
        TextToFade.transform.position += new Vector3(0,scrollValue * scrollScalar,0);
    }

    private void fade()
    {
        Debug.Log("Fade");
    }
}
