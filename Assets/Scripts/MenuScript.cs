using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject[] uiObjects;

    public void StartGame()
    {
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
