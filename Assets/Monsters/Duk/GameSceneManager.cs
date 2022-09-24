using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject obstacle;
    private float timer;
    private bool hasSpawned;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            hasSpawned = true;

            GameObject o1 = Instantiate(obstacle);
            o1.tag = "Obstacle";

            o1.transform.position = new Vector3(Random.Range(-10f, 10f), 5, 0);
            timer = 4f;
        }
    }
}
