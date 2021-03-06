using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KepriAnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer1;
    public SpriteRenderer spriteRenderer2;
    public float animationTime;

    private IEnumerator coroutine;

    private float t = 0f;
    private int numberOfAnim;
    private float min1 = 0f;
    private float max1 = 1f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer1.sprite= sprites[0];

    }
    
    private void Update() 
    {
        WaitAndPrint();
    }



    private void WaitAndPrint()
    {            
            spriteRenderer2.sprite = spriteRenderer1.sprite;
            spriteRenderer1.sprite = sprites[numberOfAnim];

            spriteRenderer1.color  = new Color(1,1,1,Mathf.Lerp(min1,max1,t));

            if (t < 1f)
            { 
            t += Time.deltaTime*animationTime;
            //Debug.Log(t);
            }

            if (t >= 1.0f)
            {
                float temp1 = max1;
                max1 = min1;
                min1 = temp1;


                t = 0.0f;
            }

            if(numberOfAnim >= sprites.Length-1)
            {
                numberOfAnim = -1;
                
            }
            numberOfAnim++;
        
    }
}
