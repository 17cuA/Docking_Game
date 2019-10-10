using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut_Manager : MonoBehaviour
{
	enum AnimationClipName
	{
		eMOVE_STOP,
		eMOVE_FRONT,
		eMOVE_BACK,
		eMOVESLIDE_LEFT,
		eMOVESLIDE_RIGHT,
	}

	[SerializeField] private Charger_Manager ChargerScript;
	[SerializeField] private Animation animation_Right;
	[SerializeField] private Animation animation_Left;
	[SerializeField] private string[] name;

    void Update()
    {
		switch (ChargerScript.Direction)
		{
			case Charger_Manager.MOVE_DIRECTION.eFRONT:
				animation_Right.CrossFade(name[(int)AnimationClipName.eMOVE_FRONT]);
				animation_Left.CrossFade(name[(int)AnimationClipName.eMOVE_FRONT]);
				break;
			case Charger_Manager.MOVE_DIRECTION.eBACK:
				animation_Right.CrossFade(name[(int)AnimationClipName.eMOVE_BACK]);
				animation_Left.CrossFade(name[(int)AnimationClipName.eMOVE_BACK]);
				break;
			case Charger_Manager.MOVE_DIRECTION.eRIGHT:
				animation_Right.CrossFade(name[(int)AnimationClipName.eMOVESLIDE_RIGHT]);
				animation_Left.CrossFade(name[(int)AnimationClipName.eMOVESLIDE_LEFT]);
				break;
			case Charger_Manager.MOVE_DIRECTION.eLEFT:
				animation_Right.CrossFade(name[(int)AnimationClipName.eMOVESLIDE_LEFT]);
				animation_Left.CrossFade(name[(int)AnimationClipName.eMOVESLIDE_RIGHT]);
				break;
			case Charger_Manager.MOVE_DIRECTION.eSTOP:
				animation_Right.CrossFade(name[(int)AnimationClipName.eMOVE_STOP]);
				animation_Left.CrossFade(name[(int)AnimationClipName.eMOVE_STOP]);
				break;
			default:
				break;
		}
	}
}
