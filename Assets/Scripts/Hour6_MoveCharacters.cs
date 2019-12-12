using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hour6_MoveCharacters : MonoBehaviour
{
    // Start is called before the first frame update

    public int numberOfHits = 0;
    private float travelDistance = 5f;
    private int currentMoveTo = 0;

    public SpriteRenderer Nepth;
    public ParticleSystem ParticleEffect;
    public GameObject Seth;
    public Transform[] moveToTransforms;



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
            //StartCoroutine(MoveCharacters(this.transform, moveToTransforms[currentMoveTo], 2f));
            //StartCoroutine(MoveCharacters(Seth.transform, moveToTransforms[currentMoveTo], 2f));
        }


    }



    IEnumerator MoveCharacters(Transform fromPos, Transform toPos, float duration)
    {

        //float counter = 0;
        Vector3 startPos = fromPos.position;
        Vector3 endPos = toPos.position;
        float t = 0;
        float startTime = Time.time;
        while (t<1)
        {
            t = (Time.time - startTime) / duration;
            t = Mathf.SmoothStep(0,1,t);
            t = Mathf.Clamp(t,0,1);
            fromPos.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        currentMoveTo++;

        if (numberOfHits == 5)
        {
            StartCoroutine(FadeOutSprite(0, 3));
            ParticleEffect.Play();
            SoundManager.Instance.PlayGodAppear();
            yield return new WaitForSeconds(1);
            Seth.transform.eulerAngles = new Vector3(0, 0, 0);
            Destroy(ParticleEffect, 5f);

            }
          

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
