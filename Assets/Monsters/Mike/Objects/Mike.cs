using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

using static Utils;

public class Mike : AObject
{

    public static IReadOnlyList<Mike> Mikes => mikes;
    static readonly List<Mike> mikes = new(); //connected in order of creation

    public Mike()
    {
        mikes.Add(this);
        position = new();
        
        var o = GameObject.Instantiate(UnityReferences.Mike);
        (mikeTransform = o.transform)
            .localPosition = position.xyz();
        m = o.GetComponent<SpriteRenderer>().material;
    }

    public float2 Position => position;
    float2 position;
    Transform mikeTransform;

    Material m;
    public override void DrawUpdate()
    {
        mikeTransform.position = (Vector3)position.xyz(1f);
        m.SetFloat("_ShieldRotation", ShieldTheta.x);
    }

    float2 mouseToMike;
    bool grabbed;

    public float R => .58f * 4 * .4f;
    public bool Check(float2 p, float r = 0f)
        => CheckCollisionSpikeball(p, r, position, 8, math.sin(Time.timeSinceLevelLoad), R);

    public override void Update(InputData input, float dt)
    {

        bool hover = Check(input.mousePosition);
        grabbed =  (hover && input.lmbp) || (grabbed && input.lmb);

        if (input.lmbp && hover)
            mouseToMike = position - input.mousePosition;
        if (grabbed)
            position = input.mousePosition + mouseToMike;

        ShieldTheta.x += (input.rmb ? 3f : 0.5f) * dt;

    }

    //public float2 ShieldTheta => new float2(1.2f * Time.timeSinceLevelLoad + 10f + math.sin(Time.timeSinceLevelLoad), 1.1f*1.5f*1.5f*1f * 1.5f);
    public float2 ShieldTheta = new float2(0f, 1.1f * 1.5f * 1.5f * 1f * 1.5f);

}