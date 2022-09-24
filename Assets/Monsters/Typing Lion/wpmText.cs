using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class wpmText : MonoBehaviour
{
    public TMP_Text wpmWord;
    public TypingLion lion;
    private int wpm;

    // Start is called before the first frame update
    void Start()
    {
        this.wpmWord.text = "WPM: 0";
    }

    public void UpdateWPMText()
    {
        this.wpm = (int)((lion.getCharCount() / 5) / (Time.time / 60));
        this.wpmWord.text = "WPM: " + this.wpm;
    }

    public int getWPM()
    {
        return this.wpm;
    }
}
