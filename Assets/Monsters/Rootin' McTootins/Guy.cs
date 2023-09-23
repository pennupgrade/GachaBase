using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite IdleRight;
    public Sprite ShootingRight;
    public Sprite Dead;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = IdleRight;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (Draw.state == 2)) {
            spriteRenderer.sprite = ShootingRight;
        }
        if (Draw.state == 3) {
            spriteRenderer.sprite = Dead;
        }
        if (Draw.state == 0) {
            spriteRenderer.sprite = IdleRight;
        }
    }
}
