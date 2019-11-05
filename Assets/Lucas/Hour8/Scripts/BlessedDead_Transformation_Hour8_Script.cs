using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessedDead_Transformation_Hour8_Script : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool isNaked = true;
    public Sprite naked;
    public Sprite clothed;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isNaked)
        {
            spriteRenderer.sprite = naked;
        }
        else
        {
            spriteRenderer.sprite = clothed;
        }
    }
}
