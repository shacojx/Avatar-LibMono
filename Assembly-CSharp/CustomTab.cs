using System;
using System.Globalization;

// Token: 0x02000033 RID: 51
public class CustomTab : Face
{
	// Token: 0x0600020E RID: 526 RVA: 0x00010F08 File Offset: 0x0000F308
	public CustomTab()
	{
		this.setSize();
		CustomTab.wStr = Canvas.blackF.getHeight() + 5 * AvMain.hd;
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00010F7A File Offset: 0x0000F37A
	public static CustomTab gI()
	{
		return (CustomTab.me != null) ? CustomTab.me : (CustomTab.me = new CustomTab());
	}

	// Token: 0x06000210 RID: 528 RVA: 0x00010F9B File Offset: 0x0000F39B
	private void setSize()
	{
		this.w = Canvas.w - 20;
		this.h = Canvas.hCan - 20;
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00010FB9 File Offset: 0x0000F3B9
	public void init()
	{
		this.setSize();
		this.setInfo(this.listImg, this.title, this.strTemp, this.idAction);
	}

	// Token: 0x06000212 RID: 530 RVA: 0x00010FE0 File Offset: 0x0000F3E0
	private void setFont(string str)
	{
		int num = str.IndexOf("Ę");
		if (num != -1)
		{
			string str2 = str.Substring(0, num);
			this.setCanhle(str2, string.Empty);
			str = str.Substring(num + 1);
			int num2 = str.IndexOf("\n");
			if (num2 != -1)
			{
				string str3 = str.Substring(0, num2);
				this.setCanhle(str3, "Ę");
				str = str.Substring(num2 + 1);
				this.setFont(str);
			}
			else
			{
				this.setCanhle(str, "Ę");
			}
		}
		else
		{
			this.setCanhle(str, string.Empty);
		}
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0001107C File Offset: 0x0000F47C
	private void setCanhle(string str, string tem)
	{
		if (str.Equals(string.Empty))
		{
			return;
		}
		int num = str.IndexOf("ę");
		if (num != -1)
		{
			string text = str.Substring(0, num);
			if (!text.Equals(string.Empty))
			{
				this.addd(text, tem, 0);
			}
			int anthor = int.Parse(str.Substring(num + 1, 1));
			str = str.Substring(num + 2, str.Length - (num + 2));
			int num2 = str.IndexOf("\n");
			if (num2 != -1)
			{
				this.addd(str.Substring(0, num2), tem, anthor);
				this.setCanhle(str.Substring(num2 + 1), tem);
			}
			else
			{
				this.addd(str, tem, anthor);
			}
		}
		else
		{
			this.addd(str, tem, 0);
		}
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00011144 File Offset: 0x0000F544
	private void addd(string str, string tem, int anthor)
	{
		string[] array;
		if (str.IndexOf("tem") != -1)
		{
			array = Canvas.blackF.splitFontBStrInLine(str, this.w - 60 * AvMain.hd);
		}
		else
		{
			array = Canvas.tempFont.splitFontBStrInLine(str, this.w - 60 * AvMain.hd);
		}
		for (int i = 0; i < array.Length; i++)
		{
			StringObj stringObj = new StringObj(this.x0, this.y0 += CustomTab.wStr, tem + array[i]);
			stringObj.anthor = anthor;
			this.listLabel.addElement(stringObj);
		}
	}

	// Token: 0x06000215 RID: 533 RVA: 0x000111FC File Offset: 0x0000F5FC
	public void setInfo(MyHashTable list, string title, string str, sbyte idAction)
	{
		this.count = 0L;
		this.ww = 0;
		this.center = null;
		if ((int)idAction != -1)
		{
			this.center = new Command(T.selectt, new CustomTab.IActionCT(idAction));
		}
		this.idAction = idAction;
		this.ww = 0;
		this.wTab = Canvas.tempFont.getWidth(title) + 80 * AvMain.hd;
		if (this.wTab < 80 * AvMain.hd)
		{
			this.wTab = 80 * AvMain.hd;
		}
		this.listImg = list;
		this.title = title;
		this.strTemp = str;
		this.listLabel.removeAllElements();
		this.listPic.removeAllElements();
		bool flag = false;
		this.x0 = 0;
		this.y0 = -10;
		for (;;)
		{
			int num = str.IndexOf("µ");
			if (num == -1)
			{
				break;
			}
			string text = str.Substring(0, num);
			str = str.Substring(num + 1, str.Length - (num + 1));
			if (flag)
			{
				int num2 = text.IndexOf(",");
				string text2 = text.Substring(0, num2);
				text = text.Substring(num2 + 1, text.Length - (num2 + 1));
				int num3 = text.IndexOf(",");
				int num4 = int.Parse(text.Substring(0, num3));
				text = text.Substring(num3 + 1, text.Length - (num3 + 1));
				bool flag2 = int.Parse(text) != 0;
				Image image = (Image)this.listImg.get(text2 + string.Empty);
				int num5 = 0;
				if (num4 == 17)
				{
					num5 = 1;
				}
				else if (num4 == 24)
				{
					num5 = 2;
				}
				PictureObj pictureObj = new PictureObj(int.Parse(text2), num5, this.y0 + CustomTab.wStr + 5, num4, flag2);
				pictureObj.w = image.getWidth();
				pictureObj.h = image.getHeight();
				if (flag2)
				{
					PictureObj pictureObj2 = (PictureObj)this.listPic.elementAt(this.listPic.size() - 1);
					int height = ((Image)this.listImg.get(pictureObj2.ID + string.Empty)).getHeight();
					if (image.getHeight() > height)
					{
						pictureObj2.y += image.getHeight() - height;
					}
					pictureObj.y = pictureObj2.y + height - image.getHeight();
				}
				this.y0 = pictureObj.y + image.getHeight() - 10;
				this.listPic.addElement(pictureObj);
				text = string.Empty;
			}
			flag = !flag;
			for (;;)
			{
				int num6 = text.IndexOf("¶");
				if (num6 != -1)
				{
					string text3 = text.Substring(0, num6);
					text = text.Substring(num6 + 1, text.Length - (num6 + 1));
					try
					{
						this.setFont("¶" + text3);
						this.y0 -= CustomTab.wStr / 2;
					}
					catch (Exception e)
					{
						Out.logError(e);
						this.setFont(text3);
					}
				}
				else
				{
					if (!text.Equals(string.Empty))
					{
						this.setFont(text.Substring(0, text.Length - 1));
					}
					if (str.IndexOf("µ") != -1 || str.IndexOf("¶") == -1)
					{
						break;
					}
					text = str;
					str = string.Empty;
				}
			}
		}
		this.setFont(str);
		this.xChange = 9 * AvMain.hd;
		int num7 = 0;
		for (int i = 0; i < this.listLabel.size(); i++)
		{
			StringObj stringObj = (StringObj)this.listLabel.elementAt(i);
			FontX fontX;
			if (stringObj.str.IndexOf("tem") != -1)
			{
				fontX = Canvas.blackF;
			}
			else
			{
				fontX = Canvas.tempFont;
			}
			int width = fontX.getWidth(stringObj.str);
			if (width + 100 * AvMain.hd > num7)
			{
				num7 = width + 100 * AvMain.hd;
			}
		}
		if (num7 < this.w)
		{
			this.w = num7;
			this.xChange = 10 * AvMain.hd;
		}
		if (this.y0 + 10 + CustomTab.wStr * 2 < this.h - 30)
		{
			this.h = this.y0 + 10 + CustomTab.wStr * 2 + 20;
		}
		if (this.w > Canvas.w - 100 * AvMain.hd)
		{
			this.w = Canvas.w;
			this.h = Canvas.hCan;
		}
		if (this.w < this.wTab * 2)
		{
			this.w = this.wTab * 2;
		}
		if (this.w > Canvas.w)
		{
			this.w = Canvas.w;
		}
		CustomTab.cmyLim = this.y0 - (this.h - (int)PaintPopup.hTab - 2 * AvMain.hDuBox - CustomTab.wStr * 2);
		if (CustomTab.cmyLim < 0)
		{
			CustomTab.cmyLim = 0;
		}
		CustomTab.cmy = (CustomTab.cmtoY = 0);
		this.x = (Canvas.w - this.w) / 2;
		this.y = (Canvas.hCan - this.h) / 2;
		this.time = (long)Environment.TickCount;
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0001178C File Offset: 0x0000FB8C
	public override void updateKey()
	{
		if ((long)Environment.TickCount - this.time >= 1000L)
		{
			if (CustomTab.timeSub != null)
			{
				for (int i = 0; i < CustomTab.timeSub.Length; i++)
				{
					if (CustomTab.timeSub[i] > 0)
					{
						CustomTab.timeSub[i]--;
						if (CustomTab.timeSub[i] == 0)
						{
							FarmService.gI().doStealInfo();
						}
					}
				}
			}
			this.time = (long)Environment.TickCount;
		}
		this.count += 1L;
		bool flag = false;
		if (CustomTab.yL != 0)
		{
			CustomTab.yL += -CustomTab.yL >> 1;
		}
		if (CustomTab.yL == -1)
		{
			CustomTab.yL = 0;
		}
		int num = Canvas.w - 45 * AvMain.hd;
		int num2 = 15 * AvMain.hd;
		if (this.w != Canvas.w)
		{
			num = this.x + this.w - 20 * AvMain.hd;
			num2 = this.y + 20 * AvMain.hd;
		}
		if (Canvas.isPointerClick && Canvas.isPointer(num, num2, 40 * AvMain.hd, 40 * AvMain.hd))
		{
			this.countClose = 5;
			this.tranKey = true;
			Canvas.isPointerClick = false;
		}
		if (this.tranKey)
		{
			if (Canvas.isPointerDown && !Canvas.isPointer(num, num2, 40 * AvMain.hd, 40 * AvMain.hd))
			{
				this.countClose = 0;
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.tranKey = false;
				if ((int)this.countClose == 5)
				{
					this.countClose = 0;
					this.listLabel.removeAllElements();
					this.listPic.removeAllElements();
					this.listImg.clear();
					base.close();
					Canvas.endDlg();
					CustomTab.me = null;
				}
			}
		}
		if (Canvas.isPointerClick && Canvas.isPointer(this.x, this.y, this.w, this.h) && !this.trans)
		{
			this.pa = CustomTab.cmy;
			this.trans = true;
			this.vY = 0;
		}
		if (this.trans)
		{
			if (Canvas.isPointerDown)
			{
				int num3 = Canvas.dy();
				if (Canvas.gameTick % 3 == 0)
				{
					this.dyTran = Canvas.py;
					this.timePointY = this.count;
				}
				this.vY = 0;
				CustomTab.cmtoY = this.pa + num3;
				if (CustomTab.cmtoY < 0 || CustomTab.cmtoY > CustomTab.cmyLim)
				{
					CustomTab.cmtoY = this.pa + num3 / 2;
				}
				CustomTab.cmy = CustomTab.cmtoY;
			}
			if (Canvas.isPointerRelease)
			{
				int num4 = (int)(this.count - this.timePointY);
				int num5 = this.dyTran - Canvas.py;
				if (CRes.abs(num5) > 40 && num4 < 10 && CustomTab.cmtoY > 0 && CustomTab.cmtoY < CustomTab.cmyLim)
				{
					this.vY = num5 / num4 * 10;
				}
				this.timePointY = -1L;
				this.trans = false;
			}
		}
		if (Canvas.keyHold[2])
		{
			CustomTab.cmtoY -= 14;
			flag = true;
		}
		else if (Canvas.keyHold[8])
		{
			flag = true;
			CustomTab.cmtoY += 14;
		}
		if (flag)
		{
			if (CustomTab.cmtoY < 0)
			{
				CustomTab.cmtoY = 0;
			}
			if (CustomTab.cmtoY > CustomTab.cmyLim)
			{
				CustomTab.cmtoY = CustomTab.cmyLim;
			}
		}
		if (CustomTab.cmy != CustomTab.cmtoY)
		{
			CustomTab.cmvy = CustomTab.cmtoY - CustomTab.cmy << 2;
			CustomTab.cmdy += CustomTab.cmvy;
			CustomTab.cmy += CustomTab.cmdy >> 4;
			CustomTab.cmdy &= 15;
		}
		if (global::Math.abs(CustomTab.cmtoY - CustomTab.cmy) < 15 && CustomTab.cmy < 0)
		{
			CustomTab.cmtoY = 0;
		}
		if (global::Math.abs(CustomTab.cmtoY - CustomTab.cmy) < 10 && CustomTab.cmy > CustomTab.cmyLim)
		{
			CustomTab.cmtoY = CustomTab.cmyLim;
		}
		if (this.vY != 0)
		{
			int num6 = this.h - (int)PaintPopup.hTab - 8 * AvMain.hd;
			if (CustomTab.cmy < 0 || CustomTab.cmy > CustomTab.cmyLim)
			{
				this.vY -= this.vY / 4;
				CustomTab.cmy += this.vY / 20;
				if (this.vY / 10 <= 1)
				{
					this.vY = 0;
				}
			}
			if (CustomTab.cmy < 0)
			{
				if (CustomTab.cmy < -num6 / 2)
				{
					CustomTab.cmy = -num6 / 2;
					CustomTab.cmtoY = 0;
					this.vY = 0;
				}
			}
			else if (CustomTab.cmy > CustomTab.cmyLim)
			{
				if (CustomTab.cmy < CustomTab.cmyLim + num6 / 2)
				{
					CustomTab.cmy = CustomTab.cmyLim + num6 / 2;
					CustomTab.cmtoY = CustomTab.cmyLim;
					this.vY = 0;
				}
			}
			else
			{
				CustomTab.cmy += this.vY / 10;
			}
			CustomTab.cmtoY = CustomTab.cmy;
			this.vY -= this.vY / 10;
			if (this.vY / 10 == 0)
			{
				this.vY = 0;
			}
		}
		else if (CustomTab.cmy < 0)
		{
			CustomTab.cmtoY = 0;
		}
		else if (CustomTab.cmy > CustomTab.cmyLim)
		{
			CustomTab.cmtoY = CustomTab.cmyLim;
		}
		base.updateKey();
	}

	// Token: 0x06000217 RID: 535 RVA: 0x00011D4C File Offset: 0x0001014C
	public override void paint(MyGraphics g)
	{
		Canvas.resetTransNotZoom(g);
		Canvas.paint.paintTransBack(g);
		Canvas.paint.paintBoxTab(g, this.x, this.y, this.h, this.w, 0, PaintPopup.gI().wSub, this.wTab, 40 * AvMain.hd, 1, 1, PaintPopup.gI().count, PaintPopup.gI().colorTab, this.title, -1, -1, false, this.w == Canvas.w, null, 0f, null);
		g.setClip((float)(this.x + 4), (float)(this.y + 45 * AvMain.hd), (float)(this.w - 8), (float)(this.h - 40 * AvMain.hd - 10));
		g.translate((float)(this.x + this.xChange), (float)(this.y + 35 * AvMain.hd));
		g.translate(0f, (float)(-(float)CustomTab.cmy));
		g.setColor(0);
		int num = 0;
		for (int i = 0; i < this.listLabel.size(); i++)
		{
			StringObj stringObj = (StringObj)this.listLabel.elementAt(i);
			if (stringObj.y > CustomTab.cmy - 10 && stringObj.y < CustomTab.cmy + this.h)
			{
				if (stringObj.str.Length > 2 && stringObj.str.Substring(0, 1).Equals("¶"))
				{
					int color = int.Parse(stringObj.str.Substring(1, stringObj.str.Length - 1), NumberStyles.HexNumber);
					PaintPopup.fill(stringObj.x, stringObj.y, Canvas.w - stringObj.x * 2, 1, color, g);
				}
				else
				{
					int num2 = stringObj.x;
					if (stringObj.anthor == 2)
					{
						num2 += (this.w - 30) / 2 + 4;
					}
					else if (stringObj.anthor == 1)
					{
						num2 += this.w - 30 + 10;
					}
					if (stringObj.str.Length > 2 && stringObj.str.Substring(0, 1).Equals("Ę"))
					{
						Canvas.blackF.drawString(g, stringObj.str.Substring(1, stringObj.str.Length - 1), num2, stringObj.y, stringObj.anthor);
					}
					else if (stringObj.str.Length > 1 && stringObj.str.Substring(0, 1).Equals("0"))
					{
						Canvas.smallFontYellow.drawString(g, stringObj.str.Substring(1) + ((CustomTab.timeSub == null || CustomTab.timeSub[num] < 0) ? string.Empty : string.Concat(new object[]
						{
							" ",
							CustomTab.timeSub[num] / 60,
							":",
							CustomTab.timeSub[num] - CustomTab.timeSub[num] / 60 * 60
						})), num2, stringObj.y + (int)AvMain.hNormal / 2 - (int)AvMain.hSmall / 2 + 4, stringObj.anthor);
						num++;
					}
					else
					{
						Canvas.tempFont.drawString(g, stringObj.str, num2, stringObj.y, stringObj.anthor);
					}
				}
			}
		}
		for (int j = 0; j < this.listPic.size(); j++)
		{
			PictureObj pictureObj = (PictureObj)this.listPic.elementAt(j);
			if (pictureObj.y + pictureObj.h > CustomTab.cmy && pictureObj.y < CustomTab.cmy + this.h)
			{
				g.drawImage((Image)this.listImg.get(pictureObj.ID + string.Empty), (float)(pictureObj.x * ((this.w - this.xChange * 2) / 2)), (float)pictureObj.y, pictureObj.orthor);
			}
		}
		base.paint(g);
		if (this.w == Canvas.w)
		{
			g.drawImage(ListScr.imgCloseTabFull[(int)this.countClose / 3], (float)(Canvas.w - 25 * AvMain.hd), (float)(35 * AvMain.hd), 3);
		}
		else
		{
			g.drawImage(ListScr.imgCloseTab[(int)this.countClose / 3], (float)(this.x + this.w - 2 * AvMain.hd), (float)(this.y + 40 * AvMain.hd), 3);
		}
	}

	// Token: 0x04000270 RID: 624
	public static CustomTab me;

	// Token: 0x04000271 RID: 625
	public int x;

	// Token: 0x04000272 RID: 626
	public int xChange;

	// Token: 0x04000273 RID: 627
	public int y;

	// Token: 0x04000274 RID: 628
	public int w;

	// Token: 0x04000275 RID: 629
	public int h;

	// Token: 0x04000276 RID: 630
	public MyVector listLabel = new MyVector();

	// Token: 0x04000277 RID: 631
	public MyVector listPic = new MyVector();

	// Token: 0x04000278 RID: 632
	public MyHashTable listImg;

	// Token: 0x04000279 RID: 633
	public string title = string.Empty;

	// Token: 0x0400027A RID: 634
	public string strTemp = string.Empty;

	// Token: 0x0400027B RID: 635
	public int selected;

	// Token: 0x0400027C RID: 636
	public int x0 = 5;

	// Token: 0x0400027D RID: 637
	public int y0 = 5;

	// Token: 0x0400027E RID: 638
	private int wTab = 30;

	// Token: 0x0400027F RID: 639
	public sbyte idAction;

	// Token: 0x04000280 RID: 640
	public sbyte countClose;

	// Token: 0x04000281 RID: 641
	private long time;

	// Token: 0x04000282 RID: 642
	public static int[] timeSub;

	// Token: 0x04000283 RID: 643
	public static int cmtoY;

	// Token: 0x04000284 RID: 644
	public static int cmy;

	// Token: 0x04000285 RID: 645
	public static int cmdy;

	// Token: 0x04000286 RID: 646
	public static int cmvy;

	// Token: 0x04000287 RID: 647
	public static int cmyLim;

	// Token: 0x04000288 RID: 648
	public static int yL;

	// Token: 0x04000289 RID: 649
	public static int wStr = 14;

	// Token: 0x0400028A RID: 650
	private int ww;

	// Token: 0x0400028B RID: 651
	private bool trans;

	// Token: 0x0400028C RID: 652
	private bool tranKey;

	// Token: 0x0400028D RID: 653
	private bool changeFocus;

	// Token: 0x0400028E RID: 654
	public int pa;

	// Token: 0x0400028F RID: 655
	public int pb;

	// Token: 0x04000290 RID: 656
	public int vY;

	// Token: 0x04000291 RID: 657
	public int dyTran;

	// Token: 0x04000292 RID: 658
	public bool transY;

	// Token: 0x04000293 RID: 659
	private long timePointY;

	// Token: 0x04000294 RID: 660
	private long count;

	// Token: 0x02000034 RID: 52
	private class IActionCT : IAction
	{
		// Token: 0x06000219 RID: 537 RVA: 0x0001222C File Offset: 0x0001062C
		public IActionCT(sbyte idAction)
		{
			this.idAction = idAction;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0001223B File Offset: 0x0001063B
		public void perform()
		{
			GlobalService.gI().doCustomTab(this.idAction);
		}

		// Token: 0x04000295 RID: 661
		private readonly sbyte idAction;
	}
}
