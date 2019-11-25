using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class BackGround_Object_Movement_Script : MonoBehaviour
{

    private float startAmplitudeX = 0f;
    private float startAmplitudeY = 0f;

    private float targetAmplitudeX;
    private float targetAmplitudeY;


    private float fadeInSpeed = 1.1f;

    public bool EaseIn = false;

    [SerializeField]
    [Range(-10, 10)]
    private float rotateSpeed;

    [SerializeField]
    [Range(0, 10)]
    private float amplitudeX;

    [SerializeField]
    [Range(0, 10)]
    private float frequenzyX;

    [SerializeField]
    [Range(0, 10)]
    private float amplitudeY;

    [SerializeField]
    [Range(0, 10)]
    private float frequenzyY;

    [SerializeField]
    private bool wheelControlled;

    [SerializeField]
    private bool rotating;

    [SerializeField]
    private bool floating;

    [SerializeField]
    [Range(0, 1)]
    private float scrollFactor;

    private Vector3 initialPosition;
    private float _time = 0;

    private void OnEnable()
    {
        initialPosition = transform.position;
        targetAmplitudeX = amplitudeX;
        targetAmplitudeY = amplitudeY;

    }

    // Update is called once per frame
    void Update()
    {


        _time += Scroll.scrollValueAccelerated() * scrollFactor;
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        if (EaseIn)
        {
            if (startAmplitudeX >= targetAmplitudeX)
            {
                startAmplitudeX *= fadeInSpeed;
                amplitudeX = startAmplitudeX;

            }
            else if (startAmplitudeX < targetAmplitudeX)
            {
                amplitudeX = targetAmplitudeX;
            }

            if (startAmplitudeY >= targetAmplitudeY)
            {
                startAmplitudeY *= fadeInSpeed;
                amplitudeY = startAmplitudeY;

            }
            else if (startAmplitudeY < targetAmplitudeY)
            {
                amplitudeY = targetAmplitudeY;
            }
        }

        if (rotating && wheelControlled)
        {
            transform.Rotate(new Vector3(0, 0, ((Scroll.scrollValueAccelerated() * scrollFactor) * rotateSpeed) + rotateSpeed));
        }
        else if (rotating && !wheelControlled)
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed));
        }
        else if (!rotating && wheelControlled)
        {
            transform.Rotate(new Vector3(0, 0, (Scroll.scrollValueAccelerated() * scrollFactor) * rotateSpeed));
        }

        if (floating && wheelControlled)
        {
            x = Mathf.Cos((_time * frequenzyX) ) * amplitudeX;
            y = Mathf.Sin((_time * frequenzyX)) * amplitudeY;
            transform.position = new Vector3(x, y, z) + initialPosition - new Vector3(x,y,0);
        }
        else if (floating && !wheelControlled)
        {
            x = Mathf.Cos(frequenzyX * Time.time) * amplitudeX;
            y = Mathf.Sin(Time.time * frequenzyY) * amplitudeY;
            transform.position = new Vector3(x, y, z) + initialPosition;
        }
        else if (!floating && wheelControlled)
        {  

            x = Mathf.Cos(_time * frequenzyX) * amplitudeX;
            y = Mathf.Sin(_time * frequenzyY) * amplitudeY;
            transform.position = new Vector3(x, y, z) + initialPosition - new Vector3(x, y, 0);
        }
    }

    public void StopAllMovement()
    {
    }
}
