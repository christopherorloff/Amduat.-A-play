using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessedDeadBehaviour_Script_Hour8 : MonoBehaviour
{
    //Behaviour
    private Vector3 velocity;

    //Screen stats

    Vector2 screenSizeMax = new Vector2(Screen.width, Screen.height);
    Vector2 screenSizeMin = Vector2.zero;

    private Vector2 screenBoundariesMax;
    private Vector2 screenBoundariesMin;
    private Vector2 screenWorldSize;

    //Interface --> move to parent object
    // public float clampedYPosition = 1 / 3;
    public float speed = 1;
    private float smoothTime;
    [SerializeField]
    private bool followState = false;
    private Transform target;
    private Vector3 currentVelocity;
    private Vector3 queueOffset;

    public Transform topClamp, bottomClamp;
    public SceneManagerScript_Hour8 sceneManager;
    Animator animator;

    public GameObject Particles;

    void Start()
    {
        animator = GetComponent<Animator>();

        //Setup for constant variables
        screenBoundariesMax = (Vector2)Camera.main.ScreenToWorldPoint((Vector3)screenSizeMax);
        screenBoundariesMin = (Vector2)Camera.main.ScreenToWorldPoint((Vector3)screenSizeMin);
        screenWorldSize.x = screenBoundariesMax.x - screenBoundariesMin.x;
        screenWorldSize.y = screenBoundariesMax.y - screenBoundariesMin.y;
        Init();
    }

    void Update()
    {
        // animator.Play(0, 0, normalizedAnimationTime);
        if (!followState)
        {
            Move();
            CheckForBoundaries();
        }
        else
        {
            Follow();
        }
    }

    private void Follow()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position - queueOffset, ref currentVelocity, smoothTime);
        if (transform.position.x < target.position.x + .5f && animator.GetBool("startPushing") == false)
        {
            //This is when the blessed dead are behind the boat and pushing
            animator.SetBool("startPushing", true);
            sceneManager.AddBlessedDeadPushing();
        }
    }

    public void StartFollowing(Transform _target, Vector2 _offsetValues, float _smoothTime)
    {

        Instantiate(Particles, transform.position, Quaternion.identity).transform.parent = this.transform;
        Destroy(Particles, 2f);

        FMOD.Studio.EventInstance blessedDeadClothedInstance;
        string blessedDeadClothedPath = "event:/HOUR 8/BlessedDeadClothed";
        blessedDeadClothedInstance = FMODUnity.RuntimeManager.CreateInstance(blessedDeadClothedPath);
        blessedDeadClothedInstance.start();

        target = _target;
        float offSetX = UnityEngine.Random.Range(0.0f, _offsetValues.x);
        float offSetY = UnityEngine.Random.Range(-_offsetValues.y - (offSetX * 0.8f), _offsetValues.y + (offSetX * 0.8f));
        queueOffset = new Vector3(offSetX, offSetY, 0);
        smoothTime = _smoothTime;
        followState = true;
    }

    private void Move()
    {
        transform.position += velocity * speed * Time.deltaTime;
    }

    private void Move(Vector3 input)
    {
        transform.position += input * speed * Time.deltaTime;
    }

    private void Init()
    {
        velocity = Vector3.left * UnityEngine.Random.Range(0.8f, 1.2f);
        transform.position = new Vector3(screenBoundariesMax.x + UnityEngine.Random.Range(0.0f, 20.0f), GetRandomYPosition(), 0);
    }

    private void CheckForBoundaries()
    {

        if (transform.position.x < screenBoundariesMin.x)
        {
            transform.position = new Vector3(screenBoundariesMax.x, GetRandomYPosition(), 0);
        }
    }


    private float GetRandomYPosition()
    {
        // float clamp = screenWorldSize.y / 2 * clampedYPosition;
        float y = UnityEngine.Random.Range(bottomClamp.position.y, topClamp.position.y);
        return y;
    }
}
