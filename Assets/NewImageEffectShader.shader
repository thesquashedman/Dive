Shader "Unlit/NewImageEffectShader"
{
    Properties
    {
        _Frequency ("Frequency", Float) = 1.0
        _Speed ("Speed", Float) = 1.0
        _Strength ("Strength", Float) = 1.0
        //_MainTex ("Texture", 2D) = "white" {}
        
    }
    SubShader
    {
        // No culling or depth
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            Name "WaveBlit"

            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // The Blit.hlsl file provides the vertex shader (Vert),
            // input structure (Attributes) and output strucutre (Varyings)
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            #pragma vertex Vert
            #pragma fragment frag

            /*
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
            */
            TEXTURE2D(_CameraOpaqueTexture);
            SAMPLER(sampler_CameraOpaqueTexture);
            float _Frequency;
            float _Speed;
            float _Strength;
            

            half4 frag (Varyings i) : SV_Target
            {
                /*
                float2 sinvertex;
                sinvertex.x = sin(((i.texcoord.y * 2 * 3.1415f * _Frequency  + _Time.x * _Speed))) * _Strength ;
                sinvertex.y = sin(((i.texcoord.x * 2 * 3.1415f * _Frequency + (_Time.x + 0.3f) * _Speed))) * _Strength;;
                //sinvertex.y = 0;
                */
                //float4 col = SAMPLE_TEXTURE2D_X(_CameraColorTexture, sampler_CameraColorTexture, sin(i.texcoord + sinvertex));
                float4 col = SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, i.texcoord);
                //float4 col = tex2D(_MainTex, sin(i.uv + sinvertex));
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                return col;
            }
            ENDHLSL
        }
    }
}
