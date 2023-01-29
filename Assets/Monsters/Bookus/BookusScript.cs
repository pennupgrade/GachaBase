using System;
using UnityEngine;
using TMPro;

public class BookusScript : MonoBehaviour
{
    public TMP_Text mainText;
    private string currentText;
    private string currentColor;
    public SpriteBehavior bookus;
    public TimerBehavior timer;
    
    private string[] colorText = { "red", "yellow", "green", "blue", "purple" };
    private string[] colorCode = { "#ff3041", "#f5e340", "#53d67a", "#4090ff", "#b369ff" };

    private int stringIndexOf(string[] arr, string str)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == str)
            {
                return i;
            }
        }
        return -1;
    }

    private void randomizeMainText()
    {
        currentText = colorText[UnityEngine.Random.Range(0, 5)];
        currentColor = colorCode[UnityEngine.Random.Range(0, 5)];
        mainText.text = "<color=" + currentColor + ">" + currentText + "</color>";
    }

    private void loseRound()
    {
        randomizeMainText();
        CurrencyManager.Instance.Currency -= 3;
        bookus.changeToHappy();
        timer.reset();
    }

    private void winRound()
    {
        randomizeMainText();
        CurrencyManager.Instance.Currency += 1;
        bookus.changeToAngry();
        timer.reset();
    }

    void Start()
    {
        randomizeMainText();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (stringIndexOf(colorCode, currentColor) == stringIndexOf(colorText, currentText))
            {
                winRound();
            } else
            {
                loseRound();
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (stringIndexOf(colorCode, currentColor) != stringIndexOf(colorText, currentText))
            {
                winRound();
            } else
            {
                loseRound();
            }
        }

        if (timer.getTimer() <= 0f)
        {
            loseRound();
            timer.reset();
        }
    }
}
