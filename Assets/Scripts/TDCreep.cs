using System;
using System.Collections.Generic;
using UnityEngine;

public class TDCreep : MonoBehaviour
{
	public TDCreepSO creepSO;
	public CreepHealthBar creepHealthBar;
	public Vector3 posOffset;
	public float travelledDistance;

	public float Health = -1;
	public float Speed;
	public float Damage;
	internal Func<bool> OnDeath;

	void OnEnable()
	{
		if (Health == -1)
		{
			Damage = creepSO.Damage;
			Health = creepSO.HP;
			Speed = creepSO.Speed;
		}
	}

	internal void TakeDamage(float damage)
	{
		Health -= damage;
		if (creepHealthBar)
			creepHealthBar.UpdateHP(Health / creepSO.HP);
		if (Health <= 0)
		{
			Death();
			CastleScript.AddGold((int)UnityEngine.Random.Range(creepSO.MinMaxGold.x, creepSO.MinMaxGold.y));
		}
	}

	internal void Death()
	{
		TDCreepController.Instance.CreepDied(this);
		Destroy(gameObject);
		OnDeath?.Invoke();
	}

	internal void DamageCastle()
	{
		CastleScript.ApplyDamage(Damage);
		Death();
	}
}
