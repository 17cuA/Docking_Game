//──────────────────────────────────────────────
// ファイル名	：EasingEditor.cs
// 概要			：カメラの動きの速さを調整する
// 作成者		：杉山 雅哉
// 作成日		：2019.05.13
// 
//──────────────────────────────────────────────
// 更新履歴：
// 2019/06/19 [杉山 雅哉] クラス作成。AnimationCurveの0番目の値を０に固定する。
// 2019/06/19 [杉山 雅哉] 前のキーが後ろのキーを抜かさないように設定する
// 2019/06/20 [杉山 雅哉] ラインクリエイターのアンカーの数にリンクしてアニメーションカーブの数を変える
// 2019/06/20 [杉山 雅哉] 名前変更
// 2019/06/27 [杉山 雅哉] アニメーションカーブの数を１つに制限する
// 2019/06/27 [杉山 雅哉] ラインクリエイターの数だけAnimationCurveをつくる
//──────────────────────────────────────────────
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
//SceneViewを取得するために宣言、エディタ外では使えないのでUNITY_EDITORで囲む
using UnityEditor;
#endif

[ExecuteInEditMode]
public class EasingEditor : MonoBehaviour
{
	//プロパティ───────────────────────────────────────
	[Header("アクロバティックカメラ情報")]
	[SerializeField] AcrobaticCamera camera;
	[SerializeField] AnimationCurve[] anims;
	[Header("デフォルトの設定時間")]
	[SerializeField] float defaltTime = 1;

	Keyframe[] prev = new Keyframe[0];

	const float Limit = 0.001f;

	int prevAnkersLength;
	//読み取り用変数─────────────────────────────────────
	public AnimationCurve[] Anims { get { return anims; } }
	//内部処理メソッド────────────────────────────────────
	void CorrectLimit(ref AnimationCurve anim)
	{
		//!< 代入用キーフレーム配列
		Keyframe[] sub = anim.keys;
		if (sub[0].time < 0)
		{
			sub[0].time = Limit;
			sub[1] = sub[0];
		}
		//!< 最初のキーは0固定
		sub[0].time = 0;
		sub[0].value = 0;

		for (int i = 1; i < sub.Length; ++i)
		{
			if (sub[i].value > 1)
			{
				sub[i].value = 1 - Limit;
			}
			if(sub[i].value < 0)
			{
				sub[i].value = 0 + Limit;
			}
		}

		if (sub[sub.Length - 1].value != 1)
		{
			sub[sub.Length - 2] = sub[sub.Length - 1];
			sub[sub.Length - 2].time -= Limit;
			sub[sub.Length - 1].time = sub[sub.Length - 2].time;
		}
		//!< 最後のキーの縦の値は1固定
		sub[sub.Length - 1].value = 1;
		//!< 値を代入
		anim.keys = sub;
		//!< キー情報を保存
		prev = anim.keys;
	}
	//実行外処理───────────────────────────────────────
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		//!< カメラ情報が存在しないのであれば実行しない
		if (!camera) return;
		//!< ラインクリエイターの数が変わったか確認する
		if(anims.Length != camera.LineCount)
		{
			//!< 配列の大きさを変更する
			Array.Resize(ref anims, camera.LineCount);
		}
		//!< AnimationCurveの中身があるか確認する
		for (int i = 0; i < anims.Length; ++i) 
		{
			//!< 中身が存在しないものには新しいAnimationCurveを入れる
			if(anims[i] == null)
			{
				anims[i] = new AnimationCurve();
				anims[i] = AnimationCurve.Linear(0, 0, defaltTime, 1);
			}
			CorrectLimit(ref anims[i]);
		}
	}
#endif
}