using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{


    public GameObject light1;
    public GameObject light2;
    public GameObject Effect;
    public GameObject ReadyEffect1;
    public GameObject EffectSpot;
    public GameObject BloodSpot;
    public SpriteRenderer Snake;
    public SpriteRenderer Panel;

    public bool running = false;


    public SpearAnimation SA;


    // Start is called before the first frame update
    void Start()
    {
        light1.transform.localScale = new Vector3(0f, 0, 1);
        light2.transform.localScale = new Vector3(0f, 0, 1);

        SA = FindObjectOfType<SpearAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        //instatiating the particle effect
        if (SA.startEffect)
        {
            Instantiate(Effect, BloodSpot.transform.position, Quaternion.Euler(270,0,0));
            SA.startEffect = false;
        }
        //Starting the growing of light1
        if (SA.animationDone == true && SA.growX == true)
        {
            print("light!!!");
            light1.transform.localScale += new Vector3(0.35f * Time.deltaTime, 0, 0);
            light2.transform.localScale += new Vector3(0.35f * Time.deltaTime, 0, 0);

            //stopping growing of light1
            if (light1.transform.localScale.x >= 1.5f)
            {
                SA.growX = false;

            }
        }
        //Starting the growing of light2
        if (SA.animationDone == true && SA.growY == true)
        {
            print("light!!!");
            light1.transform.localScale += new Vector3(0, 0.70f * Time.deltaTime, 0);
            light2.transform.localScale += new Vector3(0, 0.70f * Time.deltaTime, 0);

            //stopping growing of light2
            if (light1.transform.localScale.y >= 1.0f)
            {
                SA.growY = false;

            }
        }

        //changes the color of the snake when light has stopped growing
        if (!SA.growY && !SA.growX && SA.animationDone == true)
        {
            Snake.color = new Color(Snake.color.r + 0.005f, Snake.color.g, Snake.color.b - 0.005f * Time.deltaTime);

            if (!running)
            {
                print("startfade");
                StartCoroutine(startFadeUp());
            }
        }

        if (SA.startEffect2)
        {
            Instantiate(ReadyEffect1, EffectSpot.transform.position, Quaternion.Euler(-146, 110, -120));

            SA.startEffect2 = false;
        }

    }

    public IEnumerator startFadeUp()
    {
        running = true;
        float startTime = Time.time;
        while (Panel.color.a < 1)
        {
            print("vifader");
            float t = (Time.time - startTime) / 6;
            Color newColor = new Color(Panel.color.r, Panel.color.g, Panel.color.b, Mathf.Lerp(0, 1, t));
            Panel.color = newColor;
            yield return null;
        }

        GameManager.Instance.StartChangeToNextScene();


    }
}
