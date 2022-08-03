using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TowerBase : MonoBehaviour
{
	private Collider _collider;

	void OnEnable()
	{
		_collider = GetComponent<Collider>();
		gameObject.layer = 8;
	}

	public bool Raycast(Ray ray) => _collider.Raycast(ray, out _, float.PositiveInfinity);
}
