Shader "Custom/UICard"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Corner Radius", Range(0, 0.5)) = 0.12
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100

        Pass
        {
            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
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
            float _Radius;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);

                float2 uv = i.uv;
                float radius = _Radius;

                float2 dist = abs(uv - 0.5) - (0.5 - radius);
                float2 d = max(dist, 0.0);
                float alpha = 1.0 - step(radius, length(d));

                col.a *= alpha;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/VertexLit"
}
