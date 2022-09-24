using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleBehavior : MonoBehaviour
{
    public float decSpeed = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(0,-11,0);
    }

    void OnTriggerEnter2D(Collider2D c) {
        gameObject.transform.position = new Vector3(0,-11,0);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && gameObject.transform.position.y < -2) {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + .5f, gameObject.transform.position.z);
        }
        if (gameObject.transform.position.y >= -11) {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - decSpeed * (Time.deltaTime), gameObject.transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
