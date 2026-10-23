using GoogleMobileAds.Api;
using UnityEngine;

public class Ads : MonoBehaviour
{
	private InterstitialAd interstitial;

	private void Start()
	{
		RequestInterstitial();
	}

	public void RequestInterstitial()
	{
		string adUnitId = "ca-app-pub-4106347713178906/8742722933";
		interstitial = new InterstitialAd(adUnitId);
		AdRequest request = new AdRequest.Builder().Build();
		interstitial.LoadAd(request);
	}

	public void ShowAd()
	{
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
	}
}
