using System;

// Token: 0x02000055 RID: 85
public class MapItem : SubObject
{
	// Token: 0x060002FB RID: 763 RVA: 0x000188F3 File Offset: 0x00016CF3
	public MapItem()
	{
	}

	// Token: 0x060002FC RID: 764 RVA: 0x000188FB File Offset: 0x00016CFB
	public MapItem(int type, int x, int y, int id, short typeID)
	{
		this.type = (int)((sbyte)type);
		this.x = x;
		this.y = y;
		this.ID = (short)id;
		this.typeID = typeID;
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0001892C File Offset: 0x00016D2C
	public void setPos(int x0, int y0)
	{
		this.xTo = x0;
		this.x = x0;
		this.yTo = y0;
		this.y = y0;
	}

	// Token: 0x060002FE RID: 766 RVA: 0x0001895C File Offset: 0x00016D5C
	public int X()
	{
		MapItemType mapItemTypeByID;
		if (this.isGetImg)
		{
			mapItemTypeByID = LoadMap.getMapItemTypeByID((int)this.typeID);
		}
		else
		{
			mapItemTypeByID = AvatarData.getMapItemTypeByID((int)this.typeID);
		}
		return this.x + (int)mapItemTypeByID.dx;
	}

	// Token: 0x060002FF RID: 767 RVA: 0x000189A0 File Offset: 0x00016DA0
	public int Y()
	{
		MapItemType mapItemTypeByID;
		if (this.isGetImg)
		{
			mapItemTypeByID = LoadMap.getMapItemTypeByID((int)this.typeID);
		}
		else
		{
			mapItemTypeByID = AvatarData.getMapItemTypeByID((int)this.typeID);
		}
		return this.y + (int)mapItemTypeByID.dy;
	}

	// Token: 0x06000300 RID: 768 RVA: 0x000189E4 File Offset: 0x00016DE4
	public override void paint(MyGraphics g)
	{
		MapItemType mapItemTypeByID;
		if (this.isGetImg)
		{
			mapItemTypeByID = LoadMap.getMapItemTypeByID((int)this.typeID);
		}
		else
		{
			mapItemTypeByID = AvatarData.getMapItemTypeByID((int)this.typeID);
		}
		if (!this.isGetImg && LoadMap.TYPEMAP != 68 && LoadMap.TYPEMAP != 69 && LoadMap.TYPEMAP != 70 && LoadMap.TYPEMAP != 110)
		{
			ImageInfo imageInfo = AvatarData.listImgInfo[(int)mapItemTypeByID.imgID];
			if (this.w == 0)
			{
				this.w = imageInfo.w;
				this.h = imageInfo.h;
			}
			if ((float)((this.x + (int)mapItemTypeByID.dx + (int)imageInfo.w) * MyObject.hd) < AvCamera.gI().xCam || (float)((this.x + (int)mapItemTypeByID.dx - (int)imageInfo.w) * MyObject.hd) > AvCamera.gI().xCam + (float)Canvas.w || (float)((this.y + (int)imageInfo.h) * MyObject.hd) < AvCamera.gI().yCam || (float)((this.y + (int)mapItemTypeByID.dy - (int)imageInfo.h) * MyObject.hd) > AvCamera.gI().yCam + (float)Canvas.h)
			{
				return;
			}
			imageInfo.paintPart(g, (this.x + (int)mapItemTypeByID.dx) * MyObject.hd, (this.y + (int)mapItemTypeByID.dy) * MyObject.hd, (int)this.dir, 0);
		}
		else
		{
			this.paintPartImage(g, mapItemTypeByID.imgID, (this.x + (int)mapItemTypeByID.dx) * MyObject.hd, (this.y + (int)mapItemTypeByID.dy) * MyObject.hd, 0);
		}
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00018BA4 File Offset: 0x00016FA4
	public void paintPartImage(MyGraphics g, short imgID, int x, int y, int arthor)
	{
		ImageIcon imgIcon = AvatarData.getImgIcon(imgID);
		if ((float)(x + (int)imgIcon.w) < AvCamera.gI().xCam || (float)x > AvCamera.gI().xCam + (float)Canvas.w || (float)(y + (int)imgIcon.h) < AvCamera.gI().yCam || (float)y > AvCamera.gI().yCam + (float)Canvas.h)
		{
			return;
		}
		if (imgIcon.count == -1)
		{
			return;
		}
		if (this.w == 0)
		{
			this.w = imgIcon.w;
			this.h = imgIcon.h;
		}
		g.drawRegion(imgIcon.img, 0f, 0f, (int)imgIcon.w, (int)imgIcon.h, (int)this.dir, (float)x, (float)y, arthor);
	}

	// Token: 0x06000302 RID: 770 RVA: 0x00018C7B File Offset: 0x0001707B
	public override void update()
	{
	}

	// Token: 0x04000390 RID: 912
	public short ID;

	// Token: 0x04000391 RID: 913
	public short typeID;

	// Token: 0x04000392 RID: 914
	public new short w;

	// Token: 0x04000393 RID: 915
	public new short h;

	// Token: 0x04000394 RID: 916
	public sbyte dir;

	// Token: 0x04000395 RID: 917
	public bool isGetImg;

	// Token: 0x04000396 RID: 918
	public int xTo;

	// Token: 0x04000397 RID: 919
	public int yTo;
}
