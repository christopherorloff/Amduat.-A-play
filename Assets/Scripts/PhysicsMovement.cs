using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class PhysicsMovement : MonoBehaviour
{
    public float xSpeed = 1;
    public float ySpeed = 1;
    public float rotateSpeed = 1;
    public bool moveOnX;
    public bool moveOnY;
    public bool rotate;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (moveOnX) rb.AddForce(new Vector2(Scroll.scrollValueAccelerated() * Time.deltaTime * xSpeed, 0));
        if (moveOnY) rb.AddForce(new Vector2(0, Scroll.scrollValueAccelerated() * Time.deltaTime * ySpeed));
        if (rotate) rb.AddTorque(Scroll.scrollValueAccelerated() * Time.deltaTime * rotateSpeed);

        transform.position += new Vector3(Scroll.scrollValueAccelerated() * xSpeed, 0);
    }
}
