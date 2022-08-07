using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepHealthBar : MonoBehaviour
{
	public Transform HPBarRoot;
	public Transform HPBar;
	private IEnumerator _updateCR;
	private Camera _cam;

	public void UpdateHP(float hpPercent)
	{
		HPBar.gameObject.SetActive(true);
		HPBar.transform.localPosition = Vector3.right * (-.25f + .25f * (1 - hpPercent));
		HPBar.transform.localScale = new Vector3(.5f * hpPercent, .1f, 1f);
	}
	void OnEnable()
	{
		_cam = Camera.main;
		this.RestartCoroutine(UpdateRotation, ref _updateCR);
	}
	void OnDestroy()
	{
		StopAllCoroutines();
	}
	void OnDisable()
	{
		StopAllCoroutines();
	}

	private IEnumerator UpdateRotation()
	{
		while (true)
		{
			HPBarRoot.rotation = Quaternion.LookRotation(_cam.transform.forward);
			yield return null;
		}
	}
}
