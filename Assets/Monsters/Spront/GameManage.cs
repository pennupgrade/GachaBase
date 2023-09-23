using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{

    [HideInInspector]
    public static bool gameOn = false;
    public GameObject ball;
    public GameObject player;
    public GameObject[] enemies;

    [HideInInspector]
    public int score = 0;
    
    public GameObject lineOfScrimmage;

    [HideInInspector]
    public static float YARD_LENGTH = 0.34f;

    [SerializeField]
    private UIManager uimanager;
    // Update is called once per frame
    void Update()
    {
        if(!gameOn && Input.GetKey(KeyCode.Space)){
            turnGameOn();
        }

        if(Input.GetKeyDown(KeyCode.P)){
            turnGameOff();
        }
    }

    public void calculateScore(Vector3 finalPos){
        int roundScore = (int)((finalPos.y - lineOfScrimmage.transform.position.y)/(5f*YARD_LENGTH));
        
        Debug.Log(roundScore);
        addScore(roundScore); 
    }

    public void addScore(int add){
        score += add;
    }

    public void turnGameOn(){
        if(gameOn) return;

        ball.GetComponent<ThrowBall>().movementEnabled = true;
        player.GetComponent<Movement>().movementEnabled = true;
        foreach(GameObject enemy in enemies){
            FollowPlayer fptemp = enemy.GetComponent<FollowPlayer>();
            if(fptemp != null) fptemp.movementEnabled = true;

            ZoneCoverage zctemp = enemy.GetComponent<ZoneCoverage>();
            if(zctemp != null) zctemp.movementEnabled = true;
        }

        gameOn = true;
    }

    public void turnGameOff(){
        if(!gameOn) return;

        gameOn = false;
        FollowPlayer.intercepted = false;
        ZoneCoverage.ballCaught = false;
        uimanager.DisableInterceptedText();
        uimanager.DisableCaughtText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
