using System;

// Token: 0x02000031 RID: 49
public class CellFarm : SubObject
{
	// Token: 0x06000201 RID: 513 RVA: 0x00010710 File Offset: 0x0000EB10
	public override void paint(MyGraphics g)
	{
		if ((float)(this.x * MyObject.hd) < AvCamera.gI().xCam - 10f || (float)(this.xCell * MyObject.hd) > AvCamera.gI().xCam + (float)Canvas.w + 10f)
		{
			return;
		}
		if (this.isSelected)
		{
			if (MyObject.hd == 2)
			{
				g.drawImage(FarmScr.imgFocusCel, (float)((this.x - 13) * MyObject.hd), (float)((this.y - 18) * MyObject.hd), 0);
			}
			else
			{
				g.drawImage(FarmScr.imgFocusCel, (float)(this.x - 11), (float)(this.y - 15), 0);
			}
		}
		int x = FarmScr.focusCell.x;
		int y = FarmScr.focusCell.y;
		if (this.xCell == x && this.yCell == y)
		{
			Canvas.smallFontYellow.drawString(g, "lv" + this.level, this.x * MyObject.hd, (this.y - 44) * MyObject.hd, 2);
		}
		if (this.idTree == -1)
		{
			return;
		}
		TreeInfo treeByID = FarmData.getTreeByID(this.idTree);
		treeByID.paint(g, this.statusTree, this.x * MyObject.hd, this.y * MyObject.hd, MyGraphics.BOTTOM | MyGraphics.HCENTER);
		int num = (int)(treeByID.harvestTime * 60 + treeByID.dieTime * 60);
		if (((int)this.time > num && treeByID.dieTime > 0) || (int)this.hervestPer == 100 || this.time < 0)
		{
			return;
		}
		if (this.isGrass)
		{
			g.drawImage(FarmScr.imgWorm_G[1], (float)((this.x + 5) * MyObject.hd), (float)((this.y - 2) * MyObject.hd), 3);
		}
		if (this.isWorm)
		{
			g.drawImage(FarmScr.imgWorm_G[0], (float)((this.x - 7) * MyObject.hd), (float)(this.y * MyObject.hd), 3);
		}
		if (this.xCell == x && this.yCell == y)
		{
			treeByID.paint(g, 7, x * 24 * MyObject.hd - 3, (y * 24 - 40) * MyObject.hd, MyGraphics.BOTTOM | MyGraphics.HCENTER);
			g.setColor(1);
			g.fillRect((float)((x * 24 - 4) * MyObject.hd), (float)((y * 24 - 38) * MyObject.hd), (float)(31 * MyObject.hd), (float)(4 * MyObject.hd));
			g.setColor(65280);
			g.fillRect((float)((x * 24 - 3) * MyObject.hd), (float)((y * 24 - 37) * MyObject.hd), (float)((int)this.vitalityPer * 30 / 100 * MyObject.hd), (float)(3 * MyObject.hd));
			g.setColor(2512938);
			g.drawRect((float)((x * 24 - 4) * MyObject.hd), (float)((y * 24 - 38) * MyObject.hd), (float)(31 * MyObject.hd), (float)(4 * MyObject.hd));
			long num2 = (long)(treeByID.harvestTime * 60 * 60) - this.tempTime;
			long num3 = (long)(treeByID.harvestTime * 60 - this.time);
			string text = string.Empty;
			if (num2 < 0L)
			{
				num2 = 0L;
			}
			long num4 = num2 / 60L / 60L;
			long num5 = num2 / 60L % 60L;
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				num4,
				":",
				num5
			});
			if (num3 <= 0L)
			{
				text = T.canHarvest;
			}
			Canvas.smallFontYellow.drawString(g, text, (x * 24 + 5) * MyObject.hd, (y * 24 - 40) * MyObject.hd - (int)AvMain.hSmall, 0);
			int num6 = (int)(this.time * 100 / (treeByID.harvestTime * 60));
			num6 = num6 * 30 / 100;
			if (num6 == 0)
			{
				num6 = 1;
			}
			if (num6 >= 30)
			{
				num6 = 29;
			}
			if (treeByID.harvestTime * 60 - this.time < 0)
			{
				num6 = 30;
			}
			g.setColor(1);
			g.fillRect((float)((x * 24 - 4) * MyObject.hd), (float)((y * 24 - 32) * MyObject.hd), (float)(31 * MyObject.hd), (float)(4 * MyObject.hd));
			g.setColor(16776960);
			g.fillRect((float)((x * 24 - 3) * MyObject.hd), (float)((y * 24 - 31) * MyObject.hd), (float)(num6 * MyObject.hd), (float)(3 * MyObject.hd));
			g.setColor(2512938);
			g.drawRect((float)((x * 24 - 4) * MyObject.hd), (float)((y * 24 - 32) * MyObject.hd), (float)(31 * MyObject.hd), (float)(4 * MyObject.hd));
			int num7 = 0;
			if (this.isGrass)
			{
				num7 = 1;
				FarmScr.imgWormAndGrass.drawFrame(1, (x * 24 + 5 + ((!this.isWorm) ? 0 : 6)) * MyObject.hd, (y * 24 - 22) * MyObject.hd, 0, g);
			}
			if (this.isWorm)
			{
				FarmScr.imgWormAndGrass.drawFrame(0, (x * 24 + 4 - num7 * 6) * MyObject.hd, (y * 24 - 22) * MyObject.hd, 0, g);
			}
		}
	}

	// Token: 0x04000259 RID: 601
	public int xCell;

	// Token: 0x0400025A RID: 602
	public int yCell;

	// Token: 0x0400025B RID: 603
	public int idTree;

	// Token: 0x0400025C RID: 604
	public short time;

	// Token: 0x0400025D RID: 605
	public long tempTime;

	// Token: 0x0400025E RID: 606
	public bool isArid;

	// Token: 0x0400025F RID: 607
	public bool isWorm;

	// Token: 0x04000260 RID: 608
	public bool isGrass;

	// Token: 0x04000261 RID: 609
	public bool isSelected;

	// Token: 0x04000262 RID: 610
	public sbyte hervestPer;

	// Token: 0x04000263 RID: 611
	public sbyte vitalityPer;

	// Token: 0x04000264 RID: 612
	public sbyte status;

	// Token: 0x04000265 RID: 613
	public sbyte level;

	// Token: 0x04000266 RID: 614
	public int statusTree;
}
