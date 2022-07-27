using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class Image
{
	// Token: 0x0600002F RID: 47 RVA: 0x00003508 File Offset: 0x00001908
	public static Image createEmptyImage()
	{
		return Image.__createEmptyImage();
	}

	// Token: 0x06000030 RID: 48 RVA: 0x0000350F File Offset: 0x0000190F
	public static Image createImage(string filename)
	{
		return Image.__createImage(filename);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00003517 File Offset: 0x00001917
	public static Image createImagePNG(string filename)
	{
		return Image.__createImage_PNG(filename);
	}

	// Token: 0x06000032 RID: 50 RVA: 0x0000351F File Offset: 0x0000191F
	public static Image createImage(byte[] imageData)
	{
		return Image.__createImage(imageData);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00003527 File Offset: 0x00001927
	public static Image createImage(Image src, int x, int y, int w, int h, int transform)
	{
		return Image.__createImage(src, x, y, w, h, transform);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00003536 File Offset: 0x00001936
	public static Image createImage(int w, int h)
	{
		return Image.__createImage(w, h);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003540 File Offset: 0x00001940
	public static void update()
	{
		if (Image.status == 2)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createEmptyImage();
			Image.status = 0;
		}
		else if (Image.status == 3)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.filenametemp);
			Image.status = 0;
		}
		else if (Image.status == 4)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.datatemp);
			Image.status = 0;
		}
		else if (Image.status == 5)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.imgSrcTemp, Image.xtemp, Image.ytemp, Image.wtemp, Image.htemp, Image.transformtemp);
			Image.status = 0;
		}
		else if (Image.status == 6)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.wtemp, Image.htemp);
			Image.status = 0;
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00003638 File Offset: 0x00001A38
	private static Image _createEmptyImage()
	{
		if (Image.status != 0)
		{
			Debug.LogError("CANNOT CREATE EMPTY IMAGE WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.status = 2;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG FOR CREATE EMPTY IMAGE");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x000036B4 File Offset: 0x00001AB4
	private static Image _createImage(string filename)
	{
		if (Image.status != 0)
		{
			Debug.LogError("CANNOT CREATE IMAGE " + filename + " WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.filenametemp = filename;
		Image.status = 3;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG FOR CREATE IMAGE " + filename);
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00003748 File Offset: 0x00001B48
	private static Image _createImage(byte[] imageData)
	{
		if (Image.status != 0)
		{
			Debug.LogError("CANNOT CREATE IMAGE(FromArray) WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.datatemp = imageData;
		Image.status = 4;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG FOR CREATE IMAGE(FromArray)");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000037C8 File Offset: 0x00001BC8
	private static Image _createImage(Image src, int x, int y, int w, int h, int transform)
	{
		if (Image.status != 0)
		{
			Debug.LogError("CANNOT CREATE IMAGE(FromSrcPart) WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.imgSrcTemp = src;
		Image.xtemp = x;
		Image.ytemp = y;
		Image.wtemp = w;
		Image.htemp = h;
		Image.transformtemp = transform;
		Image.status = 5;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG FOR CREATE IMAGE(FromSrcPart)");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00003868 File Offset: 0x00001C68
	private static Image _createImage(int w, int h)
	{
		if (Image.status != 0)
		{
			Debug.LogError("CANNOT CREATE IMAGE(w,h) WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.wtemp = w;
		Image.htemp = h;
		Image.status = 6;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG FOR CREATE IMAGE(w,h)");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000038F0 File Offset: 0x00001CF0
	private static Image __createImage(string filename)
	{
		Image image = new Image();
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		if (textAsset == null || textAsset.bytes == null || textAsset.bytes.Length == 0)
		{
			Out.println("Create Image " + filename + " fail");
			return null;
		}
		image.texture.LoadImage(textAsset.bytes);
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00003990 File Offset: 0x00001D90
	private static Image __createImage_PNG(string filename)
	{
		Image image = new Image();
		image.texture = (Texture2D)Resources.Load(filename);
		if (image.texture == null)
		{
			Out.println("Create Image PNG " + filename + " fail");
			return null;
		}
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00003A08 File Offset: 0x00001E08
	private static Image __createImage(byte[] imageData)
	{
		if (imageData == null || imageData.Length == 0)
		{
			Debug.LogError("Create Image from byte array fail");
			return null;
		}
		Image image = new Image();
		try
		{
			image.texture.LoadImage(imageData);
			image.w = image.texture.width;
			image.h = image.texture.height;
			Image.setTextureQuality(image);
		}
		catch (Exception ex)
		{
			Debug.LogError("CREAT IMAGE FROM ARRAY FAIL \n" + Environment.StackTrace);
		}
		return image;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003A9C File Offset: 0x00001E9C
	private static Image __createImage(Image src, int x, int y, int w, int h, int transform)
	{
		Image image = new Image();
		image.texture = new Texture2D(w, h);
		y = src.texture.height - y - h;
		for (int i = 0; i < w; i++)
		{
			for (int j = 0; j < h; j++)
			{
				int num = i;
				if (transform == 2)
				{
					num = w - i;
				}
				int num2 = j;
				image.texture.SetPixel(i, j, src.texture.GetPixel(x + num, y + num2));
			}
		}
		image.texture.Apply();
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003B58 File Offset: 0x00001F58
	private static Image __createEmptyImage()
	{
		return new Image();
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003B60 File Offset: 0x00001F60
	public static Image __createImage(int w, int h)
	{
		Image image = new Image();
		image.texture = new Texture2D(w, h);
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00003B87 File Offset: 0x00001F87
	public int getWidth()
	{
		return this.w;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003B8F File Offset: 0x00001F8F
	public int getHeight()
	{
		return this.h;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003B97 File Offset: 0x00001F97
	private static void setTextureQuality(Image img)
	{
		Image.setTextureQuality(img.texture);
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003BA4 File Offset: 0x00001FA4
	private static void setTextureQuality(Texture2D texture)
	{
		texture.anisoLevel = 0;
		texture.filterMode = 0;
		texture.mipMapBias = 0f;
		texture.wrapMode = 1;
	}

	// Token: 0x04000026 RID: 38
	private const int INTERVAL = 5;

	// Token: 0x04000027 RID: 39
	private const int MAXTIME = 500;

	// Token: 0x04000028 RID: 40
	public Texture2D texture = new Texture2D(1, 1);

	// Token: 0x04000029 RID: 41
	public static Image imgTemp;

	// Token: 0x0400002A RID: 42
	public static string filenametemp;

	// Token: 0x0400002B RID: 43
	public static byte[] datatemp;

	// Token: 0x0400002C RID: 44
	public static Image imgSrcTemp;

	// Token: 0x0400002D RID: 45
	public static int xtemp;

	// Token: 0x0400002E RID: 46
	public static int ytemp;

	// Token: 0x0400002F RID: 47
	public static int wtemp;

	// Token: 0x04000030 RID: 48
	public static int htemp;

	// Token: 0x04000031 RID: 49
	public static int transformtemp;

	// Token: 0x04000032 RID: 50
	public int w;

	// Token: 0x04000033 RID: 51
	public int h;

	// Token: 0x04000034 RID: 52
	public static int status;
}
