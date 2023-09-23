using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite back;
    public Sprite win;
    public Sprite lose;
    bool gamble = false;
    void unCardFlip()
    {
        int card = Random.Range(0, 3);
        switch (card)
        {
            case 0: //win
                CurrencyManager.Instance.Currency += 5;
                spriteRenderer.sprite = win;
                break;
            case 1: //tails
                CurrencyManager.Instance.Currency -= 2;
                spriteRenderer.sprite = lose;
                break;
            case 2: //lose
                CurrencyManager.Instance.Currency -= 2;
                spriteRenderer.sprite = lose;
                break;

        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (Input.GetKeyDown(KeyCode.Alpha1) && gamble){
            unCardFlip();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && gamble)
        {
            spriteRenderer.sprite = back;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gamble)
        {
            spriteRenderer.sprite = back;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gamble)
            {
                gamble = false;
            }
            else
            {
                gamble = true;
            }
        }
        spriteRenderer.enabled = gamble;
    }
    }
