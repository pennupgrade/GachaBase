using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance => InventoryManager.instance;

    [SerializeField]
    private Dropdown menuSelect;
    
    private List<string> options;

    private CanvasGroup cg;


    [SerializeField]
    private List<MonsterAsset> possibleMonsters; //MonsterSO is the scriptable object Monster
    public List<MonsterAsset> Monsters {
        get { return possibleMonsters; }
    }

    private Dictionary<string, bool> ownedMonsters;
    public Dictionary<string, bool> Owned {
        get { return ownedMonsters; }
    }

    private void Awake()
    {
        if (InventoryManager.instance != null && InventoryManager.instance != this)
        {
            Destroy(this);
            //throw new System.Exception("An instance of this singleton already exists.");
        }
        else
        {
            InventoryManager.instance = this;
        }
        this.cg = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Hide()
    {
        //this.canvas.enabled = false;
        cg.interactable= false;
        cg.alpha = 0;
    }
    public void Show()
    {
        //this.canvas.enabled = true;
        cg.interactable= true;
        cg.alpha = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        menuSelect.ClearOptions();
        options = new List<string>();
        options.Add("Gacha");
        menuSelect.AddOptions(options);
        this.ownedMonsters = new Dictionary<string, bool>();
        //Eventually update this to maintain saved data
        foreach (MonsterAsset m in possibleMonsters)
        {
            ownedMonsters.Add(m.name, false);
        }
    }

    public void Pull(string pullName) {
        InventoryManager.Instance.Owned[pullName] = true;
        updateDropdown();
    }

    public void updateDropdown()
    {
        menuSelect.ClearOptions();
        options = new List<string>();
        options.Add("Gacha");
        foreach (MonsterAsset m in InventoryManager.Instance.Monsters)
        {
            if (InventoryManager.Instance.Owned[m.name])
            {
                options.Add(m.name);
            }
        }
        menuSelect.AddOptions(options);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (cg.alpha > 0)
            {
                Hide();
            } else
            {
                Show();
            }
        } 
    }

    public void visitPen() {
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
                    MainManager.Instance.loadNewLevel("GachaScene");
                    break;
                case "TestMonster":
                    MainManager.Instance.loadNewLevel("TestPenScene");
                    break;
                default:
                    Debug.Log("Selected Mon cannot be reached");
                    break;
            }
        }
    }
}
