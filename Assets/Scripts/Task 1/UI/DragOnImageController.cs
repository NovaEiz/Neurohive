using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nighday {

public class DragOnImageController : MonoBehaviour, IDragHandler {

#region IDragHandler

	public void OnDrag(PointerEventData eventData) {
		OnDragEvent?.Invoke(eventData.position);
	}

#endregion

#region Events

	public event Action<Vector2> OnDragEvent;

#endregion

}

}