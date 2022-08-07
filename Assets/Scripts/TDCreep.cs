using System;
using System.Collections.Generic;
using UnityEngine;

public class TDCreep : MonoBehaviour
{
	public TDCreepSO creepSO;
	public SimpleCreepBehaviour creepBehaviour;
	public Vector3 posOffset;
	public float travelledDistance;

	public float Health = -1;
	public float Speed;
	public float Damage;

	void OnEnable()
	{
		creepBehaviour = GetComponent<SimpleCreepBehaviour>();
		if (Health == -1)
		{
			Damage = creepSO.Damage;
			Health = creepSO.HP;
			Speed = creepSO.Speed;
		}
	}

	internal void Death()
	{
		TDCreepController.Instance.CreepDied(this);
		Destroy(gameObject);
	}

	internal void DamageCastle()
	{
		CastleScript.ApplyDamage(Damage);
		Death();
	}
}

public class SimpleCreepBehaviour : MonoBehaviour
{

}
