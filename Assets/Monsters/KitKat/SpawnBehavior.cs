using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehavior : MonoBehaviour
{
    public GameObject paw;

    int timer = 0;
    int spawnInterval = 2080;
    int numToSpawn = 10;

    bool isSpawning = false;
    public static int deaths = 0;

    void Update()
    {
        if (SpawnBehavior.deaths == 4) {
            Application.Quit();
        }
        if (isSpawning && numToSpawn >= 0) {
            if (timer % spawnInterval == 0) {
                numToSpawn--;
                isSpawning = false;
                Spawn();
            }
            timer++;
        }
    }

    void Start() {
        timer = 0;
        isSpawning = true;
        numToSpawn = 10;
    }

    void Spawn() {
        Instantiate(paw, transform.position, Quaternion.identity);
    }
}
