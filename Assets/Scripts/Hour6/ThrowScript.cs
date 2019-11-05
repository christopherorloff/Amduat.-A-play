using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{

    private IEnumerator throwKnifeMovement;
    private IEnumerator bounceKnife;

    private Vector3 target;

    [SerializeField]
    private float speedScalar;
    public GameObject BloodEffect;
    public KnifeSpawner knifeSpawner;

    private float zRotation;
    private float rotationSpeed = 2350;

    //flags
    bool isThrown = false;


    private void Start()
    {
        bounceKnife = BounceKnife(1);
        BloodEffect = GameObject.FindGameObjectWithTag("Bloodeffect");
        knifeSpawner = FindObjectOfType<KnifeSpawner>().GetComponent<KnifeSpawner>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitZone"))
        {
            StopCoroutine(throwKnifeMovement);
            KnifeHit();
            
            this.transform.parent = other.transform;
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this, 2);

        }
        else if (other.CompareTag("Knife"))
        {
            print("Bounce knife");
            //FindObjectOfType<KnifeSpawner>().SpawnKnife();
            //Destroy(this, 0);
            //SoundManager.Instance.knifeClangInstance.start();
        }
        else if (other.CompareTag("Snake"))
        {
            StopCoroutine(throwKnifeMovement);
            print("Bounce knife");
            knifeSpawner.SpawnKnife();
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());
            StartCoroutine(bounceKnife);
            Destroy(this,2);
            //SoundManager.Instance.knifeClangInstance.start();
        }

    }

    public void ThrowKnife(Vector3 target)
    {
        if (!isThrown)
        {
            SoundManager.Instance.knifeThrowInstance.start();
            SoundManager.Instance.showdownMuInstance.setParameterByName("Intensity", 1);

            print("throw!");
            target -= this.transform.position;
            // target override. Change later
            target = Vector3.down;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, target);

            throwKnifeMovement = ThrowKnifeMovement(target.normalized);
            StartCoroutine(throwKnifeMovement);
            isThrown = true;
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
        Instantiate(BloodEffect, this.transform.position, Quaternion.identity);
        SoundManager.Instance.knifeHitInstance.start();
        EventManager.knifeHitEvent();
        Destroy(GameObject.Find("KnifehitBlood(clone)"), 2);

    }

    private IEnumerator BounceKnife(float time)
    {
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
