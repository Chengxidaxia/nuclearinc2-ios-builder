using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class LoadText : MonoBehaviour
{
	private Text txt;

	public void FLoadText()
	{
		txt = base.gameObject.GetComponent<Text>();
		txt.text = LanguageManager.Instance.GetTextValue(txt.text);
	}
}
