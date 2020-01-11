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
    public bool throwStopper = false;
    float maxAngle = 10;
    float minAngle = -6;
    public bool waiting = false;
    private float timeLeft = 0;
    public float throwTimer = 5f;
    public bool done = false;
    public bool armNotUp = true;
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

    public bool resettingThrow = false;

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
        if (input > 0.05f && !waiting)
        {
            timeLeft = throwTimer;
            throwScript.throwDone = false;
            hitCount++;
            Debug.Log("throw" + hitCount);
            waiting = true;
            throwing = true;
            throwStopper = true;
        }


        //Timer 2 sec
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            waiting = false;
        }
    }

    private void ProcessInput()
    {
        if (throwScript == null)

        {
            throwScript = GetComponentInChildren<ThrowScript>();
        }
        if (throwing && !done && !thrownDone && armNotUp)
        {
            throwStopper = false;
            armNotUp = false;
            StartCoroutine(ArmDown());

        }
        if (t1 > 1f)
        {
            done = true;
            thrown = true;
        }

        if (throwing && done && thrown && !thrownDone && !throwStopper)
        {
            throwScript.ThrowKnife(snake.transform.position);
            throwStopper = true;
        }

        if (throwing && done && throwScript.throwDone == true)
        {
            StartCoroutine(WaitAndLift(1.5f));
        }

        if (resettingThrow)
        {
            t2 = 0f;
            t1 = 0f;
            done = false;
            throwing = false;
            thrownDone = false;
            thrown = false;
            resettingThrow = false;
            armNotUp = true;
        }

    }

    IEnumerator WaitAndLift(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        t2 += 0.3f * Time.deltaTime;
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, throwRotationStart.x, t2),
            Mathf.LerpAngle(currentAngle.y, throwRotationStart.y, t2),
            Mathf.LerpAngle(currentAngle.z, throwRotationStart.z, t2));
        root.transform.eulerAngles = currentAngle;
        // Debug.Log(t2);

        if (t2 > 0.13f)
        {
            resettingThrow = true;
        }
    }
    IEnumerator ArmDown()
    {
        while (t1 < 1f)
        {
            t1 += 10f * Time.deltaTime;
            currentAngle = new Vector3(
                Mathf.LerpAngle(currentAngle.x, throwRotationEnd.x, t1),
                Mathf.LerpAngle(currentAngle.y, throwRotationEnd.y, t1),
                Mathf.LerpAngle(currentAngle.z, throwRotationEnd.z, t1));
            // Debug.Log(t1);
            root.transform.eulerAngles = currentAngle;
            yield return null;
        }
        if (t1 > 1f)
        {
            done = true;
            thrown = true;
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
