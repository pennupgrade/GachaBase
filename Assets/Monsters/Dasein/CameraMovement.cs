using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float rotationS, maxR;
    [SerializeField] private float rotationA;
    private bool started;
    // Start is called before the first frame update
    void Start()
    {
        rotationA=0;rotationS=0; maxR = 3.33f; started=false;
    }

    public void ToggleRotation(){
        started=!started;
        rotationA=1;
    }

    // Update is called once per frame
    void Update()
    {   
        if(started){
            var zed=transform.eulerAngles.z;
            if(zed>180)zed-=360;
            if(zed<-3) rotationA=1;
            else if(zed>3) rotationA=-1;
            rotationS+=rotationA*Time.deltaTime;
            if(rotationS>maxR) rotationS=maxR;
            if(rotationS<-maxR) rotationS=-maxR;
            transform.Rotate(Vector3.forward, rotationS * Time.deltaTime);
        }
    }
}
