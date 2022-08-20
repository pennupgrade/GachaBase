using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    //public CurrencyManager currencyManager;
    // Start is called before the first frame update
    public string name;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void updateGUI()
    {
        GameObject.FindGameObjectWithTag("GUI").GetComponent<Text>().text = "Current Funds: " + CurrencyManager.Instance.Currency;
    }


}
