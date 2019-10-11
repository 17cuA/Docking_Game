Shader "Custom/GlitchShader"
{
	HLSLINCLUDE
	#include  "Assets/Postprocessing/Shaders/StdLib.hlsl"
	// 現在の画面
	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	
	// 波状変位
	half2 _DisplacementAmount;
	float _WavyDisplFreq;
	// ランダムストライプ
	float _StripesAmount;
	float _StripesFill;
	// 色収差
	float _ChromAberrAmountX;
	float _ChromAberrAmountY;

	// ランダム数値生成
	float rand(float2 co)
	{
		return frac(sin(dot(co, float2(12.9898, 78.233))) * 43758.5453);
	}

	half4 Frag(VaryingsDefault v) :SV_Target
	{
		// ランダムストライプ
		float stripes = floor(v.texcoord.y * _StripesAmount);
		stripes = 1 - step(_StripesFill, rand(float2(stripes, stripes)));

		// 波状変位
		float4 wavyDispl = lerp(half4(1, 0, 0, 1), half4(0, 1, 0, 1), (sin(v.texcoord.y * _WavyDisplFreq)+1)/2);
		float2 displUV = stripes * 0.01;
		displUV += float2((_DisplacementAmount.xy * wavyDispl.r) - (_DisplacementAmount.xy * wavyDispl.g));

		// 色収差
		half2 chromAberrAmount = half2(_ChromAberrAmountX, _ChromAberrAmountY);
		half colR = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex,float2(v.texcoord + displUV + chromAberrAmount)).r;
		half colG = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, v.texcoord + displUV).g;
		half colB = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(v.texcoord + displUV - chromAberrAmount)).b;
		return half4(colR, colG, colB, 1);
	}
		ENDHLSL

		SubShader
	{
		ZWrite Off
			Blend One Zero
			ZTest Always
			Cull Off
			Pass
		{
			HLSLPROGRAM
			#pragma vertex VertDefault
			#pragma fragment Frag
			ENDHLSL
		}
	}
}
