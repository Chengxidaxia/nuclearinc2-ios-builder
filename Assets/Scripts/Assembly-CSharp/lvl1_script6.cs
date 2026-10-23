using UnityEngine;

public class lvl1_script6 : MonoBehaviour
{
	public string NextLvl;

	public float Energy;

	public float TimeLeft;

	public float MaxEnergy = 2500f;

	public float MaxTime = 360f;

	public GameObject Rods;

	public GameObject Self;

	public GameObject Water;

	public GameObject Turbine;

	public GameObject Reactor;

	public GameObject EnergyPanel;

	public GameObject TimePanel;

	public GameObject Music;

	public GameObject ParametrsScript;

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
		MaxEnergy = 3100f;
		MaxTime = 420f;
		ParametrsScript.GetComponent<ReactorParametrsSqript>().EnableFire = true;
		ParametrsScript.GetComponent<ReactorParametrsSqript>().Money = 250f;
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
			PlayerPrefs.SetInt("nextlevel", 7);
			if (PlayerPrefs.GetInt("lvlunlocked") < 7)
			{
				PlayerPrefs.SetInt("lvlunlocked", 7);
			}
		}
		if (TimeLeft > MaxTime && NoTimeOnce)
		{
			NoTimeOnce = false;
			Time.timeScale = 0f;
			NoTimePanel.SetActive(true);
		}
	}
}
