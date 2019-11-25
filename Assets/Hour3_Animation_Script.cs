using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hour3_Animation_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer reflectionSpriteRenderer;
    public float animationTime;

    private IEnumerator coroutine;

    private float t = 0f;
    private int numberOfAnim;
    private float min1 = -2f;
    private float max1 = 2f;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = WaitAndPrint(animationTime);
        StartCoroutine(coroutine);
    }



    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            
            spriteRenderer.sprite = sprites[numberOfAnim];
            if(numberOfAnim >= sprites.Length-1)
            {
                numberOfAnim = -1;
                
            }
            numberOfAnim++;
        }
    }
}
