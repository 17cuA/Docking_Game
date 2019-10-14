using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code_Manager : MonoBehaviour
{
    private Target_Manager target;
    private GameObject Charger;
    private GameObject hand; 
    private int nowNum;

    private void Start()
    {
        target = GameObject.Find("GameMaster").GetComponent<Target_Manager>();
        Charger = GameObject.Find("Charger");
        hand = transform.GetChild(1).gameObject;
        nowNum = target.Get_InRadius();
    }

    void Update()
    {
        //if(target.Get_InRadius() != nowNum)
        //{
        //    targetPos.z += 10.0f;

        //    nowNum = target.Get_InRadius();
        //}

        //if(transform.position != targetPos)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.1f);
        //}

        Vector3 vector = hand.transform.position;
        vector.z += 0.1f / 60.0f;
        hand.transform.position = vector;
    }
}
