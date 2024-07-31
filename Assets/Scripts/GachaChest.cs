using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GachaChest : MonoBehaviour
{

    private int numOfMonsters;
    
    [SerializeField]
    private Button cardButton;

    [SerializeField]
    private Image cardImage;

    [SerializeField] private Image overlayImage;

    [SerializeField]
    private int ROLL_COST;
    public int RollCost {
        get { return ROLL_COST; }
    } 


    private Vector3 initialScale;
    public float targetScaleFactor;
    public float clickScaleFactor;
    private Vector3 targetScale;

    // Pity system vars
    private int rollSinceLastUnique;
    private int pityThreshold = 3;
    private float pityPercentageUp;

    private bool mousedOver;

    //Reference to player info
    private Animator animator;

    bool isOpening;
    float showCardTime;

    // Start is called before the first frame update
    void Start()
    {
        HideCard();
        LoadGacha();

        animator = GetComponent<Animator>();
        
        initialScale = transform.localScale;
        targetScale = transform.localScale * targetScaleFactor;
        
    }

    //Called in start()? to fill the bank with all the possible types of monsters?
    void LoadGacha()
    {
        numOfMonsters = InventoryManager.Instance.Monsters.Count;
    }

    bool IsInFocus()
    {
        if (!mousedOver || cardImage.enabled || isOpening)
        {
            return false;
        }
        // check if a monster icon is covering the chest
        foreach (ClickableMonsterIcon m in ClickableMonsterIcon.MonsterIcons)
            {
                if (m.HasFocus) {
                    return false;
                }
            }

        // TODO: check if gacha card is in front
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            transform.localScale *= 1.002f;
            if (Time.time >= showCardTime)
            {
                Roll();
                isOpening = false;
            }
        }
        
        else if (IsInFocus()) {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 0.05f);
            if (Input.GetMouseButtonDown(0))
            {
                AttemptRoll();
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, 0.05f);
        }
    }

    public void AttemptRoll()
    {
        if (CurrencyManager.Instance.Currency < ROLL_COST) {
            Debug.Log("You don't have enough money for that.");
            // a little bump for responsiveness even if you don't have enough money
            transform.localScale = targetScale * clickScaleFactor;
            return;
        }
        animator.SetTrigger("Open");
        isOpening = true;
        showCardTime = Time.time + 1f;
        CurrencyManager.Instance.Currency -= ROLL_COST;

    }

    //Spits out a Random Monster from the bank
    public void Roll()
    {
        rollSinceLastUnique += 1; // use this for our pity system later
        pityPercentageUp += 0.05f;

        List<MonsterAsset> uncollectedMonsters = new List<MonsterAsset>();

        foreach (var monster in InventoryManager.Instance.Monsters)
        {
            if (!InventoryManager.Instance.Owned[monster.Name])
            {
                uncollectedMonsters.Add(monster);
            }
        }
        
        List<MonsterAsset> Pool = InventoryManager.Instance.Monsters;

        // pity system
        bool isPityRoll = false;
        float numUniqueRatio = 1.0f - (uncollectedMonsters.Count / (float) Pool.Count);
        numUniqueRatio = numUniqueRatio < 0.5 ? 0.0f : numUniqueRatio / 3.0f;
        Debug.Log(numUniqueRatio);
        float randomNumber = Random.Range(0.0f, 1.0f);
        float pityPercentageThreshold = Mathf.Clamp(pityPercentageUp + numUniqueRatio, 0.0f, 0.4f);

        Debug.Log(
            "Pity system stats: " + rollSinceLastUnique + 
            ", Threshold: " + pityThreshold + "; rand: " + randomNumber + 
            " | " + pityPercentageThreshold);

        if (rollSinceLastUnique > pityThreshold || randomNumber < pityPercentageThreshold)
        {
            rollSinceLastUnique = 0;
            pityPercentageUp = 0;
            isPityRoll = true;
            Pool = uncollectedMonsters; // guaranteed unique monster pulled
        }

        //Fuck with probabilities here for rarer items hehe
        int roll = Random.Range(0, Pool.Count);
        Debug.Log("You rolled a: " + roll);

        //Update the player's inventory directly from here

        MonsterAsset pulledMonster = Pool[roll];
        string pullName = pulledMonster.Name;
        ShowCard(pulledMonster.IconSprite);

        if (!InventoryManager.Instance.Owned[pullName])
        {
            if (!isPityRoll) // in the event we get a unique but it wasn't from a pity roll
            {
                rollSinceLastUnique = 0;
                pityPercentageUp = 0;
            }

            Debug.Log("You pulled a: " + pullName);
            InventoryManager.Instance.Pull(pullName);
        } 
        else
        {
            Debug.Log("You already have a " + pullName);
        }
    }


    public void ShowCard(Sprite s) {
        cardImage.GetComponent<Image>().sprite = s;
        cardImage.enabled = true;
        cardImage.transform.rotation = Quaternion.Euler(90,0,0);
        cardImage.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        
        cardButton.GetComponent<CanvasGroup>().alpha = 1;
        cardButton.GetComponent<CanvasGroup>().interactable = true;

        overlayImage.enabled = true;
    }
    
    public void HideCard() {
        cardImage.enabled = false;
        cardButton.GetComponent<CanvasGroup>().alpha = 0;
        cardButton.GetComponent<CanvasGroup>().interactable = false;

        overlayImage.enabled = false;
    }

    private void OnMouseOver()
    {
        mousedOver = true;
    }

    private void OnMouseExit()
    {
        mousedOver = false;
    }

}
