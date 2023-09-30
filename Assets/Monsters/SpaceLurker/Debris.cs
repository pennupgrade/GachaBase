using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    float speed = 6;
    float lifetime = 60;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    { 
        direction = GameObject.FindGameObjectWithTag("Player").transform.position-transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        transform.Translate(direction.normalized * Time.deltaTime * speed);
        if (lifetime <= 0)
            Destroy(transform.parent.gameObject);
    }
}
