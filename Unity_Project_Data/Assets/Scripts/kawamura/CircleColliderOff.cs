using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleColliderOff : MonoBehaviour
{
   public  Collider[] myCollider;
    void Start()
    {
       // myCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Charger")
        {
            for (int i = 0; i < myCollider.Length; i++)
            {
                myCollider[i].enabled = false;
            }
            //myCollider.enabled = false;
        }
    }
}
