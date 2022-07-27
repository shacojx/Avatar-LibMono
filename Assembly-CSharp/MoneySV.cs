using System;

// Token: 0x020001B5 RID: 437
public class MoneySV
{
	// Token: 0x06000BF0 RID: 3056 RVA: 0x00078288 File Offset: 0x00076688
	public MoneySV(int x, int y, int xto, int yto, int valuea, int typePaint, int addFrom, int addTo, bool isMoveOK)
	{
		this.x = x;
		this.y = y;
		this.valuea = valuea;
		this.typePaint = typePaint;
		this.addFrom = addFrom;
		this.addTo = addTo;
		this.xto = xto;
		this.yto = yto;
		this.isMoveOK = isMoveOK;
		this.xbg = BCBoardScr.rW - BCBoardScr.rWT;
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x000782F4 File Offset: 0x000766F4
	public void update()
	{
		if (this.x != this.xto)
		{
			if (this.xto - this.x >> 1 == 0)
			{
				this.x = this.xto;
			}
			else
			{
				this.x += this.xto - this.x >> 1;
			}
		}
		if (this.y != this.yto)
		{
			if (this.yto - this.y >> 1 == 0)
			{
				this.y = this.yto;
			}
			else
			{
				this.y += this.yto - this.y >> 1;
			}
		}
		if (this.isMoveOK && this.x == this.xto && this.y == this.yto)
		{
			this.move = true;
		}
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x000783DC File Offset: 0x000767DC
	public void paint(MyGraphics g)
	{
		if (!this.move)
		{
			int num = this.x + BCBoardScr.rW / 4 + this.typePaint % 2 * BCBoardScr.rW / 2;
			int num2 = this.y + BCBoardScr.hH / 4 + this.typePaint / 2 * BCBoardScr.hH / 2;
			if (AvatarData.getImgIcon(870).count != -1)
			{
				g.drawRegion(AvatarData.getImgIcon(870).img, 0f, (float)(this.typePaint * BCBoardScr.hHT), BCBoardScr.rWT, BCBoardScr.hHT, 0, (float)num, (float)num2, 3);
				Canvas.smallFontYellow.drawString(g, this.valuea + string.Empty, num, num2 - (int)AvMain.hSmall / 2, 2);
			}
		}
	}

	// Token: 0x04000F34 RID: 3892
	public int x;

	// Token: 0x04000F35 RID: 3893
	public int y;

	// Token: 0x04000F36 RID: 3894
	public int valuea;

	// Token: 0x04000F37 RID: 3895
	public int typePaint;

	// Token: 0x04000F38 RID: 3896
	public int addFrom;

	// Token: 0x04000F39 RID: 3897
	public int addTo;

	// Token: 0x04000F3A RID: 3898
	public int xto;

	// Token: 0x04000F3B RID: 3899
	public int yto;

	// Token: 0x04000F3C RID: 3900
	public int xbg;

	// Token: 0x04000F3D RID: 3901
	public bool move;

	// Token: 0x04000F3E RID: 3902
	public bool isMoveOK;
}
