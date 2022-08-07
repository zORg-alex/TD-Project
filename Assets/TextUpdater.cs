using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextUpdater : MonoBehaviour
{
	TMP_Text textPro;
	public string format = "{0}";

	public void UpdateText(float value)
	{
		if (!textPro) textPro = GetComponent<TMP_Text>();
		if (textPro) textPro.text = string.Format(format, value);
	}
	public void UpdateText(float val1, float val2)
	{
		if (!textPro) textPro = GetComponent<TMP_Text>();
		if (textPro) textPro.text = string.Format(format, val1, val2);
	}
}
