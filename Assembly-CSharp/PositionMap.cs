using System;

// Token: 0x02000068 RID: 104
public class PositionMap
{
	// Token: 0x060003A1 RID: 929 RVA: 0x00021521 File Offset: 0x0001F921
	public PositionMap()
	{
		this.count = CRes.rnd(9);
	}

	// Token: 0x0400049C RID: 1180
	public int x;

	// Token: 0x0400049D RID: 1181
	public int y;

	// Token: 0x0400049E RID: 1182
	public sbyte id;

	// Token: 0x0400049F RID: 1183
	public string text;

	// Token: 0x040004A0 RID: 1184
	public short idImg;

	// Token: 0x040004A1 RID: 1185
	public short price;

	// Token: 0x040004A2 RID: 1186
	public sbyte typeMoney;

	// Token: 0x040004A3 RID: 1187
	public int count;
}
