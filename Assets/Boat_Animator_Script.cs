using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_Animator_Script : MonoBehaviour
{
    public Sprite[] boatSprites;
    public SpriteRenderer boatSpriteRenderer;
    public float animationTime;

    private IEnumerator coroutine;

    private int numberOfAnim;
    // Start is called before the first frame update
    void Start()
    {
        numberOfAnim = boatSprites.Length-1;
        coroutine = WaitAndPrint(animationTime);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            boatSpriteRenderer.sprite = boatSprites[numberOfAnim];
            if(numberOfAnim >= boatSprites.Length-1)
            {
                numberOfAnim = -1;
                
            }
            numberOfAnim++;
        }
    }
    // Update is called once per frame
}
