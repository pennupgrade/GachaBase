using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountererEnemyFollow : MonoBehaviour
{
    public GameObject counterer;
    public float speed;
    private bool canMove;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            distance = Vector2.Distance(transform.position,
                counterer.transform.position);
            Vector2 direction = counterer.transform.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position,
                counterer.transform.position, speed * Time.deltaTime);
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Counterer"))
        {
            Destroy(other.gameObject);
        }
    }
    */
}
