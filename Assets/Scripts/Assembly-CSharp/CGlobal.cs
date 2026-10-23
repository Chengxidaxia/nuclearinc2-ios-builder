using System;
using UnityEngine;

public class CGlobal
{
	private static CGlobal m_GlobalInstance;

	public Texture2D PermTex;

	public Texture2D SimplexTex;

	private string[] NoiseMaterialNames = new string[2] { "GenNoiseRidged", "GenNoiseTurbulence" };

	public Material[] GenNoiseMaterial;

	public Material ShapeNoiseMaterial;

	public Material GenNormalMaterial;

	public Material PatchMaterial;

	public Material PackNoiseMaterial;

	public Material SimpleTexMaterial;

	public Material OuterAtmosphereMaterial;

	public Material InnerAtmosphereMaterial;

	public SimplexNoiseCPU SNoiseCPU;

	public const RenderTextureFormat RENDER_TEXTURE_FORMAT = RenderTextureFormat.ARGBHalf;

	public const TextureFormat TEXTURE_FORMAT = TextureFormat.ARGB32;

	public const TextureWrapMode WRAP_MODE = TextureWrapMode.Clamp;

	public static CGlobal GetInstance()
	{
		if (m_GlobalInstance == null)
		{
			m_GlobalInstance = new CGlobal();
		}
		return m_GlobalInstance;
	}

	~CGlobal()
	{
	}

	public void Setup()
	{
		Destroy();
		if (PermTex == null)
		{
			InitGenNoise();
		}
		if (GenNoiseMaterial == null)
		{
			GenNoiseMaterial = new Material[NoiseMaterialNames.Length];
			for (int i = 0; i < NoiseMaterialNames.Length; i++)
			{
				if (GenNoiseMaterial[i] == null)
				{
					GenNoiseMaterial[i] = new Material(Shader.Find(NoiseMaterialNames[i]));
				}
			}
		}
		if (GenNormalMaterial == null)
		{
			GenNormalMaterial = new Material(Shader.Find("GenNormal"));
		}
		if (ShapeNoiseMaterial == null)
		{
			ShapeNoiseMaterial = new Material(Shader.Find("ShapeNoise"));
		}
		if (PatchMaterial == null)
		{
			PatchMaterial = new Material(Shader.Find("TexNormalSplat"));
		}
		if (PackNoiseMaterial == null)
		{
			PackNoiseMaterial = new Material(Shader.Find("PackNoise"));
		}
		if (SimpleTexMaterial == null)
		{
			SimpleTexMaterial = new Material(Shader.Find("SimpleTex"));
		}
		if (OuterAtmosphereMaterial == null)
		{
			OuterAtmosphereMaterial = new Material(Shader.Find("Surf_AtmosOut"));
		}
		if (InnerAtmosphereMaterial == null)
		{
			InnerAtmosphereMaterial = new Material(Shader.Find("Surf_AtmosIn"));
		}
		if (SNoiseCPU == null)
		{
			SNoiseCPU = new SimplexNoiseCPU();
		}
	}

	public void Destroy()
	{
		if (PatchMaterial != null)
		{
			UnityEngine.Object.Destroy(PatchMaterial);
			PatchMaterial = null;
		}
		if (PackNoiseMaterial != null)
		{
			UnityEngine.Object.Destroy(PackNoiseMaterial);
			PackNoiseMaterial = null;
		}
		if (SimpleTexMaterial != null)
		{
			UnityEngine.Object.Destroy(SimpleTexMaterial);
			SimpleTexMaterial = null;
		}
		if (OuterAtmosphereMaterial != null)
		{
			UnityEngine.Object.Destroy(OuterAtmosphereMaterial);
			OuterAtmosphereMaterial = null;
		}
		if (InnerAtmosphereMaterial != null)
		{
			UnityEngine.Object.Destroy(InnerAtmosphereMaterial);
			InnerAtmosphereMaterial = null;
		}
		if (GenNoiseMaterial != null)
		{
			for (int i = 0; i < NoiseMaterialNames.Length; i++)
			{
				if (GenNoiseMaterial[i] != null)
				{
					UnityEngine.Object.Destroy(GenNoiseMaterial[i]);
					GenNoiseMaterial[i] = null;
				}
			}
			GenNoiseMaterial = null;
		}
		if (GenNormalMaterial != null)
		{
			UnityEngine.Object.Destroy(GenNormalMaterial);
			GenNormalMaterial = null;
		}
		if (ShapeNoiseMaterial != null)
		{
			UnityEngine.Object.Destroy(ShapeNoiseMaterial);
			ShapeNoiseMaterial = null;
		}
		if (PermTex != null)
		{
			UnityEngine.Object.Destroy(PermTex);
			PermTex = null;
		}
		if (SimplexTex != null)
		{
			UnityEngine.Object.Destroy(SimplexTex);
			SimplexTex = null;
		}
		SNoiseCPU = null;
	}

