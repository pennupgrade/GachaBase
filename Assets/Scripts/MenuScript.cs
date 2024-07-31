using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject[] uiObjects;

    public void StartGame()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.CanvasGameObject.SetActive(true);
            MainManager.Instance.EventSystemGameObject.SetActive(true);
        }
        
        SceneManager.LoadScene("GachaScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenAbout()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void ReturnToMenu()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.CanvasGameObject.SetActive(false);
            MainManager.Instance.EventSystemGameObject.SetActive(false);
        }

        SceneManager.LoadScene("MenuScreen");
    }

    public void ActivateUI(int id)
    {
        for (int i = 0; i < uiObjects.Length; i++)
        {
            uiObjects[i].SetActive(id == i);
        }
    }
}
