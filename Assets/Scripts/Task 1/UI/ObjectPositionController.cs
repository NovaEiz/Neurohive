using System;
using UnityEngine;

namespace Nighday {

public class ObjectPositionController : MonoBehaviour {

#region Private fields

	[Header(" - Settable fields")]
	[SerializeField] private RectTransform _object;

#endregion

#region Public methods

	public void SetPosition(Vector2 position) {
		_object.anchoredPosition = position;
	}

#endregion

}

}