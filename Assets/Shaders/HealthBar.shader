// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/HealthBar" {
	Properties {
		_MainColor ("Main Color", Color) = (0.5, 0, 0, 1)
		_GradientTex ("Gradient Texture", 2D) = "white" {}
	}
	SubShader {
		Tags {
			"Queue" = "Overlay"
			"RenderType" = "Transparent"
		}
		Pass {
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 _MainColor;
			sampler2D _GradientTex;
			
			struct vertInput {
				float4 pos : POSITION;
				half2 texCoord : TEXCOORD0;
			};
			
			struct vertOutput {
				float4 pos : SV_POSITION;
				half2 texCoord : TEXCOORD0;
			};

			vertOutput vert(vertInput input) {
				vertOutput o;
				o.pos = UnityObjectToClipPos(input.pos);
				o.texCoord = input.texCoord;
				return o;
			}

			half4 frag(vertOutput output) : COLOR{
				float4 texColor = tex2D(_GradientTex, output.texCoord);
				if (texColor.a != 0) return _MainColor;
				else return float4(0, 0, 0, 0);
			}
		ENDCG
		}
	}
}
