using Unity.Mathematics;

public class Enemy : AObject
{

    public Enemy(float2 startPos, float2 initialVelocity, float2 acceleration)
    {
        //create prefab
    }

    float2 p, v, a;
    public override void Update(InputData input, float dt)
    {
        p += v * dt + .5f * dt * dt * a;
        v += dt * a;
    }

    public override void DrawUpdate()
    {
        
    }
}