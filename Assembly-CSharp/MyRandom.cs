using System;

// Token: 0x02000011 RID: 17
public class MyRandom
{
	// Token: 0x06000080 RID: 128 RVA: 0x00005ECC File Offset: 0x000042CC
	public MyRandom()
	{
		this.r = new Random();
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00005EDF File Offset: 0x000042DF
	public int nextInt()
	{
		return this.r.Next();
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00005EEC File Offset: 0x000042EC
	public int nextInt(int a)
	{
		return this.r.Next(a);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00005EFA File Offset: 0x000042FA
	public int nextInt(int a, int b)
	{
		return this.r.Next(a, b);
	}

	// Token: 0x04000061 RID: 97
	public Random r;
}
