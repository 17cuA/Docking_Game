//────────────────────────────────────────────
// ファイル名	：LineCreater.cs
// 概要			：惑星を回転させる
// 作成者		：杉山 雅哉
// 作成日		：2019/10/03
// 
//────────────────────────────────────────────
// 更新履歴：
// 2019/10/03 [杉山 雅哉] クラス作成。回転させる。
//────────────────────────────────────────────
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotater : MonoBehaviour
{
	[SerializeField] float rotateSpeed;
	[SerializeField] Vector3 axis;
	void Update()
	{
		transform.Rotate(axis, rotateSpeed * Time.deltaTime);
	}
}
