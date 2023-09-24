using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;
using static Utils;

public class Enemy : AObject, IEnemyCollider
{

    static Shader s = Shader.Find("Unlit/MikeGame/Enemy");
    Material m;
    GameObject obj;

    public Enemy(float2 startPos, float2 initialVelocity, float aMag)
    {
        IEnemyCollider.enemyColliders.Add(this);

        m = new(s); m.SetFloat("_Seed", UnityEngine.Random.Range(-100f, 100f));
        (obj = GameObject.Instantiate(UnityReferences.Enemy)).GetComponent<SpriteRenderer>().material = m;

        p = startPos; v = initialVelocity; 
        a = aMag * normalize(normalize(initialVelocity) + .3f * (float2) UnityEngine.Random.insideUnitCircle);

        cleanupTimer = new(10f, Kill);
    }

    Timer cleanupTimer;
    float2 p, v, a;
    public override void Update(InputData input, float dt)
    {
        p += v * dt + .5f * dt * dt * a;
        v += dt * a;

        cleanupTimer.Update(dt);
        mm = input.mousePosition;
    }

    public override void DrawUpdate()
    {
        obj.transform.position = p.xyz(0.5f);
        m.SetVector("_Direction", (Vector2) normalize(v));
    }

    void Kill()
    {
        GameObject.Destroy(obj);
        IEnemyCollider.delQueue.Add(this); //enumeration problem whtaever
        Destroy();
    }

    float R => 1.7f * 0.1f * 0.4f * 0.1f; float2 mm;
    public bool CheckCollision(Mike mike)
    {
        //Debug.Log(amod(mike.ShieldTheta.x, TAU) / PI);
        if (!mike.Check(p, R)) return false;

        float o = atanP(p - mike.Position);

        float lo = amod(o - amod(mike.ShieldTheta.x, TAU), TAU);
        lo += step(PI, lo) * (TAU - 2f * lo);

        if (lo < mike.ShieldTheta.y * .5f)
        { Kill(); new EnemyDie(p); God.Ins.Expand(); CurrencyManager.Instance.Currency += 2f; return false; }
        //Debug.Log(o);
        //return false;
        return true;
  
    }

    public override void End()
        => Kill();

}

public interface IEnemyCollider
{

    public static List<IEnemyCollider> enemyColliders = new();
    public static List<IEnemyCollider> delQueue = new();
    public bool CheckCollision(Mike mike);

}