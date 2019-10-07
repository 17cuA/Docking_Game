//作成者：川村良太
//作成日：2019/10/03
//カメラの向きとか位置を変える処理
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    //カメラの位置状態
    public enum CameraState
    {
		Top,					//見下ろし
		Left,					//横から
        Backward,		//後方
        FPS,					//チャージャー視点
    }
    //状態の変数
    public CameraState cameraState;
	public CameraState saveCameraState;

    [Header("チャージャーを手動で入れよう！")]
    public GameObject chargerObj;					//チャージャーオブジェクト
    [Header("FPS視点を手動で入れよう！")]
    public GameObject FPS_CameraPosObj;		//チャージャー視点の位置オブジェクト
	[Header("真上視点を手動で入れよう！")]
	public GameObject TopCameraPosObj;		//真上視点オブジェクト
	[Header("横から視点を手動で入れよう！")]
	public GameObject LeftCameraPosObj;		//横から視点オブジェクト
	[Header("スマホを手動で入れよう！")]
    public GameObject phoneObj;                 //スマホオブジェクト

    public Vector3 backwardCameraPos;           //後方視点の位置
    public Vector3 savePos;                             //チャージャーの前の位置を保存（チャージャーが動いているかを見るため）
    public Quaternion _rotation;                        //カメラの向く方向

	public int cameraPosNum;		// 1が後方 2が真上 3が横から

    //public float rotaSpeed;

    [Header("入力用　チャージャーとのZの距離")]
    public float defPosZ_Value;     //チャージャーからどれだけ後ろにいるかの値
    public float posZ;                  //後方位置のZ座標の値

    //Chargerの位置によって変わるカメラの回転値 XとY
    public float rotaX;
    public float rotaY;

    [Header("入力用　FPS視点になるXとYの範囲の値")]
    //public float FPS_Distance_XandY_Value;
    public float FPS_Distance_XandY;
    [Header("入力用　Z距離がこれより近くなるとFPSになる")]
    public float FPS_Distance_Z;

    public bool once = true;
    public bool isMove = false; //動いているかのチェック
	public bool isBackRotaSet = false;
	public bool isReset = false;

    void Start()
    {
		cameraPosNum = 1;
        //回転限界を初期の値に設定
        rotaX = 15;
        rotaY = -20;
        _rotation = Quaternion.Euler(rotaX, rotaY, 0);

        //位置セーブ
        savePos = chargerObj.transform.position;
    }

    void Update()
    {
        if (once)
        {
            once = false;
        }
        //今のチャージャーの位置が保存した位置と違ったら
        if (chargerObj.transform.position != savePos)
        {
            //動いているかtrue
            isMove = true;
            //チャージャーの位置保存更新
            savePos = chargerObj.transform.position;
        }
        //チャージャーの位置が保存位置と同じなら
        else if (chargerObj.transform.position == savePos)
        {
            //動いているかfalse
            isMove = false;
        }

        //動いていたら
        if (isMove)
        {
            //カメラの回転値を決める関数呼び出し
            CameraRotation();
            //カメラの向く方向を決める
            _rotation = Quaternion.Euler(rotaX, rotaY, 0);
        }

        //後方視点のZ位置を更新する
        posZ = chargerObj.transform.position.z - defPosZ_Value;
        //後方視点の位置を更新
        backwardCameraPos = new Vector3(1.4f, 1f, posZ);

        //FPS視点に移動するときの条件（チャージャーのXY座標が決めた値の範囲内で、Zの座標が決めた値よりスマホと近くなったら）
        if (chargerObj.transform.position.x > phoneObj.transform.position.x - FPS_Distance_XandY && chargerObj.transform.position.x < phoneObj.transform.position.x + FPS_Distance_XandY
            && chargerObj.transform.position.y > phoneObj.transform.position.y - FPS_Distance_XandY && chargerObj.transform.position.y < phoneObj.transform.position.y + FPS_Distance_XandY
            && chargerObj.transform.position.z > -FPS_Distance_Z && chargerObj.transform.position.z < -3.94)
        {
			if(!isReset)
			{
				//FPS視点になる前の位置を保存
				saveCameraState = cameraState;
				//FPS視点範囲外に行ったときに位置を戻すフラグON
				isReset = true;
			}
			cameraState = CameraState.FPS;
        }
        else
        {
			//FPSからカメラを戻すフラグがONなら
			if(isReset)
			{
				//カメラの位置を保存していた位置へ戻す
				cameraState = saveCameraState;
				isReset = false;
			}
		}

		//カメラの位置変更関数呼び出し
		CameraPosChange();

		//FPS視点以外のときに位置切り替えボタンが押されたら位置（カメラの状態）を変える
		if (cameraState != CameraState.FPS)
		{
			//Xボタンが押されたら　後方→横から→真上→後方…で切り替わる
			if (Input.GetButtonDown("GamePad_1_2"))
			{
				switch(cameraState)
				{
					//後方視点
					case CameraState.Backward:
						cameraState = CameraState.Left;
						break;

					case CameraState.Top:
						cameraState = CameraState.Backward;
						break;

					case CameraState.Left:
						cameraState = CameraState.Top;
						break;
				}
			}
			//Yボタンが押されたら　後方→真上→横から→後方…で切り替わる
			else if (Input.GetButtonDown("GamePad_1_3"))
			{
				switch (cameraState)
				{
					//後方視点
					case CameraState.Backward:
						cameraState = CameraState.Top;
						break;

					case CameraState.Top:
						cameraState = CameraState.Left;
						break;

					case CameraState.Left:
						cameraState = CameraState.Backward;
						break;
				}
			}
		}
	}

	//------------------ここから関数------------------

	//カメラの回転を決める関数
	void CameraRotation()
    {
        //RotationのYを決める
        if (chargerObj.transform.position.x >= 0)
        {
            rotaY = chargerObj.transform.position.x * 10 - 20;
        }
        else if (chargerObj.transform.position.x < 0)
        {
            rotaY = 6.6f * chargerObj.transform.position.x - 20;
        }
        //RotationのYを決めるやつの終わり

        //RotationのXを決める
        if (chargerObj.transform.position.y <= 0)
        {
            rotaX = 7.2f * Mathf.Abs(chargerObj.transform.position.y) + 15f;
        }
        else if (chargerObj.transform.position.y > 0)
        {
            rotaX = -11.6f * chargerObj.transform.position.y + 15f;
        }
        //RotationのXを決めるやつおわり
    }

	//カメラの位置チェンジ
	void CameraPosChange()
	{
		//カメラの状態を見て切り替える
		switch (cameraState)
		{
			//後方視点
			case CameraState.Backward:
				if (isBackRotaSet)
				{
					transform.rotation = _rotation;
				}
				//位置を後方の位置に
				transform.position = backwardCameraPos;
				//回転させる
				transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, 1);
				break;

			case CameraState.Top:
				//位置を真上の位置に
				transform.position = TopCameraPosObj.transform.position;
				//回転させる
				transform.rotation = Quaternion.Euler(90, 0, 0);
				isBackRotaSet = true;
				break;

			case CameraState.Left:
				//位置を後方の位置に
				transform.position = LeftCameraPosObj.transform.position;
				//回転させる
				transform.rotation = Quaternion.Euler(0, 90, 0);

				isBackRotaSet = true;
				break;

			//FPS視点
			case CameraState.FPS:
				//位置をFPS位置に
				transform.position = FPS_CameraPosObj.transform.position;
				//まっすぐ向ける
				transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
		}
	}

	//カメラの位置変更の数字設定
	void CameraPosSet()
	{
		switch(cameraPosNum)
		{
			case 0:
				break;

			case 1:
				cameraState = CameraState.Backward;
				break;

			case 2:
				cameraState = CameraState.Top;
				break;

			case 3:
				cameraState = CameraState.Left;
				break;
		}
	}
}
