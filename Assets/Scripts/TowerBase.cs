using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TowerBase : MonoBehaviour
{
	public bool isTowerBuilt;
	public Transform towerRoot;

	internal void Build(TDTowerSO tower)
	{
		isTowerBuilt = true;
		var t = Instantiate(tower.prefab).ResetTransform(towerRoot);
	}
}
