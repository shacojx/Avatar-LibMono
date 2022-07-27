using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class DataInputStream
{
	// Token: 0x0600000C RID: 12 RVA: 0x000022C8 File Offset: 0x000006C8
	public DataInputStream(string filename)
	{
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		this.r = new myReader(ArrayCast.cast(textAsset.bytes));
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002307 File Offset: 0x00000707
	public DataInputStream(sbyte[] data)
	{
		this.r = new myReader(data);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000231B File Offset: 0x0000071B
	public static void update()
	{
		if (DataInputStream.status == 2)
		{
			DataInputStream.status = 1;
			DataInputStream.istemp = DataInputStream.__getResourceAsStream(DataInputStream.filenametemp);
			DataInputStream.status = 0;
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002343 File Offset: 0x00000743
	public static DataInputStream getResourceAsStream(string filename)
	{
		return DataInputStream.__getResourceAsStream(filename);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000234C File Offset: 0x0000074C
	private static DataInputStream _getResourceAsStream(string filename)
	{
		if (DataInputStream.status != 0)
		{
			for (int i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				if (DataInputStream.status == 0)
				{
					break;
				}
			}
			if (DataInputStream.status != 0)
			{
				Debug.LogError("CANNOT GET INPUTSTREAM " + filename + " WHEN GETTING " + DataInputStream.filenametemp);
				return null;
			}
		}
		DataInputStream.istemp = null;
		DataInputStream.filenametemp = filename;
		DataInputStream.status = 2;
		int j;
		for (j = 0; j < 500; j++)
		{
			Thread.Sleep(5);
			if (DataInputStream.status == 0)
			{
				break;
			}
		}
		if (j == 500)
		{
			Debug.LogError("TOO LONG FOR CREATE INPUTSTREAM " + filename);
			DataInputStream.status = 0;
			return null;
		}
		return DataInputStream.istemp;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002419 File Offset: 0x00000819
	private static DataInputStream __getResourceAsStream(string filename)
	{
		return new DataInputStream(filename);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002421 File Offset: 0x00000821
	public short readShort()
	{
		return this.r.readShort();
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000242E File Offset: 0x0000082E
	public int readInt()
	{
		return this.r.readInt();
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000243B File Offset: 0x0000083B
	public void read(ref sbyte[] data)
	{
		this.r.read(ref data);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000244A File Offset: 0x0000084A
	public void close()
	{
		this.r.Close();
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002457 File Offset: 0x00000857
	public void Close()
	{
		this.r.Close();
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002464 File Offset: 0x00000864
	public string readUTF()
	{
		return this.r.readUTF();
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002471 File Offset: 0x00000871
	public sbyte readByte()
	{
		return this.r.readByte();
	}

	// Token: 0x06000019 RID: 25 RVA: 0x0000247E File Offset: 0x0000087E
	public bool readBoolean()
	{
		return this.r.readBoolean();
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000248B File Offset: 0x0000088B
	public int readUnsignedByte()
	{
		return (int)((byte)this.r.readByte());
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002499 File Offset: 0x00000899
	public int readUnsignedShort()
	{
		return (int)this.r.readUnsignedShort();
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000024A6 File Offset: 0x000008A6
	public void readFully(ref sbyte[] data)
	{
		this.r.read(ref data);
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000024B5 File Offset: 0x000008B5
	public int available()
	{
		return this.r.available();
	}

	// Token: 0x04000010 RID: 16
	private myReader r;

	// Token: 0x04000011 RID: 17
	private const int INTERVAL = 5;

	// Token: 0x04000012 RID: 18
	private const int MAXTIME = 500;

	// Token: 0x04000013 RID: 19
	public static DataInputStream istemp;

	// Token: 0x04000014 RID: 20
	private static int status;

	// Token: 0x04000015 RID: 21
	private static string filenametemp;
}
