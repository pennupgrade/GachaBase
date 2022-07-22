using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GachaBehavior : MonoBehaviour
{
    //List of Possible Monsters to Pull
    [SerializeField]
    private List<GameObject> monsterBank;

    //Reference to player info
    
    // Start is called before the first frame update
    void Start()
    {

    }

    //Called in start()? to fill the bank with all the possible types of monsters?
    void LoadGacha()
    {

    }


    // Update is called once per frame
    void Update()
    {
     
    }

    //Spits out a Random Monster from the bank
    void Roll()
    {
        //Check playerInfo to see if this monster is owned; if not, set it to true
    }

}
