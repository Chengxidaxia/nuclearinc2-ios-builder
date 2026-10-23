using UnityEngine;

public class googleplay : MonoBehaviour
{
	public void GoGooglePlay()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=ru.DmitryLomakin.NuclearInc2");
	}
}
