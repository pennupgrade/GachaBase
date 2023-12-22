using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int health = 1;

    public void Damage(int amount)
    {
        this.health -= amount;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("I am Dead!");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
