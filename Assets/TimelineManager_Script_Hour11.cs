using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class TimelineManager_Script_Hour11 : Timeline_BaseClass
{
    public float timelineScalar = 0.8f;
    

    private GameObject Cam;
    private Vector3 camPosStart = new Vector3(0, 0, -10);
    private Vector3 camPosEnd = new Vector3(0, 0, -10);
    public float sceneLength;


    void Awake()
    {

        camPosEnd = new Vector3(sceneLength, 0, -10);
        Cam = Camera.main.gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float input = Scroll.scrollValueAccelerated();

        //Needs to be custom for each Hour --> must be implemented in specific hour instance of timeline_baseclass
        ConvertInputToProgress(input);

        CamAction();
    }

        private void ConvertInputToProgress(float input)
    {
        if (input > 0)
        {
            float speed = Scroll.scrollValueAccelerated(0.99999f) * timelineScalar * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 0.001f);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
            print("Speed: " + speed);

        }
    }


    private void CamAction()
    {
        Cam.transform.position = Vector3.Lerp(camPosStart, camPosEnd, Timeline);
    }    
}
