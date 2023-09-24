Shader "Unlit/MikeGame/MikeSprite"
{
    Properties
    {
        _ShieldRotation ("Shield Rotation", Float) = 0.
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
            #include "MikeHelper.cginc"

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

            float _ShieldRotation;

            fixed4 frag (vOut i) : SV_Target
            {
                //_ShieldRotation = 0.;
                return render(i.uv*2.-1., _ShieldRotation);

            }
            ENDCG
        }
    }
}
