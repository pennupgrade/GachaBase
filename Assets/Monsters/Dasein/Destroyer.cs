using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destr());
    }
    private IEnumerator Destr(){
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}