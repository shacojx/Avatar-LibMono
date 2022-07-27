using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class RMS
{
	// Token: 0x0600009E RID: 158 RVA: 0x000063E4 File Offset: 0x000047E4
	public static void saveRMS(string filename, sbyte[] data)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			RMS.__saveRMS(filename, data);
		}
		else
		{
			RMS._saveRMS(filename, data);
		}
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00006412 File Offset: 0x00004812
	public static sbyte[] loadRMS(string filename)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			return RMS.__loadRMS(filename);
		}
		return RMS._loadRMS(filename);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x0000643C File Offset: 0x0000483C
	private static void _saveRMS(string filename, sbyte[] data)
	{
		if (RMS.status != 0)
		{
			Debug.LogError("Cannot save RMS " + filename + " because current is saving " + RMS.filename);
			return;
		}
		RMS.filename = filename;
		RMS.data = data;
		RMS.status = 2;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (RMS.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG TO SAVE RMS " + filename);
		}
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x000064C8 File Offset: 0x000048C8
	private static sbyte[] _loadRMS(string filename)
	{
		if (RMS.status != 0)
		{
			Debug.LogError("Cannot load RMS " + filename + " because current is loading " + RMS.filename);
			return null;
		}
		RMS.filename = filename;
		RMS.data = null;
		RMS.status = 3;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (RMS.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG TO LOAD RMS " + filename);
		}
		return RMS.data;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00006558 File Offset: 0x00004958
	public static void update()
	{
		if (RMS.status == 2)
		{
			RMS.status = 1;
			RMS.__saveRMS(RMS.filename, RMS.data);
			RMS.status = 0;
		}
		else if (RMS.status == 3)
		{
			RMS.status = 1;
			RMS.data = RMS.__loadRMS(RMS.filename);
			RMS.status = 0;
		}
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000065B8 File Offset: 0x000049B8
	private static void __saveRMS(string filename, sbyte[] data)
	{
		string text = RMS.ByteArrayToString(ArrayCast.cast(data));
		PlayerPrefs.SetString(filename, text);
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000065D8 File Offset: 0x000049D8
	private static sbyte[] __loadRMS(string filename)
	{
		string @string = PlayerPrefs.GetString(filename);
		byte[] array;
		try
		{
			array = RMS.StringToByteArray(@string);
		}
		catch (Exception ex)
		{
			Debug.LogError("PARSE RMS STRING FAIL " + ex.StackTrace);
			return null;
		}
		if (array.Length == 0)
		{
			return null;
		}
		return ArrayCast.cast(array);
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00006638 File Offset: 0x00004A38
	public static void deleteAll()
	{
		Debug.LogWarning("ALL RMS CLEAR");
		PlayerPrefs.DeleteAll();
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000664C File Offset: 0x00004A4C
	public static string ByteArrayToString(byte[] ba)
	{
		string text = BitConverter.ToString(ba);
		return text.Replace("-", string.Empty);
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00006670 File Offset: 0x00004A70
	public static byte[] StringToByteArray(string hex)
	{
		int length = hex.Length;
		byte[] array = new byte[length / 2];
		for (int i = 0; i < length; i += 2)
		{
			array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		}
		return array;
	}

	// Token: 0x0400006D RID: 109
	public static int status;

	// Token: 0x0400006E RID: 110
	public static sbyte[] data;

	// Token: 0x0400006F RID: 111
	public static string filename;

	// Token: 0x04000070 RID: 112
	private const int INTERVAL = 5;

	// Token: 0x04000071 RID: 113
	private const int MAXTIME = 500;
}
