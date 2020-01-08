using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;
using UnityEngine.UI;

public class Hour0_fadeAndCheck_Script : Timeline_BaseClass
{
    public GameObject Controlls;
    public Text Text1;
    public Text Text2;
    public SpriteRenderer FadeToBlack;
    public float InputScalar = 1;

    public float scrollScalar = 1;
    public float CheckTime = 0.5f;
    public bool Inverted;

    private bool ended;
    private float fadeScalar = 10f;
    private  Color textColor;
    private float fadeCount1;
    private float fadeCount2;
    private float fadeCount3;
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
        Text1.color = new Color(Text1.color.r,Text1.color.g,Text1.color.b,0);
        Text2.color = new Color(Text2.color.r,Text2.color.g,Text2.color.b,0);
        FadeToBlack.color = new Color(FadeToBlack.color.r,FadeToBlack.color.g,FadeToBlack.color.b,0);
        Checker = false;
        Checked = false;
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
        else if(Inverted && input < 0 && !ended)
        {
            speed = Scroll.scrollValueAccelerated(0.99999f) * InputScalar * Time.deltaTime;
            speed = Mathf.Clamp(speed, -0.006f, 0);
            speed = Mathf.Abs(speed);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
            Debug.Log("Timeline: " +Timeline);
            FadeScroll(speed);
        }
        else if (!Inverted && input > 0 && !ended)
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
        Controlls.transform.position += new Vector3(0,-(scrollValue * scrollScalar),0);

        if(Timeline > 0.11f && Text1.color.a < 1.0f)
        {
            fadeCount1 += scrollValue*fadeScalar;
            Text1.color = new Color(Text1.color.r,Text1.color.g,Text1.color.b,fadeCount1);
        }
        if(Timeline > 0.22f && Text2.color.a < 1.0f)
        {
            fadeCount2 += scrollValue*fadeScalar;
            Text2.color = new Color(Text2.color.r,Text2.color.g,Text2.color.b,fadeCount2);
        }
        if(Timeline > 0.35f && FadeToBlack.color.a < 1.0f)
        {
            fadeCount3 += scrollValue*fadeScalar;
            FadeToBlack.color = new Color(FadeToBlack.color.r,FadeToBlack.color.g,FadeToBlack.color.b,fadeCount3);
        }
        else if (FadeToBlack.color.a >= 1.0f)
        {
            EndScene();
        }
        
    }

    private void EndScene()
    {
        ended = true;
        Debug.Log("END ALL");
    }
}
