using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class General_WrongInput_Movement_Script : MonoBehaviour
{
    public GameObject mainObject;
    public GameObject targetDirection;
    public GameObject startDirection;
    private Vector3 _target;
    private Vector3 _startPoint;


    public bool scrollingDown;
    public float speedMod = 225;
    public float speedRubMod = 5;

    private bool invoked;

    public float targetTime = 0.2f;
    public float targetTimeNow;

    public bool wrongInput = false;
    void start()
    {
        targetTimeNow = targetTime;
    }

    void Update()
    {
        float input = Scroll.scrollValue();
        _target = targetDirection.transform.position;
        _startPoint = startDirection.transform.position;


        float dist = Vector3.Distance(_startPoint, mainObject.transform.position); 
        float rubberSpeed =  (dist*speedRubMod) * Time.deltaTime;
        mainObject.transform.position = Vector3.MoveTowards(mainObject.transform.position, _startPoint, rubberSpeed);
        

        
        if(input <0 && scrollingDown)
        {
            targetTimeNow = targetTime;
            wrongInput = true;
            rubberBand(input);
        }
        else if (input>0 && !scrollingDown)
        {
            targetTimeNow = targetTime;
            wrongInput = true;
            rubberBand(input);
        }

        targetTimeNow -= Time.deltaTime;
       
        if (targetTimeNow <= 0.0f)
        {
            wrongInput = false;
        }
    }
    public void rubberBand(float input)
    {
        
        float speed = Mathf.Abs(input) * Time.deltaTime;
        float step =  (speed*speedMod) * Time.deltaTime; // calculate distance to move
        mainObject.transform.position = Vector3.MoveTowards(mainObject.transform.position, _target, step);
        //Start Coroutine
    }

    //Lav coroutine

    /*
        bevæg object via springcruve til target position

        herefter bevæg object fra target til start postiosion via anden springcurve


        Lerp => target 
        Check når det er sket
        Herefter gå tilbage

    */
}
