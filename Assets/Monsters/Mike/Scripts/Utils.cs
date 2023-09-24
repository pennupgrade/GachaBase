using UnityEngine;
using Unity.Mathematics;

using static Unity.Mathematics.math;

public static class Utils
{

    public const float TAU = Mathf.PI * 2f;

    public static Mesh QuadMesh = new ()
    {
        vertices = new Vector3[4]
        {
            new(-1f, -1f, 0f),
            new(1f, -1f, 0f),
            new(-1f, 1f, 0f),
            new(1f, 1f, 0f)
        },
        uv = new Vector2[4]
        {
            new(-1f, -1f),
            new(1f, -1f),
            new(-1f, 1f),
            new(1f, 1f)
        }
    };

    public static float amod(float f, float m)
    {
        float s = fmod(abs(f), m);
        return s + step(f, 0f) * (m - 2f * s);
    }

    public static float pows(float f, float p)
        => sign(p) * pow(abs(f), p);

    public static float atanP(float2 p)
        => amod(atan2(p.y, p.x), TAU);

    public static float2 toPolar(float2 p)
        => float2(length(p), atanP(p));

    public static float2 toCartesian(float2 p)
        => p.x * float2(cos(p.y), sin(p.y));

    //can be changed to return pos can use distance and get the perp to closest point and use that ig
    public static bool CheckCollisionSpikeball(float2 p, float pR, float2 shapePos, int sideCount, float rotOffset, float r)
    {
        p = p - shapePos;

        float size = TAU / sideCount;
        float2 polar = toPolar(p);
        polar.y = fmod(polar.y, size) - size * .5f;

        float2 lp = toCartesian(polar);

        float2 sp = toCartesian(float2(r, size * .5f));
        float2 ep = toCartesian(float2(r, -size * .5f));

        float2 dir = normalize(ep - sp);
        float2 up = float2(-dir.y, dir.x);

        lp -= sp;
        lp = float2(dot(lp, dir), dot(lp, up));

        return lp.y - pR < 0;
    }

    public static float3 xyz(this float2 v, float z = 0f) => float3(v.x, v.y, z);
    public static float2 xy(this Vector3 v) => float2(v.x, v.y);
    public static float2 xy(this float3 v) => float2(v.x, v.y);

    public static float vmin(float2 v) => min(v.x, v.y);

}