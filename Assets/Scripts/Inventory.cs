using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    protected float currency;

    //GameObject column contains all of the possible ones in the bank (maybe even ref it?)
    //Bools are all initially set to false; updated with each roll
    private Dictionary<GameObject, bool> owned;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //change how much money we have
    public void updateCurrency(float amount)
    {
        this.currency += amount;
    }

}
