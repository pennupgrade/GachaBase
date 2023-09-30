using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        loseCoins();
        Destroy(collision.gameObject.transform.parent.gameObject);
    }
    void loseCoins() {
        Debug.Log("Coins lost");
        CurrencyManager.Instance.Currency = CurrencyManager.Instance.Currency - 2;
    }
}
