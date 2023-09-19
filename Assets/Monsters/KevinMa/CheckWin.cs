using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckWin : MonoBehaviour
{
    private Button btn;
    [SerializeField] private GameObject item1;
    [SerializeField] private GameObject item2;
    [SerializeField] private GameObject item3;
    int index1;
    int index2;
    int index3;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(DoOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoOnClick(){
		Debug.Log ("You have clicked the button!");
        index1 = item1.GetComponent<ChangeSprite>().Change();
        index2 = item2.GetComponent<ChangeSprite>().Change();
        index3 = item3.GetComponent<ChangeSprite>().Change();
        Debug.Log (index1 + " " + index2 + " " + index3);
	}

}
