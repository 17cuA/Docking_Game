/*
 *　制作：2019/09/30
 *　作者：諸岡勇樹
 *　2019/10/02：Axisの設定
 *　2019/10/08：スティックコントローラーセッティング
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 入力する軸の名前を保持しておく
/// </summary>
[System.Serializable]
public class AxisManager
{
	[SerializeField, Tooltip("Unity側で設定する軸のリスト")] List<string> defaultAxisNameList = new List<string>();   // uinty側で設定する軸の名前のリスト
	[SerializeField, Tooltip("実際に使用する名前のリスト")] List<string> useAxisNameList = new List<string>();             // スクリプト側で使用する軸名のリスト
	[SerializeField, Tooltip("確定させるまでの時間")] float decisionTime = 5f;                                                // 決定するまでの時間
	[SerializeField, Tooltip("設定時に表示するフォント")] Font textFont;                                                    // 設定時に表示するテキストのフォント
	[SerializeField, Tooltip("表示するフォントのX座標")] float textPositionX = 0;                                          // 表示するテキストのx座標

	float inputTime;                                                                                                    // 入力を受けている時間
	Dictionary<string, string> reflectAxisNameMap = new Dictionary<string, string>();                                 // スクリプト側に渡す軸の名前
	Dictionary<string, float> reflect_PositiveAndOppositeDirection_Map = new Dictionary<string, float>();               // スクリプト側に渡す軸の正負情報
	Dictionary<string, string> settingAxisNameMap = new Dictionary<string, string>();                                 // 軸の再設定をするときの一時変数
	Dictionary<string, float> setting_PositiveAndOppositeDirection_Map = new Dictionary<string, float>();               // 正負情報の再設定するときの一時変数
	int settingAxisNum = 0;                                                                                           // 設定している軸の要素番号
	string previousInputAxisName = "";                                                                                // 前フレームに入力を受けていた軸の名前
	public Dictionary<string, string> Axis { get { return reflectAxisNameMap; } }
	public Dictionary<string, float> PositiveAndOppositeDirection { get { return reflect_PositiveAndOppositeDirection_Map; } }
	Text inputInfoText;
	/// <summary>
	/// コンストラクタ
	/// </summary>
	public AxisManager()
	{
		if (defaultAxisNameList.Count < useAxisNameList.Count) { Debug.LogWarning("UseButtonListCount is more than DefaultButtonNameList.Count!"); }
		for (int i = 0; i < useAxisNameList.Count && i < defaultAxisNameList.Count; ++i)
		{
			reflectAxisNameMap.Add(useAxisNameList[i], defaultAxisNameList[i]);
			settingAxisNameMap.Add(useAxisNameList[i], "");
			reflect_PositiveAndOppositeDirection_Map.Add(useAxisNameList[i], 1.0f);
			setting_PositiveAndOppositeDirection_Map.Add(useAxisNameList[i], 1.0f);
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	public void Init()
	{
		if (defaultAxisNameList.Count < useAxisNameList.Count) { Debug.LogWarning("UseButtonListCount is more than DefaultButtonNameList.Count!"); }
		for (int i = 0; i < useAxisNameList.Count && i < defaultAxisNameList.Count; ++i)
		{
			reflectAxisNameMap.Add(useAxisNameList[i], defaultAxisNameList[i]);
			settingAxisNameMap.Add(useAxisNameList[i], "");
			reflect_PositiveAndOppositeDirection_Map.Add(useAxisNameList[i], 1.0f);
			setting_PositiveAndOppositeDirection_Map.Add(useAxisNameList[i], 1.0f);
		}
	}
	/// <summary>
	/// 呼び出されている間、ボタンの再設定を行う
	/// </summary>
	/// <returns>再設定がすべて終わったかどうか</returns>
	public bool SettingAxis()
	{
		// テキスト生成
		if (!inputInfoText)
		{
			Canvas anyCanvas = GameObject.FindObjectOfType<Canvas>();
			inputInfoText = new GameObject("InputInfo").AddComponent<Text>();
			inputInfoText.rectTransform.SetParent(anyCanvas.transform);
			inputInfoText.rectTransform.localPosition = new Vector2(textPositionX, 0f);
			inputInfoText.font = textFont;
			inputInfoText.fontSize = 50;
			inputInfoText.rectTransform.sizeDelta = new Vector2(3840f, 1080f);
		}
		bool isComplete = false;
		bool isInput = false;
		string inputButtonName = "";
		// 設定されたボタンをそれぞれ確認していく
		for (int i = 0; i < defaultAxisNameList.Count; ++i)
		{
			// すでに入力を受け付けているものはスキップする
			if (settingAxisNameMap.ContainsValue(defaultAxisNameList[i]))
			{
				continue;
			}
			// 入力を受けていたら名前を一時保存する
			if (Input.GetAxis(defaultAxisNameList[i]) > 0.7f || Input.GetAxis(defaultAxisNameList[i]) < -0.7f && !isInput)
			{
				inputButtonName = defaultAxisNameList[i];
				isInput = true;
			}
			// 同時押しされていたら解除
			else if (Input.GetAxis(defaultAxisNameList[i]) > 0.7f || Input.GetAxis(defaultAxisNameList[i]) < -0.7f && isInput)
			{
				inputButtonName = "";
				break;
			}
			// ボタンが上げられたらスキップする
			else if (Input.GetAxis(defaultAxisNameList[i]) == 0.0f)
			{
				inputButtonName = "";
				continue;
			}
		}
		// 一つのボタンが押されていたら入力時間をカウントする
		if (previousInputAxisName == inputButtonName && inputButtonName != "")
		{
			inputTime += Time.deltaTime;
		}
		// そうでない時は時間リセット
		else
		{
			inputTime = 0f;
		}
		// 決定される時間になったら決定して反転設定に映る
		if (inputTime >= decisionTime)
		{
			settingAxisNameMap[useAxisNameList[settingAxisNum]] = inputButtonName;
			++settingAxisNum;
		}
		previousInputAxisName = inputButtonName;
		// 全て設定されたら反映と初期化をしてメソッドをtrueで返す
		if (settingAxisNum >= useAxisNameList.Count)
		{
			// ボタンの名前を設定
			foreach (string buttonName in useAxisNameList)
			{
				reflectAxisNameMap[buttonName] = settingAxisNameMap[buttonName];
			}
			// 設定用のマップを初期化
			for (int i = 0; i < useAxisNameList.Count; ++i)
			{
				settingAxisNameMap[useAxisNameList[i]] = "";
			}
			isComplete = true;
			previousInputAxisName = "";
			settingAxisNum = 0;
			GameObject.Destroy(inputInfoText);
		}
		// テキスト表示
		inputInfoText.text = "Setting Now";
		for (int i = 0; i < useAxisNameList.Count; ++i)
		{
			// ボタン名の情報
			inputInfoText.text += "\n" + useAxisNameList[i] + " : ";
			// 入力状態の情報
			if (settingAxisNum == i)
			{
				inputInfoText.text += "Setting";
			}
			else if (settingAxisNameMap[useAxisNameList[i]] == "")
			{
				inputInfoText.text += "Not set";
			}
			else if (settingAxisNameMap[useAxisNameList[i]] != "")
			{
				inputInfoText.text += "Complete";
			}
		}
		return isComplete;
	}

	/// <summary>
	///  簡易コンフィグ、コントローラー切り替え
	/// </summary>
	/// <returns> </returns>
	public bool ControllerChange()
	{
		if(reflectAxisNameMap["StickRight_Y"] == defaultAxisNameList[2])
		{
			reflectAxisNameMap["StickRight_Y"] = defaultAxisNameList[6];
		}
		else
		{
			reflectAxisNameMap["StickRight_Y"] = defaultAxisNameList[2];
		}

		reflect_PositiveAndOppositeDirection_Map["StickLeft_Y"] *= -1.0f;

		return true;
	}

	public bool LeftRightSet(string temp)
	{
		if (temp == "") return false;
		else if (temp.Substring(0, 9) == "GamePad_1")
		{
			reflectAxisNameMap["StickRight_X"] = defaultAxisNameList[6];
			reflectAxisNameMap["StickRight_Y"] = defaultAxisNameList[7];

			reflectAxisNameMap["StickLeft_X"] = defaultAxisNameList[0];
			reflectAxisNameMap["StickLeft_Y"] = defaultAxisNameList[1];
		}
		else if (temp.Substring(0, 9) == "GamePad_2")
		{
			reflectAxisNameMap["StickRight_X"] = defaultAxisNameList[0];
			reflectAxisNameMap["StickRight_Y"] = defaultAxisNameList[1];

			reflectAxisNameMap["StickLeft_X"] = defaultAxisNameList[6];
			reflectAxisNameMap["StickLeft_Y"] = defaultAxisNameList[7];
		}
		return true;
	}
}
