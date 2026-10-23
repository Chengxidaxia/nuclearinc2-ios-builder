using UnityEngine;

public class lvl1_script8 : MonoBehaviour
{
	public string NextLvl;

	public float Energy;

	public float TimeLeft;

	public float MaxEnergy = 2500f;

	public float MaxTime = 300f;

	public GameObject Water;

	public GameObject Reactor;

	public GameObject EnergyPanel;

	public GameObject TimePanel;

	public GameObject ParametrsScript;

	public GameObject Music;

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
		MaxEnergy = 6000f;
		MaxTime = 480f;
		ParametrsScript.GetComponent<ReactorParametrsSqript>().EnableFire = true;
		ParametrsScript.GetComponent<ReactorParametrsSqript>().EnableEarth = true;
		ParametrsScript.GetComponent<ReactorParametrsSqript>().Money = 1200f;
		Water.SetActive(false);
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
			PlayerPrefs.SetInt("nextlevel", 9);
			if (PlayerPrefs.GetInt("lvlunlocked") < 9)
			{
				PlayerPrefs.SetInt("lvlunlocked", 9);
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
