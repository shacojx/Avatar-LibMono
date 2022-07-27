using System;

// Token: 0x02000042 RID: 66
public class FrameImage
{
	// Token: 0x0600024B RID: 587 RVA: 0x00013516 File Offset: 0x00011916
	public FrameImage(Image img, int width, int height)
	{
		this.imgFrame = img;
		this.frameWidth = width;
		this.frameHeight = height;
		this.nFrame = img.getHeight() / height;
	}

	// Token: 0x0600024C RID: 588 RVA: 0x00013541 File Offset: 0x00011941
	public static FrameImage init(string path, int w, int h)
	{
		return new FrameImage(FilePack.getImg(path), w, h);
	}

	// Token: 0x0600024D RID: 589 RVA: 0x00013550 File Offset: 0x00011950
	public void drawFrame(int idx, int x, int y, int trans, int orthor, MyGraphics g)
	{
		if (idx >= 0 && idx < this.nFrame)
		{
			g.drawRegion(this.imgFrame, 0f, (float)(idx * this.frameHeight), this.frameWidth, this.frameHeight, trans, (float)x, (float)y, orthor);
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x000135A0 File Offset: 0x000119A0
	public void drawFrame(int idx, int x, int y, int trans, MyGraphics g)
	{
		g.drawRegion(this.imgFrame, 0f, (float)(idx * this.frameHeight), this.frameWidth, this.frameHeight, trans, (float)x, (float)y, 0);
	}

	// Token: 0x0600024F RID: 591 RVA: 0x000135DC File Offset: 0x000119DC
	public void drawFrameXY(int idx, int idy, int x, int y, int anthor, MyGraphics g)
	{
		if (idx >= 0 && idx < this.nFrame)
		{
			g.drawRegion(this.imgFrame, (float)(idx * this.frameWidth), (float)(idy * this.frameHeight), this.frameWidth, this.frameHeight, 0, (float)x, (float)y, anthor);
		}
	}

	// Token: 0x06000250 RID: 592 RVA: 0x00013630 File Offset: 0x00011A30
	public void drawFrameXY(int idx, int x, int y, int anthor, MyGraphics g)
	{
		if (idx >= 0)
		{
			g.drawRegion(this.imgFrame, (float)(idx / this.nFrame * this.frameWidth), (float)(idx % this.nFrame * this.frameHeight), this.frameWidth, this.frameHeight, 0, (float)x, (float)y, anthor);
		}
	}

	// Token: 0x040002F8 RID: 760
	public int frameWidth;

	// Token: 0x040002F9 RID: 761
	public int frameHeight;

	// Token: 0x040002FA RID: 762
	public int nFrame;

	// Token: 0x040002FB RID: 763
	public Image imgFrame;
}