	public void InitGenNoise()
	{
		InitPermTexture();
		InitSimplexTexture();
	}

	private void InitPermTexture()
	{
		int[] array = new int[256]
		{
			151, 160, 137, 91, 90, 15, 131, 13, 201, 95,
			96, 53, 194, 233, 7, 225, 140, 36, 103, 30,
			69, 142, 8, 99, 37, 240, 21, 10, 23, 190,
			6, 148, 247, 120, 234, 75, 0, 26, 197, 62,
			94, 252, 219, 203, 117, 35, 11, 32, 57, 177,
			33, 88, 237, 149, 56, 87, 174, 20, 125, 136,
			171, 168, 68, 175, 74, 165, 71, 134, 139, 48,
			27, 166, 77, 146, 158, 231, 83, 111, 229, 122,
			60, 211, 133, 230, 220, 105, 92, 41, 55, 46,
			245, 40, 244, 102, 143, 54, 65, 25, 63, 161,
			1, 216, 80, 73, 209, 76, 132, 187, 208, 89,
			18, 169, 200, 196, 135, 130, 116, 188, 159, 86,
			164, 100, 109, 198, 173, 186, 3, 64, 52, 217,
			226, 250, 124, 123, 5, 202, 38, 147, 118, 126,
			255, 82, 85, 212, 207, 206, 59, 227, 47, 16,
			58, 17, 182, 189, 28, 42, 223, 183, 170, 213,
			119, 248, 152, 2, 44, 154, 163, 70, 221, 153,
			101, 155, 167, 43, 172, 9, 129, 22, 39, 253,
			19, 98, 108, 110, 79, 113, 224, 232, 178, 185,
			112, 104, 218, 246, 97, 228, 251, 34, 242, 193,
			238, 210, 144, 12, 191, 179, 162, 241, 81, 51,
			145, 235, 249, 14, 239, 107, 49, 192, 214, 31,
			181, 199, 106, 157, 184, 84, 204, 176, 115, 121,
			50, 45, 127, 4, 150, 254, 138, 236, 205, 93,
			222, 114, 67, 29, 24, 72, 243, 141, 128, 195,
			78, 66, 215, 61, 156, 180
		};
		int[,] array2 = new int[16, 3]
		{
			{ 0, 1, 1 },
			{ 0, 1, -1 },
			{ 0, -1, 1 },
			{ 0, -1, -1 },
			{ 1, 0, 1 },
			{ 1, 0, -1 },
			{ -1, 0, 1 },
			{ -1, 0, -1 },
			{ 1, 1, 0 },
			{ 1, -1, 0 },
			{ -1, 1, 0 },
			{ -1, -1, 0 },
			{ 1, 0, -1 },
			{ -1, 0, -1 },
			{ 0, -1, 1 },
			{ 0, 1, 1 }
		};
		Color[] array3 = new Color[65536];
		float num = 1f / 255f;
		for (int i = 0; i < 256; i++)
		{
			for (int j = 0; j < 256; j++)
			{
				int num2 = i * 256 + j;
				char c = (char)array[(j + array[i]) & 0xFF];
				array3[num2].r = (float)(array2[c & 0xF, 0] * 64 + 64) * num;
				array3[num2].g = (float)(array2[c & 0xF, 1] * 64 + 64) * num;
				array3[num2].b = (float)(array2[c & 0xF, 2] * 64 + 64) * num;
				array3[num2].a = (float)(int)c * num;
			}
		}
		PermTex = new Texture2D(256, 256, TextureFormat.ARGB32, false);
		PermTex.anisoLevel = 0;
		PermTex.filterMode = FilterMode.Bilinear;
		PermTex.wrapMode = TextureWrapMode.Repeat;
		PermTex.SetPixels(array3);
		PermTex.Apply(false);
	}

