using System;

// Token: 0x02000061 RID: 97
public abstract class Part
{
	// Token: 0x0600036C RID: 876 RVA: 0x0000A99C File Offset: 0x00008D9C
	public virtual void paint(MyGraphics g, int x, int y, int arthor)
	{
		if (this.IDPart != -1)
		{
			if (this.IDPart >= 2000)
			{
				this.paintDynamic(g, this.idIcon, x, y, 0, arthor);
			}
			else
			{
				AvatarData.listImgInfo[(int)this.idIcon].paintPart(g, x, y, arthor);
			}
		}
	}

	// Token: 0x0600036D RID: 877 RVA: 0x0000A9F2 File Offset: 0x00008DF2
	public virtual void paintIcon(MyGraphics g, int x, int y, int direct, int arthor)
	{
		this.paint(g, x, y, arthor);
	}

	// Token: 0x0600036E RID: 878 RVA: 0x0000A9FF File Offset: 0x00008DFF
	public virtual void paintAvatar(MyGraphics g, int index, int x, int y, int direct, int arthor)
	{
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0000AA04 File Offset: 0x00008E04
	public void paintDynamic(MyGraphics g, short idImg, int x, int y, int direct, int arthor)
	{
		ImageIcon imagePart = AvatarData.getImagePart(idImg);
		if (imagePart.count != -1 || this.IDPart == -1)
		{
			g.drawRegion(imagePart.img, 0f, 0f, (int)imagePart.w, (int)imagePart.h, direct, (float)x, (float)y, arthor);
		}
	}

	// Token: 0x04000455 RID: 1109
	public short follow;

	// Token: 0x04000456 RID: 1110
	public short IDPart;

	// Token: 0x04000457 RID: 1111
	public short idIcon;

	// Token: 0x04000458 RID: 1112
	public int[] price = new int[2];

	// Token: 0x04000459 RID: 1113
	public sbyte zOrder;

	// Token: 0x0400045A RID: 1114
	public sbyte sell;

	// Token: 0x0400045B RID: 1115
	public string name;
}
