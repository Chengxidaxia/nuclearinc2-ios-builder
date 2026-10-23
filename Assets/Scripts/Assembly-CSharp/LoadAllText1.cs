using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class LoadAllText1 : MonoBehaviour
{
	public GameObject Warning;

	public GameObject DangerArea;

	public GameObject NuclearPlant;

	public GameObject summer2018;

	public GameObject Fukushima;

	public GameObject Chernobyl;

	public GameObject Chooselevel;

	public GameObject ChooseSeason;

	private void Start()
	{
		SetText();
	}

	public void SetText()
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
		Warning.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Warning");
		DangerArea.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("DangerArea");
		NuclearPlant.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("NuclearPlant");
		summer2018.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("summer2018");
		Fukushima.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Fukushima");
		Chernobyl.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("Chernobyl");
		Chooselevel.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ChooseLevel");
		ChooseSeason.gameObject.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("ChooseSeason");
	}
}
