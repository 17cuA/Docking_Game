/*
 *　制作：2019/09/30
 *　作者：諸岡勇樹
 *　目的：インプットの名前を見た目上わかりやすくする
 *　　　　スペルミスのエラー回避
 *　2019/09/30：ドッキングゲームのオリジナル入力名で設定できる関数群
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ドッキングゲームオリジナル入力返し
namespace DockingGame_Input
{
	public class Original_Input : MonoBehaviour
	{
		private string[] Bottom_Name = new string[24]
{
		"GamePad_1_0",
		"GamePad_1_1",
		"GamePad_1_2",
		"GamePad_1_3",
		"GamePad_1_4",
		"GamePad_1_5",
		"GamePad_1_6",
		"GamePad_1_7",
		"GamePad_1_8",
		"GamePad_1_9",
		"GamePad_1_10",
		"GamePad_1_11",
		"GamePad_2_0",
		"GamePad_2_1",
		"GamePad_2_2",
		"GamePad_2_3",
		"GamePad_2_4",
		"GamePad_2_5",
		"GamePad_2_6",
		"GamePad_2_7",
		"GamePad_2_8",
		"GamePad_2_9",
		"GamePad_2_10",
		"GamePad_2_11",
};

		// GamePad1の入力群----------------------------------------------
		public bool  Pad1_ButtomA
		{
			get { return Input.GetButton("GamePad_1_1"); }
		}
		public bool  Pad1_ButtomB
		{
			get { return Input.GetButton("GamePad_1_2"); }
		}
		public bool  Pad1_ButtomX
		{
			get { return Input.GetButton("GamePad_1_0"); }
		}
		public bool  Pad1_ButtomY
		{
			get { return Input.GetButton("GamePad_1_3"); }
		}
		public bool  Pad1_ButtomL1
		{
			get { return Input.GetButton("GamePad_1_4"); }
		}
		public bool  Pad1_ButtomR1
		{
			get { return Input.GetButton("GamePad_1_5"); }
		}
		public bool  Pad1_ButtomL2
		{
			get { return Input.GetButton("GamePad_1_6"); }
		}
		public bool  Pad1_ButtomR2
		{
			get { return Input.GetButton("GamePad_1_7"); }
		}
		// GamePad1の入力群----------------------------------------------
	}
}
