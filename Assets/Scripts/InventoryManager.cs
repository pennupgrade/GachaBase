using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance => InventoryManager.instance;

    [SerializeField]
    public List<Monster> possibleMonsters; //MonsterSO is the scriptable object Monster

    public Dictionary<string, bool> ownedMonsters;

    [SerializeField]
    private Canvas canvas;

    public void updateGUI()
    {
        GameObject.FindGameObjectWithTag("GUI").GetComponent<Text>().text = "";
        string inventoryLog = "";
        foreach (Monster m in possibleMonsters)
        {
            inventoryLog += m.name + " " + ownedMonsters[m.name];
        }
        
        GameObject.FindGameObjectWithTag("GUI").GetComponent<Text>().text = inventoryLog;
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
        //this.canvas = GetComponent<Canvas>();
        Hide();
    }

    public void Hide()
    {
        this.canvas.enabled = false;
    }
    public void Show()
    {
        this.canvas.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.ownedMonsters = new Dictionary<string, bool>();
        //Eventually update this to maintain saved data
        foreach (Monster m in possibleMonsters)
        {
            ownedMonsters.Add(m.name, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (this.canvas.enabled)
            {
                Hide();
            } else
            {
                Show();
            }
        } 
    }
}
