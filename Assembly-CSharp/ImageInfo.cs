using System;

// Token: 0x0200004C RID: 76
public class ImageInfo
{
	// Token: 0x060002E0 RID: 736 RVA: 0x00017B84 File Offset: 0x00015F84
	public void paintFarm(MyGraphics g, int x, int y, int arthor)
	{
		g.drawRegion(FarmData.imgBig[(int)this.bigID], (float)((int)this.x0 * AvMain.hd), (float)((int)this.y0 * AvMain.hd), (int)this.w * AvMain.hd, (int)this.h * AvMain.hd, 0, (float)x, (float)y, arthor);
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x00017BDC File Offset: 0x00015FDC
	public void paintPart(MyGraphics g, int x, int y, int arthor)
	{
		g.drawRegion(AvatarData.getBigImgInfo((int)this.bigID).img, (float)((int)this.x0 * AvMain.hd), (float)((int)this.y0 * AvMain.hd), (int)this.w * AvMain.hd, (int)this.h * AvMain.hd, 0, (float)x, (float)y, arthor);
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00017C38 File Offset: 0x00016038
	public void paintPart(MyGraphics g, int x, int y, int dir, int arthor)
	{
		g.drawRegion(AvatarData.getBigImgInfo((int)this.bigID).img, (float)((int)this.x0 * AvMain.hd), (float)((int)this.y0 * AvMain.hd), (int)this.w * AvMain.hd, (int)this.h * AvMain.hd, dir, (float)x, (float)y, arthor);
	}

	// Token: 0x0400035E RID: 862
	public short ID;

	// Token: 0x0400035F RID: 863
	public short bigID;

	// Token: 0x04000360 RID: 864
	public short x0;

	// Token: 0x04000361 RID: 865
	public short y0;

	// Token: 0x04000362 RID: 866
	public short w;

	// Token: 0x04000363 RID: 867
	public short h;
}
