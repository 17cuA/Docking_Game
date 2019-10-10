using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorRandomThunder : MonoBehaviour
{
	//最大移動量
	public float maxMovingDistance;
	//移動後座標
	private Vector3 coordinateAfterMove;
	//移動後最大座標
	public float maxCoordinateAfterMove;
	//移動周期
	public int movingCycle;
	//移動周期カウント
	private int movingCycleCount;

	//収束トリガー
	public bool convergenceTrigger;
	//収束時間
	public float convergenceTime;
	//経過時間
	private float elapsedTime;
	//移動後最大座標バックアップ
	private float maxCoordinateAfterMoveBU;

	//基準座標
	public GameObject baseCoordinate;

	//ワープホール管理 : WarpHoleBehavior
	public WarpHoleBehavior warpHoleBehaviorCS;

	// Start is called before the first frame update
	void Start()
	{
		baseCoordinate = transform.root.gameObject;

		transform.position = new Vector3(
		Random.Range(-maxMovingDistance, maxMovingDistance),
		Random.Range(-maxMovingDistance, maxMovingDistance),
		Random.Range(-maxMovingDistance, maxMovingDistance)
		);

		movingCycleCount = 0;

		convergenceTrigger = false;
		elapsedTime = 0.0f;
		maxCoordinateAfterMoveBU = maxCoordinateAfterMove;
	}

	void Update()
	{
		movingCycleCount++;
		if (movingCycle <= movingCycleCount)
		{
			//baseCoordinate = transform.root.gameObject;

			coordinateAfterMove.x = transform.position.x + Random.Range(baseCoordinate.transform.position.x - maxMovingDistance, baseCoordinate.transform.position.x + maxMovingDistance);
			if (baseCoordinate.transform.position.x + maxCoordinateAfterMove < coordinateAfterMove.x) coordinateAfterMove.x = baseCoordinate.transform.position.x + maxCoordinateAfterMove;
			if (baseCoordinate.transform.position.x - maxCoordinateAfterMove > coordinateAfterMove.x) coordinateAfterMove.x = baseCoordinate.transform.position.x - maxCoordinateAfterMove;

			coordinateAfterMove.y = transform.position.y + Random.Range(baseCoordinate.transform.position.y - maxMovingDistance, baseCoordinate.transform.position.y + maxMovingDistance);
			if (baseCoordinate.transform.position.y + maxCoordinateAfterMove < coordinateAfterMove.y) coordinateAfterMove.y = baseCoordinate.transform.position.y + maxCoordinateAfterMove;
			if (baseCoordinate.transform.position.y - maxCoordinateAfterMove > coordinateAfterMove.y) coordinateAfterMove.y = baseCoordinate.transform.position.y - maxCoordinateAfterMove;

			coordinateAfterMove.z = transform.position.z + Random.Range(baseCoordinate.transform.position.z - maxMovingDistance, baseCoordinate.transform.position.z + maxMovingDistance);
			if (baseCoordinate.transform.position.z + maxCoordinateAfterMove < coordinateAfterMove.z) coordinateAfterMove.z = baseCoordinate.transform.position.z + maxCoordinateAfterMove;
			if (baseCoordinate.transform.position.z - maxCoordinateAfterMove > coordinateAfterMove.z) coordinateAfterMove.z = baseCoordinate.transform.position.z - maxCoordinateAfterMove;

			transform.position = coordinateAfterMove;

			movingCycleCount = 0;
		}

		if (warpHoleBehaviorCS.reproducingNum == 2)
		{
			elapsedTime += Time.deltaTime;

			maxCoordinateAfterMove = maxCoordinateAfterMoveBU * (1.0f - elapsedTime / convergenceTime);
		}
	}
}
