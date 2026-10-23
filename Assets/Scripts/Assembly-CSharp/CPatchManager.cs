using System.Collections.Generic;

public class CPatchManager
{
	private List<int[]> m_patches = new List<int[]>();

	public List<int[]> Patches
	{
		get
		{
			return m_patches;
		}
	}

	public CPatchManager(CPatchConfig config, CPlanet planet)
	{
		for (byte b = 0; b < 16; b++)
		{
			GenGrid(b, config, planet);
		}
	}

	private void GenGrid(byte edges, CPatchConfig config, CPlanet planet)
	{
		ushort num = (ushort)((config.PatchSize - 1) * 2 - 2);
		ushort num2 = (ushort)(num * 3);
		ushort num3 = (ushort)(num / 2 + num / 4);
		ushort num4 = (ushort)(num3 * 3);
		ushort num5 = (ushort)((config.PatchSize - 3) * (config.PatchSize - 3) * 6);
		ushort num6 = num5;
		for (int num7 = 1; num7 < 16; num7 <<= 1)
		{
			num6 = (((edges & num7) <= 0) ? ((ushort)(num6 + num2)) : ((ushort)(num6 + num4)));
		}
		int[] array = new int[num6];
		ushort num8 = 0;
		for (ushort num9 = 1; num9 < config.PatchSize - 2; num9++)
		{
			ushort num10 = (ushort)(num9 * config.PatchSize);
			for (ushort num11 = 1; num11 < config.PatchSize - 2; num11++)
			{
				ushort num12 = (ushort)(num10 + num11);
				array[num8++] = num12 + config.PatchSize;
				array[num8++] = num12 + 1;
				array[num8++] = num12;
				array[num8++] = num12 + 1 + config.PatchSize;
				array[num8++] = num12 + 1;
				array[num8++] = num12 + config.PatchSize;
			}
		}
		if ((edges & 1) > 0)
		{
			bool flag = false;
			ushort num13 = 0;
			ushort num14 = 0;
			while (num13 < config.PatchSize - 2)
			{
				if (!flag)
				{
					array[num8++] = num14;
					array[num8++] = num14 + config.PatchSize + 1;
					array[num8++] = num14 + 2;
					num13 += 2;
					num14 += 2;
				}
				else
				{
					array[num8++] = num14;
					array[num8++] = num14 + config.PatchSize - 1;
					array[num8++] = num14 + config.PatchSize;
					array[num8++] = num14;
					array[num8++] = num14 + config.PatchSize;
					array[num8++] = num14 + config.PatchSize + 1;
				}
				flag = !flag;
			}
		}
		else
		{
			bool flag2 = false;
			ushort num15 = 0;
			ushort num16 = 0;
			while (num15 < config.PatchSize - 2)
			{
				if (!flag2)
				{
					array[num8++] = num16;
					array[num8++] = num16 + config.PatchSize + 1;
					array[num8++] = num16 + 1;
					array[num8++] = num16 + 1;
					array[num8++] = num16 + config.PatchSize + 1;
					array[num8++] = num16 + 2;
					num15 += 2;
					num16 += 2;
				}
				else
				{
					array[num8++] = num16;
					array[num8++] = num16 + config.PatchSize - 1;
					array[num8++] = num16 + config.PatchSize;
					array[num8++] = num16;
					array[num8++] = num16 + config.PatchSize;
					array[num8++] = num16 + config.PatchSize + 1;
				}
				flag2 = !flag2;
			}
		}
		if ((edges & 2) > 0)
		{
			bool flag3 = false;
			ushort num17 = 0;
			ushort num18 = (ushort)(config.PatchSize - 1);
			while (num17 < config.PatchSize - 2)
			{
				if (!flag3)
				{
					array[num8++] = num18;
					array[num8++] = num18 + config.PatchSize - 1;
					array[num8++] = num18 + config.PatchSize * 2;
					num17 += 2;
					num18 += (ushort)(config.PatchSize * 2);
				}
				else
				{
					array[num8++] = num18;
					array[num8++] = num18 - config.PatchSize - 1;
					array[num8++] = num18 - 1;
					array[num8++] = num18;
					array[num8++] = num18 - 1;
					array[num8++] = num18 + config.PatchSize - 1;
				}
				flag3 = !flag3;
			}
		}
		else
		{
			bool flag4 = false;
			ushort num19 = 0;
			ushort num20 = (ushort)(config.PatchSize - 1);
			while (num19 < config.PatchSize - 2)
			{
				if (!flag4)
				{
					array[num8++] = num20;
					array[num8++] = num20 + config.PatchSize - 1;
					array[num8++] = num20 + config.PatchSize;
					array[num8++] = num20 + config.PatchSize;
					array[num8++] = num20 + config.PatchSize - 1;
					array[num8++] = num20 + config.PatchSize * 2;
					num19 += 2;
					num20 += (ushort)(config.PatchSize * 2);
				}
				else
				{
					array[num8++] = num20;
					array[num8++] = num20 - config.PatchSize - 1;
					array[num8++] = num20 - 1;
					array[num8++] = num20;
					array[num8++] = num20 - 1;
					array[num8++] = num20 + config.PatchSize - 1;
				}
				flag4 = !flag4;
			}
		}
		if ((edges & 4) > 0)
		{
			bool flag5 = false;
			ushort num21 = 0;
			ushort num22 = (ushort)((config.PatchSize - 1) * config.PatchSize);
			while (num21 < config.PatchSize - 2)
			{
				if (!flag5)
				{
					array[num8++] = num22;
					array[num8++] = num22 + 2;
					array[num8++] = num22 - config.PatchSize + 1;
					num21 += 2;
					num22 += 2;
				}
				else
				{
					array[num8++] = num22;
					array[num8++] = num22 - config.PatchSize;
					array[num8++] = num22 - config.PatchSize - 1;
					array[num8++] = num22;
					array[num8++] = num22 - config.PatchSize + 1;
					array[num8++] = num22 - config.PatchSize;
				}
				flag5 = !flag5;
			}
		}
		else
		{
			bool flag6 = false;
			ushort num23 = 0;
			ushort num24 = (ushort)((config.PatchSize - 1) * config.PatchSize);
			while (num23 < config.PatchSize - 2)
			{
				if (!flag6)
				{
					array[num8++] = num24;
					array[num8++] = num24 + 1;
					array[num8++] = num24 - config.PatchSize + 1;
					array[num8++] = num24 + 1;
					array[num8++] = num24 + 2;
					array[num8++] = num24 - config.PatchSize + 1;
					num23 += 2;
					num24 += 2;
				}
				else
				{
					array[num8++] = num24;
					array[num8++] = num24 - config.PatchSize;
					array[num8++] = num24 - config.PatchSize - 1;
					array[num8++] = num24;
					array[num8++] = num24 - config.PatchSize + 1;
					array[num8++] = num24 - config.PatchSize;
				}
				flag6 = !flag6;
			}
		}
		if ((edges & 8) > 0)
		{
			bool flag7 = false;
			ushort num25 = 0;
			ushort num26 = 0;
			while (num25 < config.PatchSize - 2)
			{
				if (!flag7)
				{
					array[num8++] = num26;
					array[num8++] = num26 + config.PatchSize * 2;
					array[num8++] = num26 + config.PatchSize + 1;
					num25 += 2;
					num26 += (ushort)(config.PatchSize * 2);
				}
				else
				{
					array[num8++] = num26;
					array[num8++] = num26 + 1;
					array[num8++] = num26 - config.PatchSize + 1;
					array[num8++] = num26;
					array[num8++] = num26 + config.PatchSize + 1;
					array[num8++] = num26 + 1;
				}
				flag7 = !flag7;
			}
		}
		else
		{
			bool flag8 = false;
			ushort num27 = 0;
			ushort num28 = 0;
			while (num27 < config.PatchSize - 2)
			{
				if (!flag8)
				{
					array[num8++] = num28;
					array[num8++] = num28 + config.PatchSize;
					array[num8++] = num28 + config.PatchSize + 1;
					array[num8++] = num28 + config.PatchSize;
					array[num8++] = num28 + config.PatchSize * 2;
					array[num8++] = num28 + config.PatchSize + 1;
					num27 += 2;
					num28 += (ushort)(config.PatchSize * 2);
				}
				else
				{
					array[num8++] = num28;
					array[num8++] = num28 + config.PatchSize + 1;
					array[num8++] = num28 + 1;
					array[num8++] = num28;
					array[num8++] = num28 + 1;
					array[num8++] = num28 - config.PatchSize + 1;
				}
				flag8 = !flag8;
			}
		}
		m_patches.Add(array);
	}
}
