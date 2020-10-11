Shader "Unlit/ShapeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _SecondColor ("Second color", Color) = (1.0, 1.0, 1.0, 1.0)
        _MoveDir ("Movement direction", Vector) = (0.2, 0.72, 0, 0)
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
            float4 _MainColor;
            float4 _SecondColor;
            float4 _MoveDir;

            float rect(float2 pos)
            {
                return step(0.15, pos.x) * step(pos.x, 0.85) * step(0.15, pos.y) * step(pos.y, 0.85);
            }

            float circle(float2 pos)
            {
                float r = length(pos - float2(0.5, 0.5));
                return step(r, 0.35);
            }

            #define COS120 cos(3.1415926535f / 1.5f)
            #define SIN120 sin(3.1415926535f / 1.5f)
            float triange(float2 pos)
            {
                pos -= float2(0.5, 0.5);
                float2 pos_120 = mul(float2x2(COS120, -SIN120, SIN120, COS120), pos);
                float2 pos_240 = mul(float2x2(COS120, SIN120, -SIN120, COS120), pos);

                float r = 0.15;
                return step(pos.x, r) * step(pos_120.x, r) * step(pos_240.x, r);
            }

            float draw_bg(float2 pos)
            {
                int2 ij = floor(pos);
                float2 grid = pos - ij;
                if ((ij.x + ij.y) % 2 == 0)
                    return circle(grid);
                else if (ij.x % 2 == 0)
                    return triange(grid);
                else
                    return rect(grid);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 pos = i.vertex.xy / 200.0f - _MoveDir.xy * _Time.y;
                pos.y *= -1;
                float angle = 2 * 3.1415926535 + 1.0f;
                pos = mul(float2x2(cos(angle), -sin(angle), sin(angle), cos(angle)), pos);
                float draw = lerp(0.95f, 1.0f, draw_bg(pos));
                angle += 1.0f;
                float2 colpos = mul(float2x2(cos(angle), -sin(angle), sin(angle), cos(angle)), pos);
                float col = colpos.x / 4.0f;
                col -= floor(col);
                col = col > 0.5f ? 0.85f : 0.15f;
                return draw * tex2D(_MainTex, i.uv) * lerp(_MainColor, _SecondColor, col);
            }
            ENDCG
        }
    }
}
