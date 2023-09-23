using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBeatS : MonoBehaviour
{
    private Vector3 direction;
    private float lifeTimer;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimer=3;
    }

    public void SetDirection(string s){
        if (s=="U") direction = new Vector3(0,-1,0);
        if (s=="D") direction = new Vector3(0,1,0);
        if (s=="L") direction = new Vector3(1,0,0);
        if (s=="R") direction = new Vector3(-1,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=Time.deltaTime * 7.5f * direction;
        lifeTimer-=Time.deltaTime;
        if (lifeTimer<0.01) Destroy(gameObject);
    }
}
