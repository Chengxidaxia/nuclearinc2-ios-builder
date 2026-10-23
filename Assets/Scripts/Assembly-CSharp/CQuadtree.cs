using UnityEngine;

public class CQuadtree
{
	private CPlanet m_Planet;

	private Vector3[] vertices;

	private Vector2[] uvs;

	private Vector3[] vols;

	private Vector2[] uvvols;

	private Vector4[] tangents;

	private ushort m_SplitLevel;

	private bool m_HasChildren;

	private bool m_NeedsTerrain;

	private bool m_NeedsReedge;

	private float m_Size;

	private float m_LastCamDist;

	private float m_Min;

	private float m_Max;

	private Vector3 m_Up;

	private Vector3 m_Front;

	private Vector3 m_Right;

	private Vector3 m_SFront;

	private Vector3 m_SUp;

	private CBoundingVolume m_Volume;

	private CBoundingVolume m_SVolume;

	private Plane m_Plane;

	private Vector3 m_Center;

	private Vector3 m_SCenter;

	private Vector3 m_Normal;

	private int m_HeightMapRes;

	private RenderTexture m_NormalMapRT;

	private Texture2D m_ShapeTex;

	private GameObject m_GameObj;

	private Mesh m_Mesh;

	private MeshCollider m_MeshCollider;

	private CQuadtree[] m_Children = new CQuadtree[4];

	private CQuadtree m_Parent;

	private byte m_NeedsRejoinCount;

	private TNeighbor[] m_Neighbors = new TNeighbor[4];

	private byte m_Edges;

	private byte m_GapFixMask;

	public CQuadtree(Vector3 up, Vector3 front, CPlanet planet, Texture2D shapeTex)
	{
		m_Planet = planet;
		m_ShapeTex = shapeTex;
		m_Up = (m_SUp = up);
		m_Front = (m_SFront = front);
		m_Right = -Vector3.Cross(m_Up, m_Front);
		m_Parent = null;
		m_SplitLevel = 0;
		m_Size = m_Planet.m_Radius * 2f;
		m_Neighbors[0].node = (m_Neighbors[1].node = (m_Neighbors[2].node = (m_Neighbors[3].node = null)));
		m_Neighbors[0].isFixed = (m_Neighbors[1].isFixed = (m_Neighbors[2].isFixed = (m_Neighbors[3].isFixed = false)));
		m_Children[0] = (m_Children[1] = (m_Children[2] = (m_Children[3] = null)));
		m_NeedsRejoinCount = 0;
		m_HasChildren = false;
		m_NeedsReedge = true;
		m_NeedsTerrain = true;
		m_GapFixMask = 15;
		m_NormalMapRT = null;
		GenVolume();
		m_Plane = new Plane(m_Volume.vertices[0], m_Volume.vertices[2], m_Volume.vertices[1]);
	}

	public CQuadtree(CQuadtree parent, CBoundingVolume volume)
	{
		m_Parent = parent;
		m_Planet = parent.m_Planet;
		m_ShapeTex = parent.m_ShapeTex;
		m_SplitLevel = (ushort)(m_Parent.m_SplitLevel + 1);
		if (m_SplitLevel > m_Planet.HighestSplitLevel)
		{
			m_Planet.HighestSplitLevel = m_SplitLevel;
		}
		m_Size = m_Parent.m_Size / 2f;
		m_Volume = volume;
		Vector3 vector = m_Volume.vertices[0];
		Vector3 vector2 = m_Volume.vertices[1];
		Vector3 vector3 = m_Volume.vertices[2];
		Vector3 vector4 = m_Volume.vertices[3];
		Vector2 item = m_Volume.uvs[0];
		Vector2 item2 = m_Volume.uvs[1];
		Vector2 item3 = m_Volume.uvs[2];
		Vector2 item4 = m_Volume.uvs[3];
		m_SVolume = new CBoundingVolume();
		m_SVolume.vertices.Add(CMath.ProjectToSphere(vector, m_Planet.m_Radius));
		m_SVolume.vertices.Add(CMath.ProjectToSphere(vector2, m_Planet.m_Radius));
		m_SVolume.vertices.Add(CMath.ProjectToSphere(vector3, m_Planet.m_Radius));
		m_SVolume.vertices.Add(CMath.ProjectToSphere(vector4, m_Planet.m_Radius));
		m_SVolume.uvs.Add(item);
		m_SVolume.uvs.Add(item2);
		m_SVolume.uvs.Add(item3);
		m_SVolume.uvs.Add(item4);
		float num = m_Size / (float)(m_Planet.PatchConfig.PatchSize - 1);
		Vector3 vector5 = -m_Right;
		vector += vector5 * num + m_Up * num;
		vector2 += m_Right * num + m_Up * num;
		vector3 += m_Right * num - m_Up * num;
		vector4 += vector5 * num - m_Up * num;
		m_Volume.exvertices.Add(vector);
		m_Volume.exvertices.Add(vector2);
		m_Volume.exvertices.Add(vector3);
		m_Volume.exvertices.Add(vector4);
		m_Neighbors[0].node = (m_Neighbors[1].node = (m_Neighbors[2].node = (m_Neighbors[3].node = null)));
		m_Neighbors[0].isFixed = (m_Neighbors[1].isFixed = (m_Neighbors[2].isFixed = (m_Neighbors[3].isFixed = false)));
		m_Children[0] = (m_Children[1] = (m_Children[2] = (m_Children[3] = null)));
		m_NeedsRejoinCount = 0;
		m_HasChildren = false;
		m_NeedsReedge = true;
		m_NeedsTerrain = true;
		m_GapFixMask = 15;
		m_NormalMapRT = null;
		m_Normal = m_Parent.m_Normal;
		m_Up = m_Parent.m_Up;
		m_Center = (volume.vertices[0] + volume.vertices[1] + volume.vertices[2] + volume.vertices[3]) / 4f;
		m_SCenter = m_Center;
		m_SCenter = CMath.ProjectToSphere(m_SCenter, m_Planet.m_Radius);
		m_SUp = m_SCenter;
		m_SUp.Normalize();
		m_Front = m_Parent.m_Front;
		m_SFront = Vector3.Lerp(m_SVolume.vertices[0], m_SVolume.vertices[1], 0.5f) - m_SCenter;
		m_SFront.Normalize();
		m_Plane = m_Parent.m_Plane;
		m_Right = m_Parent.m_Right;
	}

