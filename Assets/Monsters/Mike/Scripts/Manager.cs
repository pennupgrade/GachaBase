using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Linq;
using UnityEditor.SearchService;

using UnityEngine.SceneManagement;

public class Manager
{

    public static float ElapsedTime;

    InputData input = new();
    public void Start()
    { enabled = true; new God(); ElapsedTime = 0f; }

    bool enabled = true;
    public void GameOver()
    {
        //Debug.Log("Game Over " + Manager.ElapsedTime);
        enabled = false;

        IEnumerator WaitRestart() 
        { 
            yield return new WaitForSeconds(1f); 
            End(); Start(); 
        }

        Main.Ins.StartCoroutine(WaitRestart());
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

        if(IEnemyCollider.delQueue.Count != 0)
        {
            foreach (var e in IEnemyCollider.delQueue)
                IEnemyCollider.enemyColliders.Remove(e);
            IEnemyCollider.delQueue.Clear();
        }
        
        ElapsedTime += Time.deltaTime;
    }

    public void End()
        => AObject.EndAll();

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
        { foreach (var o in objectQueue) objects.Add(o); objectQueue.Clear(); }

        if (delQueue.Count != 0)
        { foreach (var o in delQueue) objects.Remove(o); delQueue.Clear(); }
    }

    // quick system
    public virtual void DrawUpdate() { }
    public virtual void Update(InputData input, float dt) { }
    //public virtual void CheckCollision() { } //this one is prolly gonna be static and stuff  

    static readonly List<AObject> delQueue = new();
    protected void Destroy() => delQueue.Add(this);

    public static void EndAll()
    {
        foreach (var obj in objects)
            obj.End();
        objects.Clear();
    }
    public virtual void End() { }

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

    public bool space;

    public void Update()
    {
        lmb = Input.GetMouseButton(0);
        lmbp = Input.GetMouseButtonDown(0);
        lmbu = Input.GetMouseButtonUp(0);

        rmb = Input.GetMouseButton(1);
        rmbp = Input.GetMouseButtonDown(1);
        rmbu = Input.GetMouseButtonUp(1);

        space = Input.GetKeyDown(KeyCode.Space);

        mousePosition = UnityReferences.Camera.ScreenToWorldPoint(Input.mousePosition).xy();
    }

}