using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    /// <summary>
    /// Checks if the arrow has fallen into the correct trigger zone and manages
    /// </summary>
    /// 

    public bool canBePressed;
    public KeyCode keyToPress;
    private float beatTempo = 60;

    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60f; //Gives us beats per second. 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0f, 8*beatTempo * Time.deltaTime, 0f); // Moves the arrow down per second by time.deltatime

        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                GameManager.instance.NoteHit(this.transform);
                Destroy(gameObject);
            }
        }

        if (transform.position.y < -4.5f){
           
            GameManager.instance.NoteMissed(this.transform);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            canBePressed = false;
        }
    }
}
