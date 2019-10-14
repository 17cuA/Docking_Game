using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceMemory : MonoBehaviour
{
    public GameObject targetAObj;
    public GameObject targetBObj;

    [SerializeField, NonEditable, Header("ターゲットA")]
    private Transform targetAT;
    [SerializeField, NonEditable, Header("ターゲットB")]
    private Transform targetBT;

    public float fDistance;

    public float magnification = 1.0f;

    public RectTransform targetMemoryRectTransform;

    public CameraWork cameraWorkScr;

    public Image memoryT0;
    public Image memoryT1;
    public Image memoryT2;
    public Image memoryT3;
    public Image memoryU1;
    public Image memoryU2;
    public Image memoryU3;


    // Start is called before the first frame update
    void Start()
    {
        targetAT = targetAObj.transform;
        targetBT = targetBObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraWorkScr.isFPS)
        {
            memoryT0.enabled = true;
            memoryT1.enabled = true;
            memoryT2.enabled = true;
            memoryT3.enabled = true;
            memoryU1.enabled = true;
            memoryU2.enabled = true;
            memoryU3.enabled = true;

            fDistance = Vector3.Distance(new Vector3(0.0f, targetAT.position.y, 0.0f), new Vector3(0.0f, targetBT.position.y, 0.0f));

            if (targetAT.position.y > targetBT.position.y)
            {
                targetMemoryRectTransform.anchoredPosition3D = new Vector3(0.0f, fDistance, 0.0f) * magnification;
            }
            else
            {
                targetMemoryRectTransform.anchoredPosition3D = new Vector3(0.0f, -fDistance, 0.0f) * magnification;
            }
        }
        else
        {
            memoryT0.enabled = false;
            memoryT1.enabled = false;
            memoryT2.enabled = false;
            memoryT3.enabled = false;
            memoryU1.enabled = false;
            memoryU2.enabled = false;
            memoryU3.enabled = false;

        }
    }
}
