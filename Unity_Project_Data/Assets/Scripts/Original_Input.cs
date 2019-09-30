/*
 *　制作：2019/09/30
 *　作者：諸岡勇樹
 *　目的：インプットの名前を見た目上わかりやすくする
 *　　　　スペルミスのエラー回避
 *　2019/09/30：ドッキングゲームのオリジナル入力名で設定できるプロパティ群
 *　2019/09/30：XInput適応
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ドッキングゲームオリジナル入力返し
namespace DockingGame_Input
{
	public class Original_Input : MonoBehaviour
	{

		// GamePad1の入力群----------------------------------------------
		#region ABXY
		public bool ButtomA { get { return Input.GetButton("GamePad_1_0"); } }
		public bool ButtomB { get { return Input.GetButton("GamePad_1_1"); } }
		public bool ButtomX { get { return Input.GetButton("GamePad_1_2"); } }
		public bool ButtomY { get { return Input.GetButton("GamePad_1_3"); } }
		#endregion
		#region トリガー
		public bool ButtomLeft1		{ get { return Input.GetButton("GamePad_1_4"); } }
		public bool ButtomRight1	{ get { return Input.GetButton("GamePad_1_5"); } }
		public bool ButtomLeft2		{ get { return Input.GetAxis("GamePad_1_Axis_3") < 0.0f; } }
		public bool ButtomRight2	{ get { return Input.GetAxis("GamePad_1_Axis_3") > 0.0f; } }
		#endregion
		#region LRボタン
		public bool ButtomL3 { get { return Input.GetButton("GamePad_1_8"); } }
		public bool ButtomR3 { get { return Input.GetButton("GamePad_1_9"); } }
		#endregion
		#region その他
		public bool Buttom6	{ get { return Input.GetButton("GamePad_1_6"); } }
		public bool Buttom7	{ get { return Input.GetButton("GamePad_1_7"); } }
		#endregion
		#region 軸系
		public float StickLeft_X	{ get { return Input.GetAxis("GamePad_1_Axis_1"); } }
		public float StickLeft_Y	{ get { return Input.GetAxis("GamePad_1_Axis_2"); } }
		public float StickRight_X	{ get { return Input.GetAxis("GamePad_1_Axis_4"); } }
		public float StickRight_Y	{ get { return Input.GetAxis("GamePad_1_Axis_5"); } }
		public float Cross_X		{ get { return Input.GetAxis("GamePad_1_Axis_6"); } }
		public float Cross_Y		{ get { return Input.GetAxis("GamePad_1_Axis_7"); } }
		#endregion
		// GamePad1の入力群----------------------------------------------
	}
}
