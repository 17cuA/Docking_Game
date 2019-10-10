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

	//ワープホール管理 : WarpHoleBehavior
	public WarpHoleBehavior warpHoleBehaviorCS;

	// Start is called before the first frame update
	void Start()
    {
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

			coordinateAfterMove.x = transform.position.x + Random.Range(-maxMovingDistance, maxMovingDistance);
			if (maxCoordinateAfterMove < coordinateAfterMove.x) coordinateAfterMove.x = maxCoordinateAfterMove;
			if (-maxCoordinateAfterMove > coordinateAfterMove.x) coordinateAfterMove.x = -maxCoordinateAfterMove;

			coordinateAfterMove.y = transform.position.y + Random.Range(-maxMovingDistance, maxMovingDistance);
			if (maxCoordinateAfterMove < coordinateAfterMove.y) coordinateAfterMove.y = maxCoordinateAfterMove;
			if (-maxCoordinateAfterMove > coordinateAfterMove.y) coordinateAfterMove.y = -maxCoordinateAfterMove;

			coordinateAfterMove.z = transform.position.z + Random.Range(-maxMovingDistance, maxMovingDistance);
			if (maxCoordinateAfterMove < coordinateAfterMove.z) coordinateAfterMove.z = maxCoordinateAfterMove;
			if (-maxCoordinateAfterMove > coordinateAfterMove.z) coordinateAfterMove.z = -maxCoordinateAfterMove;

			transform.position = coordinateAfterMove;

			movingCycleCount = 0;
		}

		if(warpHoleBehaviorCS.reproducingNum ==2)
		{
			elapsedTime += Time.deltaTime;

			maxCoordinateAfterMove = maxCoordinateAfterMoveBU * (1.0f - elapsedTime / convergenceTime);
		}
	}
}
