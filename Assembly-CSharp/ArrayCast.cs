using System;

// Token: 0x02000003 RID: 3
public class ArrayCast
{
	// Token: 0x06000009 RID: 9 RVA: 0x0000222C File Offset: 0x0000062C
	public static sbyte[] cast(byte[] data)
	{
		sbyte[] array = new sbyte[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (sbyte)data[i];
		}
		return array;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002260 File Offset: 0x00000660
	public static byte[] cast(sbyte[] data)
	{
		byte[] array = new byte[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (byte)data[i];
		}
		return array;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002294 File Offset: 0x00000694
	public static char[] ToCharArray(sbyte[] data)
	{
		char[] array = new char[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (char)data[i];
		}
		return array;
	}
}
