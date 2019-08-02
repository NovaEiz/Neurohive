using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nighday {

public class ControllerAngleBetweenObjects : MonoBehaviour {

#region Private fields
	
	[Header("AngleBetweenPointsInUI - Settable fields")]
	[SerializeField] private RectTransform _object1;
	[SerializeField] private RectTransform _object2;
	
	[Header("AngleBetweenPointsInUI - Debug fields")]
	[SerializeField] private float _angle;

	private Vector2 _savedPositionObject1;
	private Vector2 _savedPositionObject2;
	
#endregion

#region Private methods

	private void UpdatePositionLine() {
		var pos1  = _object1.anchoredPosition;
		var pos2  = _object2.anchoredPosition;
		
		if (_savedPositionObject1 == pos1 && _savedPositionObject2 == pos2) {
			return;
		}

		var angle = pos1.GetAngleToPoint(pos2);//GetAngle(pos1, pos2);
		var distance = Vector3.Distance(pos1, pos2);
		
		_angle = angle;
		_savedPositionObject1 = pos1;
		_savedPositionObject2 = pos2;
		
		OnChangedPositionObjects?.Invoke(pos1, pos2, angle, distance);
	}
	
#endregion

#region Unity Events

	private void Update() {
		UpdatePositionLine();
	}

#endregion

#region Events

	public event Action<Vector2, Vector2, float,  float> OnChangedPositionObjects;
		
#endregion
	
}

}
