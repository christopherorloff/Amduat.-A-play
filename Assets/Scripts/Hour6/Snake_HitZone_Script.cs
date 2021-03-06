using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_HitZone_Script : MonoBehaviour
{
    bool hit = false;
    bool hitVFX = false;
    public GameObject hitZoneRed;
    public GameObject Snake;
    public ParticleSystem SnakeHitParticles;

    private SpriteRenderer hitZoneRedSprite;

    public float FadeDuration = 2f;
    public Color Color1;
    public Color Color2;
    public Color Color3;

    public float magnitude;

    float duration = 1.0f;

    float startTime;

    private Color startColor;
    private Color endColor;
    private Color hitColor;
    private float lastColorChangeTime;
    // Start is called before the first frame update
    void Start()
    {
        startColor = Color1;
        endColor = Color2;
        hitColor = Color3;
        hitZoneRedSprite = hitZoneRed.GetComponent<SpriteRenderer>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit == true)
        {
            if (hitVFX == true)
            {
                
                //SnakeHitParticles.Play();
                hitZoneRedSprite.color = Color.Lerp(endColor, hitColor, FadeDuration * 0.5f);
                //SnakeHitParticles.Play();
            }
            hitVFX = false;
            Destroy(GetComponent<Collider2D>(), 1.5f);
            Destroy(hitZoneRed, FadeDuration);
        }
        else
        {
            var ratio = (Time.time - lastColorChangeTime) / FadeDuration;
            ratio = Mathf.Clamp01(ratio);
            hitZoneRedSprite.color = Color.Lerp(startColor, endColor, ratio);
            //hitZoneRedSprite.color = Color.Lerp(startColor, endColor, Mathf.Sqrt(ratio)); // A cool effect
            //hitZoneRedSprite.color = Color.Lerp(startColor, endColor, ratio * ratio); // Another cool effect

            if (ratio == 1f)
            {
                lastColorChangeTime = Time.time;

                // Switch colors
                var temp = startColor;
                startColor = endColor;
                endColor = temp;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        hitVFX = true;
        Snake.GetComponent<SnakeStateScript>().hitPlus();
    }

}
