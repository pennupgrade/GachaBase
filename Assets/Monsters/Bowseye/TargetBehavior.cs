using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    int timer;
    bool clicked;
    int maxTime;
    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        clicked = false;
        maxTime = 200;
        gameOver = false;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if (maxTime > 0 && (timer >= maxTime || clicked)) {
            Spawn();
            timer = 0;
            clicked = false;
            maxTime -= 5;
            Debug.Log("maxTime" + maxTime);
        } else if (maxTime <= 0) {
            gameOver = true;
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    void OnMouseDown() {
        Debug.Log("target clicked");
        clicked = true;
        if (!gameOver) {
            CurrencyManager.Instance.Currency += 1;
        }
    }

    void Spawn()
    {
        float x = Random.Range(-6f,6f);
        float y = Random.Range(-4f,4f);
        Debug.Log("spawning at " + x + " " + y);
        gameObject.transform.position = new Vector3 (x, y, 0f);
    }
}
