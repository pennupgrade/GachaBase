using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public static bool intercepted = false;
    public Transform interceptionReturnPoint;

    [HideInInspector]
    public bool movementEnabled = false;
    [HideInInspector]
    public Vector3 target;
    
    private Vector3 dir;

    void Start() {
        target = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(intercepted){
            target = interceptionReturnPoint.position;
        } else {target = player.transform.position;}

        if(movementEnabled){
            dir = target - transform.position;
            dir.Normalize();

            transform.position += speed*dir*Time.deltaTime;
        }
    }

    public void setTarget(Vector3 target){
        this.target = target;
    }



}
