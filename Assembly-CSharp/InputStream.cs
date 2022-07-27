using System;

// Token: 0x0200000A RID: 10
public class InputStream : myReader
{
	// Token: 0x06000045 RID: 69 RVA: 0x00004025 File Offset: 0x00002425
	public InputStream()
	{
	}

	// Token: 0x06000046 RID: 70 RVA: 0x0000402D File Offset: 0x0000242D
	public InputStream(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0000403C File Offset: 0x0000243C
	public InputStream(string filename) : base(filename)
	{
	}
}
