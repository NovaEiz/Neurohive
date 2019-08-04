using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nighday {

public class ClickOnLayer : MonoBehaviour {

#region Private fields

	[Header(" - Settable fields")]
	[SerializeField] private LayerMask _clickOnLayerMask;
	[SerializeField] private LayerMask _ignoreClickOnLayerMask;

	[Space]
	[SerializeField] private Camera _camera;

	private bool _draging;

	private RaycastHit _hit;

#endregion

#region Private methods


	private Vector3 GetPositionCursorInWorldSpace() {
		var cam = _camera;
		
		Vector3 point = new Vector3();

		point = cam.ScreenToWorldPoint(Input.mousePosition);

		return point;
	}

	private bool IsRaycastOnLayer(LayerMask layerMask, ref RaycastHit hit) {
		var camTr = _camera.transform;
		var camPos = camTr.position;
		
		var cursorPosition = GetPositionCursorInWorldSpace();

		var direction = cursorPosition - camPos;

		Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

		var res = Physics.Raycast(ray.origin, ray.direction, out hit, float.PositiveInfinity, layerMask);
		
		Debug.Log("res = " + res + "; layerMask = " + layerMask + "; ray = " + ray + "; cursorPosition = " + cursorPosition);
		Debug.Log("hit.point = " + hit.point);
		return res;
	}

	private bool CheckLayers(ref RaycastHit hit) {
		RaycastHit hitIgnoreClick = new RaycastHit();
		if (IsRaycastOnLayer(_clickOnLayerMask, ref hit) && !IsRaycastOnLayer(_ignoreClickOnLayerMask, ref hitIgnoreClick)) {
			return true;
		}
		return false;
	}

	private bool IsCursorDown() {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Down");

			if (CheckLayers(ref _hit)) {
				_draging = true;
				return true;
			}
		}
		return false;
	}

	private bool IsCursorUp() {
		if (!_draging) {
			return false;
		}

		if (Input.GetMouseButtonUp(0)) {
			Debug.Log("Up");
			if (CheckLayers(ref _hit)) {
				_draging = false;
				return true;
			}
		}
		return false;
	}

	private PointerEventData CreatePointerEventData() {
		var pointerEventData = new PointerEventData(EventSystem.current);
		
		var raycastResult = new RaycastResult();
		raycastResult.gameObject = _hit.transform.gameObject;
		raycastResult.worldPosition = _hit.point;
		pointerEventData.pointerCurrentRaycast = raycastResult;

		return pointerEventData;
	}

#endregion

#region Unity Event methods

	private void Update() {
		if (IsCursorDown()) {
			OnDown?.Invoke(CreatePointerEventData());
		} else if (IsCursorUp()) {
			var data = CreatePointerEventData();
			OnUp?.Invoke(data);
			OnClick?.Invoke(data);
		}
	}

#endregion

#region Events

	public event Action<PointerEventData> OnDown;
	public event Action<PointerEventData> OnUp;
	public event Action<PointerEventData> OnClick;

#endregion

}

}