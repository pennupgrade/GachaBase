using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GachaBehavior : MonoBehaviour
{

    private int numOfMonsters;


    [SerializeField]
    private Button cardButton;

    [SerializeField]
    private Image cardImage;

    [SerializeField]
    private int ROLL_COST;
    public int RollCost {
        get { return ROLL_COST; }
    } 

    //Reference to player info
    
    // Start is called before the first frame update
    void Start()
    {
        HideCard();
        LoadGacha();
    }

    //Called in start()? to fill the bank with all the possible types of monsters?
    void LoadGacha()
    {
        numOfMonsters = InventoryManager.Instance.Monsters.Count;
    }


    // Update is called once per frame
    void Update()
    {

    }

    //Spits out a Random Monster from the bank
    public void Roll()
    {

        Debug.Log(CurrencyManager.Instance);
        if (CurrencyManager.Instance.Currency < ROLL_COST) {
            Debug.Log("You don't have enough money for that.");
            return;
        }
        CurrencyManager.Instance.Currency -= ROLL_COST;

        //Fuck with probabilities here for rarer items hehe
        int roll = Random.Range(0, numOfMonsters);
        Debug.Log("You rolled a: " + roll);

        //Update the player's inventory directly from here

        MonsterAsset pulledMonster = InventoryManager.Instance.Monsters[roll];
        string pullName = pulledMonster.Name;
        ShowCard(pulledMonster.IconSprite);
        if (!InventoryManager.Instance.Owned[pullName])
        {
            Debug.Log("You pulled a: " + pullName);
            InventoryManager.Instance.Pull(pullName);
            //InventoryManager.Instance.Owned[pullName] = true;
            
            //GUIBehavior.instance.updateDropdown();
        } 
        else
        {
            Debug.Log("You already have a " + pullName);
        }
    }

    public void ShowCard(Sprite s) {
        cardImage.GetComponent<Image>().sprite = s;
        cardImage.enabled = true;
        cardImage.transform.rotation = Quaternion.Euler(90,0,0);
        
        cardButton.GetComponent<CanvasGroup>().alpha = 1;
        cardButton.GetComponent<CanvasGroup>().interactable = true;
    }
    public void HideCard() {
        cardImage.enabled = false;
        cardButton.GetComponent<CanvasGroup>().alpha = 0;
        cardButton.GetComponent<CanvasGroup>().interactable = false;
    }

}
