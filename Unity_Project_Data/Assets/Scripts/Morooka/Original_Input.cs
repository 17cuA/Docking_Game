/*
 *　制作：2019/09/30
 *　作者：諸岡勇樹
 *　目的：インプットの名前を見た目上わかりやすくする
 *　　　　スペルミスのエラー回避
 *　2019/09/30：ドッキングゲームのオリジナル入力名で設定できるプロパティ群
 *　2019/09/30：XInput適応
 *　2019/10/02：ゲームに使うボタン取得させ
 */
using UnityEngine;

// ドッキングゲームオリジナル入力返し
namespace DockingGame_Input
{
	[System.Serializable]
	public class X_Input
	{
		static private bool Is_ButtomLeft2_PreviousInput { get; set; } = false;        // L2トリガーに直前のフレームで入力があったかどうか
		static private bool Is_ButtomRight2_PreviousInput { get; set; } = false;        // R2トリガーに直前のフレームで入力があったかどうか

		// GamePad1の入力群----------------------------------------------
		#region　長押し Buttom Hold
		#region ABXY
		static public bool ButtomA_Hold { get { return Input.GetButton("GamePad_1_0"); } }
		static public bool ButtomB_Hold { get { return Input.GetButton("GamePad_1_1"); } }
		static public bool ButtomX_Hold { get { return Input.GetButton("GamePad_1_2"); } }
		static public bool ButtomY_Hold { get { return Input.GetButton("GamePad_1_3"); } }
		#endregion
		#region トリガー
		static public bool ButtomLeft1_Hold { get { return Input.GetButton("GamePad_1_4"); } }
		static public bool ButtomRight1_Hold { get { return Input.GetButton("GamePad_1_5"); } }
		static public bool ButtomLeft2_Hold { get { return Input.GetAxis("GamePad_1_Axis_3") < 0.0f; } }
		static public bool ButtomRight2_Hold { get { return Input.GetAxis("GamePad_1_Axis_3") > 0.0f; } }
		#endregion
		#region LRボタン
		static public bool ButtomL3_Hold { get { return Input.GetButton("GamePad_1_8"); } }
		static public bool ButtomR3_Hold { get { return Input.GetButton("GamePad_1_9"); } }
		#endregion
		#region その他
		static public bool Buttom6_Hold { get { return Input.GetButton("GamePad_1_6"); } }
		static public bool Buttom7_Hold { get { return Input.GetButton("GamePad_1_7"); } }
		#endregion
		#endregion

		#region　離した瞬間 Buttom Up
		#region ABXY
		static public bool ButtomA_Up { get { return Input.GetButtonUp("GamePad_1_0"); } }
		static public bool ButtomB_Up { get { return Input.GetButtonUp("GamePad_1_1"); } }
		static public bool ButtomX_Up { get { return Input.GetButtonUp("GamePad_1_2"); } }
		static public bool ButtomY_Up { get { return Input.GetButtonUp("GamePad_1_3"); } }
		#endregion
		#region トリガー
		static public bool ButtomLeft1_Up { get { return Input.GetButtonUp("GamePad_1_4"); } }
		static public bool ButtomRight1_Up { get { return Input.GetButtonUp("GamePad_1_5"); } }
		// 別枠-------------------------------------------------------------------
		static public bool ButtomLeft2_Up
		{
			get {
				if (!(Input.GetAxis("GamePad_1_Axis_3") < 0.0f) && Is_ButtomLeft2_PreviousInput)
				{
					Is_ButtomLeft2_PreviousInput = false;
					return true;
				}
				else { return false; }
			}
		}
		static public bool ButtomRight2_Up
		{
			get
			{
				if (!(Input.GetAxis("GamePad_1_Axis_3") > 0.0f) && Is_ButtomRight2_PreviousInput)
				{
					Is_ButtomRight2_PreviousInput = false;
					return true;
				}
				else { return false; }
			}
		}
		// 別枠-------------------------------------------------------------------
		#endregion
		#region LRボタン
		static public bool ButtomL3_Up { get { return Input.GetButtonUp("GamePad_1_8"); } }
		static public bool ButtomR3_Up { get { return Input.GetButtonUp("GamePad_1_9"); } }
		#endregion
		#region その他
		static public bool Buttom6_Up { get { return Input.GetButtonUp("GamePad_1_6"); } }
		static public bool Buttom7_Up { get { return Input.GetButtonUp("GamePad_1_7"); } }
		#endregion
		#endregion

