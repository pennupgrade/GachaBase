using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelBehavior : MonoBehaviour
{

    private float yVal = 0f;

    void OnTriggerEnter2D(Collider2D c) {
        CurrencyManager.Instance.Currency += 1;
        yVal = Random.Range(-1f,3f);
        gameObject.transform.position = new Vector3 (-10f, yVal, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        yVal = Random.Range(-1f,3f);
        gameObject.transform.position = new Vector3 (-10f, yVal, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3 (gameObject.transform.position.x + .03f, gameObject.transform.position.y, gameObject.transform.position.z);
        if (gameObject.transform.position.x > 10f) {
            gameObject.transform.position = new Vector3 (-10f, yVal, 0f);
        }
    }
}
