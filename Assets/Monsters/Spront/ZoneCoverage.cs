using UnityEngine;

public class ZoneCoverage : MonoBehaviour
{
    public float speed;
    public Transform[] targets;

    public static bool ballCaught = false;

    [HideInInspector]
    public bool movementEnabled = false;
    
    private Vector3 dir;
    private int targetIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if(FollowPlayer.intercepted || ballCaught){
           moveToFollowBall();
        }

        if(movementEnabled){
            dir = targets[targetIndex].position - transform.position;
            dir.Normalize();

            transform.position += speed * Time.deltaTime * dir;

            if((targets[targetIndex].position - transform.position).magnitude < 0.1f) {
                targetIndex++;
                if(targetIndex >= targets.Length) targetIndex = 0;
            }
        }
    }

    public void moveToFollowBall(){
        GetComponent<FollowPlayer>().enabled = true;
        enabled = false;
    }
    

}
