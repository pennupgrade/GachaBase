using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float timer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawned");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) { Destroy(gameObject); }
    }
}
