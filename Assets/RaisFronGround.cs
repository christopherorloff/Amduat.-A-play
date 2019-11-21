using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisFronGround : MonoBehaviour
{

    public GameObject pilarFar;
    public GameObject pilarNear;

    public float raiseTime;

    public float endPosYNear;
    public float endPosYFar;
    // Start is called before the first frame update
    void Start()
    {
        float startPosYNear = pilarNear.transform.postition.y;
        float startPosYFar = pilarNear.tranform.postition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
