using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessedDead_Movement_Hour8_Script : MonoBehaviour
{
    public Transform target;
    public Transform spawn;

    public float speed;
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (Vector2.Distance(target.transform.position, transform.position) < 1)
        {
            transform.position = spawn.position;
        }
    }
}
