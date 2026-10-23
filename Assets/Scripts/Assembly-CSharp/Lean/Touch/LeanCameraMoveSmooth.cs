using UnityEngine;

namespace Lean.Touch
{
	public class LeanCameraMoveSmooth : LeanCameraMove
	{
		[Tooltip("How quickly the zoom reaches the target value")]
		public float Dampening = 10f;

		private Vector3 remainingDelta;

		protected override void LateUpdate()
		{
			Vector3 localPosition = base.transform.localPosition;
			base.LateUpdate();
			remainingDelta += base.transform.localPosition - localPosition;
			float dampenFactor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime);
			Vector3 vector = Vector3.Lerp(remainingDelta, Vector3.zero, dampenFactor);
			base.transform.localPosition = localPosition + remainingDelta - vector;
			remainingDelta = vector;
		}
	}
}
