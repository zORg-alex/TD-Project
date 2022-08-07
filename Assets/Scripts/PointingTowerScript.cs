using System;
using System.Collections;
using UnityEngine;

public class PointingTowerScript : MonoBehaviour
{
	public float rotationSpeed = .5f;
	public Transform target;
	public Transform gun;
	public Vector3 gunPushBack = Vector3.back * .5f;
	public float gunPushBackRestoreTime = 1f;
	Vector3 _gunInitialPosition;

	void OnEnable()
	{
		_gunInitialPosition = gun.localPosition;
	}

	/// <summary>
	/// Store current Coroutine for cancellation and currentstate
	/// </summary>
	private IEnumerator _stateCR;
	private IEnumerator _shootCR;

	public void SetTarget(Transform target)
	{
		this.target = target;
		if (target)
			this.RestartCoroutine(TrackTarget, ref _stateCR);
	}

	public static event Action<PointingTowerScript> OnTargetLost;

	private IEnumerator TrackTarget()
	{
		while (target)
		{
			var rot = Quaternion.LookRotation(target.position - transform.position);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotationSpeed);
			yield return null;
		}
		_stateCR = null;
		this.RestartCoroutine(Idle, ref _stateCR);
		OnTargetLost?.Invoke(this);
	}

	private IEnumerator Idle()
	{
		yield return null;
		while (!transform.rotation.Equals(Quaternion.identity))
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, rotationSpeed);
			yield return null;
		}
		_stateCR = null;
	}

	internal void Shoot()
	{
		this.RestartCoroutine(ShootCR, ref _shootCR);
	}

	internal void ResetTarget()
	{
		target = null;
	}

	private IEnumerator ShootCR()
	{
		var p = _gunInitialPosition + gunPushBack;
		var t = 0f;
		while (t < gunPushBackRestoreTime)
		{
			gun.localPosition = Vector3.Lerp(p, _gunInitialPosition, t / gunPushBackRestoreTime);
			yield return null;
			t += Time.deltaTime;
		}
		_shootCR = null;
	}

#if UNITY_EDITOR

	void OnDrawGizmos()
	{
		if (target)
			UnityEditor.Handles.DrawAAPolyLine(transform.position, target.position);
	}
#endif
}
