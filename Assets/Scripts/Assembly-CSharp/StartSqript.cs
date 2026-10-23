using UnityEngine;
using UnityEngine.UI;

public class StartSqript : MonoBehaviour
{
	public GameObject mycamera;

	public GameObject mainmenu;

	public GameObject closeme;

	public GameObject closemedaddy;

	public GameObject StartPanel;

	public GameObject LangPanel;

	public GameObject StartMusic;

	public GameObject Click;

	public GameObject WarnPanel;

	public string PrefsLang;

	public string debug = "false";

	public GameObject Progress;

	public GameObject Donate;

	private void Start()
	{
		StartPanel.SetActive(false);
		StartMusic.SetActive(true);
		if (PlayerPrefs.HasKey("PrefsLang"))
		{
			GameObject gameObject = Object.Instantiate(GameObject.Find(PlayerPrefs.GetString("PrefsLang")));
			Button component = gameObject.GetComponent<Button>();
			Click.GetComponent<AudioSource>().playOnAwake = false;
			component.onClick.Invoke();
			Click.GetComponent<AudioSource>().playOnAwake = true;
			LangPanel.SetActive(false);
		}
		if (PlayerPrefs.GetString("Replay") == "true")
		{
			PlayerPrefs.SetString("Replay", "false");
			WarnPanel.SetActive(false);
		}
	}
}
