using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class myWriter
{
	// Token: 0x060000F5 RID: 245 RVA: 0x000024E8 File Offset: 0x000008E8
	public void writeSByte(sbyte value)
	{
		this.checkLenght(0);
		this.buffer[this.posWrite++] = value;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00002518 File Offset: 0x00000918
	public void writeSByteUncheck(sbyte value)
	{
		this.buffer[this.posWrite++] = value;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0000253E File Offset: 0x0000093E
	public void writeByte(sbyte value)
	{
		this.writeSByte(value);
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00002547 File Offset: 0x00000947
	public void writeByte(int value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00002551 File Offset: 0x00000951
	public void writeUnsignedByte(byte value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x0000255C File Offset: 0x0000095C
	public void writeUnsignedByte(byte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck((sbyte)value[i]);
		}
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00002590 File Offset: 0x00000990
	public void writeSByte(sbyte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck(value[i]);
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x000025C4 File Offset: 0x000009C4
	public void writeShort(short value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x060000FD RID: 253 RVA: 0x000025FC File Offset: 0x000009FC
	public void writeShort(int value)
	{
		this.checkLenght(2);
		short num = (short)value;
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(num >> i * 8));
		}
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00002634 File Offset: 0x00000A34
	public void writeUnsignedShort(ushort value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000266C File Offset: 0x00000A6C
	public void writeInt(int value)
	{
		this.checkLenght(4);
		for (int i = 3; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x000026A4 File Offset: 0x00000AA4
	public void writeLong(long value)
	{
		this.checkLenght(8);
		for (int i = 7; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x000026D9 File Offset: 0x00000AD9
	public void writeBoolean(bool value)
	{
		this.writeSByte((!value) ? 0 : 1);
	}

	// Token: 0x06000102 RID: 258 RVA: 0x000026EE File Offset: 0x00000AEE
	public void writeBool(bool value)
	{
		this.writeSByte((!value) ? 0 : 1);
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00002704 File Offset: 0x00000B04
	public void writeString(string value)
	{
		char[] array = value.ToCharArray();
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			this.writeSByteUncheck((sbyte)array[i]);
		}
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000274C File Offset: 0x00000B4C
	public void writeUTF(string value)
	{
		Encoding unicode = Encoding.Unicode;
		Encoding encoding = Encoding.GetEncoding(65001);
		byte[] bytes = unicode.GetBytes(value);
		byte[] array = Encoding.Convert(unicode, encoding, bytes);
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		foreach (sbyte value2 in array)
		{
			this.writeSByteUncheck(value2);
		}
	}

	// Token: 0x06000105 RID: 261 RVA: 0x000027B5 File Offset: 0x00000BB5
	public void write(sbyte value)
	{
		this.writeSByte(value);
	}

	// Token: 0x06000106 RID: 262 RVA: 0x000027BE File Offset: 0x00000BBE
	public void write(sbyte[] value)
	{
		this.writeSByte(value);
	}

	// Token: 0x06000107 RID: 263 RVA: 0x000027C8 File Offset: 0x00000BC8
	public sbyte[] getData()
	{
		if (this.posWrite <= 0)
		{
			return null;
		}
		sbyte[] array = new sbyte[this.posWrite];
		for (int i = 0; i < this.posWrite; i++)
		{
			array[i] = this.buffer[i];
		}
		return array;
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00002814 File Offset: 0x00000C14
	public void checkLenght(int ltemp)
	{
		if (this.posWrite + ltemp > this.lenght)
		{
			sbyte[] array = new sbyte[this.lenght + 1024 + ltemp];
			for (int i = 0; i < this.lenght; i++)
			{
				array[i] = this.buffer[i];
			}
			this.buffer = null;
			this.buffer = array;
			this.lenght += 1024 + ltemp;
		}
	}

	// Token: 0x06000109 RID: 265 RVA: 0x0000288C File Offset: 0x00000C8C
	private static void convertString(string[] args)
	{
		string path = args[0];
		string path2 = args[1];
		using (StreamReader streamReader = new StreamReader(path, Encoding.Unicode))
		{
			using (StreamWriter streamWriter = new StreamWriter(path2, false, Encoding.UTF8))
			{
				myWriter.CopyContents(streamReader, streamWriter);
			}
		}
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00002904 File Offset: 0x00000D04
	private static void CopyContents(TextReader input, TextWriter output)
	{
		char[] array = new char[8192];
		int count;
		while ((count = input.Read(array, 0, array.Length)) != 0)
		{
			output.Write(array, 0, count);
		}
		output.Flush();
		string text = output.ToString();
		Debug.Log(text);
	}

	// Token: 0x0600010B RID: 267 RVA: 0x0000294F File Offset: 0x00000D4F
	public byte convertSbyteToByte(sbyte var)
	{
		if ((int)var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00002968 File Offset: 0x00000D68
	public byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if ((int)var[i] > 0)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x0600010D RID: 269 RVA: 0x000029B7 File Offset: 0x00000DB7
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x000029C0 File Offset: 0x00000DC0
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x04000092 RID: 146
	public sbyte[] buffer = new sbyte[2048];

	// Token: 0x04000093 RID: 147
	private int posWrite;

	// Token: 0x04000094 RID: 148
	private int lenght = 2048;
}
