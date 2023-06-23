Shader "Unlit/NewImageEffectShader1"
{
    Properties
    {
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

                float2 pixelToWorld;
                pixelToWorld.y = (5 * (i.uv.y - 0.5)) * 2 + _WorldSpaceCameraPos.y; //Replace 5 with the orthoraphic camera size
                pixelToWorld.x = (_ScreenParams.x / _ScreenParams.y * 5 * (i.uv.x - 0.5)) * 2 + _WorldSpaceCameraPos.y; //Replace 5 with the orthoraphic camera size

                fixed4 col = tex2D(_MainTex, i.uv);
                col.r *= frac(pixelToWorld.x); //Multiply the colors by the decimal part of the world coordinate. This should result in red bands whenever the x is near 1
                col.b *= frac(pixelToWorld.y); //Multiply the colors by the decimal part of the world coordinate. This should result in blue bands whenever the y is near 1
                
                
                return col;
                
            }
            
            ENDCG
        }
    }
}
