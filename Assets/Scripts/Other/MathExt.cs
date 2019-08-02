using UnityEngine;

namespace Nighday {
public static class MathExt {
	
	/// <summary>
	/// Получить угол по вектору, направленному к точке
	/// </summary>
	/// <param name="self"></param>
	/// <param name="pos1"></param>
	/// <param name="pos2"></param>
	/// <returns></returns>
	public static float GetAngleToPoint(this Vector2 self, Vector2 point) {
		return Math.GetAngleBetweenPoints(self, point);
	}
	
}

}