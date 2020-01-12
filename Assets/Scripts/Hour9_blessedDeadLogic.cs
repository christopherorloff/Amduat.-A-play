using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class Hour9_blessedDeadLogic : MonoBehaviour
{

    public Transform[] blessedDead;
    public ParticleSystem[] showSpawnsParticle;
    public GameObject[] masks;
    public FadeUIScript fadeScript;
    public int currentBlessed = 0;
    public float animationDuration = 1.5f;
    public float yMax = 1;
    private int maxBoatsSpawned = 17;
    public bool running = false;
    public bool stopEffect = false;
    public Transform Target;
    public GameObject Boat;
    public ParticleSystem particle;

    private bool boatPaddlePlaying;
    private bool musicPlaying;


    void Update()
    {
        if (!running && Scroll.scrollValue() < 0 && currentBlessed < blessedDead.Length && fadeScript.sceneReady == true)
        {
            StartCoroutine(RaiseBlessedDead(blessedDead[currentBlessed]));
        }
    }


    private IEnumerator RaiseBlessedDead(Transform BlessedGO)
    {
        running = true;

        SoundManager.Instance.PlayBlessedDeadAppearBoat();

        if (!musicPlaying)
        {
            SoundManager.Instance.PlayRitualTheme();
            musicPlaying = true;
        }

        float offset = BlessedGO.GetComponent<SpriteRenderer>().bounds.max.y;
        Instantiate(particle, new Vector3(BlessedGO.position.x, offset), particle.transform.rotation);
        showSpawnsParticle[currentBlessed].Stop();
        float startTime = Time.time;
        while (BlessedGO.localPosition.y < yMax)
        {
            float t = (Time.time - startTime) / animationDuration;
            BlessedGO.localPosition = new Vector3(BlessedGO.transform.localPosition.x, Mathf.SmoothStep(BlessedGO.localPosition.x, yMax, t), 0);
            yield return null;
        }

        currentBlessed++;
        if (currentBlessed >= maxBoatsSpawned)
        {

            stopEffect = true;

            for (int i = 0; i < masks.Length; i++)
            {
                Destroy(masks[i]);
            }

            for (int i = 0; i < blessedDead.Length; i++)
            {
                StartCoroutine(MoveCharacters(blessedDead[i].transform, Target.transform, 10f, false));
                if (!boatPaddlePlaying)
                {
                    SoundManager.Instance.PlayBoatPaddle();
                    SoundManager.Instance.PlayBlessedDeadBoatMove();
                    boatPaddlePlaying = true;
                }
            }


            StartCoroutine(MoveCharacters(Boat.transform, Target.transform, 10f, true));

        }

        running = false;
    }

    IEnumerator MoveCharacters(Transform fromPos, Transform toPos, float duration, bool last)
    {

        print("coroutine");
        //float counter = 0;
        Vector3 startPos = fromPos.position;
        Vector3 endPos = toPos.position;
        float t = 0;
        float startTime = Time.time;

        while (t < 1)
        {
            t = (Time.time - startTime) / duration;
            t = Mathf.SmoothStep(0, 1, t);
            t = Mathf.Clamp(t, 0, 1);
            fromPos.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        if (last)
        {
            StartCoroutine(fadeScript.FadeSpriteCoroutineUp(1, 2));
        }

    }
}
