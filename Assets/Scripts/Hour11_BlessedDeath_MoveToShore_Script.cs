using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hour11_BlessedDeath_MoveToShore_Script : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool going;
    public bool shoreHit;
    public SpriteRenderer onShoreSprite;

    public Transform boatTarget;
    private Vector3 BDVelocities;
    private float BDXOffset;
    private float BDSmoothTimes;
    public float speed = -0.2f;
    public float colorChangeDuration;

    public TimelineManager_Script_Hour11 timeline;
    private float t = 0;
    private float myTimeLine;
    private float min1 = 1f;
    private float max1 = 0f;
    private float min2 = 0f;
    private float max2 = 1f;
    
    public Color color1;
    public Color color2;
    public float magnitude;
    private float _animationTimePosition1;
    private float _animationTimePosition2;
    public AnimationCurve rubberBandAnimationCurve;
    public float rubberBandDuration = 1f;
    private bool ani1;
    private bool ani2;
    private bool active = true;

    private bool moveIsPlaying;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        BDXOffset = Vector3.Distance(boatTarget.position, transform.position) - UnityEngine.Random.Range(0.3f, 1.2f);
        BDSmoothTimes = UnityEngine.Random.Range(1.0f, 1.5f);
        BDVelocities = Vector3.zero;
        color1 = spriteRenderer.color;
        color2 = onShoreSprite.color;
    }

    void Update()
    {
        myTimeLine = timeline.GetTimeline();
        moveTowardShore();
    }

    public void moveToShore()
    {
        if (!moveIsPlaying) {
            SoundManager.Instance.PlayBlessedDeadAppear();
            moveIsPlaying = true;
        }
        going = true;

    }

    private void moveTowardShore()
    {
        if(going)
        {
            active = false;
            transform.parent = null;
            transform.position += new Vector3(0,  Time.deltaTime*speed, 0);
        }
        else if (!shoreHit)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(boatTarget.position.x - BDXOffset, Mathf.Lerp(transform.position.y, boatTarget.transform.position.y - 0.4f, myTimeLine - 0.5f), 0), ref BDVelocities, BDSmoothTimes);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Shore")
        {
            SoundManager.Instance.PlayBlessedDeadAground();

            //StartCoroutine(changeSprite());
            shoreHit = true;
            going = false;
        }
    }

    private IEnumerator changeSprite()
    {
        while(t < 1f)
        {
            Debug.Log("changing sprite2");
            t += Time.deltaTime / colorChangeDuration;
            color1.a = Mathf.Lerp(min1, max1, t);
            color2.a = Mathf.Lerp(min2, max2, t);
        
            spriteRenderer.color =  color1;
            onShoreSprite.color =  color2;  
            yield return null;
        }
        if (t >= 1.0f)
        {
            t = 0.0f;
        }
        
    }

    public void Shake(float x, float y)
    {
        float initialY = transform.position.y;
        float randomY = initialY + Random.Range(0.01f,0.025f);
        ani2 = true;
        ani1 = true;
        if(active)
        {
        StartCoroutine(RubberBand(randomY,initialY));
        }
    }
     private IEnumerator RubberBand(float newy, float oldy)
    {
        while(_animationTimePosition1 < 1f)
        {
            
            _animationTimePosition1 += Time.deltaTime *rubberBandDuration;
            float step = Mathf.SmoothStep(oldy, newy, rubberBandAnimationCurve.Evaluate(_animationTimePosition1));
            transform.position = new Vector3(transform.position.x, step, 0);
            //Debug.Log("rubber band blessed death1" + _animationTimePosition1*rubberBandDuration);
            yield return null;
        }
        if (_animationTimePosition1 >= 1.0f)
        {
            _animationTimePosition1 = 0.0f;
        }
        while(_animationTimePosition2 < 1f)
        {
            
            _animationTimePosition2 += Time.deltaTime *rubberBandDuration;
            float step = Mathf.SmoothStep(newy, oldy, rubberBandAnimationCurve.Evaluate(_animationTimePosition2));
            transform.position = new Vector3(transform.position.x, step, 0);
            //Debug.Log("rubber band blessed death2" + _animationTimePosition2*rubberBandDuration);
            yield return null;
        }
        if (_animationTimePosition2 >= 1.0f)
        {
            _animationTimePosition2 = 0.0f; 
        }
        transform.position = new Vector3(transform.position.x, oldy, 0);
    }
    //start corutine der skifter sprite og placering.
}
