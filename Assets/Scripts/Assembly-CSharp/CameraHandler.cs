using UnityEngine;

public class CameraHandler : MonoBehaviour
{
	public bool OnInterface;

	public GameObject MainCamera;

	public float speed = 0.01f;

	public float scale = 0.01f;

	public float z;

	public GameObject test;

	public float perspectiveZoomSpeed = 0.5f;

	private void Update()
	{
		if (Input.touchCount == 2)
		{
			Touch touch = Input.GetTouch(0);
			Touch touch2 = Input.GetTouch(1);
			Vector2 vector = touch.position - touch.deltaPosition;
			Vector2 vector2 = touch2.position - touch2.deltaPosition;
			float magnitude = (vector - vector2).magnitude;
			float magnitude2 = (touch.position - touch2.position).magnitude;
			float num = magnitude - magnitude2;
			base.transform.Translate(0f, 0f, (0f - num) * scale);
		}
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y, Mathf.Clamp(base.transform.position.z, -13.34f, -6.5f));
		base.transform.position = position;
		position = new Vector3(Mathf.Clamp(base.transform.position.x, -1.97f + 5f * (-13.34f - base.transform.position.z) / 9f, -1.97f - 5f * (-13.34f - base.transform.position.z) / 9f), Mathf.Clamp(base.transform.position.y, 7.2f + 7.7f * (-13.34f - base.transform.position.z) / 9f, 7.2f - 2.5f * (-13.34f - base.transform.position.z) / 9f), base.transform.position.z);
		base.transform.position = position;
	}
}
