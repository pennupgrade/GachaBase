using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance => InventoryManager.instance;

    [SerializeField]
    private List<Monster> possibleMonsters;

    private Dictionary<Monster, bool> ownedMonsters;

    private Canvas canvas;

    private void Awake()
    {
        if (InventoryManager.instance != null && InventoryManager.instance != this)
        {
            Destroy(this);
            throw new System.Exception("An instance of this singleton already exists.");
        }
        else
        {
            InventoryManager.instance = this;
        }
        this.canvas = GetComponent<Canvas>();
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
