using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    private string name;
    private int serialNum;

    //Contain the behavior that allows you to make money from this creature
    public abstract void GenerateCurrency();
}
