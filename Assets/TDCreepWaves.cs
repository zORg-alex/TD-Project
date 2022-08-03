using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TD CreepWaves")]
public class TDCreepWaves :ScriptableObject
{
	public List<TDCreepWave> Groups;
}