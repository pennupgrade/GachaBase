using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    Manager manager;

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

    public static Transform Mike;
    public static Camera Camera;

    public static void Initialize()
    {
        Mike = GameObject.Find("Mike").GetComponent<Transform>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

}