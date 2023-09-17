using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed  = 5;

    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.position += new Vector3(0, speed*Time.deltaTime, 0);
        }
        if(Input.GetKey(KeyCode.S)){
            transform.position += new Vector3(0, -speed*Time.deltaTime, 0);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.position += new Vector3(-speed*Time.deltaTime, 0, 0);
        }
        if(Input.GetKey(KeyCode.D)){
            transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
        }

    }

}
