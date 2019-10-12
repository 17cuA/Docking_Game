using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Manager : MonoBehaviour
{

	Vector3[] Target_Pos;
	[SerializeField] GameObject[] Target;

	[SerializeField] GameObject Charger;            //充電器（unity側にて設定）

	GameMaster gameMAster_Script;
	public float distance;
	public int Target_cnt;

	bool onceCheck = true;
    // Start is called before the first frame update
    void Start()
    {
		gameMAster_Script = gameObject.GetComponent<GameMaster>();

		Target_Pos = new Vector3[Target.Length];
		for(int i = 0; i < Target.Length; i++)
		{
			Target_Pos[i] = Target[i].transform.position;
			if(i != 0)
			{
				Target[i].SetActive(false);
			}
		}
		Target_cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
		//if (Charger.transform.position.z > Target[Target_cnt].transform.position.z + distance && onceCheck)
		//{
		//	onceCheck = false;
		//	gameMAster_Script.SetStageState(GameMaster.StageState.STAGEFAILURE);
		//}

		if (Target_cnt < Target.Length && Vector3.Distance(Target_Pos[Target_cnt], Charger.transform.position) < distance )
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
