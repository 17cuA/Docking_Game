using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Manager : MonoBehaviour
{
	public UI_Gauge ui_gauge;
	public int nowO2;

	[Header("移動した際に減らす値（整数）")]
	[SerializeField]private int Substract;
    // Start is called before the first frame update
    void Start()
    {
		nowO2 = ui_gauge.Get_nowValue();
    }

    // Update is called once per frame
    void Update()
    {
		ui_gauge.Call_UpdateGuage(nowO2);
    }
	/// <summary>
	/// 減速した際に減らす
	/// </summary>
	public void Substract_Os ()
	{
		nowO2 -= Substract;
	}
}
