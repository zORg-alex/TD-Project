using System;
using System.Collections;
using UnityEngine;

public class PointingTowerScript : MonoBehaviour
{
	public float rotationSpeed = .5f;
	public float MaxDistance;
	public Transform target;
	public Transform gun;

	/// <summary>
	/// Store current Coroutine for cancellation and currentstate
	/// </summary>
	private IEnumerator _stateCR;


	public void SetTarget(Transform target)
	{
		this.target = target;
		if (target)
			this.RestartCoroutine(TrackTarget, ref _stateCR);
	}

	public static event Action<PointingTowerScript> OnTargetLost;

	private IEnumerator TrackTarget()
	{
		while (target && TargetInRange(target))
		{
			var rot = Quaternion.LookRotation(transform.InverseTransformPoint(target.position));
			gun.rotation = Quaternion.RotateTowards(gun.transform.rotation, rot, rotationSpeed);
			yield return null;
		}
		_stateCR = null;
		this.RestartCoroutine(Idle, ref _stateCR);
		OnTargetLost?.Invoke(this);
	}

	private bool TargetInRange(Transform target)
	{
		return Vector3.Distance(transform.position, target.position) <= MaxDistance;
	}

	private IEnumerator Idle()
	{
		yield return null;
		while (!gun.rotation.Equals(transform.rotation))
		{
			gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, transform.rotation, rotationSpeed);
			yield return null;
		}
		_stateCR = null;
	}

}
