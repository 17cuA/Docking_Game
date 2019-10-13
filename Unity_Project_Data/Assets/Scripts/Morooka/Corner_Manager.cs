using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner_Manager : MonoBehaviour
{
    private Target_Manager target;
    private int nowNum;

    void Start()
    {
        target = GameObject.Find("GameMaster").GetComponent<Target_Manager>();
    }

    void Update()
    {
        if (target.Get_InRadius() != nowNum)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        if(target != null)
        nowNum = target.Get_InRadius();
    }
}
