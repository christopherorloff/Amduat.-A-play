using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoddessesFaceBoatFollower : MonoBehaviour
{
    public GameObject boat;
    public float followSpeed;
    private Vector3 velocity;

    void Update()
    {
        Vector3 target = new Vector3(boat.transform.position.x, transform.position.y, boat.transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, followSpeed * Time.deltaTime);
    }
}
