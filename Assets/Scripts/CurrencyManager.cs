using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{

    private static CurrencyManager instance;
    public static CurrencyManager Instance => CurrencyManager.instance;
    // Start is called before the first frame update
    void Start()
    {

    }

    [SerializeField]
    private float currency;
    public float Currency {
        get { return currency; }
        set { SetCurrency(value); }
    }
    public void SetCurrency(float f)
    {
        currency = f;
        //GetComponent<Text>().text = currency.ToString();
    }
    public void AddCurrency(float f)
    {
        SetCurrency(currency - f);
    }

    public void RemoveCurrency(float f)
    {
        SetCurrency(currency + f);
    }

    private void Awake()
    {
        if (CurrencyManager.instance != null && CurrencyManager.instance != this)
        {
            Destroy(this);
            //throw new System.Exception("An instance of this singleton already exists.");
        }
        else
        {
            CurrencyManager.instance = this;
        }
        //GetComponent<Text>().text = currency.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
