//──────────────────────────────────────────────
// ファイル名	：EasingEditor_Simple.cs
// 概要			：イージングエディター機能
// 作成者		：杉山 雅哉
// 作成日		：2019.05.13
// 
//──────────────────────────────────────────────
// 更新履歴：
// 2019/06/19 [杉山 雅哉] クラス作成。EasingEditorをパクッてシンプルにする
//──────────────────────────────────────────────
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingEditor_Simple : MonoBehaviour
{
	//プロパティ───────────────────────────────────────
	[SerializeField] string[] memo = new string[0];
	[SerializeField] AnimationCurve[] anims = new AnimationCurve[1];
	Keyframe[] prev = new Keyframe[0];

	const float Limit = 0.001f;
	const float defaltTime = 1;

	int prevAnkersLength;
	//読み取り用変数─────────────────────────────────────
	public AnimationCurve[] Anims { get { return anims; } }

	//外部呼出しメソッド───────────────────────────────────
	public Keyframe GetStartkey(int num)
	{
		return anims[num].keys[0];
	}
	public Keyframe GetEndkey(int num)
	{
		return anims[num].keys[anims[num].keys.Length - 1];
	}
	public Keyframe Getkey(int animNum,int keyNum)
	{
		return anims[animNum].keys[keyNum];
	}
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
			if (sub[i].value < 0)
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
		//!< AnimationCurveの中身があるか確認する
		for (int i = 0; i < anims.Length; ++i)
		{
			//!< 中身が存在しないものには新しいAnimationCurveを入れる
			if (anims[i] == null)
			{
				anims[i] = new AnimationCurve();
				anims[i] = AnimationCurve.Linear(0, 0, defaltTime, 1);
			}
			CorrectLimit(ref anims[i]);
		}
	}
#endif
}
