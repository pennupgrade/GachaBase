using System;
using UnityEngine;

public class SpriteBehavior : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite angry;
    public Sprite happy;

    public void changeToHappy()
    {
        spriteRenderer.sprite = happy;
    }

    public void changeToAngry()
    {
        spriteRenderer.sprite = angry;
    }

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = angry;
    }
}
