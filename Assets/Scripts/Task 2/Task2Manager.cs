using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nighday {


public class Task2Manager : MonoBehaviour {

#region Private fields

	[Header(" - Settable fields")]
	[SerializeField] private ClickOnLayer _clickOnGroundBehindRoom;
	[SerializeField] private SpawnPointMovement _spawnPointMovement;
	[SerializeField] private AgentsDestinationController _agentsDestinationController;
	
	[Space]
	[SerializeField] private SpawnObjectInBordersByObjects _spawnerInsideRoom;
	[SerializeField] private SpawnObjectInBordersByObjects _spawnerOutsideRoom;

	[Space]
	[SerializeField] private Button _restartButton;

#endregion

#region Unity Event methods

	private void Awake() {
		_clickOnGroundBehindRoom.OnClick += (PointerEventData pointerEventData) => {
			var destination = pointerEventData.pointerCurrentRaycast.worldPosition;
			Debug.Log("destination = " + destination);
			_spawnPointMovement.SetPosition(destination);
			_agentsDestinationController.SetDestination(destination);
		};
		
		_restartButton.onClick.AddListener(() => {
			_agentsDestinationController.StopMove();
			_spawnerInsideRoom.RespawnObject();
			_spawnerOutsideRoom.RespawnObject();

			_spawnPointMovement.Restart();
		});
	}

#endregion

}

}