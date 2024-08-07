using UnityEngine.SceneManagement;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    private static MainManager instance;
    public static MainManager Instance => MainManager.instance;

    public GameObject backButton;
    public GameObject CanvasGameObject;
    public GameObject EventSystemGameObject;
    
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

        if (scene.name == "GachaScene")
        {
            backButton.SetActive(false);
        }
        else
        {
            backButton.SetActive(true);
        }
    }
}
