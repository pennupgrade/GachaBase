using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    private int drawTime;
    private int enemyReflex;
    private int timer;
    private bool draw;
    private bool end;

    public static int state; //state 0 is Ready...; state 1 is Draw!; state 2 is Win; state 3 is Lose; state 4 is too early

    private SpriteRenderer spriteRenderer;

    public Sprite TextR;
    public Sprite TextD;
    public Sprite TextW;
    public Sprite TextL;
    public Sprite TextE;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        reset();
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + 1;

        if ((timer == drawTime) && (!end)) {
            spriteRenderer.sprite = TextD;
            draw = true;
            state = 1;
        }

        if ((Input.GetKeyDown(KeyCode.Space)) && (!end)) {
            if (draw) {
                spriteRenderer.sprite = TextW;
                CurrencyManager.Instance.Currency += 1;
                end = true;
                state = 2;
            } else {
                spriteRenderer.sprite = TextE;
                end = true;
                state = 4;
            }
        }

        if ((timer == enemyReflex) && (!end)) {
            spriteRenderer.sprite = TextL;
            end = true;
            state = 3;
        }

        if (end) {
            if (Input.GetKeyDown(KeyCode.R)) {
                reset();
            }
        }


    }

    private void reset() 
    {
        timer = 0;
        draw = false;
        end = false;
        drawTime = Random.Range(300, 1000);
        enemyReflex = drawTime + Random.Range(50, 100);
        spriteRenderer.sprite = TextR;
        state = 0;
    }

}
