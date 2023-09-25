using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckWin : MonoBehaviour
{
    private GameObject group;

    // Start is called before the first frame update
    void Start()
    {
        group = GameObject.Find("Items");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollOnce() {
		Debug.Log ("Roll once");
        GameObject item = group.transform.GetChild(0).gameObject;
        item.GetComponent<ChangeSprite>().Change();
        
	}

    public void RollTen() {
		Debug.Log ("Roll 10 times");
        for (int i = 0; i < 10; i++) {
            GameObject item = group.transform.GetChild(i).gameObject;
            item.GetComponent<ChangeSprite>().Change();
        }
	}

}
