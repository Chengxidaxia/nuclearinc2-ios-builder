using SmartLocalization;
using UnityEngine;

public class MyLocalizedScripts : MonoBehaviour
{
	public string ChooseLanguage = "ChooseLanguage";

	public string Warning = "Warning";

	public string WarningText = "WarningText";

	public string VRHeadset = "VRHeadset";

	public string SomebodyCode = "SomebodyCode";

	public string EnterCode = "EnterCode";

	public string Start = "Start";

	public string Language = "Language";

	public string Exit = "Exit";

	public string gameProgress = "gameProgress";

	public string AcroPhobia = "AcroPhobia";

	public string Alice = "Alice";

	public string ArachnoPhobia = "ArachnoPhobia";

	public string BloodPhobia = "BloodPhobia";

	public string ChoosePhobia = "ChoosePhobia";

	public string ClaustroPhobia = "ClaustroPhobia";

	public string DepthPhobia = "DepthPhobia";

	public string WelcomePlane = "WelcomePlane";

	public string YouCanWalk = "YouCanWalk";

	public string PlaneFindCode = "PlaneFindCode";

	public string WatchAds = "WatchAds";

	public string Turbulence = "Turbulence";

	public string OtherSide = "OtherSide";

	public string NoEnergy = "NoEnergy";

	public string Mission = "Mission";

	public string LoveAds = "LoveAds";

	public string Level = "Level";

	public string Energy = "Energy";

	public string CantFind = "CantFind";

	public string ChooseLevel = "ChooseLevel";

	public string DontBrokeTheGlass = "DontBrokeTheGlass";

	public string Horrordepths = "Horrordepths";

	public string LikeIt = "LikeIt";

	public string Share = "Share";

	public static bool LanguageChanged;

	public void PressEngButton()
	{
		LanguageManager.Instance.ChangeLanguage("en");
		LanguageChanged = true;
		PlayerPrefs.SetString("PrefsLang", "English");
		PlayerPrefs.Save();
	}

	public void PressRusButton()
	{
		LanguageManager.Instance.ChangeLanguage("ru");
		LanguageChanged = true;
		PlayerPrefs.SetString("PrefsLang", "Russian");
		PlayerPrefs.Save();
	}

	public void PressGerButton()
	{
		LanguageManager.Instance.ChangeLanguage("de");
		LanguageChanged = true;
		PlayerPrefs.SetString("PrefsLang", "Germany");
		PlayerPrefs.Save();
	}

	public void PressFreButton()
	{
		LanguageManager.Instance.ChangeLanguage("fr");
		LanguageChanged = true;
		PlayerPrefs.SetString("PrefsLang", "France");
		PlayerPrefs.Save();
	}
}
