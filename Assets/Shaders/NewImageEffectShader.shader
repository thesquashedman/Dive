Shader "Unlit/NewImageEffectShader"
{
    Properties
    {
        _Frequency ("Frequency", Float) = 1.0
        _Speed ("Speed", Float) = 1.0
        _Strength ("Strength", Float) = 1.0
        _MainTex ("Texture", 2D) = "white" {}
        
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Frequency;
            float _Speed;
            float _Strength;
            

            fixed4 frag (v2f i) : SV_Target
            {
                float2 sinvertex;
                sinvertex.x = sin(((i.uv.y * 2 * 3.1415f * _Frequency  + _Time.x * _Speed))) * _Strength ;
                sinvertex.y = sin(((i.uv.x * 2 * 3.1415f * _Frequency + (_Time.x + 0.3f) * _Speed))) * _Strength;;
                //sinvertex.y = 0;
                
                fixed4 col = tex2D(_MainTex, sin(i.uv + sinvertex));
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
