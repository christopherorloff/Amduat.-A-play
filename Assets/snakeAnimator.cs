using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeAnimator : MonoBehaviour
{

    public SpearAnimation SA;
    public AnimationClip SnakeIdle;
    public AnimationClip SnakeDie;
    float animSpeed = 0.2f;

    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        SA = FindObjectOfType<SpearAnimation>();

        anim = GetComponent<Animation>();

        anim.clip = SnakeIdle;
        anim["SnakeAniIdle"].speed = animSpeed;
        anim.Play();

    }

    private void Update()
    {
        if (SA.stapDone)
        {
            anim.clip = SnakeDie;
            anim["SnakeAniDead"].speed = animSpeed;
            anim.Play();
        }
    }


}
