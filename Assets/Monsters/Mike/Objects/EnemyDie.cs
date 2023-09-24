using Unity.Mathematics;
using UnityEngine;

public class EnemyDie : AObject
{

    static Shader s = Shader.Find("Unlit/MikeGame/EnemyDie");

    float2 p;
    GameObject o;
    Material m;
    public EnemyDie(float2 p)
    {
        m = new(s);

        this.p = p;

        (o = GameObject.Instantiate(UnityReferences.EnemyDie)).transform.position = p.xyz(0.7f);
        (o.GetComponent<SpriteRenderer>().material = m).SetFloat("_Interpolation", interpolation);
    }

    const float dieTime = 0.42f;
    float interpolation = 0.12f;
    public override void Update(InputData input, float dt)
    {
        base.Update(input, dt);
        interpolation += dt / dieTime;

        if(interpolation >= 1f)
        {
            GameObject.Destroy(o);
            Destroy();
        }
    }

    public override void DrawUpdate()
        => m.SetFloat("_Interpolation", interpolation);

    public override void End()
        => GameObject.Destroy(o);

}