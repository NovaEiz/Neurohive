using System;
using TMPro;
using UnityEngine;

namespace Nighday {

public class AngleView : MonoBehaviour {

#region Private fields

	[Header(" - Settable fields")]
	[SerializeField] private TextMeshProUGUI _angleText;

	private string _angleTextForReplace;

#endregion

#region Public methods

	public void SetAngle(float value) {
		var angle = value + 90;

		if (angle > 360) {
			angle -= 360;
		}
		
		var angleStr = angle.ToString("0.00");
		_angleText.text = _angleTextForReplace.Replace("{1}", angleStr);
	}

#endregion

#region Unity Event methods

	private void Awake() {
		_angleTextForReplace = _angleText.text;
	}

#endregion

}

}