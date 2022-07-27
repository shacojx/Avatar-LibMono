using System;

// Token: 0x02000067 RID: 103
public class PopupShop : MyScreen
{
	// Token: 0x06000382 RID: 898 RVA: 0x0001FBB4 File Offset: 0x0001DFB4
	public PopupShop()
	{
		PopupShop.hT = (int)AvMain.hBlack + 2;
		PopupShop.wCell = 45 * AvMain.hd;
		PopupShop.init();
		this.initCmd();
		PopupShop.hPrice = 25 * (2 - AvMain.hd) + 40 * (Canvas.stypeInt + 1) + 10 * (AvMain.hd - 1) + MsgDlg.hCell + (AvMain.hd - 1) * 40;
		PopupShop.imgScroll = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/scroll"), 12 * AvMain.hd, 12 * AvMain.hd);
		PopupShop.imgShadow = Image.createImagePNG(T.getPath() + "/temp/shadowbox");
		PopupShop.imgTimeUse = new Image[2];
		for (int i = 0; i < 2; i++)
		{
			PopupShop.imgTimeUse[i] = Image.createImagePNG(T.getPath() + "/iconMenu/perUse" + i);
		}
		PopupShop.imgTimeUsePer = new FrameImage(Image.createImagePNG(T.getPath() + "/iconMenu/perUse2"), 3, (AvMain.hd != 2) ? 4 : 9);
		PopupShop.imgCell = new Image[2];
		for (int j = 0; j < 2; j++)
		{
			PopupShop.imgCell[j] = Image.createImagePNG(T.getPath() + "/iconMenu/focusCell" + j);
		}
	}

	// Token: 0x06000383 RID: 899 RVA: 0x0001FD25 File Offset: 0x0001E125
	public static PopupShop gI()
	{
		if (PopupShop.me == null)
		{
			PopupShop.me = new PopupShop();
		}
		return PopupShop.me;
	}

	// Token: 0x06000384 RID: 900 RVA: 0x0001FD40 File Offset: 0x0001E140
	public override void switchToMe()
	{
		PopupShop.lastScr = Canvas.currentMyScreen;
		this.xL = Canvas.h + 50;
		this.fliped = Canvas.getSecond();
		PopupShop.isTransFocus = true;
		PopupShop.wPrice = 86 + 60 * Canvas.stypeInt;
		PopupShop.isHorizontal = false;
		PopupShop.isQuaTrang = false;
		base.switchToMe();
	}

	// Token: 0x06000385 RID: 901 RVA: 0x0001FD98 File Offset: 0x0001E198
	public override void commandTab(int index)
	{
		if (index == 1)
		{
			this.doSelect();
		}
	}

	// Token: 0x06000386 RID: 902 RVA: 0x0001FDB1 File Offset: 0x0001E1B1
	public override void commandAction(int index)
	{
		PopupShop.lastScr.commandAction(index);
	}

	// Token: 0x06000387 RID: 903 RVA: 0x0001FDC0 File Offset: 0x0001E1C0
	public override void close()
	{
		Canvas.cameraList.close();
		this.isFull = false;
		PopupShop.lastScr.switchToMe();
		MapScr.avatarShop = null;
		PopupShop.isHorizontal = false;
		this.center = null;
		if (Canvas.isInitChar)
		{
			if (LoadMap.TYPEMAP == 25 && Welcome.indexFarmPath != 0)
			{
				Canvas.welcome = new Welcome();
				if (Welcome.indexFarmPath == 2)
				{
					Welcome.indexFarmPath = 3;
				}
				Canvas.welcome.initFarmPath(MapScr.instance);
				GameMidlet.avatar.direct = Base.LEFT;
			}
			else if (LoadMap.TYPEMAP == 57)
			{
				Canvas.welcome = new Welcome();
				Canvas.welcome.initShop(MapScr.instance);
			}
		}
	}

	// Token: 0x06000388 RID: 904 RVA: 0x0001FE7E File Offset: 0x0001E27E
	public void initCmd()
	{
		this.center = new Command(T.selectt, 1);
		this.setPosCenter();
	}

