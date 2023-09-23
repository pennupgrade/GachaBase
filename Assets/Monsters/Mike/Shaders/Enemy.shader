Shader "Unlit/MikeGame/Enemy"
{
    Properties
    {
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

            float sdHighlight(float2 p, float2 dDim, float2 hDim, float t)
            {
                float2 lp = p;

                lp.x = amod(lp.x-t, dDim.x) - dDim.x*.5;
                lp.y = abs(lp.y);
                
                lp = abs(lp - float2(0., dDim.y*.5));

                return sdBox(lp, float2(1.,0.), hDim);
            }

            fixed4 frag (vOut i) : SV_Target
            {
                float2 p = i.uv*2.-1.; 

                float2 fo = float2(1.,0.);//_Direction;
                float2 up = perp(fo);
                //p = float2(dot(p, fo), dot(p, up)); 

                float exists = 0.;

                float2 lp = p - float2(-.2, .0); lp = lp.yx * float2(-1.,1.); lp.x = abs(lp.x);
                //float d = sdTri(lp, float2(-.2, .0), float2(.8, 0.), float2(-.5, .8));
                float d = sdRightTri(lp, float2(0., 0.), float2(.4-.1, .5));
                d = max(d, -sdRightTri(lp, float2(0., 0.), float2(.38-.1, .25)));
                d = min(d, max(sdHighlight(p, float2(.4, .35), float2(.24, .06), -_Time.y), sdRightTri(lp, float2(-1.,0.), float2(4.,2.))));

                exists += sb(d);

                //

                return float4(exists, exists, exists, exists);

            }
            ENDCG
        }
    }
}
