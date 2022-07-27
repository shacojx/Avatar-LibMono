using System;

// Token: 0x020001B4 RID: 436
public class MoneyPut
{
	// Token: 0x06000BED RID: 3053 RVA: 0x000781B0 File Offset: 0x000765B0
	public MoneyPut(int x, int y, int valuea, int typePaint)
	{
		this.x = x;
		this.y = y;
		this.valuea = valuea;
		this.typePaint = typePaint;
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x000781D8 File Offset: 0x000765D8
	public void paint(MyGraphics g)
	{
		ImageIcon imgIcon = AvatarData.getImgIcon((Canvas.w <= 200) ? 871 : 870);
		if (imgIcon.count != -1)
		{
			g.drawRegion(imgIcon.img, 0f, (float)(this.typePaint * BCBoardScr.hHT), BCBoardScr.rWT, BCBoardScr.hHT, 0, (float)this.x, (float)this.y, 3);
			Canvas.smallFontYellow.drawString(g, this.valuea + string.Empty, this.x, this.y - (int)AvMain.hSmall / 2, 2);
		}
	}

	// Token: 0x04000F2F RID: 3887
	public int x;

	// Token: 0x04000F30 RID: 3888
	public int y;

	// Token: 0x04000F31 RID: 3889
	public int valuea;

	// Token: 0x04000F32 RID: 3890
	public int typePaint;

	// Token: 0x04000F33 RID: 3891
	public static int fl;
}
