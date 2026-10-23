using UnityEngine;

public class lvl1_script12 : MonoBehaviour
{
	public string NextLvl;

	public float Energy;

	public float TimeLeft;

	public float MaxEnergy = 2500f;

	public float MaxTime = 300f;

	public GameObject EnergyPanel;

	public GameObject TimePanel;

	public GameObject ParametrsScript;

	public GameObject Music;

	public GameObject Slide3;

	public GameObject Transparent;

	public GameObject WinPanel;

	public GameObject NoTimePanel;

	public GameObject BuyGener;

	public GameObject Cooling;

	public bool FindOnce1000 = true;

	public bool FindOnce50 = true;

	public bool GoodOnce;

	public bool NoTimeOnce = true;

	public float DestroyTime;

	private void Start()
	{
		Time.timeScale = 0f;
		MaxEnergy = 7000f;
		MaxTime = 600f;
		FindOnce1000 = true;
		FindOnce50 = false;
		GoodOnce = false;
		NoTimeOnce = true;
		DestroyTime = 0f;
		ParametrsScript.GetComponent<ReactorParametrsSqript>().EnableFire = true;
		ParametrsScript.GetComponent<ReactorParametrsSqript>().EnableEarth = true;
		ParametrsScript.GetComponent<ReactorParametrsSqript>().Money = 2500f;
		Transparent.SetActive(true);
		InvokeRepeating("Find", 1f, 1f);
		EnergyPanel.SetActive(false);
		TimePanel.SetActive(false);
	}

	public void Find()
	{
		TimeLeft++;
		if (ParametrsScript.GetComponent<ReactorParametrsSqript>().TempReactor > 2300f && FindOnce1000 && ParametrsScript.GetComponent<ReactorParametrsSqript>().KSterj == 1f)
		{
			DestroyTime = TimeLeft + 5f;
			FindOnce1000 = false;
			FindOnce50 = true;
			Time.timeScale = 0f;
			Slide3.SetActive(true);
			Transparent.SetActive(true);
		}
		if (DestroyTime < TimeLeft && FindOnce50)
		{
			FindOnce50 = false;
			ParametrsScript.GetComponent<ReactorParametrsSqript>().Chern = 1f;
			Cooling.SetActive(false);
			BuyGener.SetActive(false);
			GoodOnce = true;
		}
		if (ParametrsScript.GetComponent<ReactorParametrsSqript>().TempReactor < 300f && GoodOnce)
		{
			GoodOnce = false;
			Time.timeScale = 0f;
			WinPanel.SetActive(true);
			Music.SetActive(false);
			PlayerPrefs.SetInt("nextlevel", 13);
			if (PlayerPrefs.GetInt("lvlunlocked") < 13)
			{
				PlayerPrefs.SetInt("lvlunlocked", 13);
			}
		}
	}
}
