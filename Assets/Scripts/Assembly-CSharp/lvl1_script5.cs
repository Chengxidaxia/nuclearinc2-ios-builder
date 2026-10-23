using UnityEngine;

public class lvl1_script5 : MonoBehaviour
{
	public string NextLvl;

	public float Energy;

	public float TimeLeft;

	public float MaxEnergy = 2500f;

	public float MaxTime = 300f;

	public GameObject Valve;

	public GameObject Radiation;

	public GameObject Fire;

	public GameObject Rods;

	public GameObject Self;

	public GameObject Water;

	public GameObject Turbine;

	public GameObject Reactor;

	public GameObject Music;

	public GameObject EnergyPanel;

	public GameObject TimePanel;

	public GameObject ParametrsScript;

	public GameObject Slide3;

	public GameObject Transparent;

	public GameObject WinPanel;

	public GameObject NoTimePanel;

	public bool FindOnce1000 = true;

	public bool FindOnce50 = true;

	public bool GoodOnce = true;

	public bool NoTimeOnce = true;

	private void Start()
	{
		Time.timeScale = 0f;
		MaxEnergy = 2820f;
		MaxTime = 420f;
		Valve.SetActive(false);
		Radiation.SetActive(false);
		Fire.SetActive(false);
		Rods.SetActive(false);
		Self.SetActive(false);
		Water.SetActive(false);
		Turbine.SetActive(false);
		Reactor.SetActive(false);
		Transparent.SetActive(true);
		InvokeRepeating("Find", 1f, 1f);
	}

	public void Find()
	{
		TimeLeft++;
		Energy += ParametrsScript.GetComponent<ReactorParametrsSqript>().Power;
		EnergyPanel.GetComponent<RectTransform>().localScale = new Vector3(Energy / MaxEnergy, 1f, 1f);
		TimePanel.GetComponent<RectTransform>().localScale = new Vector3(TimeLeft / MaxTime, 1f, 1f);
		if (Energy > MaxEnergy && GoodOnce)
		{
			GoodOnce = false;
			Time.timeScale = 0f;
			WinPanel.SetActive(true);
			Music.SetActive(false);
			PlayerPrefs.SetInt("nextlevel", 6);
			if (PlayerPrefs.GetInt("lvlunlocked") < 6)
			{
				PlayerPrefs.SetInt("lvlunlocked", 6);
			}
		}
		if (ParametrsScript.GetComponent<ReactorParametrsSqript>().FuelAmount < 30f && FindOnce1000)
		{
			FindOnce1000 = false;
			Time.timeScale = 0f;
			Slide3.SetActive(true);
			Transparent.SetActive(true);
		}
		if (TimeLeft > MaxTime && NoTimeOnce)
		{
			NoTimeOnce = false;
			Time.timeScale = 0f;
			NoTimePanel.SetActive(true);
		}
	}
}
