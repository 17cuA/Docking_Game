/*
 *　制作：2019/10/04
 *　作者：諸岡勇樹
 *　2019/10/04：充電ケーブルの移動
 *　2019/10/04：穴に入ったとき、ポジションスナップ
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DockingGame_Input;

public class Charger_Manager : MonoBehaviour
{
    /// <summary>
    /// 充電の移動向き
    /// </summary>
    public enum MOVE_DIRECTION
    {
        eFRONT,     // まえ
        eBACK,      // うしろ
        eRIGHT,     // みぎ
        eLEFT,      // ひだり
        eSTOP,      // とまれ
    }

    private Rigidbody MyRigidbody { get; set; }     // 自身のRigidbody
    private bool IsEnteredTheSlot { get; set; }     // スマホの差込口に入ったか
    private bool isGameOverCheck { get; set; }     // ゲームオーバーに変更できるか
    public MOVE_DIRECTION Direction { private set; get; } //現在の移動向き
    [SerializeField, Tooltip("スナップtarget")] private GameObject snapTargetPos;        // スナップターゲット位置
    [SerializeField, Tooltip("スナップtarget")] public GameObject[] circleObjcts;       // リングオブジェクト                                                  //
    [SerializeField, Tooltip("加速時の最大の値")] private float add_Max;
    [SerializeField, Tooltip("ゲームマスター")] private GameMaster GM;           //ゲームマスター（ゲームクリアかどうかの判定をしたりするよう）
    [SerializeField, Tooltip("移動速度")] private float speed;
    [SerializeField, Tooltip("ブレーキ速度")] private float brakeSpeed;
    [SerializeField, Tooltip("酸素ゲージのスクリプト")] private UI_Gauge O2UI;
    [SerializeField, Tooltip("酸素ゲージの減る時間(秒)")] private float timeToReduce;
    [SerializeField, Tooltip("コーナープレハブ")] private GameObject cornerPrefab;
    [SerializeField, Tooltip("cornerを置く場所")] private GameObject cornerpoint;
    private Target_Manager target;
    private int nowNum;

    public GameObject[] Corners;
    public int circleCnt;       //順番にリングを見るためのカウント
    private float elapsedTime;

    #region いじるなBy諸岡
    /// <summary>
    /// 最大速度を渡す
    /// </summary>
    public float MaxSpeed { get { return new Vector3(add_Max, add_Max, add_Max).magnitude; } }
    /// <summary>
    ///  現在の速度を渡す
    /// </summary>
    public float NowSpeed { get { return MyRigidbody.velocity.magnitude; } }
    /// <summary>
    /// Z軸の最大速度
    /// </summary>
    public float MaxSpeed_Z { get { return add_Max; } }
    /// <summary>
    /// Z軸の現在速度
    /// </summary>
    public float NowSpeed_Z { get { return Mathf.Abs(MyRigidbody.velocity.z); } }
    #endregion

    //public Time
    private void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        IsEnteredTheSlot = false;
        isGameOverCheck = true;
        circleCnt = 0;

        Corners = new GameObject[3]
        {
         Instantiate(cornerPrefab, cornerpoint.transform.position, Quaternion.identity),
         Instantiate(cornerPrefab, cornerpoint.transform.position, Quaternion.identity),
         Instantiate(cornerPrefab, cornerpoint.transform.position, Quaternion.identity),
        };

        foreach(var obj in Corners)
        {
            obj.SetActive(false);
        }
        target = GameObject.Find("GameMaster").GetComponent<Target_Manager>();
    }
    private void Update()
    {
        if (!IsEnteredTheSlot)
        {
            if (GameMaster.instance.stageState == GameMaster.StageState.PLAYING)
            {
                Movement();         //移動処理
            }
        }
        else
        {
            MyRigidbody.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, snapTargetPos.transform.position, 0.01f);
        }

        //スマホを越えてしまったらゲームオーバー
        if (transform.position.z > -3.53f && isGameOverCheck)
        {
            GM.SetStageState(GameMaster.StageState.STAGEFAILURE);
            isGameOverCheck = false;
        }
        //リングを越えてしまったらゲームオーバー
        if (circleCnt < 7)
        {
            if (transform.position.z > circleObjcts[circleCnt].transform.position.z && isGameOverCheck)
            {
                GM.SetStageState(GameMaster.StageState.STAGEFAILURE);
                isGameOverCheck = false;
            }
        }

        if (target.Get_InRadius() != nowNum)
        {
            Instantiate(cornerPrefab, cornerpoint.transform.position, Quaternion.identity);
            nowNum = target.Get_InRadius();
        }
    }
    #region ムーブ2
    private void Movement()
    {

        Vector3 saveInputNum = new Vector3((Original_Input.StickLeft_X / 100.0f) * speed, (Original_Input.StickLeft_Y / 100.0f) * speed, 0.0f);
        if (Original_Input.ButtomFront_Hold || Input.GetKey(KeyCode.Space))
        {
            saveInputNum.z -= (1 / 100.0f) * brakeSpeed;
            if (MyRigidbody.velocity.z < 0.1f)
            {
                saveInputNum.z = 0.0f;
            }

            elapsedTime += Time.deltaTime;
            if (elapsedTime > timeToReduce)
            {
                O2UI.Calc_nowVolue();
                elapsedTime = 0.0f;
            }
        }
        else
        {
            elapsedTime = timeToReduce;
            saveInputNum.z += (1 / 100.0f) * speed;
            Direction = MOVE_DIRECTION.eFRONT;
        }

        #region デバッグキー
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            saveInputNum.x -= (1 / 100.0f) * speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            saveInputNum.x += (1 / 100.0f) * speed;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            saveInputNum.y += (1 / 100.0f) * speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            saveInputNum.y -= (1 / 100.0f) * speed;
        }
        #endregion
        if (saveInputNum.x < 0)
        {
            Direction = MOVE_DIRECTION.eLEFT;
        }
        else if (saveInputNum.x > 0)
        {
            Direction = MOVE_DIRECTION.eRIGHT;
        }

        //	加速後の Velocity 値の仮保存
        Vector3 tempVelocity = MyRigidbody.velocity + saveInputNum;

        // スピード制限(絶対値より)--------------------
        if (Mathf.Abs(tempVelocity.x) > add_Max) tempVelocity.x = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.x);
        if (Mathf.Abs(tempVelocity.y) > add_Max) tempVelocity.y = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.y);
        if (Mathf.Abs(tempVelocity.z) > add_Max) tempVelocity.z = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.z);
        //----------------------------------------------

        // 速度適応
        MyRigidbody.velocity = tempVelocity;
    }
    #endregion

    private void OnTriggerEnter(Collider col)
    {
        // 穴に触れたとき
        if (col.tag == "Slot")
        {
            IsEnteredTheSlot = true;
        }

        //見るリングを次のリングにする
        else if (col.tag == "Circle")
        {
            foreach(var obj in Corners)
            {
                if(!obj.activeSelf)
                {
                    obj.transform.position = cornerpoint.transform.position;
                    obj.SetActive(true);
                    break;
                }
            }
            circleCnt++;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        //穴以外のスマホ部分に触れたらゲームオーバー
        if (col.gameObject.tag == "Wall" && isGameOverCheck)
        {
            GM.SetStageState(GameMaster.StageState.STAGEFAILURE);
            isGameOverCheck = false;
        }

    }
}
