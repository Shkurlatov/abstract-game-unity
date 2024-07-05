Shader "Custom/UICard"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Corner Radius", Range(0, 0.5)) = 0.12
        _Alpha ("Alpha", Range(0, 1.0)) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
        LOD 100

        Pass
        {
            Cull Back
            Lighting Off
            ZWrite Off
            ZTest [unity_GUIZTestMode]
            Blend SrcAlpha OneMinusSrcAlpha
            Fog { Mode Off }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Radius;
            float _Alpha;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv) * i.color;

                float2 uv = i.uv;
                float radius = _Radius;

                float2 dist = abs(uv - 0.5) - (0.5 - radius);
                float2 d = max(dist, 0.0);
                float alpha = 1.0 - step(radius, length(d));

                col.a *= alpha * _Alpha; // Apply the global alpha

                return col;
            }
            ENDCG
        }
    }
    FallBack "UI/Default"
}