	private void InitSimplexTexture()
	{
		byte[,] array = new byte[64, 4]
		{
			{ 0, 64, 128, 192 },
			{ 0, 64, 192, 128 },
			{ 0, 0, 0, 0 },
			{ 0, 128, 192, 64 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 64, 128, 192, 0 },
			{ 0, 128, 64, 192 },
			{ 0, 0, 0, 0 },
			{ 0, 192, 64, 128 },
			{ 0, 192, 128, 64 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 64, 192, 128, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 64, 128, 0, 192 },
			{ 0, 0, 0, 0 },
			{ 64, 192, 0, 128 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 128, 192, 0, 64 },
			{ 128, 192, 64, 0 },
			{ 64, 0, 128, 192 },
			{ 64, 0, 192, 128 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 128, 0, 192, 64 },
			{ 0, 0, 0, 0 },
			{ 128, 64, 192, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 128, 0, 64, 192 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 192, 0, 64, 128 },
			{ 192, 0, 128, 64 },
			{ 0, 0, 0, 0 },
			{ 192, 64, 128, 0 },
			{ 128, 64, 0, 192 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 192, 64, 0, 128 },
			{ 0, 0, 0, 0 },
			{ 192, 128, 0, 64 },
			{ 192, 128, 64, 0 }
		};
		float num = 1f / 255f;
		Color[] array2 = new Color[64];
		for (int i = 0; i < 64; i++)
		{
			array2[i] = new Color((float)(int)array[i, 0] * num, (float)(int)array[i, 1] * num, (float)(int)array[i, 2] * num, (float)(int)array[i, 3] * num);
		}
		SimplexTex = new Texture2D(64, 1, TextureFormat.ARGB32, false);
		SimplexTex.anisoLevel = 0;
		SimplexTex.filterMode = FilterMode.Bilinear;
		SimplexTex.wrapMode = TextureWrapMode.Repeat;
		SimplexTex.SetPixels(array2);
		SimplexTex.Apply(false);
	}

	public Mesh MakeSphere(float radius, int divr, int divh)
	{
		if (divr < 3)
		{
			divr = 3;
		}
		if (divh < 3)
		{
			divh = 3;
		}
		int num = (divh - 2) * divr + 2 + (divh - 2);
		int num2 = (divr * 2 + divr * 2 * (divh - 3)) * 3;
		Mesh mesh = new Mesh();
		Vector3[] array = new Vector3[num];
		Vector2[] array2 = new Vector2[num];
		int[] array3 = new int[num2];
		int num3 = 0;
		int num4 = 0;
		array[num3++] = new Vector3(0f, radius, 0f);
		array[num3++] = new Vector3(0f, 0f - radius, 0f);
		float num5 = 0f;
		float num6 = (float)Math.PI / (float)(divh - 1);
		float num7 = (float)Math.PI * 2f / (float)divr;
		float num9;
		for (int i = 0; i < divh - 2; i++)
		{
			num5 += num6;
			float y = Mathf.Cos(num5) * radius;
			float num8 = Mathf.Sin(num5) * radius;
			num9 = 0f;
			for (int j = 0; j < divr; j++)
			{
				array[num3++] = new Vector3(Mathf.Cos(num9) * num8, y, Mathf.Sin(num9) * num8);
				if (j == divr - 1)
				{
					Vector3 vector = array[i * (divr + 1) + 2];
					array[num3++] = new Vector3(vector.x, vector.y, vector.z);
				}
				num9 += num7;
			}
		}
		num9 = 0f;
		float num10 = 0f;
		float num11 = 1f / (float)divr;
		float num13;
		float num12 = (num13 = 1f / (float)divh);
		for (int k = 0; k < divr; k++)
		{
			int[] array4 = new int[3]
			{
				0,
				2 + k + 1,
				2 + k
			};
			array3[num4++] = array4[0];
			array3[num4++] = array4[1];
			array3[num4++] = array4[2];
			array2[array4[0]].x = num10;
			array2[array4[0]].y = 0f;
			array2[array4[1]].x = num10 + num11;
			array2[array4[1]].y = num13;
			array2[array4[2]].x = num10;
			array2[array4[2]].y = num13;
			num10 += num11;
		}
		num7 = 1f / (float)(divr + 1);
		int num14 = 2;
		for (int l = 0; l < divh - 3; l++)
		{
			num10 = 0f;
			for (int m = 0; m < divr; m++)
			{
				int[] array5 = new int[3]
				{
					num14 + m,
					num14 + (divr + 1) + m + 1,
					num14 + (divr + 1) + m
				};
				array3[num4++] = array5[0];
				array3[num4++] = array5[1];
				array3[num4++] = array5[2];
				array2[array5[0]].x = num10;
				array2[array5[0]].y = num13;
				array2[array5[1]].x = num10 + num11;
				array2[array5[1]].y = num13 + num12;
				array2[array5[2]].x = num10;
				array2[array5[2]].y = num13 + num12;
				int[] array6 = new int[3]
				{
					num14 + m,
					num14 + m + 1,
					num14 + m + 1 + (divr + 1)
				};
				array3[num4++] = array6[0];
				array3[num4++] = array6[1];
				array3[num4++] = array6[2];
				array2[array6[0]].x = num10;
				array2[array6[0]].y = num13;
				array2[array6[1]].x = num10 + num11;
				array2[array6[1]].y = num13;
				array2[array6[2]].x = num10 + num11;
				array2[array6[2]].y = num13 + num12;
				num10 += num11;
			}
			num13 += num12;
			num14 += divr + 1;
		}
		int num15 = num - divr - 1;
		num10 = 0f;
		for (int n = 0; n < divr; n++)
		{
			int[] array7 = new int[3]
			{
				1,
				num15 + n,
				num15 + n + 1
			};
			array3[num4++] = array7[0];
			array3[num4++] = array7[1];
			array3[num4++] = array7[2];
			array2[array7[0]].x = num10;
			array2[array7[0]].y = 1f;
			array2[array7[1]].x = num10;
			array2[array7[1]].y = num13;
			array2[array7[2]].x = num10 + num11;
			array2[array7[2]].y = num13;
			num10 += num11;
		}
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array3;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		return mesh;
	}