	// Token: 0x06000389 RID: 905 RVA: 0x0001FE98 File Offset: 0x0001E298
	public void setPosCenter()
	{
		if (this.center != null && (this.center.caption == null || this.center.caption.Equals(string.Empty)))
		{
			this.center = null;
		}
		if (this.left != null && (this.left.caption == null || this.left.caption.Equals(string.Empty)))
		{
			this.left = null;
		}
		int num = 0;
		if (this.left != null)
		{
			num++;
		}
		if (this.center != null)
		{
			num++;
		}
		if (this.right != null)
		{
			num++;
		}
		int num2 = Canvas.cameraList.y + PopupShop.wCell * 2 + 2 * AvMain.hd;
		int num3 = PaintPopup.gI().h - (Canvas.cameraList.y - PopupShop.y) - PopupShop.wCell * 2;
		int num4 = PaintPopup.hButtonSmall - 5 * AvMain.hd;
		if (num == 2)
		{
			num4 += 10 * AvMain.hd;
		}
		int num5 = num2 + num3 / 2 - num4 * num / 2;
		num3 /= 2;
		num2 = Canvas.cameraList.y + PopupShop.wCell * 2;
		if (this.center != null)
		{
			this.center.x = PopupShop.x + PopupShop.w - PaintPopup.wButtonSmall / 2 - 10 * AvMain.hd;
			this.center.y = num5;
			num5 += num4;
		}
		if (this.left != null)
		{
			this.left.x = PopupShop.x + PopupShop.w - PaintPopup.wButtonSmall / 2 - 10 * AvMain.hd;
			this.left.y = num5;
			num5 += num4;
		}
		if (this.right != null)
		{
			this.right.x = PopupShop.x + PopupShop.w - PaintPopup.wButtonSmall / 2 - 10 * AvMain.hd;
			this.right.y = num5;
			num5 += num4;
		}
	}

	// Token: 0x0600038A RID: 906 RVA: 0x000200A4 File Offset: 0x0001E4A4
	public static void init()
	{
		PopupShop.w = PopupShop.wCell * 6 + 11 + AvMain.hDuBox + 2;
		PopupShop.h = PopupShop.wCell * 5 + 40 + AvMain.hDuBox * AvMain.hd;
		PopupShop.x = Canvas.hw - PopupShop.wCell * 6 / 2;
	}

	// Token: 0x0600038B RID: 907 RVA: 0x000200F7 File Offset: 0x0001E4F7
	public static void addStr(string str)
	{
		if (str != null)
		{
			PopupShop.strDes.addElement(new StringObj(str, Canvas.tempFont.getWidth(str)));
		}
	}

	// Token: 0x0600038C RID: 908 RVA: 0x0002011C File Offset: 0x0001E51C
	public void addElement(string[] name1, MyVector[] listCell1, MyVector cmd, sbyte[] idIcon)
	{
		PopupShop.focusTap = 0;
		this.listCell = listCell1;
		this.left = (this.center = (this.right = null));
		this.listCmdL = new Command[listCell1.Length];
		this.listCmdR = new Command[listCell1.Length];
		this.isDuCell = new bool[listCell1.Length];
		this.textTop = new string[listCell1.Length];
		this.listCmd = cmd;
		PopupShop.name = name1;
		PopupShop.numTap = this.listCell.Length;
		PaintPopup.gI().setInfo(PopupShop.name[PopupShop.focusTap], PopupShop.w, PopupShop.h, PopupShop.numTap, 0, PopupShop.name, idIcon);
		PopupShop.x = PaintPopup.gI().x;
		PopupShop.y = PaintPopup.gI().y;
		this.setCmyLim();
	}

