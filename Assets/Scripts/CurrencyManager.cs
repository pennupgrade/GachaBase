using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyManager : MonoBehaviour
{

    private static CurrencyManager instance;
    public static CurrencyManager Instance => CurrencyManager.instance;
    
    // Start is called before the first frame update
    void Start()
    {   
        GetComponent<TMP_Text>().text = currency.ToString();
    }

    [SerializeField]
    private float currency;
    public float Currency {
        get { return currency; }
        set { SetCurrency(value); }
    }
    
    public void SetCurrency(float f)
    {
        currency = Mathf.Max(0,f);
        GetComponent<TMP_Text>().text = currency.ToString();
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
    }
}
