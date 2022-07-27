using System;

// Token: 0x02000194 RID: 404
public abstract class BoardScr : OnScreen, IChatable
{
	// Token: 0x06000AC3 RID: 2755 RVA: 0x0006AD90 File Offset: 0x00069190
	public BoardScr()
	{
		this.init();
		BoardScr.cmdMenu = new Command(T.menu, 0);
		BoardScr.cmdCloseBoard = new Command(T.OK, 1);
		BoardScr.cmdStart = new Command(T.start, 2);
		BoardScr.cmdBack = new Command(T.continuee, 3);
		BoardScr.cmdFire = new Command(T.fire, 4);
		BoardScr.cmdReady = new Command(T.ready, 5);
		BoardScr.cmdWaiting = new Command(T.pleaseWait, 6);
		base.addCmd(10, 1);
		base.addCmd(0, 5);
		base.addCmd(11, 2);
		base.addCmd(12, 4);
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x0006AE4B File Offset: 0x0006924B
	public void initImg()
	{
		if (BoardScr.imgReady != null)
		{
			return;
		}
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x0006AE58 File Offset: 0x00069258
	public override void close()
	{
		this.doExit();
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x0006AE60 File Offset: 0x00069260
	public override void switchToMe()
	{
		Canvas.clearKeyPressed();
		base.switchToMe();
		BoardScr.me = this;
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x0006AE74 File Offset: 0x00069274
	public virtual void init()
	{
		if (AvMain.hd == 2)
		{
			BoardScr.wCard = 144;
			BoardScr.hcard = 194;
		}
		else
		{
			BoardScr.wCard = 72;
			BoardScr.hcard = 97;
		}
		BoardScr.posAvatar = new AvPosition[]
		{
			new AvPosition(Canvas.hw, 55 * AvMain.hd, 2),
			new AvPosition(13 * AvMain.hd, Canvas.hh - 20, 0),
			new AvPosition(Canvas.hw, Canvas.h - 5 * AvMain.hd, 2),
			new AvPosition(Canvas.w - 13 * AvMain.hd, Canvas.hh - 20, 1)
		};
		this.setPosCam();
		if (BoardScr.isStartGame || BoardScr.disableReady)
		{
			this.setPosPlaying();
		}
		if (BoardScr.me != null)
		{
			base.repaint();
		}
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0006AF57 File Offset: 0x00069357
	public void doCloseBoard()
	{
		BoardScr.chatHistory.removeAllElements();
		this.setPosCam();
		Canvas.endDlg();
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x0006AF6E File Offset: 0x0006936E
	public void closeBoard(string info)
	{
		this.left = null;
		this.center = null;
		Canvas.startOK(info, new BoardScr.IActionClose());
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0006AF8C File Offset: 0x0006938C
	public virtual void doReady()
	{
		Avatar avatarByID = BoardScr.getAvatarByID(GameMidlet.avatar.IDDB);
		if ((int)avatarByID.action == 1)
		{
			return;
		}
		if (MapScr.isNewVersion && BoardScr.money > GameMidlet.avatar.money[3])
		{
			BoardListOnScr.gI().setXeng();
			return;
		}
		bool flag = !((Avatar)BoardScr.avatarInfos.elementAt((int)BoardScr.indexOfMe)).isReady;
		if (flag)
		{
			BoardScr.notReadyDelay = 100;
		}
		this.setCmdWaiting();
		Canvas.startWaitDlg();
		CasinoService.gI().ready(BoardScr.roomID, BoardScr.boardID, flag);
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x0006B02D File Offset: 0x0006942D
	public void setCmdWaiting()
	{
		BoardScr.me.center = BoardScr.cmdWaiting;
		BoardScr.me.right = null;
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x0006B049 File Offset: 0x00069449
	public virtual void doFire()
	{
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x0006B04C File Offset: 0x0006944C
	protected void doStartGame()
	{
		if (BoardScr.isStartGame)
		{
			return;
		}
		if (MapScr.isNewVersion && BoardScr.money > GameMidlet.avatar.money[3])
		{
			BoardListOnScr.gI().setXeng();
			return;
		}
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != GameMidlet.avatar.IDDB && avatar.IDDB != -1)
			{
				if (avatar.isReady)
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
		}
		if (num == 0 || num2 > 0)
		{
			Canvas.startOKDlg(T.opponentAreNotReady);
			return;
		}
		if (BoardScr.me == PBoardScr.instance)
		{
			BoardScr.me.center = BoardScr.cmdWaiting;
			BoardScr.me.right = null;
		}
		else
		{
			Canvas.startWaitDlg();
		}
		base.repaint();
		GlobalService.gI().startGame(BoardScr.roomID, BoardScr.boardID);
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x0006B158 File Offset: 0x00069558
	public override void commandActionPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 1:
			this.doOption();
			break;
		case 2:
			this.doKick();
			break;
		case 3:
			BoardScr.doAddFriend();
			break;
		case 4:
			this.doViewMessage();
			break;
		case 5:
			this.doExit();
			break;
		case 6:
			Canvas.startWaitDlg();
			GlobalService.gI().requestInfoOf(GameMidlet.avatar.IDDB);
			break;
		case 10:
			this.doSettingMoney();
			break;
		case 11:
			this.doSetMaxPlayer();
			break;
		case 12:
			this.doSettingPassword();
			break;
		}
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x0006B218 File Offset: 0x00069618
	public override void commandAction(int index)
	{
		switch (index)
		{
		case 1:
			this.doOption();
			break;
		case 2:
			this.doKick();
			break;
		case 3:
			BoardScr.doAddFriend();
			break;
		case 4:
			this.doViewMessage();
			break;
		case 5:
			this.doExit();
			break;
		case 6:
			Canvas.startWaitDlg();
			GlobalService.gI().requestInfoOf(GameMidlet.avatar.IDDB);
			break;
		case 10:
			this.doSettingMoney();
			break;
		case 11:
			this.doSetMaxPlayer();
			break;
		case 12:
			this.doSettingPassword();
			break;
		}
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x0006B2D8 File Offset: 0x000696D8
	private void doOption()
	{
		MyVector myVector = new MyVector();
		Command o = new Command(T.setMoney, 10, this);
		Command o2 = new Command(T.setNumPlayers, 11, this);
		Command o3 = new Command(T.setPass, 12, this);
		myVector.addElement(o);
		if ((int)BoardListOnScr.type != 0)
		{
			myVector.addElement(o2);
		}
		myVector.addElement(o3);
		MenuCenter.gI().startAt(myVector);
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x0006B340 File Offset: 0x00069740
	public override void doMenu()
	{
		Command o = new Command(T.option, 1, this);
		Command o2 = new Command(T.kick, 2, this);
		int num = 0;
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			if (((Avatar)BoardScr.avatarInfos.elementAt(i)).IDDB != -1)
			{
				num++;
			}
		}
		MyVector myVector = new MyVector();
		if (BoardScr.ownerID == GameMidlet.avatar.IDDB && !BoardScr.isStartGame)
		{
			myVector.addElement(o);
			if (num > 1)
			{
				myVector.addElement(o2);
			}
		}
		if (num > 1)
		{
			myVector.addElement(new Command(T.addFriend, 3, this));
		}
		myVector.addElement(new Command(T.viewMyInfo, 6, this));
		myVector.addElement(new Command(T.viewMessage, 4, this));
		myVector.addElement(new Command(T.exitBoard, 5, this));
		if (myVector.size() == 1 && BoardScr.ownerID == GameMidlet.avatar.IDDB)
		{
			this.commandAction(1);
		}
		else
		{
			MenuCenter.gI().startAt(myVector);
		}
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x0006B466 File Offset: 0x00069866
	public static void startMenu(MyVector menu, int pos)
	{
		MenuLeft.gI().startAt(menu);
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0006B473 File Offset: 0x00069873
	public virtual void resetCard()
	{
		BoardScr.currentTime = 0L;
		BoardScr.dieTime = 0L;
		BoardScr.isStartGame = false;
		BoardScr.disableReady = false;
		BoardScr.isGameEnd = false;
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0006B495 File Offset: 0x00069895
	public void setPosCam()
	{
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0006B497 File Offset: 0x00069897
	public void loadMap(int type)
	{
		BoardScr.listPosAvatar.removeAllElements();
		BoardScr.listPosCasino.removeAllElements();
		this.setPosPlaying();
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x0006B4B3 File Offset: 0x000698B3
	public void setAt(int seat, Avatar ava)
	{
		BoardScr.avatarInfos.setElementAt(ava, seat);
		this.setPosBoard();
		this.setPosPlaying();
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x0006B4D0 File Offset: 0x000698D0
	public void setPosTrans(int id)
	{
		Avatar avatarByID = BoardScr.getAvatarByID(id);
		if (avatarByID != null)
		{
			int indexByID = BoardScr.getIndexByID(id);
			AvPosition avPosition = (AvPosition)BoardScr.listPosCasino.elementAt(indexByID);
			avatarByID.x = avPosition.x;
			avatarByID.y = avPosition.y;
			avatarByID.action = 1;
		}
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0006B521 File Offset: 0x00069921
	public virtual void doContinue()
	{
		this.setPosCam();
		base.repaint();
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0006B530 File Offset: 0x00069930
	public void resetPos()
	{
		for (int i = 0; i < BoardScr.avatarInfos.size(); i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			AvPosition avPosition = (AvPosition)BoardScr.listPosAvatar.elementAt(i);
			avatar.setPos(avPosition.x, avPosition.y);
			if ((avatar.IDDB == BoardScr.ownerID || avatar.isReady) && (int)avatar.action == 0)
			{
				avatar.ySat = -8;
				avatar.setAction(2);
			}
			if (((int)BoardListOnScr.type == (int)BoardListOnScr.STYLE_5PLAYER && i % 2 == 1) || ((int)BoardListOnScr.type != (int)BoardListOnScr.STYLE_5PLAYER && i % 2 == 0))
			{
				avatar.direct = (avatar.dirFirst = Base.RIGHT);
			}
			else
			{
				avatar.direct = (avatar.dirFirst = Base.LEFT);
			}
		}
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x0006B622 File Offset: 0x00069A22
	public override void updateKey()
	{
		base.updateKey();
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x0006B62C File Offset: 0x00069A2C
	public override void update()
	{
		base.update();
		if (BoardScr.notReadyDelay > 0)
		{
			BoardScr.notReadyDelay--;
		}
		if (!BoardScr.isStartGame)
		{
		}
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != -1)
			{
				avatar.updateAvatar();
			}
		}
		if (BoardScr.chatPublic != null && BoardScr.chatPublic.setOut())
		{
			BoardScr.chatPublic = null;
		}
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x0006B6C0 File Offset: 0x00069AC0
	public void updateCo()
	{
		if (!BoardScr.isStartGame)
		{
			this.updateReady();
		}
		else
		{
			if (this.turn == GameMidlet.avatar.IDDB)
			{
				this.center = BoardScr.cmdFire;
			}
			else
			{
				this.center = null;
			}
			this.right = null;
			if (onMainMenu.isOngame)
			{
				this.left = BoardScr.cmdMenu;
			}
			else
			{
				this.left = null;
			}
			if (BoardScr.dieTime != 0L)
			{
				BoardScr.currentTime = Canvas.getTick();
				if (BoardScr.currentTime > BoardScr.dieTime)
				{
					BoardScr.dieTime = 0L;
				}
			}
		}
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x0006B764 File Offset: 0x00069B64
	public virtual void updateReady()
	{
		if (BoardScr.ownerID == GameMidlet.avatar.IDDB)
		{
			if (this.center != BoardScr.cmdWaiting)
			{
				this.center = BoardScr.cmdStart;
				BoardScr.cmdStart.caption = T.start;
			}
			bool flag = true;
			for (int i = 0; i < BoardScr.numPlayer; i++)
			{
				Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
				if (avatar.IDDB == -1)
				{
					flag = false;
				}
				else if (avatar.IDDB != GameMidlet.avatar.IDDB && !avatar.isReady)
				{
					flag = false;
				}
			}
			if (flag && Canvas.gameTick % 10 > 7)
			{
				BoardScr.cmdStart.caption = string.Empty;
			}
		}
		else if (!BoardScr.disableReady)
		{
			this.center = BoardScr.cmdReady;
			BoardScr.cmdReady.caption = T.ready;
			for (int j = 0; j < BoardScr.numPlayer; j++)
			{
				Avatar avatar2 = (Avatar)BoardScr.avatarInfos.elementAt(j);
				if (avatar2.IDDB == GameMidlet.avatar.IDDB)
				{
					if (!avatar2.isReady)
					{
						if (Canvas.gameTick % 10 > 7)
						{
							BoardScr.cmdReady.caption = " ";
						}
					}
					else
					{
						BoardScr.cmdReady.caption = T.noReady;
						if (BoardScr.notReadyDelay == 0)
						{
							this.center = BoardScr.cmdReady;
						}
						else
						{
							this.center = null;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x0006B8F5 File Offset: 0x00069CF5
	public override void keyPress(int keyCode)
	{
		ChatTextField.gI().startChat(keyCode, this);
		base.keyPress(keyCode);
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x0006B90A File Offset: 0x00069D0A
	public override void paint(MyGraphics g)
	{
		if (BoardScr.chatPublic != null)
		{
			BoardScr.chatPublic.paintAnimal(g);
		}
		base.paint(g);
		Canvas.loadMap.paintEffectCamera(g);
		Canvas.paintPlus2(g);
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x0006B93C File Offset: 0x00069D3C
	public virtual void paintNamePlayers(MyGraphics g)
	{
		int num = AvMain.hd;
		if (BoardScr.isStartGame || BoardScr.disableReady || onMainMenu.isOngame)
		{
			num = 1;
		}
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != -1)
			{
				avatar.paintIcon(g, avatar.x * num, avatar.y * num, false);
				avatar.paintName(g, avatar.x * num, avatar.y * num);
				this.paintReady(g, avatar.x * num, (avatar.y - 50 * ((num != 1) ? 1 : AvMain.hd)) * num - 10 * (num - 1), MyGraphics.HCENTER | MyGraphics.VCENTER, avatar);
			}
		}
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x0006BA18 File Offset: 0x00069E18
	public void paintChat(MyGraphics g)
	{
		for (int i = 0; i < BoardScr.avatarInfos.size(); i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != -1 && avatar.chat != null)
			{
				avatar.chat.paintAnimal(g);
			}
		}
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x0006BA7C File Offset: 0x00069E7C
	public override void paintMain(MyGraphics g)
	{
		Canvas.resetTransNotZoom(g);
		g.setClip(0f, 0f, (float)Canvas.w, (float)Canvas.hCan);
		if (BoardScr.isStartGame || BoardScr.disableReady)
		{
			this.paintBgOngame(g);
		}
		else
		{
			Canvas.resetTrans(g);
			if (onMainMenu.isOngame)
			{
				this.paintBgOngame(g);
			}
		}
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x0006BAE4 File Offset: 0x00069EE4
	public virtual void paintBgOngame(MyGraphics g)
	{
		Canvas.paint.paintDefaultBg(g);
		if (Canvas.load == -1)
		{
			if (Canvas.currentMyScreen != DiamondScr.me)
			{
				this.paintBoard(g);
			}
			if (!BoardScr.isStartGame)
			{
				Canvas.normalWhiteFont.drawString(g, string.Concat(new object[]
				{
					"P: ",
					BoardScr.roomID,
					" - B: ",
					BoardScr.boardID
				}), Canvas.hw, Canvas.h / 2 - 10 * AvMain.hd, 2);
				Canvas.smallFontYellow.drawString(g, BoardScr.money + T.dola, Canvas.hw, Canvas.h / 2 + 10 * AvMain.hd, 2);
			}
			else if (Canvas.currentMyScreen == DiamondScr.me)
			{
				DiamondScr.me.paintCaro(g);
			}
		}
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x0006BBD1 File Offset: 0x00069FD1
	private void paintBoard(MyGraphics g)
	{
		if (BoardScr.imgBoard == null)
		{
			return;
		}
		g.drawImageScale(BoardScr.imgBoard, BoardScr.xBoard - BoardScr.wBoard / 2, BoardScr.yBoard - BoardScr.hBoard / 2, BoardScr.wBoard, BoardScr.hBoard, 0);
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x0006BC10 File Offset: 0x0006A010
	public virtual void paintBgCo(MyGraphics g)
	{
		Canvas.paint.paintDefaultBg(g);
		Canvas.normalWhiteFont.drawString(g, string.Concat(new object[]
		{
			"P: ",
			BoardScr.roomID,
			" - B: ",
			BoardScr.boardID
		}), Canvas.hw, Canvas.h / 2 - 10 * AvMain.hd, 2);
		Canvas.smallFontYellow.drawString(g, BoardScr.money + T.dola, Canvas.hw, Canvas.h / 2 + 10 * AvMain.hd, 2);
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x0006BCB4 File Offset: 0x0006A0B4
	public void paintNameRoom(MyGraphics g)
	{
		Canvas.smallFontYellow.drawString(g, RoomListOnScr.title, (int)(AvCamera.gI().xCam + (float)Canvas.hw), (int)(AvCamera.gI().yCam + (float)Canvas.hh - (float)AvMain.hSmall - (float)((int)AvMain.hSmall / 2) - 5f - (float)(10 * AvMain.hd) - 10f), 2);
		Canvas.smallFontYellow.drawString(g, string.Concat(new object[]
		{
			"P: ",
			BoardScr.roomID,
			" - B: ",
			BoardScr.boardID
		}), (int)(AvCamera.gI().xCam + (float)Canvas.hw), (int)(AvCamera.gI().yCam + (float)Canvas.hh - (float)((int)AvMain.hSmall / 2) - 5f - (float)(10 * AvMain.hd) - 10f), 2);
		Canvas.smallFontYellow.drawString(g, BoardScr.money + T.dola, (int)(AvCamera.gI().xCam + (float)Canvas.hw), (int)(AvCamera.gI().yCam + (float)Canvas.hh - 5f + (float)((int)AvMain.hSmall / 2) - (float)(10 * AvMain.hd) - 10f), 2);
		this.paintChat(g);
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x0006BE10 File Offset: 0x0006A210
	public virtual void paintPlayerCo(MyGraphics g)
	{
		for (int i = 0; i < BoardScr.avatarInfos.size(); i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (!avatar.name.Equals(string.Empty))
			{
				avatar.paintIcon(g, avatar.x, avatar.y, false);
				avatar.paintName(g, avatar.x, avatar.y);
				this.paintReady(g, avatar.x, avatar.y - 50 * AvMain.hd, MyGraphics.HCENTER | MyGraphics.VCENTER, avatar);
			}
		}
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x0006BEB0 File Offset: 0x0006A2B0
	public void paintReady(MyGraphics g, int x, int y, int author, Avatar ava)
	{
		if (!BoardScr.isStartGame)
		{
			if (ava.IDDB == BoardScr.ownerID)
			{
				g.drawImage(BoardScr.imgReady[1], (float)x, (float)y, author);
			}
			else if (ava.isReady)
			{
				g.drawImage(BoardScr.imgReady[0], (float)x, (float)y, author);
			}
		}
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x0006BF10 File Offset: 0x0006A310
	public void paintName(MyGraphics g)
	{
		int num = 10;
		if (Canvas.stypeInt > 0)
		{
			num = -5;
		}
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != -1)
			{
				string text = string.Empty;
				if (avatar.IDDB == this.turn && BoardScr.dieTime != 0L)
				{
					long num2 = (BoardScr.currentTime - BoardScr.dieTime) / 1000L;
					text += -num2;
				}
				int width = Canvas.arialFont.getWidth(avatar.name);
				if (GameMidlet.avatar.IDDB == avatar.IDDB)
				{
					Canvas.numberFont.drawString(g, text, Canvas.hw - width - 3, Canvas.hCan - Canvas.hTab - Canvas.numberFont.getHeight() + num, 1);
					int num3 = 0;
					int y = Canvas.hCan - Canvas.hTab - (int)AvMain.hBlack + num;
					if (avatar.idImg != -1)
					{
						num3 = 6 * AvMain.hd;
						AvatarData.paintImg(g, (int)avatar.idImg, Canvas.hw - Canvas.arialFont.getWidth(avatar.showName) / 2 - num3, y, 0);
					}
					Canvas.arialFont.drawString(g, avatar.showName, Canvas.hw + num3, y, 2);
					Canvas.smallFontYellow.drawString(g, avatar.getMoneyNew() + " " + T.getMoney(), Canvas.hw + width + 3, Canvas.hCan - Canvas.hTab - (int)AvMain.hSmall + num, 0);
					if (avatar.chat != null)
					{
						avatar.chat.setPos(Canvas.hw, Canvas.h - 35 * AvMain.hd);
						avatar.chat.paintAnimal(g);
					}
				}
				else
				{
					Canvas.numberFont.drawString(g, text, Canvas.hw - width - 3, 2, 1);
					int num4 = 0;
					if (avatar.idImg != -1)
					{
						num4 = 6 * AvMain.hd;
						AvatarData.paintImg(g, (int)avatar.idImg, Canvas.hw - Canvas.arialFont.getWidth(avatar.showName) / 2 - num4, 3, 0);
					}
					Canvas.arialFont.drawString(g, avatar.showName, Canvas.hw + num4, 2, 2);
					Canvas.smallFontYellow.drawString(g, avatar.getMoneyNew() + " " + T.getMoney(), Canvas.hw + Canvas.arialFont.getWidth(avatar.name) + 3, 6, 0);
					if (avatar.chat != null)
					{
						avatar.chat.setPos(Canvas.hw, 4 + avatar.chat.h);
						avatar.chat.paintAnimal(g);
					}
				}
			}
		}
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x0006C1DB File Offset: 0x0006A5DB
	public void doViewMessage()
	{
		MessageScr.gI().switchToMe();
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x0006C1E8 File Offset: 0x0006A5E8
	protected void doExit()
	{
		IAction action = new BoardScr.IActionExit(BoardScr.me);
		if (BoardScr.isStartGame && !BoardScr.disableReady)
		{
			Canvas.startOKDlg(T.doYouWantExit, action);
			return;
		}
		action.perform();
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x0006C22C File Offset: 0x0006A62C
	public override void commandTab(int index)
	{
		switch (index)
		{
		case 0:
			this.doMenu();
			break;
		case 1:
			BoardScr.me.doCloseBoard();
			break;
		case 2:
			BoardScr.me.doStartGame();
			break;
		case 3:
			BoardScr.me.doContinue();
			break;
		case 4:
			BoardScr.me.doFire();
			break;
		case 5:
			BoardScr.me.doReady();
			break;
		case 6:
			break;
		default:
			if (index != 100)
			{
				if (index == 101)
				{
					GlobalService.gI().setPassword(BoardScr.roomID, BoardScr.boardID, Canvas.inputDlg.getText());
					Canvas.startOKDlg(T.setPassed);
				}
			}
			else
			{
				try
				{
					int num = int.Parse(Canvas.inputDlg.getText());
					if (num >= 0)
					{
						Canvas.endDlg();
						GlobalService.gI().setMoney(BoardScr.roomID, BoardScr.boardID, num);
					}
				}
				catch (Exception ex)
				{
				}
			}
			break;
		case 10:
			this.doExit();
			break;
		case 11:
			ChatTextField.gI().showTF();
			break;
		case 12:
			MapScr.gI().doEvent();
			break;
		}
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0006C390 File Offset: 0x0006A790
	protected void doSettingMoney()
	{
		ipKeyboard.openKeyBoard(T.numTienCuoc, ipKeyboard.NUMBERIC, string.Empty, new BoardScr.IActionSettingMoney(), false);
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0006C3AC File Offset: 0x0006A7AC
	protected void doSetMaxPlayer()
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < 3; i++)
		{
			myVector.addElement(new Command(T.numPlayer[i], new BoardScr.IActionMaxPlayer(2 + i)));
		}
		BoardScr.startMenu(myVector, 0);
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x0006C3F2 File Offset: 0x0006A7F2
	public void doSettingPassword()
	{
		ipKeyboard.openKeyBoard(T.setPass, ipKeyboard.PASS, string.Empty, new BoardScr.IActionPass(), false);
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x0006C410 File Offset: 0x0006A810
	protected void doKick()
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != GameMidlet.avatar.IDDB && avatar.IDDB != -1)
			{
				myVector.addElement(new Command(avatar.showName, new BoardScr.IActionKick(avatar.IDDB)));
			}
		}
		BoardScr.startMenu(myVector, 0);
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x0006C490 File Offset: 0x0006A890
	protected static void doAddFriend()
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != GameMidlet.avatar.IDDB && avatar.IDDB != -1)
			{
				myVector.addElement(new Command(avatar.name, new BoardScr.IActionAddFriend(avatar)));
			}
		}
		BoardScr.startMenu(myVector, 0);
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x0006C50C File Offset: 0x0006A90C
	public void playerLeave(int leaveID)
	{
		Avatar avatarByID = BoardScr.getAvatarByID(leaveID);
		if (avatarByID != null)
		{
			BoardScr.addInfo(avatarByID.name + T.exited, 30, avatarByID.IDDB);
			avatarByID.IDDB = -1;
			avatarByID.setName(string.Empty);
			avatarByID.setExp(0);
			avatarByID.isReady = false;
		}
		this.setPosBoard();
		if (BoardScr.isStartGame || BoardScr.disableReady)
		{
			this.setPosPlaying();
		}
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x0006C584 File Offset: 0x0006A984
	public static void setOwner(int newOwner)
	{
		BoardScr.ownerID = newOwner;
		Avatar avatarByID = BoardScr.getAvatarByID(BoardScr.ownerID);
		if (avatarByID != null)
		{
			avatarByID.isReady = true;
		}
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x0006C5B0 File Offset: 0x0006A9B0
	public virtual void setPlayers(sbyte roomID1, sbyte boardID1, int ownerID, int money1, MyVector playerInfos)
	{
		this.initImg();
		BoardScr.roomID = roomID1;
		BoardScr.boardID = boardID1;
		BoardScr.money = money1;
		if (BoardScr.avatarInfos != null)
		{
			BoardScr.avatarInfos.removeAllElements();
		}
		BoardScr.avatarInfos = playerInfos;
		BoardScr.setOwner(ownerID);
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			avatar.direct = Base.RIGHT;
			avatar.setAction(2);
			avatar.setFrame((int)avatar.action);
			if (avatar.IDDB == GameMidlet.avatar.IDDB)
			{
				BoardScr.indexOfMe = (sbyte)i;
				break;
			}
		}
		this.setPosBoard();
		if ((int)BoardListOnScr.type == (int)BoardListOnScr.STYLE_4PLAYER)
		{
			Canvas.paint.initImgCard();
		}
		else
		{
			Canvas.paint.resetCasino();
		}
		Canvas.load = -1;
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x0006C694 File Offset: 0x0006AA94
	public void resetReady()
	{
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			avatar.isReady = false;
		}
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x0006C6CF File Offset: 0x0006AACF
	public void setMoney(int money1)
	{
		BoardScr.money = money1;
		this.resetReady();
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x0006C6E0 File Offset: 0x0006AAE0
	public void setReady(int id, bool isReady)
	{
		Avatar avatarByID = BoardScr.getAvatarByID(id);
		if (avatarByID != null)
		{
			avatarByID.isReady = isReady;
			if (!onMainMenu.isOngame)
			{
				if (isReady)
				{
					avatarByID.ySat = -8;
					avatarByID.setAction(2);
				}
				else
				{
					avatarByID.ySat = 0;
					avatarByID.setAction(0);
				}
			}
		}
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x0006C733 File Offset: 0x0006AB33
	public virtual void onChatFromMe(string text)
	{
		if (text.Trim().Equals(string.Empty))
		{
			return;
		}
		CasinoService.gI().chatToBoard(text);
		BoardScr.showChat(GameMidlet.avatar.IDDB, text);
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0006C768 File Offset: 0x0006AB68
	public static void showChat(int fromID, string text)
	{
		Avatar avatarByID = BoardScr.getAvatarByID(fromID);
		Avatar avatar = new Avatar();
		if (avatarByID == null)
		{
			return;
		}
		avatar.x = avatarByID.x;
		avatar.y = avatarByID.y;
		avatar.IDDB = avatarByID.IDDB;
		if (avatar != null && avatar.IDDB != -1)
		{
			if ((BoardScr.isStartGame || onMainMenu.isOngame) && (int)BoardListOnScr.type == (int)BoardListOnScr.STYLE_2PLAYER)
			{
				avatar.x = Canvas.hw;
				if (avatar.IDDB != GameMidlet.avatar.IDDB)
				{
					avatar.y = 30;
				}
				else
				{
					avatar.y = Canvas.h - 40 * AvMain.hd;
				}
			}
			BoardScr.addInfo(text, 50, avatar.IDDB);
		}
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x0006C834 File Offset: 0x0006AC34
	public static void showFlyText(int fromID, int money)
	{
		if (money == 0)
		{
			return;
		}
		if (!BoardScr.isStartGame)
		{
			int indexByID = BoardScr.getIndexByID(fromID);
			Canvas.addFlyText(money, BoardScr.posAvatar[BoardScr.indexPlayer[indexByID]].x, BoardScr.posAvatar[BoardScr.indexPlayer[indexByID]].y, -1, -1);
		}
		else
		{
			Avatar avatarByID = BoardScr.getAvatarByID(fromID);
			Canvas.addFlyText(money, avatarByID.x, avatarByID.y, -1, -1);
		}
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x0006C8A5 File Offset: 0x0006ACA5
	public static bool setR_B(sbyte roomID1, sbyte boardID1)
	{
		return (int)BoardScr.roomID == (int)roomID1 && (int)BoardScr.boardID == (int)boardID1;
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x0006C8C4 File Offset: 0x0006ACC4
	public virtual void start(int whoMoveFirst, int interval2)
	{
		this.setPosBoard();
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x0006C8CC File Offset: 0x0006ACCC
	public virtual void move(int whoMove, sbyte x, sbyte y, int nextMove)
	{
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x0006C8D0 File Offset: 0x0006ACD0
	public static void addInfo(string info, int time, int id)
	{
		if (id == -1)
		{
			if (BoardScr.chatPublic == null)
			{
				BoardScr.chatPublic = new ChatPopup(time, info, 0);
				BoardScr.chatPublic.setPos(Canvas.hw, Canvas.hh - 20);
			}
			else
			{
				BoardScr.chatPublic.prepareData(time, info);
			}
		}
		else
		{
			for (int i = 0; i < BoardScr.avatarInfos.size(); i++)
			{
				Base @base = (Base)BoardScr.avatarInfos.elementAt(i);
				if (@base.IDDB == id)
				{
					if (@base.chat == null)
					{
						@base.chat = new ChatPopup(time, info, 0);
						@base.chat.setPos(@base.x, @base.y - 65 * AvMain.hd);
					}
					else
					{
						@base.chat.prepareData(time, info);
					}
				}
			}
		}
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x0006C9AC File Offset: 0x0006ADAC
	public void setPosBoard()
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			avatar.setAction(0);
			if (avatar.IDDB != -1)
			{
				num++;
				if (avatar.IDDB != GameMidlet.avatar.IDDB)
				{
					num2 = i;
				}
			}
		}
		int[] array = new int[BoardScr.numPlayer];
		int num3 = 2;
		if (num == 2)
		{
			array[(int)BoardScr.indexOfMe] = 2;
			array[num2] = 0;
		}
		else
		{
			for (int j = (int)BoardScr.indexOfMe; j < (int)BoardScr.indexOfMe + BoardScr.numPlayer; j++)
			{
				int num4 = j;
				if (num4 > BoardScr.numPlayer - 1)
				{
					num4 -= BoardScr.numPlayer;
				}
				array[num4] = num3;
				num3++;
				if (num3 >= BoardScr.numPlayer)
				{
					num3 = 0;
				}
			}
		}
		BoardScr.indexPlayer = array;
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x0006CAA4 File Offset: 0x0006AEA4
	public static Avatar getAvatarByID(int id)
	{
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB == id)
			{
				return avatar;
			}
		}
		return null;
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x0006CAE8 File Offset: 0x0006AEE8
	public static int getIndexByID(int id)
	{
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB == id)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x0006CB2C File Offset: 0x0006AF2C
	public virtual void setPosPlaying()
	{
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != -1)
			{
				avatar.ySat = 0;
				avatar.setAction(0);
				avatar.setFrame((int)avatar.action);
				avatar.xCur = (avatar.x = BoardScr.posAvatar[BoardScr.indexPlayer[i]].x);
				avatar.yCur = (avatar.y = BoardScr.posAvatar[BoardScr.indexPlayer[i]].y);
				if (BoardScr.indexPlayer[i] == 2 || BoardScr.indexPlayer[i] == 3)
				{
					avatar.direct = (avatar.dirFirst = Base.LEFT);
				}
				else
				{
					avatar.direct = (avatar.dirFirst = Base.RIGHT);
				}
			}
		}
	}

	// Token: 0x04000E2A RID: 3626
	public static BoardScr me;

	// Token: 0x04000E2B RID: 3627
	public static bool isStartGame;

	// Token: 0x04000E2C RID: 3628
	public static bool disableReady;

	// Token: 0x04000E2D RID: 3629
	public static bool isGameEnd;

	// Token: 0x04000E2E RID: 3630
	public static MyVector avatarInfos;

	// Token: 0x04000E2F RID: 3631
	public int currentPlayer;

	// Token: 0x04000E30 RID: 3632
	public int selectedCard;

	// Token: 0x04000E31 RID: 3633
	public static sbyte roomID;

	// Token: 0x04000E32 RID: 3634
	public static sbyte boardID;

	// Token: 0x04000E33 RID: 3635
	public static int ownerID;

	// Token: 0x04000E34 RID: 3636
	public static int money;

	// Token: 0x04000E35 RID: 3637
	public static sbyte indexOfMe;

	// Token: 0x04000E36 RID: 3638
	public static long dieTime;

	// Token: 0x04000E37 RID: 3639
	public static long currentTime;

	// Token: 0x04000E38 RID: 3640
	public static int interval;

	// Token: 0x04000E39 RID: 3641
	public static int notReadyDelay;

	// Token: 0x04000E3A RID: 3642
	public static int[] indexPlayer = new int[4];

	// Token: 0x04000E3B RID: 3643
	public int disCard = 10;

	// Token: 0x04000E3C RID: 3644
	public static int wCard;

	// Token: 0x04000E3D RID: 3645
	public static int hcard;

	// Token: 0x04000E3E RID: 3646
	public static Image imgBoard;

	// Token: 0x04000E3F RID: 3647
	public static int xBoard;

	// Token: 0x04000E40 RID: 3648
	public static int yBoard;

	// Token: 0x04000E41 RID: 3649
	public static int wBoard;

	// Token: 0x04000E42 RID: 3650
	public static int hBoard;

	// Token: 0x04000E43 RID: 3651
	public int turn = -1;

	// Token: 0x04000E44 RID: 3652
	public static Command cmdCloseBoard;

	// Token: 0x04000E45 RID: 3653
	public static Command cmdStart;

	// Token: 0x04000E46 RID: 3654
	public static Command cmdBack;

	// Token: 0x04000E47 RID: 3655
	public static Command cmdFire;

	// Token: 0x04000E48 RID: 3656
	public static Command cmdReady;

	// Token: 0x04000E49 RID: 3657
	public static Command cmdWaiting;

	// Token: 0x04000E4A RID: 3658
	public static Command cmdMenu;

	// Token: 0x04000E4B RID: 3659
	public static Image[] imgReady;

	// Token: 0x04000E4C RID: 3660
	public static AvPosition[] posAvatar;

	// Token: 0x04000E4D RID: 3661
	public static MyVector chatHistory = new MyVector();

	// Token: 0x04000E4E RID: 3662
	public static Image imgBan;

	// Token: 0x04000E4F RID: 3663
	public static int numPlayer = 4;

	// Token: 0x04000E50 RID: 3664
	public static MyVector listPosAvatar = new MyVector();

	// Token: 0x04000E51 RID: 3665
	public static MyVector listPosCasino = new MyVector();

	// Token: 0x04000E52 RID: 3666
	private static ChatPopup chatPublic;

	// Token: 0x02000195 RID: 405
	private class IActionClose : IAction
	{
		// Token: 0x06000B05 RID: 2821 RVA: 0x0006CC4E File Offset: 0x0006B04E
		public void perform()
		{
			BoardScr.me.doContinue();
			BoardScr.me.doCloseBoard();
			BoardScr.isStartGame = false;
			BoardScr.me.turn = -1;
			BoardScr.interval = 0;
		}
	}

	// Token: 0x02000196 RID: 406
	private class IActionExit : IAction
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x0006CC7B File Offset: 0x0006B07B
		public IActionExit(BoardScr me)
		{
			this.me = me;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0006CC8C File Offset: 0x0006B08C
		public void perform()
		{
			Canvas.load = 0;
			this.me.resetCard();
			CasinoService.gI().leaveBoard(BoardScr.roomID, BoardScr.boardID);
			CasinoService.gI().requestBoardList(BoardScr.roomID);
			this.me.setPosCam();
			Canvas.endDlg();
			Canvas.load = 0;
		}

		// Token: 0x04000E53 RID: 3667
		private BoardScr me;
	}

	// Token: 0x02000197 RID: 407
	private class IActionSettingMoney : IKbAction
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x0006CCEC File Offset: 0x0006B0EC
		public void perform(string text)
		{
			try
			{
				int num = int.Parse(text);
				if (num >= 0)
				{
					if (MapScr.isNewVersion && num > GameMidlet.avatar.money[3])
					{
						BoardListOnScr.gI().setXeng();
					}
					else
					{
						Canvas.endDlg();
						GlobalService.gI().setMoney(BoardScr.roomID, BoardScr.boardID, num);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x02000198 RID: 408
	private class IActionMaxPlayer : IAction
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x0006CD6C File Offset: 0x0006B16C
		public IActionMaxPlayer(int i)
		{
			this.ii = i;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0006CD7B File Offset: 0x0006B17B
		public void perform()
		{
			CasinoService.gI().setMaxPlayer(this.ii);
		}

		// Token: 0x04000E54 RID: 3668
		private int ii;
	}

	// Token: 0x02000199 RID: 409
	private class IActionPass : IKbAction
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x0006CD95 File Offset: 0x0006B195
		public void perform(string text)
		{
			GlobalService.gI().setPassword(BoardScr.roomID, BoardScr.boardID, text);
			Canvas.startOKDlg(T.setPassed);
		}
	}

	// Token: 0x0200019A RID: 410
	private class IActionKick : IAction
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x0006CDB6 File Offset: 0x0006B1B6
		public IActionKick(int id)
		{
			this.IDDB = id;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0006CDC5 File Offset: 0x0006B1C5
		public void perform()
		{
			CasinoService.gI().kick(BoardScr.roomID, BoardScr.boardID, this.IDDB);
		}

		// Token: 0x04000E55 RID: 3669
		private int IDDB;
	}

	// Token: 0x0200019B RID: 411
	private class IActionAddFriend : IAction
	{
		// Token: 0x06000B10 RID: 2832 RVA: 0x0006CDE1 File Offset: 0x0006B1E1
		public IActionAddFriend(Avatar p)
		{
			this.p = p;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0006CDF0 File Offset: 0x0006B1F0
		public void perform()
		{
			MapScr.gI().doRequestAddFriend(this.p);
		}

		// Token: 0x04000E56 RID: 3670
		private Avatar p;
	}
}
