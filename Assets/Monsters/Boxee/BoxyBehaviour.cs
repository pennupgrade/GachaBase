using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxyBehaviour : MonoBehaviour
{
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 character = transform.localScale;
        {
        if (Input.GetKeyDown(KeyCode.A)) {
		    transform.Translate(-1,0,0);
            if (sprite != null) {
                sprite.flipX = true;
            }
		}
        if (Input.GetKeyDown(KeyCode.D)) {
            transform.Translate(1,0,0);
            if (sprite != null) {
                sprite.flipX = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) {
		    transform.Translate(0,1,0);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
		    transform.Translate(0,-1,0);
		}
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided");
        CurrencyManager.Instance.Currency += 1;
        Vector3 pos = new Vector3((float) Random.Range(-11, 11), Random.Range(-3.5f, 3.5f), 0);
        collision.transform.position = pos;
    }
	

 
}
