using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 10f;

    float limitX = 15f;

    // Update is called once per frame
    void Update()
    {
        projectileMovement();
        if (gameObject.transform.position.x >= limitX) redButton();
    }

    /* Controlls enemy movement */
    void projectileMovement() {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    /* 
    Explodes if the Player is intercepted.
    Input: Collision container. Output: None.
     */
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Obstacle") {
            AddCurrency();
            redButton();
        }
    }

    public void AddCurrency() {
        CurrencyManager.Instance.Currency++;
    }

    void redButton() { Destroy(gameObject); }
}
