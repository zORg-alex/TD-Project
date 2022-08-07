using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TDCreepController : MonoBehaviour
{
	public List<TDCreep> spawnedCreeps = new List<TDCreep>();
	public PathScript path;
	public float timeSinceLasSpawn;
	private Vector3 _spawnPoint;

	[Serializable]
	public class FloatFloatUEvent : UnityEvent<float, float> { }
	public FloatFloatUEvent OnWaveNumberChanged = new FloatFloatUEvent();
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
		int wi = 0;
		foreach (var wave in TDCreepWaves.Instance.Waves)
		{
			wi++;
			OnWaveNumberChanged?.Invoke(wi, TDCreepWaves.Instance.Waves.Count);
			//yield return new WaitUntil(() => spawnedCreeps.Count == 0);
			//yield return new WaitForSeconds(TDCreepWaves.Instance.timeBetween);
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
			yield return new WaitForSeconds(wave.Duration);
		}
	}
	IEnumerator UpdateCreep()
	{
		while (true)
		{
			foreach (var creep in spawnedCreeps)
			{
				if (!creep) continue;
				//float deltaDist = creep.creepSO.Speed * Time.deltaTime;
				var nextPos = path.GetPosition(creep.travelledDistance + 1f, Vector3.zero);
				creep.transform.position = Vector3.MoveTowards(creep.transform.position, nextPos, creep.Speed * Time.deltaTime);
				creep.travelledDistance = path.GetDistance(creep.transform.position);
				if (creep.travelledDistance > path.FullLength)
				{
					creep.DamageCastle();
				}
			}
			yield return null;
		}
	}
}
