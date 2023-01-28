using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimbBearScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public float jumpAmount = 10;

    public void GenerateCurrency()
    {
        CurrencyManager.Instance.Currency += 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.magnitude == 0)
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            GenerateCurrency();
        }
    }

}
