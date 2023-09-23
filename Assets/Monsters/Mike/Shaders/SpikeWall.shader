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

            float aspect;

            /*float sdBox(float2 p, float2 dim)
            {
                p = abs(p) - dim*.5;
                float s = sqrt(max(0., sign(p.x)*p.x*p.x) + max(0., sign(p.y)*p.y*p.y));
                return s + step(s, 0.) * max(p.y, p.x);
            }*/

            //float sb(float d) {return step(d, 0.);}
            float sinpulse(float theta) {return pow(sin(theta), 8.); }

            float _Encroach;

            float sbSpikeFloor(float2 p, float2 fo, float spikeSize, float sharp, float offset, float t, float shiftRand, float oscAmp)
            {
                p = float2(dot(p, fo), dot(p, perp(fo)));
                float2 lp = float2(amod(p.x, spikeSize) - spikeSize*.5, p.y);

                float idx = p.x - lp.x; float rand = hash11(idx)*2.-1.;

                lp.x = abs(lp.x - rand*spikeSize*shiftRand*.5); //*.4 bandaid
                t *= 40.; offset *= (1. + oscAmp*sin(t*(1.+rand*.2)+rand*100.));

                return step(lp.y, .5*(offset - sharp*10. * lp.x));
            }

            float sbSpikeMultiFloor(float2 p, float2 fo, float t)
            {
                float exists = 0.;
                
                float2 o = float2(t, 0.);
                //t *= 2.;

                p = float2(dot(p, fo), dot(p, perp(fo)));

                exists += sbSpikeFloor(p + o*.1, float2(1.,0.), 0.04, .3*1.7, 0.5, t, .1, .1);
                exists += sbSpikeFloor(p - o*.2, float2(1., 0.), 0.09*2., .3, 0.57, t, .4, .1);
                exists += sbSpikeFloor(p + o*.07, float2(1., 0.), 0.49, .4*1.5, 0.63-.04 , t, .3, .23);

                return saturate(exists);
            }

            float sbSpikyCircle(float2 p, float r, float2 params, float t)
            {
                return sb(sdSpikeBall(float2(r - params.y, r + params.y), params.x, p, r*.1, 1., t));
            }

            fixed4 frag (vOut i) : SV_Target
            {
                
                i.uv = i.uv*2.-1.; i.uv.x *= aspect;
                float2 p = i.uv; p = -p;
                
                //
                float exists = 0.;

                // temp, will be controlled cpu later
                _Encroach = .2 + .05* _Time.y; //.5

                //
                float t = _Time.y*.004*50.;

                float2 dim = float2(aspect*2., 2.) - _Encroach;
                //float2 dsp = p*2. + float2(t, 1.9*t);
                //dim.x += .04* (sinpulse(dsp.y * 200.+5.) + 1.8*sinpulse(dsp.y*100. + 40.) + pow(sin(dsp.y*500.), 8.));
                //dim.y += .04* (sinpulse(dsp.x * 180.+500.) + 1.8*sinpulse(dsp.x*120. + 400.) + pow(sin(dsp.x*430.+100.), 8.));
                //exists = 1.-sb(sdBox(p, dim));

                exists += sbSpikeMultiFloor(p + dim*.5, float2(1., 0.), t);
                exists += sbSpikeMultiFloor(p - dim*.5, float2(-1., 0.), t+10.);
                exists += sbSpikeMultiFloor(p - dim*.5, float2(0., 1.), t+20.);
                exists += sbSpikeMultiFloor(p + dim*.5, float2(0., -1.), t+30.);
                
                //no abs cuz i want slightly unique
                exists += sbSpikyCircle(p + dim*.5, .4, float2(160., .01), t*.2);
                exists += sbSpikyCircle(p - .5 * float2(-dim.x, dim.y), .4, float2(170., .01), t*.2);
                exists += sbSpikyCircle(p - .5 * float2(dim.x, -dim.y), .4, float2(180., .01), t*.2);
                exists += sbSpikyCircle(p - .5 * float2(dim.x, dim.y), .4, float2(190., .01), t*.2);

                return float4(1., 1., 1., saturate(exists));

            }
            ENDCG
        }
    }
}