	public void DestroyTree()
	{
		if (m_HasChildren)
		{
			for (int i = 0; i < 4; i++)
			{
				m_Children[i].DestroyTree();
				m_Children[i] = null;
			}
			m_HasChildren = false;
		}
		DestroyNode();
	}

	private void DestroyNode()
	{
		if (m_GameObj != null)
		{
			m_GameObj.transform.parent = null;
			Object.Destroy(m_GameObj.GetComponent<Renderer>().sharedMaterial);
			Object.Destroy(m_Mesh);
			Object.Destroy(m_GameObj);
			Object.Destroy(m_NormalMapRT);
			m_NormalMapRT = null;
			m_Mesh = null;
			m_GameObj = null;
		}
	}

	private void GenVolume()
	{
		Vector3 vector = -m_Right;
		Vector3 vector2 = vector * m_Planet.m_Radius + m_Front * m_Planet.m_Radius + m_Up * m_Planet.m_Radius;
		Vector3 vector3 = vector * (0f - m_Planet.m_Radius) + m_Front * m_Planet.m_Radius + m_Up * m_Planet.m_Radius;
		Vector3 vector4 = vector * (0f - m_Planet.m_Radius) + m_Front * (0f - m_Planet.m_Radius) + m_Up * m_Planet.m_Radius;
		Vector3 vector5 = vector * m_Planet.m_Radius + m_Front * (0f - m_Planet.m_Radius) + m_Up * m_Planet.m_Radius;
		Vector3 vector6 = new Vector3(0f, 0f, 0f);
		Vector3 vector7 = new Vector3(1f, 0f, 0f);
		Vector3 vector8 = new Vector3(1f, 1f, 0f);
		Vector3 vector9 = new Vector3(0f, 1f, 0f);
		m_Volume = new CBoundingVolume();
		m_Volume.vertices.Add(vector2);
		m_Volume.uvs.Add(vector6);
		m_Volume.vertices.Add(vector3);
		m_Volume.uvs.Add(vector7);
		m_Volume.vertices.Add(vector4);
		m_Volume.uvs.Add(vector8);
		m_Volume.vertices.Add(vector5);
		m_Volume.uvs.Add(vector9);
		float num = m_Size / (float)(m_Planet.PatchConfig.PatchSize - 1);
		vector2 += vector * num + m_Up * num;
		vector3 += m_Right * num + m_Up * num;
		vector4 += m_Right * num - m_Up * num;
		vector5 += vector * num - m_Up * num;
		m_Volume.exvertices.Add(vector2);
		m_Volume.exvertices.Add(vector3);
		m_Volume.exvertices.Add(vector4);
		m_Volume.exvertices.Add(vector5);
		Vector3 v = vector2;
		Vector3 v2 = vector3;
		Vector3 v3 = vector4;
		Vector3 v4 = vector5;
		v = CMath.ProjectToSphere(v, m_Planet.m_Radius);
		v2 = CMath.ProjectToSphere(v2, m_Planet.m_Radius);
		v3 = CMath.ProjectToSphere(v3, m_Planet.m_Radius);
		v4 = CMath.ProjectToSphere(v4, m_Planet.m_Radius);
		m_SVolume = new CBoundingVolume();
		m_SVolume.vertices.Add(v);
		m_SVolume.vertices.Add(v2);
		m_SVolume.vertices.Add(v3);
		m_SVolume.vertices.Add(v4);
		m_Normal = m_Up;
		m_Center = (vector2 + vector3 + vector4 + vector5) / 4f;
		m_SCenter = m_Center;
		m_SCenter = CMath.ProjectToSphere(m_SCenter, m_Planet.m_Radius);
	}

