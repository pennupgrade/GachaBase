using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    private float beatTempo = 120;
    //Typical time scale is 120 bpm, divide by 60 to get beats per second
    //2 beats per second = 2 places per second. 

    public bool hasStarted;
    
    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60f; //Gives us beats per second. 

    }

    // Update is called once per frame
    void fixedUpdate()
    {
        if (!hasStarted) {
            
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f); // Moves the arrow down per second by time.deltatime. 
        }
    }
}
