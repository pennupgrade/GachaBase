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
        
        Invoke("DestroyCoin", 10f);
    }

    void DestroyCoin() {
        Destroy(gameObject);
        Debug.Log("Obj destroyed.");
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        coinType = Random.Range(0, 3);
        if (coinType == 0) {
            spriteRenderer.sprite = coinSprite;
            coinNum = 1;
        } else if (coinType == 1) {
            spriteRenderer.sprite = coinStackSprite;
            Vector2 sizeIncre = new Vector2(4f, 4f);
            this.GetComponent<BoxCollider2D>().size += sizeIncre;
            coinNum = 4;
        } else {
            spriteRenderer.sprite = ghostCoinSprite;
            Vector2 sizeIncre = new Vector2(4f, 4f);
            this.GetComponent<BoxCollider2D>().size += sizeIncre;
            coinNum = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // OnMouseDown
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider == this.GetComponent<Collider2D>()) {
                GenerateCurrency(coinNum);
                Destroy(gameObject);
            }
        }
        //move 
        this.transform.position += Time.deltaTime * dir * 0.5f;
    }
}
