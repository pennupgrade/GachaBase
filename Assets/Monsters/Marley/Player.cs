using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using coins = CurrencyManager.Instance.Currency;
using posX = transform.position.x;
using posY = transform.position.y;

// This Script controlls player input behavior
public class Player : MonoBehaviour
{
    /* 
    limitX: Halved horizontal limit from 0.
    limitY: Halved Vertical limit from 0.
    speed: Speed of the rocket.
    offset: Puts projectile forward.
    */
    const float limitX, limitY, speed, offset;
    limitY = 4.5f; 
    limitX = 8f; 
    speed = 3f;
    offset = 0.8f;

    const

    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {   
        playerMovement();
        if (Input.GetMouseButtonDown || Input.GetKeyDown(KeyCode.Space))
            spawnProjectile();
    }

    void spawnProjectile() {
        Vector 3 vec = new Vector 3(posX + offset, posY, 0);
        Instantiate(projectile, vec, Quaternion.identity);
    }

    /* 
    Moves the Player token if WASD is detected.
    Input: None. OutPut: None
     */
    void playerMovement() {
        // UP
        if (Input.GetKey(KeyCode.W) && posY <= limitY)
            transform.Translate(0, speed * Time.deltaTime, 0);
        // LEFT
        if (Input.GetKey(KeyCode.A) && posX >= -limitX)
            transform.Translate(- speed * Time.deltaTime, 0, 0);
        // DOWN
        if (Input.GetKey(KeyCode.S) && posY >= -limitY)
            transform.Translate(0, - speed * Time.deltaTime, 0);
        // RIGHT
        if (Input.GetKey(KeyCode.D) && posX <= limitX)
            transform.Translate(speed * Time.deltaTime, 0, 0);        
    }

    /* 
    Removes a Coin if an enemy is intercepted.
    Input: Collision container. Output: None.
     */
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") lessCoin(); 
    }

    /* Currency Instance Handlers */
    private void moreCoin() { coins++; }
    private void lessCoin() { if (coins > 0) coins--; }
}
