using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CPlanet : MonoBehaviour
{
	public bool m_Sphere = true;

	public bool m_Wysiwyg;

	public Transform m_SunTransform;

	public Camera m_Camera;

	public en_PatchQuality m_PatchQuality = en_PatchQuality.Standard;

	public en_NormalQuality m_NormalQuality = en_NormalQuality.Standard;

	public float m_Radius = 64f;

	public float m_Temperature = 2f;

	public float m_WaterLevel = 0.1f;

	public float m_HeightScale = 1f;

	public float m_NormalMultiplier = 16f;

	public int m_MaxSplitLevel = 10;

	public float m_SizeSplit = 5f;

	public float m_SizeRejoin = 8f;

	public bool m_EnablePhysics;

	public float m_GravityPower = 8f;

	public int m_MinPhysicsSplitLevel;

	public int m_MinShadowsSplitLevel = 10;

	public bool m_FilteredHeights = true;

	public CNoiseLayer[] layers = new CNoiseLayer[1]
	{
		new CNoiseLayer(NoiseLayerType.Turbulence, Vector3.zero, 16, 0.52f, 0.1f, 0f, 2f, 2, 1f)
	};

	public Texture2D m_WaterTex;

	public Texture2D m_WaterNormal;

	public float m_WaterTexTile = 1f;

	public float m_WaterSpeed = 0.1f;

	public Texture2D m_SnowTex;

	public float m_SnowTexTile = 16f;

	public Texture2D m_BaseTex;

	public float m_BaseTexTile = 32f;

	public Texture2D m_Diffuse1Tex;

	public float m_Diffuse1TexTile = 32f;

	public CLutLayer m_Diffuse1LutLayer = new CLutLayer(0f, 0f, 0f, 0f);

	public Texture2D m_Diffuse2Tex;

	public float m_Diffuse2TexTile = 32f;

	public CLutLayer m_Diffuse2LutLayer = new CLutLayer(0f, 0f, 0f, 0f);

	public Texture2D m_Diffuse3Tex;

	public float m_Diffuse3TexTile = 32f;

	public CLutLayer m_Diffuse3LutLayer = new CLutLayer(0f, 0f, 0f, 0f);

	public Texture2D m_Diffuse4Tex;

	public float m_Diffuse4TexTile = 32f;

	public CLutLayer m_Diffuse4LutLayer = new CLutLayer(0f, 0f, 0f, 0f);

	public Texture2D m_ShapeTop;

	public Texture2D m_ShapeBottom;

	public Texture2D m_ShapeLeft;

	public Texture2D m_ShapeRight;

	public Texture2D m_ShapeFront;

	public Texture2D m_ShapeBack;

	public bool hasAtmosphere;

	public float m_AtmosScale = 1f;

	public Color m_AtmosColor1 = new Color(0.8f, 0.8f, 1.1f) * 0.4f;

	public Color m_AtmosColor2 = new Color(0.3f, 0.3f, 1.3f) * 0.3f;

	public float m_sunIntensity = 2f;

	public float m_MinAtmosAlpha = 0.8f;

	public float m_HorizonHeight = 1f;

	public float m_HorizonIntensity = 32f;

	public float m_HorizonPower = 5f;

	public float m_FogIntensity;

	public float m_FogMaxAlpha;

	public float m_FogHeight = 0.5f;

	public float m_FogNear = 5f;

	public float m_FogFar = 50f;

	private float m_AtmosRadius;

	private Vector3 m_InvSunPos;

	private Vector3 m_SunDir;

	private List<CQuadtree> m_Quadtrees = new List<CQuadtree>();

	private float m_MinCamDist;

	private CQuadtree m_CloserNode;

	private ushort m_HighestSplitLevel;

	private bool m_Splitted;

	private bool m_Rejoined;

	private CPatchManager m_PatchManager;

	private CPatchConfig m_PatchConfig;

	private CLutGenerator m_LutGen;

	private CLutLayer[] m_LutLayers = new CLutLayer[4];

	private CLodSphere[] m_LodSpheres = new CLodSphere[2];

	public ushort HighestSplitLevel
	{
		get
		{
			return m_HighestSplitLevel;
		}
		set
		{
			m_HighestSplitLevel = value;
		}
	}

	public bool Splitted
	{
		get
		{
			return m_Splitted;
		}
		set
		{
			m_Splitted = value;
		}
	}

	public bool Rejoined
	{
		get
		{
			return m_Rejoined;
		}
		set
		{
			m_Rejoined = value;
		}
	}

	public float MinCamDist
	{
		get
		{
			return m_MinCamDist;
		}
		set
		{
			m_MinCamDist = value;
		}
	}

	public CQuadtree CloserNode
	{
		get
		{
			return m_CloserNode;
		}
		set
		{
			m_CloserNode = value;
		}
	}

	public CPatchManager PatchManager
	{
		get
		{
			return m_PatchManager;
		}
	}

	public CPatchConfig PatchConfig
	{
		get
		{
			return m_PatchConfig;
		}
	}

	public CLutGenerator LutGen
	{
		get
		{
			return m_LutGen;
		}
	}

	public float AtmosRadius
	{
		get
		{
			return m_AtmosRadius;
		}
	}

	public Vector3 InvSunPos
	{
		get
		{
			return m_InvSunPos;
		}
	}

	public Vector3 SunDir
	{
		get
		{
			return m_SunDir;
		}
	}

	private void Awake()
	{
		DestroyPlanet();
		Rebuild();
	}

	private void DestroyPlanet()
	{
		for (int i = 0; i < m_Quadtrees.Count; i++)
		{
			m_Quadtrees[i].DestroyTree();
		}
		m_Quadtrees.Clear();
		for (int j = 0; j < base.gameObject.transform.childCount; j++)
		{
			UnityEngine.Object.Destroy(base.gameObject.transform.GetChild(j).gameObject);
		}
		if (m_LodSpheres[0] != null)
		{
			m_LodSpheres[0].Destroy();
		}
		if (m_LodSpheres[1] != null)
		{
			m_LodSpheres[1].Destroy();
		}
		GC.Collect();
	}

	private void OnApplicationQuit()
	{
	}

	~CPlanet()
	{
	}

	public void Rebuild()
	{
		CGlobal instance = CGlobal.GetInstance();
		m_LutGen = new CLutGenerator();
		m_LutLayers[0] = new CLutLayer(m_Diffuse1LutLayer.minh, m_Diffuse1LutLayer.maxh, m_Diffuse1LutLayer.slope, m_Diffuse1LutLayer.aperture);
		m_LutLayers[1] = new CLutLayer(m_Diffuse2LutLayer.minh, m_Diffuse2LutLayer.maxh, m_Diffuse2LutLayer.slope, m_Diffuse2LutLayer.aperture);
		m_LutLayers[2] = new CLutLayer(m_Diffuse3LutLayer.minh, m_Diffuse3LutLayer.maxh, m_Diffuse3LutLayer.slope, m_Diffuse3LutLayer.aperture);
		m_LutLayers[3] = new CLutLayer(m_Diffuse4LutLayer.minh, m_Diffuse4LutLayer.maxh, m_Diffuse4LutLayer.slope, m_Diffuse4LutLayer.aperture);
		m_LutGen.UpdateLutTex(m_LutLayers);
		m_PatchConfig = new CPatchConfig(m_PatchQuality, m_NormalQuality, m_FilteredHeights);
		m_PatchManager = new CPatchManager(m_PatchConfig, this);
		instance.Setup();
		DestroyPlanet();
		m_Quadtrees.Add(new CQuadtree(new Vector3(0f, 1f, 0f), new Vector3(0f, 0f, -1f), this, m_ShapeTop));
		m_Quadtrees.Add(new CQuadtree(new Vector3(0f, -1f, 0f), new Vector3(0f, 0f, 1f), this, m_ShapeBottom));
		m_Quadtrees.Add(new CQuadtree(new Vector3(-1f, 0f, 0f), new Vector3(0f, 1f, 0f), this, m_ShapeLeft));
		m_Quadtrees.Add(new CQuadtree(new Vector3(1f, 0f, 0f), new Vector3(0f, 1f, 0f), this, m_ShapeRight));
		m_Quadtrees.Add(new CQuadtree(new Vector3(0f, 0f, 1f), new Vector3(0f, 1f, 0f), this, m_ShapeFront));
		m_Quadtrees.Add(new CQuadtree(new Vector3(0f, 0f, -1f), new Vector3(0f, 1f, 0f), this, m_ShapeBack));
		m_Quadtrees[0].SetNeighbor(en_NeighborDirection.NB_Top, m_Quadtrees[5], en_NeighborDirection.NB_Top);
		m_Quadtrees[0].SetNeighbor(en_NeighborDirection.NB_Bottom, m_Quadtrees[4], en_NeighborDirection.NB_Top);
		m_Quadtrees[0].SetNeighbor(en_NeighborDirection.NB_Left, m_Quadtrees[2], en_NeighborDirection.NB_Top);
		m_Quadtrees[0].SetNeighbor(en_NeighborDirection.NB_Right, m_Quadtrees[3], en_NeighborDirection.NB_Top);
		m_Quadtrees[1].SetNeighbor(en_NeighborDirection.NB_Top, m_Quadtrees[4], en_NeighborDirection.NB_Bottom);
		m_Quadtrees[1].SetNeighbor(en_NeighborDirection.NB_Bottom, m_Quadtrees[5], en_NeighborDirection.NB_Bottom);
		m_Quadtrees[1].SetNeighbor(en_NeighborDirection.NB_Left, m_Quadtrees[2], en_NeighborDirection.NB_Bottom);
		m_Quadtrees[1].SetNeighbor(en_NeighborDirection.NB_Right, m_Quadtrees[3], en_NeighborDirection.NB_Bottom);
		m_Quadtrees[2].SetNeighbor(en_NeighborDirection.NB_Top, m_Quadtrees[0], en_NeighborDirection.NB_Left);
		m_Quadtrees[2].SetNeighbor(en_NeighborDirection.NB_Bottom, m_Quadtrees[1], en_NeighborDirection.NB_Left);
		m_Quadtrees[2].SetNeighbor(en_NeighborDirection.NB_Left, m_Quadtrees[5], en_NeighborDirection.NB_Right);
		m_Quadtrees[2].SetNeighbor(en_NeighborDirection.NB_Right, m_Quadtrees[4], en_NeighborDirection.NB_Left);
		m_Quadtrees[3].SetNeighbor(en_NeighborDirection.NB_Top, m_Quadtrees[0], en_NeighborDirection.NB_Right);
		m_Quadtrees[3].SetNeighbor(en_NeighborDirection.NB_Bottom, m_Quadtrees[1], en_NeighborDirection.NB_Right);
		m_Quadtrees[3].SetNeighbor(en_NeighborDirection.NB_Left, m_Quadtrees[4], en_NeighborDirection.NB_Right);
		m_Quadtrees[3].SetNeighbor(en_NeighborDirection.NB_Right, m_Quadtrees[5], en_NeighborDirection.NB_Left);
		m_Quadtrees[4].SetNeighbor(en_NeighborDirection.NB_Top, m_Quadtrees[0], en_NeighborDirection.NB_Bottom);
		m_Quadtrees[4].SetNeighbor(en_NeighborDirection.NB_Bottom, m_Quadtrees[1], en_NeighborDirection.NB_Top);
		m_Quadtrees[4].SetNeighbor(en_NeighborDirection.NB_Left, m_Quadtrees[2], en_NeighborDirection.NB_Right);
		m_Quadtrees[4].SetNeighbor(en_NeighborDirection.NB_Right, m_Quadtrees[3], en_NeighborDirection.NB_Left);
		m_Quadtrees[5].SetNeighbor(en_NeighborDirection.NB_Top, m_Quadtrees[0], en_NeighborDirection.NB_Top);
		m_Quadtrees[5].SetNeighbor(en_NeighborDirection.NB_Bottom, m_Quadtrees[1], en_NeighborDirection.NB_Bottom);
		m_Quadtrees[5].SetNeighbor(en_NeighborDirection.NB_Left, m_Quadtrees[3], en_NeighborDirection.NB_Right);
		m_Quadtrees[5].SetNeighbor(en_NeighborDirection.NB_Right, m_Quadtrees[2], en_NeighborDirection.NB_Left);
		m_AtmosRadius = m_Radius + m_HeightScale * m_AtmosScale;
		m_LodSpheres = new CLodSphere[2]
		{
			new CLodSphere(true, this),
			new CLodSphere(false, this)
		};
		if (m_Camera == null)
		{
			Debug.Log("[ETHEREA1] Planet " + base.name + " had no associated Camera. Main Camera is being selected as the Camera for this Planet.");
			m_Camera = Camera.main;
		}
		if (m_SunTransform == null)
		{
			Debug.Log("[ETHEREA1] Planet " + base.name + " has no Sun. Please select a Light as the Sun for this Planet.");
		}
	}

	public void LateUpdate()
	{
		Vector3 vector = (Application.isPlaying ? base.transform.InverseTransformPoint(m_Camera.transform.position) : new Vector3(65535f, 65535f, 65535f));
		m_Splitted = (m_Rejoined = false);
		m_HighestSplitLevel = 0;
		m_MinCamDist = 9999999f;
		m_CloserNode = null;
		for (byte b = 0; b < m_Quadtrees.Count; b++)
		{
			m_Quadtrees[b].DoAnUpdate(vector);
		}
		for (byte b2 = 0; b2 < m_Quadtrees.Count; b2++)
		{
			m_Quadtrees[b2].RefreshTerrain(vector);
		}
		for (byte b3 = 0; b3 < m_Quadtrees.Count; b3++)
		{
			m_Quadtrees[b3].RefreshGaps();
		}
		float magnitude = vector.magnitude;
		int lod = 2 - Mathf.Min(2, (int)(magnitude / (m_AtmosRadius * 2f)));
		m_InvSunPos = base.transform.InverseTransformPoint(m_SunTransform.transform.position);
		m_SunDir = m_InvSunPos;
		m_SunDir.Normalize();
		m_LodSpheres[0].Select(hasAtmosphere && magnitude > m_AtmosRadius, lod, m_SunDir, vector);
		m_LodSpheres[1].Select(hasAtmosphere && magnitude <= m_AtmosRadius * 2f, lod, m_SunDir, vector);
	}
}
