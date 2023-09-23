using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This Script controlls static enemy behavior
public class Enemy : MonoBehaviour
{
    /* 
    limitX: Halved horizontal limit from 0.
    speed: Speed of the enemy.
    */
    float speed = -1.5f;

    float limitX = -9f;

    /* 
    Implement Spawn from a scene Controller
    float spawnX = 15f;
    float spawnY = 0f;
    */

    // Update is called once per frame
    void Update()
    {
        enemyMovement();
        if (gameObject.transform.position.x <= limitX) redButton();
    }

    /* Controlls enemy movement */
    void enemyMovement() {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    /* 
    Explodes if the Player is intercepted.
    Input: Collision container. Output: None.
     */
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Activator") redButton();
    }

    void redButton() { 
        EnemyManager.DestroyMe();
        Destroy(gameObject); 
    }
}
