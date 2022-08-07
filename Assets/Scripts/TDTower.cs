using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TDTower : MonoBehaviour
{
	public TDTowerSO towerSO;
	public PointingTowerScript _pointingTowerScript;
	public TriggerScript CreepTrigger;

	List<TDCreep> _creepList = new List<TDCreep>();
	TDCreep _targetedCreep;
	private IEnumerator _onShootCreeps;

	void OnEnable()
	{
		CreepTrigger?.SetRadius(towerSO.MaxDistance);
		CreepTrigger.OnTriggerEntered += OnTriggerEntered;
		CreepTrigger.OnTriggerExited += OnTriggerExited;
	}

	private void OnTriggerExited(Collider other)
	{
		if (other.GetComponent<TDCreep>() is TDCreep creep)
			_creepList.Remove(creep);
	}

	private void OnTriggerEntered(Collider other)
	{
		if (other.GetComponent<TDCreep>() is TDCreep creep)
		{
			_creepList.Add(creep);
			creep.OnDeath += () => _creepList.Remove(creep);
			_creepList.Sort((a, b) => a.travelledDistance.CompareTo(b.travelledDistance));
			if (_onShootCreeps == null)
				this.RestartCoroutine(OnShootCreeps, ref _onShootCreeps);
		}
	}

	private IEnumerator OnShootCreeps()
	{
		while (_creepList.Count > 0)
		{
			_targetedCreep = _creepList.LastOrDefault();
			if (_targetedCreep)
				_pointingTowerScript?.SetTarget(_targetedCreep.transform);
			else
				break;

			for (int i = 0; i < towerSO.BurstShotCount; i++)
			{
				_pointingTowerScript?.Shoot();
				float dist = Vector3.Distance(transform.position, _targetedCreep.transform.position);
				if (_targetedCreep && dist > towerSO.MinDistance && dist < towerSO.MaxDistance)
					_targetedCreep.TakeDamage(towerSO.Damage);
				yield return new WaitForSeconds(towerSO.PauseBetweenShots);
			}

			yield return new WaitForSeconds(towerSO.BurstRechargeTime);
		}
		_onShootCreeps = null;
		_pointingTowerScript?.ResetTarget();
	}
}
