Shader "Unlit/MikeGame/EnemyDie"
{
    Properties
    {
        _Interpolation ("Interpolation", Float) = 0.
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent"} 
        LOD 100
        ZWrite Off
        Cull Off

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Common.hlsl"

            struct vIn
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vOut
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            vOut vert (vIn v)
            {
                vOut o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            float _Interpolation;

            float sdRing(float2 p, float r, float width)
            {
                return abs(length(p) - r) - width*.5f;
            }

            float sdMultiAngledLines(float2 p, float domainCutCount, float thetaWidth, float thetaOffset)
            {
                float2 polar = toPolar(p);
                float rTheta = amod(polar.y + thetaOffset, TAU / domainCutCount) - .5 * TAU / domainCutCount;

                float angDist = -abs(amod(rTheta, TAU) - PI) + PI;
                return (angDist - thetaWidth*.5) * polar.x;
            }

            fixed4 frag (vOut i) : SV_Target
            {
                float ii = pow(_Interpolation, 0.42);

                //
                float2 p = i.uv*2.-1.;
                float t = .5 * (_Time.y+ii);
                float randSeed = hash11(0.);
                

                float exists = sb(max(max(
                    sdSpikeBall(ii*.7, 3., p, 0., 0., t), 
                    -sdSpikeBall(ii*.6, 3., p, 0., 0., t)),
                    sdMultiAngledLines(p, 3., 1.0*3.141592/3., PI/3.-(t * (randSeed * 4. - 2.) + randSeed * 100.)*2.)
                    ));//float exists = sb(sdRing(p, ii*.7, .04));
                float alpha = exists*smoothstep(1., .7, ii);

                return float4(exists, exists, exists, alpha);

            }
            ENDCG
        }
    }
}
