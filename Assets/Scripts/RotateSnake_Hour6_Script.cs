using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSnake_Hour6_Script : MonoBehaviour
{
    [SerializeField]
    [Range(-50, 50)]
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime *rotateSpeed));

    }
}
