using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class FlyTextInfo
{
	// Token: 0x06000245 RID: 581 RVA: 0x00013160 File Offset: 0x00011560
	public FlyTextInfo(int x0, int y0, int text1, int dir1, Image img1, int delay1, int imgID, int imgID_2)
	{
		this.delay = delay1;
		this.dir = (sbyte)dir1;
		this.x = x0;
		this.y = y0;
		if (text1 > 0)
		{
			this.text = "+";
		}
		this.text += text1;
		if (text1 == 0)
		{
			this.text = string.Empty;
		}
		this.img = img1;
		this.isSmall = false;
		this.normal = -1;
		this.imgID = imgID;
		this.imgID_2 = imgID_2;
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0001320C File Offset: 0x0001160C
	public FlyTextInfo(int x0, int y0, string text1, int dir1, int type, int delay1)
	{
		this.delay = delay1;
		this.dir = (sbyte)dir1;
		this.x = x0;
		this.y = y0;
		this.text = text1;
		this.state = 0;
		this.isSmall = true;
		this.normal = (sbyte)type;
		this.imgID = -1;
		this.imgID_2 = -1;
	}

	// Token: 0x06000247 RID: 583 RVA: 0x00013284 File Offset: 0x00011684
	public void update()
	{
		if (this.delay > 0)
		{
			this.delay--;
			return;
		}
		this.state++;
		if (this.state > 40)
		{
			this.img = null;
			Canvas.flyTexts.removeElement(this);
		}
		if (this.state < 3)
		{
			this.y += -2 * (int)this.dir;
		}
		else
		{
			this.y += (int)this.dir;
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x00013318 File Offset: 0x00011718
	public void paint(MyGraphics g)
	{
		GUIUtility.ScaleAroundPivot(Vector2.one * AvMain.zoom, Vector2.zero);
		if (Canvas.currentMyScreen == RaceScr.me)
		{
			Canvas.resetTrans(g);
		}
		if (this.delay > 0)
		{
			GUIUtility.ScaleAroundPivot(Vector2.one / AvMain.zoom, Vector2.zero);
			return;
		}
		int num = AvMain.hd;
		if ((Canvas.currentMyScreen == BoardScr.me && (BoardScr.isStartGame || BoardScr.disableReady)) || Canvas.currentMyScreen == RaceScr.me)
		{
			num = 1;
		}
		if (this.isSmall)
		{
			if ((int)this.normal == 0)
			{
				Canvas.smallFontRed.drawString(g, this.text, this.x * num, this.y * num, 2);
			}
			else
			{
				Canvas.borderFont.drawString(g, this.text, this.x * num, this.y * num, 2);
			}
		}
		else
		{
			Canvas.numberFont.drawString(g, this.text, this.x * num, this.y * num, 2);
			if (this.img == null)
			{
				if (this.imgID != -1)
				{
					FarmData.paintImg(g, this.imgID, this.x * num, (this.y - 5) * num, MyGraphics.HCENTER | MyGraphics.BOTTOM);
				}
				else if (this.imgID_2 != -1)
				{
					AvatarData.paintImg(g, this.imgID_2, this.x * num, (this.y - 5) * num, MyGraphics.HCENTER | MyGraphics.BOTTOM);
				}
			}
			else if (this.img != null)
			{
				g.drawImage(this.img, (float)(this.x * num), (float)(this.y * num), MyGraphics.BOTTOM | MyGraphics.HCENTER);
			}
		}
		GUIUtility.ScaleAroundPivot(Vector2.one / AvMain.zoom, Vector2.zero);
	}

	// Token: 0x040002E4 RID: 740
	private string text = string.Empty;

	// Token: 0x040002E5 RID: 741
	private int x;

	// Token: 0x040002E6 RID: 742
	private int y;

	// Token: 0x040002E7 RID: 743
	private int state;

	// Token: 0x040002E8 RID: 744
	private int delay;

	// Token: 0x040002E9 RID: 745
	private bool isSmall;

	// Token: 0x040002EA RID: 746
	private Image img;

	// Token: 0x040002EB RID: 747
	private int imgID = -1;

	// Token: 0x040002EC RID: 748
	private int imgID_2;

	// Token: 0x040002ED RID: 749
	private sbyte dir;

	// Token: 0x040002EE RID: 750
	private sbyte normal = -1;
}
