using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrbPlace : MonoBehaviour
{
    public Transform orb = null;
    public Rigidbody2D rigidbody = null;
    public float maxDelay = 10f;
    private float delay = 0f;
    private Vector2 position;

    public void Update()
    {
        delay -= Time.deltaTime;
        if (Input.GetMouseButton(0) && delay<=0f)
        {
            Debug.Log("Clicked");
            position = Input.mousePosition;
            position = Camera.main.ScreenToWorldPoint(position);
            if (position.y > 2.9 && position.y < 4.7)
            {
                orb.position = position;
                rigidbody.velocity = Vector2.zero;
                //Instantiate(orb, new Vector3(position.x, position.y, 0), Quaternion.identity);
                delay = maxDelay;
            }
        }
    }
}
