using System.Linq;
using UnityEngine;
using UnityEditor;

/**
* This is a class for initializing the MainManager.
* Makes sure you don't double add the MainManager.
* Add this to all scenes for testing.
*/
public class InitManager : MonoBehaviour
{
    void Awake()
    {
        var asset = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/MainManager.prefab");
        var gameObject = Object.FindObjectsOfType<MainManager>().FirstOrDefault();
        if (gameObject == null)
        {
            var obj = GameObject.Instantiate(asset);
            obj.name = "MainManager";
            Debug.Log("Added new MainManager prefab instance to scene");
        }
        else
        {
            Debug.Log("MainManager already exists on scene");
        }
    }
}
