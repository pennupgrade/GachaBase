using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    int coinType; 

    public void GenerateCurrencyStack()
    {
        // This line adds 4 to our currency.
        CurrencyManager.Instance.Currency += 4;
    }

    public void SubtractCurrency() {
        // This line subtracts 1 to our currency.
        CurrencyManager.Instance.Currency -= 1;
    }

    public void GenerateCurrency() {
        // This line adds 1 to our currency.
        CurrencyManager.Instance.Currency += 1;
    }

    void Awake() {
        // Assign coinType
        // Read coinType from input

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // OnMouseDown
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null) {
                if (coinType == 0) {
                    GenerateCurrency();
                } else if (coinType == 1) {
                    GenerateCurrencyStack();
                } else if (coinType == 2) {
                    SubtractCurrency();
                } else {
                    // Throw error
                }
            }
        }
    }
}
