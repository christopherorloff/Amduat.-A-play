using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;
using UnityEngine.UI;

public class Hour0_fadeAndCheck_Script : Timeline_BaseClass
{
    public GameObject Controlls1;
    public GameObject Controlls2;
    public Text Text1;
    public Text Text2;
    public SpriteRenderer FadeToBlack;
    public float InputScalar = 1;
    public float animationTime2;
    public float scrollScalar = 1;
    public float CheckTime = 0.5f;
    public bool Inverted;
    public bool locked;

    private IEnumerator coroutine2;
    private bool firstend;
    private bool ended;
    private float fadeScalar = 15f;
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
        Controlls1.SetActive(true);
        Controlls2.SetActive(false);
        HandleKeys();
    }

    // Update is called once per frame
    void Update()
    {
        input = Scroll.scrollValueAccelerated();
        if(!locked)
        {
            ConvertInput(input);
        }
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
        else if(Inverted && input < 0 && !firstend && !ended)
        {
            speed = Scroll.scrollValueAccelerated(0.99999f) * InputScalar*0.7f * Time.deltaTime;
            speed = Mathf.Clamp(speed, -0.006f, 0);
            speed = Mathf.Abs(speed);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
            Debug.Log("Timeline1: " +Timeline);
            FadeScroll(speed);
        }
        else if(Inverted && input > 0 && !ended && firstend)
        {
            speed = Scroll.scrollValueAccelerated(0.99999f) * InputScalar * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 0.0006f);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
            Debug.Log("Timeline2: " +Timeline);
            FadeScroll(speed);
        }
        else if (!Inverted && input > 0 && !firstend && !ended)
        {
            speed = Scroll.scrollValueAccelerated(0.99999f) * InputScalar * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 0.0006f);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
            Debug.Log("Timeline3: " +Timeline);
            FadeScroll(speed);
        }
        else if (!Inverted && input < 0 && !ended && firstend)
        {
            speed = Scroll.scrollValueAccelerated(0.99999f) * InputScalar*0.7f * Time.deltaTime;
            speed = Mathf.Clamp(speed, -0.006f, 0);
            speed = Mathf.Abs(speed);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
            Debug.Log("Timeline4: " +Timeline);
            FadeScroll(speed);
        }
    }

    private void FadeScroll(float scrollValue)
    {
        if(!firstend)
        {
            Controlls1.transform.position += new Vector3(0,-(scrollValue * scrollScalar),0);
        }
        else if (firstend)
        {
            Controlls2.transform.position += new Vector3(0,(scrollValue * scrollScalar),0);
        }
        

        if(Timeline > 0.18f && FadeToBlack.color.a < 1.0f && !firstend)
        {
            fadeCount3 += scrollValue*fadeScalar;
            FadeToBlack.color = new Color(FadeToBlack.color.r,FadeToBlack.color.g,FadeToBlack.color.b,fadeCount3);
        }
        else if (FadeToBlack.color.a >= 1.0f && !firstend)
        {
            firstend = true;
            coroutine2 = WaitAndPrint2(animationTime2);
            StartCoroutine(coroutine2);
        }
        else if(Timeline > 0.75f && FadeToBlack.color.a < 1.0f && firstend)
        {
            fadeCount3 += scrollValue*fadeScalar;
            FadeToBlack.color = new Color(FadeToBlack.color.r,FadeToBlack.color.g,FadeToBlack.color.b,fadeCount3*1.2f);
        }
        else if (Timeline > 0.88f && FadeToBlack.color.a >= 1.0f && firstend)
        {
            EndScene();
        }
        

        if(Timeline > 0.50f && Text2.color.a < 1.0f)
        {
            fadeCount2 += scrollValue*fadeScalar;
            Text2.color = new Color(Text2.color.r,Text2.color.g,Text2.color.b,fadeCount2);
            if(Text2.color.a >= 0.5f)
            {
                Controlls2.SetActive(false);
            }
        }
         if(Timeline > 0.5f && Text1.color.a < 1.0f)
        {
            fadeCount1 += scrollValue*fadeScalar;
            Text1.color = new Color(Text1.color.r,Text1.color.g,Text1.color.b,fadeCount1);
        }
        
        if(Timeline > 0.65f && Text2.color.a >= 1.0f)
        {
            Text1.transform.position += new Vector3(0,(scrollValue * scrollScalar),0);
            Text2.transform.position += new Vector3(0,(scrollValue * scrollScalar),0);
        }

        


        
    }

    private IEnumerator WaitAndPrint2(float waitTime)
    {
        
            yield return new WaitForSeconds(waitTime);
            Controlls1.SetActive(false);
            Controlls2.SetActive(true);
            FadeToBlack.color = new Color(FadeToBlack.color.r, FadeToBlack.color.g, FadeToBlack.color.b, 1);
            while (FadeToBlack.color.a > 0.0f)
            {
                FadeToBlack.color = new Color(FadeToBlack.color.r, FadeToBlack.color.g, FadeToBlack.color.b, FadeToBlack.color.a - (Time.deltaTime / 2));
                yield return null;
            }
            firstend = true;
            fadeCount3 = 0;
    }

    private void EndScene()
    {
        ended = true;
        Debug.Log("END ALL");
    }
}
