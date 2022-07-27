using System;

// Token: 0x0200006D RID: 109
public class StarFruitObj : SubObject
{
	// Token: 0x060003AE RID: 942 RVA: 0x00021A06 File Offset: 0x0001FE06
	public StarFruitObj()
	{
		this.catagory = 8;
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00021A18 File Offset: 0x0001FE18
	public void setFruit()
	{
		if (this.numberFruit > 0)
		{
			int num = CRes.rnd(3) + 3;
			this.xFruit = new sbyte[num];
			this.yFruit = new sbyte[num];
			for (int i = 0; i < num; i++)
			{
				this.xFruit[i] = (sbyte)(CRes.rnd(this.w0 - 10) - (this.w0 - 10) / 2);
				this.yFruit[i] = (sbyte)(CRes.rnd(this.h0 - 10) - (this.h0 - 10) / 2);
			}
		}
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00021AAC File Offset: 0x0001FEAC
	public override void update()
	{
		this.type = StarFruitObj.imgID;
		if (Canvas.getTick() - this.time >= 1000L)
		{
			if (this.timeFinish > 0)
			{
				this.timeFinish--;
				if (this.timeFinish == 0)
				{
					FarmService.gI().doFinishStarFruit();
				}
			}
			this.time = Canvas.getTick();
			ImageIcon imgIcon = FarmData.getImgIcon((short)StarFruitObj.imgID);
			if (imgIcon.w > 0)
			{
				if (this.w == 0)
				{
					this.w = imgIcon.w;
					this.h = imgIcon.h;
				}
				if (this.w0 == 0)
				{
					this.w0 = (int)(imgIcon.w / 3 * 2);
					this.h0 = (int)(imgIcon.h / 2);
					this.setFruit();
				}
			}
		}
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00021B80 File Offset: 0x0001FF80
	public override void paint(MyGraphics g)
	{
		if (this.type < 0 && ((float)(this.x * MyObject.hd + (int)(this.w / 2)) < AvCamera.gI().xCam || (float)(this.x * MyObject.hd - (int)(this.w / 2)) > AvCamera.gI().xCam + (float)Canvas.w))
		{
			return;
		}
		FarmData.paintImg(g, StarFruitObj.imgID, this.x * MyObject.hd, this.y * MyObject.hd, MyGraphics.HCENTER | MyGraphics.BOTTOM);
		if (this.numberFruit > 0 && this.xFruit != null)
		{
			for (int i = 0; i < this.xFruit.Length; i++)
			{
				FarmData.paintImg(g, (int)this.fruitID, this.x * MyObject.hd + (int)this.xFruit[i], this.y * MyObject.hd - (int)(FarmData.getImgIcon((short)StarFruitObj.imgID).h / 2 + 5) + (int)this.yFruit[i], 3);
			}
		}
		int num = (int)FarmData.getImgIcon((short)StarFruitObj.imgID).h + (int)AvMain.hBorder;
		if (this.timeFinish > 0)
		{
			num += (int)AvMain.hSmall;
		}
		FarmData.paintImg(g, (int)this.fruitID, (this.x - 8) * MyObject.hd, this.y * MyObject.hd - num, 3);
		Canvas.borderFont.drawString(g, "Lv" + this.lv, this.x * MyObject.hd, this.y * MyObject.hd - num - (int)AvMain.hBorder / 2, 0);
		if (this.timeFinish > 0)
		{
			int num2 = this.timeFinish / 3600;
			int num3 = (this.timeFinish - num2 * 3600) / 60;
			int num4 = this.timeFinish - num2 * 3600 - num3 * 60;
			Canvas.smallFontYellow.drawString(g, string.Concat(new object[]
			{
				num2,
				":",
				num3,
				":",
				num4
			}), (this.x + 3) * MyObject.hd, this.y * MyObject.hd - num + Canvas.borderFont.getHeight() / 2 + 2 * MyObject.hd, 2);
		}
	}

	// Token: 0x040004BD RID: 1213
	public short lv;

	// Token: 0x040004BE RID: 1214
	public short productID;

	// Token: 0x040004BF RID: 1215
	public short fruitID;

	// Token: 0x040004C0 RID: 1216
	public short numberFruit;

	// Token: 0x040004C1 RID: 1217
	public short anTrom;

	// Token: 0x040004C2 RID: 1218
	public static int imgID;

	// Token: 0x040004C3 RID: 1219
	public int timeFinish;

	// Token: 0x040004C4 RID: 1220
	public int w0;

	// Token: 0x040004C5 RID: 1221
	public int h0;

	// Token: 0x040004C6 RID: 1222
	public long time;

	// Token: 0x040004C7 RID: 1223
	public sbyte[] xFruit;

	// Token: 0x040004C8 RID: 1224
	public sbyte[] yFruit;
}
