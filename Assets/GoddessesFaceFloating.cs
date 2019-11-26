using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoddessesFaceFloating : MonoBehaviour
{
    public float speed = 1;
    public float xMagnitude = 1;
    public float yMagnitude = 1;
    public float ySizeFactor = 1;

    private float time;

    private Vector3 initSize;

    private void Start()
    {
        initSize = transform.localScale;
    }

    void Update()
    {
        time += Time.deltaTime;
        transform.localPosition = new Vector3(Mathf.Sin(time * speed) * xMagnitude, Mathf.Cos(time * speed) * yMagnitude, 0);
        transform.localScale = new Vector3(initSize.x * (Mathf.Cos(time * speed) * ySizeFactor + 1), initSize.y * (Mathf.Cos(time * speed) * ySizeFactor + 1), initSize.z * (Mathf.Cos(time * speed) * ySizeFactor + 1));
    }
}
