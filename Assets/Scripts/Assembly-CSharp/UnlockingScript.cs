using UnityEngine;

public class UnlockingScript : MonoBehaviour
{
	public GameObject ImputField;

	public int lvlunlocked;

	public GameObject Lock2;

	public GameObject Lock3;

	public GameObject Lock4;

	public GameObject Lock5;

	public GameObject Lock6;

	public GameObject Lock7;

	public GameObject Lock8;

	public GameObject Lock9;

	public GameObject Lock10;

	public GameObject Lock11;

	public GameObject Lock12;

	public GameObject Lock13;

	private void Start()
	{
		SetUnlocked();
	}

	public void SetUnlocked()
	{
		lvlunlocked = PlayerPrefs.GetInt("lvlunlocked");
		if (lvlunlocked > 1)
		{
			Lock2.SetActive(false);
		}
		if (lvlunlocked > 2)
		{
			Lock3.SetActive(false);
		}
		if (lvlunlocked > 3)
		{
			Lock4.SetActive(false);
		}
		if (lvlunlocked > 4)
		{
			Lock5.SetActive(false);
		}
		if (lvlunlocked > 5)
		{
			Lock6.SetActive(false);
		}
		if (lvlunlocked > 6)
		{
			Lock7.SetActive(false);
		}
		if (lvlunlocked > 7)
		{
			Lock8.SetActive(false);
		}
		if (lvlunlocked > 8)
		{
			Lock9.SetActive(false);
		}
		if (lvlunlocked > 9)
		{
			Lock10.SetActive(false);
		}
		if (lvlunlocked > 10)
		{
			Lock11.SetActive(false);
		}
		if (lvlunlocked > 11)
		{
			Lock12.SetActive(false);
		}
		if (lvlunlocked > 12)
		{
			Lock13.SetActive(false);
		}
	}

	public void GodMode()
	{
		PlayerPrefs.SetInt("lvlunlocked", 12);
		SetUnlocked();
	}

	public void DeathMode()
	{
		Lock2.SetActive(true);
		Lock3.SetActive(true);
		Lock4.SetActive(true);
		Lock5.SetActive(true);
		Lock6.SetActive(true);
		Lock7.SetActive(true);
		Lock8.SetActive(true);
		Lock9.SetActive(true);
		Lock10.SetActive(true);
		Lock11.SetActive(true);
		Lock12.SetActive(true);
		Lock13.SetActive(true);
		PlayerPrefs.SetInt("lvlunlocked", 0);
		SetUnlocked();
	}
}
