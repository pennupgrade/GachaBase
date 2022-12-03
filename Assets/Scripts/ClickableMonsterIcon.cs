using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickableMonsterIcon : MonoBehaviour
{
    public MonsterAsset monster;

    private bool unlocked;

    private SpriteRenderer spriteRenderer;

    public Vector3 activePosition;
    public float activeScaleFactor;
    private Vector3 activeScale;

    public Text description;
    public CanvasGroup uiCanvas;


    private Vector3 intialPosition;
    private Vector3 initialScale;

    private bool mousedOver;
    private bool hasFocus;


    private static List<ClickableMonsterIcon> monsterIcons = null;
    public static List<ClickableMonsterIcon> MonsterIcons => monsterIcons;

    public void Unfocus()
    {
        hasFocus = false;
    }

    public void Focus()
    {
        foreach (ClickableMonsterIcon m in monsterIcons)
        {
            m.Unfocus();
        }
        hasFocus = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.black;
        spriteRenderer.sprite = monster.TransparentSprite;
        description.text = monster.Description;

        uiCanvas.alpha = 0;

        intialPosition = transform.position;
        initialScale = transform.localScale;
        activeScale = new Vector3(activeScaleFactor, activeScaleFactor, activeScaleFactor);


        if (monsterIcons == null)
        {
            monsterIcons = new List<ClickableMonsterIcon>();
        }

        monsterIcons.Add(this);

    }


    // Update is called once per frame
    void Update()
    {
        if (InventoryManager.Instance.Owned[monster.Name])
        {
            UpdateSprite();
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {
            InventoryManager.Instance.Owned[monster.Name] = true;
        }

        if (!hasFocus) // does not have focus
        {
            // fade description out 
            uiCanvas.alpha = Mathf.Max(0, uiCanvas.alpha - Time.deltaTime*3);

            if (mousedOver && InventoryManager.Instance.Owned[monster.Name])
            {
                // slightly increase scale if moused over
                transform.position = Vector3.Lerp(transform.position, intialPosition, 0.01f);
                transform.localScale = Vector3.Lerp(transform.localScale, initialScale * 1.1f, 0.01f);
                
                if (Input.GetMouseButtonDown(0))
                {
                    Focus();
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, intialPosition, 0.01f);
                transform.localScale = Vector3.Lerp(transform.localScale, initialScale, 0.01f);

            }
        }
        else // has focus
        {
            // fade description in 
            uiCanvas.alpha = Mathf.Min(1, uiCanvas.alpha + Time.deltaTime*3);

            if (mousedOver && InventoryManager.Instance.Owned[monster.Name])
            {
                // slightly increase scale if moused over
                transform.position = Vector3.Lerp(transform.position, activePosition, 0.01f);
                transform.localScale = Vector3.Lerp(transform.localScale, activeScale * 1.1f, 0.01f);

                if (Input.GetMouseButtonDown(0)) // if you click away from monster, lose focus
                {
                    MainManager.Instance.loadNewLevel(monster.SceneName);
                }

            }
            else 
            {
                transform.position = Vector3.Lerp(transform.position, activePosition, 0.01f);
                transform.localScale = Vector3.Lerp(transform.localScale, activeScale, 0.01f);

                if (Input.GetMouseButtonDown(0)) // if you click away from monster, lose focus
                {
                    Unfocus();

                }

            }
        }
    }

    void UpdateSprite()
    {
        //This changes from black to not
        spriteRenderer.color = Color.white;
    }

    private void OnMouseOver()
    {
        mousedOver = true;
    }

    private void OnMouseExit()
    {
        mousedOver = false;
    }

}
