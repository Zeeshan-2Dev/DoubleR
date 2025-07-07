// CurvedWorldURP.shader
Shader "Custom/URP/CurvedWorld (Unlit)"
{
    Properties
    {
        _Tint       ("Color Tint", Color) = (1,1,1,1)
        _MainTex    ("Base (RGB)", 2D)    = "white" {}
        _Curvature  ("Curvature",  Float) = 0.001
    }

    SubShader
    {
        // Tell Unity this pass is meant for the Universal Render Pipeline
        Tags{ "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" }

        Pass
        {
            // ----------‑– HLSL ‑–----------
            HLSLPROGRAM
            #pragma vertex   Vert
            #pragma fragment Frag
            #pragma target   3.0       // same as before

            // Core URP include gives us transform helpers
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // ----------‑– Properties ‑–----------
            TEXTURE2D(_MainTex);            SAMPLER(sampler_MainTex);
            float4       _MainTex_ST;
            float4       _Tint;
            float        _Curvature;

            // ----------‑– Shader I/O ‑–----------
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
            };

            // ----------‑– Vertex – apply curvature ‑–----------
            Varyings Vert (Attributes v)
            {
                Varyings o;

                // Object → world space
                float3 worldPos = TransformObjectToWorld(v.positionOS.xyz);

                // Distance from camera in world Z
                float dz = worldPos.z - _WorldSpaceCameraPos.z;

                // Quadratic bend downward (negative Y) by curvature factor
                worldPos.y += -(dz * dz) * _Curvature;

                // World → homogeneous clip space
                o.positionHCS = TransformWorldToHClip(worldPos);

                // Standard UV transform macro so tiling/offset still work
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            // ----------‑– Fragment – tint & texture ‑–----------
            half4 Frag (Varyings i) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * _Tint;
                return col;
            }
            ENDHLSL
        }
    }

    // FallBack is unnecessary in URP but keeps inspector tidy in Built‑in
    Fallback Off
}
