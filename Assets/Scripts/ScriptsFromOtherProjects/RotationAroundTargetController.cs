using System;
using UnityEngine;

namespace Nighday {

public interface IRotationAroundTargetController {
	
	void SetDistance(float distance);
	void SetAngleX(float   y);
	void SetAngleY(float   y);
	
	void UpdatePositionAroundTarget(bool smooth = false);
	
	Vector2 Angles   { get; }
	float   Distance { get; }

	event Action<Vector2> OnChangedAngles;

}

[ExecuteInEditMode]
public class RotationAroundTargetController : MonoBehaviour, IRotationAroundTargetController {


	public event Action<float> OnChangeDistance;
	
#region Private fields

	[Header(" - Settable fields")]
	[SerializeField] private Vector2 _angles;
	[SerializeField] private float _distance;
	[SerializeField] private float _moveSpeedAngles;
	[SerializeField] private float _moveSpeedDistance;
	[SerializeField] private float _moveSpeedPosition;

	[Space]
	[SerializeField] private bool _smoothMove;

	[Space]
	[SerializeField] private bool _readOnlyAngleX;
	[SerializeField] private bool _readOnlyAngleY;

	[Space]
	[SerializeField] private Vector3 _offsetPosition;
	
	[Space]
	[SerializeField] protected Transform _target;
	[Space]
	[SerializeField] protected Camera _camera;

	[Header(" = Debug fields")]
	[SerializeField] private Vector2 _currentAngles;
	[SerializeField] private float _currentDistance;
	[SerializeField] private Vector3 _currentPosition;

#endregion
	
#region Public fields

	public Transform Target => _target;
	
	public Vector2 Angles => _currentAngles;
	
	public float Distance => _distance;
	
	public void SetDistance(float distance) {
		_distance = distance;

		_camera.orthographicSize = distance;
		
		OnChangeDistance?.Invoke(_distance);
	}

	public void SetAngleX(float x) {
		if (_readOnlyAngleX) {
			return;
		}
		_angles.x = x;
	}
	public void SetAngleY(float y) {
		if (_readOnlyAngleY) {
			return;
		}
		_angles.y = y;
	}
	public void ChangeAngleX(float x) {
		if (_readOnlyAngleX) {
			return;
		}
		_angles.x += x;
	}
	public void ChangeAngleY(float y) {
		if (_readOnlyAngleX) {
			return;
		}
		_angles.y += y;
	}
	
	

#endregion

#region Private methods

	private Vector3 GetCurrentPosition() {
		var currentEuler           = new Vector3(-_currentAngles.x, _currentAngles.y - 180, 0);
		var rotation               = Quaternion.Euler(currentEuler);
		var positionByAroundSphere = rotation * Vector3.forward * _currentDistance;

		var currentPosition = positionByAroundSphere;
		if (_target != null) {
			currentPosition += _currentPosition;
		}
		
		return currentPosition;
	}
	
	private void UpdateDirection() {
		var lookAtPosition = Vector3.zero;
		if (_target != null) {
			lookAtPosition = _target.position;
		}
		transform.LookAt(lookAtPosition);
	}


	private void UpdatePosition(bool smooth = false) {
		if (smooth) {
			var step = _moveSpeedPosition * Time.deltaTime;
			_currentPosition = Vector3.MoveTowards(_currentPosition, _target.position, step);
		} else {
			if (_target != null) {
				_currentPosition = _target.position;
			}
		}
	}
	private void UpdateDistance(bool smooth = false) {
		if (smooth) {
			var step = _moveSpeedDistance * Time.deltaTime;
			_currentDistance = Mathf.MoveTowards(_currentDistance, _distance, step);
		} else {
			_currentDistance = _distance;
		}
	}
	private void UpdateAngles(bool smooth = false) {
		if (smooth) {
			var step = _moveSpeedAngles * Time.deltaTime;
		
			var currentEuler = new Vector3(_currentAngles.x, _currentAngles.y, 0);
			var euler        = new Vector3(_angles.x,        _angles.y,        0);
			var rotation     = Quaternion.Euler(currentEuler);
			var toRotation   = Quaternion.Euler(euler);
			
			rotation       = Quaternion.RotateTowards(rotation, toRotation, step);
			if (Vector3.Distance(_currentAngles, rotation.eulerAngles) > float.Epsilon) {
				OnChangedAngles?.Invoke(rotation.eulerAngles);
			}
			_currentAngles = rotation.eulerAngles;
		} else {
			if (Vector3.Distance(_currentAngles, _angles) > float.Epsilon) {
				OnChangedAngles?.Invoke(_angles);
			}
			_currentAngles = _angles;
		}
	}

#endregion

#region Public methods

	private bool _isUpdated;

	public void SetTarget(Transform target) {
		_target = target;
	}
	
	public void UpdatePositionAroundTarget(bool smooth = false) {
		if (_isUpdated) {
			return;
		}

		UpdateAngles(smooth);
		UpdateDistance(smooth);
		UpdatePosition(smooth);

		var toPosition = GetCurrentPosition() + _offsetPosition;
		transform.position = toPosition;
		
		UpdateDirection();
	}

#endregion
	
#region Unity event methods
	
	protected virtual void LateUpdate() {
		if (!_isUpdated) {
			UpdatePositionAroundTarget(_smoothMove);
		}

		_isUpdated = false;
	}
	

	
	protected virtual void Awake() {
		_currentPosition = transform.position;
		_currentAngles   = _angles;
		_currentDistance = _distance;
	}
	
#endregion
	
	public event Action<Vector2> OnChangedAngles;

}

}