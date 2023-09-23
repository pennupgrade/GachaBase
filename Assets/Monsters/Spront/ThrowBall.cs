using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject player;
    public GameManage gamemanager;
    public float speed = 2;
    public GameObject targetMarker;
    public LayerMask enemyLayer;

    [HideInInspector]
    public bool movementEnabled{get; set;} = false;

    private Vector3 target;
    private Vector3 dir;

    private bool moving = false;
    private bool targetSet = false;
    private bool gameOnSpace = false;

    private bool incomplete = false;

    [SerializeField]
    private UIManager uimanager;

    // Update is called once per frame
    void Update()
    {
        if(GameManage.gameOn && Input.GetKeyDown(KeyCode.Space)) {
            if(!gameOnSpace){
                gameOnSpace = true; //TODO GetKey calls true multiple times, try and use getkeydown instead, we want it only to happen on the first frame
                return;
            }
            moving = true;
        }

        if(moving) {
            
            if(!targetSet){
                Vector3 velocity = player.GetComponent<Movement>().getVelocity();
                target = player.transform.position + velocity*25*(player.transform.position-transform.position).magnitude;
                dir = (target - transform.position).normalized;
                targetMarker.SetActive(true);
                targetMarker.transform.position = target;
                targetSet = true;
            }

            transform.position += speed * Time.deltaTime * dir;

            if(Vector3.Dot(dir, target-transform.position) < 0.0f){
                Debug.Log("Incomplete");
                incomplete = true;
                targetMarker.SetActive(false);
                moving = false;
            }
        }

    }

    private void OnCollisionEnter(UnityEngine.Collision other){
        if(incomplete) return;

        if(other.gameObject == player){
            Debug.Log("ball caught");
            ZoneCoverage.ballCaught = true;
            other.gameObject.GetComponent<Movement>().tackleable = true;
            gameObject.SetActive(false);
            uimanager.EnableCaughtText(other.gameObject.transform.position.x, other.gameObject.transform.position.y);
        }
        else if(enemyLayer == (enemyLayer | (1 << other.gameObject.layer))){
            Debug.Log("intercepted");
            gameObject.SetActive(false);
            gamemanager.addScore(-3);
            FollowPlayer.intercepted = true;
            uimanager.EnableInterceptedText(other.gameObject.transform.position.x, other.gameObject.transform.position.y);
        }
    }

}
