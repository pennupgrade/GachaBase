using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBeatScript : MonoBehaviour
{
    private float beatOfThisNote;
    private GameObject GameM;
    private Vector3 SpawnPos, RemovePos;
    // Start is called before the first frame update
    void Start()
    {
        RemovePos = Vector3.zero;
        SpawnPos = transform.position;
    }

    public void Init(float bNote, GameObject gm){
        GameM=gm;
        beatOfThisNote = bNote;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(
        SpawnPos,
        RemovePos,
        (6 - (beatOfThisNote - GameM.GetComponent<GM>().songPosInBeats)) / 6
    ); 
    }
    public void Destr(){
        StartCoroutine(Destruction());
    }
    private IEnumerator Destruction(){
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
