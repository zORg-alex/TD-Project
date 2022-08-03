using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildTowerUIPanel : MonoBehaviour
{
	public Button buildButton;
	public TMP_Text buttonText;
	public Image buttonIcon;
	public List<Button> buttons = new List<Button>();
	private TowerBase _base;
	private Action _onClick;
	public Transform root;

	void OnEnable()
	{
		buildButton.gameObject.SetActive(true);


		foreach (var tower in TDTowerCollection.Instance.TowerCollection)
		{
			buttonText.text = tower.name;
			//setIcon here
			var b = Instantiate(buildButton, buildButton.transform.parent);
			var t = tower;
			b.onClick.AddListener(() =>
			{
				_base.Build(t);
				_onClick();
			});
			buttons.Add(b);
		}

		buildButton.gameObject.SetActive(false);
	}

	internal void Init(TowerBase towerBase, Action onClick)
	{
		_base = towerBase;
		_onClick = onClick;
	}
}
