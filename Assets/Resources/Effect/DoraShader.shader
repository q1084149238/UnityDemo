Shader "GameEffect/Dora"
{
    Properties
    {
        _MainTex ("AnimationTexture", 2D) = "white" {}
       _Width("Width", Range(1, 10)) = 5
       _Light("Light", Range(0.2, 1)) = 0.5
       _Speed("Speed", Range(-2, 2)) = -1
       _Angle("Angle", Range(0.2, 1)) = 0.4
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque"
            "IgnoreProjector"="True" 
            "RenderType"="TransparentCutout"
        }
        LOD 200

        pass
        {
           CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Pixel
            #include "UnityCG.cginc"
            
            half4 _Light;
            half _Speed;
            half _Width;
            half _Angle;
            sampler2D _MainTex;

            struct vertexOutput
            {
                half4 pos: SV_POSITION;
                half3 uv: TEXCOORD0;

            };

            vertexOutput Vertex(appdata_base v)
            {
                vertexOutput o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 Pixel(vertexOutput i): SV_TARGET
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
				float y = i.uv.y + i.uv.x * _Angle;
                /*通过时间和uv坐标构建正弦函数，实现
                正弦波形的循环效果*/
				float v = sin(y - _Time.w * _Speed);
                //平滑阶梯函数，剔除小于0的值
				v = smoothstep(1 - _Width / 250, 1, v);
				float3 target = float3(v, v, v) + col.rgb;

				fixed4 targetCol = fixed4(target, col.a);
                //剔除透明部分
                clip(targetCol.a - 0.8);
				return targetCol;
            }
            
            ENDCG
        }
    }
    FallBack "Diffuse"
}
