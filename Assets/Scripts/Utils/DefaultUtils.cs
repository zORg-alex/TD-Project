using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DefaultUtils
{
	public static void RestartCoroutine(this MonoBehaviour mbeh, Func<IEnumerator> coroutineMethod, ref IEnumerator prevCoroutine)
	{
		if (prevCoroutine != null) mbeh.StopCoroutine(prevCoroutine);
		prevCoroutine = coroutineMethod();
		mbeh.StartCoroutine(prevCoroutine);
	}

	public static void Reset(this Transform t, Transform parent = null, bool keepScale = false)
	{
		if (parent != null && parent)
			t.parent = parent;
		t.localPosition = Vector3.zero;
		t.localRotation = Quaternion.identity;
		if (!keepScale)
			t.localScale = Vector3.one;
	}

	public static T ResetTransform<T>(this T script, Transform parent = null, bool keepScale = false) where T : Component
	{
		script.transform.Reset(parent, keepScale);
		return script;
	}
	public static GameObject ResetTransform(this GameObject go, Transform parent = null, bool keepScale = false)
	{
		go.transform.Reset(parent, keepScale);
		return go;
	}
}
