using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code_Manager : MonoBehaviour
{
    private Target_Manager target;
    private GameObject Charger;
    private int nowNum;

    private void Start()
    {
        target = GameObject.Find("GameMaster").GetComponent<Target_Manager>();
        Charger = GameObject.Find("Charger");
        nowNum = target.Get_InRadius();
    }

    void Update()
    {
        if(target.Get_InRadius() != nowNum)
        {
            Vector3 temp = transform.position;
            temp.z += Charger.transform.position.z;
        }
    }
}
