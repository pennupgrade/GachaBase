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

    public int key; 
    private int goodNum = 0;
    private int bonus = 0; 
    private bool NoOther; 
   
    //randomly cboose the good ghost 
    void ChooseGoodNum()
    {
        goodNum = Random.Range(0,3);
    }

    //detect a mouse click on one of the ghosts
    void OnMouseDown() {
        
        //goodNum = 1;
        // print("clicked yay!!!" + goodNum);
        
        RevealClicked(); //show clicked ghost
        if (NoOther) 
        {
            NoOther = false; 
            Invoke(nameof(RevealAll), 0.5f); //show all ghosts
            print("clicked key:" + key);
            print("gooodNum:" + goodNum);
            if (goodNum == key) 
            {
                print("true");
                //print("coins num " + (1 + bonus));
                CurrencyManager.Instance.Currency += (1 + bonus);
                bonus += 1;  
            }
            else {
                print("mismatch");
                bonus = 0;
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
        spriteRenderer0.sprite = GhostBad;
        spriteRenderer1.sprite = GhostBad;
        spriteRenderer2.sprite = GhostBad;

        if (goodNum==0) { spriteRenderer0.sprite = GhostGood; }
        if (goodNum==1) { spriteRenderer1.sprite = GhostGood; }
        if (goodNum==2) { spriteRenderer2.sprite = GhostGood; }
    }


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer0.sprite = Ghost0;
        spriteRenderer1.sprite = Ghost1;
        spriteRenderer2.sprite = Ghost2;
        bonus = 0;
        ChooseGoodNum();
        NoOther = true; 
    }
}

