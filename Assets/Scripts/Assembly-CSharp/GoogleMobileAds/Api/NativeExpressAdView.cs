using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	public class NativeExpressAdView
	{
		private INativeExpressAdClient client;

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

		public NativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position)
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildNativeExpressAdClient", BindingFlags.Static | BindingFlags.Public);
			client = (INativeExpressAdClient)method.Invoke(null, null);
			client.CreateNativeExpressAdView(adUnitId, adSize, position);
			ConfigureNativeExpressAdEvents();
		}

		public NativeExpressAdView(string adUnitId, AdSize adSize, int x, int y)
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildNativeExpressAdClient", BindingFlags.Static | BindingFlags.Public);
			client = (INativeExpressAdClient)method.Invoke(null, null);
			client.CreateNativeExpressAdView(adUnitId, adSize, x, y);
			ConfigureNativeExpressAdEvents();
		}

		public void LoadAd(AdRequest request)
		{
			client.LoadAd(request);
		}

		public void Hide()
		{
			client.HideNativeExpressAdView();
		}

		public void Show()
		{
			client.ShowNativeExpressAdView();
		}

		public void Destroy()
		{
			client.DestroyNativeExpressAdView();
		}

		private void ConfigureNativeExpressAdEvents()
		{
			client.OnAdLoaded += (object sender, EventArgs args) =>
			{
				if (OnAdLoaded__BackingField != null)
				{
					OnAdLoaded__BackingField(this, args);
				}
			};
			client.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs args) =>
			{
				if (OnAdFailedToLoad__BackingField != null)
				{
					OnAdFailedToLoad__BackingField(this, args);
				}
			};
			client.OnAdOpening += (object sender, EventArgs args) =>
			{
				if (OnAdOpening__BackingField != null)
				{
					OnAdOpening__BackingField(this, args);
				}
			};
			client.OnAdClosed += (object sender, EventArgs args) =>
			{
				if (OnAdClosed__BackingField != null)
				{
					OnAdClosed__BackingField(this, args);
				}
			};
			client.OnAdLeavingApplication += (object sender, EventArgs args) =>
			{
				if (OnAdLeavingApplication__BackingField != null)
				{
					OnAdLeavingApplication__BackingField(this, args);
				}
			};
		}

		public string MediationAdapterClassName()
		{
			return client.MediationAdapterClassName();
		}
	}
}
