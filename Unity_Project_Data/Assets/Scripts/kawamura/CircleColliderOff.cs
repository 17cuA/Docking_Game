using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleColliderOff : MonoBehaviour
{
    Collider myCollider;
    void Start()
    {
        myCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Charger")
        {
            myCollider.enabled = false;
        }
    }
}
