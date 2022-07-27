using System;

// Token: 0x02000023 RID: 35
public class APartInfo : Part
{
	// Token: 0x0600016D RID: 365 RVA: 0x0000AA64 File Offset: 0x00008E64
	public override void paintAvatar(MyGraphics g, int index, int x, int y, int direct, int arthor)
	{
		if (this.IDPart != -1)
		{
			if (this.IDPart >= 2000)
			{
				ImageIcon imagePart = AvatarData.getImagePart(this.imgID[index]);
				if (imagePart.count != -1)
				{
					g.drawRegion(imagePart.img, 0f, 0f, (int)imagePart.w, (int)imagePart.h, direct, (float)(x + (int)this.dx[index] * AvMain.hd - ((direct != (int)Base.LEFT) ? 0 : ((int)this.dx[index] * AvMain.hd * 2 + (int)imagePart.w))), (float)(y + (int)this.dy[index] * AvMain.hd), 0);
				}
			}
			else
			{
				ImageInfo imageInfo = AvatarData.listImgInfo[(int)this.imgID[index]];
				AvatarData.paintPart(g, (int)imageInfo.bigID, (int)imageInfo.x0, (int)imageInfo.y0, (int)imageInfo.w, (int)imageInfo.h, x + (int)this.dx[index] * AvMain.hd - ((direct != (int)Base.LEFT) ? 0 : ((int)this.dx[index] * AvMain.hd * 2 + (int)imageInfo.w * AvMain.hd)), y + (int)this.dy[index] * AvMain.hd, direct, 0);
			}
		}
	}

	// Token: 0x0400014E RID: 334
	public sbyte level;

	// Token: 0x0400014F RID: 335
	public sbyte gender;

	// Token: 0x04000150 RID: 336
	public short[] imgID;

	// Token: 0x04000151 RID: 337
	public sbyte[] dx;

	// Token: 0x04000152 RID: 338
	public sbyte[] dy;
}
