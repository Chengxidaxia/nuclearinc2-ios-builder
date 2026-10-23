using System;
using System.Diagnostics;
using System.Threading;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	public class NativeExpressAdClient : AndroidJavaProxy, INativeExpressAdClient
	{
		private AndroidJavaObject nativeExpressAdView;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<EventArgs> OnAdLoaded__BackingField;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad__BackingField;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<EventArgs> OnAdOpening__BackingField;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<EventArgs> OnAdClosed__BackingField;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<EventArgs> OnAdLeavingApplication__BackingField;

		public event EventHandler<EventArgs> OnAdLoaded
		{
			add
			{
				EventHandler<EventArgs> eventHandler = OnAdLoaded__BackingField;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref OnAdLoaded__BackingField, (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value), eventHandler);
				}
				while ((object)eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<EventArgs> eventHandler = OnAdLoaded__BackingField;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref OnAdLoaded__BackingField, (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value), eventHandler);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad
		{
			add
			{
				EventHandler<AdFailedToLoadEventArgs> eventHandler = OnAdFailedToLoad__BackingField;
				EventHandler<AdFailedToLoadEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref OnAdFailedToLoad__BackingField, (EventHandler<AdFailedToLoadEventArgs>)Delegate.Combine(eventHandler2, value), eventHandler);
				}
				while ((object)eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<AdFailedToLoadEventArgs> eventHandler = OnAdFailedToLoad__BackingField;
				EventHandler<AdFailedToLoadEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref OnAdFailedToLoad__BackingField, (EventHandler<AdFailedToLoadEventArgs>)Delegate.Remove(eventHandler2, value), eventHandler);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		public event EventHandler<EventArgs> OnAdOpening
		{
			add
			{
				EventHandler<EventArgs> eventHandler = OnAdOpening__BackingField;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref OnAdOpening__BackingField, (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value), eventHandler);
				}
				while ((object)eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<EventArgs> eventHandler = OnAdOpening__BackingField;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref OnAdOpening__BackingField, (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value), eventHandler);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		public event EventHandler<EventArgs> OnAdClosed
		{
			add
			{
				EventHandler<EventArgs> eventHandler = OnAdClosed__BackingField;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref OnAdClosed__BackingField, (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value), eventHandler);
				}
				while ((object)eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<EventArgs> eventHandler = OnAdClosed__BackingField;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref OnAdClosed__BackingField, (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value), eventHandler);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		public event EventHandler<EventArgs> OnAdLeavingApplication
		{
			add
			{
				EventHandler<EventArgs> eventHandler = OnAdLeavingApplication__BackingField;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref OnAdLeavingApplication__BackingField, (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value), eventHandler);
				}
				while ((object)eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<EventArgs> eventHandler = OnAdLeavingApplication__BackingField;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref OnAdLeavingApplication__BackingField, (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value), eventHandler);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		public NativeExpressAdClient()
			: base("com.google.unity.ads.UnityAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			nativeExpressAdView = new AndroidJavaObject("com.google.unity.ads.NativeExpressAd", androidJavaObject, this);
		}

		public void CreateNativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position)
		{
			nativeExpressAdView.Call("create", adUnitId, Utils.GetAdSizeJavaObject(adSize), (int)position);
		}

		public void CreateNativeExpressAdView(string adUnitId, AdSize adSize, int x, int y)
		{
			nativeExpressAdView.Call("create", adUnitId, Utils.GetAdSizeJavaObject(adSize), x, y);
		}

		public void LoadAd(AdRequest request)
		{
			nativeExpressAdView.Call("loadAd", Utils.GetAdRequestJavaObject(request));
		}

		public void SetAdSize(AdSize adSize)
		{
			nativeExpressAdView.Call("setAdSize", Utils.GetAdSizeJavaObject(adSize));
		}

		public void ShowNativeExpressAdView()
		{
			nativeExpressAdView.Call("show");
		}

		public void HideNativeExpressAdView()
		{
			nativeExpressAdView.Call("hide");
		}

		public void DestroyNativeExpressAdView()
		{
			nativeExpressAdView.Call("destroy");
		}

		public string MediationAdapterClassName()
		{
			return nativeExpressAdView.Call<string>("getMediationAdapterClassName", Array.Empty<object>());
		}

		public void onAdLoaded()
		{
			if (OnAdLoaded__BackingField != null)
			{
				OnAdLoaded__BackingField(this, EventArgs.Empty);
			}
		}

		public void onAdFailedToLoad(string errorReason)
		{
			if (OnAdFailedToLoad__BackingField != null)
			{
				AdFailedToLoadEventArgs e = new AdFailedToLoadEventArgs();
				e.Message = errorReason;
				AdFailedToLoadEventArgs e2 = e;
				OnAdFailedToLoad__BackingField(this, e2);
			}
		}

		public void onAdOpened()
		{
			if (OnAdOpening__BackingField != null)
			{
				OnAdOpening__BackingField(this, EventArgs.Empty);
			}
		}

		public void onAdClosed()
		{
			if (OnAdClosed__BackingField != null)
			{
				OnAdClosed__BackingField(this, EventArgs.Empty);
			}
		}

		public void onAdLeftApplication()
		{
			if (OnAdLeavingApplication__BackingField != null)
			{
				OnAdLeavingApplication__BackingField(this, EventArgs.Empty);
			}
		}
	}
}
