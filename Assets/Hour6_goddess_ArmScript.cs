using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class Hour6_goddess_ArmScript : MonoBehaviour
{
   
    //private
    float input;
    Transform root;
    int hitCount = 0;
    ThrowScript throwScript;
    float snapBack;

    bool inputIsActive = false;
    public bool throwing = false;
    float maxAngle = 10;
    float minAngle = -6;
    public bool waiting = false;
    private float timeLeft = 0;
    public float throwTimer = 5f;
    public bool done = false;
    //public
    public float scalar = 1;
    public float deltaThreshold = 0.3f;
    public float drag = 0.9f;
    public float snapBackSpeed = 0;

    public GameObject snake;
    SnakeStateScript snakeState;

    public Vector3 throwRotationEnd;

    public Vector3 throwRotationStart;
    private Vector3 currentAngle;
    static float t1 = 0.0f;
    static float t2 = 0.0f;

    public bool thrown = false;
    public bool thrownDone = false;


    void OnEnable()
    {

        EventManager.turnOffInputEvent += TurnOffInput;
        EventManager.turnOnInputEvent += TurnOnInput;
    }

    void OnDisable()
    {
        EventManager.turnOffInputEvent -= TurnOffInput;
        EventManager.turnOnInputEvent -= TurnOnInput;
    }

    void TurnOffInput()
    {
        inputIsActive = false;
    }

    void TurnOnInput()
    {
        inputIsActive = true;
    }

    void Start()
    {
        root = FindComponentInChildWithTag(this.gameObject, "Root");
        throwScript = GetComponentInChildren<ThrowScript>();
        snakeState = snake.GetComponent<SnakeStateScript>();
        timeLeft = throwTimer;
        currentAngle = root.transform.eulerAngles;
    }

    void Update()
    {
        if (inputIsActive)
        {
            TakeInput();
            ProcessInput();
        }      
    }


    private void TakeInput()
    {
        //input = Scroll.scrollValueAccelerated() * scalar * drag;
        input = (Scroll.scrollValue() * scalar * drag);
        if(input > 0.05 && !waiting)
        {
            throwScript.throwDone = false;
            hitCount++;
            Debug.Log("throw" + hitCount);
            waiting = true;
            throwing = true;
            timeLeft = throwTimer;
            
        }
    }

    private void ProcessInput()
    {
        if (throwScript == null)
            
            {
                throwScript = GetComponentInChildren<ThrowScript>();
            }
        if(throwing && !done && !thrown)
        {
            t1 += 0.5f * Time.deltaTime;
            currentAngle = new Vector3(
                Mathf.LerpAngle(currentAngle.x, throwRotationEnd.x, t1),
                Mathf.LerpAngle(currentAngle.y, throwRotationEnd.y, t1),
                Mathf.LerpAngle(currentAngle.z, throwRotationEnd.z, t1));
            Debug.Log(t1);
        }
        if (t1 > 0.2f)
        {
            done = true;
            thrown = true;
        }

        if(thrown && done && thrown && !thrownDone)
        {
            throwScript.ThrowKnife(snake.transform.position);
            //thrownDone = true;
        }

        if(throwing && done && throwScript.throwDone == true)
        {
            t2 += Time.deltaTime;
            currentAngle = new Vector3(
                Mathf.LerpAngle(currentAngle.x, throwRotationStart.x, t2),
                Mathf.LerpAngle(currentAngle.y, throwRotationStart.y, t2),
                Mathf.LerpAngle(currentAngle.z, throwRotationStart.z, t2));
        }
        if(t2 > 0.5f)
        {
            throwing = false;
            done = false;

        }

        root.transform.eulerAngles = currentAngle;

        if(!done && !throwing)
        {
            t2 = 0f;
            t1 = 0f;
            waiting = false;
            thrownDone = false;
            thrown = false;
        }
     }
    

    public Transform FindComponentInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.GetComponent<Transform>();
            }
        }
        return null;
    }
}
