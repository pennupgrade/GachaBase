using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrenJiScript : MonoBehaviour
{
    public int counter;
    public int frameCounter;
    public Sprite zero;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    private SpriteRenderer spriteRenderer;
    public void GenerateCurrency()
    {
        CurrencyManager.Instance.Currency += 1;
    }

    // Start is called before the first frame update
    void Start()
        
    {
        counter = 0;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePicture();
        frameCounter++;
        
        if (Input.GetKeyDown("space") && (counter%4==3))
        {

            Debug.Log("pressed");
            GenerateCurrency();
        }
    }

    int PictureFrame()
    {
        if (frameCounter % 120 == 0)
            counter++;
        return counter% 4;
    }
    void UpdatePicture()
    {
        switch (PictureFrame())
        {
            case 0:
              spriteRenderer.sprite = zero;
                break;
            case 1:
                spriteRenderer.sprite = one;
                break;
            case 2:
                spriteRenderer.sprite = two;
                break;
            case 3:
                spriteRenderer.sprite = three;
                break;
        }
    }
}
