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
}
