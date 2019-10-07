using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordTip : MonoBehaviour
{
	[SerializeField, Tooltip("afsd")] private Rigidbody rigidbody;

    private void Update()
    {
		rigidbody.velocity = new Vector3(0.0f, 0.0f, -0.01f);
    }
}
