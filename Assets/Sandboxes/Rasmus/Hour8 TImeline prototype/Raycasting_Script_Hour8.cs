using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting_Script_Hour8 : MonoBehaviour
{
    Transform[] childTransforms;

    Animator animator;
    Animation animation;
    public AnimationClip animationClip;

    void Start()
    {
        childTransforms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childTransforms[i] = transform.GetChild(i);
        }
        animator = GetComponent<Animator>();
        animation = GetComponent<Animation>();
        animator.speed = 0.1f;

        animationClip.legacy = true;

    }

    // Update is called once per frame
    void Update()
    {
        animation["Cone_Animation_Hour8"].normalizedTime = 0.5f;
        for (int i = 0; i < childTransforms.Length; i++)
        {

            // Debug.DrawRay(childTransforms[i].position, Color.red);
        }
    }
}
