using System;
using UnityEngine;

namespace Nighday {

public class SpawnPointMovement : MonoBehaviour {

#region Private fields

	[Header(" - Settable fields")]
	[SerializeField] private Transform _object;

#endregion

#region Public methods

	public void SetPosition(Vector3 position) {
		_object.position = position;
		_object.gameObject.SetActive(true);
	}

	public void Restart() {
		_object.gameObject.SetActive(false);
	}

#endregion

#region Public methods

	private void Awake() {
		Restart();
	}

#endregion

}

}