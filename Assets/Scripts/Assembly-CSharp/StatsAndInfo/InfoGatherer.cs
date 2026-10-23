using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace StatsAndInfo
{
	[Serializable]
	public class InfoGatherer : MonoBehaviour
	{
		public enum InfoType
		{
			FPS = 0,
			FPSMin = 1,
			FPSMax = 2,
			FPSAvg = 3,
			GPU = 4,
			GPUMemory = 5,
			CPU = 6,
			CPUCores = 7,
			CPUFrequency = 8,
			RAMTotal = 9,
			RAMAlloc = 10,
			OperatingSystem = 11,
			DeviceModel = 12
		}

		public bool showOnStart;

		public bool saveMinMaxAvgData;

		[Tooltip("How many times should the info get updated")]
		public int updateFrequency;

		[HideInInspector]
		public int dataAmount;

		[HideInInspector]
		public int fpsWarningLimit;

		[HideInInspector]
		public int totalFPS;

		[HideInInspector]
		public bool fpsWarning;

		[HideInInspector]
		public Color fpsWarningColor;

		[HideInInspector]
		public Color fpsInitialColor;

		[HideInInspector]
		public List<Action> actions;

		[HideInInspector]
		public Text[] texts;

		private int fps;

		private int min;

		private int max;

		private int avg;

		private bool init;

		private bool shouldUpdate;

		private float timer;

		private float period;

		private void Initialize()
		{
			init = true;
			if (saveMinMaxAvgData)
			{
				int num = 0;
				num = PlayerPrefs.GetInt("FPSMin", -9);
				if (num == -9)
				{
					min = 1000000;
					PlayerPrefs.SetInt("FPSMin", min);
				}
				else
				{
					min = num;
				}
				num = PlayerPrefs.GetInt("FPSMax", -9);
				if (num == -9)
				{
					max = 0;
					PlayerPrefs.SetInt("FPSMax", max);
				}
				else
				{
					max = num;
				}
				num = PlayerPrefs.GetInt("TotalFPS", -9);
				if (num == -9)
				{
					totalFPS = 0;
					dataAmount = 0;
					PlayerPrefs.SetInt("TotalFPS", 0);
					PlayerPrefs.SetInt("FPSDataAmount", dataAmount);
				}
				else
				{
					totalFPS = num;
					dataAmount = PlayerPrefs.GetInt("FPSDataAmount");
				}
			}
			else
			{
				min = 1000000;
				max = 0;
				avg = 0;
				dataAmount = 0;
				totalFPS = 0;
			}
			actions = new List<Action>();
			if (updateFrequency <= 0)
			{
				updateFrequency = 10;
			}
			period = 1f / (float)updateFrequency;
			timer = 0f;
			if ((bool)texts[0])
			{
				actions.Add(UpdateFPS);
				fpsInitialColor = texts[0].color;
			}
			if ((bool)texts[1])
			{
				if (!actions.Contains(UpdateFPS))
				{
					actions.Add(UpdateFPS);
				}
				actions.Add(UpdateFPSMin);
			}
			if ((bool)texts[2])
			{
				if (!actions.Contains(UpdateFPS))
				{
					actions.Add(UpdateFPS);
				}
				actions.Add(UpdateFPSMax);
			}
			if ((bool)texts[3])
			{
				if (!actions.Contains(UpdateFPS))
				{
					actions.Add(UpdateFPS);
				}
				actions.Add(UpdateFPSAvg);
			}
			if ((bool)texts[4])
			{
				actions.Add(UpdateGPU);
			}
			if ((bool)texts[5])
			{
				actions.Add(UpdateGPUMem);
			}
			if ((bool)texts[6])
			{
				UpdateCPU();
			}
			if ((bool)texts[7])
			{
				actions.Add(UpdateCPUCores);
			}
			if ((bool)texts[8])
			{
				actions.Add(UpdateCPUFreq);
			}
			if ((bool)texts[9])
			{
				UpdateRAMTotal();
			}
			if ((bool)texts[10])
			{
				actions.Add(UpdateRAMAlloc);
			}
			if ((bool)texts[11])
			{
				UpdateOS();
			}
			if ((bool)texts[12])
			{
				UpdateDeviceModel();
			}
			if (showOnStart)
			{
				shouldUpdate = true;
				Text[] array = texts;
				foreach (Text text in array)
				{
					if ((bool)text)
					{
						text.gameObject.SetActive(true);
					}
				}
				return;
			}
			shouldUpdate = false;
			Text[] array2 = texts;
			foreach (Text text2 in array2)
			{
				if ((bool)text2)
				{
					text2.gameObject.SetActive(false);
				}
			}
		}

		private void Update()
		{
			if (!init)
			{
				Initialize();
			}
			if (!shouldUpdate)
			{
				return;
			}
			if (timer >= period)
			{
				timer -= period;
				foreach (Action action in actions)
				{
					action();
				}
			}
			timer += Time.deltaTime;
		}

		public void Hide()
		{
			shouldUpdate = false;
			for (int i = 0; i < texts.Length; i++)
			{
				if ((bool)texts[i])
				{
					texts[i].gameObject.SetActive(false);
				}
			}
		}

		public void ResetFPSMin()
		{
			min = fps;
		}

		public void ResetFPSMax()
		{
			max = fps;
		}

		public void ResetFPSAvg()
		{
			avg = fps;
			totalFPS = 0;
			dataAmount = 0;
		}

		public void Show()
		{
			shouldUpdate = true;
			for (int i = 0; i < texts.Length; i++)
			{
				if ((bool)texts[i])
				{
					texts[i].gameObject.SetActive(true);
				}
			}
		}

		public void SetFPSWarning(int limit, Color color)
		{
			fpsWarningLimit = limit;
			fpsWarningColor = color;
		}

		public bool GetEnabled(InfoType type)
		{
			switch (type)
			{
			case InfoType.FPS:
				return actions.Contains(UpdateFPS) && texts[(int)type] != null;
			case InfoType.FPSMin:
				return actions.Contains(UpdateFPSMin) && texts[(int)type] != null;
			case InfoType.FPSMax:
				return actions.Contains(UpdateFPSMax) && texts[(int)type] != null;
			case InfoType.FPSAvg:
				return actions.Contains(UpdateFPSAvg) && texts[(int)type] != null;
			case InfoType.GPU:
				return actions.Contains(UpdateGPU) && texts[(int)type] != null;
			case InfoType.GPUMemory:
				return actions.Contains(UpdateGPUMem) && texts[(int)type] != null;
			case InfoType.CPU:
				return actions.Contains(UpdateCPU) && texts[(int)type] != null;
			case InfoType.CPUCores:
				return actions.Contains(UpdateCPUCores) && texts[(int)type] != null;
			case InfoType.CPUFrequency:
				return actions.Contains(UpdateCPUFreq) && texts[(int)type] != null;
			case InfoType.RAMTotal:
				return actions.Contains(UpdateRAMTotal) && texts[(int)type] != null;
			case InfoType.RAMAlloc:
				return actions.Contains(UpdateRAMAlloc) && texts[(int)type] != null;
			case InfoType.OperatingSystem:
				return actions.Contains(UpdateOS) && texts[(int)type] != null;
			default:
				return false;
			}
		}

		public void SetEnabled(InfoType type, bool enabled, Text text = null)
		{
			switch (type)
			{
			case InfoType.FPS:
				if (enabled)
				{
					EnableInfo(UpdateFPS, (int)type, text);
					fpsInitialColor = ((!(text == null)) ? text.color : texts[0].color);
				}
				else
				{
					DisableInfo(UpdateFPS);
				}
				break;
			case InfoType.FPSMin:
				if (enabled)
				{
					EnableInfo(UpdateFPSMin, (int)type, text);
				}
				else
				{
					DisableInfo(UpdateFPSMin);
				}
				break;
			case InfoType.FPSMax:
				if (enabled)
				{
					EnableInfo(UpdateFPSMax, (int)type, text);
				}
				else
				{
					DisableInfo(UpdateFPSMax);
				}
				break;
			case InfoType.FPSAvg:
				if (enabled)
				{
					EnableInfo(UpdateFPSAvg, (int)type, text);
				}
				else
				{
					DisableInfo(UpdateFPSAvg);
				}
				break;
			case InfoType.GPU:
				if (enabled)
				{
					EnableInfo(UpdateGPU, (int)type, text);
				}
				else
				{
					DisableInfo(UpdateGPU);
				}
				break;
			case InfoType.GPUMemory:
				if (enabled)
				{
					EnableInfo(UpdateGPUMem, (int)type, text);
				}
				else
				{
					DisableInfo(UpdateGPUMem);
				}
				break;
			case InfoType.CPU:
				if (enabled)
				{
					EnableInfo(UpdateCPU, (int)type, text);
				}
				else
				{
					DisableInfo(UpdateCPU);
				}
				break;
			case InfoType.CPUCores:
				if (enabled)
				{
					EnableInfo(UpdateCPUCores, (int)type, text);
				}
				else
				{
					DisableInfo(UpdateCPUCores);
				}
				break;
			case InfoType.CPUFrequency:
				if (enabled)
				{
					EnableInfo(UpdateCPUFreq, (int)type, text);
				}
				else
				{
					DisableInfo(UpdateCPUFreq);
				}
				break;
			case InfoType.RAMTotal:
				if (enabled)
				{
					EnableInfo(UpdateRAMTotal, (int)type, text);
				}
				else
				{
					DisableInfo(UpdateRAMTotal);
				}
				break;
			case InfoType.RAMAlloc:
				if (enabled)
				{
					EnableInfo(UpdateRAMAlloc, (int)type, text);
				}
				else
				{
					DisableInfo(UpdateRAMAlloc);
				}
				break;
			case InfoType.OperatingSystem:
				if (enabled)
				{
					EnableInfo(UpdateOS, (int)type, text);
				}
				else
				{
					DisableInfo(UpdateOS);
				}
				break;
			}
		}

		private void EnableInfo(Action a, int i, Text text)
		{
			if (actions.Contains(a))
			{
				return;
			}
			if (!texts[i] && !text)
			{
				Debug.LogError(string.Concat("Stats&Info  -  You are trying to enable the info ", (InfoType)i, ", but its text field is empty. Either set it in the inspector, or pass a 'Text' value with the 'SetEnabled' function."));
				return;
			}
			actions.Add(a);
			if ((bool)text)
			{
				texts[i] = text;
			}
		}

		private void DisableInfo(Action a)
		{
			if (actions.Contains(a))
			{
				actions.Remove(a);
			}
		}

		public void UpdateFPS()
		{
			fps = (int)(1f / Time.deltaTime);
			if (!(texts[0] != null))
			{
				return;
			}
			texts[0].text = fps.ToString();
			if (fpsWarning)
			{
				if (fps < fpsWarningLimit)
				{
					texts[0].color = fpsWarningColor;
				}
				else
				{
					texts[0].color = fpsInitialColor;
				}
			}
		}

		public void UpdateFPSMin()
		{
			if (fps < min && fps != 0)
			{
				min = fps;
				PlayerPrefs.SetInt("FPSMin", min);
			}
			texts[1].text = min.ToString();
		}

		public void UpdateFPSMax()
		{
			if (fps > max)
			{
				max = fps;
				PlayerPrefs.SetInt("FPSMax", max);
			}
			texts[2].text = max.ToString();
		}

		public void UpdateFPSAvg()
		{
			dataAmount++;
			totalFPS += fps;
			avg = (int)((float)totalFPS / (float)dataAmount);
			PlayerPrefs.SetInt("TotalFPS", totalFPS);
			PlayerPrefs.SetInt("FPSDataAmount", dataAmount);
			texts[3].text = avg.ToString();
		}

		public void UpdateGPU()
		{
			texts[4].text = SystemInfo.graphicsDeviceName;
		}

		public void UpdateGPUMem()
		{
			texts[5].text = SystemInfo.graphicsMemorySize + " MB";
		}

		public void UpdateCPU()
		{
			if (SystemInfo.processorType != null)
			{
				texts[6].text = SystemInfo.processorType;
			}
		}

		public void UpdateCPUCores()
		{
			texts[7].text = SystemInfo.processorCount.ToString();
		}

		public void UpdateCPUFreq()
		{
			texts[8].text = (Mathf.Ceil((float)SystemInfo.processorFrequency / 10f) / 100f).ToString("0.00") + " GHz";
		}

		public void UpdateRAMTotal()
		{
			texts[9].text = SystemInfo.systemMemorySize + " MB";
		}

		public void UpdateRAMAlloc()
		{
			texts[10].text = ((float)Profiler.GetTotalReservedMemory() / 1000000f).ToString("0") + " MB";
		}

		public void UpdateOS()
		{
			texts[11].text = SystemInfo.operatingSystem;
		}

		public void UpdateDeviceModel()
		{
			texts[12].text = SystemInfo.deviceModel;
		}
	}
}
