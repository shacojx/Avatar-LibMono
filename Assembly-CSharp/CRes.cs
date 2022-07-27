using System;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class CRes
{
	// Token: 0x060001DD RID: 477 RVA: 0x0000F134 File Offset: 0x0000D534
	public static void init()
	{
		CRes.coss = new short[91];
		CRes.tann = new int[91];
		for (int i = 0; i <= 90; i++)
		{
			CRes.coss[i] = CRes.sinn[90 - i];
			if (CRes.coss[i] == 0)
			{
				CRes.tann[i] = 32767;
			}
			else
			{
				CRes.tann[i] = ((int)CRes.sinn[i] << 10) / (int)CRes.coss[i];
			}
		}
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000F1B4 File Offset: 0x0000D5B4
	public static int sin(int a)
	{
		a = CRes.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return (int)CRes.sinn[a];
		}
		if (a >= 90 && a < 180)
		{
			return (int)CRes.sinn[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return (int)(-(int)CRes.sinn[a - 180]);
		}
		return (int)(-(int)CRes.sinn[360 - a]);
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000F234 File Offset: 0x0000D634
	public static int cos(int a)
	{
		a = CRes.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return (int)CRes.coss[a];
		}
		if (a >= 90 && a < 180)
		{
			return (int)(-(int)CRes.coss[180 - a]);
		}
		if (a >= 180 && a < 270)
		{
			return (int)(-(int)CRes.coss[a - 180]);
		}
		return (int)CRes.coss[360 - a];
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0000F2B4 File Offset: 0x0000D6B4
	public static int tan(int a)
	{
		a = CRes.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return CRes.tann[a];
		}
		if (a >= 90 && a < 180)
		{
			return -CRes.tann[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return CRes.tann[a - 180];
		}
		return -CRes.tann[360 - a];
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000F334 File Offset: 0x0000D734
	public static int atan(int a)
	{
		for (int i = 0; i <= 90; i++)
		{
			if (CRes.tann[i] >= a)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000F364 File Offset: 0x0000D764
	public static int angle(int dx, int dy)
	{
		int num;
		if (dx != 0)
		{
			int a = global::Math.abs((dy << 10) / dx);
			num = CRes.atan(a);
			if (dy >= 0 && dx < 0)
			{
				num = 180 - num;
			}
			if (dy < 0 && dx < 0)
			{
				num = 180 + num;
			}
			if (dy < 0 && dx >= 0)
			{
				num = 360 - num;
			}
		}
		else
		{
			num = ((dy <= 0) ? 270 : 90);
		}
		return num;
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000F3E8 File Offset: 0x0000D7E8
	public static int myAngle(int dx, int dy)
	{
		int num = CRes.angle(dx, dy);
		if (num >= 315)
		{
			num = 360 - num;
		}
		return num;
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0000F411 File Offset: 0x0000D811
	public static int fixangle(int angle)
	{
		if (angle >= 360)
		{
			angle -= 360;
		}
		if (angle < 0)
		{
			angle += 360;
		}
		return angle;
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0000F438 File Offset: 0x0000D838
	public static int subangle(int a1, int a2)
	{
		int num = a2 - a1;
		if (num < -180)
		{
			return num + 360;
		}
		if (num > 180)
		{
			return num - 360;
		}
		return num;
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x0000F470 File Offset: 0x0000D870
	public static int random(int a)
	{
		return CRes.r.nextInt() % a;
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000F47E File Offset: 0x0000D87E
	public static int rnd(int a)
	{
		return CRes.r.nextInt(a);
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0000F48C File Offset: 0x0000D88C
	public static int rnd(int a, int b)
	{
		if (CRes.r.nextInt(2) == 0)
		{
			return a;
		}
		return b;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000F4AE File Offset: 0x0000D8AE
	public static int abs(int a)
	{
		return (a < 0) ? (-a) : a;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000F4BF File Offset: 0x0000D8BF
	public static bool isHit(int x, int y, int w, int h, int tX, int tY, int tW, int tH)
	{
		return x + w >= tX && x <= tX + tW && y + h >= tY && y <= tY + tH;
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000F4F0 File Offset: 0x0000D8F0
	public static int sqrt(int a)
	{
		if (a <= 0)
		{
			return 0;
		}
		int num = (a + 1) / 2;
		int num2;
		do
		{
			num2 = num;
			num = num / 2 + a / (2 * num);
		}
		while (global::Math.abs(num2 - num) > 1);
		return num;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000F527 File Offset: 0x0000D927
	public static int distance(int x1, int y1, int x2, int y2)
	{
		return CRes.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000F53D File Offset: 0x0000D93D
	public static int byte2int(sbyte b)
	{
		return (int)b & 255;
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000F547 File Offset: 0x0000D947
	public static int getShort(int off, sbyte[] data)
	{
		return CRes.byte2int(data[off]) << 8 | CRes.byte2int(data[off + 1]);
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000F560 File Offset: 0x0000D960
	public static void saveRMSInt(string file, int x)
	{
		try
		{
			RMS.saveRMS(file, new sbyte[]
			{
				(sbyte)x
			});
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000F5A0 File Offset: 0x0000D9A0
	public static short byte2short(sbyte[] data)
	{
		short num = (short)data[1];
		return (short)((int)num << 8 | (int)((byte)data[0]));
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000F5C0 File Offset: 0x0000D9C0
	public static sbyte[] encoding(sbyte[] array)
	{
		if (array != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (sbyte)(~(sbyte)((int)array[i]));
			}
		}
		return array;
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000F5F4 File Offset: 0x0000D9F4
	public static int loadRMSInt(string file)
	{
		sbyte[] array = RMS.loadRMS(file);
		return (array != null) ? ((int)array[0]) : -1;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000F618 File Offset: 0x0000DA18
	public static Image createImgByByteArray(byte[] array)
	{
		return Image.createImage(array);
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0000F620 File Offset: 0x0000DA20
	public static void rndaaa()
	{
		LoginScr.aa = 1;
		for (int i = 0; i < FarmScr.gI().listLeftMenu.size(); i++)
		{
			Command command = (Command)FarmScr.gI().listLeftMenu.elementAt(i);
			LoginScr.aa += command.caption.GetHashCode();
		}
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000F680 File Offset: 0x0000DA80
	public static Image createImgByHeader(sbyte[] header, sbyte[] data)
	{
		sbyte[] array = new sbyte[header.Length + data.Length];
		Array.Copy(header, 0, array, 0, header.Length);
		Array.Copy(data, 0, array, header.Length, data.Length);
		return CRes.createImgByByteArray(ArrayCast.cast(array));
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000F6BF File Offset: 0x0000DABF
	public static Image createImgByImg(int x, int y, int w, int h, Image img)
	{
		return Image.createImage(img, x, y, w, h, 0);
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000F6D0 File Offset: 0x0000DAD0
	public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
	{
		Texture2D texture2D = new Texture2D(targetWidth, targetHeight, source.format, true);
		Color[] pixels = texture2D.GetPixels(0);
		float num = 1f / (float)source.width * ((float)source.width / (float)targetWidth);
		float num2 = 1f / (float)source.height * ((float)source.height / (float)targetHeight);
		for (int i = 0; i < pixels.Length; i++)
		{
			pixels[i] = source.GetPixelBilinear(num * ((float)i % (float)targetWidth), num2 * Mathf.Floor((float)(i / targetWidth)));
		}
		texture2D.SetPixels(pixels, 0);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x0400022A RID: 554
	public static MyRandom r = new MyRandom();

	// Token: 0x0400022B RID: 555
	private static short[] sinn = new short[]
	{
		0,
		18,
		36,
		54,
		71,
		89,
		107,
		125,
		143,
		160,
		178,
		195,
		213,
		230,
		248,
		265,
		282,
		299,
		316,
		333,
		350,
		367,
		384,
		400,
		416,
		433,
		449,
		465,
		481,
		496,
		512,
		527,
		543,
		558,
		573,
		587,
		602,
		616,
		630,
		644,
		658,
		672,
		685,
		698,
		711,
		724,
		737,
		749,
		761,
		773,
		784,
		796,
		807,
		818,
		828,
		839,
		849,
		859,
		868,
		878,
		887,
		896,
		904,
		912,
		920,
		928,
		935,
		943,
		949,
		956,
		962,
		968,
		974,
		979,
		984,
		989,
		994,
		998,
		1002,
		1005,
		1008,
		1011,
		1014,
		1016,
		1018,
		1020,
		1022,
		1023,
		1023,
		1024,
		1024
	};

	// Token: 0x0400022C RID: 556
	private static short[] coss;

	// Token: 0x0400022D RID: 557
	private static int[] tann;
}
