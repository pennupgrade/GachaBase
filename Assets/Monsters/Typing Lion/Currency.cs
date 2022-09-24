using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Currency : MonoBehaviour
{
    public TMP_Text currencyWord;
    public wpmText wpm;
    private int currWpm;
    private int currency;

    // Start is called before the first frame update
    void Start()
    {
        this.currencyWord.text = "Total coins gained: 0";
        this.currency = 0;
    }

    public void UpdateCurrency()
    {
        this.currWpm = wpm.getWPM();
        int add = 0;
        if (currWpm < 31) {
            add = 1;
        }
        else if (currWpm < 61) {
            add = 2;
        }
        else if (currWpm < 91) {
            add = 3;
        }
        else {
            add = 4;
        }
        this.currency += add;
        this.currencyWord.text = "Total coins gained: " + this.currency;
        CurrencyManager.Instance.Currency += add;
    }

}
