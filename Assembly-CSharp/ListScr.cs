using System;
using System.IO;

// Token: 0x02000113 RID: 275
public class ListScr : MyScreen
{
	// Token: 0x06000791 RID: 1937 RVA: 0x0004532F File Offset: 0x0004372F
	public ListScr()
	{
		this.focus = 0;
		this.wSmall = 60 * AvMain.hd;
		this.cmdClose = new Command(T.close, 1);
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00045365 File Offset: 0x00043765
	public static ListScr gI()
	{
		if (ListScr.instance == null)
		{
			ListScr.instance = new ListScr();
		}
		return ListScr.instance;
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00045380 File Offset: 0x00043780
	public override void switchToMe()
	{
		this.selected = 0;
		if (Canvas.currentMyScreen != ListScr.gI())
		{
			this.backMyScreen = Canvas.currentMyScreen;
		}
		this.reSize();
		base.switchToMe();
		this.isHide = true;
		if (onMainMenu.isOngame)
		{
			this.right = this.cmdClose;
		}
		this.isJoinH = false;
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x000453E0 File Offset: 0x000437E0
	public void init()
	{
		Scroll.gI().init(PaintPopup.gI().h - 5 - ((int)PaintPopup.hTab + 2 * AvMain.hDuBox), ListScr.tempList.size() * this.wSmall, (int)CameraList.cmy);
		if (onMainMenu.isOngame)
		{
			Canvas.cameraList.setInfo(0, 50 * AvMain.hd, Canvas.w, this.wSmall, Canvas.w, ListScr.tempList.size() * this.wSmall, Canvas.w, Canvas.h - 50 * AvMain.hd - 4, ListScr.tempList.size());
		}
		else
		{
			Canvas.cameraList.setInfo(4 * AvMain.hd, PaintPopup.gI().y + 40 * AvMain.hd, Canvas.w - 8 * AvMain.hd, this.wSmall, Canvas.w - 8 * AvMain.hd, ListScr.tempList.size() * this.wSmall, Canvas.w - 8 * AvMain.hd, PaintPopup.gI().h - 40 * AvMain.hd - 15, ListScr.tempList.size());
		}
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x0004550D File Offset: 0x0004390D
	public override void initTabTrans()
	{
		if (!onMainMenu.isOngame)
		{
			this.reSize();
		}
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00045520 File Offset: 0x00043920
	public void reSize()
	{
		if (this.name == null)
		{
			return;
		}
		PaintPopup.gI().setInfo(this.name, Canvas.w, Canvas.hCan, 1, 0, null, null);
		PaintPopup.gI().y = 0;
		PaintPopup.gI().isFull = true;
		PaintPopup.gI().countCloseTab = -1;
		if (ListScr.tempList != null)
		{
			this.init();
		}
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x00045588 File Offset: 0x00043988
	public override void setSelected(int se, bool isAc)
	{
		if (isAc && se == this.selected)
		{
			if (this.left != null)
			{
				this.left.perform();
			}
			else
			{
				this.cmdSelected.perform();
			}
		}
		this.xCus = -20;
		if (se >= 0 && se < ListScr.tempList.size())
		{
			this.selected = se;
		}
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x000455F3 File Offset: 0x000439F3
	public override void setHidePointer(bool iss)
	{
		this.isHide = iss;
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x000455FC File Offset: 0x000439FC
	public override void closeTabAll()
	{
		this.cmdSelected = null;
		this.right = null;
		this.left = null;
		ListScr.tempList = null;
		if (onMainMenu.isOngame)
		{
			onMainMenu.gI().switchToMe();
		}
		else
		{
			Canvas.cameraList.close();
			MapScr.gI().switchToMe();
		}
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00045654 File Offset: 0x00043A54
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		g.translate(-AvCamera.gI().xCam, -AvCamera.gI().yCam);
		if (!onMainMenu.isOngame)
		{
			Canvas.loadMap.paintM(g);
			Canvas.resetTrans(g);
			Canvas.paint.paintTransBack(g);
			PaintPopup.gI().paint(g);
			g.setClip((float)(5 * AvMain.hd), 0f, (float)(Canvas.w - 10 * AvMain.hd), (float)Canvas.h);
			g.translate(0f, (float)Canvas.cameraList.y);
		}
		else
		{
			Canvas.resetTrans(g);
			Canvas.paint.paintDefaultBg(g);
			g.translate(0f, -CameraList.cmy);
			onMainMenu.paintTitle(g, this.name, Canvas.w / 2, 30 * AvMain.hd);
			g.translate(0f, CameraList.cmy);
			g.translate(0f, (float)Canvas.cameraList.y);
		}
		int num = (int)CameraList.cmy / this.wSmall;
		if (onMainMenu.isOngame)
		{
			num--;
		}
		if (num < 0)
		{
			num = 0;
		}
		int num2 = num + (Canvas.h - 40) / this.wSmall + 2;
		if (onMainMenu.isOngame)
		{
			num2++;
		}
		if (num2 > ListScr.tempList.size())
		{
			num2 = ListScr.tempList.size();
		}
		if (this.focus == 5)
		{
			this.paintCustom(g, num, num2);
		}
		else if (this.focus == 6 || this.focus == 0)
		{
			this.paintCustomAvatar(g, num, num2);
		}
		if (onMainMenu.isOngame)
		{
			Canvas.resetTransNotZoom(g);
			Canvas.paint.paintTabSoft(g);
			Canvas.paint.paintCmdBar(g, this.left, this.center, this.right);
		}
		Canvas.resetTrans(g);
		if (!onMainMenu.isOngame)
		{
			g.drawImage(ListScr.imgCloseTabFull[(int)ListScr.countClose / 3], (float)(Canvas.w - 25 * AvMain.hd), (float)(35 * AvMain.hd), 3);
		}
		Canvas.paintPlus(g);
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00045871 File Offset: 0x00043C71
	public override void commandTab(int index)
	{
		if (index != 0)
		{
			if (index != 1)
			{
				if (index == 2)
				{
					this.doSendMessage();
				}
			}
			else
			{
				this.closeTabAll();
			}
		}
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x000458A7 File Offset: 0x00043CA7
	public override void doMenuTab()
	{
		if (this.left != null)
		{
			this.left.perform();
		}
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x000458C0 File Offset: 0x00043CC0
	private void paintCustomAvatar(MyGraphics g, int x0, int y0)
	{
		int num = 0;
		int num2 = 0;
		num += this.wSmall * x0;
		for (int i = x0; i < y0; i++)
		{
			if (!onMainMenu.isOngame)
			{
				g.setClip((float)(5 * AvMain.hd), (float)(6 * AvMain.hd), (float)(Canvas.w - 10 * AvMain.hd), (float)PaintPopup.gI().h);
			}
			g.translate(0f, -CameraList.cmy);
			Avatar avatar = (Avatar)ListScr.tempList.elementAt(i);
			int num3 = 0;
			if (!this.isHide && i == this.selected)
			{
				if (onMainMenu.isOngame)
				{
					Canvas.paint.paintSelect(g, 4 * AvMain.hd, num + 2, Canvas.w - 8 * AvMain.hd, this.wSmall - 4);
				}
				else
				{
					g.setColor(16777215);
					g.fillRect((float)(4 * AvMain.hd), (float)(num + 2), (float)(Canvas.w - 8 * AvMain.hd), (float)this.wSmall);
				}
				int width = Canvas.fontChatB.getWidth(avatar.text2);
				if (width > PaintPopup.gI().w - (57 + (AvMain.hd - 1) * 30))
				{
					this.xCus += 2;
					if (this.xCus > width - (PaintPopup.gI().w - (57 + (AvMain.hd - 1) * 30)))
					{
						this.xCus = -20;
					}
				}
				num3 = this.xCus;
				if (this.xCus < 0)
				{
					num3 = 0;
				}
			}
			avatar.paintIcon(g, 45 + (AvMain.hd - 1) * 20, num + this.wSmall / 2 + 35 * AvMain.hd / 2, false);
			int num4 = 0;
			int num5 = num + this.wSmall / 2 - this.wSmall / 5 - (int)AvMain.hNormal / 2;
			if (avatar.text2 != null && avatar.text2.Equals(string.Empty))
			{
				num5 = num + this.wSmall / 2 - (int)AvMain.hNormal / 2;
			}
			if (avatar.idImg != -1)
			{
				num4 = 6 * AvMain.hd;
				AvatarData.paintImg(g, (int)avatar.idImg, 60 + (AvMain.hd - 1) * 30 + num4, num5 + (int)AvMain.hNormal / 2, 3);
			}
			if (!onMainMenu.isOngame)
			{
				g.setClip((float)(5 * AvMain.hd), (float)((int)CameraList.cmy + 6 * AvMain.hd), (float)(Canvas.w - 10 * AvMain.hd), (float)PaintPopup.gI().h);
			}
			if (onMainMenu.isOngame)
			{
				Canvas.normalWhiteFont.drawString(g, avatar.name, 60 + 10 * AvMain.hd + num4 * 2 + (AvMain.hd - 1) * 30, num5 + 5 * AvMain.hd, 0);
				Canvas.fontChatB.drawString(g, avatar.text2, 60 + 10 * AvMain.hd - num3 + (AvMain.hd - 1) * 30, num + this.wSmall / 2 + this.wSmall / 5 - Canvas.normalWhiteFont.getHeight() / 2 - 2 * AvMain.hd, 0);
			}
			else
			{
				if (avatar.idWedding != -1)
				{
					AvatarData.paintImg(g, (int)avatar.idWedding, 60 + 6 * AvMain.hd + num4 * 2 + (AvMain.hd - 1) * 30 + Canvas.normalFont.getWidth(avatar.name), num + this.wSmall / 2 - 12 * AvMain.hd, 3);
				}
				if (avatar.idStatus != -1)
				{
					num2 = 12 * AvMain.hd;
					AvatarData.paintImg(g, (int)avatar.idStatus, 60 - num3 + (AvMain.hd - 1) * 30 + 6 * AvMain.hd, num + this.wSmall / 2 + 3 * AvMain.hd + (int)AvMain.hBlack / 2, 3);
				}
				Canvas.normalFont.drawString(g, avatar.name, 60 + num4 * 2 + (AvMain.hd - 1) * 30, num5, 0);
				Canvas.fontChat.drawString(g, avatar.text2, 62 - num3 + (AvMain.hd - 1) * 30 + num2, num + this.wSmall / 2 + this.wSmall / 5 - Canvas.fontChat.getHeight() / 2, 0);
			}
			num += this.wSmall;
			g.translate(0f, CameraList.cmy);
		}
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x00045D1C File Offset: 0x0004411C
	private void paintCustom(MyGraphics g, int x0, int y0)
	{
		int num = 0;
		num += this.wSmall * x0;
		for (int i = x0; i < y0; i++)
		{
			if (!onMainMenu.isOngame)
			{
				g.setClip((float)(5 * AvMain.hd), (float)(5 * AvMain.hd), (float)(Canvas.w - 10 * AvMain.hd), (float)PaintPopup.gI().h);
			}
			g.translate(0f, -CameraList.cmy);
			int num2 = 0;
			if (!this.isHide && i == this.selected)
			{
				if (onMainMenu.isOngame)
				{
					Canvas.paint.paintSelect(g, 4 * AvMain.hd, num + 2, Canvas.w - 8 * AvMain.hd, this.wSmall - 4);
				}
				else
				{
					g.setColor(16777215);
					g.fillRect((float)(4 * AvMain.hd), (float)(num + 2), (float)(Canvas.w - 8 * AvMain.hd), (float)this.wSmall);
				}
				num2 = this.xCus;
				if (this.xCus < 0)
				{
					num2 = 0;
				}
			}
			StringObj stringObj = (StringObj)ListScr.tempList.elementAt(i);
			int num3 = (int)(AvatarData.getImgIcon((short)stringObj.dis).h + 4);
			AvatarData.paintImg(g, stringObj.dis, 30 + num3 / 2, num + this.wSmall / 2 - 12 * AvMain.hd + Canvas.normalWhiteFont.getHeight() / 2, 3);
			if (onMainMenu.isOngame)
			{
				Canvas.normalWhiteFont.drawString(g, stringObj.str, 30 + num3, num + this.wSmall / 2 - 5 * AvMain.hd, 0);
				onMainMenu.smallGrey.drawString(g, stringObj.str2, 30 - num2, num + this.wSmall / 2, 0);
			}
			else
			{
				Canvas.normalFont.drawString(g, stringObj.str, 30 + num3, num + this.wSmall / 2 - 12 * AvMain.hd, 0);
				Canvas.fontChat.drawString(g, stringObj.str2, 30 - num2, num + this.wSmall / 2 + 3 * AvMain.hd, 0);
			}
			num += this.wSmall;
			g.translate(0f, CameraList.cmy);
		}
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00045F4C File Offset: 0x0004434C
	public override void updateKey()
	{
		if (onMainMenu.isOngame)
		{
			Canvas.paint.updateKeyOn(this.left, this.center, this.right);
		}
		else
		{
			base.updateKey();
		}
		if (Canvas.isPointerClick && !onMainMenu.isOngame && Canvas.isPoint(Canvas.w - 45 * AvMain.hd, 15 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
		{
			this.isTranClose = true;
			ListScr.countClose = 5;
			Canvas.isPointerClick = false;
		}
		if (this.isTranClose)
		{
			if (Canvas.isPointerDown && !Canvas.isPoint(Canvas.w - 45 * AvMain.hd, 15 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
			{
				ListScr.countClose = 0;
			}
			if (Canvas.isPointerRelease)
			{
				this.isTranClose = false;
				Canvas.isPointerRelease = false;
				if ((int)ListScr.countClose != 0)
				{
					ListScr.countClose = 0;
					this.closeTabAll();
				}
			}
		}
		PaintPopup.gI().setupdateTab();
		if (this.isRemove)
		{
			if (Canvas.isPointerClick && Canvas.isPoint(Canvas.cameraList.x + Canvas.cameraList.disX - 70, Canvas.cameraList.y, 70, Canvas.cameraList.disY))
			{
				Canvas.isPointerClick = false;
				this.transY = true;
			}
			if (this.transY && Canvas.isPointerRelease && Canvas.isPoint(Canvas.cameraList.x + Canvas.cameraList.disX - 70, Canvas.cameraList.y, 70, Canvas.cameraList.disY) && global::Math.abs(Canvas.dy()) < 5 * AvMain.hd)
			{
				int num = (int)((CameraList.cmtoY + (float)Canvas.py - (float)Canvas.cameraList.y) / (float)this.wSmall);
				if (num >= 0 && num < ListScr.tempList.size() && Canvas.isPointer(Canvas.cameraList.x + Canvas.cameraList.disX - 70, Canvas.cameraList.y, 70, Canvas.cameraList.disY))
				{
					Canvas.msgdlg.setInfoLR(T.uRemoveFriend, new Command(T.yes, new ListScr.IActionURemoveFr(num)), new Command(T.no, 0, this));
				}
				Canvas.isPointerRelease = false;
			}
		}
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x000461C4 File Offset: 0x000445C4
	public override void update()
	{
		PaintPopup.gI().update();
		this.backMyScreen.update();
		if (this.focus == 5 && this.selected >= 0 && this.selected < ListScr.tempList.size())
		{
			StringObj stringObj = (StringObj)ListScr.tempList.elementAt(this.selected);
			if (!this.isHide && (int)stringObj.w > PaintPopup.gI().w - 10 * AvMain.hd)
			{
				this.xCus += 2;
				if (this.xCus > PaintPopup.gI().w)
				{
					this.xCus = -20;
				}
			}
		}
		Scroll.gI().updateScroll((int)CameraList.cmy, (int)CameraList.cmtoY, (int)Canvas.cameraList.vY);
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x000462A0 File Offset: 0x000446A0
	public void onList(int type, MyVector list, MyScreen backMyScreen)
	{
		if (Canvas.currentMyScreen != ListScr.gI())
		{
			this.backMyScreen = backMyScreen;
		}
		switch (this.focus)
		{
		case 0:
			ListScr.isGetTypeHouse = true;
			ListScr.friendL = list;
			if ((int)ListScr.typeListFriend == 1)
			{
				MapScr.gI().doRequestAddFriend(MapScr.focusP);
			}
			else if ((int)ListScr.typeListFriend == 2)
			{
				ListScr.isGetTypeHouse = false;
				Canvas.startWaitDlg();
				AvatarService.gI().getTypeHouse(1);
			}
			else if (Canvas.currentMyScreen != this)
			{
				this.switchToMe();
			}
			ListScr.typeListFriend = 0;
			break;
		}
		ListScr.tempList = null;
		ListScr.tempList = list;
		if (this.focus != 5)
		{
			for (int i = 0; i < ListScr.tempList.size(); i++)
			{
				Avatar avatar = (Avatar)ListScr.tempList.elementAt(i);
				avatar.initPet();
				avatar.orderSeriesPath();
			}
		}
		this.type = type;
		this.selected = 0;
		this.setCam();
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x000463C1 File Offset: 0x000447C1
	public void setCam()
	{
		this.init();
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x000463CC File Offset: 0x000447CC
	public void setFriendList(bool isJoinFarm)
	{
		this.focus = 0;
		if (ListScr.friendL == null)
		{
			Canvas.startWaitDlg();
			CasinoService.gI().requestFriendList();
		}
		else
		{
			this.backMyScreen = Canvas.currentMyScreen;
			this.setList(ListScr.idFriendList);
			this.switchToMe();
		}
		if (isJoinFarm)
		{
			this.isAction = true;
			Out.println("setFriendList");
			this.cmdSelected = new Command(T.selectt, 1, this);
		}
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x00046444 File Offset: 0x00044844
	public static Avatar getAvatar(int id)
	{
		int num = ListScr.friendL.size();
		for (int i = 0; i < num; i++)
		{
			Avatar avatar = (Avatar)ListScr.friendL.elementAt(i);
			if (avatar.IDDB == id)
			{
				return avatar;
			}
		}
		return null;
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x00046490 File Offset: 0x00044890
	public bool setList(string idType)
	{
		sbyte[] array = (sbyte[])ListScr.hList.get(idType);
		Canvas.endDlg();
		if (array == null)
		{
			return false;
		}
		this.readList(array, idType);
		return true;
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x000464C4 File Offset: 0x000448C4
	public void readList(sbyte[] data, string idType)
	{
		string[] array = null;
		sbyte[] array2 = null;
		DataInputStream dataInputStream = new DataInputStream(data);
		try
		{
			string text = dataInputStream.readUTF();
			int idList = dataInputStream.readInt();
			sbyte b = dataInputStream.readByte();
			sbyte page = dataInputStream.readByte();
			short num = dataInputStream.readShort();
			MyVector myVector = new MyVector();
			if ((int)b == 0)
			{
				this.focus = 5;
				for (int i = 0; i < (int)num; i++)
				{
					StringObj stringObj = new StringObj();
					stringObj.dis = (int)dataInputStream.readShort();
					stringObj.str = dataInputStream.readUTF();
					stringObj.str2 = dataInputStream.readUTF();
					stringObj.w = (short)Canvas.fontChatB.getWidth(stringObj.str2);
					if ((int)stringObj.w > Canvas.w)
					{
						stringObj.w = (short)Canvas.w;
					}
					myVector.addElement(stringObj);
				}
			}
			else
			{
				this.focus = 6;
				for (int j = 0; j < (int)num; j++)
				{
					Avatar avatar = new Avatar();
					avatar.direct = 0;
					sbyte b2 = dataInputStream.readByte();
					avatar.seriPart = new MyVector();
					for (int k = 0; k < (int)b2; k++)
					{
						avatar.addSeri(new SeriPart(dataInputStream.readShort()));
					}
					avatar.IDDB = dataInputStream.readInt();
					avatar.idImg = dataInputStream.readShort();
					if (idType.Equals(ListScr.idFriendList))
					{
						avatar.idWedding = dataInputStream.readShort();
						avatar.idStatus = dataInputStream.readShort();
					}
					avatar.name = dataInputStream.readUTF();
					avatar.text2 = dataInputStream.readUTF();
					myVector.addElement(avatar);
				}
			}
			int num2 = (int)dataInputStream.readByte();
			if (num2 > 0)
			{
				array = new string[num2];
				array2 = new sbyte[num2];
				for (int l = 0; l < num2; l++)
				{
					array2[l] = dataInputStream.readByte();
					array[l] = dataInputStream.readUTF();
				}
			}
			if (idType.Equals(ListScr.idFriendList))
			{
				this.focus = 0;
			}
			ListScr.gI().onList(this.focus, myVector, Canvas.currentMyScreen);
			this.name = text;
			this.switchToMe();
			string[] tex = array;
			sbyte[] idMe = array2;
			this.left = null;
			if (num2 > 0)
			{
				if (this.isAction)
				{
					this.left = this.cmdSelected;
				}
				else
				{
					this.left = new Command(T.menu, new ListScr.IActionListMenu(tex, idType, idMe, idList, page, idType.Equals(ListScr.idFriendList)));
				}
			}
			if (!this.isAction)
			{
				if (idType.Equals(ListScr.idFriendList))
				{
					this.cmdSelected = new Command(T.sendMessage, 2);
				}
				else if (!this.isAction)
				{
					this.cmdSelected = new Command(T.selectt, new ListScr.IActionReadList(idList, page));
				}
			}
			this.isAction = false;
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x000467E4 File Offset: 0x00044BE4
	public override void commandActionPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			this.isRemove = false;
			break;
		case 1:
			Canvas.startWaitDlg();
			FarmScr.gI().doJoinFarm(((Avatar)ListScr.friendL.elementAt(ListScr.instance.selected)).IDDB, true);
			break;
		case 2:
			this.isRemove = true;
			break;
		case 3:
			CasinoService.gI().requestFriendList();
			break;
		}
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00046868 File Offset: 0x00044C68
	protected void doSendMessage()
	{
		if (this.selected < 0 || this.selected >= ListScr.tempList.size())
		{
			return;
		}
		Avatar p = (Avatar)ListScr.tempList.elementAt(this.selected);
		MessageScr.gI().startChat(p);
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x000468B8 File Offset: 0x00044CB8
	public void removeList()
	{
		ListScr.hList.remove(ListScr.idFriendList);
		ListScr.friendL = null;
	}

	// Token: 0x0400097F RID: 2431
	public static ListScr instance;

	// Token: 0x04000980 RID: 2432
	public MyScreen backMyScreen;

	// Token: 0x04000981 RID: 2433
	public int focus;

	// Token: 0x04000982 RID: 2434
	public int type;

	// Token: 0x04000983 RID: 2435
	public static MyVector tempList = new MyVector();

	// Token: 0x04000984 RID: 2436
	public Command cmdSelected;

	// Token: 0x04000985 RID: 2437
	public Command cmdClose;

	// Token: 0x04000986 RID: 2438
	public static MyVector friendL;

	// Token: 0x04000987 RID: 2439
	private int wSmall;

	// Token: 0x04000988 RID: 2440
	public static sbyte typeListFriend = 0;

	// Token: 0x04000989 RID: 2441
	public static sbyte countClose;

	// Token: 0x0400098A RID: 2442
	public static bool isGetTypeHouse = false;

	// Token: 0x0400098B RID: 2443
	public new int selected;

	// Token: 0x0400098C RID: 2444
	public static string idFriendList = "friendlist";

	// Token: 0x0400098D RID: 2445
	public static MyHashTable hList = new MyHashTable();

	// Token: 0x0400098E RID: 2446
	public bool isAction;

	// Token: 0x0400098F RID: 2447
	public bool isRemove;

	// Token: 0x04000990 RID: 2448
	public bool isJoinH;

	// Token: 0x04000991 RID: 2449
	private string name;

	// Token: 0x04000992 RID: 2450
	public static Image[] imgCloseTabFull;

	// Token: 0x04000993 RID: 2451
	public static Image[] imgCloseTab;

	// Token: 0x04000994 RID: 2452
	private new bool isHide;

	// Token: 0x04000995 RID: 2453
	private int xCus = -20;

	// Token: 0x04000996 RID: 2454
	private bool transY;

	// Token: 0x04000997 RID: 2455
	private bool isTranClose;

	// Token: 0x02000114 RID: 276
	private class IActionURemoveFr : IAction
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x000468FB File Offset: 0x00044CFB
		public IActionURemoveFr(int sel)
		{
			this.sel = sel;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0004690C File Offset: 0x00044D0C
		public void perform()
		{
			ListScr.instance.isRemove = false;
			Avatar avatar = (Avatar)ListScr.tempList.elementAt(this.sel);
			GlobalService.gI().doRemoveFriend(avatar.IDDB);
			ListScr.tempList.removeElement(avatar);
			ListScr.instance.init();
		}

		// Token: 0x04000998 RID: 2456
		private int sel;
	}

	// Token: 0x02000115 RID: 277
	private class IActionReadList : IAction
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x0004695F File Offset: 0x00044D5F
		public IActionReadList(int idList, sbyte page)
		{
			this.idList = idList;
			this.page = page;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00046975 File Offset: 0x00044D75
		public void perform()
		{
			GlobalService.gI().doListCustom(this.idList, this.page, ListScr.instance.selected, -1);
		}

		// Token: 0x04000999 RID: 2457
		private readonly int idList;

		// Token: 0x0400099A RID: 2458
		private readonly sbyte page;
	}

	// Token: 0x02000116 RID: 278
	private class IActionListMenu : IAction
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x00046998 File Offset: 0x00044D98
		public IActionListMenu(string[] tex, string idType, sbyte[] idMe, int idList, sbyte page, bool isFr)
		{
			this.tex = tex;
			this.idType = idType;
			this.idMe = idMe;
			this.idList = idList;
			this.page = page;
			this.isFr = isFr;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000469D0 File Offset: 0x00044DD0
		public void perform()
		{
			MyVector myVector = new MyVector();
			if (ListScr.instance.isJoinH)
			{
				ListScr.instance.cmdSelected.perform();
				return;
			}
			if (this.isFr)
			{
				myVector.addElement(ListScr.instance.cmdSelected);
			}
			for (int i = 0; i < this.tex.Length; i++)
			{
				int ii = i;
				myVector.addElement(new Command(this.tex[i], new ListScr.IActionListMenu2(this.idList, this.page, this.idMe, ii)));
			}
			if (!ListScr.instance.isAction && this.idType.Equals(ListScr.idFriendList))
			{
				myVector.addElement(new Command(T.updateList, 3, ListScr.instance));
			}
			int num = (int)((float)(ListScr.instance.selected * ListScr.instance.wSmall) - AvCamera.gI().yCam + (float)(ListScr.instance.wSmall / 2));
			if (num + MenuSub.gI().h > Canvas.h)
			{
				num = Canvas.h - MenuSub.gI().h;
			}
			MenuSub.gI().startAt(myVector, Canvas.w / 2 - MenuSub.gI().w / 2, num);
		}

		// Token: 0x0400099B RID: 2459
		private readonly string[] tex;

		// Token: 0x0400099C RID: 2460
		private readonly string idType;

		// Token: 0x0400099D RID: 2461
		private readonly sbyte[] idMe;

		// Token: 0x0400099E RID: 2462
		private readonly int idList;

		// Token: 0x0400099F RID: 2463
		private readonly sbyte page;

		// Token: 0x040009A0 RID: 2464
		private bool isFr;
	}

	// Token: 0x02000117 RID: 279
	private class IActionListMenu2 : IAction
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00046B15 File Offset: 0x00044F15
		public IActionListMenu2(int idList, sbyte page, sbyte[] idMe, int ii)
		{
			this.idList = idList;
			this.page = page;
			this.idMe = idMe;
			this.ii = ii;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00046B3A File Offset: 0x00044F3A
		public void perform()
		{
			GlobalService.gI().doListCustom(this.idList, this.page, ListScr.instance.selected, this.idMe[this.ii]);
		}

		// Token: 0x040009A1 RID: 2465
		private readonly int idList;

		// Token: 0x040009A2 RID: 2466
		private readonly sbyte page;

		// Token: 0x040009A3 RID: 2467
		private readonly sbyte[] idMe;

		// Token: 0x040009A4 RID: 2468
		private readonly int ii;
	}
}