	private void GenTerrain()
	{
		CGlobal instance = CGlobal.GetInstance();
		m_HeightMapRes = m_Planet.PatchConfig.LevelHeightMapRes(m_SplitLevel);
		m_NormalMapRT = new RenderTexture(m_HeightMapRes, m_HeightMapRes, 0, RenderTextureFormat.ARGBHalf);
		m_NormalMapRT.anisoLevel = 0;
		m_NormalMapRT.isPowerOfTwo = true;
		m_NormalMapRT.wrapMode = TextureWrapMode.Clamp;
		m_NormalMapRT.filterMode = FilterMode.Point;
		m_NormalMapRT.useMipMap = false;
		m_NormalMapRT.Create();
		RenderTexture.active = m_NormalMapRT;
		GL.Clear(false, true, new Color(0f, 0f, 0f, 0f));
		CNoiseLayer[] layers = m_Planet.layers;
		foreach (CNoiseLayer layer in layers)
		{
			CNoiseFactory.Execute(layer, m_NormalMapRT, m_Volume, false, m_Planet.m_Radius);
		}
		if (m_ShapeTex != null)
		{
			CNoiseFactory.ShapeNoise(m_NormalMapRT, m_ShapeTex, m_Volume);
		}
		m_NormalMapRT.filterMode = (m_Planet.m_FilteredHeights ? FilterMode.Bilinear : FilterMode.Point);
		float num = m_Size / (float)m_HeightMapRes;
		float num2 = num / m_Planet.m_HeightScale;
		instance.GenNormalMaterial.SetFloat("_uvStep", 1f / (float)m_HeightMapRes);
		instance.GenNormalMaterial.SetFloat("_step", m_Planet.m_NormalMultiplier * num2);
		instance.GenNormalMaterial.SetTexture("_hmap", m_NormalMapRT);
		RenderTexture.active = m_NormalMapRT;
		instance.RenderQuadVolume(m_HeightMapRes, m_HeightMapRes, instance.GenNormalMaterial, m_Volume, false);
		RenderTexture.active = null;
		instance.PackNoiseMaterial.SetTexture("_hmap", m_NormalMapRT);
		RenderTexture.active = m_Planet.PatchConfig.LowResHeightmapRT;
		instance.RenderQuadVolume(m_Planet.PatchConfig.LowResHeightmapRT.width, m_Planet.PatchConfig.LowResHeightmapRT.height, instance.PackNoiseMaterial, m_Volume, false);
		m_Planet.PatchConfig.LowResHeightmapTex.ReadPixels(new Rect(0f, 0f, (int)m_Planet.PatchConfig.LowResHeightMapSize, (int)m_Planet.PatchConfig.LowResHeightMapSize), 0, 0, false);
		m_Planet.PatchConfig.LowResHeightmapTex.Apply();
		RenderTexture.active = null;
		Color[] pixels = m_Planet.PatchConfig.LowResHeightmapTex.GetPixels();
		m_GameObj = new GameObject();
		m_GameObj.name = "lvl " + m_SplitLevel + " : " + m_Up;
		m_GameObj.AddComponent<MeshFilter>();
		m_GameObj.AddComponent<MeshRenderer>();
		m_Mesh = new Mesh();
		vertices = new Vector3[m_Planet.PatchConfig.GridSize];
		uvs = new Vector2[m_Planet.PatchConfig.GridSize];
		vols = new Vector3[m_Planet.PatchConfig.GridSize];
		uvvols = new Vector2[m_Planet.PatchConfig.GridSize];
		tangents = new Vector4[m_Planet.PatchConfig.GridSize];
		Vector3 vector = m_Volume.vertices[0];
		float num3 = m_Size / (float)(m_Planet.PatchConfig.PatchSize - 1);
		float num4 = 1f / (float)m_HeightMapRes;
		float num5 = 1f - num4;
		float num6 = (num5 - num4) / (float)(m_Planet.PatchConfig.PatchSize - 1);
		float num7 = num4;
		float num8 = num4;
		float num9 = m_Volume.uvs[0].y;
		float num10 = (m_Volume.uvs[1].x - m_Volume.uvs[0].x) / (float)(m_Planet.PatchConfig.PatchSize - 1);
		float num11 = -1E+09f;
		int num12 = 0;
		for (ushort num13 = 0; num13 < m_Planet.PatchConfig.PatchSize; num13++)
		{
			Vector3 vector2 = vector;
			num7 = num4;
			float num14 = m_Volume.uvs[0].x;
			for (ushort num15 = 0; num15 < m_Planet.PatchConfig.PatchSize; num15++)
			{
				float num16 = (pixels[num12].r * 65536f + pixels[num12].g * 256f + pixels[num12].b) * 255f / 8355840f - 1f;
				if (num16 <= m_Planet.m_WaterLevel)
				{
					num16 = m_Planet.m_WaterLevel;
				}
				num16 *= m_Planet.m_HeightScale;
				if (num16 > num11)
				{
					num11 = num16;
				}
				uvs[num12] = new Vector2(num7, num8);
				num7 += num6;
				uvvols[num12] = new Vector2(num14, num9);
				num14 += num10;
				Vector3 vector3 = vector2;
				vector3.Normalize();
				tangents[num12] = -Vector3.Cross(vector3, m_SFront);
				tangents[num12].w = 1f;
				if (m_Planet.m_Sphere)
				{
					vector3 *= m_Planet.m_Radius + num16;
				}
				else
				{
					vector3 = vector2 + m_Up * num16;
				}
				vertices[num12] = vector3;
				vols[num12] = vector2;
				num12++;
				vector2 += m_Right * num3;
			}
			vector -= m_Front * num3;
			num8 += num6;
			num9 += num10;
		}
		m_SCenter = CMath.ProjectToSphere(m_SCenter, m_Planet.m_Radius + num11);
		Vector3 position = m_Planet.gameObject.transform.position;
		Quaternion rotation = m_Planet.gameObject.transform.rotation;
		m_Planet.gameObject.transform.position = Vector3.zero;
		m_Planet.gameObject.transform.rotation = Quaternion.identity;
		m_GameObj.transform.parent = m_Planet.gameObject.transform;
		m_Mesh.vertices = vertices;
		m_Mesh.uv = uvs;
		m_Mesh.uv2 = uvvols;
		m_Mesh.normals = vols;
		m_Mesh.tangents = tangents;
		m_Mesh.triangles = m_Planet.PatchManager.Patches[m_Edges];
		m_GameObj.GetComponent<Renderer>().sharedMaterial = Object.Instantiate(instance.PatchMaterial);
		m_Mesh.RecalculateBounds();
		m_GameObj.GetComponent<MeshFilter>().mesh = m_Mesh;
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetTexture("_hmap", m_NormalMapRT);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetTexture("_rndmap", instance.PermTex);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_planetRadius", m_Planet.m_Radius);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_atmosRadius", m_Planet.AtmosRadius);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_temperature", m_Planet.m_Temperature);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetTexture("_blendLut", m_Planet.LutGen.m_LutTex);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_waterLevel", m_Planet.m_WaterLevel);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetTexture("_waterTex", m_Planet.m_WaterTex);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetTexture("_waterNormal", m_Planet.m_WaterNormal);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_tilingWater", m_Planet.m_WaterTexTile);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetTexture("_snowTex", m_Planet.m_SnowTex);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_tilingSnow", m_Planet.m_SnowTexTile);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetTexture("_diffuseBase", m_Planet.m_BaseTex);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_tilingBase", m_Planet.m_BaseTexTile);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetTexture("_diffuse1", m_Planet.m_Diffuse1Tex);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_tiling1", m_Planet.m_Diffuse1TexTile);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetTexture("_diffuse2", m_Planet.m_Diffuse2Tex);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_tiling2", m_Planet.m_Diffuse2TexTile);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetTexture("_diffuse3", m_Planet.m_Diffuse3Tex);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_tiling3", m_Planet.m_Diffuse3TexTile);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetTexture("_diffuse4", m_Planet.m_Diffuse4Tex);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_tiling4", m_Planet.m_Diffuse4TexTile);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_sunIntensity", m_Planet.m_sunIntensity);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_horizonHeight", m_Planet.m_HorizonHeight);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_horizonIntensity", m_Planet.m_HorizonIntensity);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_horizonPower", m_Planet.m_HorizonPower);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetColor("_color1", m_Planet.m_AtmosColor1);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetColor("_color2", m_Planet.m_AtmosColor2);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_fogIntensity", m_Planet.m_FogIntensity);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_fogHeight", m_Planet.m_FogHeight);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_fogNear", m_Planet.m_FogNear);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_fogFar", m_Planet.m_FogFar);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_fogMaxAlpha", m_Planet.m_FogMaxAlpha);
		m_Planet.gameObject.transform.position = position;
		m_Planet.gameObject.transform.rotation = rotation;
		m_GameObj.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.NotEditable;
		if (m_Planet.m_EnablePhysics && m_SplitLevel >= m_Planet.m_MinPhysicsSplitLevel)
		{
			m_MeshCollider = m_GameObj.AddComponent<MeshCollider>();
			m_MeshCollider.enabled = true;
		}
		if (m_SplitLevel >= m_Planet.m_MinShadowsSplitLevel)
		{
			m_GameObj.GetComponent<Renderer>().castShadows = true;
			m_GameObj.GetComponent<Renderer>().receiveShadows = true;
		}
		else
		{
			m_GameObj.GetComponent<Renderer>().castShadows = false;
			m_GameObj.GetComponent<Renderer>().receiveShadows = false;
		}
		m_NeedsTerrain = false;
		if (m_Parent != null)
		{
			m_Parent.DestroyNode();
		}
	}

	public byte NEXT_EDGE(byte e)
	{
		return (byte)((e != 3) ? ((uint)(e + 1)) : 0u);
	}

	public byte PREV_EDGE(byte e)
	{
		return (byte)((e != 0) ? ((uint)(e - 1)) : 3u);
	}

	private void GapFix(byte directionsMask)
	{
		short num = 0;
		short num2 = 0;
		short num3 = 1;
		short num4 = 1;
		short num5 = 0;
		short num6 = (short)(m_Planet.PatchConfig.PatchSize - 1);
		short num7 = (short)((m_Planet.PatchConfig.PatchSize - 1) * m_Planet.PatchConfig.PatchSize);
		short num8 = (short)(num7 + num6);
		for (byte b = 0; b < 4; b++)
		{
			byte b2 = (byte)(1 << (int)b);
			if ((b2 & directionsMask) > 0)
			{
				short num9 = 0;
				if (!m_Neighbors[b].node.m_HasChildren)
				{
					switch ((en_NeighborDirection)b)
					{
					case en_NeighborDirection.NB_Top:
					{
						num = 0;
						num3 = 1;
						CQuadtree parent4 = m_Neighbors[1].node.m_Parent;
						num9 = (short)((parent4 == null || m_Parent == null || !parent4.Equals(m_Parent) || !m_Neighbors[1].node.m_Neighbors[0].node.Equals(m_Neighbors[b].node)) ? (m_Planet.PatchConfig.PatchSize >> 1) : 0);
						switch (m_Neighbors[b].directionThere)
						{
						case en_NeighborDirection.NB_Top:
							num2 = num6;
							num4 = -1;
							break;
						case en_NeighborDirection.NB_Bottom:
							num2 = num7;
							num4 = 1;
							break;
						case en_NeighborDirection.NB_Left:
							num2 = num5;
							num4 = (short)m_Planet.PatchConfig.PatchSize;
							break;
						case en_NeighborDirection.NB_Right:
							num2 = num8;
							num4 = (short)(-m_Planet.PatchConfig.PatchSize);
							break;
						}
						break;
					}
					case en_NeighborDirection.NB_Right:
					{
						num = num6;
						num3 = (short)m_Planet.PatchConfig.PatchSize;
						CQuadtree parent2 = m_Neighbors[2].node.m_Parent;
						num9 = (short)((parent2 == null || m_Parent == null || !parent2.Equals(m_Parent) || !m_Neighbors[2].node.m_Neighbors[1].node.Equals(m_Neighbors[b].node)) ? (m_Planet.PatchConfig.PatchSize >> 1) : 0);
						switch (m_Neighbors[b].directionThere)
						{
						case en_NeighborDirection.NB_Top:
							num2 = num6;
							num4 = -1;
							break;
						case en_NeighborDirection.NB_Bottom:
							num2 = num7;
							num4 = 1;
							break;
						case en_NeighborDirection.NB_Left:
							num2 = num5;
							num4 = (short)m_Planet.PatchConfig.PatchSize;
							break;
						case en_NeighborDirection.NB_Right:
							num2 = num8;
							num4 = (short)(-m_Planet.PatchConfig.PatchSize);
							break;
						}
						break;
					}
					case en_NeighborDirection.NB_Bottom:
					{
						num = num8;
						num3 = -1;
						CQuadtree parent3 = m_Neighbors[3].node.m_Parent;
						num9 = (short)((parent3 == null || m_Parent == null || !parent3.Equals(m_Parent) || !m_Neighbors[3].node.m_Neighbors[2].node.Equals(m_Neighbors[b].node)) ? (m_Planet.PatchConfig.PatchSize >> 1) : 0);
						switch (m_Neighbors[b].directionThere)
						{
						case en_NeighborDirection.NB_Top:
							num2 = num6;
							num4 = -1;
							break;
						case en_NeighborDirection.NB_Bottom:
							num2 = num7;
							num4 = 1;
							break;
						case en_NeighborDirection.NB_Left:
							num2 = num5;
							num4 = (short)m_Planet.PatchConfig.PatchSize;
							break;
						case en_NeighborDirection.NB_Right:
							num2 = num8;
							num4 = (short)(-m_Planet.PatchConfig.PatchSize);
							break;
						}
						break;
					}
					case en_NeighborDirection.NB_Left:
					{
						num = num7;
						num3 = (short)(-m_Planet.PatchConfig.PatchSize);
						CQuadtree parent = m_Neighbors[0].node.m_Parent;
						num9 = (short)((parent == null || m_Parent == null || !parent.Equals(m_Parent) || !m_Neighbors[0].node.m_Neighbors[3].node.Equals(m_Neighbors[b].node)) ? (m_Planet.PatchConfig.PatchSize >> 1) : 0);
						switch (m_Neighbors[b].directionThere)
						{
						case en_NeighborDirection.NB_Top:
							num2 = num6;
							num4 = -1;
							break;
						case en_NeighborDirection.NB_Bottom:
							num2 = num7;
							num4 = 1;
							break;
						case en_NeighborDirection.NB_Left:
							num2 = num5;
							num4 = (short)m_Planet.PatchConfig.PatchSize;
							break;
						case en_NeighborDirection.NB_Right:
							num2 = num8;
							num4 = (short)(-m_Planet.PatchConfig.PatchSize);
							break;
						}
						break;
					}
					}
					ushort num10 = m_Planet.PatchConfig.PatchSize;
					if ((m_Edges & b2) > 0)
					{
						num3 <<= 1;
						num10 >>= 1;
						num10++;
						num2 += (short)(num9 * num4);
					}
					if (!m_Neighbors[PREV_EDGE(b)].isFixed)
					{
						vertices[num] = m_Neighbors[b].node.vertices[num2];
					}
					else
					{
						m_Neighbors[b].node.vertices[num2] = vertices[num];
					}
					num += num3;
					num2 += num4;
					ushort num11 = 0;
					while (num11 < num10 - 2)
					{
						m_Neighbors[b].node.vertices[num2] = vertices[num];
						num11++;
						num += num3;
						num2 += num4;
					}
					if (!m_Neighbors[NEXT_EDGE(b)].isFixed)
					{
						vertices[num] = m_Neighbors[b].node.vertices[num2];
					}
					else
					{
						m_Neighbors[b].node.vertices[num2] = vertices[num];
					}
					m_Mesh.vertices = vertices;
					m_Mesh.RecalculateBounds();
					if ((bool)m_MeshCollider)
					{
						m_MeshCollider.sharedMesh = null;
						m_MeshCollider.sharedMesh = m_Mesh;
						m_MeshCollider.sharedMesh.RecalculateBounds();
					}
					m_Neighbors[b].node.m_Mesh.vertices = m_Neighbors[b].node.vertices;
					m_Neighbors[b].node.m_Mesh.RecalculateBounds();
					if ((bool)m_Neighbors[b].node.m_MeshCollider)
					{
						m_Neighbors[b].node.m_MeshCollider.sharedMesh = null;
						m_Neighbors[b].node.m_MeshCollider.sharedMesh = m_Neighbors[b].node.m_Mesh;
						m_Neighbors[b].node.m_MeshCollider.sharedMesh.RecalculateBounds();
					}
					m_Neighbors[b].isFixed = true;
					byte b3 = (byte)m_Neighbors[b].directionThere;
					m_Neighbors[b].node.m_Neighbors[b3].isFixed = true;
				}
			}
		}
	}

	public void RefreshTerrain(Vector3 entityPos)
	{
		if (!m_HasChildren)
		{
			if (m_NeedsTerrain)
			{
				GenTerrain();
			}
			UpdateShaderParms(entityPos);
		}
		else
		{
			for (byte b = 0; b < 4; b++)
			{
				m_Children[b].RefreshTerrain(entityPos);
			}
		}
	}

	public void RefreshGaps()
	{
		if (!m_HasChildren)
		{
			if (m_NeedsReedge)
			{
				Reedge();
			}
			if (m_GapFixMask > 0)
			{
				GapFix(m_GapFixMask);
				m_GapFixMask = 0;
			}
		}
		else
		{
			for (byte b = 0; b < 4; b++)
			{
				m_Children[b].RefreshGaps();
			}
		}
	}

	public void DoAnUpdate(Vector3 entityPos)
	{
		if (!m_HasChildren)
		{
			Vector3 vector = entityPos;
			float distanceToPoint = m_Plane.GetDistanceToPoint(vector);
			float distance = m_Plane.distance;
			float num = distance - distanceToPoint;
			float num2 = ((num == 0f) ? 0f : ((0f - distanceToPoint) / num));
			vector *= 1f - num2;
			float num3 = CMath.QuickDistance(m_Center, vector);
			if (num3 < m_Planet.MinCamDist)
			{
				m_Planet.MinCamDist = num3;
				m_Planet.CloserNode = this;
			}
			if (m_SplitLevel > m_Planet.HighestSplitLevel)
			{
				m_Planet.HighestSplitLevel = m_SplitLevel;
			}
			if (m_Planet.Splitted)
			{
				return;
			}
			m_LastCamDist = CMath.QuickDistance(m_SCenter, entityPos);
			if (m_LastCamDist < m_Size * m_Size * m_Planet.m_SizeSplit)
			{
				if (m_SplitLevel < m_Planet.m_MaxSplitLevel)
				{
					Split();
				}
			}
			else if (m_Parent != null)
			{
				float num4 = CMath.QuickDistance(m_Parent.m_SCenter, entityPos);
				if (num4 > m_Parent.m_Size * m_Parent.m_Size * m_Planet.m_SizeRejoin && (m_SplitLevel > m_Neighbors[0].node.m_SplitLevel || (!m_Neighbors[0].node.m_HasChildren && m_SplitLevel == m_Neighbors[0].node.m_SplitLevel)) && (m_SplitLevel > m_Neighbors[1].node.m_SplitLevel || (!m_Neighbors[1].node.m_HasChildren && m_SplitLevel == m_Neighbors[1].node.m_SplitLevel)) && (m_SplitLevel > m_Neighbors[2].node.m_SplitLevel || (!m_Neighbors[2].node.m_HasChildren && m_SplitLevel == m_Neighbors[2].node.m_SplitLevel)) && (m_SplitLevel > m_Neighbors[3].node.m_SplitLevel || (!m_Neighbors[3].node.m_HasChildren && m_SplitLevel == m_Neighbors[3].node.m_SplitLevel)))
				{
					m_Parent.m_NeedsRejoinCount++;
				}
			}
		}
		else
		{
			m_NeedsRejoinCount = 0;
			for (byte b = 0; b < 4; b++)
			{
				m_Children[b].DoAnUpdate(entityPos);
			}
			if (m_NeedsRejoinCount == 4)
			{
				Rejoin();
			}
		}
	}

	private void Split()
	{
		for (byte b = 0; b < 4; b++)
		{
			if (m_SplitLevel > m_Neighbors[b].node.m_SplitLevel && !m_Neighbors[b].node.m_HasChildren)
			{
				m_Neighbors[b].node.Split();
				return;
			}
		}
		CBoundingVolume cBoundingVolume = new CBoundingVolume();
		Vector3 item = m_Volume.vertices[0];
		Vector3 item2 = Vector3.Lerp(m_Volume.vertices[0], m_Volume.vertices[1], 0.5f);
		Vector3 center = m_Center;
		Vector3 item3 = Vector3.Lerp(m_Volume.vertices[3], m_Volume.vertices[0], 0.5f);
		cBoundingVolume.vertices.Add(item);
		cBoundingVolume.vertices.Add(item2);
		cBoundingVolume.vertices.Add(center);
		cBoundingVolume.vertices.Add(item3);
		Vector3 vector = m_Volume.uvs[0];
		Vector3 vector2 = Vector3.Lerp(m_Volume.uvs[0], m_Volume.uvs[1], 0.5f);
		Vector3 vector3 = Vector3.Lerp(m_Volume.uvs[3], m_Volume.uvs[0], 0.5f);
		Vector3 vector4 = new Vector3(vector2.x, vector3.y, 0f);
		cBoundingVolume.uvs.Add(vector);
		cBoundingVolume.uvs.Add(vector2);
		cBoundingVolume.uvs.Add(vector4);
		cBoundingVolume.uvs.Add(vector3);
		CQuadtree cQuadtree = new CQuadtree(this, cBoundingVolume);
		CBoundingVolume cBoundingVolume2 = new CBoundingVolume();
		Vector3 item4 = Vector3.Lerp(m_Volume.vertices[0], m_Volume.vertices[1], 0.5f);
		Vector3 item5 = m_Volume.vertices[1];
		Vector3 item6 = Vector3.Lerp(m_Volume.vertices[1], m_Volume.vertices[2], 0.5f);
		Vector3 center2 = m_Center;
		cBoundingVolume2.vertices.Add(item4);
		cBoundingVolume2.vertices.Add(item5);
		cBoundingVolume2.vertices.Add(item6);
		cBoundingVolume2.vertices.Add(center2);
		Vector3 vector5 = Vector3.Lerp(m_Volume.uvs[0], m_Volume.uvs[1], 0.5f);
		Vector3 vector6 = m_Volume.uvs[1];
		Vector3 vector7 = Vector3.Lerp(m_Volume.uvs[1], m_Volume.uvs[2], 0.5f);
		Vector3 vector8 = new Vector3(vector5.x, vector7.y, 0f);
		cBoundingVolume2.uvs.Add(vector5);
		cBoundingVolume2.uvs.Add(vector6);
		cBoundingVolume2.uvs.Add(vector7);
		cBoundingVolume2.uvs.Add(vector8);
		CQuadtree cQuadtree2 = new CQuadtree(this, cBoundingVolume2);
		CBoundingVolume cBoundingVolume3 = new CBoundingVolume();
		Vector3 center3 = m_Center;
		Vector3 item7 = Vector3.Lerp(m_Volume.vertices[1], m_Volume.vertices[2], 0.5f);
		Vector3 item8 = m_Volume.vertices[2];
		Vector3 item9 = Vector3.Lerp(m_Volume.vertices[3], m_Volume.vertices[2], 0.5f);
		cBoundingVolume3.vertices.Add(center3);
		cBoundingVolume3.vertices.Add(item7);
		cBoundingVolume3.vertices.Add(item8);
		cBoundingVolume3.vertices.Add(item9);
		Vector3 vector9 = Vector3.Lerp(m_Volume.uvs[1], m_Volume.uvs[2], 0.5f);
		Vector3 vector10 = m_Volume.uvs[2];
		Vector3 vector11 = Vector3.Lerp(m_Volume.uvs[3], m_Volume.uvs[2], 0.5f);
		Vector3 vector12 = new Vector3(vector11.x, vector9.y, 0f);
		cBoundingVolume3.uvs.Add(vector12);
		cBoundingVolume3.uvs.Add(vector9);
		cBoundingVolume3.uvs.Add(vector10);
		cBoundingVolume3.uvs.Add(vector11);
		CQuadtree cQuadtree3 = new CQuadtree(this, cBoundingVolume3);
		CBoundingVolume cBoundingVolume4 = new CBoundingVolume();
		Vector3 item10 = Vector3.Lerp(m_Volume.vertices[3], m_Volume.vertices[0], 0.5f);
		Vector3 center4 = m_Center;
		Vector3 item11 = Vector3.Lerp(m_Volume.vertices[3], m_Volume.vertices[2], 0.5f);
		Vector3 item12 = m_Volume.vertices[3];
		cBoundingVolume4.vertices.Add(item10);
		cBoundingVolume4.vertices.Add(center4);
		cBoundingVolume4.vertices.Add(item11);
		cBoundingVolume4.vertices.Add(item12);
		Vector3 vector13 = Vector3.Lerp(m_Volume.uvs[3], m_Volume.uvs[0], 0.5f);
		Vector3 vector14 = Vector3.Lerp(m_Volume.uvs[3], m_Volume.uvs[2], 0.5f);
		Vector3 vector15 = m_Volume.uvs[3];
		Vector3 vector16 = new Vector3(vector14.x, vector13.y, 0f);
		cBoundingVolume4.uvs.Add(vector13);
		cBoundingVolume4.uvs.Add(vector16);
		cBoundingVolume4.uvs.Add(vector14);
		cBoundingVolume4.uvs.Add(vector15);
		CQuadtree cQuadtree4 = new CQuadtree(this, cBoundingVolume4);
		cQuadtree.SetNeighbor(en_NeighborDirection.NB_Bottom, cQuadtree4, en_NeighborDirection.NB_Top);
		cQuadtree.SetNeighbor(en_NeighborDirection.NB_Right, cQuadtree2, en_NeighborDirection.NB_Left);
		cQuadtree2.SetNeighbor(en_NeighborDirection.NB_Bottom, cQuadtree3, en_NeighborDirection.NB_Top);
		cQuadtree2.SetNeighbor(en_NeighborDirection.NB_Left, cQuadtree, en_NeighborDirection.NB_Right);
		cQuadtree3.SetNeighbor(en_NeighborDirection.NB_Top, cQuadtree2, en_NeighborDirection.NB_Bottom);
		cQuadtree3.SetNeighbor(en_NeighborDirection.NB_Left, cQuadtree4, en_NeighborDirection.NB_Right);
		cQuadtree4.SetNeighbor(en_NeighborDirection.NB_Top, cQuadtree, en_NeighborDirection.NB_Bottom);
		cQuadtree4.SetNeighbor(en_NeighborDirection.NB_Right, cQuadtree3, en_NeighborDirection.NB_Left);
		m_Children[0] = cQuadtree;
		m_Children[1] = cQuadtree2;
		m_Children[2] = cQuadtree3;
		m_Children[3] = cQuadtree4;
		m_Planet.Splitted = true;
		m_HasChildren = true;
		Relink();
	}

	private void Relink()
	{
		m_Children[0].SetNeighbor(en_NeighborDirection.NB_Top, m_Neighbors[0].node, m_Neighbors[0].directionThere);
		m_Children[0].SetNeighbor(en_NeighborDirection.NB_Left, m_Neighbors[3].node, m_Neighbors[3].directionThere);
		m_Children[1].SetNeighbor(en_NeighborDirection.NB_Top, m_Neighbors[0].node, m_Neighbors[0].directionThere);
		m_Children[1].SetNeighbor(en_NeighborDirection.NB_Right, m_Neighbors[1].node, m_Neighbors[1].directionThere);
		m_Children[2].SetNeighbor(en_NeighborDirection.NB_Bottom, m_Neighbors[2].node, m_Neighbors[2].directionThere);
		m_Children[2].SetNeighbor(en_NeighborDirection.NB_Right, m_Neighbors[1].node, m_Neighbors[1].directionThere);
		m_Children[3].SetNeighbor(en_NeighborDirection.NB_Bottom, m_Neighbors[2].node, m_Neighbors[2].directionThere);
		m_Children[3].SetNeighbor(en_NeighborDirection.NB_Left, m_Neighbors[3].node, m_Neighbors[3].directionThere);
	}

	private void Reedge()
	{
		byte edges = m_Edges;
		m_Edges = 0;
		for (byte b = 0; b < 4; b++)
		{
			if (m_Neighbors[b].node.m_SplitLevel < m_SplitLevel)
			{
				m_Edges |= (byte)(1 << (int)b);
			}
		}
		if (m_Edges != edges)
		{
			m_Mesh.triangles = m_Planet.PatchManager.Patches[m_Edges];
		}
		m_NeedsReedge = false;
	}

	public void SetNeighbor(en_NeighborDirection direction, CQuadtree pToNode, en_NeighborDirection directionFromThere)
	{
		if (pToNode.m_HasChildren)
		{
			CQuadtree cQuadtree = null;
			float num = 0f;
			byte b = 0;
			for (byte b2 = 0; b2 < 4; b2++)
			{
				CQuadtree cQuadtree2 = pToNode.m_Children[b2];
				for (byte b3 = 0; b3 < 4; b3++)
				{
					if (cQuadtree2.m_Neighbors[b3].node.Equals(m_Parent))
					{
						if (cQuadtree == null)
						{
							cQuadtree = cQuadtree2;
							b = b3;
							num = CMath.QuickDistance(cQuadtree2.m_Center, m_Center);
							break;
						}
						if (CMath.QuickDistance(cQuadtree2.m_Center, m_Center) < num)
						{
							cQuadtree = cQuadtree2;
							b = b3;
							b2 = 4;
							break;
						}
					}
					else if (cQuadtree2.m_Neighbors[b3].node == this)
					{
						cQuadtree = cQuadtree2;
						b = b3;
						m_Neighbors[(int)direction].node = cQuadtree;
						m_Neighbors[(int)direction].directionThere = (en_NeighborDirection)b;
						m_NeedsReedge = true;
						return;
					}
				}
			}
			if (cQuadtree != null)
			{
				m_Neighbors[(int)direction].node = cQuadtree;
				m_Neighbors[(int)direction].directionThere = (en_NeighborDirection)b;
				cQuadtree.m_Neighbors[b].node = this;
				cQuadtree.m_Neighbors[b].directionThere = direction;
				m_NeedsReedge = true;
				cQuadtree.m_NeedsReedge = true;
				cQuadtree.m_GapFixMask |= (byte)(1 << (int)b);
				cQuadtree.m_Neighbors[b].isFixed = false;
			}
		}
		else
		{
			m_Neighbors[(int)direction].node = pToNode;
			m_Neighbors[(int)direction].directionThere = directionFromThere;
			m_Neighbors[(int)direction].node.m_NeedsReedge = true;
			m_NeedsReedge = true;
			m_GapFixMask |= (byte)(1 << (int)direction);
			m_Neighbors[(int)direction].isFixed = false;
		}
	}

	public TNeighbor GetNeighbor(en_NeighborDirection direction)
	{
		return m_Neighbors[(int)direction];
	}

	private void Rejoin()
	{
		m_HasChildren = false;
		m_NeedsReedge = true;
		m_Children[0].m_Neighbors[0].node.m_Neighbors[(int)m_Children[0].m_Neighbors[0].directionThere].node = this;
		m_Children[0].m_Neighbors[0].node.m_Neighbors[(int)m_Children[0].m_Neighbors[0].directionThere].isFixed = false;
		m_Children[0].m_Neighbors[0].node.m_GapFixMask |= (byte)(1 << (int)m_Children[0].m_Neighbors[0].directionThere);
		m_Children[0].m_Neighbors[0].node.m_NeedsReedge = true;
		m_Children[0].m_Neighbors[3].node.m_Neighbors[(int)m_Children[0].m_Neighbors[3].directionThere].node = this;
		m_Children[0].m_Neighbors[3].node.m_Neighbors[(int)m_Children[0].m_Neighbors[3].directionThere].isFixed = false;
		m_Children[0].m_Neighbors[3].node.m_GapFixMask |= (byte)(1 << (int)m_Children[0].m_Neighbors[3].directionThere);
		m_Children[0].m_Neighbors[3].node.m_NeedsReedge = true;
		m_Children[1].m_Neighbors[0].node.m_Neighbors[(int)m_Children[1].m_Neighbors[0].directionThere].node = this;
		m_Children[1].m_Neighbors[0].node.m_Neighbors[(int)m_Children[1].m_Neighbors[0].directionThere].isFixed = false;
		m_Children[1].m_Neighbors[0].node.m_GapFixMask |= (byte)(1 << (int)m_Children[1].m_Neighbors[0].directionThere);
		m_Children[1].m_Neighbors[0].node.m_NeedsReedge = true;
		m_Children[1].m_Neighbors[1].node.m_Neighbors[(int)m_Children[1].m_Neighbors[1].directionThere].node = this;
		m_Children[1].m_Neighbors[1].node.m_Neighbors[(int)m_Children[1].m_Neighbors[1].directionThere].isFixed = false;
		m_Children[1].m_Neighbors[1].node.m_GapFixMask |= (byte)(1 << (int)m_Children[1].m_Neighbors[1].directionThere);
		m_Children[1].m_Neighbors[1].node.m_NeedsReedge = true;
		m_Children[2].m_Neighbors[2].node.m_Neighbors[(int)m_Children[2].m_Neighbors[2].directionThere].node = this;
		m_Children[2].m_Neighbors[2].node.m_Neighbors[(int)m_Children[2].m_Neighbors[2].directionThere].isFixed = false;
		m_Children[2].m_Neighbors[2].node.m_GapFixMask |= (byte)(1 << (int)m_Children[2].m_Neighbors[2].directionThere);
		m_Children[2].m_Neighbors[2].node.m_NeedsReedge = true;
		m_Children[2].m_Neighbors[1].node.m_Neighbors[(int)m_Children[2].m_Neighbors[1].directionThere].node = this;
		m_Children[2].m_Neighbors[1].node.m_Neighbors[(int)m_Children[2].m_Neighbors[1].directionThere].isFixed = false;
		m_Children[2].m_Neighbors[1].node.m_GapFixMask |= (byte)(1 << (int)m_Children[2].m_Neighbors[1].directionThere);
		m_Children[2].m_Neighbors[1].node.m_NeedsReedge = true;
		m_Children[3].m_Neighbors[2].node.m_Neighbors[(int)m_Children[3].m_Neighbors[2].directionThere].node = this;
		m_Children[3].m_Neighbors[2].node.m_Neighbors[(int)m_Children[3].m_Neighbors[2].directionThere].isFixed = false;
		m_Children[3].m_Neighbors[2].node.m_GapFixMask |= (byte)(1 << (int)m_Children[3].m_Neighbors[2].directionThere);
		m_Children[3].m_Neighbors[2].node.m_NeedsReedge = true;
		m_Children[3].m_Neighbors[3].node.m_Neighbors[(int)m_Children[3].m_Neighbors[3].directionThere].node = this;
		m_Children[3].m_Neighbors[3].node.m_Neighbors[(int)m_Children[3].m_Neighbors[3].directionThere].isFixed = false;
		m_Children[3].m_Neighbors[3].node.m_GapFixMask |= (byte)(1 << (int)m_Children[3].m_Neighbors[3].directionThere);
		m_Children[3].m_Neighbors[3].node.m_NeedsReedge = true;
		for (byte b = 0; b < 4; b++)
		{
			if (m_Planet.CloserNode == m_Children[b])
			{
				m_Planet.CloserNode = this;
			}
			m_Children[b].DestroyTree();
			m_Children[b] = null;
		}
		if (m_Parent != null)
		{
			m_Parent.Relink();
		}
		m_Planet.Rejoined = true;
		m_NeedsTerrain = true;
	}

	private void UpdateShaderParms(Vector3 entityPos)
	{
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetFloat("_frame", Time.time * m_Planet.m_WaterSpeed);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetVector("_sunDir", m_Planet.SunDir);
		m_GameObj.GetComponent<Renderer>().sharedMaterial.SetVector("_camPos", entityPos);
	}
}
