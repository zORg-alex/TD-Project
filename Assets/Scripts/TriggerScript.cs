using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class TriggerScript : MonoBehaviour
{
	[HideInInspector]
	public Collider _collider;
	void OnEnable()
	{
		_collider = GetComponent<Collider>();
		_collider.isTrigger = true;
	}
	public Action<Collider> OnTriggerEntered;

	internal void SetRadius(float radius)
	{
		if (!_collider) _collider = GetComponent<Collider>();
		if (_collider is SphereCollider s) s.radius = radius;
		else if (_collider is CapsuleCollider c) c.radius = radius;
		else if (_collider is BoxCollider b) b.size = Vector3.one * radius;
	}

	public Action<Collider> OnTriggerExited;
	void OnTriggerEnter(Collider other) => OnTriggerEntered?.Invoke(other);
	void OnTriggerExit(Collider other) => OnTriggerExited?.Invoke(other);
}