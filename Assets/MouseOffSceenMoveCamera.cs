using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseOffSceenMoveCamera : MonoBehaviour
{
	public InputAction pointerPosition;
	private IEnumerator _moveCameraCR;
	private CinemachineTrackedDolly _cam;
	public float inertia;
	public float Sensetivity = 1f;
	public float Dampening = .5f;
	public float zzz;

	void OnEnable()
	{
		_cam = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
		pointerPosition.Enable();

		this.RestartCoroutine(MoveCamera, ref _moveCameraCR);
	}
	void OnDisable()
	{
		StopAllCoroutines();
	}

	private IEnumerator MoveCamera()
	{
		while(this)
		{
			if (!_cam) break;
			var pos = _cam.m_PathPosition;
			zzz = pointerPosition.ReadValue<Vector2>().x / Screen.width;
			float hmpos = pointerPosition.ReadValue<Vector2>().x / Screen.width;
			if (hmpos > 0 && hmpos < .1f)
				inertia -= (.1f - hmpos) * Sensetivity * Time.deltaTime;
			if (hmpos < 1 && hmpos > .9f) 
				inertia -= (.9f - hmpos) * Sensetivity * Time.deltaTime;

			_cam.m_PathPosition = Mathf.Clamp01(_cam.m_PathPosition + inertia);
			inertia *= Dampening;

			yield return null;
		}
	}
}
