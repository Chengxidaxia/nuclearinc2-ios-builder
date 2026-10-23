using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class LoadAllText : MonoBehaviour
{
	public GameObject Power;

	public GameObject RestoreGenerator;

	public GameObject FuelRecharge;

	public GameObject RestoreWorker;

	public GameObject FireFightText;

	public GameObject ReactorTemp;

	public GameObject GenerTemp;

	public GameObject ReactorPressure;

	public GameObject Radiation;

	public GameObject Menu;

	public GameObject Upgrade;

	public GameObject Time;

	public GameObject Energy;

	public GameObject KlapanDes;

	public GameObject RadiationDes;

	public GameObject FireDes;

	public GameObject RodsDes;

	public GameObject SelfDes;

	public GameObject WaterDes;

	public GameObject TurbineDes;

	public GameObject ReactorDes;

	public GameObject RefuelDes;

	public GameObject Pause;

	public GameObject ToMainMenu;

	public GameObject ToMainMenu2;

	public GameObject ToMainMenu3;

	public GameObject ToMainMenu4;

	public GameObject ContinueGame;

	public GameObject ContinueGame1;

	public GameObject Replay;

	public GameObject Replay1;

	public GameObject Replay2;

	public GameObject Replay3;

	public GameObject Music;

	public GameObject Next;

	public GameObject Next2;

	public GameObject Next3;

	public GameObject ReactorDestroyed;

	public GameObject TimeIsOver;

	public GameObject YouWon;

	public GameObject Safe;

	public GameObject Effective;

	public GameObject Others;

	public GameObject Upgrades;

	public GameObject NextLevel1;

	public GameObject NextLevel2;

	public GameObject GameCompleted;

	public GameObject Rate;

	public GameObject ToMenu;

	public GameObject Transl1_1;

	public GameObject Transl1_2;

	public GameObject Transl2_1;

	public GameObject Transl2_2;

	public GameObject Transl2_3;

	public GameObject Transl2_4;

	public GameObject Transl2_5;

	public GameObject Transl2_6;

	public GameObject Transl3_1;

	public GameObject Transl3_2;

	public GameObject Transl3_3;

	public GameObject Transl3_4;

	public GameObject Transl4_1;

	public GameObject Transl5_1;

	public GameObject Transl5_2;

	public GameObject Transl5_3;

	public GameObject Transl6_1;

	public GameObject Transl7_1;

	public GameObject Transl8_1;

	public GameObject Transl9_1;

	public GameObject Transl10_1;

	public GameObject Transl11_1;

	public GameObject Transl11_2;

	public GameObject Transl11_3;

	public GameObject Transl12_0;

	public GameObject Transl12_1;

	public GameObject Transl12_2;

	public GameObject Transl12_3;

	public GameObject FireText;

	public GameObject EarthText;

	public GameObject ShareText;

	private void Start()
	{
		if (PlayerPrefs.GetString("PrefsLang") == "English")
		{
			LanguageManager.Instance.ChangeLanguage("en");
		}
		if (PlayerPrefs.GetString("PrefsLang") == "Russian")
		{
			LanguageManager.Instance.ChangeLanguage("ru");
		}
		if (PlayerPrefs.GetString("PrefsLang") == "Germany")
		{
			LanguageManager.Instance.ChangeLanguage("de");
		}
		if (PlayerPrefs.GetString("PrefsLang") == "France")
		{
			LanguageManager.Instance.ChangeLanguage("fr");
		}
		Power.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Power");
		RestoreGenerator.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("RestoreGenerator");
		FuelRecharge.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("FuelRecharge");
		RestoreWorker.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("RestoreWorker");
		FireFightText.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("FireFightText");
		ReactorTemp.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ReactorTemp");
		GenerTemp.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("GenerTemp");
		ReactorPressure.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ReactorPressure");
		Radiation.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Radiation");
		Menu.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Menu");
		Upgrade.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Upgrade");
		Time.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Time");
		Energy.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Energy");
		NextLevel1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("NextLevel");
		NextLevel2.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("NextLevel");
		Pause.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Pause");
		ToMainMenu.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ToMainMenu");
		ToMainMenu2.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ToMainMenu");
		ToMainMenu3.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ToMainMenu");
		ToMainMenu4.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ToMainMenu");
		ContinueGame.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ContinueGame");
		ContinueGame1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ContinueGame");
		Replay.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Replay");
		Replay1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Replay");
		Replay2.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Replay");
		Replay3.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("NextLevel");
		Music.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Music");
		Next.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Next");
		Next2.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Next");
		Next3.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Next");
		ReactorDestroyed.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ReactorDestroyed");
		TimeIsOver.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("TimeIsOver");
		YouWon.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("YouWon");
		Safe.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Safe");
		Effective.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Effective");
		Others.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Others");
		Upgrades.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Upgrades");
		GameCompleted.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("GameCompleted");
		Rate.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Rate");
		ToMenu.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ToMainMenu");
		KlapanDes.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("KlapanDes");
		RadiationDes.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("RadiationDes");
		FireDes.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("FireDes");
		RodsDes.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("RodsDes");
		SelfDes.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("SelfDes");
		WaterDes.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("WaterDes");
		TurbineDes.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("TurbineDes");
		ReactorDes.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ReactorDes");
		RefuelDes.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("RefuelDes");
		Transl1_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl1_1");
		Transl1_2.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl1_2");
		Transl2_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl2_1");
		Transl2_2.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl2_2");
		Transl2_3.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl2_3");
		Transl2_4.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl2_4");
		Transl2_5.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl2_5");
		Transl2_6.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl2_6");
		Transl3_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl3_1");
		Transl3_2.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl3_2");
		Transl3_3.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl3_3");
		Transl3_4.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl3_4");
		Transl4_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl4_1");
		Transl5_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl5_1");
		Transl5_2.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl5_2");
		Transl5_3.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl5_1");
		Transl6_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl6_1");
		Transl7_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl7_1");
		Transl8_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl8_1");
		Transl9_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl9_1");
		Transl10_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl10_1");
		Transl11_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl11_1");
		Transl11_2.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl11_2");
		Transl11_3.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl11_3");
		Transl12_0.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl12_0");
		Transl12_1.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl12_1");
		Transl12_2.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl12_2");
		Transl12_3.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Transl12_3");
		FireText.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("FireText");
		EarthText.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("EarthText");
		ShareText.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ShareText");
	}

	private void Update()
	{
	}
}
