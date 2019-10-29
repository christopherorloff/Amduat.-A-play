using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class SceneManager_Hour12_Script : MonoBehaviour
{

    public GameObject Sun;
    public GameObject Boat;

    public SpriteRenderer SunColor;
    public SpriteRenderer Background;
    public SpriteRenderer LightCone;
    public SpriteRenderer FadePanel;

    bool startFade = false;

    float colorChange = 0.001f;
    float smallColorChange = 0.0006f;
    float sunMoveSpeedX = 0.7f;
    float sunMoveSpeedY = 0.25f;
    float fadeSpeed = 0.3f;
    float boatMoveSpeedX = 0.5f;

    private void Start()
    {
        Background.color = new Color(0.10f, 0.10f, 0);
    }

    void Update()
    {

        if (Scroll.scrollValueAccelerated() < 0)
        {
            SunColor.color = new Color(SunColor.color.r, SunColor.color.g + colorChange, SunColor.color.g);
            Background.color = new Color(Background.color.r + smallColorChange, Background.color.g + smallColorChange, 0);

            Sun.transform.position -= new Vector3(sunMoveSpeedX * Time.deltaTime, -sunMoveSpeedY * Time.deltaTime, 0);
            Boat.transform.position -= new Vector3(boatMoveSpeedX * Time.deltaTime, 0, 0);

            if (LightCone.color.a >= 0.7f)
            {
                LightCone.color = new Color(LightCone.color.r, LightCone.color.g, LightCone.color.b, 0.0f);
            }
            else
            {
                LightCone.color = new Color(LightCone.color.r, LightCone.color.g, LightCone.color.b, LightCone.color.a - colorChange);
            }
        }

        if (Boat.transform.localPosition.x <= 90)
        {
            print(startFade);
            startFade = true;
        }
        if (startFade)
        {
            FadePanel.color = new Color(FadePanel.color.r, FadePanel.color.g, FadePanel.color.b, FadePanel.color.a + fadeSpeed * Time.deltaTime);

        }


    }
}
