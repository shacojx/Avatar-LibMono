using System;

// Token: 0x020001B2 RID: 434
public class ParkListSrc : MyScreen
{
	// Token: 0x06000BAA RID: 2986 RVA: 0x00075D78 File Offset: 0x00074178
	public ParkListSrc()
	{
		this.w = Canvas.stypeInt * 50;
		this.h = this.w * this.maxH;
		this.imgIcon = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/parkIcon"), 34 * AvMain.hd, 16 * AvMain.hd);
		this.imgFocus = Image.createImagePNG(T.getPath() + "/temp/statfocus");
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x00075E03 File Offset: 0x00074203
	public static ParkListSrc gI()
	{
		if (ParkListSrc.instance == null)
		{
			ParkListSrc.instance = new ParkListSrc();
		}
		return ParkListSrc.instance;
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x00075E1E File Offset: 0x0007421E
	public void switchToMe(MyScreen lastScr)
	{
		base.switchToMe();
		this.lastScr = lastScr;
		this.selected = 0;
		this.isHide = true;
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x00075E3B File Offset: 0x0007423B
	protected void doSelect()
	{
		Canvas.cameraList.close();
		this.lastScr.switchToMe();
		ParkService.gI().doJoinPark((int)MapScr.roomID, this.selected);
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x00075E68 File Offset: 0x00074268
	public override void setSelected(int se, bool isAc)
	{
		if (isAc && this.selected == se)
		{
			this.doSelect();
		}
		base.setSelected(se, isAc);
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x00075E8C File Offset: 0x0007428C
	public void setList(int[] list)
	{
		this.listBoard = list;
		int num = this.listBoard.Length / this.maxW;
		if (this.listBoard.Length % this.maxW != 0)
		{
			num++;
		}
		this.x = Canvas.hw - (this.w * this.maxW + 10) / 2;
		this.y = Canvas.hCan / 2 - this.w * this.maxH / 2 + 40 * AvMain.hd / 2;
		this.wAll = this.w * this.maxW + 10;
		int num2 = this.listBoard.Length / this.maxW;
		if (this.listBoard.Length % this.maxW != 0)
		{
			num2++;
		}
		Canvas.cameraList.setInfo(this.x, this.y, this.w, this.w, this.maxW * this.w, num2 * this.w, this.w * this.maxW, this.h - AvMain.hDuBox, list.Length);
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x00075FA0 File Offset: 0x000743A0
	public override void updateKey()
	{
		if (Canvas.isPointerClick && Canvas.isPointer(this.x - 10 * AvMain.hd + (this.wAll + 20 * AvMain.hd) - 3 - 20 * AvMain.hd, this.y - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
		{
			this.isTranKey = true;
			this.countClose = 5;
			Canvas.isPointerClick = false;
		}
		if (this.isTranKey)
		{
			if (Canvas.isPointerDown && (int)this.countClose == 5 && !Canvas.isPointer(this.x - 10 * AvMain.hd + (this.wAll + 20 * AvMain.hd) - 3 - 20 * AvMain.hd, this.y - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
			{
				this.countClose = 0;
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranKey = false;
				if ((int)this.countClose == 5)
				{
					Canvas.cameraList.close();
					this.lastScr.switchToMe();
				}
			}
		}
		base.updateKey();
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x000760DC File Offset: 0x000744DC
	public override void update()
	{
		if (!this.isTranKey && (int)this.countClose > 0)
		{
			this.countClose = (sbyte)((int)this.countClose - 1);
			if ((int)this.countClose == 0)
			{
				return;
			}
		}
		this.lastScr.update();
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x0007612C File Offset: 0x0007452C
	public override void paint(MyGraphics g)
	{
		g.translate(0f, 0f);
		g.setClip(0f, 0f, (float)Canvas.w, (float)Canvas.h);
		this.lastScr.paint(g);
		Canvas.paint.paintPopupBack(g, this.x - 10 * AvMain.hd, this.y, this.wAll + 20 * AvMain.hd, this.h, (int)this.countClose / 3, false);
		this.paintList(g);
		base.paint(g);
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x000761C0 File Offset: 0x000745C0
	private void paintList(MyGraphics g)
	{
		g.translate((float)(Canvas.hw - (this.w * this.maxW + 10) / 2 + 4), (float)Canvas.cameraList.y);
		g.setClip(0f, (float)(4 * AvMain.hd), (float)(this.w * this.maxW + 2), (float)Canvas.cameraList.disY);
		g.translate(1f, -CameraList.cmy);
		if (!this.isHide)
		{
			g.drawImage(this.imgFocus, (float)(this.selected % this.maxW * this.w + this.w / 2), (float)(this.selected / this.maxW * this.w + this.w / 2), 3);
		}
		int num = (int)(CameraList.cmy / (float)this.w * (float)this.maxW - (float)this.maxW);
		if (num < 0)
		{
			num = 0;
		}
		int num2 = num + this.maxH * this.maxW + this.maxW * 2;
		if (num2 > this.listBoard.Length)
		{
			num2 = this.listBoard.Length;
		}
		for (int i = num; i < num2; i++)
		{
			this.imgIcon.drawFrame(this.listBoard[i], i % this.maxW * this.w + this.w / 2, i / this.maxW * this.w + this.w / 2, 0, 3, g);
		}
		for (int j = num; j < num2; j++)
		{
			Canvas.smallFontYellow.drawString(g, j + string.Empty, j % this.maxW * this.w + this.w / 2, j / this.maxW * this.w + this.w / 2 - (int)AvMain.hSmall / 2 - ((AvMain.hd != 1) ? 0 : 2), 2);
		}
	}

	// Token: 0x04000EF5 RID: 3829
	public static ParkListSrc instance;

	// Token: 0x04000EF6 RID: 3830
	public int[] listBoard;

	// Token: 0x04000EF7 RID: 3831
	private MyScreen lastScr;

	// Token: 0x04000EF8 RID: 3832
	private int maxW = 4;

	// Token: 0x04000EF9 RID: 3833
	private int w;

	// Token: 0x04000EFA RID: 3834
	private int maxH = 4;

	// Token: 0x04000EFB RID: 3835
	private int x;

	// Token: 0x04000EFC RID: 3836
	private int y;

	// Token: 0x04000EFD RID: 3837
	private int wAll;

	// Token: 0x04000EFE RID: 3838
	private int h;

	// Token: 0x04000EFF RID: 3839
	private sbyte countClose;

	// Token: 0x04000F00 RID: 3840
	private FrameImage imgIcon;

	// Token: 0x04000F01 RID: 3841
	private Image imgFocus;

	// Token: 0x04000F02 RID: 3842
	private bool isTranKey;
}
