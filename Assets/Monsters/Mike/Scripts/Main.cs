using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public static Main Ins;
    Manager manager;

    //
    public GameObject mikePrefab;
    public GameObject enemyPrefab;
    public GameObject enemyDiePrefab;

    void Start()
    {
        Ins = this;

        Shader.SetGlobalFloat("aspect", 16f / 9f);

        UnityReferences.Initialize();

        manager = new();
        manager.Start();
    }

    void Update()
    {
        manager.Loop();
    }
}

public static class UnityReferences
{

    public static GameObject Mike;
    public static GameObject Enemy;
    public static GameObject EnemyDie;

    public static Camera Camera;

    public static Material SpikeWallMaterial;

    public static void Initialize()
    {
        var m = GameObject.Find("Main").GetComponent<Main>(); 
            (Mike, Enemy, EnemyDie) = (m.mikePrefab, m.enemyPrefab, m.enemyDiePrefab);

        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        SpikeWallMaterial = GameObject.Find("Spike Wall").GetComponent<MeshRenderer>().material;
    }

}