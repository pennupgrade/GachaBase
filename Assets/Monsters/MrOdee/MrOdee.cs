using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MrOdee : MonoBehaviour
{

    private bool bloomed = false;
    private int clicks = 0;
    int timer = 0;

    private Animator animationRenderer;
    
    // pick some random direction
    void growthClick()
    {
        clicks++;
        CurrencyManager.Instance.Currency -= 10;
        if (clicks > 5)
        {
            animationRenderer.SetBool("Boolean", true);
            bloomed = true;
            clicks = 0;
        }
    }

    void harvestClick()
    {
        // change the sprite to reflect that direction
        CurrencyManager.Instance.Currency += 5;
    }



    // Start is called before the first frame update
    void Start()
    {
        
        animationRenderer = gameObject.GetComponent<Animator>();
        animationRenderer.SetBool("Boolean", false);
        
    }

    // Update is called once per frame
    void Update()
    {
        animationRenderer = gameObject.GetComponent<Animator>();
        // detect a click
        if (!bloomed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Checking if the click will hit this monster's box collider 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Debug.Log(ray);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                if (hit.collider != null)
                {
                    growthClick();
                }
            }
        } else
        {
            timer++;
            if (Input.GetMouseButtonDown(0))
            {
                // Checking if the click will hit this monster's box collider 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Debug.Log(ray);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                if (hit.collider != null)
                {
                    harvestClick();
                }
            }
            if (timer > 1000)
            {
                bloomed = false;
                animationRenderer.SetBool("Boolean", false); ;
                timer = 0;
            }
        }
        



    }
}
