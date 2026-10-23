using UnityEngine;
using UnityEngine.UI;

public class lvl1_script : MonoBehaviour
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

	public GameObject ReplayButton;

	public GameObject NextButton;

	public GameObject Transparent;

	public GameObject Ads;

	public GameObject Ads1;

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
		ReplayButton.SetActive(false);
		NextButton.SetActive(true);
		BuyGenerPanel.SetActive(false);
		Transparent.SetActive(true);
		Ads.SetActive(false);
		Ads1.SetActive(false);
	}

	public void SetWin()
	{
		PlayerPrefs.SetInt("nextlevel", 2);
		if (PlayerPrefs.GetInt("lvlunlocked") < 2)
		{
			PlayerPrefs.SetInt("lvlunlocked", 2);
		}
	}
}
