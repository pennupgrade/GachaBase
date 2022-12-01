using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickableMonIcon : MonoBehaviour
{
    public MonsterAsset monster;

    private bool unlocked;

    private SpriteRenderer spriteRender;

    public GameObject description;

    // Start is called before the first frame update
    void Start()
    {
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
        spriteRender.color = Color.black;
        description.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryManager.Instance.Owned[monster.Name])
        {
            UpdateSprite();
        }

        // Checks if mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Checking if the click will hit this monster's box collider 

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null && InventoryManager.Instance.Owned[monster.Name])
            {
                Debug.Log("Clicked");
                MainManager.Instance.loadNewLevel(monster.SceneName);
            }
        }
    }

    void UpdateSprite()
    {
        //This changes from black to not
        spriteRender.color = Color.white;
    }

    void onMouseHover()
    {
        //Shows the description
    }

    void onClick()
    {
        //Loads the monster's play scene
    }

    private void OnMouseOver()
    {
        if (InventoryManager.Instance.Owned[monster.Name])
        {
            description.gameObject.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (InventoryManager.Instance.Owned[monster.Name])
        {
            description.gameObject.SetActive(false);
        }
    }

}
