using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
	Camera cam;
	UIInput input;
	private void OnEnable()
	{
		cam = Camera.main;
		input = new UIInput();
		input.Enable();
		input.UI.PrimaryClick.started += PrimaryClick_started;
	}

	private void PrimaryClick_started(InputAction.CallbackContext obj)
	{
		var ray = cam.ScreenPointToRay(input.UI.PointerPosition.ReadValue<Vector2>());
		var hits = Physics.RaycastAll(ray, float.MaxValue, 1 << 8);
		foreach (var hit in hits)
		{
			var ctx = new ClickContext() { IsMouseButton = true, MouseButton = 0 };
				IClickable clickable = hit.transform.GetComponent<IClickable>();
			if (clickable != null)
			{
				clickable.OnClick(ctx);
				return;
			}
		}
	}

	void OnDrawGizmos()
	{
		var ray = cam.ScreenPointToRay(input.UI.PointerPosition.ReadValue<Vector2>());
		UnityEditor.Handles.DrawAAPolyLine(ray.origin, ray.origin + ray.direction * 10);
	}
}

public interface IClickable
{
    void OnClick(ClickContext ctx);
}

public struct ClickContext
{
    public bool IsMouseButton;
    public int MouseButton;
}