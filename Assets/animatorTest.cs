using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorTest : MonoBehaviour
{

    Animation anim;
    public AnimationClip wingUp;

    // Start is called before the first frame update
    void Start()
    {
        wingUp.legacy = true;

        anim = GetComponent<Animation>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            anim["WingAnimationUp"].speed = 0.3f;
        }
    }
}
