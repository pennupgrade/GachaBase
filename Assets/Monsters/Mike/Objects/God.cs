using System;
using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;
using static Utils;

// Doesn't update or draw objects like Manager, but will create them
public class God : AObject, IEnemyCollider
{

    public static God Ins;

    Timer timer;

    public God()
    {
        Ins = this;

        IEnemyCollider.enemyColliders.Add(this);

        new Mike();
        timer = new(2f, SpawnSpikeBall); SpawnSpikeBall();
    }

    float wallSize = -0.2f;
    float wallExpandV = 0f; float wallExpandA = -2.0f; public void Expand() => wallExpandV = .89f*1f;
    public override void Update(InputData input, float dt)
    {
        //wallSize = 0.1f + sin(Time.timeSinceLevelLoad*0.1f*10f) * .2f;
        timer.Update(dt);
        timer.ChangeMaxTime(-0.04f * dt * exp(-pow(Manager.ElapsedTime*0.05f, 2.0f)));

        wallSize += (0.04f * (0.5f + 1.3f*(1f-exp(-Manager.ElapsedTime* 0.05f)))) * dt;
        wallSize -= wallExpandV * dt; wallExpandV = max(0f, wallExpandV + wallExpandA * dt);
        wallSize = max(-0.2f, wallSize);
    }

    public override void DrawUpdate()
    {
        UnityReferences.SpikeWallMaterial.SetFloat("_Encroach", wallSize+0.1f);
    }

    float2 boxDim => (5f * ((1f + float2(.6f*smoothstep(0.8f, -0.1f, wallSize), .4f*smoothstep(0.3f, -0.1f, wallSize))) *float2(16f / 9f, 1f) - wallSize)) + 0.85f;

    void SpawnSpikeBall() //bad
    {
        //Debug.Log("spawning spike");

        float2 p = float2(UnityEngine.Random.Range(-boxDim.x * .5f, boxDim.x * .5f), UnityEngine.Random.Range(-boxDim.y * .5f, boxDim.y * .5f));
        float2 dist = boxDim * .5f - abs(p);
        if (dist.x <= dist.y)
            p = float2(sign(p.x) * boxDim.x * .5f, p.y);
        else
            p = float2(p.x, sign(p.y) * boxDim.y * .5f);

        float2 v = UnityEngine.Random.Range(0.6f, 2f) * normalize(normalize(-p) + .2f* (float2) UnityEngine.Random.insideUnitCircle);

        new Enemy(p*2f, v, 0.4f);

    }

    public bool CheckCollision(Mike mike)
    {
        float3 xyr = float3(mike.Position.x, mike.Position.y, mike.R - 0.05f);
        float dist = vmin(boxDim*.5f - abs(xyr.xy()));
        if (dist - xyr.z < 0f) Debug.Log("Spike wall hit");
        return dist - xyr.z < 0f;
    }

}

public struct Timer
{
    float2 t; // <time, maxTime>
    Action loopAct;
    public Timer(float t, Action loopAct)
    { this.t = t; this.loopAct = loopAct; }
    public void ChangeMaxTime(float change) => t.y += change;
    public void Update(float dt)
    {
        t.x -= dt;
        if ( t.x <= 0f )
        { loopAct(); t.x = t.y; }
    }
}