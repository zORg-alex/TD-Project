using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerBase))]
public class TowerBaseUI : MonoBehaviour, IClickable
{
	private Collider _collider;
	private TowerBase _base;
	public BuildTowerUIPanel UIPanelPrefab;
	public Transform UIRoot;
	BuildTowerUIPanel uiPanel;
	public bool PanelVisible => uiPanel.gameObject.activeSelf;

	void OnEnable()
	{
		_base = GetComponent<TowerBase>();
		_collider = GetComponent<Collider>();
		gameObject.layer = 8;
	}

	public void OnClick(ClickContext ctx)
	{
		if (ctx.IsMouseButton && ctx.MouseButton == 0)
		{
			OpenUI();
		}
	}

	internal void OpenUI()
	{
		if (_base.isTowerBuilt) return;
		if (!uiPanel)
			InstantiatePanel();
		uiPanel.Init(_base, HideUI);
		uiPanel.gameObject.SetActive(true);
	}
	internal void HideUI()
	{
		if (!uiPanel)
			InstantiatePanel();
		uiPanel.gameObject.SetActive(false);
	}

	private void InstantiatePanel()
	{
		uiPanel = Instantiate(UIPanelPrefab).ResetTransform(UIRoot, true);
	}
}
