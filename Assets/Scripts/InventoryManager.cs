using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance => InventoryManager.instance;
    
    private List<string> options;
    
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
    }

    // Start is called before the first frame update
    void Start()
    {
        options = new List<string>();
        options.Add("Gacha");
        this.ownedMonsters = new Dictionary<string, bool>();
        //Eventually update this to maintain saved data
        foreach (MonsterAsset m in possibleMonsters)
        {
            ownedMonsters[m.Name] = false;
        }
    }

    public void Pull(string pullName) {
        InventoryManager.Instance.Owned[pullName] = true;
        updateDropdown();
    }

    public void updateDropdown()
    {
        options = new List<string>();
        options.Add("Gacha");
        foreach (MonsterAsset m in InventoryManager.Instance.Monsters)
        {
            if (InventoryManager.Instance.Owned[m.Name])
            {
                options.Add(m.Name);
            }
        }
    }
}
