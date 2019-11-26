using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_Animator_Script : MonoBehaviour
{
    public Sprite[] boatSprites;
    public SpriteRenderer boatSpriteRenderer;
    public SpriteRenderer boatReflectionSpriteRenderer;
    public float animationTime;

    public GameObject masks;

    public float bounceMod;

    private IEnumerator coroutine;

    private float t = 0f;
    private int numberOfAnim;
    private float min1 = -2f;
    private float max1 = 2f;
    // Start is called before the first frame update
    void Start()
    {
        /*coroutine = WaitAndPrint(animationTime);
        StartCoroutine(coroutine);*/
    }

    void Update()
    {
        boatSpriteRenderer.transform.localRotation = Quaternion.Euler(0,0,Mathf.Lerp(min1,max1,t));
        boatReflectionSpriteRenderer.transform.localRotation = Quaternion.Euler(180,0,Mathf.Lerp(min1,max1,t));
        boatSpriteRenderer.transform.localPosition = new Vector3(0,Mathf.Lerp((min1/bounceMod),(max1/bounceMod),t),0);
        boatReflectionSpriteRenderer.transform.localPosition = new Vector3(0,(Mathf.Lerp((min1/bounceMod),(max1/bounceMod),t)-1.1f),0);
        masks.transform.localPosition = new Vector3(0,(Mathf.Lerp((min1/bounceMod),(max1/bounceMod),t)-0.55f),0);

        if (t < 1f)
        { 
            t += Time.deltaTime/animationTime;
            //Debug.Log(t);
        }

        if (t >= 1.0f)
        {
            float temp1 = max1;
            max1 = min1;
            min1 = temp1;

            t = 0.0f;
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            
            /*boatSpriteRenderer.sprite = boatSprites[numberOfAnim];
            if(numberOfAnim >= boatSprites.Length-1)
            {
                numberOfAnim = -1;
                
            }
            numberOfAnim++;*/
        }
    }
    // Update is called once per frame
}
