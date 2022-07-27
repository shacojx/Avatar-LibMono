using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class PaintPopup
{
	// Token: 0x0600035B RID: 859 RVA: 0x0001DD24 File Offset: 0x0001C124
	public PaintPopup()
	{
		this.wSub = 30;
		PaintPopup.hTab = (sbyte)(40 * AvMain.hd);
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0001DD51 File Offset: 0x0001C151
	public static PaintPopup gI()
	{
		if (PaintPopup.me == null)
		{
			PaintPopup.me = new PaintPopup();
		}
		return PaintPopup.me;
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0001DD6C File Offset: 0x0001C16C
	public void setInfo(string na, int wp, int hp, int num, sbyte countCloseAll, string[] nameList, sbyte[] idIcon)
	{
		this.focusTab = 0;
		PaintPopup.parent = null;
		this.isFull = false;
		this.isMenu = false;
		this.imgIcon = null;
		if (idIcon != null)
		{
			this.imgIcon = new Image[idIcon.Length][];
			for (int i = 0; i < idIcon.Length; i++)
			{
				this.imgIcon[i] = new Image[2];
				this.imgIcon[i][0] = Image.createImagePNG(string.Concat(new object[]
				{
					T.getPath(),
					"/iconShop/shop",
					idIcon[i],
					"0"
				}));
				this.imgIcon[i][1] = Image.createImagePNG(string.Concat(new object[]
				{
					T.getPath(),
					"/iconShop/shop",
					idIcon[i],
					"1"
				}));
			}
		}
		this.wSub = 70 * AvMain.hd;
		this.w = wp;
		this.h = hp;
		this.numTab = num;
		this.nameList = null;
		this.wTab = 0;
		if (nameList != null)
		{
			this.nameList = new string[nameList.Length];
			for (int j = 0; j < nameList.Length; j++)
			{
				if (nameList[j].Length > 10)
				{
					this.nameList[j] = nameList[j].Substring(0, 10).Trim() + "..";
				}
				else
				{
					this.nameList[j] = nameList[j];
				}
				int num2 = Canvas.normalFont.getWidth(this.nameList[j]) + 12 * AvMain.hd;
				if (num2 > this.wTab)
				{
					this.wTab = num2;
				}
				if (this.wTab < 65 * AvMain.hd)
				{
					this.wTab = 65 * AvMain.hd;
				}
				if (this.wTab > 100 * AvMain.hd)
				{
					this.wTab = 100 * AvMain.hd;
				}
			}
			if (nameList.Length > 3)
			{
				PaintPopup.xTab = 8 * AvMain.hd;
				PaintPopup.wTabDu = this.wTab + 4 * AvMain.hd;
			}
			this.setName(nameList[this.focusTab]);
		}
		else
		{
			this.setName(na);
			this.wTab = Canvas.normalFont.getWidth(na) + 12 * AvMain.hd;
			if (this.wTab < 65 * AvMain.hd)
			{
				this.wTab = 65 * AvMain.hd;
			}
			if (this.wTab > 100 * AvMain.hd)
			{
				this.wTab = 100 * AvMain.hd;
			}
		}
		if (idIcon != null)
		{
			this.wTab = 55 * AvMain.hd;
		}
		PaintPopup.xTab = 12 * AvMain.hd;
		PaintPopup.wTabDu = this.wTab + 10 * AvMain.hd;
		this.init();
		this.colorTab = new int[this.numTab];
		this.count = new int[this.numTab];
		this.maxTab = (this.w - this.wTab) / this.wSub;
		this.countCloseTab = countCloseAll;
		PaintPopup.cmxLim = 0f;
		if (nameList != null)
		{
			PaintPopup.cmxLim = (float)(nameList.Length * this.wSub - (this.w - 30 * AvMain.hd) + this.wSub);
			if (PaintPopup.cmxLim < 0f)
			{
				PaintPopup.cmxLim = 0f;
			}
			if (PaintPopup.cmx > PaintPopup.cmxLim)
			{
				PaintPopup.cmx = (PaintPopup.cmtoX = PaintPopup.cmxLim);
			}
		}
		else
		{
			PaintPopup.cmx = (PaintPopup.cmtoX = 0f);
		}
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0001E108 File Offset: 0x0001C508
	public void setIcon(sbyte[] idIcon)
	{
	}

	// Token: 0x0600035F RID: 863 RVA: 0x0001E10C File Offset: 0x0001C50C
	public void setFocus(int focus)
	{
		int num = 0;
		if (this.isMenu)
		{
			num = 29 * AvMain.hd;
		}
		this.focusTab = focus;
		if ((float)this.x - PaintPopup.cmx + (float)num + (float)(this.focusTab * this.wSub) <= 0f)
		{
			PaintPopup.cmtoX = (float)(this.x + num + this.focusTab * this.wSub);
		}
		else if ((float)this.x - PaintPopup.cmtoX + (float)num + (float)(this.focusTab * this.wSub) >= (float)(this.w - 30 * AvMain.hd - this.wTab))
		{
			PaintPopup.cmtoX = (float)(this.x + num + this.focusTab * this.wSub - (this.w - 30 * AvMain.hd - this.wTab));
		}
	}

	// Token: 0x06000360 RID: 864 RVA: 0x0001E1EE File Offset: 0x0001C5EE
	public void setName(string na)
	{
		this.name = na;
		this.wName = Canvas.fontWhiteBold.getWidth(this.name);
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0001E20D File Offset: 0x0001C60D
	public void init()
	{
		this.x = Canvas.hw - this.w / 2;
		this.y = (Canvas.hCan - ((!TouchScreenKeyboard.visible) ? 0 : 10)) / 2 - this.h / 2;
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0001E24C File Offset: 0x0001C64C
	public void setColor(int col, int index)
	{
		if (index != this.focusTab)
		{
			this.colorTab[index] = col;
			this.count[index] = CRes.rnd(20);
		}
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0001E274 File Offset: 0x0001C674
	public void setNameAndFocus(string na, int focus)
	{
		if (this.colorTab != null && focus < this.colorTab.Length)
		{
			this.colorTab[focus] = 0;
		}
		this.name = na;
		this.wName = Canvas.fontWhiteBold.getWidth(this.name);
		this.focusTab = focus;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0001E2C7 File Offset: 0x0001C6C7
	public void setNumTab(int num)
	{
		this.colorTab = new int[num];
		this.count = new int[num];
		this.numTab = num;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x0001E2E8 File Offset: 0x0001C6E8
	public void update()
	{
		if (this.wName > this.wTab - 15 * AvMain.hd)
		{
			if ((int)this.countName + this.wName / 2 <= (this.wTab - 15 * AvMain.hd) / 2)
			{
				if (this.countTempName == 20)
				{
					this.countName = (sbyte)((int)this.countName * -1);
				}
				this.countTempName--;
			}
			else if (this.countTempName == 0 || this.countTempName == 40)
			{
				this.countTempName = 40;
				this.countName = (sbyte)((int)this.countName - 1);
			}
			else
			{
				this.countTempName--;
			}
		}
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0001E3A8 File Offset: 0x0001C7A8
	public int setupdateTab()
	{
		this.countTouch++;
		if (Canvas.isPointerClick)
		{
			if ((int)this.countCloseTab != -1 && Canvas.isPoint(this.x + this.w + 5 - 5 * AvMain.hd - 20 * AvMain.hd, this.y + (int)PaintPopup.hTab - 6 + 3 * AvMain.hd - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
			{
				this.trans = true;
				Canvas.isPointerClick = false;
				this.countCloseTab = 5;
			}
			else
			{
				for (int i = 0; i < this.numTab; i++)
				{
					if (Canvas.isPoint(PaintPopup.xTab + this.x + i * PaintPopup.wTabDu, this.y, PaintPopup.wTabDu, (int)PaintPopup.hTab))
					{
						Canvas.isPointerClick = false;
						this.pxLast = Canvas.pyLast;
						this.isG = false;
						if (this.vY != 0)
						{
							this.isG = true;
						}
						this.pa = PaintPopup.cmtoX;
						this.timeDelay = this.countTouch;
						this.trans = true;
						this.indexFocus = i;
						break;
					}
				}
			}
		}
		if (this.trans)
		{
			int num = this.pxLast - Canvas.px;
			this.pxLast = Canvas.px;
			long num2 = (long)(this.countTouch - this.timeDelay);
			int num3 = this.wTab + 20;
			if (Canvas.isPointerDown)
			{
				if ((int)this.countCloseTab != 0 && !Canvas.isPoint(this.x + this.w + 5 - 5 * AvMain.hd - 20 * AvMain.hd, this.y + (int)PaintPopup.hTab - 6 + 3 * AvMain.hd - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
				{
					this.countCloseTab = 0;
				}
				else if (this.indexFocus != -1)
				{
					if (this.countTouch % 2 == 0)
					{
						this.dxTran = Canvas.px;
						this.timePoint = this.countTouch;
					}
					this.vY = 0;
					if (global::Math.abs(num) < 10 * AvMain.hd)
					{
						int num4 = this.x;
						int num5 = (int)((PaintPopup.cmtoX + (float)Canvas.px - (float)num4) / (float)this.wTab);
						if (num5 < 0 || num5 < this.numTab)
						{
						}
					}
					if (CRes.abs(Canvas.dy()) >= 10 * AvMain.hd)
					{
						this.isHide = true;
					}
					else if (num2 > 3L && num2 < 8L)
					{
						int countTab = Canvas.countTab;
						int num6 = (int)((PaintPopup.cmtoX + (float)Canvas.px - (float)countTab) / (float)this.wTab);
						if (num6 >= 0 && num6 < this.numTab && !this.isG)
						{
							this.isHide = false;
						}
					}
					if (PaintPopup.cmtoX < 0f || PaintPopup.cmtoX > PaintPopup.cmxLim)
					{
						PaintPopup.cmtoX = this.pa + (float)(num / 2);
						this.pa = PaintPopup.cmtoX;
					}
					else
					{
						PaintPopup.cmtoX = this.pa + (float)(num / 2);
						this.pa = PaintPopup.cmtoX;
					}
					PaintPopup.cmx = PaintPopup.cmtoX;
				}
			}
			if (Canvas.isPointerRelease)
			{
				this.trans = false;
				this.isG = false;
				if ((int)this.countCloseTab == 5)
				{
					Canvas.currentMyScreen.closeTabAll();
					this.countCloseTab = 0;
				}
				else if (this.indexFocus != -1)
				{
					int num7 = this.countTouch - this.timePoint;
					int num8 = this.dxTran - Canvas.px;
					if (CRes.abs(num8) > 40 && num7 < 10 && PaintPopup.cmtoX > 0f && PaintPopup.cmtoX < PaintPopup.cmxLim)
					{
						this.vX = (float)(num8 / num7 * 10);
					}
					this.timePoint = -1;
					if (global::Math.abs(Canvas.dx()) < 10 * AvMain.hd)
					{
						int result = this.indexFocus - this.focusTab;
						if (this.indexFocus > this.focusTab)
						{
							this.focusTab = this.indexFocus;
							return result;
						}
						if (this.indexFocus < this.focusTab)
						{
							this.focusTab = this.indexFocus;
							return result;
						}
					}
					this.indexFocus = -1;
				}
				Canvas.isPointerRelease = false;
			}
		}
		return 0;
	}

	// Token: 0x06000367 RID: 871 RVA: 0x0001E828 File Offset: 0x0001CC28
	public void paint(MyGraphics g)
	{
		Canvas.paint.paintBoxTab(g, this.x, this.y, this.h, this.w, this.focusTab, this.wSub, this.wTab, (int)PaintPopup.hTab, this.numTab, this.maxTab, this.count, this.colorTab, this.name, (sbyte)(((int)this.countCloseTab == -1) ? ((int)this.countCloseTab) : ((int)this.countCloseTab / 3)), this.countName, this.isMenu, this.isFull, this.nameList, PaintPopup.cmx, this.imgIcon);
		Canvas.resetTrans(g);
	}

	// Token: 0x06000368 RID: 872 RVA: 0x0001E8DC File Offset: 0x0001CCDC
	public static void paintCell(MyGraphics g, int x, int y, int w, int h)
	{
		PaintPopup.fill(x, y, w, h, PaintPopup.color[0], g);
		g.setColor(PaintPopup.color[2]);
		g.drawRect((float)x, (float)y, (float)w, (float)h);
		g.setColor(12450472);
		g.drawRect((float)(x + 1), (float)(y + 1), (float)(w - 2), (float)(h - 2));
		g.setColor(5738823);
		g.drawRect((float)(x + 2), (float)(y + 2), (float)(w - 4), (float)(h - 4));
	}

	// Token: 0x06000369 RID: 873 RVA: 0x0001E95B File Offset: 0x0001CD5B
	public static void fill(int x, int y, int w, int h, int color, MyGraphics g)
	{
		g.setColor(color);
		g.fillRect((float)x, (float)y, (float)w, (float)h);
	}

	// Token: 0x04000425 RID: 1061
	public static FrameImage frame;

	// Token: 0x04000426 RID: 1062
	public static PaintPopup me;

	// Token: 0x04000427 RID: 1063
	public static int[] color;

	// Token: 0x04000428 RID: 1064
	public int h;

	// Token: 0x04000429 RID: 1065
	public int w;

	// Token: 0x0400042A RID: 1066
	public int x;

	// Token: 0x0400042B RID: 1067
	public int y;

	// Token: 0x0400042C RID: 1068
	public int numTab;

	// Token: 0x0400042D RID: 1069
	public int wTab;

	// Token: 0x0400042E RID: 1070
	public int wSub = 10;

	// Token: 0x0400042F RID: 1071
	public int focusTab;

	// Token: 0x04000430 RID: 1072
	public int maxTab;

	// Token: 0x04000431 RID: 1073
	public int[] colorTab;

	// Token: 0x04000432 RID: 1074
	public int[] count;

	// Token: 0x04000433 RID: 1075
	public string name;

	// Token: 0x04000434 RID: 1076
	public string[] nameList;

	// Token: 0x04000435 RID: 1077
	public static sbyte hTab;

	// Token: 0x04000436 RID: 1078
	public sbyte countCloseTab;

	// Token: 0x04000437 RID: 1079
	public sbyte countName;

	// Token: 0x04000438 RID: 1080
	private int wName;

	// Token: 0x04000439 RID: 1081
	public static Image[][] imgMuiIOS;

	// Token: 0x0400043A RID: 1082
	public bool isMenu;

	// Token: 0x0400043B RID: 1083
	public bool isFull;

	// Token: 0x0400043C RID: 1084
	public static float cmtoX;

	// Token: 0x0400043D RID: 1085
	public static float cmx;

	// Token: 0x0400043E RID: 1086
	public static float cmdx;

	// Token: 0x0400043F RID: 1087
	public static float cmvx;

	// Token: 0x04000440 RID: 1088
	public static float cmxLim;

	// Token: 0x04000441 RID: 1089
	public static MyScreen parent;

	// Token: 0x04000442 RID: 1090
	public static int wButtonSmall;

	// Token: 0x04000443 RID: 1091
	public static int hButtonSmall;

	// Token: 0x04000444 RID: 1092
	public static int xTab;

	// Token: 0x04000445 RID: 1093
	public static int wTabDu;

	// Token: 0x04000446 RID: 1094
	private Image[][] imgIcon;

	// Token: 0x04000447 RID: 1095
	private int countTempName;

	// Token: 0x04000448 RID: 1096
	private int pxLast;

	// Token: 0x04000449 RID: 1097
	private int vY;

	// Token: 0x0400044A RID: 1098
	private int timePoint;

	// Token: 0x0400044B RID: 1099
	private int countTouch;

	// Token: 0x0400044C RID: 1100
	private int timeDelay;

	// Token: 0x0400044D RID: 1101
	private int dxTran;

	// Token: 0x0400044E RID: 1102
	private int timeOpen;

	// Token: 0x0400044F RID: 1103
	private int indexFocus = -1;

	// Token: 0x04000450 RID: 1104
	private float pa;

	// Token: 0x04000451 RID: 1105
	private float vX;

	// Token: 0x04000452 RID: 1106
	private bool isG;

	// Token: 0x04000453 RID: 1107
	private bool trans;

	// Token: 0x04000454 RID: 1108
	private bool isHide;
}
