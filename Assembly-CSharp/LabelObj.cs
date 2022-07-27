using System;

// Token: 0x02000053 RID: 83
public class LabelObj
{
	// Token: 0x060002F6 RID: 758 RVA: 0x0001887D File Offset: 0x00016C7D
	public LabelObj(string str, int w, int x, int y)
	{
		this.str = Canvas.normalFont.splitFontBStrInLine(str, w);
		this.x = x;
		this.y = y;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x000188A8 File Offset: 0x00016CA8
	public void paint(MyGraphics g)
	{
		for (int i = 0; i < this.str.Length; i++)
		{
			Canvas.normalFont.drawString(g, this.str[i], this.x, this.y + 14 * i, 0);
		}
	}

	// Token: 0x0400038D RID: 909
	public int x;

	// Token: 0x0400038E RID: 910
	public int y;

	// Token: 0x0400038F RID: 911
	public string[] str;
}
