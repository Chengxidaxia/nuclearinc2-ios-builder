using UnityEngine;
using UnityEngine.SceneManagement;

public class OnButton : MonoBehaviour
{
	public void LoadScene(string leveltoload)
	{
		SceneManager.LoadScene(leveltoload);
	}
}
