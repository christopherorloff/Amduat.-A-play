using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class animateWing : MonoBehaviour
{

    public Animation anim;
    //public AnimationClip wingAnim1;

    //public AnimationClip WingUp;

    bool running = false;

    float startAnim = 0.0f;
    float midAnim = 0.5f;
    float endAnim = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        //anim["WingAnimUp"].speed = 1f;
       anim.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey)
        {
            //anim.clip = wingAnim1;
            anim["WingAnimUp"].speed = 1f;
            anim.Play();
        }

        if (Scroll.scrollValue() < 0 && !running)
        {
            print("scroller");
           // anim.clip = wingAnim1;
            anim["WingAnimUp"].speed = 0f;
            anim.Play();
            if (anim["WingAnimUp"].normalizedTime == 0f)
            {
                StartCoroutine(AnimStart(startAnim, midAnim, 1f, "WingAnimUp"));
            }

            if (anim["WingAnimUp"].normalizedTime == 0.5f)
            {
                print("sut pik");
                StartCoroutine(AnimStart(midAnim, endAnim, 1f, "WingAnimUp"));
            }

        }


    }


    public IEnumerator AnimStart(float startValue, float endValue, float time, string animation)
    {
        running = true;
        float startTime = Time.time;

        while (anim[animation].normalizedTime < endValue)
        {
            float t = (Time.time - startTime) / time;
            anim[animation].normalizedTime = Mathf.Lerp(startValue, endValue, t);
            yield return null;

        }
        running = false;


    }
}
