Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _Intensity ("Glow Intensity", Range(0, 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float _Intensity;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texcol = tex2D(_MainTex, i.texcoord) * _Color;
                texcol.rgb *= _Intensity; // Increase intensity of the color
                texcol.rgb = saturate(texcol.rgb); // Limit color values to [0, 1]
                texcol.a = 1.0; // Ensure alpha is set to 1 to prevent unwanted edges
                return texcol;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
