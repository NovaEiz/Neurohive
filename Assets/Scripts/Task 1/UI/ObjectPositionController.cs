using System;
using UnityEngine;

namespace Nighday {

public class ObjectPositionController : MonoBehaviour {

#region Private fields

	[Header(" - Settable fields")]
	[SerializeField] private RectTransform _object;

	[Space]
	[SerializeField] private RectTransform _rtCanvas;
	[SerializeField] private Camera _camera;
	
#endregion

#region Public methods

	public void SetPosition(Vector2 position) {
		Debug.Log("position = " + position);
		
		RectTransformUtility.ScreenPointToLocalPointInRectangle(_rtCanvas, position, null, out Vector2 res);
		Debug.Log("res = " + res);

		_object.anchoredPosition = res;
	}

#endregion

}

}