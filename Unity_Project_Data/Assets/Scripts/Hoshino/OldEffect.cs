using System;

namespace UnityEngine.Rendering.PostProcessing
{
	[Serializable]
	[PostProcess(typeof(OldEffectRenderer), PostProcessEvent.AfterStack, "Custom/OldEffect", false)]
	public sealed class OldEffect : PostProcessEffectSettings
	{
		[Range(0f, 1f), Tooltip("コントラスト")]
		public FloatParameter contrast = new FloatParameter { value = 0f };

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

	public sealed class OldEffectRenderer : PostProcessEffectRenderer<OldEffect>
	{

		public override void Render(PostProcessRenderContext context)
		{
			// コマンドバッファ取得
			var cmd = context.command;
			// シェーダシート取得
			var sheet = context.propertySheets.Get(Shader.Find("Custom/OldEffect"));

			// GPUにパラメータ送信
			sheet.properties.SetFloat("_Contrast", settings.contrast + 1.0f);
			// 画面に描画
			cmd.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}