		#region　押した瞬間 Buttom Down
		#region ABXY
		static public bool ButtomA_Down { get { return Input.GetButtonDown("GamePad_1_0"); } }
		static public bool ButtomB_Down { get { return Input.GetButtonDown("GamePad_1_1"); } }
		static public bool ButtomX_Down { get { return Input.GetButtonDown("GamePad_1_2"); } }
		static public bool ButtomY_Down { get { return Input.GetButtonDown("GamePad_1_3"); } }
		#endregion
		#region トリガー
		static public bool ButtomLeft1_Down { get { return Input.GetButtonDown("GamePad_1_4"); } }
		static public bool ButtomRight1_Down { get { return Input.GetButtonDown("GamePad_1_5"); } }
		// 別枠-------------------------------------------------------------------
		static public bool ButtomLeft2_Down
		{
			get {
				if (Input.GetAxis("GamePad_1_Axis_3") < 0.0f && !Is_ButtomLeft2_PreviousInput)
				{
					Is_ButtomLeft2_PreviousInput = true;
					return true;
				}
				else { return false; }
			}
		}
		static public bool ButtomRight2_Down
		{
			get
			{
				if (Input.GetAxis("GamePad_1_Axis_3") > 0.0f && !Is_ButtomRight2_PreviousInput)
				{
					Is_ButtomRight2_PreviousInput = true;
					return true;
				}
				else { return false; }
			}
		}
		// 別枠-------------------------------------------------------------------
		#endregion
		#region LRボタン
		static public bool ButtomL3_Down { get { return Input.GetButtonDown("GamePad_1_8"); } }
		static public bool ButtomR3_Down { get { return Input.GetButtonDown("GamePad_1_9"); } }
		#endregion
		#region その他
		static public bool Buttom6_Down { get { return Input.GetButtonDown("GamePad_1_6"); } }
		static public bool Buttom7_Down { get { return Input.GetButtonDown("GamePad_1_7"); } }
		#endregion
		#endregion

		#region 軸系 Float返し
		static public float StickLeft_X { get { return Input.GetAxis("GamePad_1_Axis_1"); } }
		static public float StickLeft_Y { get { return Input.GetAxis("GamePad_1_Axis_2"); } }
		static public float StickRight_X { get { return Input.GetAxis("GamePad_1_Axis_4"); } }
		static public float StickRight_Y { get { return Input.GetAxis("GamePad_1_Axis_5"); } }
		static public float Cross_X { get { return Input.GetAxis("GamePad_1_Axis_6"); } }
		static public float Cross_Y { get { return Input.GetAxis("GamePad_1_Axis_7"); } }
		#endregion
		// GamePad1の入力群----------------------------------------------
	}

	[System.Serializable]
	public class Original_Input : MonoBehaviour
	{
		[SerializeField] InputManager manager;
		bool isSetUpButton = false;
		public static Original_Input instance;
		static public bool ButtomTurbo_Down { get { return Input.GetButtonDown(instance.manager.Button["Turbo"]); } }
		static public bool ButtomB_Down { get { return Input.GetButtonDown(instance.manager.Button["LockOn"]); } }

		private void Start()
		{
			instance = FindObjectOfType<Original_Input>();
			manager.Init();
			DontDestroyOnLoad(this);
		}
		private void Update()
		{
			if (isSetUpButton)
			{
				isSetUpButton = !manager.SettingButton();
			}
			else
			{
				isSetUpButton = X_Input.Buttom6_Down;
			}
		}
	}
}
