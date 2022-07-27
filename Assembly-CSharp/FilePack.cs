using System;
using System.Collections;
using System.IO;

// Token: 0x02000109 RID: 265
public class FilePack
{
	// Token: 0x06000734 RID: 1844 RVA: 0x0004216C File Offset: 0x0004056C
	public FilePack(string name)
	{
		this.codeLen = this.code.Length;
		int num = 0;
		int num2 = 0;
		this.name = name;
		this.hSize = 0;
		this.open();
		try
		{
			this.nFile = this.encode(this.file.readUnsignedByte());
			this.hSize++;
			this.fname = new string[this.nFile];
			this.fpos = new int[this.nFile];
			this.flen = new int[this.nFile];
			for (int i = 0; i < this.nFile; i++)
			{
				int num3 = this.encode((int)this.file.readByte());
				sbyte[] array = new sbyte[num3];
				this.file.read(ref array);
				this.encode(array);
				this.fname[i] = new string(ArrayCast.ToCharArray(array));
				this.fpos[i] = num;
				this.flen[i] = this.encode(this.file.readUnsignedShort());
				num += this.flen[i];
				num2 += this.flen[i];
				this.hSize += num3 + 3;
			}
			this.fullData = new sbyte[num2];
			this.file.readFully(ref this.fullData);
			this.encode(this.fullData);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
		this.close();
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0004231C File Offset: 0x0004071C
	public static void reset()
	{
		FilePack.instance.close();
		FilePack.instance = null;
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0004232E File Offset: 0x0004072E
	public static Image getImg(string path)
	{
		return FilePack.instance.loadImage(path + string.Empty);
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00042345 File Offset: 0x00040745
	public static Image getImgHome(string path)
	{
		return FilePack.instanceHome.loadImage(path + string.Empty);
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0004235C File Offset: 0x0004075C
	public static void init(string path)
	{
		FilePack filePack = (FilePack)FilePack.cachedFilePack[path];
		if (filePack != null)
		{
			FilePack.instance = filePack;
			return;
		}
		FilePack.instance = new FilePack(T.getPath() + path);
		FilePack.cachedFilePack.Add(path, FilePack.instance);
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x000423AC File Offset: 0x000407AC
	public static void initHome(string path)
	{
		FilePack.instanceHome = new FilePack(T.getPath() + path);
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x000423C3 File Offset: 0x000407C3
	private int encode(int i)
	{
		return i;
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x000423C8 File Offset: 0x000407C8
	private void encode(sbyte[] bytes)
	{
		int num = bytes.Length;
		for (int i = 0; i < num; i++)
		{
			bytes[i] = (sbyte)((int)bytes[i] ^ (int)this.code[i % this.codeLen]);
		}
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x00042404 File Offset: 0x00040804
	private void open()
	{
		this.file = DataInputStream.getResourceAsStream(this.name);
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x00042418 File Offset: 0x00040818
	public void close()
	{
		try
		{
			if (this.file != null)
			{
				this.file.close();
			}
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x0004245C File Offset: 0x0004085C
	public sbyte[] loadFile(string fileName)
	{
		for (int i = 0; i < this.nFile; i++)
		{
			if (this.fname[i].CompareTo(fileName) == 0)
			{
				sbyte[] array = new sbyte[this.flen[i]];
				Array.Copy(this.fullData, this.fpos[i], array, 0, this.flen[i]);
				return array;
			}
		}
		throw new Exception("File '" + fileName + "' not found!");
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x000424D8 File Offset: 0x000408D8
	public Image loadImage(string fileName)
	{
		fileName += ".png";
		for (int i = 0; i < this.nFile; i++)
		{
			if (this.fname[i].CompareTo(fileName) == 0)
			{
				sbyte[] array = new sbyte[this.flen[i]];
				Array.Copy(this.fullData, this.fpos[i], array, 0, this.flen[i]);
				return Image.createImage(ArrayCast.cast(array));
			}
		}
		return null;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x00042554 File Offset: 0x00040954
	public sbyte[] loadData(string name)
	{
		for (int i = 0; i < this.nFile; i++)
		{
			if (this.fname[i].CompareTo(name) == 0)
			{
				sbyte[] array = new sbyte[this.flen[i]];
				Array.Copy(this.fullData, this.fpos[i], array, 0, this.flen[i]);
				return array;
			}
		}
		return null;
	}

	// Token: 0x04000921 RID: 2337
	public static Hashtable cachedFilePack = new Hashtable();

	// Token: 0x04000922 RID: 2338
	public static FilePack instance;

	// Token: 0x04000923 RID: 2339
	public static FilePack instanceHome;

	// Token: 0x04000924 RID: 2340
	private string[] fname;

	// Token: 0x04000925 RID: 2341
	private int[] fpos;

	// Token: 0x04000926 RID: 2342
	private int[] flen;

	// Token: 0x04000927 RID: 2343
	private sbyte[] fullData;

	// Token: 0x04000928 RID: 2344
	private int nFile;

	// Token: 0x04000929 RID: 2345
	private int hSize;

	// Token: 0x0400092A RID: 2346
	private string name;

	// Token: 0x0400092B RID: 2347
	public sbyte[] code = new sbyte[]
	{
		78,
		103,
		117,
		121,
		101,
		110,
		86,
		97,
		110,
		77,
		105,
		110,
		104
	};

	// Token: 0x0400092C RID: 2348
	private int codeLen;

	// Token: 0x0400092D RID: 2349
	private DataInputStream file;
}
