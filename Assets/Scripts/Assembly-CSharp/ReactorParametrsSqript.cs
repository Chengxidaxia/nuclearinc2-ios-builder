using UnityEngine;
using UnityEngine.UI;

public class ReactorParametrsSqript : MonoBehaviour
{
	public string currentlevel;

	public float TempFuel = 1f;

	public float TempReactor = 1f;

	public float TempGenerat = 1f;

	public float Pressure;

	public float Radiation;

	public float Power;

	public float Money;

	public float KGenerator = 0.03f;

	public float KPump;

	public float KSterj;

	public float KCoolgenerat = 0.9f;

	public float MaxReactorTemp = 1500f;

	public float MaxGeneratorTemp = 400f;

	public float MaxPressure = 100f;

	public float FuelAmount = 100f;

	public float HPWorker = 100f;

	public float EarthTimer = 100f;

	public float End;

	public float FireTimer;

	public float AvarWater = 100f;

	public bool WorkerLogic = true;

	public bool GeneratLogic = true;

	public bool Radiationlogic;

	public bool SelfLogic;

	public bool RodsLogic;

	public bool Earthquakelogic;

	public bool Firelogic;

	public bool FireFightlogic;

	public bool RodsSafelogic;

	public bool ReactorNewlogic;

	public bool EnableFire;

	public bool EnableEarth;

	public float Fusushima;

	public float Chern;

	public GameObject StrelkaTempReactor;

	public GameObject StrelkaTempGenerat;

	public GameObject StrelkaPressure;

	public GameObject StrelkaRadiation;

	public GameObject SterjniControl;

	public GameObject PumpControl;

	public GameObject ReactorTemp;

	public GameObject Sterjni;

	public GameObject Nasos;

	public GameObject ShkalaTopliva;

	public GameObject WorkerHP;

	public GameObject WorkerHPValue;

	public GameObject Worker;

	public GameObject ButtonWorker;

	public Material GeneratMaterial;

	public GameObject GeneratorButton;

	public GameObject GeneratorExplosion;

	public GameObject ReFuelObj;

	public GameObject ReFuelObj2;

	public GameObject ReFuelGraph;

	public GameObject RadiationGraph;

	public GameObject FireGraph;

	public GameObject RodsGraph;

	public GameObject SelfGraph;

	public GameObject WaterGraph;

	public GameObject TurbineGraph;

	public GameObject ReactorGraph;

	public GameObject RadiationObj;

	public GameObject RadiationMove;

	public GameObject SelfObj;

	public GameObject EarthTimerText;

	public GameObject EarthAlert;

	public GameObject EarthAnimaton;

	public GameObject EarthTablo;

	public GameObject EarthAlertText;

	public GameObject FireAlert;

	public GameObject FireObj;

	public GameObject FireFightButton;

	public GameObject FireFightObj;

	public GameObject FireFightWater;

	public GameObject WaterSlider;

	public GameObject WaterPanel;

	public GameObject WaterObj;

	public GameObject WaterGraphObj;

	public GameObject ReactorTextMaxTemp;

	public GameObject ReactorTextMaxPressure;

	public GameObject TurbineTextMaxPressure;

	public GameObject MusicLow;

	public GameObject MusicHigh;

	public Material ReactorMat;

	public GameObject MoneyText;

	public GameObject PowerText;

	public GameObject GeneratorTempText;

	public GameObject PressureText;

	public GameObject RadiationText;

	public GameObject VoiceOnceSterjni;

	public GameObject ParKlapan;

	public GameObject SignalReactor;

	private bool SignalReactorActive;

	public GameObject SignalGenerator;

	private bool SignalGeneratorActive;

	public GameObject SignalPressure;

	private bool SignalPressureActive;

	public GameObject SignalRadiation;

	private bool SignalRadiationActive;

	public GameObject VoiceHighReactor1Temp;

	public GameObject VoiceHighGeneratorTemp;

	public GameObject VoiceHighPressureTemp;

	public GameObject VoiceHighRadiationTemp;

	public GameObject VoiceHighReactor2Temp;

	public GameObject StateOn;

	public GameObject StateOff;

	public GameObject Siren;

	public GameObject BadEnd;

	public GameObject MiddleEnd;

	public GameObject GoodEnd;

	public GameObject VoiceBadPressure;

