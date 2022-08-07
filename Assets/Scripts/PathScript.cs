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

	internal Vector3 GetPosition(float distnce, Vector3 offset)
	{
		var rot = curve._vertexData.GetRotationAtLength(distnce);
		return transform.TransformPoint(curve._vertexData.GetPointAtLength(distnce) + rot * offset);
	}

	public float FullLength => curve.VertexDataLength;
	public float GetDistance(Vector3 position)
	{
		var t = curve.GetClosestPointTimeSegment(transform.InverseTransformPoint(position), out int segmentInd);
		//lines.Add(new Vector3[] { position, curve.GetPoint(segmentInd, t) });
		return curve._vertexData.GetLength(segmentInd, t);
	}

	//List<Vector3[]> lines = new List<Vector3[]>();
	//void OnDrawGizmos()
	//{
	//	foreach (var l in lines)
	//	{
	//		UnityEditor.Handles.DrawAAPolyLine(l[0], transform.TransformPoint(l[1]));
	//	}
	//	if (!UnityEditor.EditorApplication.isPaused)
	//		lines.Clear();
	//}
}
