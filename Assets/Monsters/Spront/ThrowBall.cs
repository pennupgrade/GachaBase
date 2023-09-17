using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject player;
    public float speed = 2;

    private Vector3 target;
    private Vector3 dir;

    private bool moving = false;
    private bool targetSet = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)) {
            moving = true;
        }

        if(moving) {
            
            if(!targetSet){
                target = player.transform.position;
                dir = (target - transform.position).normalized;
                targetSet = true;
            }

            transform.position += speed*dir*Time.deltaTime;

            if(Vector3.Dot(dir, target-transform.position) < 0.0f){
                Debug.Log("Incomplete");
                moving = false;
            }
        }

    }

}
