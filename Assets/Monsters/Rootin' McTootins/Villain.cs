using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite VillainIdle;
    public Sprite VillainShoot;
    public Sprite VillainDead;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = VillainIdle;
    }

    // Update is called once per frame
    void Update()
    {
        if (Draw.state == 2) {
            spriteRenderer.sprite = VillainDead;
        }
        if (Draw.state == 3) {
            spriteRenderer.sprite = VillainShoot;
        }
        if (Draw.state == 0) {
            spriteRenderer.sprite = VillainIdle;
        }
    }
}
