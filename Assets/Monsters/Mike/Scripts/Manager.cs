using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Manager
{

    InputData input = new();
    public void Update()
    {
        input.Update();
        foreach (var obj in AObject.Objects)
            obj.Update(input);
    }

    public void Draw()
    {
        foreach (var obj in AObject.Objects)
            obj.Draw();
    }

}

public abstract class AObject
{

    public static IReadOnlyList<AObject> Objects => objects;
    
    static readonly List<AObject> objects = new();
    public AObject() => objects.Add(this);

    // quick system
    public virtual void Draw() { }
    public virtual void Update(InputData input) { }
    public virtual void CheckCollision() { } //this one is prolly gonna be static and stuff  

}

public struct InputData
{

    public float2 mousePosition;
    
    public bool lmb;
    public bool lmbp;
    public bool lmbu;

    public void Update()
    {
        lmb = Input.GetMouseButton(0);
        lmbp = Input.GetMouseButtonDown(0);
        lmbu = Input.GetMouseButtonUp(0);

        mousePosition = UnityReferences.Camera.ScreenToWorldPoint(Input.mousePosition).xy();
    }

}