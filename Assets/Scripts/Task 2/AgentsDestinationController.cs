using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Nighday {

public class AgentsDestinationController : MonoBehaviour {
	
#region Private fields

	[Header(" - Settable fields")]
	[SerializeField] private List<NavMeshAgent> _agents;
	
#endregion
	
#region Private methods

	private void StartMove() {
		foreach (var agent in _agents) {
			agent.isStopped = false;
		}
	}

	private float GetDistance(NavMeshPath path) {
		float dist = 0f;
		for (int i = 0; i < path.corners.Length - 1; i++)
		{
			dist += Vector3.Distance(path.corners[i], path.corners[i + 1]);
		}

		return dist;
	}
	
	private void CalculateMoveSpeed() {
		var maxDistance = GetDistance(_agents[0].path);//_agents[0].remainingDistance;
		Debug.Log("maxDistance = " + maxDistance);
		foreach (var agent in _agents) {
			var distance = GetDistance(agent.path);//agent.remainingDistance;
			if (distance > maxDistance) {
				maxDistance = distance;
			}
		}

		foreach (var agent in _agents) {
			var distance = GetDistance(agent.path);//agent.remainingDistance;
			var speed    = distance / maxDistance;
			agent.speed = speed;
		}
	}

	private IEnumerator SetDestinationIe(Vector3 destination) {
		StopMove();
		
		foreach (var agent in _agents) {
			agent.SetDestination(destination);
		}

		var isPendingPath = true;
		while (isPendingPath) {
			var isProccess = false;
			
			foreach (var agent in _agents) {
				if (agent.pathPending) {
					isProccess = true;
				}
			}

			isPendingPath = isProccess;
			yield return null;
		}
		
		yield return null;

		CalculateMoveSpeed();
		StartMove();
	}

#endregion
	
#region Public methods
	
	public void StopMove() {
		foreach (var agent in _agents) {
			agent.isStopped = true;
		}
	}
	
	public void SetDestination(Vector3 destination) {
		StartCoroutine(SetDestinationIe(destination));
	}
	
#endregion

}

}