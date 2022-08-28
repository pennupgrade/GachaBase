using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GachaBehavior : MonoBehaviour
{
    //List of Possible Monsters to Pull
    [SerializeField]
    private List<GameObject> monsterBank;

    private int numOfMonsters;


    //Reference to player info
    
    // Start is called before the first frame update
    void Start()
    {
        LoadGacha();
    }

    //Called in start()? to fill the bank with all the possible types of monsters?
    void LoadGacha()
    {
        numOfMonsters = monsterBank.Count;
    }


    // Update is called once per frame
    void Update()
    {

    }

    //Spits out a Random Monster from the bank
    public void Roll()
    {
        //Fuck with probabilities here for rarer items hehe
        int roll = (int)(Random.Range(0, numOfMonsters - 1));
        Debug.Log("You rolled a: " + roll);

        //Update the player's inventory directly from here
        InventoryManager playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        string pullName = playerInventory.possibleMonsters[roll].name;
        if (!playerInventory.ownedMonsters[pullName])
        {
            Debug.Log("You pulled a: " + pullName);
            playerInventory.ownedMonsters[pullName] = true;

            GUIBehavior.instance.updateDropdown();
        } 
        else
        {
            Debug.Log("You already have a " + pullName);
        }

        playerInventory.updateGUI();
    }

}
