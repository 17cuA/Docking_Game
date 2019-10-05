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
        Backward,       //後方
        FPS,                //チャージャー視点
    }
    //状態の変数
    public CameraState cameraState;

    [Header("手動で入れよう！その1")]
    public GameObject chargerObj;                   //チャージャーオブジェクト
    [Header("手動で入れよう！その2")]
    public GameObject FPS_CameraPosObj;     //チャージャー視点の位置オブジェクト
    [Header("手動で入れよう！その3")]
    public GameObject phoneObj;                 //スマホオブジェクト
    public Vector3 backwardCameraPos;           //後方視点の位置オブジェクト
    public Vector3 savePos;                             //チャージャーの前の位置を保存（チャージャーが動いているかを見るため）
    public Quaternion _rotation;                        //カメラの向く方向

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
    public bool isMove = false;	//動いているかのチェック

    public bool xxxxxxxxxxxxx = false;
    public bool yyyyyyyyyyyyy = false;
    public bool zzzzzzzzzzzzz = false;

    void Start()
    {
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
        //transform.position = new Vector3(transform.position.x, transform.position.y, posZ);

        if (chargerObj.transform.position.x > phoneObj.transform.position.x - FPS_Distance_XandY && chargerObj.transform.position.x < phoneObj.transform.position.x + FPS_Distance_XandY)
        {
            xxxxxxxxxxxxx = true;
        }
        else
        {
            xxxxxxxxxxxxx = false;
        }

        if (chargerObj.transform.position.y > phoneObj.transform.position.y - FPS_Distance_XandY && chargerObj.transform.position.y < phoneObj.transform.position.y + FPS_Distance_XandY)
        {
            yyyyyyyyyyyyy = true;
        }
        else
        {
            yyyyyyyyyyyyy = false;
        }

        if (chargerObj.transform.position.z > -FPS_Distance_Z && chargerObj.transform.position.z < 0)
        {
            zzzzzzzzzzzzz = true;
        }
        else
        {
            zzzzzzzzzzzzz = false;
        }

        //FPS視点に移動するときの条件（チャージャーのXY座標が決めた値の範囲内で、Zの座標が決めた値よりスマホと近くなったら）
        if (chargerObj.transform.position.x > phoneObj.transform.position.x - FPS_Distance_XandY && chargerObj.transform.position.x < phoneObj.transform.position.x + FPS_Distance_XandY
            && chargerObj.transform.position.y > phoneObj.transform.position.y - FPS_Distance_XandY && chargerObj.transform.position.y < phoneObj.transform.position.y + FPS_Distance_XandY
            && chargerObj.transform.position.z > -FPS_Distance_Z && chargerObj.transform.position.z < 0)
        {
            cameraState = CameraState.FPS;
        }
        else
        {
            cameraState = CameraState.Backward;
        }

        //カメラの状態を見て切り替える
        switch (cameraState)
        {
            //後方視点
            case CameraState.Backward:
                //位置を後方の位置に
                transform.position = backwardCameraPos;
                //回転させる
                transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, 1);
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
}
