using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Manager : MonoBehaviour
{

	Vector3[] Target_Pos;
	[SerializeField] GameObject[] Target;

	[SerializeField] GameObject Charger;            //充電器（unity側にて設定）
	float distance;
	int Target_cnt;
    // Start is called before the first frame update
    void Start()
    {
		for(int i = 0; i < Target.Length; i++)
		{
			Target_Pos[i] = Target[i].transform.position;
		}
		Target_cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if (Vector3.Distance(Target_Pos[Target_cnt], Charger.transform.position) > distance)
		{

			Next_Target();
		}
	}

	private void Next_Target()
	{
		Target[Target_cnt].SetActive(false);
		Target_cnt += 1;
		if(Target_cnt < Target.Length)
		{
			Target[Target_cnt].SetActive(true);
		}
	}
	public int Get_InRadius()
	{
		return Target_cnt;
	}
}
