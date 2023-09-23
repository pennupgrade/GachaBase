using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5;
    public LayerMask enemyLayer;
    private Vector3 velocity = new(0, 0, 0);

    public GameManage manager;

    [HideInInspector]
    public bool movementEnabled = false, tackleable = false;

    void Update()
    {

        if(movementEnabled){
            if(Input.GetKey(KeyCode.W)){
                velocity = new Vector3(velocity.x, 1, 0);
            }
            if(Input.GetKey(KeyCode.S)){
                velocity = new Vector3(velocity.x, -1, 0);
            }
            if(!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) {
                velocity = new Vector3(velocity.x, 0, 0);
            }
            if(Input.GetKey(KeyCode.A)){
                velocity = new Vector3(-1, velocity.y, 0);
            }
            if(Input.GetKey(KeyCode.D)){
                velocity = new Vector3(1, velocity.y, 0);
            }
            if(!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
                velocity = new Vector3(0, velocity.y, 0);
            }

            transform.position += speed * Time.deltaTime * velocity.normalized;
        }
    }

    public Vector3 getVelocity(){
        return speed * Time.deltaTime * velocity.normalized;
    }


    private void OnCollisionEnter(UnityEngine.Collision other) {
        if(!tackleable) return;
        if(enemyLayer == (enemyLayer | (1 << other.gameObject.layer))){
            Debug.Log("tackled");
            manager.calculateScore(transform.position);
            movementEnabled = false;
            other.gameObject.GetComponent<FollowPlayer>().movementEnabled = false;
        }
    }
}
