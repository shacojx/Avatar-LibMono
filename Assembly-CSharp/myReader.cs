using System;
using System.Text;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class myReader
{
	// Token: 0x060000D8 RID: 216 RVA: 0x00003BC6 File Offset: 0x00001FC6
	public myReader()
	{
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00003BCE File Offset: 0x00001FCE
	public myReader(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00003BE0 File Offset: 0x00001FE0
	public myReader(string filename)
	{
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		this.buffer = mSystem.convertToSbyte(textAsset.bytes);
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00003C1C File Offset: 0x0000201C
	public sbyte readSByte()
	{
		if (this.posRead < this.buffer.Length)
		{
			return this.buffer[this.posRead++];
		}
		this.posRead = this.buffer.Length;
		return 0;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00003C64 File Offset: 0x00002064
	public sbyte readsbyte()
	{
		return this.readSByte();
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00003C6C File Offset: 0x0000206C
	public sbyte readByte()
	{
		return this.readSByte();
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00003C74 File Offset: 0x00002074
	public void mark(int readlimit)
	{
		this.posMark = this.posRead;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00003C82 File Offset: 0x00002082
	public void reset()
	{
		this.posRead = this.posMark;
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00003C90 File Offset: 0x00002090
	public byte readUnsignedByte()
	{
		return myReader.convertSbyteToByte(this.readSByte());
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00003CA0 File Offset: 0x000020A0
	public short readShort()
	{
		short num = 0;
		for (int i = 0; i < 2; i++)
		{
			num = (short)(num << 8);
			num |= (short)(255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00003CEC File Offset: 0x000020EC
	public ushort readUnsignedShort()
	{
		ushort num = 0;
		for (int i = 0; i < 2; i++)
		{
			num = (ushort)(num << 8);
			num |= (ushort)(255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00003D38 File Offset: 0x00002138
	public int readInt()
	{
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			num <<= 8;
			num |= (255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00003D80 File Offset: 0x00002180
	public long readLong()
	{
		long num = 0L;
		for (int i = 0; i < 8; i++)
		{
			num <<= 8;
			num |= (long)(255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00003DCA File Offset: 0x000021CA
	public bool readBool()
	{
		return (int)this.readSByte() > 0;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00003DE0 File Offset: 0x000021E0
	public bool readBoolean()
	{
		return (int)this.readSByte() > 0;
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00003DF8 File Offset: 0x000021F8
	public string readString()
	{
		short num = this.readShort();
		byte[] array = new byte[(int)num];
		for (int i = 0; i < (int)num; i++)
		{
			array[i] = myReader.convertSbyteToByte(this.readSByte());
		}
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		return utf8Encoding.GetString(array);
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00003E40 File Offset: 0x00002240
	public string readStringUTF()
	{
		short num = this.readShort();
		byte[] array = new byte[(int)num];
		for (int i = 0; i < (int)num; i++)
		{
			array[i] = myReader.convertSbyteToByte(this.readSByte());
		}
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		return utf8Encoding.GetString(array);
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00003E88 File Offset: 0x00002288
	public string readUTF()
	{
		return this.readStringUTF();
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00003E90 File Offset: 0x00002290
	public int read()
	{
		if (this.posRead < this.buffer.Length)
		{
			return (int)this.readSByte();
		}
		return -1;
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00003EB0 File Offset: 0x000022B0
	public int read(ref sbyte[] data)
	{
		if (data == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = this.readSByte();
			if (this.posRead > this.buffer.Length)
			{
				return -1;
			}
			num++;
		}
		return num;
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00003F04 File Offset: 0x00002304
	public void readFully(ref sbyte[] data)
	{
		if (data == null || data.Length + this.posRead > this.buffer.Length)
		{
			return;
		}
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = this.readSByte();
		}
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00003F50 File Offset: 0x00002350
	public int available()
	{
		return this.buffer.Length - this.posRead;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00003F61 File Offset: 0x00002361
	public static byte convertSbyteToByte(sbyte var)
	{
		if ((int)var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00003F78 File Offset: 0x00002378
	public static byte[] convertSbyteToByte(sbyte[] var)
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

	// Token: 0x060000F0 RID: 240 RVA: 0x00003FC7 File Offset: 0x000023C7
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00003FD0 File Offset: 0x000023D0
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00003FDC File Offset: 0x000023DC
	public void read(ref sbyte[] data, int arg1, int arg2)
	{
		if (data == null)
		{
			return;
		}
		for (int i = 0; i < arg2; i++)
		{
			data[i + arg1] = this.readSByte();
			if (this.posRead > this.buffer.Length)
			{
				return;
			}
		}
	}

	// Token: 0x0400008D RID: 141
	public sbyte[] buffer;

	// Token: 0x0400008E RID: 142
	private int posRead;

	// Token: 0x0400008F RID: 143
	private int posMark;

	// Token: 0x04000090 RID: 144
	private static string fileName;

	// Token: 0x04000091 RID: 145
	private static int status;
}
