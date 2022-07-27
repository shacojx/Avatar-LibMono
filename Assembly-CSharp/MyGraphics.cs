using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class MyGraphics
{
	// Token: 0x0600005E RID: 94 RVA: 0x000046F4 File Offset: 0x00002AF4
	private void cache(string key, Texture value)
	{
		if (this.areaTexture > 100f)
		{
			MyGraphics.cachedTextures.Clear();
			this.areaTexture = 0f;
		}
		this.areaTexture += (float)(value.width * value.height);
		MyGraphics.cachedTextures.Add(key, value);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x0000474D File Offset: 0x00002B4D
	public static void clearCache()
	{
		MyGraphics.cachedTextures.Clear();
	}

	// Token: 0x06000060 RID: 96 RVA: 0x0000475C File Offset: 0x00002B5C
	public void translate(float tx, float ty)
	{
		this.translateX += tx;
		this.translateY += ty;
		this.isTranslate = true;
		if (this.translateX == 0f && this.translateY == 0f)
		{
			this.isTranslate = false;
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x000047B3 File Offset: 0x00002BB3
	public float getTranslateX()
	{
		return this.translateX;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000047BB File Offset: 0x00002BBB
	public float getTranslateY()
	{
		return this.translateY;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000047C3 File Offset: 0x00002BC3
	public void setClip(float x, float y, float w, float h)
	{
		this.clipTX = this.translateX;
		this.clipTY = this.translateY;
		this.clipX = x;
		this.clipY = y;
		this.clipW = w;
		this.clipH = h;
		this.isClip = true;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00004804 File Offset: 0x00002C04
	public void drawLine(float x1, float y1, float x2, float y2)
	{
		if (y1 == y2)
		{
			if (x1 > x2)
			{
				float num = x2;
				x2 = x1;
				x1 = num;
			}
			this.fillRect(x1, y1, x2 - x1, 1f);
			return;
		}
		if (x1 == x2)
		{
			if (y1 > y2)
			{
				float num2 = y2;
				y2 = y1;
				y1 = num2;
			}
			this.fillRect(x1, y1, 1f, y2 - y1);
			return;
		}
		if (this.isTranslate)
		{
			x1 += this.translateX;
			y1 += this.translateY;
			x2 += this.translateX;
			y2 += this.translateY;
		}
		string key = string.Concat(new object[]
		{
			"dl",
			this.r,
			this.g,
			this.b
		});
		Texture2D texture2D = (Texture2D)MyGraphics.cachedTextures[key];
		if (texture2D == null)
		{
			texture2D = new Texture2D(1, 1);
			Color color;
			color..ctor(this.r, this.g, this.b);
			texture2D.SetPixel(0, 0, color);
			texture2D.Apply();
			this.cache(key, texture2D);
		}
		Vector2 vector;
		vector..ctor(x1, y1);
		Vector2 vector2;
		vector2..ctor(x2, y2);
		Vector2 vector3 = vector2 - vector;
		float num3 = 57.29578f * Mathf.Atan(vector3.y / vector3.x);
		if (vector3.x < 0f)
		{
			num3 += 180f;
		}
		float num4 = Mathf.Ceil(0f);
		GUIUtility.RotateAroundPivot(num3, vector);
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		if (this.isClip)
		{
			num5 = this.clipX;
			num6 = this.clipY;
			num7 = this.clipW;
			num8 = this.clipH;
			if (this.isTranslate)
			{
				num5 += this.clipTX;
				num6 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect(num5, num6, num7, num8));
		}
		Graphics.DrawTexture(new Rect(vector.x - num5, vector.y - num4 - num6, vector3.magnitude, 1f), texture2D);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
		GUIUtility.RotateAroundPivot(-num3, vector);
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00004A64 File Offset: 0x00002E64
	public void drawRect(float x, float y, float w, float h)
	{
		this.fillRect(x, y, w, 1f);
		this.fillRect(x, y, 1f, h);
		this.fillRect(x + w, y, 1f, h);
		this.fillRect(x, y + h, w, 1f);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00004AB0 File Offset: 0x00002EB0
	public void fillRect(float x, float y, float w, float h)
	{
		if (w < 0f || h < 0f)
		{
			return;
		}
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		int num = 1;
		int num2 = 1;
		string key = string.Concat(new object[]
		{
			"fr",
			num,
			num2,
			this.r,
			this.g,
			this.b
		});
		Texture2D texture2D = (Texture2D)MyGraphics.cachedTextures[key];
		if (texture2D == null)
		{
			texture2D = new Texture2D(num, num2);
			Color color;
			color..ctor(this.r, this.g, this.b);
			texture2D.SetPixel(0, 0, color);
			texture2D.Apply();
			this.cache(key, texture2D);
		}
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		if (this.isClip)
		{
			num3 = this.clipX;
			num4 = this.clipY;
			num5 = this.clipW;
			num6 = this.clipH;
			if (this.isTranslate)
			{
				num3 += this.clipTX;
				num4 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect(num3, num4, num5, num6));
		}
		GUI.DrawTexture(new Rect(x - num3, y - num4, w, h), texture2D);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00004C4C File Offset: 0x0000304C
	public void setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00004CA0 File Offset: 0x000030A0
	public void setColor(Color color)
	{
		this.b = color.b;
		this.g = color.g;
		this.r = color.r;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00004CCC File Offset: 0x000030CC
	public void setBgColor(int rgb)
	{
		if (rgb != this.currentBGColor)
		{
			this.currentBGColor = rgb;
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			int num3 = rgb >> 16 & 255;
			this.b = (float)num / 256f;
			this.g = (float)num2 / 256f;
			this.r = (float)num3 / 256f;
			Main.main.GetComponent<Camera>().backgroundColor = new Color(this.r, this.g, this.b);
		}
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00004D5C File Offset: 0x0000315C
	public void drawString(string s, float x, float y, GUIStyle style)
	{
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		if (this.isClip)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			if (this.isTranslate)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect(num, num2, num3, num4));
		}
		GUI.Label(new Rect(x - num, y - num2, ScaleGUI.WIDTH, 100f), s, style);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00004E2C File Offset: 0x0000322C
	public void drawString(string s, float x, float y, GUIStyle style, int w)
	{
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		if (this.isClip)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			if (this.isTranslate)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect(num, num2, num3, num4));
		}
		GUI.Label(new Rect(x - num, y - num2, (float)w, 100f), s, style);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00004EF8 File Offset: 0x000032F8
	public void drawRegion(Image image, float x0, float y0, int w, int h, int transform, float x, float y, int anchor)
	{
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		float num = (float)w;
		float num2 = (float)h;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 1f;
		float num8 = 0f;
		if ((anchor & MyGraphics.HCENTER) == MyGraphics.HCENTER)
		{
			num5 -= num / 2f;
		}
		if ((anchor & MyGraphics.VCENTER) == MyGraphics.VCENTER)
		{
			num6 -= num2 / 2f;
		}
		if ((anchor & MyGraphics.RIGHT) == MyGraphics.RIGHT)
		{
			num5 -= num;
		}
		if ((anchor & MyGraphics.BOTTOM) == MyGraphics.BOTTOM)
		{
			num6 -= num2;
		}
		x += num5;
		y += num6;
		float num9 = 0f;
		float num10 = 0f;
		if (this.isClip)
		{
			num9 = this.clipX;
			float num11 = this.clipY;
			num10 = this.clipW;
			float num12 = this.clipH;
			if (this.isTranslate)
			{
				num9 += this.clipTX;
				num11 += this.clipTY;
			}
			Rect r;
			r..ctor(x, y, (float)w, (float)h);
			Rect r2;
			r2..ctor(num9, num11, num10, num12);
			Rect rect = this.intersectRect(r, r2);
			if (rect.width <= 0f || rect.height <= 0f)
			{
				return;
			}
			num = rect.width;
			num2 = rect.height;
			num3 = rect.x - r.x;
			num4 = rect.y - r.y;
		}
		float num13 = 0f;
		if (transform == 2)
		{
			num13 += num;
			num7 = -1f;
			if (this.isClip)
			{
				if (num9 > x)
				{
					num8 = -num3;
				}
				else if (num9 + num10 < x + (float)w)
				{
					num8 = -(num9 + num10 - x - (float)w);
				}
			}
		}
		Graphics.DrawTexture(new Rect(x + num3 + num13, y + num4, num * num7, num2), image.texture, new Rect((x0 + num3 + num8) / (float)image.texture.width, ((float)image.texture.height - num2 - (y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
	}

	// Token: 0x0600006D RID: 109 RVA: 0x0000518C File Offset: 0x0000358C
	public void drawRegion(Image image, float x0, float y0, int w, int h, int transform, float x, float y, float wScale, float hScale, int anchor)
	{
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		float num = (float)w;
		float num2 = (float)h;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		if ((anchor & MyGraphics.HCENTER) == MyGraphics.HCENTER)
		{
			num5 -= num / 2f;
		}
		if ((anchor & MyGraphics.VCENTER) == MyGraphics.VCENTER)
		{
			num6 -= num2 / 2f;
		}
		if ((anchor & MyGraphics.RIGHT) == MyGraphics.RIGHT)
		{
			num5 -= num;
		}
		if ((anchor & MyGraphics.BOTTOM) == MyGraphics.BOTTOM)
		{
			num6 -= num2;
		}
		x += num5;
		y += num6;
		float num8 = 0f;
		float num9 = 0f;
		if (this.isClip)
		{
			num8 = this.clipX;
			float num10 = this.clipY;
			num9 = this.clipW;
			float num11 = this.clipH;
			if (this.isTranslate)
			{
				num8 += this.clipTX;
				num10 += this.clipTY;
			}
			Rect r;
			r..ctor(x, y, (float)w, (float)h);
			Rect r2;
			r2..ctor(num8, num10, num9, num11);
			Rect rect = this.intersectRect(r, r2);
			if (rect.width <= 0f || rect.height <= 0f)
			{
				return;
			}
			num = rect.width;
			num2 = rect.height;
			num3 = rect.x - r.x;
			num4 = rect.y - r.y;
		}
		float num12 = 0f;
		if (transform == 2)
		{
			num12 += num;
			if (this.isClip)
			{
				if (num8 > x)
				{
					num7 = -num3;
				}
				else if (num8 + num9 < x + (float)w)
				{
					num7 = -(num8 + num9 - x - (float)w);
				}
			}
		}
		Graphics.DrawTexture(new Rect(x + num3 + num12, y + num4, wScale, hScale), image.texture, new Rect((x0 + num3 + num7) / (float)image.texture.width, ((float)image.texture.height - num2 - (y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00005420 File Offset: 0x00003820
	public void drawImage(Image image, float x, float y, int anchor)
	{
		if (image == null)
		{
			return;
		}
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		float num = (float)image.w;
		float num2 = (float)image.h;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		if ((anchor & MyGraphics.HCENTER) == MyGraphics.HCENTER)
		{
			num5 -= num / 2f;
		}
		if ((anchor & MyGraphics.VCENTER) == MyGraphics.VCENTER)
		{
			num6 -= num2 / 2f;
		}
		if ((anchor & MyGraphics.RIGHT) == MyGraphics.RIGHT)
		{
			num5 -= num;
		}
		if ((anchor & MyGraphics.BOTTOM) == MyGraphics.BOTTOM)
		{
			num6 -= num2;
		}
		x += num5;
		y += num6;
		if (this.isClip)
		{
			float num7 = this.clipX;
			float num8 = this.clipY;
			float num9 = this.clipW;
			float num10 = this.clipH;
			if (this.isTranslate)
			{
				num7 += this.clipTX;
				num8 += this.clipTY;
			}
			Rect r;
			r..ctor(x, y, (float)image.w, (float)image.h);
			Rect r2;
			r2..ctor(num7, num8, num9, num10);
			Rect rect = this.intersectRect(r, r2);
			if (rect.width <= 0f || rect.height <= 0f)
			{
				return;
			}
			num = rect.width;
			num2 = rect.height;
			num3 = rect.x - r.x;
			num4 = rect.y - r.y;
		}
		Graphics.DrawTexture(new Rect(x + num3, y + num4, num, num2), image.texture, new Rect(num3 / (float)image.texture.width, ((float)image.texture.height - num2 - num4) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0000564E File Offset: 0x00003A4E
	public void reset()
	{
		this.isClip = false;
		this.isTranslate = false;
		this.translateX = 0f;
		this.translateY = 0f;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00005674 File Offset: 0x00003A74
	public Rect intersectRect(Rect r1, Rect r2)
	{
		float num = r1.x;
		float num2 = r1.y;
		float x = r2.x;
		float y = r2.y;
		float num3 = num;
		num3 += r1.width;
		float num4 = num2;
		num4 += r1.height;
		float num5 = x;
		num5 += r2.width;
		float num6 = y;
		num6 += r2.height;
		if (num < x)
		{
			num = x;
		}
		if (num2 < y)
		{
			num2 = y;
		}
		if (num3 > num5)
		{
			num3 = num5;
		}
		if (num4 > num6)
		{
			num4 = num6;
		}
		num3 -= num;
		num4 -= num2;
		if (num3 < -30000f)
		{
			num3 = -30000f;
		}
		if (num4 < -30000f)
		{
			num4 = -30000f;
		}
		return new Rect(num, num2, (float)((int)num3), (float)((int)num4));
	}

	// Token: 0x06000071 RID: 113 RVA: 0x0000574C File Offset: 0x00003B4C
	public void drawImageScaleClip(Image image, float x, float y, float w, float h, int tranform)
	{
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		float num = w;
		float num2 = h;
		float num3 = 0f;
		float num4 = 0f;
		if (this.isClip)
		{
			float num5 = this.clipX;
			float num6 = this.clipY;
			float num7 = this.clipW;
			float num8 = this.clipH;
			if (this.isTranslate)
			{
				num5 += this.clipTX;
				num6 += this.clipTY;
			}
			Rect r;
			r..ctor(x, y, w, h);
			Rect r2;
			r2..ctor(num5, num6, num7, num8);
			Rect rect = this.intersectRect(r, r2);
			if (rect.width <= 0f || rect.height <= 0f)
			{
				return;
			}
			num = rect.width;
			num2 = rect.height;
			num3 = rect.x - r.x;
			num4 = rect.y - r.y;
		}
		Graphics.DrawTexture(new Rect(x + num3, y + num4, num, num2), image.texture, new Rect(num3 / (float)image.texture.width, ((float)image.texture.height - num2 - num4) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000058E7 File Offset: 0x00003CE7
	public void drawImageScale(Image image, int x, int y, int w, int h, int tranform)
	{
		Graphics.DrawTexture(new Rect((float)x + this.translateX, (float)y + this.translateY, (float)((tranform != 0) ? (-(float)w) : w), (float)h), image.texture);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00005920 File Offset: 0x00003D20
	public void drawImageSimple(Image image, int x, int y)
	{
		Graphics.DrawTexture(new Rect((float)x, (float)y, (float)image.w, (float)image.h), image.texture);
	}

	// Token: 0x04000049 RID: 73
	public static int HCENTER = 1;

	// Token: 0x0400004A RID: 74
	public static int VCENTER = 2;

	// Token: 0x0400004B RID: 75
	public static int LEFT = 4;

	// Token: 0x0400004C RID: 76
	public static int RIGHT = 8;

	// Token: 0x0400004D RID: 77
	public static int TOP = 16;

	// Token: 0x0400004E RID: 78
	public static int BOTTOM = 32;

	// Token: 0x0400004F RID: 79
	private float r;

	// Token: 0x04000050 RID: 80
	private float g;

	// Token: 0x04000051 RID: 81
	private float b;

	// Token: 0x04000052 RID: 82
	public float clipX;

	// Token: 0x04000053 RID: 83
	public float clipY;

	// Token: 0x04000054 RID: 84
	public float clipW;

	// Token: 0x04000055 RID: 85
	public float clipH;

	// Token: 0x04000056 RID: 86
	private bool isClip;

	// Token: 0x04000057 RID: 87
	private bool isTranslate = true;

	// Token: 0x04000058 RID: 88
	private float translateX;

	// Token: 0x04000059 RID: 89
	private float translateY;

	// Token: 0x0400005A RID: 90
	private float areaTexture;

	// Token: 0x0400005B RID: 91
	public static Hashtable cachedTextures = new Hashtable();

	// Token: 0x0400005C RID: 92
	private float clipTX;

	// Token: 0x0400005D RID: 93
	private float clipTY;

	// Token: 0x0400005E RID: 94
	private int currentBGColor;
}
