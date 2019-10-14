using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRollMover : MonoBehaviour
{
	[Header("移動スピード")]
	[SerializeField] float speed;
	[Header("移動方向")]
	[SerializeField]Vector2 moveDirection;
	RectTransform rectTransform;
	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	void Update()
	{
		if(gameObject.active = true)
		{
			rectTransform.position += new Vector3(speed * moveDirection.x, speed * moveDirection.y);
		}
	}
}
