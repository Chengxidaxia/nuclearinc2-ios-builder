using UnityEngine;

public class CPatchConfig
{
	private ushort m_PatchSize;

	private ushort m_MaxHeightMapSize;

	private ushort m_LowResHeightMapSize;

	private ushort m_AtmosSlices;

	private ushort m_AtmosStacks;

	private ushort[] m_LevelHeightMapRes;

	private ushort m_GridSize;

	private Texture2D m_LowResHeightmapTex;

	private RenderTexture m_LowResHeightmapRT;

	public ushort PatchSize
	{
		get
		{
			return m_PatchSize;
		}
	}

	public ushort GridSize
	{
		get
		{
			return m_GridSize;
		}
	}

	public ushort MaxHeightMapSize
	{
		get
		{
			return m_MaxHeightMapSize;
		}
	}

	public ushort LowResHeightMapSize
	{
		get
		{
			return m_LowResHeightMapSize;
		}
	}

	public ushort AtmosSlices
	{
		get
		{
			return m_AtmosSlices;
		}
	}

	public ushort AtmosStacks
	{
		get
		{
			return m_AtmosStacks;
		}
	}

	public ushort MaxSplitLevel
	{
		get
		{
			return (ushort)(m_LevelHeightMapRes.Length - 1);
		}
	}

	public Texture2D LowResHeightmapTex
	{
		get
		{
			return m_LowResHeightmapTex;
		}
	}

	public RenderTexture LowResHeightmapRT
	{
		get
		{
			return m_LowResHeightmapRT;
		}
	}

	public CPatchConfig(en_PatchQuality patchQuality, en_NormalQuality normalQuality, bool filteredHeights)
	{
		switch (patchQuality)
		{
		case en_PatchQuality.Maximum:
			m_PatchSize = 33;
			m_AtmosSlices = 128;
			m_AtmosStacks = 96;
			break;
		case en_PatchQuality.High:
			m_PatchSize = 21;
			m_AtmosSlices = 128;
			m_AtmosStacks = 96;
			break;
		case en_PatchQuality.Standard:
			m_PatchSize = 17;
			m_AtmosSlices = 128;
			m_AtmosStacks = 96;
			break;
		case en_PatchQuality.Low:
			m_PatchSize = 11;
			m_AtmosSlices = 96;
			m_AtmosStacks = 72;
			break;
		case en_PatchQuality.Minimum:
			m_PatchSize = 7;
			m_AtmosSlices = 96;
			m_AtmosStacks = 72;
			break;
		}
		switch (normalQuality)
		{
		case en_NormalQuality.Maximum:
			m_MaxHeightMapSize = 512;
			m_LevelHeightMapRes = new ushort[6] { 512, 512, 256, 256, 256, 128 };
			break;
		case en_NormalQuality.High:
			m_MaxHeightMapSize = 256;
			m_LevelHeightMapRes = new ushort[4] { 256, 256, 256, 128 };
			break;
		case en_NormalQuality.Standard:
			m_MaxHeightMapSize = 256;
			m_LevelHeightMapRes = new ushort[6] { 256, 256, 128, 128, 128, 64 };
			break;
		case en_NormalQuality.Low:
			m_MaxHeightMapSize = 128;
			m_LevelHeightMapRes = new ushort[4] { 128, 128, 128, 64 };
			break;
		case en_NormalQuality.Minimum:
			m_MaxHeightMapSize = 64;
			m_LevelHeightMapRes = new ushort[1] { 64 };
			break;
		}
		m_LowResHeightMapSize = m_PatchSize;
		m_GridSize = (ushort)(m_PatchSize * m_PatchSize);
		if (m_LowResHeightmapTex != null)
		{
			Object.Destroy(m_LowResHeightmapTex);
			Object.Destroy(m_LowResHeightmapRT);
		}
		m_LowResHeightmapTex = new Texture2D(m_LowResHeightMapSize, m_LowResHeightMapSize, TextureFormat.ARGB32, false);
		m_LowResHeightmapTex.anisoLevel = 0;
		m_LowResHeightmapTex.filterMode = (filteredHeights ? FilterMode.Bilinear : FilterMode.Point);
		m_LowResHeightmapTex.wrapMode = TextureWrapMode.Clamp;
		m_LowResHeightmapTex.Apply();
		m_LowResHeightmapRT = new RenderTexture(m_LowResHeightMapSize, m_LowResHeightMapSize, 0, RenderTextureFormat.ARGB32);
		m_LowResHeightmapRT.anisoLevel = 0;
		m_LowResHeightmapRT.isPowerOfTwo = false;
		m_LowResHeightmapRT.wrapMode = TextureWrapMode.Clamp;
		m_LowResHeightmapRT.filterMode = (filteredHeights ? FilterMode.Bilinear : FilterMode.Point);
		m_LowResHeightmapRT.useMipMap = false;
		m_LowResHeightmapRT.Create();
	}

	public ushort LevelHeightMapRes(ushort level)
	{
		return m_LevelHeightMapRes[(level < MaxSplitLevel) ? level : MaxSplitLevel];
	}

	~CPatchConfig()
	{
	}

	public void Destroy()
	{
		if (m_LowResHeightmapTex != null)
		{
			Object.Destroy(m_LowResHeightmapTex);
			Object.Destroy(m_LowResHeightmapRT);
		}
		m_LowResHeightmapTex = null;
		m_LowResHeightmapRT = null;
	}
}
