using System;
using System.Collections.Generic;
using UnityEngine;

public class TDCreep : MonoBehaviour
{
	public TDCreepSO creepSO;
	public SimpleCreepBehaviour creepBehaviour;
	public Vector3 posOffset;
	public float travelledDistance;

	void OnEnable()
	{
		creepBehaviour = GetComponent<SimpleCreepBehaviour>();
	}

	private void Death()
	{
		TDCreepController.Instance.CreepDied(this);
		Destroy(gameObject);
	}
}

public class SimpleCreepBehaviour : MonoBehaviour
{

}
