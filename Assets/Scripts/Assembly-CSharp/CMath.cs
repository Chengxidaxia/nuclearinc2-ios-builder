using UnityEngine;

public static class CMath
{
	public static Vector3 ProjectToSphere(Vector3 v, float radius)
	{
		Vector3 result = v;
		result.Normalize();
		result.x *= radius;
		result.y *= radius;
		result.z *= radius;
		return result;
	}

	public static Vector3 TriNormal(Vector3 v1, Vector3 v2, Vector3 v3)
	{
		Vector3 lhs = default(Vector3);
		lhs.x = v2.x - v1.x;
		lhs.y = v2.y - v1.y;
		lhs.z = v2.z - v1.z;
		Vector3 rhs = default(Vector3);
		rhs.x = v3.x - v2.x;
		rhs.y = v3.y - v2.y;
		rhs.z = v3.z - v2.z;
		Vector3 result = Vector3.Cross(lhs, rhs);
		result.Normalize();
		return result;
	}

	public static float QuickDistance(Vector3 v1, Vector3 v2)
	{
		Vector3 vector = v2 - v1;
		return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
	}
}
