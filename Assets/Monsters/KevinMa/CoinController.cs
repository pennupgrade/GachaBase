using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    public int coinCount;
    public GameObject coinTextMesh;
    private TextMeshPro coinText;


    void Start()
    {
        coinCount = 10;
        coinText = coinTextMesh.GetComponent<TextMeshPro>();
        coinText.SetText(coinCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
