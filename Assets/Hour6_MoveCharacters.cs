using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hour6_MoveCharacters : MonoBehaviour
{
    // Start is called before the first frame update

    public int numberOfHits = 0;
    public float travelDistance = 5f;
    public SpriteRenderer Nepth;
    public ParticleSystem ParticleEffect;
    public GameObject Seth;

    public Transform[] moveToTransforms;
    int currentMoveTo = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DecideAction()
    {
        print("starter funktion");

        if (numberOfHits<6)
        {

           StartCoroutine(MoveCharacters(this.transform, moveToTransforms[currentMoveTo], 2f));
        }
     
        if (numberOfHits == 6)
        {
            Seth.GetComponent<BackGround_Object_Movement_Script>().enabled = false;
            StartCoroutine(MoveCharacters(this.transform, moveToTransforms[currentMoveTo], 2f));
            StartCoroutine(MoveCharacters(Seth.transform, moveToTransforms[currentMoveTo], 2f));


        }


    }



    IEnumerator MoveCharacters(Transform fromPos, Transform toPos, float duration)
    {

        float counter = 0;
        Vector3 startPos = fromPos.position;
        Vector3 endPos = toPos.position;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            fromPos.position = Vector3.Lerp(startPos, endPos, counter/duration);
            yield return null;
        }

        currentMoveTo++;

        if (numberOfHits == 5)
        {
            StartCoroutine(FadeOutSprite(0, 3));
            ParticleEffect.Play();
            yield return new WaitForSeconds(1);
            Seth.transform.eulerAngles = new Vector3(0, 0, 0);
            Destroy(ParticleEffect, 5f);

            }
            /*
            float startTime = Time.time;
            float xStart = transform.position.x;
            float xEnd = xStart + travelDistance;

            while (transform.position.x < xEnd)
            {
                float t = (Time.time - startTime) / 3;
                float step = Mathf.SmoothStep(xStart, xEnd, t);
                transform.position = new Vector3(step, transform.position.y, 0);
                yield return null;
            }

            yield return new WaitForSeconds(3f);

            if (numberOfHits == 5)
            {
                StartCoroutine(FadeOutSprite(0, 3));
                ParticleEffect.Play();
                yield return new WaitForSeconds(1);
                Seth.transform.eulerAngles = new Vector3(0, 0, 0);
              //  Destroy(ParticleEffect, 5f);

            }
            */

        }

        IEnumerator FadeOutSprite(float value, float time)
    {
        float startValue = 1;
        float startTime = Time.time;

        while (Nepth.color.a > value)
        {
            float t = (Time.time - startTime) / time;
            Color newColor = new Color(Nepth.color.r, Nepth.color.g, Nepth.color.b, Mathf.Lerp(startValue, value, t));
            Nepth.color = newColor;
            yield return null;
        }
        

    }

   


}
