using System;

// Token: 0x020001B6 RID: 438
public class PimgBC
{
	// Token: 0x06000BF4 RID: 3060 RVA: 0x000784B4 File Offset: 0x000768B4
	public void paint(MyGraphics g)
	{
		if (AvatarData.getImgIcon(872).count != -1)
		{
			g.drawRegion(AvatarData.getImgIcon(872).img, 0f, (float)(this.type * BCBoardScr.hH), BCBoardScr.rW, BCBoardScr.hH, 0, (float)this.x, (float)this.y, 0);
		}
	}

	// Token: 0x04000F3F RID: 3903
	public int type;

	// Token: 0x04000F40 RID: 3904
	public int moneyPut;

	// Token: 0x04000F41 RID: 3905
	public int x;

	// Token: 0x04000F42 RID: 3906
	public int y;
}
