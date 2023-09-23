using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    [SerializeField] FBGM FBMan;
    [SerializeField] GameObject CenterC;
    [SerializeField] CameraMovement CM;
    [SerializeField] Sprite Great, Good, Poor;
    [SerializeField] AudioClip song2;
    public GameObject DaseinImage;
    public GameObject ScorePanel, FailedMessage;
    public GameObject ContinueButton, RetryButton;
    public TextMeshProUGUI P, G, M, Score, Letter, mCombo;
    private float songPosition;
    public float songPosInBeats;
    private float secPerBeat, FakeBeatTimer;
    private float dsptimesong; private float bpm;
    float[] notesL, notesR, notesU, notesD, notesF;
    int nextIndexL, nextIndexR, nextIndexU, nextIndexD, nextIndexF;
    private bool started, calledStart, ended, levelTwo;
    [SerializeField] private GameObject left, right, up, down;

    void Start(){
        bpm = 180;
        secPerBeat = 60f / bpm;
        notesL = IncrementSong2(NoteSheet.Larray,-0.1f);
        notesR = IncrementSong2(NoteSheet.Rarray,-0.1f);
        notesU = IncrementSong2(NoteSheet.Uarray,-0.1f);
        notesD = IncrementSong2(NoteSheet.Darray,-0.1f);
        notesF = IncrementSong2(NoteSheet.Farray,-0.1f);
        nextIndexL=0; nextIndexR=0; nextIndexU=0; nextIndexD=0; nextIndexF=0;
        started=false;ended=false;calledStart=false;
        FakeBeatTimer=0;
        levelTwo=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!started&&!calledStart&&!ended&&(Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)
        ||Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow)
        ||Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)
        ||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))){
            StartCoroutine(StartFunction()); calledStart=true;
        }

        if (started&&!ended){
            songPosition = (float) (AudioSettings.dspTime - dsptimesong);
            songPosInBeats = songPosition / secPerBeat;
            if (nextIndexL < notesL.Length && notesL[nextIndexL] < songPosInBeats + 6){
                SpawnL(notesL[nextIndexL]);nextIndexL++;
            }if (nextIndexR < notesR.Length && notesR[nextIndexR] < songPosInBeats + 6){
                SpawnR(notesR[nextIndexR]);nextIndexR++;
            }if (nextIndexU < notesU.Length && notesU[nextIndexU] < songPosInBeats + 6){
                SpawnU(notesU[nextIndexU]);nextIndexU++;
            }if (nextIndexD < notesD.Length && notesD[nextIndexD] < songPosInBeats + 6){
                SpawnD(notesD[nextIndexD]);nextIndexD++;
            }
            SpawnFake();
            if(!levelTwo&&songPosInBeats>303) EndGame();
            if(levelTwo&&songPosInBeats>464) EndGame();
        }

        FakeBeatTimer=TimerF(FakeBeatTimer);
    }

    private IEnumerator StartFunction(){
        yield return new WaitForSeconds(3);
        started = true;
        dsptimesong = (float) AudioSettings.dspTime;
        GetComponent<AudioSource>().Play();
        CM.ToggleRotation();
    }
    private void EndGame(){
        ended=true;
        CM.ToggleRotation();
        var D = DaseinImage.GetComponent<Image>();
        var A = CenterC.GetComponent<CenterScript>();
        P.text = A.perfect.ToString();
        G.text = A.hit.ToString();
        M.text = A.missed.ToString();
        if (!levelTwo&&A.maxCombo==269) mCombo.text = "Full Combo!"; 
        if (levelTwo&&A.maxCombo==498) mCombo.text = "Full Combo!";
        else mCombo.text = "Max Combo: "+A.maxCombo;
        Score.text = Mathf.Ceil(A.score).ToString().PadLeft(7,'0');
        if(Mathf.Ceil(A.score)==1000000) {Letter.text = "Perfect"; D.sprite = Great; ContinueButton.SetActive(true);}
        else if (A.score>=950000) {Letter.text="S"; D.sprite = Great; ContinueButton.SetActive(true);}
        else if (A.score>=900000) {Letter.text="A"; D.sprite = Good;}
        else if (A.score>=800000) {Letter.text="B"; D.sprite = Good;}
        else if (A.score>=700000) {Letter.text="C"; D.sprite = Poor;}
        else {Letter.text="F"; D.sprite = Poor;}
        if(levelTwo){
            RetryButton.GetComponentInChildren<TextMeshProUGUI>().text = "Reset";
            ContinueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Retry";
        }
        StartCoroutine(FadeInPanel());
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public float[] IncrementSong2(float[] a, float c){
        float[] b = new float[a.Length];
        for(int i =0;i<a.Length;i++){
            b[i]=a[i]+c;
        }
        return b;
    }

    public void ContinueButtonFunction(){
        ended=false; levelTwo=true;
        StartCoroutine(FadeOutPanel());
        nextIndexL=0; nextIndexR=0; nextIndexU=0; nextIndexD=0; nextIndexF=0;
        notesL = IncrementSong2(NoteSheet.Larray2,0.5f);
        notesR = IncrementSong2(NoteSheet.Rarray2,0.5f);
        notesU = IncrementSong2(NoteSheet.Uarray2,0.5f);
        notesD = IncrementSong2(NoteSheet.Darray2,0.5f);
        notesF = IncrementSong2(NoteSheet.Farray2,0.5f);
        GetComponent<AudioSource>().clip=song2;
        GetComponent<AudioSource>().Play();
        dsptimesong = (float) AudioSettings.dspTime;
        var A = CenterC.GetComponent<CenterScript>();
        A.score=0; A.perfect=0; A.hit=0; A.missed=0; A.wrongPress=0; A.bar=60; A.combo=0; A.maxCombo=0; A.zeroTimer=0;
        A.LevelTwoStart();
        CM.ToggleRotation();
    }

    public void Failed(){
        StartCoroutine(FailedCo());
    }
    private IEnumerator FailedCo(){
        GetComponent<AudioSource>().Stop();
        FailedMessage.SetActive(true);
        yield return new WaitForSeconds(3);
        Restart();
    }

    private IEnumerator FadeInPanel(){
        ScorePanel.SetActive(true);
        CanvasGroup CG = ScorePanel.GetComponent<CanvasGroup>();
        CG.interactable = true;
        while (CG.alpha<1){
            CG.alpha+=Time.deltaTime/2;
            yield return null;
        }
    }
    private IEnumerator FadeOutPanel(){
        CanvasGroup CG = ScorePanel.GetComponent<CanvasGroup>();
        while (CG.alpha>0){
            CG.alpha-=Time.deltaTime;
            yield return null;
        }
        ScorePanel.SetActive(false);
    }

    private float TimerF( float val){
        if(val>=0){
            val-=Time.deltaTime;
            if (val<0) val = 0;
        }
        return val;
    }

    private void SpawnFake(){
        if (FakeBeatTimer<0.001){
            if(!levelTwo){
                if (songPosInBeats>211.95 && songPosInBeats<212.05){
                    FakeBeatTimer=0.2f;
                    FBMan.SpawnFB(2);FBMan.SpawnFB(3);
                }
                if (songPosInBeats>213.95 && songPosInBeats<214.05){
                    FakeBeatTimer=0.2f;
                    FBMan.SpawnFB(6);FBMan.SpawnFB(7);
                }
                if (songPosInBeats>215.95 && songPosInBeats<216.05){
                    FakeBeatTimer=0.2f;
                    FBMan.SpawnFB(2);FBMan.SpawnFB(3);
                }
                if (songPosInBeats>217.95 && songPosInBeats<218.05){
                    FakeBeatTimer=0.2f;
                    FBMan.SpawnFB(6);FBMan.SpawnFB(7);
                }
                if (songPosInBeats>288.95 && songPosInBeats<289.05){
                    FakeBeatTimer=0.2f;
                    FBMan.SpawnFB(1);FBMan.SpawnFB(2);FBMan.SpawnFB(3);FBMan.SpawnFB(4);
                }
            }else{
                if (songPosInBeats>163.95 && songPosInBeats<164.05){
                    FakeBeatTimer=0.2f;
                    FBMan.SpawnFB(5);FBMan.SpawnFB(6);FBMan.SpawnFB(7);FBMan.SpawnFB(8);
                }
                if (songPosInBeats>290.95 && songPosInBeats<291.05){
                    FakeBeatTimer=0.2f;
                    FBMan.SpawnFB(5);FBMan.SpawnFB(1);FBMan.SpawnFB(4);FBMan.SpawnFB(8);
                    FBMan.SpawnFB(9);FBMan.SpawnFB(10);FBMan.SpawnFB(11);FBMan.SpawnFB(12);
                }
            }
        }
        if (nextIndexF < notesF.Length && notesF[nextIndexF] < songPosInBeats + 6){
            FBMan.SpawnFB(Random.Range(1,13));nextIndexF++;
        }
    }

    private void SpawnL(float bNote){
        GameObject note = Instantiate(left,new Vector3(-15,0,0), Quaternion.identity);
        note.GetComponent<NewBeatScript>().Init(bNote, this.gameObject);
    }private void SpawnR(float bNote){
        GameObject note = Instantiate(right,new Vector3(15,0,0), Quaternion.identity);
        note.GetComponent<NewBeatScript>().Init(bNote, this.gameObject);
    }private void SpawnU(float bNote){
        GameObject note = Instantiate(up,new Vector3(0,15,0), Quaternion.identity);
        note.GetComponent<NewBeatScript>().Init(bNote, this.gameObject);
    }private void SpawnD(float bNote){
        GameObject note = Instantiate(down,new Vector3(0,-15,0), Quaternion.identity);
        note.GetComponent<NewBeatScript>().Init(bNote, this.gameObject);
    }


}