	// Token: 0x0600038D RID: 909 RVA: 0x000201F1 File Offset: 0x0001E5F1
	public void setHorizontal(bool isHori)
	{
		PopupShop.isHorizontal = isHori;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x000201F9 File Offset: 0x0001E5F9
	public override void closeTabAll()
	{
		this.close();
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00020204 File Offset: 0x0001E604
	public void setCmyLim()
	{
		try
		{
			PopupShop.focus = 0;
			PopupShop.hDuHori = 0;
			if (PopupShop.isHorizontal || this.isDuCell[PopupShop.focusTap])
			{
				PopupShop.hDuHori = 25 * AvMain.hd;
			}
			if (this.listCell[PopupShop.focusTap] != null)
			{
				PopupShop.hAllCell = this.listCell[PopupShop.focusTap].size() / 5;
				PopupShop.numH = 2;
				if (this.listCell[PopupShop.focusTap].size() % 5 != 0)
				{
					PopupShop.hAllCell++;
				}
				if (PopupShop.hAllCell < PopupShop.numH || PopupShop.isHorizontal)
				{
					PopupShop.hAllCell = PopupShop.numH;
				}
			}
			int num = 1;
			if (this.listCell[PopupShop.focusTap] == null)
			{
				PopupShop.num = 1;
			}
			else
			{
				num = this.listCell[PopupShop.focusTap].size();
				PopupShop.num = 6;
				if (PopupShop.isHorizontal)
				{
					PopupShop.num = num / 2 + 1;
					if (PopupShop.num < 6)
					{
						PopupShop.num = 6;
					}
				}
			}
			if (PopupShop.numH > 2 || this.isDuCell[PopupShop.focusTap])
			{
				PopupShop.duCam = 0;
			}
			Canvas.cameraList.setInfo(PaintPopup.gI().x + (PopupShop.w - 6 * PopupShop.wCell) / 2, PaintPopup.gI().y + (int)PaintPopup.hTab + ((!PopupShop.isHorizontal && !this.isDuCell[PopupShop.focusTap]) ? 0 : PopupShop.hDuHori) + AvMain.hDuBox, PopupShop.wCell, PopupShop.wCell, PopupShop.wCell * PopupShop.num, PopupShop.wCell * PopupShop.hAllCell, PopupShop.wCell * 6, (PopupShop.numH != 5) ? (PopupShop.numH * PopupShop.wCell - ((!PopupShop.isHorizontal) ? PopupShop.duCam : 0)) : (PopupShop.h - (int)PaintPopup.hTab - AvMain.hDuBox * 2), num);
			Canvas.cameraList.isQuaTrang = PopupShop.isQuaTrang;
			this.setCaption();
			PaintPopup.gI().setNameAndFocus(PopupShop.name[PopupShop.focusTap], PopupShop.focusTap);
			this.setSelected(PopupShop.focus, false);
			this.setPosCenter();
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000390 RID: 912 RVA: 0x00020478 File Offset: 0x0001E878
	public void setDuCell(bool isa, int index, string text)
	{
		this.isDuCell[index] = isa;
		this.textTop[index] = text;
		this.setCmyLim();
	}

	// Token: 0x06000391 RID: 913 RVA: 0x00020494 File Offset: 0x0001E894
	private void doSelect()
	{
		if (PopupShop.focus < this.listCell[PopupShop.focusTap].size())
		{
			((Command)this.listCell[PopupShop.focusTap].elementAt(PopupShop.focus)).action.perform();
			this.setCaption();
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x000204E8 File Offset: 0x0001E8E8
	public override void update()
	{
		PaintPopup.gI().update();
		PopupShop.lastScr.update();
		if (this.xL != 0)
		{
			this.xL += -this.xL >> 1;
		}
		if (this.xL == -1)
		{
			this.xL = 0;
		}
		if (this.listCell[PopupShop.focusTap] != null)
		{
			int num = this.listCell[PopupShop.focusTap].size();
			for (int i = 0; i < num; i++)
			{
				if (PopupShop.isTransFocus)
				{
					((Command)this.listCell[PopupShop.focusTap].elementAt(i)).update();
				}
			}
		}
		if (this.listCell[PopupShop.focusTap] == null)
		{
			((Command)this.listCmd.elementAt(PopupShop.focusTap)).update();
		}
		if (PopupShop.isHorizontal)
		{
			float num2 = (float)((PopupShop.num * PopupShop.wCell - Canvas.cameraList.disX) / 3);
			this.indexScroll = (int)(CameraList.cmtoX / num2);
			if (this.indexScroll > 2)
			{
				this.indexScroll = 2;
			}
		}
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00020608 File Offset: 0x0001EA08
	public void setCmdLeft(Command cmd, int index)
	{
		this.listCmdL[index] = cmd;
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00020613 File Offset: 0x0001EA13
	public void setCmdRight(Command cmd, int index)
	{
		this.listCmdR[index] = cmd;
	}

	// Token: 0x06000395 RID: 917 RVA: 0x0002061E File Offset: 0x0001EA1E
	public override void updateKey()
	{
		this.updateKeyMain();
		if (!this.isHide)
		{
			base.updateKey();
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00020637 File Offset: 0x0001EA37
	public void setNumberPrice(int dir)
	{
		PopupShop.numberPrice += dir;
		if (PopupShop.numberPrice < 0)
		{
			PopupShop.numberPrice = 99;
		}
		if (PopupShop.numberPrice > 99)
		{
			PopupShop.numberPrice = 0;
		}
		this.setCaption();
	}

	// Token: 0x06000397 RID: 919 RVA: 0x00020670 File Offset: 0x0001EA70
	public override void setSelected(int se, bool isAc)
	{
		if (this.listCell[PopupShop.focusTap] == null)
		{
			return;
		}
		if (PopupShop.focus == se && this.center != null && isAc && this.center == null)
		{
			this.center.perform();
		}
		if (this.center != null && this.listCmdL[PopupShop.focusTap] != null)
		{
			this.left = this.listCmdL[PopupShop.focusTap];
			this.right = this.listCmdR[PopupShop.focusTap];
			if (this.listCell[PopupShop.focusTap] != null && PopupShop.focus < this.listCell[PopupShop.focusTap].size())
			{
				this.center = (Command)this.listCell[PopupShop.focusTap].elementAt(PopupShop.focus);
			}
			this.setPosCenter();
		}
		else
		{
			this.left = null;
		}
		PopupShop.focus = se;
		this.setCaption();
	}

	// Token: 0x06000398 RID: 920 RVA: 0x0002076C File Offset: 0x0001EB6C
	public void updateKeyMain()
	{
		int num = PaintPopup.gI().setupdateTab();
		if (num != 0)
		{
			this.setTap(num);
			Canvas.isPointerClick = false;
		}
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00020798 File Offset: 0x0001EB98
	public void setTap(int dir)
	{
		PopupShop.focusTap += dir;
		if (PopupShop.focusTap == PopupShop.numTap)
		{
			PopupShop.focusTap = 0;
		}
		if (PopupShop.focusTap < 0)
		{
			PopupShop.focusTap = PopupShop.numTap - 1;
		}
		this.left = (this.center = (this.right = null));
		this.setCmyLim();
		if (this.listCmdL != null && this.listCmdL[PopupShop.focusTap] != null)
		{
			this.left = this.listCmdL[PopupShop.focusTap];
			if (this.listCmdR != null)
			{
				this.right = this.listCmdR[PopupShop.focusTap];
			}
			else
			{
				this.right = null;
			}
			this.left.x = PopupShop.x + PopupShop.w - MyScreen.wTab / 2 - 15 * AvMain.hd;
			this.left.y = PopupShop.y + PopupShop.h - PaintPopup.hButtonSmall - 20 * AvMain.hd;
			this.setPosCenter();
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x000208A8 File Offset: 0x0001ECA8
	public void setCaption()
	{
		if (this.listCell[PopupShop.focusTap] != null && PopupShop.focus < this.listCell[PopupShop.focusTap].size())
		{
			this.center = (Command)this.listCell[PopupShop.focusTap].elementAt(PopupShop.focus);
			this.setPosCenter();
		}
		else if (this.listCmd != null && PopupShop.focusTap < this.listCmd.size())
		{
			Command command = (Command)this.listCmd.elementAt(PopupShop.focusTap);
			if (command != null)
			{
				this.center = command;
				this.setPosCenter();
			}
		}
		else
		{
			this.center = null;
		}
		PopupShop.isTransFocus = true;
		this.fliped = Canvas.getSecond();
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00020973 File Offset: 0x0001ED73
	public override void setHidePointer(bool isa)
	{
		this.isHide = isa;
	}

	// Token: 0x0600039C RID: 924 RVA: 0x0002097C File Offset: 0x0001ED7C
	public override void paint(MyGraphics g)
	{
		PopupShop.lastScr.paintMain(g);
		Canvas.resetTrans(g);
		PaintPopup.gI().paint(g);
		g.setColor(0);
		g.translate((float)Canvas.cameraList.x, (float)Canvas.cameraList.y);
		if (PopupShop.isHorizontal && this.listCell[PopupShop.focusTap] != null)
		{
			int num = this.xMoney;
			if (CRes.abs(num) > 200)
			{
				num = 0;
			}
			g.setClip((float)(-5 * AvMain.hd), (float)(-2 - PopupShop.hDuHori + 13 * AvMain.hd - 20 * AvMain.hd), (float)(PopupShop.w - 5 * AvMain.hd), 100f);
			g.drawImage(MyInfoScr.gI().imgIcon[0], (float)(num + 15 * AvMain.hd), (float)(-2 - PopupShop.hDuHori + 13 * AvMain.hd), 3);
			g.drawImage(MyInfoScr.gI().imgIcon[1], (float)(num + 115 * AvMain.hd), (float)(-2 - PopupShop.hDuHori + 13 * AvMain.hd), 3);
			g.drawImage(MyInfoScr.gI().imgIcon[4], (float)(num + 200 * AvMain.hd), (float)(-2 - PopupShop.hDuHori + 13 * AvMain.hd), 3);
			g.drawImage(MyInfoScr.gI().imgIcon[2], (float)(num + 280 * AvMain.hd), (float)(-2 - PopupShop.hDuHori + 13 * AvMain.hd), 3);
			Canvas.tempFont.drawString(g, Canvas.getMoneys(GameMidlet.avatar.money[0]) + string.Empty, num + 15 * AvMain.hd + MyInfoScr.gI().imgIcon[0].getWidth() / 2 + 5 * AvMain.hd, -2 - PopupShop.hDuHori + 10 * AvMain.hd - Canvas.tempFont.getHeight() / 2, 0);
			Canvas.tempFont.drawString(g, Canvas.getMoneys(GameMidlet.avatar.money[2]), num + 115 * AvMain.hd + MyInfoScr.gI().imgIcon[1].getWidth() / 2 + 5 * AvMain.hd, -2 - PopupShop.hDuHori + 10 * AvMain.hd - Canvas.tempFont.getHeight() / 2, 0);
			Canvas.tempFont.drawString(g, Canvas.getMoneys(GameMidlet.avatar.luongKhoa) + string.Empty, num + 200 * AvMain.hd + MyInfoScr.gI().imgIcon[1].getWidth() / 2 + 5 * AvMain.hd, -2 - PopupShop.hDuHori + 10 * AvMain.hd - Canvas.tempFont.getHeight() / 2, 0);
			Canvas.tempFont.drawString(g, Canvas.getMoneys(GameMidlet.avatar.money[3]) + string.Empty, num + 280 * AvMain.hd + MyInfoScr.gI().imgIcon[2].getWidth() / 2 + 5 * AvMain.hd, -2 - PopupShop.hDuHori + 10 * AvMain.hd - Canvas.tempFont.getHeight() / 2, 0);
			if (CRes.abs(this.xMoney) > 250)
			{
				this.xMoney = 0;
			}
			this.xMoney--;
		}
		if (this.listCell[PopupShop.focusTap] != null)
		{
			if (this.isDuCell[PopupShop.focusTap])
			{
				Canvas.normalFont.drawString(g, this.textTop[PopupShop.focusTap], 0, -Canvas.blackF.getHeight() - 10, 0);
			}
			g.setClip(0f, 0f, (float)(6 * PopupShop.wCell), (float)Canvas.cameraList.disY);
			if (!PopupShop.isHorizontal)
			{
				g.translate(0f, -CameraList.cmy);
			}
			else
			{
				g.translate(-CameraList.cmx, 0f);
			}
			for (int i = 0; i < PopupShop.hAllCell * PopupShop.num; i++)
			{
				if (i == PopupShop.focus && !this.isHide)
				{
					g.drawImage(PopupShop.imgCell[1], (float)(PopupShop.wCell * (i % PopupShop.num) + 2 + PopupShop.wCell / 2), (float)(PopupShop.wCell * (i / PopupShop.num) + PopupShop.wCell / 2), 3);
				}
				else
				{
					g.drawImage(PopupShop.imgCell[0], (float)(PopupShop.wCell * (i % PopupShop.num) + 2 + PopupShop.wCell / 2), (float)(PopupShop.wCell * (i / PopupShop.num) + PopupShop.wCell / 2), 3);
				}
			}
			int num2 = this.listCell[PopupShop.focusTap].size();
			int num3 = (int)CameraList.cmy / PopupShop.wCell * PopupShop.num;
			if (num3 < 0)
			{
				num3 = 0;
			}
			int num4 = (int)CameraList.cmy / PopupShop.wCell * PopupShop.num + (PopupShop.numH + 1) * PopupShop.num;
			if (num4 > this.listCell[PopupShop.focusTap].size())
			{
				num4 = this.listCell[PopupShop.focusTap].size();
			}
			int num5 = num3;
			while (num5 < num4 && num5 < num2)
			{
				g.setClip(CameraList.cmx, 0f, (float)(6 * PopupShop.wCell), (float)Canvas.cameraList.disY);
				((Command)this.listCell[PopupShop.focusTap].elementAt(num5)).paint(g, PopupShop.wCell * (num5 % PopupShop.num), PopupShop.wCell * (num5 / PopupShop.num));
				num5++;
			}
			if (!PopupShop.isHorizontal)
			{
				g.translate(0f, CameraList.cmy - (float)PopupShop.duCam);
			}
			else
			{
				g.translate(CameraList.cmx, 0f);
			}
			g.setClip(0f, 0f, (float)(PopupShop.w - 9), (float)PopupShop.h);
			if (PopupShop.numH == 2)
			{
				if (PopupShop.isHorizontal && PopupShop.strDes != null && PopupShop.focus < this.listCell[PopupShop.focusTap].size() && !this.isHide && MapScr.avatarShop != null)
				{
					MapScr.avatarShop.paintIcon(g, 25 * AvMain.hd - ((AvMain.hd != 1) ? 0 : 10), PopupShop.numH * PopupShop.wCell + PopupShop.hDuHori + (int)PaintPopup.hTab, false);
					g.translate((float)(50 * AvMain.hd - ((AvMain.hd != 1) ? 0 : 20)), 0f);
				}
				if (!this.isHide)
				{
					this.paintStringSmall(g);
				}
			}
			else
			{
				this.paintStringBig(g);
			}
		}
		else
		{
			g.setClip(-5f, 0f, (float)(PopupShop.w - 10 * AvMain.hd), (float)PopupShop.h);
			((Command)this.listCmd.elementAt(PopupShop.focusTap)).paint(g, 0, 0);
		}
		if (!this.isHide && (Canvas.welcome == null || Welcome.isOut || !Welcome.isPaintArrow))
		{
			base.paint(g);
		}
		Canvas.resetTrans(g);
		Canvas.paintPlus(g);
	}

	// Token: 0x0600039D RID: 925 RVA: 0x000210C0 File Offset: 0x0001F4C0
	public void paintStringBig(MyGraphics g)
	{
		if (Canvas.getSecond() - this.fliped < 1)
		{
			return;
		}
		if (PopupShop.strDes != null && PopupShop.focus < this.listCell[PopupShop.focusTap].size())
		{
			int num = PopupShop.focus % PopupShop.num * PopupShop.wCell - this.wStr / 2 + PopupShop.wCell / 2;
			int num2 = (int)((float)((PopupShop.focus / PopupShop.num + 1) * PopupShop.wCell) - CameraList.cmy + 5f);
			int num3 = PopupShop.strDes.size() * (int)AvMain.hBlack2 + AvMain.hDuBox * 2 + (int)AvMain.hBlack2 * 2;
			if (num2 + num3 + PopupShop.y + 12 > Canvas.h)
			{
				num2 -= num3 + PopupShop.wCell + 10;
			}
			if (num2 + PopupShop.y < 0)
			{
				num2 = -PopupShop.y;
			}
			if (num + PopupShop.x + 5 + this.wStr > Canvas.w)
			{
				num = Canvas.w - (PopupShop.x + 5 + this.wStr);
			}
			else if (num + PopupShop.x < 0)
			{
				num = -PopupShop.x;
			}
			g.setClip((float)num, (float)num2, (float)this.wStr, (float)(num3 * AvMain.hd));
			Canvas.paint.paintPopupBack(g, num, num2, this.wStr, num3, -1, false);
			num += AvMain.hDuBox;
			num2 += AvMain.hDuBox;
			for (int i = 0; i < PopupShop.strDes.size(); i++)
			{
				StringObj stringObj = (StringObj)PopupShop.strDes.elementAt(i);
				int num4 = 0;
				if (stringObj.w2 > this.wStr + 5)
				{
					stringObj.transTextLimit(this.wStr - 30);
					if (stringObj.dis >= 0)
					{
						num4 = stringObj.dis;
					}
				}
				Canvas.tempFont.drawString(g, stringObj.str, num - num4, num2 + 5 + i * PopupShop.hT, 0);
			}
		}
	}

	// Token: 0x0600039E RID: 926 RVA: 0x000212B8 File Offset: 0x0001F6B8
	public void paintStringSmall(MyGraphics g)
	{
		int num = PopupShop.w;
		if (this.center != null || this.left != null)
		{
			num = PopupShop.w / 2;
		}
		g.setClip(0f, (float)(PopupShop.numH * PopupShop.wCell), (float)(PopupShop.w - 95 * AvMain.hd), (float)(100 * AvMain.hd));
		int num2 = (int)PaintPopup.hTab + PopupShop.hDuHori + AvMain.hDuBox + PopupShop.numH * PopupShop.wCell + (PopupShop.h - ((int)PaintPopup.hTab + PopupShop.hDuHori + AvMain.hDuBox * 2 + PopupShop.numH * PopupShop.wCell)) / 2;
		if (PopupShop.strDes != null && PopupShop.focus < this.listCell[PopupShop.focusTap].size())
		{
			int num3 = PopupShop.wCell * 2;
			int num4 = PaintPopup.gI().h - (Canvas.cameraList.y - PopupShop.y) - PopupShop.wCell * 2;
			int num5 = PaintPopup.hButtonSmall - 5 * AvMain.hd;
			int num6 = num3 + num4 / 2 - PopupShop.strDes.size() * Canvas.tempFont.getHeight() / 2 - 5 * AvMain.hd;
			for (int i = 0; i < PopupShop.strDes.size(); i++)
			{
				StringObj stringObj = (StringObj)PopupShop.strDes.elementAt(i);
				int num7 = 0;
				if (stringObj.w2 > num - 25 * AvMain.hd + 5)
				{
					stringObj.transTextLimit(num - 25 * AvMain.hd - 10 * AvMain.hd);
					if (stringObj.dis >= 0)
					{
						num7 = stringObj.dis;
					}
				}
				if (PopupShop.isHorizontal)
				{
					Canvas.tempFont.drawString(g, stringObj.str, 2 - num7, num6 + i * Canvas.tempFont.getHeight(), 0);
				}
				else
				{
					Canvas.tempFont.drawString(g, stringObj.str, 2 - num7, num6 + i * Canvas.tempFont.getHeight(), 0);
				}
			}
		}
	}

	// Token: 0x0600039F RID: 927 RVA: 0x000214C1 File Offset: 0x0001F8C1
	public static void resetIsTrans()
	{
		PopupShop.isTransFocus = false;
		PopupShop.strDes.removeAllElements();
	}

	// Token: 0x04000471 RID: 1137
	public static PopupShop me;

	// Token: 0x04000472 RID: 1138
	private static string[] name;

	// Token: 0x04000473 RID: 1139
	public static int numTap;

	// Token: 0x04000474 RID: 1140
	public static int x;

	// Token: 0x04000475 RID: 1141
	public static int y;

	// Token: 0x04000476 RID: 1142
	public static int w;

	// Token: 0x04000477 RID: 1143
	public static int h;

	// Token: 0x04000478 RID: 1144
	public static int wCell;

	// Token: 0x04000479 RID: 1145
	public static int num = 6;

	// Token: 0x0400047A RID: 1146
	public static int hAllCell;

	// Token: 0x0400047B RID: 1147
	public static int numH = 5;

	// Token: 0x0400047C RID: 1148
	public static int hT;

	// Token: 0x0400047D RID: 1149
	public static int focusTap;

	// Token: 0x0400047E RID: 1150
	public MyVector[] listCell;

	// Token: 0x0400047F RID: 1151
	private MyVector listCmd;

	// Token: 0x04000480 RID: 1152
	private Command[] listCmdL;

	// Token: 0x04000481 RID: 1153
	private Command[] listCmdR;

	// Token: 0x04000482 RID: 1154
	public static int focus = 0;

	// Token: 0x04000483 RID: 1155
	public static int numberPrice = 0;

	// Token: 0x04000484 RID: 1156
	public static int duCam = 0;

	// Token: 0x04000485 RID: 1157
	public static MyVector strDes = new MyVector();

	// Token: 0x04000486 RID: 1158
	public static MyScreen lastScr;

	// Token: 0x04000487 RID: 1159
	private int xL;

	// Token: 0x04000488 RID: 1160
	private int fliped;

	// Token: 0x04000489 RID: 1161
	public bool isFull;

	// Token: 0x0400048A RID: 1162
	public static bool isTransFocus = false;

	// Token: 0x0400048B RID: 1163
	public static bool isHorizontal = false;

	// Token: 0x0400048C RID: 1164
	public static bool isSelectedTab = false;

	// Token: 0x0400048D RID: 1165
	public static bool isQuaTrang;

	// Token: 0x0400048E RID: 1166
	public static int wPrice = 0;

	// Token: 0x0400048F RID: 1167
	public static int hPrice;

	// Token: 0x04000490 RID: 1168
	public static int hDuHori;

	// Token: 0x04000491 RID: 1169
	public static Image imgShadow;

	// Token: 0x04000492 RID: 1170
	public static FrameImage imgScroll;

	// Token: 0x04000493 RID: 1171
	private int indexScroll;

	// Token: 0x04000494 RID: 1172
	public bool[] isDuCell;

	// Token: 0x04000495 RID: 1173
	public string[] textTop;

	// Token: 0x04000496 RID: 1174
	public static Image[] imgCell;

	// Token: 0x04000497 RID: 1175
	public static Image[] imgTimeUse;

	// Token: 0x04000498 RID: 1176
	public static FrameImage imgTimeUsePer;

	// Token: 0x04000499 RID: 1177
	public new bool isHide;

	// Token: 0x0400049A RID: 1178
	private int xMoney;

	// Token: 0x0400049B RID: 1179
	private int wStr = 120 * AvMain.hd;
}
