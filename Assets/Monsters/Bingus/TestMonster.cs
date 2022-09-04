using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A simple test monster that will give you 1 currency when you click on it.
public class TestMonster : MonoBehaviour
{
    public void GenerateCurrency()
    {
        // This line adds 1 to our currency.
        CurrencyManager.Instance.Currency += 1;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Checking if the click will hit this monster's box collider 

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {
                Debug.Log("Clicked");
                GenerateCurrency();
            }
        }
    }
}
