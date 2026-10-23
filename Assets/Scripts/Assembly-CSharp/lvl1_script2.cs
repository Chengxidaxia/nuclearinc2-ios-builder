using UnityEngine;
using UnityEngine.UI;

public class lvl1_script2 : MonoBehaviour
{
	public string NextLvl;

	public GameObject Valve;

	public GameObject Radiation;

	public GameObject Fire;

	public GameObject Rods;

	public GameObject Self;

	public GameObject Water;

	public GameObject Turbine;

	public GameObject Reactor;

	public GameObject ReFuel;

	public GameObject UpgradeButton;

	public GameObject Energy;

	public GameObject TimeText;

	public GameObject BuyGenerPanel;

	public GameObject ParametrsScript;

	public GameObject Slide6;

	public GameObject Transparent;

	public GameObject NasosSlider;

	public GameObject NasosSliderBlock;

	public GameObject WinPanel;

	public GameObject Music;

	public bool FindOnce1000 = true;

	public bool FindOnce50 = true;

	private void Start()
	{
		Time.timeScale = 0f;
		Valve.SetActive(false);
		Radiation.SetActive(false);
		Fire.SetActive(false);
		Rods.SetActive(false);
		Valve.SetActive(false);
		Valve.SetActive(false);
		Valve.SetActive(false);
		Valve.SetActive(false);
		Valve.SetActive(false);
		UpgradeButton.GetComponent<Button>().interactable = false;
		Energy.SetActive(false);
		TimeText.SetActive(false);
		Transparent.SetActive(true);
		BuyGenerPanel.SetActive(false);
		InvokeRepeating("Find", 1f, 1f);
		NasosSlider.GetComponent<Slider>().value = 0.5f;
		NasosSliderBlock.SetActive(true);
	}

	public void Find()
	{
		if (ParametrsScript.GetComponent<ReactorParametrsSqript>().TempReactor > 1000f && FindOnce1000)
		{
			FindOnce1000 = false;
			Time.timeScale = 0f;
			Slide6.SetActive(true);
			Transparent.SetActive(true);
		}
		if (ParametrsScript.GetComponent<ReactorParametrsSqript>().TempReactor < 50f && !FindOnce1000 && FindOnce50)
		{
			FindOnce50 = false;
			Time.timeScale = 0f;
			WinPanel.SetActive(true);
			Music.SetActive(false);
			PlayerPrefs.SetInt("nextlevel", 3);
			if (PlayerPrefs.GetInt("lvlunlocked") < 3)
			{
				PlayerPrefs.SetInt("lvlunlocked", 3);
			}
		}
	}
}
