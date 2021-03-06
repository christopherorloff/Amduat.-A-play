using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveFadeHover_Script_hour5 : MonoBehaviour
{
    SpriteRenderer[] sprites;
    bool active = false;
    float y;
    public ParticleSystem graveEffect;

    void Start()
    {
        y = transform.position.y;
        sprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 0);
        }
    }


    public void StartFadeIn(float duration)
    {
        SoundManager.Instance.PlayGodAppear();
        SoundManager.Instance.PlayApopisTheme();
        SoundManager.Instance.snakesHissInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        StartCoroutine(FadeInSprite(duration));
        active = true;
    }

    IEnumerator FadeInSprite(float duration)
    {
        float startTime = Time.time;
        float t = 0;
        graveEffect.Play(); 
        while (t < 1)
        {
            t = (Time.time - startTime) / duration;
            t = Mathf.Clamp(t, 0, 1);
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, t);
            }
            yield return null;
        }
    }
}
