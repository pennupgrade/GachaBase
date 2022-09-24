using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DukScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int speed;
    public Sprite right;
    public Sprite left;
    public Sprite rest;

    public GameObject cash;
    public float randomX;

    public GameObject obj;
    private bool notStunned = true;
    private float timer = 2f;

    void spawnCash()
    {
        randomX = Random.Range(-10, 10);

        if (transform.position.x <= 0) //just spawn to the right
        {
            randomX = Random.Range(
                Mathf.Min(transform.position.x + 1f, 9.5f), // dont spawn outside
                10);
        }
        if (transform.position.x > 0) //just spawn to the left
        {
            randomX = Random.Range(
                -10,
                Mathf.Max(transform.position.x - 1f, -9.5f) // dont spawn outside
                );
        }

        obj.transform.position = new Vector3(randomX, -3.7f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Obstacle")
        {
            collision.gameObject.SetActive(false);
            notStunned = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spawnCash();
        CurrencyManager.Instance.Currency += 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //render sprite
        spawnCash();
    }   

    // Update is called once per frame
    void Update()
    {
        /*   
        if (Vector3.Distance(transform.position, obj.transform.position) < 1.7f)
        {
            spawnCash();
            CurrencyManager.Instance.Currency += 2;
        }
        */

        speed = 0; //speed is initially zero
        spriteRenderer.sprite = rest;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;


        if (notStunned)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                spriteRenderer.sprite = left;
                rest = left;
                speed = -10;

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                spriteRenderer.sprite = right;
                rest = right;
                speed = 10;
            }
        }
        else
        {
            if(timer <= 0)
            {
                notStunned = true;
                timer = 2f;
            }
            timer -= Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }


        transform.position += new Vector3(speed * Time.deltaTime, 0, 0); //move the sprite

        if (transform.position.x < -10)
        {
            transform.position = new Vector3(-10, -3.5f, 0); //correct
        }
        if (transform.position.x > 10)
        {
            transform.position = new Vector3(10, -3.5f, 0);
        }
    }
}
