using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using coins = CurrencyManager.Instance.Currency;
using posX = transform.position.x;
using posY = transform.position.y;

// This Script controlls static enemy behavior
public class Enemy : MonoBehaviour
{
    /* 
    limitX: Halved horizontal limit from 0.
    speed: Speed of the enemy.
    */
    float speed = 2f;

    float limitX = -15f;

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
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    /* 
    Explodes if the Player is intercepted.
    Input: Collision container. Output: None.
     */
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player");
        // it should destroy itself
    }

    void redButton() { Destroy(gameObject); }
}
