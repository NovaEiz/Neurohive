using System;
using UnityEngine;

namespace Nighday {

public class SpawnObjectInBordersByObjects : MonoBehaviour {

#region Private fields

	[Header(" - Settable fields")]
	[SerializeField] private Transform _object;

	[SerializeField] private Transform _outsideBordersScale;
	[SerializeField] private Transform _insideBordersScale;

#endregion

#region Private methods

	private Vector3 GetPosition() {
		var scaleOutside = _outsideBordersScale.localScale;
		
		Vector3 scaleInside = Vector3.zero;
		if (_insideBordersScale != null) {
			scaleInside = _insideBordersScale.localScale;
		}

		var lengthX = scaleOutside.x/2 - scaleInside.x/2;
		var lengthZ = scaleOutside.z/2 - scaleInside.z/2;
		
		var position = new Vector3(
			UnityEngine.Random.Range(-lengthX, lengthX),	
			0,
			UnityEngine.Random.Range(-lengthZ, lengthZ)
		);

		if (position.x < 0) {
			position.x += - scaleInside.x/2;
		} else {
			position.x += scaleInside.x/2;
		}
		if (position.z < 0) {
			position.z += - scaleInside.z/2;
		} else {
			position.z += scaleInside.z/2;
		}

		return position;
	}

#endregion

#region Public methods

	public void RespawnObject() {
		_object.localPosition = GetPosition();
	}

#endregion

	private void Awake() {
		RespawnObject();
	}

}

}