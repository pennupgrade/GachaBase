using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cereal : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite Knead1;

    public Sprite Knead2;

    public Sprite Knead3;

    public Sprite Knead4;

    public Sprite Biscuit;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private int numClicks = 0;

    void Kneading()
    {
        if (numClicks == 17)
        {
            spriteRenderer.sprite = Biscuit;
            CurrencyManager.Instance.Currency += 17;
            numClicks = 0;
        }
        else if (numClicks % 4 == 0)
        {
            spriteRenderer.sprite = Knead1; 
        }
        else if (numClicks % 4 == 1) 
        {
            spriteRenderer.sprite = Knead2;
        }
        else if (numClicks % 4 == 2)
        {
            spriteRenderer.sprite = Knead4;
        }
        else if (numClicks % 4 == 3)
        {
            spriteRenderer.sprite = Knead3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            numClicks++;
            Kneading();
        }
    }
}
