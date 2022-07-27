using System;

// Token: 0x02000062 RID: 98
public class PartFollow : Part
{
	// Token: 0x06000371 RID: 881 RVA: 0x0001E980 File Offset: 0x0001CD80
	public override void paintIcon(MyGraphics g, int x, int y, int direct, int arthor)
	{
		APartInfo apartInfo = (APartInfo)AvatarData.getPart(this.follow);
		if (this.idIcon == apartInfo.imgID[0])
		{
			ImageInfo imageInfo = AvatarData.listImgInfo[(int)apartInfo.imgID[0]];
			g.drawRegion(AvatarData.getBigImgInfo((int)this.color).img, (float)((int)imageInfo.x0 * AvMain.hd), (float)((int)imageInfo.y0 * AvMain.hd), (int)imageInfo.w * AvMain.hd, (int)imageInfo.h * AvMain.hd, direct, (float)x, (float)y, arthor);
		}
		else
		{
			apartInfo.paint(g, x, y, arthor);
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0001EA20 File Offset: 0x0001CE20
	public override void paintAvatar(MyGraphics g, int index, int x, int y, int direct, int arthor)
	{
		APartInfo apartInfo = (APartInfo)AvatarData.getPart(this.follow);
		ImageInfo imageInfo = AvatarData.listImgInfo[(int)apartInfo.imgID[index]];
		AvatarData.paintPart(g, (int)this.color, (int)imageInfo.x0, (int)imageInfo.y0, (int)imageInfo.w, (int)imageInfo.h, x + (int)apartInfo.dx[index] * AvMain.hd - ((direct != (int)Base.LEFT) ? 0 : ((int)apartInfo.dx[index] * AvMain.hd * 2 + (int)imageInfo.w * AvMain.hd)), y + (int)apartInfo.dy[index] * AvMain.hd, direct, 0);
	}

	// Token: 0x0400045C RID: 1116
	public short color;
}
