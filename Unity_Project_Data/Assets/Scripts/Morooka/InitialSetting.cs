using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSetting : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
    }
}
