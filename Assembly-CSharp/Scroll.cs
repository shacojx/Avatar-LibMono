using System;

// Token: 0x0200006B RID: 107
public class Scroll
{
	// Token: 0x060003A6 RID: 934 RVA: 0x00021640 File Offset: 0x0001FA40
	public Scroll()
	{
		if (AvMain.hd == 2)
		{
			this.img = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/scrollList"), 7, 20);
		}
		else
		{
			this.img = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/scrollList"), 4, 10);
		}
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000216AF File Offset: 0x0001FAAF
	public static Scroll gI()
	{
		if (Scroll.instance == null)
		{
			Scroll.instance = new Scroll();
		}
		return Scroll.instance;
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x000216CC File Offset: 0x0001FACC
	public void init(int dis, int size, int cmy)
	{
		if (size == 0)
		{
			size = 1;
		}
		if (this.limit == 0)
		{
			this.limit = 1;
		}
		try
		{
			this.dis = dis;
			this.hScroll = 100;
			this.limit = size - dis;
			if (size > dis)
			{
				this.hScroll = dis * 100 / size;
				if (this.hScroll < 2)
				{
					this.hScroll = 2;
				}
				this.h = cmy * 100 / this.limit;
				int num = dis - dis * this.hScroll / 100;
				this.yScroll = this.h * num / 100;
			}
			Scroll.Disvisible = (this.limit + dis < dis);
			Scroll.isAble = true;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00021794 File Offset: 0x0001FB94
	public void updateScroll(int cmy, int cmtoY, int v)
	{
		if (Scroll.Disvisible || this.limit == 0)
		{
			return;
		}
		if (cmtoY < 0)
		{
			cmtoY = 0;
		}
		this.h = cmy * 100 / this.limit;
		int num = this.dis * this.hScroll / 100;
		if (num < 22 * AvMain.hd)
		{
			num = 22 * AvMain.hd;
		}
		int num2 = this.dis - num;
		this.yScroll = this.h * num2 / 100;
		if (this.yScroll > this.dis - 3)
		{
			this.yScroll = this.dis - 3;
		}
		if (this.yScroll < 0)
		{
			this.yScroll = 0;
		}
		if (this.yScroll + num > this.dis)
		{
			this.yScroll = this.dis - num;
		}
		if (cmy != cmtoY || (Canvas.isPointerDown | v != 0))
		{
			Scroll.isAble = true;
		}
		else
		{
			Scroll.isAble = false;
		}
	}

	// Token: 0x060003AA RID: 938 RVA: 0x00021894 File Offset: 0x0001FC94
	public void paintScroll(MyGraphics g, int x, int y)
	{
		if (Scroll.Disvisible || !Scroll.isAble)
		{
			return;
		}
		int num = this.dis * this.hScroll / 100;
		if (num < 22 * AvMain.hd)
		{
			num = 22 * AvMain.hd;
		}
		g.setClip((float)x, (float)(y + this.yScroll), 7f, (float)num);
		this.img.drawFrame(0, x, y + 1 + this.yScroll, 0, g);
		this.img.drawFrame(2, x, y + 1 + this.yScroll + num - 2 - 10 * AvMain.hd, 0, g);
		int num2 = (num - 20 * AvMain.hd) / (10 * AvMain.hd);
		for (int i = 0; i < num2; i++)
		{
			this.img.drawFrame(1, x, y + 1 + this.yScroll + 10 * AvMain.hd + 10 * AvMain.hd * i, 0, g);
		}
		int num3 = num - 2 - 10 * AvMain.hd - (10 * AvMain.hd + num2 * 10 * AvMain.hd);
		g.drawRegion(this.img.imgFrame, 0f, (float)(10 * AvMain.hd), 7, num3, 0, (float)x, (float)(y + 1 + this.yScroll + 10 * AvMain.hd + num2 * 10 * AvMain.hd), 0);
	}

	// Token: 0x040004B0 RID: 1200
	private static Scroll instance;

	// Token: 0x040004B1 RID: 1201
	public int limit;

	// Token: 0x040004B2 RID: 1202
	public int temp;

	// Token: 0x040004B3 RID: 1203
	public int dis;

	// Token: 0x040004B4 RID: 1204
	public int yScroll;

	// Token: 0x040004B5 RID: 1205
	public int hScroll = 100;

	// Token: 0x040004B6 RID: 1206
	public int h;

	// Token: 0x040004B7 RID: 1207
	public static bool Disvisible;

	// Token: 0x040004B8 RID: 1208
	public static bool isAble;

	// Token: 0x040004B9 RID: 1209
	private FrameImage img;
}
