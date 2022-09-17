using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Vector3 dir;
    public int coinType; 
    private int coinNum;
    public SpriteRenderer spriteRenderer;
    public Sprite coinSprite;
    public Sprite coinStackSprite;
    public Sprite ghostCoinSprite;

    public void GenerateCurrency(int coinNum) {
        if (CurrencyManager.Instance.Currency + coinNum >= 0) {
            CurrencyManager.Instance.Currency += coinNum;
        }
    }

    void Awake() {
        // Assign coinType
        // Read coinType from input
        if (coinType == 0) {
            spriteRenderer.sprite = coinSprite;
            coinNum = 1;
        } else if (coinType == 1) {
            spriteRenderer.sprite = coinStackSprite;
            coinNum = 4;
        } else {
            spriteRenderer.sprite = ghostCoinSprite;
            coinNum = -1;
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
        // OnMouseDown
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null) {
                GenerateCurrency(coinNum);
            }
            Destroy(gameObject);
        }
        //move 
        this.transform.position += Time.deltaTime * dir * 0.5f;
    }
}
