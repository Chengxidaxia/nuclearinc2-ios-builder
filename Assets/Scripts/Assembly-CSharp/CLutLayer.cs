using System;

[Serializable]
public class CLutLayer
{
	public float minh;

	public float maxh;

	public float slope;

	public float aperture;

	public CLutLayer(float minh, float maxh, float slope, float aperture)
	{
		this.minh = minh;
		this.maxh = maxh;
		this.slope = slope;
		this.aperture = aperture;
	}
}
