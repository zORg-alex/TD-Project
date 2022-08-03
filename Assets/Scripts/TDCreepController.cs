using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TDCreepController : MonoBehaviour
{
	public List<TDCreep> spawnedCreeps = new List<TDCreep>();
	public PathScript path;
	public float timeSinceLasSpawn;
	private Vector3 _spawnPoint;
	public static TDCreepController Instance { get; private set; }

	void OnEnable()
	{
		Instance = this;
		if (!path) path = FindObjectOfType<PathScript>();
		if (!path) {
			Debug.LogError("PathScript not found");
			return;
		}
		_spawnPoint = path.GetSpawn();
		if (Application.isPlaying)
		{
			StartCoroutine(SpawnCycle());
			StartCoroutine(UpdateCreep());
		}
	}

	void OnDisable()
	{
		StopAllCoroutines();
	}

	internal void CreepDied(TDCreep creep)
	{
		spawnedCreeps.Remove(creep);
	}

	IEnumerator SpawnCycle()
	{
		foreach (var wave in TDCreepWaves.Instance.Waves)
		{
			yield return new WaitForSeconds(TDCreepWaves.Instance.timeBetween);
			foreach (var group in wave.Groups)
			{
				var positions = HexPositionsUtility.FillHex(group.count, group.creep.Diameter / 2).Select(v => new Vector3(v.x, 0, v.y)).ToArray();
				for (int i = 0; i < group.count; i++)
				{
					var inst = Instantiate(group.creep.prefab);
					inst.transform.position = positions[i] + _spawnPoint;
					inst.posOffset = positions[i];
					spawnedCreeps.Add(inst);
				}

				yield return new WaitForSeconds(wave.timeBetween);
			}
		}
	}
	IEnumerator UpdateCreep()
	{
		while (true)
		{
			foreach (var creep in spawnedCreeps)
			{
				if (!creep) continue;
				float deltaDist = creep.creepSO.Speed * Time.deltaTime;
				creep.transform.position = path.GetNextPosition(creep.travelledDistance, creep.posOffset, deltaDist);
				creep.travelledDistance += deltaDist;
			}
			yield return null;
		}
	}
}
