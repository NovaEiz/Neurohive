using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nighday {

public class ControllerAngleBetweenObjects : MonoBehaviour {

#region Private fields
	
	[Header("AngleBetweenPointsInUI - Settable fields")]
	[SerializeField] private RectTransform _objectA;
	[SerializeField] private RectTransform _objectB;
	
	[Header("AngleBetweenPointsInUI - Debug fields")]
	[SerializeField] private float _angle;

	private Vector2 _savedPositionObject1;
	private Vector2 _savedPositionObject2;
	
#endregion

#region Private methods

	private void UpdateRotationObjectA() {
		_objectA.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
	}

	private void UpdatePositionLine() {
		var pos1  = _objectA.anchoredPosition;
		var pos2  = _objectB.anchoredPosition;
		
		if (_savedPositionObject1 == pos1 && _savedPositionObject2 == pos2) {
			return;
		}

		var angle = pos1.GetAngleToPoint(pos2);//GetAngle(pos1, pos2);
		var distance = Vector3.Distance(pos1, pos2);
		
		_angle = angle;
		_savedPositionObject1 = pos1;
		_savedPositionObject2 = pos2;
		
		UpdateRotationObjectA();
		
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