	public void RenderQuadVolume(int width, int height, Material material, CBoundingVolume volume, bool flip)
	{
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.Viewport(new Rect(0f, 0f, width, height));
		material.SetPass(0);
		Vector3 v = volume.vertices[0];
		Vector3 v2 = volume.uvs[0];
		Vector3 v3 = volume.vertices[3];
		Vector3 v4 = volume.uvs[3];
		Vector3 v5 = volume.vertices[2];
		Vector3 v6 = volume.uvs[2];
		Vector3 v7 = volume.vertices[1];
		Vector3 v8 = volume.uvs[1];
		GL.Begin(7);
		if (flip)
		{
			GL.MultiTexCoord(0, new Vector3(0f, 1f, 0f));
			GL.MultiTexCoord(1, v);
			GL.MultiTexCoord(2, v2);
			GL.Vertex3(-1f, 1f, 0f);
			GL.MultiTexCoord(0, new Vector3(0f, 0f, 0f));
			GL.MultiTexCoord(1, v3);
			GL.MultiTexCoord(2, v4);
			GL.Vertex3(-1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 0f, 0f));
			GL.MultiTexCoord(1, v5);
			GL.MultiTexCoord(2, v6);
			GL.Vertex3(1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 1f, 0f));
			GL.MultiTexCoord(1, v7);
			GL.MultiTexCoord(2, v8);
			GL.Vertex3(1f, 1f, 0f);
		}
		else
		{
			GL.MultiTexCoord(0, new Vector3(0f, 0f, 0f));
			GL.MultiTexCoord(1, v);
			GL.MultiTexCoord(2, v2);
			GL.Vertex3(-1f, 1f, 0f);
			GL.MultiTexCoord(0, new Vector3(0f, 1f, 0f));
			GL.MultiTexCoord(1, v3);
			GL.MultiTexCoord(2, v4);
			GL.Vertex3(-1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 1f, 0f));
			GL.MultiTexCoord(1, v5);
			GL.MultiTexCoord(2, v6);
			GL.Vertex3(1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 0f, 0f));
			GL.MultiTexCoord(1, v7);
			GL.MultiTexCoord(2, v8);
			GL.Vertex3(1f, 1f, 0f);
		}
		GL.End();
		GL.PopMatrix();
	}

	public void RenderQuadVolumeExCoord(int width, int height, Material material, CBoundingVolume volume, bool flip)
	{
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.Viewport(new Rect(0f, 0f, width, height));
		material.SetPass(0);
		Vector3 v = volume.exvertices[0];
		Vector3 v2 = volume.uvs[0];
		Vector3 v3 = volume.exvertices[3];
		Vector3 v4 = volume.uvs[3];
		Vector3 v5 = volume.exvertices[2];
		Vector3 v6 = volume.uvs[2];
		Vector3 v7 = volume.exvertices[1];
		Vector3 v8 = volume.uvs[1];
		GL.Begin(7);
		if (flip)
		{
			GL.MultiTexCoord(0, new Vector3(0f, 1f, 0f));
			GL.MultiTexCoord(1, v);
			GL.MultiTexCoord(2, v2);
			GL.Vertex3(-1f, 1f, 0f);
			GL.MultiTexCoord(0, new Vector3(0f, 0f, 0f));
			GL.MultiTexCoord(1, v3);
			GL.MultiTexCoord(2, v4);
			GL.Vertex3(-1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 0f, 0f));
			GL.MultiTexCoord(1, v5);
			GL.MultiTexCoord(2, v6);
			GL.Vertex3(1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 1f, 0f));
			GL.MultiTexCoord(1, v7);
			GL.MultiTexCoord(2, v8);
			GL.Vertex3(1f, 1f, 0f);
		}
		else
		{
			GL.MultiTexCoord(0, new Vector3(0f, 0f, 0f));
			GL.MultiTexCoord(1, v);
			GL.MultiTexCoord(2, v2);
			GL.Vertex3(-1f, 1f, 0f);
			GL.MultiTexCoord(0, new Vector3(0f, 1f, 0f));
			GL.MultiTexCoord(1, v3);
			GL.MultiTexCoord(2, v4);
			GL.Vertex3(-1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 1f, 0f));
			GL.MultiTexCoord(1, v5);
			GL.MultiTexCoord(2, v6);
			GL.Vertex3(1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 0f, 0f));
			GL.MultiTexCoord(1, v7);
			GL.MultiTexCoord(2, v8);
			GL.Vertex3(1f, 1f, 0f);
		}
		GL.End();
		GL.PopMatrix();
	}

	public void RenderQuadVolumeExUv(int width, int height, int sourceWidth, Material material, CBoundingVolume volume, bool flip)
	{
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.Viewport(new Rect(0f, 0f, width, height));
		material.SetPass(0);
		float num = 1f / (float)sourceWidth;
		float num2 = 1f - num;
		Vector3 v = volume.vertices[0];
		Vector3 v2 = new Vector3(num, num, 0f);
		Vector3 v3 = volume.vertices[3];
		Vector3 v4 = new Vector3(num, num2, 0f);
		Vector3 v5 = volume.vertices[2];
		Vector3 v6 = new Vector3(num2, num2, 0f);
		Vector3 v7 = volume.vertices[1];
		Vector3 v8 = new Vector3(num2, num, 0f);
		GL.Begin(7);
		if (flip)
		{
			GL.MultiTexCoord(0, new Vector3(0f, 1f, 0f));
			GL.MultiTexCoord(1, v);
			GL.MultiTexCoord(2, v2);
			GL.Vertex3(-1f, 1f, 0f);
			GL.MultiTexCoord(0, new Vector3(0f, 0f, 0f));
			GL.MultiTexCoord(1, v3);
			GL.MultiTexCoord(2, v4);
			GL.Vertex3(-1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 0f, 0f));
			GL.MultiTexCoord(1, v5);
			GL.MultiTexCoord(2, v6);
			GL.Vertex3(1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 1f, 0f));
			GL.MultiTexCoord(1, v7);
			GL.MultiTexCoord(2, v8);
			GL.Vertex3(1f, 1f, 0f);
		}
		else
		{
			GL.MultiTexCoord(0, new Vector3(0f, 0f, 0f));
			GL.MultiTexCoord(1, v);
			GL.MultiTexCoord(2, v2);
			GL.Vertex3(-1f, 1f, 0f);
			GL.MultiTexCoord(0, new Vector3(0f, 1f, 0f));
			GL.MultiTexCoord(1, v3);
			GL.MultiTexCoord(2, v4);
			GL.Vertex3(-1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 1f, 0f));
			GL.MultiTexCoord(1, v5);
			GL.MultiTexCoord(2, v6);
			GL.Vertex3(1f, -1f, 0f);
			GL.MultiTexCoord(0, new Vector3(1f, 0f, 0f));
			GL.MultiTexCoord(1, v7);
			GL.MultiTexCoord(2, v8);
			GL.Vertex3(1f, 1f, 0f);
		}
		GL.End();
		GL.PopMatrix();
	}
}
