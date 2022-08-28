using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    void Awake()
    {
        //Stop DontDestroyOnLoads from Stacking
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void test() 
    {
        Debug.Log("loading new level");
        SceneManager.LoadScene("TestPenScene");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void loadNewLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    public void returnToGacha()
    {
        Debug.Log("loading new level");
        SceneManager.LoadScene("GachaScene");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("finished loading");
        if (scene.name.Contains("PenScene"))
        {
            //Monster penMon = GameObject.FindGameObjectWithTag("Monster").GetComponent<TestMonster>();
            //penMon.currencyManager = this.currencyManager;
        }
    }
}
