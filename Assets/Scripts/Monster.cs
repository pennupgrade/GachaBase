using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    protected string name;

    [SerializeField]
    protected int serialNum;

    //Contain the behavior that allows you to make money from this creature
    public virtual void GenerateCurrency()
    {
        Debug.Log("Please Implement");
    }
}
