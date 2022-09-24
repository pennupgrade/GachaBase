using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3((float) Random.Range(-11, 11), Random.Range(-3.5f, 3.5f), 0);
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
