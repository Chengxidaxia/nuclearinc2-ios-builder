using System;
using UnityEngine;

[Serializable]
public class CNoiseLayer
{
	public NoiseLayerType m_LayerType = NoiseLayerType.Turbulence;

	public Vector3 m_NoiseOffset = new Vector3(0f, 0f, 0f);

	public int m_Octaves = 1;

	public float m_Persistence = 0.6f;

	public float m_Frequency = 0.001f;

	public float m_Offset;

	public float m_Lacunarity = 1f;

	public int m_HPower = 2;

	public float m_Contribution = 1f;

	public CNoiseLayer(NoiseLayerType layerType, Vector3 noiseOffset, int octaves, float persistence, float frequency, float offset, float lacunarity, int hpower, float contribution)
	{
		m_LayerType = layerType;
		m_NoiseOffset = noiseOffset;
		m_Octaves = octaves;
		m_Persistence = persistence;
		m_Frequency = frequency;
		m_Offset = offset;
		m_Lacunarity = lacunarity;
		m_HPower = hpower;
		m_Contribution = contribution;
	}
}