	public GameObject VoiceBadOverHeated;

	public GameObject SteamAway;

	public GameObject ExplosionObject;

	public GameObject Yes;

	public GameObject Klapan;

	public GameObject lvl1;

	public GameObject Scriptlvl1;

	public GameObject Scriptlvl2;

	public GameObject Scriptlvl3;

	public GameObject Scriptlvl4;

	public GameObject Scriptlvl5;

	public GameObject Scriptlvl6;

	public GameObject Scriptlvl7;

	public GameObject Scriptlvl8;

	public GameObject Scriptlvl9;

	public GameObject Scriptlvl10;

	public GameObject Scriptlvl11;

	public GameObject Scriptlvl12;

	public float FireStart;

	public float KTurbine = 1f;

	private float Kcolor;

	private int ReactorDeath;

	private bool ReactorAlreadyDeath;

	public string UpgradeID;

	public bool KlapanLogic;

	public GameObject KlapanImage;

	public float EarthMax;

	private void Start()
	{
		if (PlayerPrefs.GetInt("nextlevel") == 10)
		{
			FireStart = 160f;
		}
		else
		{
			FireStart = 40f;
		}
		ReactorDeath = 0;
		ReactorAlreadyDeath = false;
		Time.timeScale = 1f;
		GeneratMaterial.color = new Color(1f, 121f / 255f, 0f, 1f);
		InvokeRepeating("TextShow", 0f, 0.5f);
		InvokeRepeating("MoneyShow", 0f, 1f);
		InvokeRepeating("ParametrsShow", 0f, 0.1f);
		InvokeRepeating("Alert", 0f, 1f);
		InvokeRepeating("Signals", 0f, 1f);
		InvokeRepeating("MusicChange", 0f, 0.5f);
		InvokeRepeating("VoiceAlert", 0f, 8f);
		InvokeRepeating("EndSearcher", 0f, 0.5f);
		InvokeRepeating("Explosion", 0f, 0.1f);
		InvokeRepeating("EarthquakeChance", 30f, 15f);
		InvokeRepeating("FireChance", FireStart, 20f);
		InvokeRepeating("Timer", 1f, 1f);
		if (PlayerPrefs.GetInt("nextlevel") == 1)
		{
			Scriptlvl1.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") == 2)
		{
			Scriptlvl2.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") == 3)
		{
			Scriptlvl3.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") == 4)
		{
			Scriptlvl4.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") == 5)
		{
			Scriptlvl5.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") == 6)
		{
			Scriptlvl6.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") == 7)
		{
			Scriptlvl7.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") == 8)
		{
			Scriptlvl8.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") == 9)
		{
			Scriptlvl9.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") == 10)
		{
			Scriptlvl10.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") == 11)
		{
			Scriptlvl11.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") == 12)
		{
			Scriptlvl12.SetActive(true);
		}
		if (PlayerPrefs.GetInt("nextlevel") != 13)
		{
		}
	}

	private void VoiceAlert()
	{
		if (TempReactor > 0.7f * MaxReactorTemp)
		{
			if (Random.value > 0.5f)
			{
				VoiceHighReactor1Temp.SetActive(false);
				VoiceHighReactor1Temp.SetActive(true);
			}
			else
			{
				VoiceHighReactor2Temp.SetActive(false);
				VoiceHighReactor2Temp.SetActive(true);
			}
		}
		else if (Pressure > 0.7f * MaxPressure && !KlapanLogic)
		{
			VoiceHighPressureTemp.SetActive(false);
			VoiceHighPressureTemp.SetActive(true);
		}
		else if (TempGenerat > 0.8f * MaxGeneratorTemp)
		{
			VoiceHighGeneratorTemp.SetActive(false);
			VoiceHighGeneratorTemp.SetActive(true);
		}
		else if (Radiation > 7f)
		{
			VoiceHighRadiationTemp.SetActive(false);
			VoiceHighRadiationTemp.SetActive(true);
		}
	}

	private void Signals()
	{
		if (TempReactor > 0.7f * MaxReactorTemp)
		{
			if (!SignalReactorActive)
			{
				SignalReactor.SetActive(true);
				SignalReactorActive = true;
			}
			else
			{
				SignalReactor.SetActive(false);
				SignalReactorActive = false;
			}
		}
		else
		{
			SignalReactor.SetActive(false);
			SignalReactorActive = false;
		}
		if (TempGenerat > 0.8f * MaxGeneratorTemp)
		{
			if (!SignalGeneratorActive)
			{
				SignalGenerator.SetActive(true);
				SignalGeneratorActive = true;
			}
			else
			{
				SignalGenerator.SetActive(false);
				SignalGeneratorActive = false;
			}
		}
		else
		{
			SignalGenerator.SetActive(false);
			SignalGeneratorActive = false;
		}
		if (Pressure > 0.7f * MaxPressure)
		{
			if (!SignalPressureActive)
			{
				SignalPressure.SetActive(true);
				SignalPressureActive = true;
			}
			else
			{
				SignalPressure.SetActive(false);
				SignalPressureActive = false;
			}
		}
		else
		{
			SignalPressure.SetActive(false);
			SignalPressureActive = false;
		}
		if (Radiation > 7f)
		{
			if (!SignalRadiationActive)
			{
				SignalRadiation.SetActive(true);
				SignalRadiationActive = true;
			}
			else
			{
				SignalRadiation.SetActive(false);
				SignalRadiationActive = false;
			}
		}
		else
		{
			SignalRadiation.SetActive(false);
			SignalRadiationActive = false;
		}
	}

	private void TextShow()
	{
		ReactorTemp.GetComponent<Text>().text = ((int)TempReactor/*cast due to constrained. prefix*/).ToString();
		PowerText.GetComponent<Text>().text = ((int)Power/*cast due to constrained. prefix*/).ToString();
		MoneyText.GetComponent<Text>().text = ((int)Money/*cast due to constrained. prefix*/).ToString();
		GeneratorTempText.GetComponent<Text>().text = ((int)TempGenerat/*cast due to constrained. prefix*/).ToString();
		PressureText.GetComponent<Text>().text = ((int)Pressure/*cast due to constrained. prefix*/).ToString();
		RadiationText.GetComponent<Text>().text = ((int)Radiation/*cast due to constrained. prefix*/).ToString();
	}

	private void MoneyShow()
	{
		Money += (int)(Power / 2f);
	}

	private void ParametrsShow()
	{
		if (TempFuel < 0f)
		{
			TempFuel = 0f;
		}
		if (TempGenerat < 1f)
		{
			TempGenerat = 1f;
		}
		if (Radiation < 0f)
		{
			Radiation = 0f;
		}
		if (Radiation > 10f)
		{
			Radiation = 10f;
		}
		KSterj = SterjniControl.GetComponent<Slider>().normalizedValue;
		if (Earthquakelogic && RodsSafelogic && !SelfLogic)
		{
			SterjniControl.GetComponent<Slider>().normalizedValue = SterjniControl.GetComponent<Slider>().normalizedValue - 0.01f;
		}
		KPump = PumpControl.GetComponent<Slider>().normalizedValue;
		if (Earthquakelogic && !SelfLogic)
		{
			KPump = 0f;
		}
		if (Fusushima == 1f)
		{
			KPump = 0f;
		}
		if (WaterSlider.GetComponent<Slider>().normalizedValue == 1f && AvarWater > 5f)
		{
			TempFuel -= 30f;
			AvarWater -= 1.1f;
		}
		if (AvarWater < 100f)
		{
			AvarWater += 0.2f;
		}
		if (GeneratLogic)
		{
			TempFuel = TempFuel + KSterj * 13f * (FuelAmount / 100f) + 0.45f * (FuelAmount / 100f) - KPump * 10f * (TempReactor - TempGenerat + 1f) / (TempReactor + 1f) * (1f - Fusushima);
		}
		else
		{
			TempFuel = TempFuel + KSterj * 13f * (FuelAmount / 100f) + 0.45f * (FuelAmount / 100f);
		}
		TempReactor += 0.01f * (TempFuel - TempReactor);
		if (!GeneratLogic)
		{
			TempGenerat = 0f;
		}
		else
		{
			TempGenerat = TempGenerat - KCoolgenerat + KPump * 3f * (TempReactor - TempGenerat + 1f) / (TempReactor + 1f);
		}
		Pressure = TempReactor / MaxReactorTemp * 4f / 3f * MaxPressure;
		Power = TempGenerat * KGenerator * KTurbine;
		if (PlayerPrefs.GetInt("nextlevel") > 4)
		{
			FuelAmount -= 0.037f;
		}
		Radiation -= 0.01f;
		if (FuelAmount < 0f)
		{
			FuelAmount = 0f;
		}
		if (Radiation > 0f && !Radiationlogic)
		{
			HPWorker -= Radiation / 50f;
		}
		WorkerHPValue.GetComponent<RectTransform>().localScale = new Vector3(HPWorker / 100f, 1f, 1f);
		if (TempReactor > MaxReactorTemp * 0.4f)
		{
			Kcolor = TempReactor / MaxReactorTemp - 0.4f;
		}
		else
		{
			Kcolor = 0f;
		}
		if (!ReactorNewlogic)
		{
			ReactorMat.color = new Color(1f, 1f - Kcolor, 1f - Kcolor);
		}
		else
		{
			ReactorMat.color = new Color(0.75f, 1f - Kcolor, 1f - Kcolor);
		}
		WaterPanel.GetComponent<RectTransform>().localScale = new Vector3(AvarWater / 100f, 1f, 1f);
		ShkalaTopliva.GetComponent<RectTransform>().localScale = new Vector3(FuelAmount / 100f, 1f, 1f);
		if (KlapanLogic && Pressure > 0.95f * MaxPressure)
		{
			Pressure = 0.95f * MaxPressure;
			Radiation += 0.014f;
			ParKlapan.SetActive(true);
		}
		if (KlapanLogic && Pressure < 0.95f * MaxPressure)
		{
			ParKlapan.SetActive(false);
		}
		if (HPWorker < 0f)
		{
			KillWorker();
		}
		if (!WorkerLogic || (Earthquakelogic && !SelfLogic) || Fusushima == 1f || Chern == 1f)
		{
			SterjniControl.GetComponent<Slider>().interactable = false;
			PumpControl.GetComponent<Slider>().interactable = false;
		}
		else
		{
			SterjniControl.GetComponent<Slider>().interactable = true;
			PumpControl.GetComponent<Slider>().interactable = true;
		}
		if (TempGenerat > MaxGeneratorTemp + 1f)
		{
			KillGenerat();
		}
	}

	private void Update()
	{
		Vector3 position = new Vector3(Sterjni.transform.position.x, Sterjni.transform.position.y + 0.04f * (KSterj * 1.32f - Sterjni.transform.position.y), Sterjni.transform.position.x);
		Sterjni.transform.position = position;
		Nasos.transform.Rotate(0f, 0f, 10f * KPump);
		Nasos.GetComponent<AudioSource>().pitch = 0.4f + KPump / 2f;
		Nasos.GetComponent<AudioSource>().volume = KPump * 0.5f;
		StrelkaTempReactor.transform.localEulerAngles = new Vector3(StrelkaTempReactor.transform.localRotation.x, StrelkaTempReactor.transform.localRotation.y, 114f - TempReactor / MaxReactorTemp * 224f);
		StrelkaTempGenerat.transform.localEulerAngles = new Vector3(StrelkaTempGenerat.transform.localRotation.x, StrelkaTempGenerat.transform.localRotation.y, 114f - TempGenerat / MaxGeneratorTemp * 224f);
		StrelkaPressure.transform.localEulerAngles = new Vector3(StrelkaPressure.transform.localRotation.x, StrelkaPressure.transform.localRotation.y, 114f - Pressure / MaxPressure * 224f);
		StrelkaRadiation.transform.localEulerAngles = new Vector3(StrelkaRadiation.transform.localRotation.x, StrelkaRadiation.transform.localRotation.y, 114f - Radiation / 10f * 224f);
	}

	private void Alert()
	{
		if (TempReactor > 0.85f * MaxReactorTemp || (Pressure > 0.85f * MaxPressure && !KlapanLogic) || Firelogic)
		{
			StateOn.SetActive(true);
			StateOff.SetActive(false);
			Siren.SetActive(true);
		}
		if (TempReactor < 0.8f * MaxReactorTemp && (Pressure < 0.8f * MaxPressure || KlapanLogic) && !Firelogic)
		{
			StateOn.SetActive(false);
			StateOff.SetActive(true);
			Siren.SetActive(false);
		}
	}

	private void MusicChange()
	{
		if (TempReactor > 0.65f * MaxReactorTemp)
		{
			MusicHigh.SetActive(true);
			MusicLow.SetActive(false);
		}
		if (TempReactor < 0.45f * MaxReactorTemp)
		{
			MusicHigh.SetActive(false);
			MusicLow.SetActive(true);
		}
	}

	public void StartSterjni()
	{
		VoiceOnceSterjni.SetActive(true);
	}

	public void SetPause()
	{
		Time.timeScale = 0f;
	}

	public void SetUnPause()
	{
		Time.timeScale = 1f;
	}

	public void EndSearcher()
	{
		if (TempReactor > MaxReactorTemp + 7f || Pressure > MaxPressure + 3f)
		{
			ReactorAlreadyDeath = true;
			SteamAway.SetActive(true);
			if (PlayerPrefs.GetInt("nextlevel") == 1)
			{
				lvl1.GetComponent<lvl1_script>().SetWin();
			}
		}
		if (ReactorAlreadyDeath)
		{
			ReactorDeath++;
		}
		if (ReactorDeath > 4)
		{
			ExplosionObject.SetActive(true);
		}
		if (ReactorDeath > 6)
		{
			Siren.SetActive(false);
			BadEnd.SetActive(true);
			SteamAway.SetActive(false);
			Time.timeScale = 0f;
			if (TempReactor > MaxReactorTemp + 15f)
			{
				VoiceBadOverHeated.SetActive(true);
			}
			else
			{
				VoiceBadPressure.SetActive(true);
			}
		}
	}

	public void Explosion()
	{
		if (ReactorDeath > 4)
		{
			ExplosionObject.GetComponent<Light>().range++;
			ExplosionObject.GetComponent<Light>().intensity++;
		}
	}

	public void SetUpgradeID(string ID)
	{
		UpgradeID = ID;
		if (UpgradeID == "Klapan" && Money < 300f)
		{
			Yes.SetActive(false);
		}
		if (UpgradeID == "Radiation" && Money < 450f)
		{
			Yes.SetActive(false);
		}
		if (UpgradeID == "Fire" && Money < 400f)
		{
			Yes.SetActive(false);
		}
		if (UpgradeID == "Rods" && Money < 250f)
		{
			Yes.SetActive(false);
		}
		if (UpgradeID == "Self" && Money < 650f)
		{
			Yes.SetActive(false);
		}
		if (UpgradeID == "Water" && Money < 400f)
		{
			Yes.SetActive(false);
		}
		if (UpgradeID == "Turbine" && Money < 1500f)
		{
			Yes.SetActive(false);
		}
		if (UpgradeID == "Reactor" && Money < 700f)
		{
			Yes.SetActive(false);
		}
		if (UpgradeID == "ReFuel" && Money < 150f)
		{
			Yes.SetActive(false);
		}
	}

	public void Purchase()
	{
		if (UpgradeID == "Klapan")
		{
			Money -= 300f;
			KlapanLogic = true;
			Klapan.SetActive(true);
			KlapanImage.SetActive(false);
		}
		if (UpgradeID == "Radiation")
		{
			Money -= 450f;
			RadiationObj.SetActive(true);
			Radiationlogic = true;
			RadiationMove.transform.localPosition = new Vector3(-150f, 0f, 0f);
			WorkerHP.SetActive(false);
			RadiationGraph.SetActive(false);
		}
		if (UpgradeID == "Fire")
		{
			Money -= 400f;
			FireGraph.SetActive(false);
			FireFightObj.SetActive(true);
			FireFightButton.SetActive(true);
		}
		if (UpgradeID == "Rods")
		{
			Money -= 250f;
			RodsLogic = true;
			RodsGraph.SetActive(false);
			RodsSafelogic = true;
		}
		if (UpgradeID == "Self")
		{
			Money -= 650f;
			SelfObj.SetActive(true);
			SelfLogic = true;
			SelfGraph.SetActive(false);
		}
		if (UpgradeID == "Water")
		{
			Money -= 400f;
			WaterGraph.SetActive(false);
			WaterObj.SetActive(true);
			WaterGraphObj.SetActive(true);
		}
		if (UpgradeID == "Turbine")
		{
			Money -= 1500f;
			TurbineGraph.SetActive(false);
			KTurbine = 2.5f;
			GeneratMaterial.color = new Color(0f, 121f / 255f, 1f);
			MaxGeneratorTemp = 700f;
			TurbineTextMaxPressure.GetComponent<Text>().text = "700";
		}
		if (UpgradeID == "Reactor")
		{
			Money -= 700f;
			ReactorGraph.SetActive(false);
			ReactorNewlogic = true;
			MaxReactorTemp = 2500f;
			MaxPressure = 150f;
			ReactorTextMaxTemp.GetComponent<Text>().text = "2500";
			ReactorTextMaxPressure.GetComponent<Text>().text = "150";
		}
		if (UpgradeID == "ReFuel")
		{
			Money -= 150f;
			ReFuelObj.SetActive(true);
			ReFuelObj2.SetActive(true);
			ReFuelGraph.SetActive(false);
		}
		MoneyText.GetComponent<Text>().text = ((int)Money/*cast due to constrained. prefix*/).ToString();
	}

	private void KillWorker()
	{
		WorkerLogic = false;
		WorkerHP.SetActive(false);
		ButtonWorker.SetActive(true);
		Worker.transform.localEulerAngles = new Vector3(0f, 0f, 87.1f);
		Worker.transform.localPosition = new Vector3(-38.62f, 144.2f, 22f);
	}

	public void RestoreWorker()
	{
		if (Money > 300f)
		{
			WorkerLogic = true;
			WorkerHP.SetActive(true);
			Money -= 300f;
			HPWorker = 100f;
			Worker.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			Worker.transform.localPosition = new Vector3(-66f, 128.8f, 22f);
			ButtonWorker.SetActive(false);
		}
	}

	private void KillGenerat()
	{
		GeneratMaterial.color = new Color(0.01f, 0.01f, 0.01f);
		GeneratLogic = false;
		GeneratorButton.SetActive(true);
		GeneratorExplosion.SetActive(false);
		GeneratorExplosion.SetActive(true);
	}

	public void RestoreGener()
	{
		if (Money > 250f)
		{
			GeneratLogic = true;
			Money -= 250f;
			if (KTurbine == 1f)
			{
				GeneratMaterial.color = new Color(1f, 121f / 255f, 0f);
			}
			else
			{
				GeneratMaterial.color = new Color(0f, 121f / 255f, 1f);
			}
			GeneratorButton.SetActive(false);
		}
	}

	private void EarthquakeChance()
	{
		if (Random.value < 0.17f && !Earthquakelogic && EnableEarth)
		{
			End = 6f + Random.value * 22f;
			EarthMax = End;
			Earthquakelogic = true;
			EarthTimer = 0f;
			EarthAnimaton.GetComponent<Animator>().SetTrigger("Earthquake");
		}
	}

	private void FireChance()
	{
		if (Random.value < 0.25f && !Firelogic && EnableFire)
		{
			Firelogic = true;
			FireAlert.SetActive(true);
			FireObj.SetActive(true);
			SetPause();
		}
	}

	public void FireFight()
	{
		FireTimer = 0f;
		FireFightlogic = true;
		FireFightWater.SetActive(true);
	}

	private void Timer()
	{
		End--;
		EarthTimerText.GetComponent<Text>().text = ((int)End/*cast due to constrained. prefix*/).ToString();
		if (End == EarthMax - 3f && Earthquakelogic)
		{
			EarthAlertText.GetComponent<Text>().text = ((int)End/*cast due to constrained. prefix*/).ToString();
			EarthTablo.SetActive(true);
			SetPause();
			EarthAlert.SetActive(true);
		}
		if (End < 1f && Earthquakelogic)
		{
			Earthquakelogic = false;
			EarthTablo.SetActive(false);
			EarthAlert.SetActive(false);
		}
		if (Firelogic)
		{
			TempFuel += 42f;
			if (!Radiationlogic)
			{
				HPWorker -= 3f;
			}
		}
		if (FireFightlogic)
		{
			FireTimer++;
		}
		if (FireFightlogic && FireTimer > 5f)
		{
			Firelogic = false;
			FireObj.SetActive(false);
		}
		if (FireFightlogic && FireTimer > 7f)
		{
			FireFightWater.SetActive(false);
			FireFightlogic = false;
		}
	}
}
