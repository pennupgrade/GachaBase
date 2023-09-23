using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

using static Utils;

public class Mike : AObject
{

    public static IReadOnlyList<Mike> Mikes;
    static readonly List<Mike> mikes = new(); //connected in order of creation

    public Mike()
    {
        mikes.Add(this);
        position = new();
        (mikeTransform = GameObject.Instantiate(UnityReferences.Mike).transform)
            .localPosition = position.xyz();
    }
    
    float2 position;
    Transform mikeTransform;

    public override void DrawUpdate()
        => mikeTransform.position = (Vector3)position.xyz(1f);

    float2 mouseToMike;
    bool grabbed;

    bool Check(float2 p)
        => CheckCollisionSpikeball(p, position, 8, math.sin(Time.timeSinceLevelLoad), .58f*5 * .4f);

    public override void Update(InputData input, float dt)
    {

        bool hover = Check(input.mousePosition);
        grabbed =  (hover && input.lmbp) || (grabbed && input.lmb);

        if (input.lmbp && hover)
            mouseToMike = position - input.mousePosition;
        if (grabbed)
            position = input.mousePosition + mouseToMike;
        
    }

}