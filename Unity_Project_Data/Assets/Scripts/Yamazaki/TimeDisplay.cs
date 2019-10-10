// 20191004
// 時間表示
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    // ゲームプレイ時間
    [SerializeField, NonEditable]
    private float stagePlayDelay;       // 現在の残り時間
    public float stagePlayDelayMax;     // 最大の残り時間
    public Text stageTimeText;          // 時間テキスト
	[Header("減少していく値")]
	public float subtract;		//移動を繰り返したときに減っていく値
    public enum TimeMode
    {
        NONE,
        PLAY,
        END,
        STOP,
    }

    [SerializeField, NonEditable]
    private TimeMode timeMode;

	public UI_Gauge ui_gauge;

    public void SetTimeMode(TimeMode t)
    {
        timeMode = t;
    }

    public TimeMode GetTimeMode()
    {
        return timeMode;
    }

    public void SetTime(float t)
    {
        if (t > 0.0f) stagePlayDelay = stagePlayDelayMax = t;
        else stagePlayDelay = stagePlayDelayMax;

        //10/04　時間表示を1:30から90表示に変えました(そのほかの場所も)＠川村
        //stageTimeText.text = ((int)stagePlayDelay / 60).ToString("0") + "'" + (stagePlayDelay % 60.0f).ToString("00.000");
        stageTimeText.text = stagePlayDelay.ToString("00.000");
    }

    // Start is called before the first frame update
    void Start()
    {
        //stageTimeText.text = ((int)stagePlayDelay / 60).ToString("0") + "'" + (stagePlayDelay % 60.0f).ToString("00.000");
        stageTimeText.text = stagePlayDelay.ToString("00.000");
    }

    // Update is called once per frame
    void Update()
    {
        switch (timeMode)
        {
            case TimeMode.PLAY:
                //stageTimeText.text = ((int)stagePlayDelay / 60).ToString("0") + "'" + (stagePlayDelay % 60.0f).ToString("00.000");
                stageTimeText.text = stagePlayDelay.ToString("00.000");
				//追加------------------------------------
				ui_gauge.Calc_nowVolue((int)Time.deltaTime) ;
                //stagePlayDelay -= Time.deltaTime;
                //if (stagePlayDelay <= 0.0f)
				if(ui_gauge.Get_nowValue() <= 0.0f)
                {
					//stagePlayDelay = 0.0f;
					//o2manager.nowO2 = 0;
                    SetTimeMode(TimeMode.END);
                }
                break;

            case TimeMode.END:
                //stageTimeText.text = ((int)stagePlayDelay / 60).ToString("0") + "'" + (stagePlayDelay % 60.0f).ToString("00.000");
　              stageTimeText.text = stagePlayDelay.ToString("00.000");
                break;

            case TimeMode.STOP:
                //stageTimeText.text = ((int)stagePlayDelay / 60).ToString("0") + "'" + (stagePlayDelay % 60.0f).ToString("00.000");
                stageTimeText.text = stagePlayDelay.ToString("00.000");
                break;

            default:
                break;
        }
    }

	public void Subtract_Value()
	{
		stagePlayDelay -= subtract;
	}
}
