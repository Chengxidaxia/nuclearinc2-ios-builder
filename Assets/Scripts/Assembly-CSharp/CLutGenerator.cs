using UnityEngine;

public class CLutGenerator
{
	public Texture2D m_LutTex;

	public CLutGenerator()
	{
		m_LutTex = new Texture2D(256, 256, TextureFormat.ARGB32, false);
	}

	public void UpdateLutTex(CLutLayer[] lutLayers)
	{
		float num = 1f / 256f;
		Color[] array = new Color[65536];
		int num2 = ((lutLayers.Length < 4) ? lutLayers.Length : 4);
		for (int i = 0; i < num2; i++)
		{
			float num3 = 0f;
			for (int j = 0; j < 256; j++)
			{
				if (num3 >= lutLayers[i].minh && num3 <= lutLayers[i].maxh)
				{
					float num4 = 0f;
					for (int k = 0; k < 256; k++)
					{
						float num5 = Mathf.Abs(lutLayers[i].slope - num4);
						float num8;
						if (num5 <= lutLayers[i].aperture)
						{
							float num6 = 1f - num5 / lutLayers[i].aperture;
							float num7 = 1f - (num3 - lutLayers[i].minh) / (lutLayers[i].maxh - lutLayers[i].minh);
							num8 = Mathf.Min(1f, Mathf.Max(0f, num6 * num7));
						}
						else
						{
							num8 = 0f;
						}
						switch (i)
						{
						case 0:
							array[j * 256 + k].r = num8;
							break;
						case 1:
							array[j * 256 + k].g = num8;
							break;
						case 2:
							array[j * 256 + k].b = num8;
							break;
						case 3:
							array[j * 256 + k].a = num8;
							break;
						}
						num4 += num;
					}
				}
				num3 += num;
			}
		}
		m_LutTex.anisoLevel = 0;
		m_LutTex.filterMode = FilterMode.Bilinear;
		m_LutTex.wrapMode = TextureWrapMode.Clamp;
		m_LutTex.SetPixels(array);
		m_LutTex.Apply(false);
	}
}
