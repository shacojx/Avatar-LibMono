using System;

// Token: 0x0200005F RID: 95
public class ObjPicture : SubObject
{
	// Token: 0x06000359 RID: 857 RVA: 0x0001DC68 File Offset: 0x0001C068
	public ObjPicture(int x, int y, Image im)
	{
		this.x = x;
		this.y = y;
		this.img = im;
		this.w = (short)im.getWidth();
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0001DC94 File Offset: 0x0001C094
	public override void paint(MyGraphics g)
	{
		if ((float)(this.x * MyObject.hd + (int)(this.w / 2)) < AvCamera.gI().xCam || (float)(this.x * MyObject.hd - (int)(this.w / 2)) > AvCamera.gI().xCam + (float)Canvas.w)
		{
			return;
		}
		g.drawImage(this.img, (float)(this.x * MyObject.hd), (float)(this.y * MyObject.hd), MyGraphics.BOTTOM | MyGraphics.HCENTER);
	}

	// Token: 0x04000424 RID: 1060
	private Image img;
}
