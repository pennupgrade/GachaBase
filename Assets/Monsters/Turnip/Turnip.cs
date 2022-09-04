using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnip : MonoBehaviour
{
    //  1
    // 0 2
    //  3
    private int dir = 0;
    private SpriteRenderer spriteRenderer;

    public Sprite left;
    public Sprite up;
    public Sprite right;
    public Sprite down;

    // pick some random direction
    void changeDir()
    {
        dir = Random.Range(0, 4);

        // change the sprite to reflect that direction
        switch (dir) {
            case 0: // left
                spriteRenderer.sprite = left;
                break;
            case 1: // up
                spriteRenderer.sprite = up;
                break;
            case 2: // right
                spriteRenderer.sprite = right;
                break;
            case 3: // down
                spriteRenderer.sprite = down;
                break;

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        changeDir();
    }



    // Update is called once per frame
    void Update()
    {
        // detect a key press and make it matches the direction of the turnip and incremement currency if so
        if (Input.GetKeyDown(KeyCode.LeftArrow) && dir == 0)
        {
            CurrencyManager.Instance.Currency += 2;
            changeDir();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && dir == 1)
        {
            CurrencyManager.Instance.Currency += 2;
            changeDir();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && dir == 2)
        {
            CurrencyManager.Instance.Currency += 2;
            changeDir();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && dir == 3)
        {
            CurrencyManager.Instance.Currency += 2;
            changeDir();
        }

    }
}
