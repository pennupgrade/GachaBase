using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadNewLevel()
    {
        Debug.Log("loading new level");
        SceneManager.LoadScene("TestPenScene");
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
