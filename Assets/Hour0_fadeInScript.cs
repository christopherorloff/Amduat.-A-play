using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hour0_fadeInScript : MonoBehaviour
{
    public Text text;
    public SpriteRenderer spriteRenderer;
    private IEnumerator coroutine1;
    private IEnumerator coroutine2;
    public float animationTime1;
    public float animationTime2;
    public Hour0_fadeAndCheck_Script hour0;
    void Start()
    {
        hour0.locked = true;
        coroutine1 = WaitAndPrint1(animationTime1);
        StartCoroutine(coroutine1);
        coroutine2 = WaitAndPrint2(animationTime2);
        StartCoroutine(coroutine2);

    }

    private IEnumerator WaitAndPrint1(float waitTime)
    {
        
            yield return new WaitForSeconds(waitTime);
            
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            while (text.color.a > 0.0f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 3));
                yield return null;
            }
            hour0.locked = false;
            
    }
    private IEnumerator WaitAndPrint2(float waitTime)
    {
        
            yield return new WaitForSeconds(waitTime);
            
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
            while (spriteRenderer.color.a > 0.0f)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - (Time.deltaTime / 3));
                yield return null;
            }
            hour0.locked = false;
            
    }


}
