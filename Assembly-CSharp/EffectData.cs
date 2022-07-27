using System;

// Token: 0x02000037 RID: 55
public class EffectData
{
	// Token: 0x06000228 RID: 552 RVA: 0x00012600 File Offset: 0x00010A00
	public ImageInfo getImgInfo(sbyte id)
	{
		for (int i = 0; i < this.imgImfo.Length; i++)
		{
			if ((int)this.imgImfo[i].ID == (int)id)
			{
				return this.imgImfo[i];
			}
		}
		return null;
	}

	// Token: 0x06000229 RID: 553 RVA: 0x00012644 File Offset: 0x00010A44
	public void paint(MyGraphics g, int x, int y, int index)
	{
		Frame frame = this.frame[(int)this.arrFrame[index]];
		for (int i = 0; i < frame.dx.Length; i++)
		{
			ImageInfo imgInfo = this.getImgInfo(frame.idImg[i]);
			g.drawRegion(this.img, (float)((int)imgInfo.x0 * AvMain.hd), (float)((int)imgInfo.y0 * AvMain.hd), (int)imgInfo.w * AvMain.hd, (int)imgInfo.h * AvMain.hd, 0, (float)(x * AvMain.hd + (int)frame.dx[i] * AvMain.hd), (float)(y * AvMain.hd + (int)frame.dy[i] * AvMain.hd), 0);
		}
	}

	// Token: 0x040002A9 RID: 681
	public sbyte[] arrFrame;

	// Token: 0x040002AA RID: 682
	public ImageInfo[] imgImfo;

	// Token: 0x040002AB RID: 683
	public Image img;

	// Token: 0x040002AC RID: 684
	public Frame[] frame;

	// Token: 0x040002AD RID: 685
	public short ID;
}
