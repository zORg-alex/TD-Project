using BezierCurveZ;
using MeshGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[ExecuteAlways]
public class PathScript : MonoBehaviour
{
    public Curve curve;
    public Curve profile;
    public Vector3 scale = new Vector3(1,.1f);
	private MeshFilter _meshFilter;

	public void OnEnable()
	{
		_meshFilter = GetComponent<MeshFilter>();
		var mesh = ProfileUtility.GenerateProfileMesh(curve, profile, Vector3.zero, scale, true, true);
		_meshFilter.sharedMesh = mesh;
	}

	internal Vector3 GetSpawn()
	{
		return transform.TransformPoint(curve.GetPoint(0, 0));
	}

	internal Vector3 GetNextPosition(float currentDistance, Vector3 offset, float distance)
	{
		var rot = curve.GetRotationAtLength(currentDistance + distance);
		return transform.TransformPoint(curve.GetPointAtLength(currentDistance + distance) + rot * offset);
	}
}
