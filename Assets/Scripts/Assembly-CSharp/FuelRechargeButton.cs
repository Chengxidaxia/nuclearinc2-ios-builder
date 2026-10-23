using UnityEngine;

public class FuelRechargeButton : MonoBehaviour
{
	public GameObject MainCamera;

	public GameObject Button;

	private void OnEnable()
	{
		InvokeRepeating("ReviveButton", 17f, 100f);
		InvokeRepeating("KillButton", 0f, 100f);
	}

	public void KillButton()
	{
		Button.SetActive(false);
	}

	public void ReviveButton()
	{
		Button.SetActive(true);
	}
}
