using UnityEngine;

public class UpdateEnergy : MonoBehaviour
{
	public GameObject MainCamera;

	private int i;

	private void OnEnable()
	{
		InvokeRepeating("Fuel", 1.2f, 3f);
		i = 0;
	}

	public void Fuel()
	{
		i++;
		if (i == 1)
		{
			UpdateFuel_1();
		}
		if (i == 2)
		{
			UpdateFuel_2();
		}
		if (i == 3)
		{
			UpdateFuel_3();
		}
	}

	public void UpdateFuel_1()
	{
		float num = 100f - MainCamera.GetComponent<ReactorParametrsSqript>().FuelAmount;
		MainCamera.GetComponent<ReactorParametrsSqript>().FuelAmount = MainCamera.GetComponent<ReactorParametrsSqript>().FuelAmount + num / 3f;
		MainCamera.GetComponent<ReactorParametrsSqript>().Radiation = MainCamera.GetComponent<ReactorParametrsSqript>().Radiation + 2f;
	}

	public void UpdateFuel_2()
	{
		float num = 100f - MainCamera.GetComponent<ReactorParametrsSqript>().FuelAmount;
		MainCamera.GetComponent<ReactorParametrsSqript>().FuelAmount = MainCamera.GetComponent<ReactorParametrsSqript>().FuelAmount + num / 2f;
		MainCamera.GetComponent<ReactorParametrsSqript>().Radiation = MainCamera.GetComponent<ReactorParametrsSqript>().Radiation + 2f;
	}

	public void UpdateFuel_3()
	{
		float num = 100f - MainCamera.GetComponent<ReactorParametrsSqript>().FuelAmount;
		MainCamera.GetComponent<ReactorParametrsSqript>().FuelAmount = MainCamera.GetComponent<ReactorParametrsSqript>().FuelAmount + num;
		MainCamera.GetComponent<ReactorParametrsSqript>().Radiation = MainCamera.GetComponent<ReactorParametrsSqript>().Radiation + 2f;
	}
}
