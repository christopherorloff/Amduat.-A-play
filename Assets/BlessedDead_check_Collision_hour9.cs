using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessedDead_check_Collision_hour9 : MonoBehaviour
{

    public Boat_movement_Splash_Hour9_script BoatScript;
    SpriteRenderer sprite;
    float fadeoutSpeed = 0.02f;
    bool startFadeOut = false;


    void Start()
    {
        BoatScript = FindObjectOfType<Boat_movement_Splash_Hour9_script>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (startFadeOut)
        {
            print("start fadeout");
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - fadeoutSpeed);

        }

        if (sprite.color.a <= 0.1)
        {
            Destroy(this.gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boat" && BoatScript.waterSplash)
        {
            print("Hit");
            startFadeOut = true;
        }
    }



}
