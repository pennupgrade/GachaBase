using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CenterScript : MonoBehaviour
{
    public DaseinSpriteChange spriteChanger;
    public Slider TheBar;
    public GM theGameM;
    public TextMeshProUGUI Multiplier, ScoreKeeper, ComboKeeper;
    public GameObject Prompt;
    [SerializeField] private GameObject hitPrefab, perfectPrefab, missPrefab;
    private float leftHit, rightHit, upHit, downHit;
    private bool waitingLeft, waitingRight, waitingUp, waitingDown, started, startTimerCoroutine, levelTwo;
    private float leftBlock, rightBlock, upBlock, downBlock;
    private float hitTime, blockTime, noteScore;
    public float score, zeroTimer;
    private int frameTimer=8;
    public int perfect, hit, missed, wrongPress, bar, combo, maxCombo;
    
    // Start is called before the first frame update
    void Start()
    {
        started=false; startTimerCoroutine = false;
        bar=60;
        noteScore=3717.4721f;
        hitTime=0.26f; blockTime=0.25f;
        leftHit=0; rightHit=0; upHit=0; downHit=0;
        waitingLeft=false; waitingRight=false; waitingUp=false; waitingDown=false;
        leftBlock=0; rightBlock=0; upBlock=0; downBlock=0;
        perfect=0;hit=0;missed=0;wrongPress=0;combo=0;maxCombo=0;
        score=0;zeroTimer=0;
        levelTwo=false;
    }

    // Update is called once per frame
    void Update()
    {
        //hitting notes
        if(Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)){
            spriteChanger.PressedSprite();
            if(leftHit>0.001&&waitingLeft){
                if(leftHit<0.2f&&leftHit>0.06){
                    PerfectNote();
                }else{
                    GoodNote();
                }
                CoinIncrement();
                leftHit=0;
                waitingLeft=false;
            }else{
                WrongKey();
                leftBlock=blockTime;
            }
        }if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow)){
            spriteChanger.PressedSprite();
            if(rightHit>0.001&&waitingRight){
                if(rightHit<0.2f&&rightHit>0.06){
                    PerfectNote();
                }else{
                    GoodNote();
                }
                CoinIncrement();
                rightHit=0;
                waitingRight=false;
            }else{
                WrongKey();
                rightBlock=blockTime;
            }
        }if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)){
            spriteChanger.PressedSprite();
            if(upHit>0.001&&waitingUp){
                if(upHit<0.2f&&upHit>0.06){
                    PerfectNote();
                }else{
                    GoodNote();
                }
                CoinIncrement();
                upHit=0;
                waitingUp=false;
            }else{
                WrongKey();
                upBlock=blockTime;
            }
        }if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow)){
            spriteChanger.PressedSprite();
            if(downHit>0.001&&waitingDown){
                if(downHit<0.2f&&downHit>0.06){
                    PerfectNote();
                }else{
                    GoodNote();
                }
                CoinIncrement();
                downHit=0;
                waitingDown=false;
            }else{
                WrongKey();
                downBlock=blockTime;
            }
        }

        if(!levelTwo&&bar<1){
            zeroTimer+=Time.deltaTime;
            if (zeroTimer>12){
                theGameM.Failed();
            }
        }

        //expired notes
        if(leftHit<0.001&&waitingLeft){
            MissedNote();
            waitingLeft=false;
        }if(rightHit<0.001&&waitingRight){
            MissedNote();
            waitingRight=false;
        }if(upHit<0.001&&waitingUp){
            MissedNote();
            waitingUp=false;
        }if(downHit<0.001&&waitingDown){
            MissedNote();
            waitingDown=false;
        }


        if(bar>60)bar=60; if (bar<0)bar=0;
        if(frameTimer>0) frameTimer--;
        else{
            frameTimer=8;
            TheBar.value=bar;
            if(bar==60) Multiplier.text = "2x Coin Multiplier!";
            else Multiplier.text = "";
            spriteChanger.ChangeSprite(bar);
        }
        leftHit=TimerF(leftHit); rightHit=TimerF(rightHit); upHit=TimerF(upHit); downHit=TimerF(downHit);
        leftBlock=TimerF(leftBlock); rightBlock=TimerF(rightBlock); upBlock=TimerF(upBlock); downBlock=TimerF(downBlock);
    }

    private void PerfectNote(){
        score+=noteScore;
        perfect++;bar+=2;combo++;
        if(combo>maxCombo)maxCombo = combo;
        ScoreKeeper.text=Mathf.Ceil(score).ToString().PadLeft(7,'0');
        if (combo>9) ComboKeeper.text="Combo: " + combo;
        Instantiate(perfectPrefab,Vector3.zero, Quaternion.identity);
    }
    private void GoodNote(){
        hit++;bar++;combo++;
        if(combo>maxCombo)maxCombo = combo;
        score+=(noteScore-900);
        ScoreKeeper.text=Mathf.Ceil(score).ToString().PadLeft(7,'0');
        if (combo>9) ComboKeeper.text="Combo: " + combo;
        Instantiate(hitPrefab,Vector3.zero, Quaternion.identity);
    }
    private void MissedNote(){
        missed++;bar-=12;
        if(combo>maxCombo)maxCombo = combo;
        combo=0;
        score-=5000; CurrencyManager.Instance.Currency-=2;
        if(score<0)score=0;
        ScoreKeeper.text=Mathf.Ceil(score).ToString().PadLeft(7,'0');
        ComboKeeper.text="";
        Instantiate(missPrefab,Vector3.zero, Quaternion.identity);
    }
    private void WrongKey(){
        if(!started){
            if (!startTimerCoroutine){
                spriteChanger.StartAnimation();
                Prompt.SetActive(false);
                startTimerCoroutine=true;
                StartCoroutine(StartTimer());
            }
        }else{
            wrongPress++;
            score-=1000;
            if(score<0)score=0;
            ScoreKeeper.text=Mathf.Ceil(score).ToString().PadLeft(7,'0');
            Instantiate(missPrefab,Vector3.zero, Quaternion.identity);
        }
    }

    public void LevelTwoStart(){
        ScoreKeeper.text="0000000";
        ComboKeeper.text="";
        TheBar.value=bar;
        noteScore=2008.0321f;
        levelTwo=true;
    }

    private IEnumerator StartTimer(){
        yield return new WaitForSeconds(5);
        started=true;
    }

    private float TimerF( float val){
        if(val>=0){
            val-=Time.deltaTime;
            if (val<0) val = 0;
        }
        return val;
    }

    void OnTriggerEnter2D(Collider2D c){
        c.gameObject.GetComponent<NewBeatScript>().Destr();
        if(c.gameObject.tag=="Dasein_Left"){
            if(leftBlock<0.001){
                leftHit=hitTime;
                if (waitingLeft) MissedNote();
                waitingLeft=true;
            } else {score+=100;wrongPress--; MissedNote();}
        }if(c.gameObject.tag=="Dasein_Right"){
            if(rightBlock<0.001){
                rightHit=hitTime;
                if (waitingRight) MissedNote();
                waitingRight=true;
            } else {score+=100;wrongPress--; MissedNote();}
        }if(c.gameObject.tag=="Dasein_Up"){
            if(upBlock<0.001){
                upHit=hitTime;
                if (waitingUp) MissedNote();
                waitingUp=true;
            } else {score+=100;wrongPress--; MissedNote();}
        }if(c.gameObject.tag=="Dasein_Down"){
            if(downBlock<0.001){
                downHit=hitTime;
                if (waitingDown) MissedNote();
                waitingDown=true;
            } else {score+=100;wrongPress--; MissedNote();}
        }
    }

    private void CoinIncrement(){
        if(bar>=59){
            CurrencyManager.Instance.Currency+=1;
            CurrencyManager.Instance.Currency+=1;
        }else{
            CurrencyManager.Instance.Currency+=1;
        }
    }

}
