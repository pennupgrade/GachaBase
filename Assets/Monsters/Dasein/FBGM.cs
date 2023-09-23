using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBGM : MonoBehaviour
{
    public GameObject FB;

    public void SpawnFB(int i){
        GameObject spawned;
        switch (i){
            case 1: spawned=Instantiate(FB, new Vector3(-6,15,0), Quaternion.identity); break;
            case 2: spawned=Instantiate(FB, new Vector3(-3,15,0), Quaternion.identity); break;
            case 3: spawned=Instantiate(FB, new Vector3(3,15,0), Quaternion.identity); break;
            case 4: spawned=Instantiate(FB, new Vector3(6,15,0), Quaternion.identity); break;
            case 5: spawned=Instantiate(FB, new Vector3(-6,-15,0), Quaternion.identity); break;
            case 6: spawned=Instantiate(FB, new Vector3(-3,-15,0), Quaternion.identity); break;
            case 7: spawned=Instantiate(FB, new Vector3(3,-15,0), Quaternion.identity); break;
            case 8: spawned=Instantiate(FB, new Vector3(6,-15,0), Quaternion.identity); break;
            case 9: spawned=Instantiate(FB, new Vector3(-15,4,0), Quaternion.identity); break;
            case 10: spawned=Instantiate(FB, new Vector3(-15,-4,0), Quaternion.identity); break;
            case 11: spawned=Instantiate(FB, new Vector3(15,4,0), Quaternion.identity); break;
            case 12: spawned=Instantiate(FB, new Vector3(15,-4,0), Quaternion.identity); break;
            default: spawned=Instantiate(FB, new Vector3(15,-4,0), Quaternion.identity); break;
        }
        if (i<=4) spawned.GetComponent<FakeBeatS>().SetDirection("U");
        else if (i<=8) spawned.GetComponent<FakeBeatS>().SetDirection("D");
        else if (i<=10) spawned.GetComponent<FakeBeatS>().SetDirection("L");
        else spawned.GetComponent<FakeBeatS>().SetDirection("R");
    }
}
