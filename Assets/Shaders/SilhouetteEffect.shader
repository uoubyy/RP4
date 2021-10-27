Shader "Custom/SilhouetteEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NearColour ("Near Clip Colour", Color) = (0.75, 0.35, 0, 1)
        _FarColour  ("Far Clip Colour", Color)  = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _NearColour;
            fixed4 _FarColour;

            sampler2D _CameraDepthTexture;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                float depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uv));

                // give objects close the near plane more color space
                // depth = pow(Linear01Depth(depth), 0.75f);
                return lerp(_NearColour, _FarColour, depth);
            }
            ENDCG
        }
    }
}
