using System;
using UnityEngine;

namespace Nighday {

public class LineView : MonoBehaviour {

#region Private fields

	[Header("AngleBetweenPointsInUI - Settable fields")]
	[SerializeField] private RectTransform _line;

#endregion

#region Private methods

	private void SetPosition(Vector2 position) {
		_line.anchoredPosition = position;
	}

	private void SetAngle(float angle) {
		_line.rotation = Quaternion.Euler(0, 0, angle);
	}

	private void SetLength(float length) {
		var sizeDelta = _line.sizeDelta;
		sizeDelta.y     = length;
		_line.sizeDelta = sizeDelta;
	}

#endregion

#region Public methods

	public void SetData(Vector2 position, float angle, float length) {
		SetPosition(position);
		SetAngle(angle);
		SetLength(length);
	}

#endregion

}

}