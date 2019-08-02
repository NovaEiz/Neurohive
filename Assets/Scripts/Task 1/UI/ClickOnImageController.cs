using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nighday {

public class ClickOnImageController : MonoBehaviour, IPointerClickHandler {

#region IPointerClickHandler

	public void OnPointerClick(PointerEventData eventData) {
		OnClick?.Invoke(eventData.position);
	}

#endregion

#region Events

	public event Action<Vector2> OnClick;

#endregion
}

}