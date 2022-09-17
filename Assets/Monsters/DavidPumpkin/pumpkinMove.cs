using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pumpkinMove : MonoBehaviour
{
    private Vector2 screenBound;
    private Vector3 destination;
    private Vector3 velocity;

    public Coin coin;

    private SpriteRenderer spriteRenderer;  
    public Sprite originalSprite;
    public Sprite throwSprite;
    // Start is called before the first frame update
    void Start()
    {
        screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        destination = new Vector3(Random.Range(-screenBound.x, screenBound.x), Random.Range(-screenBound.y, screenBound.y), 0);

        Invoke("throwCoin", 2);
    }

    // Update is called once per frame
    void Update()
    {
        //Move Randomly
        if (Vector3.Distance(this.transform.position, destination) <= 2.0) {
            destination.x = Random.Range(-screenBound.x, screenBound.x);
            destination.y = Random.Range(-screenBound.y, screenBound.y);
            velocity = destination - this.transform.position;
        } else {
            //velocity = destination - this.transform.position;
        }
        this.transform.position += velocity * Time.deltaTime * 0.1f;

        //


        //
    }

    void throwCoin() {
        //There will be modes of throwing, for now we just aim for the center
        Vector3 randomPos = new Vector3(-this.transform.position.x, -this.transform.position.y, 0f);
        GameObject newCoin = Instantiate(coin, this.transform);
        newCoin.dir = randomPos;
        newCoin.type = 0;
        //play animation/change sprite 
        spriteRenderer.sprite = throwSprite;
        Invoke("changeSprite", 0.5f);
        Invoke("throwCoin", 2.0f);
    }

    void changeSprite() {
        spriteRenderer.sprite = originalSprite;
    }
}
