Shader "Unlit/MikeGame/SpikeWall"
{
    Properties
    {
        _Encroach ("Encroach", Float) = .5
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent"} 
        LOD 100
        ZWrite Off

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            float aspect;

            float sdBox(float2 p, float2 dim)
            {
                p = abs(p) - dim*.5;
                float s = sqrt(max(0., sign(p.x)*p.x*p.x) + max(0., sign(p.y)*p.y*p.y));
                return s + step(s, 0.) * max(p.y, p.x);
            }

            float sb(float d) {return step(d, 0.);}
            float sinpulse(float theta) {return pow(sin(theta), 8.); }

            float _Encroach;

            fixed4 frag (vOut i) : SV_Target
            {
                
                i.uv = i.uv*2.-1.; i.uv.x *= aspect;
                float2 p = i.uv;
                
                //
                float exists = 0.;

                //
                _Encroach = .5 + .05* _Time.y;

                //
                float t = _Time.y*.004;

                float2 dim = float2(aspect*2., 2.) - _Encroach;
                float2 dsp = p*2. + float2(t, 1.9*t);
                dim.x += .04* (sinpulse(dsp.y * 200.+5.) + 1.8*sinpulse(dsp.y*100. + 40.) + pow(sin(dsp.y*500.), 8.));
                dim.y += .04* (sinpulse(dsp.x * 180.+500.) + 1.8*sinpulse(dsp.x*120. + 400.) + pow(sin(dsp.x*430.+100.), 8.));
                exists = 1.-sb(sdBox(p, dim));

                return float4(1., 1., 1., exists);

            }
            ENDCG
        }
    }
}
