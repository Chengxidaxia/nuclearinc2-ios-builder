using UnityEngine;

public class LoadLevel : MonoBehaviour
{
	public void LoadFunction(int levelname)
	{
		if (levelname == 0)
		{
			PlayerPrefs.SetString("Replay", "true");
			Application.LoadLevel("Main Menu");
		}
		if (levelname == 1)
		{
			Application.LoadLevel("lvl1_2");
		}
	}

	public void LoadLevelFunction(int levelname)
	{
		PlayerPrefs.SetInt("nextlevel", levelname);
		Application.LoadLevelAsync("lvl1_2");
	}
}
