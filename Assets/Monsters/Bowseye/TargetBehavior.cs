using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    int timer;
    bool clicked;
    int maxTime;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hi");
        timer = 0;
        clicked = false;
        maxTime = 200;
        score = 0;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if (timer >= maxTime || clicked) {
            if (!clicked) {
                missed += 1;
            }
            Spawn();
            timer = 0;
            clicked = false;
            maxTime -= 5;
            Debug.Log("maxTime" + maxTime);
        }
    }

    void OnMouseDown() {
        Debug.Log("target clicked");
        clicked = true;
        score += 1;
        Debug.Log(score);
    }

    void Spawn()
    {
        float x = Random.Range(-6f,6f);
        float y = Random.Range(-4f,4f);
        Debug.Log("spawning at " + x + " " + y);
        gameObject.transform.position = new Vector3 (x, y, 0f);
    }
}
