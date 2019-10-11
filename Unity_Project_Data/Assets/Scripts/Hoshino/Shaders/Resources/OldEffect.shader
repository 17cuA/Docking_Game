Shader "Custom/OldEffect"
{
	HLSLINCLUDE
#include "Assets/Postprocessing/Shaders/StdLib.hlsl"
	// 元の画像
	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	float4 _MainTex_TexelSize;

	// コントラスト
	float _Contrast;

		// フラグメントシェーダー
		half4 Frag(VaryingsDefault v) : SV_Target
	{
			// 元の画像
			half4 distance = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, v.texcoord);

			// 色収差
			distance.r = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, v.texcoord).r;
			distance.g = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, v.texcoord - half2(0.001, 0)).g;
			distance.b = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, v.texcoord - half2(0.002, 0)).b;

			// 残像
			half4 blendTex0 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, v.texcoord - half2(0.007, 0.007));
			half4 blendTex1 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, v.texcoord - half2(0.01, 0.01));
			distance += blendTex0 * 0.08;
			distance += blendTex1 * 0.05;
			distance /= 1 + 0.05 + 0.08;

			// グレーライン
			float gray = clamp(0.95 + 0.05 * cos(3.14 * v.texcoord.x * 120.0), 0.0, 1.0);

			// スキャンライン
			float scan = clamp(0.95 + 0.05 * cos(3.14 * (v.texcoord.y + _Time.x) *240.0), 0.0, 1.0);

			// ノイズ
			half3 noise = half3(floor(v.texcoord.xy * 120.0), _Time.x);
			noise = frac( sin(dot(noise, half3(17.0, 59.4, 15.0))));
			noise /= max(max(noise.x,noise.y),noise.z);


			distance.rgb *= gray * scan *noise * _Contrast;

			return distance;
	}
		ENDHLSL

		Subshader
	{
			ZTest Always Cull Off ZWrite Off
			Pass
			{
				HLSLPROGRAM
				#pragma vertex VertDefault
				#pragma fragment Frag
				ENDHLSL
			}
	}
}