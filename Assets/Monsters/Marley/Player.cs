using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This Script controlls player input behavior
public class Player : MonoBehaviour {

    /* 
    limitX: Halved horizontal limit from 0.
    limitY: Halved Vertical limit from 0.
    speed: Speed of the rocket.
    offset: Puts projectile forward.
    */
    const float limitX  = 8f;
    const float limitY = 4.5f;
    const float speed = 3.5f;
    const float offset = 0.8f;

    [SerializeField] private GameObject projectile;
    [SerializeField] private AudioSource blaster;

    // Update is called once per frame
    void Update()
    {   
        playerMovement();
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            blaster.Play();
            spawnProjectile();
        }            
    }

    void spawnProjectile() {
        Vector3 vec = new Vector3(transform.position.x + offset, 
                                  transform.position.y, 0);
        Instantiate(projectile, vec, Quaternion.identity);
    }

    /* 
    Moves the Player token if WASD is detected.
    Input: None. OutPut: None
     */
    void playerMovement() {
        // UP
        if (Input.GetKey(KeyCode.W) && transform.position.y <= limitY)
            transform.Translate(0, speed * Time.deltaTime, 0);
        // LEFT
        if (Input.GetKey(KeyCode.A) && transform.position.x >= -limitX)
            transform.Translate(- speed * Time.deltaTime, 0, 0);
        // DOWN
        if (Input.GetKey(KeyCode.S) && transform.position.y >= -limitY)
            transform.Translate(0, - speed * Time.deltaTime, 0);
        // RIGHT
        if (Input.GetKey(KeyCode.D) && transform.position.x <= limitX)
            transform.Translate(speed * Time.deltaTime, 0, 0);        
    }

    /* 
    Removes a Coin if an enemy is intercepted.
    Input: Collision container. Output: None.
     */
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Obstacle") RemoveCurrency(1); 
    }

    /* Currency Instance Handlers */

    public void RemoveCurrency(int i) {
        if (CurrencyManager.Instance.Currency >= 0) {
            CurrencyManager.Instance.Currency--;
        }        
    }
}
