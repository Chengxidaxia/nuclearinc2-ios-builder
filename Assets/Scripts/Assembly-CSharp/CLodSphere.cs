using UnityEngine;

public class CLodSphere
{
	private GameObject[] m_Spheres;

	private Material m_Material;

	private CPlanet m_Planet;

	public CLodSphere(bool outer, CPlanet planet)
	{
		CGlobal instance = CGlobal.GetInstance();
		m_Planet = planet;
		if (outer)
		{
			m_Material = Object.Instantiate(instance.OuterAtmosphereMaterial);
		}
		else
		{
			m_Material = Object.Instantiate(instance.InnerAtmosphereMaterial);
		}
		m_Material.SetFloat("_planetRadius", m_Planet.m_Radius);
		m_Material.SetFloat("_atmosRadius", m_Planet.AtmosRadius);
		m_Material.SetColor("_color1", m_Planet.m_AtmosColor1);
		m_Material.SetColor("_color2", m_Planet.m_AtmosColor2);
		m_Material.SetFloat("_sunIntensity", m_Planet.m_sunIntensity);
		m_Material.SetFloat("_horizonHeight", m_Planet.m_HorizonHeight);
		m_Material.SetFloat("_horizonIntensity", m_Planet.m_HorizonIntensity);
		m_Material.SetFloat("_horizonPower", m_Planet.m_HorizonPower);
		m_Material.SetFloat("_minAlpha", m_Planet.m_MinAtmosAlpha);
		m_Spheres = new GameObject[3];
		for (int i = 0; i < 3; i++)
		{
			Vector3 position = m_Planet.gameObject.transform.position;
			Quaternion rotation = m_Planet.gameObject.transform.rotation;
			m_Planet.gameObject.transform.position = Vector3.zero;
			m_Planet.gameObject.transform.rotation = Quaternion.identity;
			m_Spheres[i] = new GameObject("atmos lod " + i);
			m_Spheres[i].AddComponent<MeshFilter>();
			m_Spheres[i].AddComponent<MeshRenderer>();
			m_Spheres[i].GetComponent<MeshFilter>().sharedMesh = instance.MakeSphere((!outer) ? (m_Planet.AtmosRadius * 1.25f) : m_Planet.AtmosRadius, 15 * ((i + 1) * 4), 11 * ((i + 1) * 4));
			m_Spheres[i].transform.parent = m_Planet.gameObject.transform;
			m_Spheres[i].GetComponent<Renderer>().sharedMaterial = m_Material;
			m_Planet.gameObject.transform.position = position;
			m_Planet.gameObject.transform.rotation = rotation;
			m_Spheres[i].hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.NotEditable;
		}
	}

	public void Destroy()
	{
		if (m_Spheres != null && m_Spheres.Length > 0)
		{
			for (int i = 0; i < 3; i++)
			{
				Object.Destroy(m_Spheres[i]);
			}
			m_Spheres = null;
		}
	}

	public void Select(bool hasAtmosphere, int lod, Vector3 invSunDir, Vector3 invCamPos)
	{
		GameObject obj = m_Spheres[0];
		bool flag = false;
		m_Spheres[2].active = flag;
		flag = flag;
		m_Spheres[1].active = flag;
		obj.active = flag;
		m_Spheres[lod].active = hasAtmosphere;
		m_Spheres[lod].GetComponent<Renderer>().sharedMaterial.SetVector("_sunDir", invSunDir);
		m_Spheres[lod].GetComponent<Renderer>().sharedMaterial.SetVector("_camPos", invCamPos);
	}
}
