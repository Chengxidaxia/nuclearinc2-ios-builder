using UnityEngine;
using UnityEngine.UI;

public class lvl1_script4 : MonoBehaviour
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

	public GameObject ParametrsScript;

	public GameObject Music;

	public GameObject Transparent;

	public GameObject WinPanel;

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
		InvokeRepeating("Find", 1f, 1f);
	}

	public void Find()
	{
		if (ParametrsScript.GetComponent<ReactorParametrsSqript>().Money > 700f && FindOnce1000)
		{
			FindOnce1000 = false;
			Time.timeScale = 0f;
			WinPanel.SetActive(true);
			Music.SetActive(false);
			PlayerPrefs.SetInt("nextlevel", 5);
			if (PlayerPrefs.GetInt("lvlunlocked") < 5)
			{
				PlayerPrefs.SetInt("lvlunlocked", 5);
			}
		}
	}
}
