using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountererMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    private Vector2 directions;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        OnAnimatorMove();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        directions = new Vector2(moveX, moveY).normalized;
    }

    void OnAnimatorMove()
    {
        rb.velocity = new Vector2(directions.x * speed, directions.y * speed);
    }
}
