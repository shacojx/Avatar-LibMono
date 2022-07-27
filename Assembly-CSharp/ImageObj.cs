using System;

// Token: 0x0200004D RID: 77
public class ImageObj : SubObject
{
	// Token: 0x060002E3 RID: 739 RVA: 0x00017C98 File Offset: 0x00016098
	public ImageObj(int type, int x, int y, int w, int h) : base(type, x, y, w, h)
	{
		if (type >= 0)
		{
			this.img = Image.createImagePNG(T.getPath() + "/home/" + type);
		}
		if (this.img != null)
		{
			this.w = (short)this.img.w;
			this.h = (short)this.img.h;
		}
		else
		{
			this.w = (this.h = 0);
		}
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00017D1D File Offset: 0x0001611D
	public override void update()
	{
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00017D20 File Offset: 0x00016120
	public override void paint(MyGraphics g)
	{
		if (this.img == null)
		{
			if (this.w == 0)
			{
				this.w = AvatarData.getImgIcon((short)this.type).w;
				this.h = AvatarData.getImgIcon((short)this.type).h;
			}
			AvatarData.paintImg(g, this.type, this.x * MyObject.hd, this.y * MyObject.hd, 33);
		}
		else
		{
			g.drawImage(this.img, (float)(this.x * MyObject.hd), (float)(this.y * MyObject.hd), 33);
		}
		if (this.type == 846)
		{
			Canvas.blackF.drawString(g, MapScr.boardID + string.Empty, this.x * MyObject.hd, this.y * MyObject.hd - 30 * MyObject.hd, 2);
		}
		else if (this.type == 1029 && FarmScr.foodID != 0)
		{
			Food foodByID = FarmData.getFoodByID(FarmScr.foodID);
			FarmItem farmItem = FarmScr.getFarmItem((int)foodByID.productID);
			string text = string.Empty;
			int num = FarmScr.remainTime / 3600;
			if (num > 0)
			{
				text = num + ":";
			}
			int num2 = (FarmScr.remainTime - num * 3600) / 60;
			if (num2 > 0 || num > 0)
			{
				text = text + num2 + ":";
			}
			int num3 = FarmScr.remainTime - num * 3600 - num2 * 60;
			text = text + num3 + string.Empty;
			if (FarmScr.remainTime == 0)
			{
				text = "Hoan thanh";
			}
			FarmScr.xPosCook = this.x - Canvas.smallFontYellow.getWidth(text) / 2 / MyObject.hd;
			int num4 = 60 * MyObject.hd;
			FarmScr.yPosCook = this.y - num4 / MyObject.hd - 10;
			FarmData.paintImg(g, (int)farmItem.IDImg, this.x * MyObject.hd - Canvas.smallFontYellow.getWidth(text) / 2, this.y * MyObject.hd - num4 - 10 * MyObject.hd, 3);
			Canvas.smallFontYellow.drawString(g, text, this.x * MyObject.hd - Canvas.smallFontYellow.getWidth(text) / 2 + 10 * MyObject.hd, this.y * MyObject.hd - num4 - 10 * MyObject.hd - (int)AvMain.hSmall / 2 + 2, 0);
		}
	}

	// Token: 0x04000364 RID: 868
	public Image img;
}
