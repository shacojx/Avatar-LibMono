using System;

// Token: 0x0200000C RID: 12
public class Math
{
	// Token: 0x06000057 RID: 87 RVA: 0x000045FB File Offset: 0x000029FB
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000460C File Offset: 0x00002A0C
	public static int min(int x, int y)
	{
		return (x >= y) ? y : x;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000461C File Offset: 0x00002A1C
	public static int max(int x, int y)
	{
		return (x <= y) ? y : x;
	}
}
