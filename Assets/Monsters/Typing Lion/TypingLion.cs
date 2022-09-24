using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// wpm monster
// 0 - 30 wpm -> sprite asleep
// 31 - 60 wpm -> sprite neutral
// 61+ wpm -> sprite happy
// simplified version of the game:
// show word on screen
// if word properly completed, play good sound
// else play bad sound
// give coin based on count of consec. correct word
public class TypingLion : MonoBehaviour {
    public TMP_Text word;
    // pointer that goes from 0 to word.Length - 1
    public int p;
    private string[] lines;
    private string oWord;
    private string[] strarr;
    public wpmText wpm;
    public Currency coins;
    private int charCount;
    

    // Start is called before the first frame update
    void Start() {
        this.lines = System.IO.File.ReadAllLines("./Assets/Monsters/Typing Lion/WordList.txt");
        this.charCount = 0;
        NewWord();
    }

    void NewWord() {
        this.p = 0;
        int r = UnityEngine.Random.Range(0, lines.Length - 1);
        string line = lines[r];
        this.word.text = line;
        this.strarr = new string[this.word.text.Length];
        for (int i = 0; i < this.word.text.Length; i++) {
            this.strarr[i] = "" + this.word.text[i];
        }
        this.oWord = line;
    }

    void UpdateWordText() {
        string temp = "";
        for (int i = 0; i < this.strarr.Length; i++) {
            temp += this.strarr[i];
        }
        this.word.text = temp;
    }

    public int getCharCount() {
        return this.charCount;
    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKeyDown) {
            string c = Input.inputString;
            if (c.Length == 1) {
                // cursed
                if (c[0] == this.oWord[p]) {
                    this.strarr[p] = "<color=green>" + oWord[p] + "</color>";
                    p++;
                    if (p > this.oWord.Length - 1) {
                        this.charCount += this.oWord.Length;
                        wpm.UpdateWPMText();
                        coins.UpdateCurrency();
                        NewWord();
                    }
                }
                else {
                    this.strarr[p] = "<color=red>" + this.oWord[p] + "</color>";
                }
                UpdateWordText();
            }
        }
    // Checks if mouse is clicked
    //if (Input.GetMouseButtonDown(0))
    //{
    //    // Checking if the click will hit this monster's box collider 

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    //Debug.Log(ray);
    //    RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
    //    if (hit.collider != null)
    //    {
    //        Debug.Log("Clicked");
    //        GenerateCurrency();
    //    }
    //}
    }
}
