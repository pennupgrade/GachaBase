using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFade : MonoBehaviour
{


    public void Fade(){
        StartCoroutine(fadeout());
    }

    private IEnumerator fadeout(){
        for(int i =0; i<10;i++){
            GetComponent <SpriteRenderer> ().color -= new Color (0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.08f);
        }
        gameObject.SetActive(false);
    }
}
