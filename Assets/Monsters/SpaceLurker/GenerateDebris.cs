using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDebris : MonoBehaviour
{
    public GameObject debris;
    float maxDelay = 10;
    float delay = 0;
    // Start is called before the first frame update
    void Start()
    {
        delay += Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            delay = maxDelay;
            GameObject g = Instantiate(debris);
            g.transform.position = transform.position;
        }
    }
}
