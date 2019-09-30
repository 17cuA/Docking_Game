/*
 *　制作：2019/09/30
 *　作者：諸岡勇樹
 *　目的：インプットの名前を見た目上わかりやすくする
 *　　　　スペルミスのエラー回避
 *　2019/09/30：ドッキングゲームのオリジナル入力名で設定できるプロパティ群
 *　2019/09/30：XInput適応
 */
using UnityEngine;

// ドッキングゲームオリジナル入力返し
namespace DockingGame_Input
{
	public class Original_Input
	{
		// GamePad1の入力群----------------------------------------------
		#region ABXY
		static public bool ButtomA	{ get { return Input.GetButton("GamePad_1_0"); } }
		static public bool ButtomB	{ get { return Input.GetButton("GamePad_1_1"); } }
		static public bool ButtomX	{ get { return Input.GetButton("GamePad_1_2"); } }
		static public bool ButtomY	{ get { return Input.GetButton("GamePad_1_3"); } }
		#endregion
		#region トリガー
		static public bool ButtomLeft1	{ get { return Input.GetButton("GamePad_1_4"); } }
		static public bool ButtomRight1	{ get { return Input.GetButton("GamePad_1_5"); } }
		static public bool ButtomLeft2	{ get { return Input.GetAxis("GamePad_1_Axis_3") < 0.0f; } }
		static public bool ButtomRight2	{ get { return Input.GetAxis("GamePad_1_Axis_3") > 0.0f; } }
		#endregion
		#region LRボタン
		static public bool ButtomL3	{ get { return Input.GetButton("GamePad_1_8"); } }
		static public bool ButtomR3	{ get { return Input.GetButton("GamePad_1_9"); } }
		#endregion
		#region その他
		static public bool Buttom6	{ get { return Input.GetButton("GamePad_1_6"); } }
		static public bool Buttom7	{ get { return Input.GetButton("GamePad_1_7"); } }
		#endregion
		#region 軸系
		static public float StickLeft_X		{ get { return Input.GetAxis("GamePad_1_Axis_1"); } }
		static public float StickLeft_Y		{ get { return Input.GetAxis("GamePad_1_Axis_2"); } }
		static public float StickRight_X	{ get { return Input.GetAxis("GamePad_1_Axis_4"); } }
		static public float StickRight_Y	{ get { return Input.GetAxis("GamePad_1_Axis_5"); } }
		static public float Cross_X			{ get { return Input.GetAxis("GamePad_1_Axis_6"); } }
		static public float Cross_Y			{ get { return Input.GetAxis("GamePad_1_Axis_7"); } }
		#endregion
		// GamePad1の入力群----------------------------------------------
	}
}
