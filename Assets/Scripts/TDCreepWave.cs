using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/TD CreepWave")]
public class TDCreepWave : ScriptableObject
{
	public List<CreepGroup> Groups;
	public float timeBetween = 5f;
	public float Duration = 30f;
}
