using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{

    private IEnumerator throwKnifeMovement;
    private IEnumerator bounceKnife;

    private Vector3 target;
    bool hit = false;

    [SerializeField]
    private float speedScalar;
    public GameObject BloodEffect;
    public GameObject LightEmergeEffect;
    public PulseLight_Script pulseLightScript;

    public KnifeSpawner knifeSpawner;

    public bool throwDone = false;
    private float zRotation;
    private float rotationSpeed = 2350;

    //flags
    bool isThrown = false;


    private void Start()
    {
        bounceKnife = BounceKnife(1);
        BloodEffect = GameObject.FindGameObjectWithTag("Bloodeffect");
        LightEmergeEffect = GameObject.FindGameObjectWithTag("LightEffect");
        pulseLightScript = FindObjectOfType<PulseLight_Script>();
        knifeSpawner = FindObjectOfType<KnifeSpawner>().GetComponent<KnifeSpawner>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitZone"))
        {
            //throwDone = true;
            hit = true;
            StopCoroutine(throwKnifeMovement);
            KnifeHit();
            
            this.transform.parent = other.transform;
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this, 2);
            

        }
        else if (other.CompareTag("Snake") && hit==false)
        {
            //throwDone = true;
            StopCoroutine(throwKnifeMovement);
            print("Bounce knife");
            knifeSpawner.SpawnKnife();
            StartCoroutine(bounceKnife);
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this,2);
            
            //SoundManager.Instance.knifeClangInstance.start();
        }

    }

    public void ThrowKnife(Vector3 target)
    {
        if (!isThrown)
        {
            
            SoundManager.Instance.PlayKnifeThrow();
            SoundManager.Instance.showdownMuInstance.setParameterByName("Intensity", 1);

            print("throw!");
            transform.parent = null;
            target -= this.transform.position;
            // target override. Change later
            target = Vector3.down;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, target);

            throwKnifeMovement = ThrowKnifeMovement(target.normalized);
            StartCoroutine(throwKnifeMovement);
            isThrown = true;
            throwDone = true;
        }
    }

    IEnumerator ThrowKnifeMovement(Vector3 target)
    {
        while (true)
        {
            this.transform.position += target * speedScalar * Time.deltaTime;
            yield return null;
        }
    }

    public void KnifeHit()
    {
        SoundManager.Instance.PlayKnifeHit();

        Instantiate(BloodEffect, this.transform.position, Quaternion.identity);
        Instantiate(LightEmergeEffect, this.transform.position, LightEmergeEffect.transform.rotation);
        EventManager.knifeHitEvent();
        Destroy(GameObject.Find("Hour6_Light_emmit_Snake_Knife_hit_particle(Clone)"), 4f);
        Destroy(GameObject.Find("Knife_HitBlood_Particle(Clone)"), 2);

    }

    private IEnumerator BounceKnife(float time)
    {
        SoundManager.Instance.PlayKnifeBounce();

        pulseLightScript.StartPulse = true;
        //Renderer renderer = GetComponent<Renderer>();
        float timer = time;
        Vector3 leftOrRight = new Vector3(Mathf.Sign(UnityEngine.Random.Range(-1, 1)), 0, 0);
        print(leftOrRight);
        while (timer > 0)
        {
            this.transform.position += (Vector3.up + leftOrRight).normalized * speedScalar * 0.7f * Time.deltaTime;
            zRotation = rotationSpeed * Time.deltaTime;
            this.transform.Rotate(new Vector3(0, 0, zRotation));
            timer -= Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
