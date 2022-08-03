using BezierCurveZ;
using MeshGeneration;
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
}
