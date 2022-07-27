using System;
using System.Text;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class mSystem
{
	// Token: 0x060000CE RID: 206 RVA: 0x00006F22 File Offset: 0x00005322
	public static void arraycopy(sbyte[] scr, int scrPos, sbyte[] dest, int destPos, int lenght)
	{
		Array.Copy(scr, scrPos, dest, destPos, lenght);
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00006F30 File Offset: 0x00005330
	public static void arrayReplace(sbyte[] scr, int scrPos, ref sbyte[] dest, int destPos, int lenght)
	{
		if (scr == null || dest == null || scrPos + lenght > scr.Length)
		{
			return;
		}
		sbyte[] array = new sbyte[dest.Length + lenght];
		for (int i = 0; i < destPos; i++)
		{
			array[i] = dest[i];
		}
		for (int j = destPos; j < destPos + lenght; j++)
		{
			array[j] = scr[scrPos + j - destPos];
		}
		for (int k = destPos + lenght; k < array.Length; k++)
		{
			array[k] = dest[destPos + k - lenght];
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00006FC0 File Offset: 0x000053C0
	public static sbyte[] convertToSbyte(byte[] scr)
	{
		sbyte[] array = new sbyte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (sbyte)scr[i];
		}
		return array;
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00006FF4 File Offset: 0x000053F4
	public static sbyte[] convertToSbyte(string scr)
	{
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		byte[] bytes = asciiencoding.GetBytes(scr);
		return mSystem.convertToSbyte(bytes);
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00007018 File Offset: 0x00005418
	public static byte[] convetToByte(sbyte[] scr)
	{
		byte[] array = new byte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			if ((int)scr[i] > 0)
			{
				array[i] = (byte)scr[i];
			}
			else
			{
				array[i] = (byte)((int)scr[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00007068 File Offset: 0x00005468
	public static char[] ToCharArray(sbyte[] scr)
	{
		char[] array = new char[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (char)scr[i];
		}
		return array;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x0000709A File Offset: 0x0000549A
	public static long currentTimeMillis()
	{
		return (long)Environment.TickCount;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x000070A4 File Offset: 0x000054A4
	public static int currentHour()
	{
		return DateTime.Now.Hour;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x000070BE File Offset: 0x000054BE
	public static void println(object str)
	{
		Debug.Log(str);
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x000070C6 File Offset: 0x000054C6
	public static void gc()
	{
		GC.Collect();
	}
}
