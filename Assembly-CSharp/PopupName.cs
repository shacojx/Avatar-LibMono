using System;

// Token: 0x02000066 RID: 102
public class PopupName : SubObject
{
	// Token: 0x0600037F RID: 895 RVA: 0x0001F86B File Offset: 0x0001DC6B
	public PopupName(string name, int x, int y)
	{
		this.x = x;
		this.y = y;
		this.name = name;
		this.num = (sbyte)CRes.rnd(8);
	}

	// Token: 0x06000380 RID: 896 RVA: 0x0001F895 File Offset: 0x0001DC95
	public override void update()
	{
		this.num = (sbyte)((int)this.num + 1);
		if ((int)this.num >= 8)
		{
			this.num = 0;
		}
	}

	// Token: 0x06000381 RID: 897 RVA: 0x0001F8BC File Offset: 0x0001DCBC
	public override void paint(MyGraphics g)
	{
		if (OptionScr.gI().mapFocus[1] == 1 || (MainMenu.gI().isGO && (int)this.iPrivate == 0))
		{
			return;
		}
		if ((float)(this.x * MyObject.hd) < AvCamera.gI().xCam || (float)(this.x * MyObject.hd) > AvCamera.gI().xCam + (float)Canvas.w || (float)(this.y * MyObject.hd) < AvCamera.gI().yCam || (float)(this.y * MyObject.hd) > AvCamera.gI().yCam + (float)Canvas.hCan + 10f)
		{
			return;
		}
		g.drawImage(LoadMap.imgShadow, (float)(this.x * MyObject.hd), (float)(this.y * MyObject.hd), 3);
		if (MiniMap.imgArrow != null)
		{
			MiniMap.imgArrow.drawFrame(0, this.x * MyObject.hd, (this.y - 10 + (int)this.num / 2) * MyObject.hd, 0, MyGraphics.BOTTOM | MyGraphics.HCENTER, g);
		}
		Canvas.smallFontYellow.drawString(g, this.name, this.x * MyObject.hd, (this.y - 32 + (int)this.num / 2) * MyObject.hd, 2);
	}

	// Token: 0x0400046E RID: 1134
	private string name;

	// Token: 0x0400046F RID: 1135
	public sbyte num;

	// Token: 0x04000470 RID: 1136
	public sbyte iPrivate;
}
