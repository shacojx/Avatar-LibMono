using System;

// Token: 0x0200006E RID: 110
public class StringObj : SubObject
{
	// Token: 0x060003B2 RID: 946 RVA: 0x00021DE7 File Offset: 0x000201E7
	public StringObj()
	{
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00021DF6 File Offset: 0x000201F6
	public StringObj(string st, int ww)
	{
		this.str = st;
		this.w2 = ww;
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00021E13 File Offset: 0x00020213
	public StringObj(int x, int y, string str)
	{
		this.x = x;
		this.y = y;
		this.str = str;
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00021E37 File Offset: 0x00020237
	public void reset()
	{
		this.dir = 1;
		this.dis = 0;
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00021E47 File Offset: 0x00020247
	public void transTextLimit(int limit)
	{
		this.dis += this.dir;
		if (this.dis > this.w2 - limit)
		{
			this.dis = -20;
		}
	}

	// Token: 0x040004C9 RID: 1225
	public string str;

	// Token: 0x040004CA RID: 1226
	public string str2;

	// Token: 0x040004CB RID: 1227
	public int w2;

	// Token: 0x040004CC RID: 1228
	public int dir = 1;

	// Token: 0x040004CD RID: 1229
	public int dis;

	// Token: 0x040004CE RID: 1230
	public int anthor;
}
