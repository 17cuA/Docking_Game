/*
 *　制作：2019/10/08
 *　作者：諸岡勇樹
 *　2019/10/08：スマホの全体管理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartphoneManagement : MonoBehaviour
{
	[SerializeField, Tooltip("スマホについてるコライダー持ちオブジェクト")] private GameObject[] colliderObject;

	private List<Collider> OnTriggerColliders = new List<Collider>();
	private List<Collider> OffTriggerColliders = new List<Collider>();

    void Start()
    {
        foreach(var col in colliderObject)
		{
			Collider temp = col.GetComponent<Collider>();
			if(temp.isTrigger)
			{
				OnTriggerColliders.Add(temp);
			}
			else
			{
				OffTriggerColliders.Add(temp);
			}
		}
    }

	public bool OnTriggerCollidersEnabledSet
	{
		set {
			foreach (var col in OnTriggerColliders)
			{
				col.enabled = value;
			}
		}
		get {
			bool reFlag = true;
			foreach (var col in OnTriggerColliders)
			{
				reFlag = col.enabled;
				if(!reFlag)
				{
					break;
				}
			}
			return reFlag;
		}
	}
	public bool OffTriggerCollidersEnabledSet
	{
		set {
			foreach (var col in OffTriggerColliders)
			{
				col.enabled = value;
			}
		}
		get {
			bool reFlag = true;
			foreach (var col in OffTriggerColliders)
			{
				reFlag = col.enabled;
				if(!reFlag)
				{
					break;
				}
			}
			return reFlag;
		}
	}
}
