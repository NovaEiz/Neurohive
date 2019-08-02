using System;
using UnityEngine;

namespace Nighday {

public class Task1Manager : MonoBehaviour {
	
#region Private fields

	[Header(" - Settable fields")]
	[SerializeField] private ControllerAngleBetweenObjects _controllerAngleBetweenObjects;
	[SerializeField] private LineView _lineView;
	[SerializeField] private AngleView _angleView;
	[SerializeField] private DragOnImageController _dragOnImageController;
	[SerializeField] private ObjectPositionController _objectPositionController;

#endregion

#region Unity Event methods

	private void Awake() {
		_controllerAngleBetweenObjects.OnChangedPositionObjects += (Vector2 pos1, Vector2 pos2, float angle, float distance) => {
			_lineView.SetData(pos1, angle, distance);
			_angleView.SetAngle(angle);
		};

		_dragOnImageController.OnDragEvent += _objectPositionController.SetPosition;
	}

#endregion

}

}