using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaseinSpriteChange : MonoBehaviour
{
    public Sprite[] N = new Sprite[11];
    public Sprite normal;
    public GameObject WhiteBG;
    public Sprite ball1, ball2, ball3, ballPressed;
    // Start is called before the first frame update
    private Sprite current;
    private float pressedTimer, frameTimer;
    private bool started;
    private SpriteRenderer SR;

    void Start(){
        pressedTimer=0;
        started=false;
        frameTimer=4;
        SR = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(frameTimer==0){
            frameTimer=4;
            if(started){
                if (pressedTimer>0){
                    SR.sprite=ballPressed;
                }else{
                    SR.sprite=current;
                }
            }
        }frameTimer--;

        pressedTimer=TimerF(pressedTimer);
    }

    private float TimerF( float val){
        if(val>=0){
            val-=Time.deltaTime;
            if (val<0) val = 0;
        }
        return val;
    }

    public void StartAnimation(){//done
        StartCoroutine(StartA());
    }

    private IEnumerator StartA(){
        for (int k = 0; k<11;k++){
            if(k==5||k==4||k==6) yield return new WaitForSeconds(0.5f);
            else yield return new WaitForSeconds(0.35f);
            SR.sprite = N[k];
        }
        WhiteBG.GetComponent<WhiteFade>().Fade();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        current=ball3;
        SR.sprite = current;
        yield return new WaitForSeconds(1);
        started=true;
    }

    public void PressedSprite(){//done
        pressedTimer=0.15f;
    }

    public void ChangeSprite(int bar){
        if (started){
            if (bar<10){
                current=ball1;
            } else if (bar<45){
                current=ball2;
            } else{
                current=ball3;
            }
        }
    }
}
