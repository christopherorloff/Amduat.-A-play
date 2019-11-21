using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class Hour4_SceneManager : MonoBehaviour
{
    public TowBoatLogic TowScript;
    public PulseLight_Script Pulse;
    
    public ParticleSystem StartParticles;
    public SpriteRenderer GoddessIcon;
    public SpriteRenderer[] Goddesses;
    float fade = 0.15f;
    float slowFade = 0.07f;
    bool firstUpdate = true;

    void Start()
    {
        StartCoroutine(WaitAndStart());
        

    }

    void Update()
    {

        if (Scroll.scrollValue() > 0 && !TowScript.readyToPull && !Pulse.running)
        {
            Pulse.StartPulse = true;
        }

    }



    IEnumerator WaitAndStart()
    {

        print("vent");
        yield return new WaitForSeconds (2);
        StartParticles.Play();
        while (GoddessIcon.color.a > 0)
        {
            GoddessIcon.color = new Color(GoddessIcon.color.r, GoddessIcon.color.g, GoddessIcon.color.b, GoddessIcon.color.a - fade * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(FadeInGoddesses());


        StartParticles.Stop();

    }

    IEnumerator FadeInGoddesses()
    {

        while (Goddesses[0].color.a < 1 )
        {
            for (int i = 0; i < Goddesses.Length; i++)
            {
                Goddesses[i].color = new Color(Goddesses[i].color.r, Goddesses[i].color.g, Goddesses[i].color.b, Goddesses[i].color.a + fade * Time.deltaTime);

            }
            yield return null;

        }



    }

}
