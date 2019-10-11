using System;
namespace UnityEngine.Rendering.PostProcessing
{
	[Serializable]
	[PostProcess(typeof(GlitchRenderer), PostProcessEvent.AfterStack, "Custom/Glitch", false)]
	public sealed class Glitch : PostProcessEffectSettings
	{
		[Header("波状変位")]
		[Tooltip("周波数")]
		public FloatParameter WaveFrequecy = new FloatParameter { value = 0.0f };
		[Tooltip("変化量")]
		public Vector2Parameter DisplAmount  = new Vector2Parameter{ value = new Vector2(0.0f,0.0f) };

		[Header("ランダムストライプ")]
		[Tooltip("ストライプの量")]
		public FloatParameter StripesAmount = new FloatParameter { value = 0.0f};
		[Range(0, 1)]
		[Tooltip("発生頻度")]
		public FloatParameter StripesFill = new FloatParameter { value = 0.0f };

		[Header("色収差")]
		[Tooltip("左右の収差")]
		public FloatParameter ChromAberrAmountX = new FloatParameter { value = 0.0f };
		[Tooltip("上下の収差")]
		public FloatParameter ChromAberrAmountY = new FloatParameter { value = 0.0f };


		// シェーダーが使用できるかのチェック
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return enabled.value
#if UNITY_EDITOR
				// Don't render motion blur preview when the editor is not playing as it can in some
				// cases results in ugly artifacts (i.e. when resizing the game view).
				&& Application.isPlaying
#endif
				&& !RuntimeUtilities.isVREnabled;
		}
	}
	public sealed class GlitchRenderer : PostProcessEffectRenderer<Glitch>
	{
		public override void Render(PostProcessRenderContext context)
		{
			// コマンドバッファ取得
			var cmd = context.command;

			var sheet = context.propertySheets.Get(Shader.Find("Custom/GlitchShader"));
			sheet.ClearKeywords();

			// 波状変位============================================================================================================================================
			Vector2 displAmount = new Vector2(Random.Range(-settings.DisplAmount.value.x, settings.DisplAmount.value.x), Random.Range(-settings.DisplAmount.value.y, settings.DisplAmount.value.y));
			sheet.properties.SetVector("_DisplacementAmount", 0.01f * displAmount);
			sheet.properties.SetFloat("_WavyDisplFreq", Random.Range(-settings.WaveFrequecy, settings.WaveFrequecy));
			// ===================================================================================================================================================
			// ランダムストライプ===================================================================================================================================
			float stripeAmount = Mathf.Sin( Time.frameCount)* settings.StripesAmount;
			sheet.properties.SetFloat("_StripesAmount",stripeAmount);
			sheet.properties.SetFloat("_StripesFill", settings.StripesFill);
			// ====================================================================================================================================================
			// 色収差==============================================================================================================================================
			sheet.properties.SetFloat("_ChromAberrAmountX", 0.01f * Random.Range(-settings.ChromAberrAmountX, settings.ChromAberrAmountX));
			sheet.properties.SetFloat("_ChromAberrAmountY", 0.01f * Random.Range(-settings.ChromAberrAmountY, settings.ChromAberrAmountY));
			// ====================================================================================================================================================

			// 画面にレンダリング
			cmd.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}
