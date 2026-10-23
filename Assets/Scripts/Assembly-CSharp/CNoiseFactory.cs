using UnityEngine;

public static class CNoiseFactory
{
	public static void ShapeNoise(RenderTexture noiseRT, Texture2D shapeTex, CBoundingVolume volume)
	{
		CGlobal instance = CGlobal.GetInstance();
		RenderTexture.active = noiseRT;
		instance.ShapeNoiseMaterial.SetTexture("_hmap", noiseRT);
		instance.ShapeNoiseMaterial.SetTexture("_shape", shapeTex);
		instance.RenderQuadVolume(noiseRT.width, noiseRT.height, instance.ShapeNoiseMaterial, volume, false);
		RenderTexture.active = null;
	}

	public static void Execute(CNoiseLayer layer, RenderTexture noiseRT, CBoundingVolume volume, bool flip, float planetRadius)
	{
		CGlobal instance = CGlobal.GetInstance();
		int num = 0;
		float num2 = 1f;
		float num3 = layer.m_Frequency;
		switch (layer.m_LayerType)
		{
		case NoiseLayerType.Cell:
			Debug.Log("[ETHEREA1] NoiseLayerType.Cell is not implemented yet! Please select Ridged or Turbulence instead.");
			break;
		case NoiseLayerType.Ridged:
			num = 0;
			break;
		case NoiseLayerType.Turbulence:
			num = 1;
			break;
		}
		for (int i = 0; i < layer.m_Octaves; i++)
		{
			RenderTexture.active = noiseRT;
			instance.GenNoiseMaterial[num].SetFloat("_planetRadius", planetRadius);
			instance.GenNoiseMaterial[num].SetVector("_noiseOffset", layer.m_NoiseOffset);
			instance.GenNoiseMaterial[num].SetFloat("_offset", layer.m_Offset);
			instance.GenNoiseMaterial[num].SetFloat("_hpower", layer.m_HPower);
			instance.GenNoiseMaterial[num].SetTexture("_permTexture", instance.PermTex);
			instance.GenNoiseMaterial[num].SetTexture("_simplexTexture", instance.SimplexTex);
			instance.GenNoiseMaterial[num].SetFloat("_contribution", layer.m_Contribution);
			instance.GenNoiseMaterial[num].SetTexture("_previousPass", noiseRT);
			instance.GenNoiseMaterial[num].SetFloat("_amp", num2);
			instance.GenNoiseMaterial[num].SetFloat("_frequency", num3);
			instance.RenderQuadVolume(noiseRT.width, noiseRT.height, instance.GenNoiseMaterial[num], volume, flip);
			num3 *= layer.m_Lacunarity;
			num2 *= layer.m_Persistence;
			RenderTexture.active = null;
		}
	}
}
