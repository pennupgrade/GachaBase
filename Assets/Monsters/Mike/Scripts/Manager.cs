using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Linq;

public class Manager
{

    InputData input = new();
    public void Start()
        => new God();

    bool enabled = true;
    public void GameOver()
    {
        Debug.Log("Game Over " + Time.timeSinceLevelLoad);
        enabled = false;
    }

    public void Loop()
    {
        if (!enabled) return;

        input.Update();
        foreach (var obj in AObject.Objects)
            obj.Update(input, Time.deltaTime);

        AObject.DoQueues();

        foreach (var obj in AObject.Objects)
            obj.DrawUpdate();

        foreach (Mike m in Mike.Mikes)
        {
            foreach (var e in IEnemyCollider.enemyColliders)
                if (e.CheckCollision(m)) { GameOver(); }
        }
    }

}

public abstract class AObject
{

    public static IReadOnlyList<AObject> Objects => objects;
    
    static readonly List<AObject> objects = new();
    static readonly List<AObject> objectQueue = new();
    public AObject() => objectQueue.Add(this);

    public static void DoQueues()
    {
        if (objectQueue.Count != 0)
            foreach (var o in objectQueue) objects.Add(o); objectQueue.Clear();

        if(delQueue.Count != 0)
            foreach (var o in delQueue) objects.Remove(o); delQueue.Clear();
    }

    // quick system
    public virtual void DrawUpdate() { }
    public virtual void Update(InputData input, float dt) { }
    //public virtual void CheckCollision() { } //this one is prolly gonna be static and stuff  

    static readonly List<AObject> delQueue = new();
    protected void Destroy() => delQueue.Add(this);

}

public struct InputData
{

    public float2 mousePosition;
    
    public bool lmb;
    public bool lmbp;
    public bool lmbu;

    public bool rmb;
    public bool rmbp;
    public bool rmbu;

    public void Update()
    {
        lmb = Input.GetMouseButton(0);
        lmbp = Input.GetMouseButtonDown(0);
        lmbu = Input.GetMouseButtonUp(0);

        rmb = Input.GetMouseButton(1);
        rmbp = Input.GetMouseButtonDown(1);
        rmbu = Input.GetMouseButtonUp(1);

        mousePosition = UnityReferences.Camera.ScreenToWorldPoint(Input.mousePosition).xy();
    }

}