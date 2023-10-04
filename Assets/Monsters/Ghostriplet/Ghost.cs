using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    //Ghosts are 0, 1, 2 from left to center to right
    public SpriteRenderer spriteRenderer0;
    public SpriteRenderer spriteRenderer1;
    public SpriteRenderer spriteRenderer2;

 
    public Sprite Ghost0;
    public Sprite Ghost1;
    public Sprite Ghost2;

    public Sprite GhostGood;
    public Sprite GhostBad;
    public Sprite GhostRainbow;

    public int key; 
    private int goodNum;
    private bool NoOther; 
    public static int level; 
    //randomly cboose the good ghost 
    void ChooseGoodNum()
    {
        goodNum = Random.Range(0,3);
    }

    //detect a mouse click on one of the ghosts
    void OnMouseDown() {
        if (NoOther) 
        {
            print("clicked!"); 
            NoOther = false;
            if (level > 4) {
                print("all rainbow now");
                RevealAllRainbow(); 
                CurrencyManager.Instance.Currency +=3;
            }
             
            else {
                RevealClicked(); //show clicked ghost
                Invoke(nameof(RevealAll), 0.5f); //show all ghosts
                if (goodNum == key) 
                {
                    print("guessed right!");
                    // correct ++; 
                    // print("correct added" + correct);
                    level = level+1;
                    print("level " + level);
                    CurrencyManager.Instance.Currency += 5;
                }
                else 
                {
                    print("guessed wrong!");
                    CurrencyManager.Instance.Currency -= (1);
                }
            }
            
            
            Invoke(nameof(Refresh), 1.2f); //restart
        } 
        NoOther = true; 
    }

    public void Refresh() 
    {
        spriteRenderer0.sprite = Ghost0;
        spriteRenderer1.sprite = Ghost1;
        spriteRenderer2.sprite = Ghost2;
        ChooseGoodNum(); 
        // print("new game");
    }

    public void RevealClicked() //***simplify latr
    {
        if (key==0) 
        {
            if (key==goodNum) {spriteRenderer0.sprite = GhostGood;}
            else {spriteRenderer0.sprite = GhostBad;}
        }
        else if (key==1) 
        {
            if (key==goodNum) {spriteRenderer1.sprite = GhostGood;}
            else {spriteRenderer1.sprite = GhostBad;}
        }
        else if (key==2) 
        {
            if (key==goodNum) {spriteRenderer2.sprite = GhostGood;}
            else {spriteRenderer2.sprite = GhostBad;}
        }
    }

    public void RevealAll() 
    {
        // spriteRenderer0.sprite = GhostRainbow;
        // spriteRenderer1.sprite = GhostRainbow;
        // spriteRenderer2.sprite = GhostRainbow;
        spriteRenderer0.sprite = GhostBad;
        spriteRenderer1.sprite = GhostBad;
        spriteRenderer2.sprite = GhostBad;

        if (goodNum==0) { spriteRenderer0.sprite = GhostGood; }
        if (goodNum==1) { spriteRenderer1.sprite = GhostGood; }
        if (goodNum==2) { spriteRenderer2.sprite = GhostGood; }
    }

    public void RevealAllRainbow() 
    {
        spriteRenderer0.sprite = GhostRainbow;
        spriteRenderer1.sprite = GhostRainbow;
        spriteRenderer2.sprite = GhostRainbow;
        // spriteRenderer0.sprite = GhostGood;
        // spriteRenderer1.sprite = GhostGood;
        // spriteRenderer2.sprite = GhostGood;
    }

    


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer0.sprite = Ghost0;
        spriteRenderer1.sprite = Ghost1;
        spriteRenderer2.sprite = Ghost2;
        print("Start is run");
        //correct = 0;
        ChooseGoodNum();
        NoOther = true; 
        level = 0;

    }
}

