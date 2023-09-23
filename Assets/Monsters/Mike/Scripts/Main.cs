using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    Manager manager;

    //
    public GameObject mikePrefab;
    public GameObject enemyPrefab;

    void Start()
    {
        UnityReferences.Initialize();

        new Mike();
        manager = new();

        Shader.SetGlobalFloat("aspect", 16f / 9f);
    }

    void Update()
    {
        manager.Update();
        manager.Draw();
    }
}

public static class UnityReferences
{

    public static GameObject Mike;
    public static GameObject Enemy;
    public static Camera Camera;

    public static void Initialize()
    {
        var m = GameObject.Find("Main").GetComponent<Main>(); 
            (Mike, Enemy) = (m.mikePrefab, m.enemyPrefab);
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

}