using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public static GameManager instance;
    private float timeCount = 0;
    private bool spawned;
    public GameObject dragon;


    [SerializeField] private GameObject leftArrowPrefab;
    [SerializeField] private GameObject rightArrowPrefab;
    [SerializeField] private GameObject upArrowPrefab;
    [SerializeField] private GameObject downArrowPrefab;
    [SerializeField] private GameObject fxPrefab;

    [SerializeField] private GameObject leftSpawn;
    [SerializeField] private GameObject rightSpawn;
    [SerializeField] private GameObject upSpawn;
    [SerializeField] private GameObject downSpawn;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource missSound;
    [SerializeField] private Sprite hitImg;
    [SerializeField] private Sprite missImg;



    // Start is called before the first frame update
    void Start()
    {
        theMusic.Play();
        instance = this;
    }

    void ChangeBackToIdleAnimation()
    {
        Debug.Log("gone back");
    }

    // Update is called once per frame
    void Update()
    {
        
        timeCount += Time.deltaTime;
        if(Mathf.FloorToInt(timeCount % 60)%2 == 0) { // Some multiple, will change later
            if (!spawned) { //Prevents multiple arrows being spawned 
                int spawnInt = Random.Range(1, 5);
                if (spawnInt == 1){ //Spawn left
                    GameObject newNote = Instantiate(leftArrowPrefab, leftSpawn.transform.position, Quaternion.identity);
                } else if (spawnInt == 2){
                    GameObject newNote = Instantiate(rightArrowPrefab, rightSpawn.transform.position, Quaternion.identity);
                } else if (spawnInt == 3) {
                    GameObject newNote = Instantiate(upArrowPrefab, upSpawn.transform.position, Quaternion.identity);
                } else {
                    GameObject newNote = Instantiate(downArrowPrefab, downSpawn.transform.position, Quaternion.identity);
                }
                spawned = true;
            }
        }
        else
        {
            spawned = false; 
        }

    }

    public void NoteHit(Transform t) { 
        CurrencyManager.Instance.Currency++; // Adding currency

        //Handling Movement
        if (Input.GetKeyDown(KeyCode.A))
        {
            dragon.GetComponent<Dragon>().danceLeft();
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            dragon.GetComponent<Dragon>().danceRight();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            dragon.GetComponent<Dragon>().danceUp();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dragon.GetComponent<Dragon>().danceDown();
        }
        //StartCoroutine(waitForSeconds(10));
        //dragon.GetComponent<Dragon>().stopMove();

        // Handling SFX
        GameObject newFx = Instantiate(fxPrefab, t.position, Quaternion.identity);
        newFx.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 1f, 0);
        newFx.GetComponent<SpriteRenderer>().sprite = hitImg;
        hitSound.Play();
    }
    public void NoteMissed(Transform t)
    {
        GameObject newFx = Instantiate(fxPrefab, t.position, Quaternion.identity);
        newFx.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 2, 0);
        newFx.GetComponent<SpriteRenderer>().sprite = missImg;
        missSound.Play();
        if (CurrencyManager.Instance.Currency > 0)
        {  //Functionality to assure currency never goes below 0. 
            CurrencyManager.Instance.Currency--;
        }
        }
    IEnumerator waitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}

