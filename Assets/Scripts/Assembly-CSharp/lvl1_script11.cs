using UnityEngine;

public class lvl1_script11 : MonoBehaviour
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

	public GameObject Slide2;

	public GameObject Slide3;

	public GameObject Transparent;

	public GameObject WinPanel;

	public GameObject NoTimePanel;

	public bool FindOnce1000 = true;

	public bool FindOnce50 = true;

	public bool GoodOnce;

	public bool NoTimeOnce = true;

	public float DestroyTime;

	private void Start()
	{
		Time.timeScale = 0f;
		MaxEnergy = 11200f;
		MaxTime = 600f;
		FindOnce1000 = true;
		FindOnce50 = false;
		GoodOnce = false;
		NoTimeOnce = true;
		DestroyTime = 0f;
		ParametrsScript.GetComponent<ReactorParametrsSqript>().EnableFire = true;
		ParametrsScript.GetComponent<ReactorParametrsSqript>().EnableEarth = true;
		ParametrsScript.GetComponent<ReactorParametrsSqript>().Money = 1000f;
		Transparent.SetActive(true);
		InvokeRepeating("Find", 1f, 1f);
	}

	public void Find()
	{
		TimeLeft++;
		Energy += ParametrsScript.GetComponent<ReactorParametrsSqript>().Power;
		EnergyPanel.GetComponent<RectTransform>().localScale = new Vector3(Energy / MaxEnergy, 1f, 1f);
		TimePanel.GetComponent<RectTransform>().localScale = new Vector3(TimeLeft / MaxTime, 1f, 1f);
		if (TimeLeft > MaxTime * 0.5f && FindOnce1000)
		{
			DestroyTime = TimeLeft + 10f;
			FindOnce1000 = false;
			FindOnce50 = true;
			Time.timeScale = 0f;
			Slide2.SetActive(true);
			Transparent.SetActive(true);
		}
		if (DestroyTime < TimeLeft && FindOnce50)
		{
			FindOnce50 = false;
			Time.timeScale = 0f;
			Slide3.SetActive(true);
			Transparent.SetActive(true);
			ParametrsScript.GetComponent<ReactorParametrsSqript>().Fusushima = 1f;
			GoodOnce = true;
		}
		if (ParametrsScript.GetComponent<ReactorParametrsSqript>().TempReactor < 100f && ParametrsScript.GetComponent<ReactorParametrsSqript>().TempGenerat < 100f && GoodOnce)
		{
			GoodOnce = false;
			Time.timeScale = 0f;
			WinPanel.SetActive(true);
			Music.SetActive(false);
			PlayerPrefs.SetInt("nextlevel", 12);
			if (PlayerPrefs.GetInt("lvlunlocked") < 12)
			{
				PlayerPrefs.SetInt("lvlunlocked", 12);
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
