using System;
using System.Collections.Generic;
using UnityEngine;

public class TDCreep : MonoBehaviour
{
	public TDCreepSO creepSO;
	public SimpleCreepBehaviour creepBehaviour;

	void OnEnable()
	{
		creepBehaviour = GetComponent<SimpleCreepBehaviour>();
	}
}

public class SimpleCreepBehaviour : MonoBehaviour
{

}
