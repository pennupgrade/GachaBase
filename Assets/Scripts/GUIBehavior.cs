using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GUIBehavior : MonoBehaviour
{
    public static GUIBehavior instance;
    [SerializeField]    
    private Dropdown menuSelect;
    private List<string> options;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        menuSelect.ClearOptions();
        options = new List<string>();
        options.Add("Gacha");
        menuSelect.AddOptions(options);
    }

    public void updateDropdown()
    {
        menuSelect.ClearOptions();
        options = new List<string>();
        options.Add("Gacha");
        foreach (Monster m in InventoryManager.Instance.possibleMonsters)
        {
            if (InventoryManager.Instance.ownedMonsters[m.name])
            {
                options.Add(m.name);
            }
        }
        menuSelect.AddOptions(options);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void visitPen()
    {
        Debug.Log("Clicked: " + menuSelect.value);
        string clickedMon = options[menuSelect.value];
        Debug.Log("Clicked: " + clickedMon);

        if (options[0] == "empty") {
            Debug.Log("You are Monsterless");
        } else
        {
            switch (clickedMon)
            {
                case "Gacha":
                    MainManager.instance.loadNewLevel("GachaScene");
                    break;
                case "Bingus":
                    MainManager.instance.loadNewLevel("TestPenScene");
                    break;
                default:
                    Debug.Log("Selected Mon cannot be reached");
                    break;
            }
        }

        
        


        
    }

}
