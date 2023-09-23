using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static int counter = 0;
    public int limitPosX = 12;
    public int limitNetX = 9;
    public int limitY = 4;
 
    public GameObject enemy;

    // Update is called once per frame
    void Update() {   
        Vector3 vec = new Vector3(Random.Range(limitNetX, limitPosX), 
                                  Random.Range(-limitY, limitY), 0);
        if (counter < 5) {
            counter++;
            Instantiate(enemy, vec, Quaternion.identity);
        }
    }

    public static void DestroyMe() {
        counter--;
    }

}
