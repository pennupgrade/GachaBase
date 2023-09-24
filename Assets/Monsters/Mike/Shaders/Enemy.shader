Shader "Unlit/MikeGame/Enemy"
{
    Properties
    {
        _Direction ("Direction", Vector) = (1., 0., 0., 0.)
        _Seed ("Seed", Float) = 0.
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

            float2 _Direction;
            float _Seed;

            float sdHighlight(float2 p, float2 dDim, float2 hDim, float t)
            {
                float2 lp = p;

                lp.x = amod(lp.x-t, dDim.x) - dDim.x*.5;
                lp.y = abs(lp.y);
                
                lp = abs(lp - float2(0., dDim.y*.5));

                return sdBox(lp, float2(1.,0.), hDim);
            }

            float sdTriObject(float2 p)
            {
                return max(
                    sdRightTri(p, float2(0., 0.), float2(.4-.1, .5)),
                    -sdRightTri(p, float2(0., 0.), float2(.38-.1, .25))
                    );
            }

            float sdAngledLines(float2 p, float startTheta, float thetaWidth)
            {
                float angDist = -abs(amod(toPolar(p).y-startTheta, TAU) - PI) + PI;
                return (angDist - thetaWidth*.5) * length(p);
            }

            float sdMultiAngledLines(float2 p, float domainCutCount, float thetaWidth, float thetaOffset)
            {
                float2 polar = toPolar(p);
                float rTheta = amod(polar.y + thetaOffset, TAU / domainCutCount) - .5 * TAU / domainCutCount;

                //float id = polar.y - rTheta;
                //thetaWidth *= (1. - .2 * smoothstep(.6, .3, length(id)));

                float angDist = -abs(amod(rTheta, TAU) - PI) + PI;
                return (angDist - thetaWidth*.5) * polar.x;
            }

            fixed4 frag (vOut i) : SV_Target
            {
                float t = _Time.y + _Seed * 100.;
                
                float2 p = i.uv*2.-1.; 

                float2 fo = _Direction;
                float2 up = perp(fo);
                p = float2(dot(p, fo), dot(p, up)); 

                float exists = 0.;

                float2 lp = p - float2(-.2, .0); lp = lp.yx * float2(-1.,1.); lp.x = abs(lp.x);
                //float d = sdTri(lp, float2(-.2, .0), float2(.8, 0.), float2(-.5, .8));
                float d = sdTriObject(lp);
                d = min(d, max(sdHighlight(p, float2(.4, .46-.1), float2(.24, .06), -t), sdRightTri(lp, float2(0.,-1.), float2(1.6,1.25))));

                float dOuter = max(sdTriObject(lp / 1.2), -sdTriObject(lp / 1.1));
                dOuter = max(dOuter, sdMultiAngledLines(p - float2(-.2, 0.), 4., .6, 2.*t));
                exists += sb(min(d, dOuter));
                //exists += step(abs(d - .1), .05);

                exists = saturate(exists);

                float3 col = float3(exists, exists, exists);
                float alpha = exists*smoothstep(-.8-.1, -.3-.1, p.x);

                //

                return float4(col, alpha);

            }
            ENDCG
        }
    }
}
