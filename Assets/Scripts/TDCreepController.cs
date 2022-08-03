using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDCreepController : MonoBehaviour
{
	public PathScript path;

	void OnEnable()
	{
		if (!path) path = FindObjectOfType<PathScript>();
		if (!path) Debug.LogError("PathScript not found");
	}
}
