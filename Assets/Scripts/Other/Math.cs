using UnityEngine;

namespace Nighday {

public class Math {
	
	public static float GetAngleBetweenPoints(Vector2 pos1, Vector2 pos2) {
		var direction = (pos2 - pos1).normalized;
		var angle     = Vector2.SignedAngle(Vector2.up, direction);
		if (angle < 0) {
			angle = 360 + angle;
		}
		return angle;
	}
	
}

}