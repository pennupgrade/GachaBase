using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 a, b;
    public float deltaTime = 1f / 10f, currentTime, x1, x2;
    public static SpawnBehavior deaths;

    void Start()
    {
        InvokeRepeating("UpdateDestiny", 0f, 1f);
        InvokeRepeating("Move", 0f, deltaTime);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {
                if(hit.collider.gameObject==gameObject) {
                    SpawnBehavior.deaths++;
                    CancelInvoke();
                    Destroy(gameObject);
                }
            }
        }

    }

    void OnCollisionEnter2D(Collision2D col) // if object collides with screen bounds then change destination to middle of the screen.
    {
        b = new Vector3(0, 0, 0);
    }
 
    void Move()
    {
        currentTime += deltaTime;
        gameObject.transform.position = Vector3.Lerp(a, b, currentTime);
    }
 
    void UpdateDestiny()
    {
        currentTime = 0.0f;
        a = gameObject.transform.position;
        x1 = a.x;
        b = gameObject.transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
        x2 = b.x;
        if (x2 < x1)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

}
