using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class SceneManager_Hour1_Script2 : MonoBehaviour
{
    public GameObject cam;
    public GameObject Sun;
    public GameObject Boat;
    public SpriteRenderer SunColor;
    public SpriteRenderer Background;
    public SpriteRenderer LightCone;

    float colorChange = 0.0025f;
    float smallColorChange = 0.0012f;
    float sunMoveSpeedX = 1.2f;
    float sunMoveSpeedY = 0.25f;
    float camMoveSpeedX = 0.6f;

    float boatMoveSpeedX = 1.1f;

    float time = 0;

    void Update()
    {

        if (Scroll.scrollValueAccelerated() > 0)
        {
            SunColor.color = new Color(SunColor.color.r, SunColor.color.g - colorChange, SunColor.color.g);
            Background.color = new Color(Background.color.r - smallColorChange, Background.color.g - smallColorChange, Background.color.b - smallColorChange);
            
            cam.transform.position += new Vector3(camMoveSpeedX * Time.deltaTime, 0, 0);
            Sun.transform.position += new Vector3(sunMoveSpeedX * Time.deltaTime, -sunMoveSpeedY * Time.deltaTime, 0);
            Boat.transform.position += new Vector3(boatMoveSpeedX * Time.deltaTime, 0, 0);

            if (LightCone.color.a >= 0.7f)
            {
                LightCone.color = new Color(LightCone.color.r, LightCone.color.g, LightCone.color.b, 0.7f);
            }
            else
            {
                LightCone.color = new Color(LightCone.color.r, LightCone.color.g, LightCone.color.b, LightCone.color.a + colorChange);
            }
        }



        
    }
}
