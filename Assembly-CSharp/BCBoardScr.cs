using System;

// Token: 0x020001B3 RID: 435
public class BCBoardScr : BoardScr
{
	// Token: 0x06000BB4 RID: 2996 RVA: 0x000763B8 File Offset: 0x000747B8
	public BCBoardScr()
	{
		Xingau.array[0] = new int[]
		{
			6,
			0,
			7,
			1,
			6,
			2,
			7,
			3
		};
		Xingau.array[1] = new int[]
		{
			6,
			5,
			7,
			4,
			6,
			3,
			7,
			2
		};
		Xingau.array[2] = new int[]
		{
			7,
			4,
			6,
			1,
			7,
			3,
			6,
			5
		};
		for (int i = 0; i < 5; i++)
		{
			this.moneySV[i] = new sbyte[6];
		}
		this.resetdata();
		this.moneyP = null;
		this.init();
		this.cmdskipBC = new Command(T.skip, 7);
		this.cmdNextBC = new Command(T.continuee, 8);
		this.cmdSkip = new Command(T.skip, 9);
		if (Canvas.w > 200)
		{
			BCBoardScr.rWT = (BCBoardScr.hHT = 23);
			BCBoardScr.rW = (BCBoardScr.hH = 48);
			if (AvMain.hd == 2)
			{
				BCBoardScr.rW = (BCBoardScr.hH = 96);
			}
		}
		this.loadIMG();
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x00076535 File Offset: 0x00074935
	public static BoardScr gI()
	{
		return (BCBoardScr.me != null) ? BCBoardScr.me : (BCBoardScr.me = new BCBoardScr());
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x00076556 File Offset: 0x00074956
	public override void switchToMe()
	{
		this.init();
		base.switchToMe();
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x00076564 File Offset: 0x00074964
	public void resetdata()
	{
		for (int i = 0; i < this.result.Length; i++)
		{
			this.result[i] = -1;
		}
		for (int j = 0; j < this.isFinish.Length; j++)
		{
			this.isFinish[j] = false;
		}
		for (int k = 0; k < this.moneySV.Length; k++)
		{
			for (int l = 0; l < this.moneySV[k].Length; l++)
			{
				this.moneySV[k][l] = 0;
			}
		}
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x000765F4 File Offset: 0x000749F4
	public override void commandTab(int index)
	{
		if (index != 7)
		{
			if (index != 8)
			{
				if (index == 9)
				{
					this.doSkip();
				}
			}
			else
			{
				this.doNextBC();
			}
		}
		else
		{
			this.onSkip();
		}
		base.commandTab(index);
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x00076644 File Offset: 0x00074A44
	public override void init()
	{
		base.init();
		BCBoardScr.posAvatar5 = new AvPosition[]
		{
			new AvPosition(20 * AvMain.hd, 50 + 30 * AvMain.hd, MyGraphics.VCENTER | MyGraphics.LEFT),
			new AvPosition(20 * AvMain.hd, Canvas.hh + 60, MyGraphics.VCENTER | MyGraphics.LEFT),
			new AvPosition(Canvas.hw, Canvas.hCan - Canvas.hTab - 10, MyGraphics.BOTTOM | MyGraphics.HCENTER),
			new AvPosition(Canvas.w - 14 * AvMain.hd, Canvas.hh + 60, MyGraphics.VCENTER | MyGraphics.RIGHT),
			new AvPosition(Canvas.w - 14 * AvMain.hd, 50 + 30 * AvMain.hd, MyGraphics.VCENTER | MyGraphics.RIGHT)
		};
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x00076728 File Offset: 0x00074B28
	private void setAnimateBC()
	{
		sbyte b = 0;
		while ((int)b < 5)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt((int)b);
			if (avatar.IDDB != -1)
			{
				BoardScr.showChat(avatar.IDDB, this.moneyP[(int)b] + string.Empty);
				avatar.setMoneyNew(avatar.getMoneyNew() + this.moneyP[(int)b]);
			}
			b = (sbyte)((int)b + 1);
		}
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x000767A6 File Offset: 0x00074BA6
	public void onSetPlayer(sbyte fromUse, sbyte toUse, int moneyValue)
	{
		this.showFlyText5Baucua(fromUse, toUse, moneyValue);
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x000767B4 File Offset: 0x00074BB4
	public void showFlyText5Baucua(sbyte fro, sbyte to, int money2)
	{
		if (money2 == 0)
		{
			return;
		}
		Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt((int)fro);
		Avatar avatar2 = (Avatar)BoardScr.avatarInfos.elementAt((int)to);
		Point point = new Point(avatar.x, avatar.y);
		point.distant = (short)money2;
		point.color = CRes.rnd(3);
		int g = CRes.angle(avatar2.x - avatar.x, -(avatar2.x - avatar.y));
		point.g = g;
		point.catagory = (sbyte)CRes.rnd(-1, 1);
		point.h = CRes.fixangle(point.g + (int)point.catagory * 90);
		int num = 10 * CRes.cos(point.h) >> 10;
		int num2 = -(10 * CRes.sin(point.h)) >> 10;
		point.xTo = (short)avatar2.x;
		point.yTo = (short)avatar2.y;
		point.x += num;
		point.y += num2;
		point.color = 0;
		point.dis = (sbyte)(CRes.rnd(4) + 2);
		point.height = (short)(8 + CRes.rnd(5));
		this.listFireWork.addElement(point);
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x000768F4 File Offset: 0x00074CF4
	public void checkTimeLimit()
	{
		BoardScr.dieTime = (long)((int)((long)Canvas.getSecond() - BoardScr.currentTime));
		if (!BoardScr.isStartGame || BoardScr.isGameEnd || BoardScr.disableReady)
		{
			return;
		}
		if ((long)BoardScr.interval - BoardScr.dieTime < 0L)
		{
			this.canpointer = true;
			if (GameMidlet.avatar.IDDB != BoardScr.ownerID)
			{
				if ((int)this.autoLuot == 0)
				{
					this.autoLuot = 1;
					this.putMoneyFN();
				}
				if ((int)this.autoLuot == 2)
				{
					this.autoLuot = 3;
					this.onSkip();
				}
			}
		}
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x00076994 File Offset: 0x00074D94
	public void resetGame()
	{
		this.idffr = -1;
		this.idT = -1;
		this.addfr = 0;
		this.addt = 0;
		this.seat = 0;
		this.canTa = false;
		this.taOK = false;
		this.beginCharTa = false;
		this.count = 0;
		this.canpointer = false;
		this.moneyInput.removeAllElements();
		this.vtmoneySV.removeAllElements();
		this.xn.removeAllElements();
		this.countEnter = 0;
		this.isStopXn = false;
		BoardScr.isStartGame = false;
		this.currentPlayer = -1;
		this.autoLuot = 0;
		BoardScr.disableReady = false;
		this.canMoveBoard = false;
		this.resetdata();
		for (int i = 0; i < this.bc.size(); i++)
		{
			PimgBC pimgBC = (PimgBC)this.bc.elementAt(i);
			pimgBC.moneyPut = 0;
		}
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x00076A74 File Offset: 0x00074E74
	private void loadIMG()
	{
		this.bc.removeAllElements();
		this.xbg = Canvas.w / 2 - BCBoardScr.rW - BCBoardScr.rW / 2 - 10;
		this.ybg = Canvas.h / 2 - BCBoardScr.hH - 12;
		for (int i = 0; i < 6; i++)
		{
			PimgBC pimgBC = new PimgBC();
			pimgBC.type = i;
			pimgBC.x = this.xbg + i % 3 * (BCBoardScr.rW + 10);
			pimgBC.y = this.ybg + i / 3 * (BCBoardScr.hH + 8);
			this.bc.addElement(pimgBC);
		}
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00076B1C File Offset: 0x00074F1C
	private void loadXingau()
	{
		int y = 10;
		if (this.xn.size() <= 0)
		{
			if (Canvas.w > 200)
			{
				int num = Canvas.w / 2 - 64 * AvMain.hd;
				for (int i = 0; i < 3; i++)
				{
					this.creatXn(num + i * 64 * AvMain.hd, y, i, i, false);
				}
			}
			else
			{
				int num = Canvas.w / 2 - 49;
				for (int j = 0; j < 3; j++)
				{
					this.creatXn(num + j * 49, 0, j, j, false);
				}
			}
		}
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x00076BBC File Offset: 0x00074FBC
	public void ta()
	{
		if (!this.taOK)
		{
			this.canpointer = true;
			CasinoService.gI().ta(BoardScr.roomID, BoardScr.boardID, this.idffr, this.idT);
			BoardScr.disableReady = true;
			this.currentPlayer = -1;
			this.canMoveBoard = true;
		}
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x00076C0F File Offset: 0x0007500F
	public void onSkip()
	{
		base.setCmdWaiting();
		BoardScr.disableReady = true;
		CasinoService.gI().skip(BoardScr.roomID, BoardScr.boardID);
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x00076C34 File Offset: 0x00075034
	public void reset()
	{
		BoardScr.isGameEnd = false;
		BoardScr.isStartGame = false;
		BoardScr.disableReady = false;
		this.canMoveBoard = false;
		Canvas.startWaitDlg();
		CasinoService.gI().leaveBoard(BoardScr.roomID, BoardScr.boardID);
		CasinoService.gI().requestBoardList(BoardScr.roomID);
		this.resetGame();
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00076C88 File Offset: 0x00075088
	private void setMoney()
	{
		PimgBC pimgBC = (PimgBC)this.bc.elementAt((int)this.index);
		pimgBC.moneyPut++;
		this.paintPutMoney();
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00076CC4 File Offset: 0x000750C4
	public void paintMoneyTa()
	{
		if (this.taOK)
		{
			PimgBC pimgBC = (PimgBC)this.bc.elementAt((int)this.addt);
			int seatATmapSeat = this.getSeatATmapSeat(this.mapSeat, (int)this.seat);
			this.creatSVMoneyPut(pimgBC.x, pimgBC.y, pimgBC.x, pimgBC.y, (int)this.moneySV[(int)this.seat][(int)this.addt], this.getIndex(seatATmapSeat), (int)this.addt, (int)this.addt, false);
			this.taOK = false;
		}
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x00076D5C File Offset: 0x0007515C
	public void paintSVmoney()
	{
		for (int i = 0; i < 6; i++)
		{
			PimgBC pimgBC = (PimgBC)this.bc.elementAt(i);
			int seatATmapSeat = this.getSeatATmapSeat(this.mapSeat, (int)this.seat);
			this.creatSVMoneyPut(pimgBC.x, pimgBC.y, pimgBC.x, pimgBC.y, (int)this.moneySV[(int)this.seat][i], this.getIndex(seatATmapSeat), i, i, false);
		}
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00076DDC File Offset: 0x000751DC
	public void paintXingau(MyGraphics g)
	{
		if (this.xn.size() > 0)
		{
			for (int i = 0; i < this.xn.size(); i++)
			{
				Xingau xingau = (Xingau)this.xn.elementAt(i);
				xingau.paint(g);
			}
		}
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x00076E2F File Offset: 0x0007522F
	private int getIndex(int vt)
	{
		switch (vt)
		{
		case 0:
			return 3;
		case 1:
			return 0;
		case 2:
			return 1;
		case 3:
			return 2;
		default:
			return -1;
		}
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x00076E58 File Offset: 0x00075258
	public void paintPutMoney()
	{
		for (int i = 0; i < 6; i++)
		{
			PimgBC pimgBC = (PimgBC)this.bc.elementAt(i);
			int seatATmapSeat = this.getSeatATmapSeat(this.mapSeat, BoardScr.getIndexByID(GameMidlet.avatar.IDDB));
			this.creatMoneyPut(pimgBC.x + BCBoardScr.rW / 2, pimgBC.y + BCBoardScr.hH / 2, pimgBC.moneyPut, this.getIndex(seatATmapSeat));
		}
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x00076ED4 File Offset: 0x000752D4
	private void creatMoneyPut(int x, int y, int value, int typePaint)
	{
		MoneyPut o = new MoneyPut(x, y, value, typePaint);
		this.moneyInput.addElement(o);
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x00076EF8 File Offset: 0x000752F8
	private void creatXn(int x, int y, int type, int typeStop, bool stopHere)
	{
		Xingau o = new Xingau(x, y, type, typeStop, stopHere);
		this.xn.addElement(o);
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x00076F20 File Offset: 0x00075320
	private void creatSVMoneyPut(int x, int y, int xto, int yto, int value, int typePaint, int addFrom, int addTo, bool isMoveOK)
	{
		MoneySV o = new MoneySV(x, y, xto, yto, value, typePaint, addFrom, addTo, isMoveOK);
		this.vtmoneySV.addElement(o);
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x00076F50 File Offset: 0x00075350
	public void paintImgBC(MyGraphics g)
	{
		if (this.bc.size() > 0)
		{
			if ((int)this.idffr != -1)
			{
				g.setColor(16777215);
				if (Canvas.gameTick % 20 > 10)
				{
					g.fillRect((float)(this.xbg + (int)this.idffr % 3 * (BCBoardScr.rW + 10)), (float)(this.ybg + (int)this.idffr / 3 * (BCBoardScr.hH + 8)), (float)BCBoardScr.rW, (float)BCBoardScr.hH);
				}
			}
			if ((int)this.idT != -1)
			{
				g.setColor(1112500);
				if (Canvas.gameTick % 20 > 10)
				{
					g.fillRect((float)(this.xbg + (int)this.idT % 3 * (BCBoardScr.rW + 10)), (float)(this.ybg + (int)this.idT / 3 * (BCBoardScr.hH + 8)), (float)BCBoardScr.rW, (float)BCBoardScr.hH);
				}
			}
			for (int i = 0; i < this.bc.size(); i++)
			{
				PimgBC pimgBC = (PimgBC)this.bc.elementAt(i);
				if (AvatarData.getImgIcon(872).count != -1)
				{
					g.drawRegion(AvatarData.getImgIcon(872).img, 0f, (float)(pimgBC.type * BCBoardScr.hH), BCBoardScr.rW, BCBoardScr.hH, 0, (float)(this.xbg + i % 3 * (BCBoardScr.rW + 10)), (float)(this.ybg + i / 3 * (BCBoardScr.hH + 8)), 0);
				}
			}
		}
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x000770E4 File Offset: 0x000754E4
	public void paintMoneyAtPlayer(MyGraphics g)
	{
		for (int i = 0; i < BoardScr.avatarInfos.size(); i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB == BoardScr.ownerID || avatar.IDDB != -1)
			{
				if (this.currentPlayer != avatar.IDDB || Canvas.gameTick % 10 >= 5)
				{
					Canvas.smallFontYellow.drawString(g, avatar.getMoneyNew() + " " + T.getMoney(), avatar.x, avatar.y + 5, 2);
				}
				int seatATmapSeat = this.getSeatATmapSeat(this.mapSeat, BoardScr.getIndexByID(avatar.IDDB));
				if (seatATmapSeat != -1 && AvatarData.getImgIcon(871).count != -1)
				{
					g.drawRegion(AvatarData.getImgIcon(871).img, 0f, (float)(this.getIndex(seatATmapSeat) * 12), 12, 12, 0, (float)avatar.x, (float)(avatar.y + 5 + (int)AvMain.hSmall), MyGraphics.TOP | MyGraphics.HCENTER);
				}
			}
		}
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x00077214 File Offset: 0x00075614
	public void onSetTurn(sbyte seatI)
	{
		int indexByID = BoardScr.getIndexByID(GameMidlet.avatar.IDDB);
		Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt((int)seatI);
		if (indexByID == (int)seatI)
		{
			this.isFinish[indexByID] = false;
			this.canTa = true;
			this.right = null;
			this.autoLuot = 2;
			this.canpointer = false;
		}
		this.currentPlayer = avatar.IDDB;
		BoardScr.interval = (int)this.saveTime;
		BoardScr.currentTime = (long)Canvas.getSecond();
		if (!this.beginCharTa)
		{
			this.beginCharTa = true;
		}
		if (GameMidlet.avatar.IDDB != BoardScr.ownerID && indexByID == (int)seatI)
		{
			this.setBotCmdTa();
		}
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x000772C7 File Offset: 0x000756C7
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		Canvas.resetTrans(g);
		this.paintFireWork(g);
		base.paint(g);
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x000772E4 File Offset: 0x000756E4
	private void paintFireWork(MyGraphics g)
	{
		for (int i = 0; i < this.listFireWork.size(); i++)
		{
			Point point = (Point)this.listFireWork.elementAt(i);
			if ((int)point.dis >= 0)
			{
				Canvas.numberFont.drawString(g, "+" + point.distant, point.x, point.y, 2);
			}
		}
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x0007735C File Offset: 0x0007575C
	public override void paintMain(MyGraphics g)
	{
		base.paintMain(g);
		if (BoardScr.isStartGame || BoardScr.disableReady)
		{
			Canvas.resetTrans(g);
			this.paintImgBC(g);
		}
		this.paintNamePlayers(g);
		if (BoardScr.isStartGame || BoardScr.disableReady)
		{
			Canvas.resetTrans(g);
			this.paintMoneyAtPlayer(g);
			if (BoardScr.isStartGame || BoardScr.disableReady)
			{
				int num = (int)((long)BoardScr.interval - BoardScr.dieTime);
				if (num > 0 && !BoardScr.isGameEnd && this.xn.size() <= 0)
				{
					Canvas.numberFont.drawString(g, num + string.Empty, Canvas.hw, 10, 2);
				}
				if (this.beginCharTa)
				{
					if ((int)this.count < 100)
					{
						this.count = (sbyte)((int)this.count + 1);
					}
					else
					{
						this.count = 100;
					}
					if ((int)this.count < 50)
					{
						Canvas.borderFont.drawString(g, T.startTa, Canvas.hw, this.ybg - 40, 2);
					}
				}
			}
			if (this.moneyInput.size() > 0)
			{
				for (int i = 0; i < this.moneyInput.size(); i++)
				{
					MoneyPut moneyPut = (MoneyPut)this.moneyInput.elementAt(i);
					if (moneyPut.valuea > 0)
					{
						moneyPut.paint(g);
					}
				}
			}
			if (this.vtmoneySV.size() > 0)
			{
				for (int j = 0; j < this.vtmoneySV.size(); j++)
				{
					MoneySV moneySV = (MoneySV)this.vtmoneySV.elementAt(j);
					if (moneySV.valuea > 0)
					{
						moneySV.paint(g);
					}
				}
			}
			if (GameMidlet.avatar.IDDB != BoardScr.ownerID && BoardScr.isStartGame && this.xn.size() == 0)
			{
				g.drawImage(BCBoardScr.pointer, (float)(this.xbg + BCBoardScr.rW / 2 + (int)this.index % 3 * (BCBoardScr.rW + 10)), (float)(this.ybg + BCBoardScr.hH / 2 + (int)this.index / 3 * (BCBoardScr.hH + 8) + Canvas.gameTick % 4 + 5), 3);
			}
			this.paintXingau(g);
		}
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x000775B8 File Offset: 0x000759B8
	public void putMoneyFN()
	{
		base.setCmdWaiting();
		this.canpointer = true;
		CasinoService.gI().PutMoneyOk(this.bc, BoardScr.roomID, BoardScr.boardID);
		this.moneyInput.removeAllElements();
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x000775EC File Offset: 0x000759EC
	public override void updateKey()
	{
		base.updateKey();
		this.updateK();
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x000775FC File Offset: 0x000759FC
	private void updatePointer()
	{
		if (this.canpointer)
		{
			return;
		}
		if (BoardScr.isStartGame && !BoardScr.isGameEnd && this.bc.size() > 0 && Canvas.isPointerClick)
		{
			Canvas.isPointerClick = false;
			for (int i = 0; i < this.bc.size(); i++)
			{
				PimgBC pimgBC = (PimgBC)this.bc.elementAt(i);
				if (Canvas.px >= pimgBC.x && Canvas.px <= pimgBC.x + BCBoardScr.rW && Canvas.py >= pimgBC.y && Canvas.py <= pimgBC.y + BCBoardScr.hH)
				{
					this.index = (sbyte)i;
					this.pointerFire();
					break;
				}
			}
		}
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x000776D7 File Offset: 0x00075AD7
	private void updateK()
	{
		if (!this.isFinish[BoardScr.getIndexByID(GameMidlet.avatar.IDDB)] && GameMidlet.avatar.IDDB != BoardScr.ownerID)
		{
			this.updatePointer();
		}
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00077710 File Offset: 0x00075B10
	private void testMoneySVupdate()
	{
		if (this.vtmoneySV.size() > 0 && this.bc.size() > 0)
		{
			for (int i = 0; i < this.vtmoneySV.size(); i++)
			{
				MoneySV moneySV = (MoneySV)this.vtmoneySV.elementAt(i);
				moneySV.update();
				if (moneySV.move)
				{
					this.vtmoneySV.removeElement(moneySV);
					this.paintMoneyTa();
				}
			}
			PimgBC pimgBC = (PimgBC)this.bc.elementAt((int)this.addt);
			if (this.taOK)
			{
				for (int j = 0; j < this.vtmoneySV.size(); j++)
				{
					MoneySV moneySV2 = (MoneySV)this.vtmoneySV.elementAt(j);
					if (moneySV2.addFrom == (int)this.addfr)
					{
						moneySV2.xto = pimgBC.x;
						moneySV2.yto = pimgBC.y;
						moneySV2.isMoveOK = true;
					}
				}
			}
		}
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00077817 File Offset: 0x00075C17
	public override void doReady()
	{
		base.doReady();
		if (!BoardScr.isStartGame && !BoardScr.disableReady)
		{
			this.resetGame();
		}
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x0007783C File Offset: 0x00075C3C
	public override void update()
	{
		base.update();
		if (BoardScr.isStartGame || BoardScr.disableReady)
		{
			this.checkTimeLimit();
			this.testMoneySVupdate();
			if (this.xn.size() > 0)
			{
				for (int i = 0; i < this.xn.size(); i++)
				{
					Xingau xingau = (Xingau)this.xn.elementAt(i);
					xingau.update();
					if (this.isStopXn)
					{
						xingau.typeStop = (int)this.result[i];
						xingau.stopHere = true;
					}
				}
			}
			for (int j = 0; j < this.listFireWork.size(); j++)
			{
				Point point = (Point)this.listFireWork.elementAt(j);
				int num = CRes.angle((int)point.xTo - point.x, -((int)point.yTo - point.y));
				if (CRes.abs(num - point.h) > 10)
				{
					point.h -= (int)point.height * (int)point.catagory;
					point.h = CRes.fixangle(point.h);
				}
				else
				{
					point.h = num;
					Point point2 = point;
					point2.dis = (sbyte)((int)point2.dis + 2);
				}
				if (point.color >= 4)
				{
					point.color = 0;
				}
				point.color++;
				int num2 = (int)point.dis * CRes.cos(point.h) >> 10;
				int num3 = -((int)point.dis * CRes.sin(point.h)) >> 10;
				if (CRes.distance(point.x, point.y, (int)point.xTo, (int)point.yTo) >= (int)point.dis)
				{
					point.x += num2;
					point.y += num3;
				}
				else
				{
					this.listFireWork.removeElement(point);
				}
			}
		}
		else
		{
			this.updateReady();
		}
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x00077A38 File Offset: 0x00075E38
	public void onFinish(int[] moneyP)
	{
		this.moneyP = moneyP;
		this.isStopXn = true;
		this.canMoveBoard = false;
		BoardScr.isGameEnd = true;
		this.right = null;
		this.beginCharTa = false;
		this.count = 0;
		this.center = this.cmdNextBC;
		this.setAnimateBC();
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x00077A88 File Offset: 0x00075E88
	protected void doNextBC()
	{
		this.doContinue();
		this.listFireWork.removeAllElements();
		BoardScr.isGameEnd = false;
		BoardScr.isStartGame = false;
		BoardScr.disableReady = false;
		this.currentPlayer = -1;
		this.moneyP = null;
		this.moneyInput.removeAllElements();
		this.vtmoneySV.removeAllElements();
		this.idffr = -1;
		this.idT = -1;
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00077AEC File Offset: 0x00075EEC
	public void onPlaying(sbyte time)
	{
		BoardScr.isStartGame = false;
		BoardScr.disableReady = true;
		this.mapSeat.removeAllElements();
		this.setPosPlaying();
		for (int i = 0; i < BoardScr.avatarInfos.size(); i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != BoardScr.ownerID)
			{
				this.mapSeat.addElement(i + string.Empty);
			}
		}
		this.paintSVmoney();
		this.center = BoardScr.cmdWaiting;
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00077B7E File Offset: 0x00075F7E
	public void setCmdWaitingBC()
	{
		this.center = null;
		this.right = null;
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x00077B8E File Offset: 0x00075F8E
	public void setCmdWaitingRechoose()
	{
		this.right = null;
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x00077B98 File Offset: 0x00075F98
	private void pointerFire()
	{
		if (!this.canTa)
		{
			if (!this.isFinish[BoardScr.getIndexByID(GameMidlet.avatar.IDDB)])
			{
				if ((int)this.countEnter < 6)
				{
					this.setMoney();
				}
				this.countEnter = (sbyte)((int)this.countEnter + 1);
			}
		}
		else if ((int)this.idT == -1)
		{
			if ((int)this.idffr == -1)
			{
				MoneySV moneySV = (MoneySV)this.vtmoneySV.elementAt((int)this.index);
				if (moneySV.valuea > 0)
				{
					this.idffr = this.index;
					this.center.caption = T.ta;
					this.setBotCmdReChoose();
				}
			}
			else
			{
				this.idT = this.index;
				this.ta();
			}
		}
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00077C6C File Offset: 0x0007606C
	public override void doFire()
	{
		if (!this.canTa)
		{
			if (!this.isFinish[BoardScr.getIndexByID(GameMidlet.avatar.IDDB)])
			{
				if ((int)this.countEnter < 6)
				{
					this.setMoney();
				}
				this.countEnter = (sbyte)((int)this.countEnter + 1);
			}
		}
		else if ((int)this.idT == -1)
		{
			if ((int)this.idffr == -1)
			{
				MoneySV moneySV = (MoneySV)this.vtmoneySV.elementAt((int)this.index);
				if (moneySV.valuea > 0)
				{
					this.idffr = this.index;
					this.center.caption = T.ta;
					this.setBotCmdReChoose();
				}
			}
			else
			{
				this.idT = this.index;
				this.ta();
			}
		}
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00077D40 File Offset: 0x00076140
	public void doSkip()
	{
		if (!this.canTa)
		{
			if (!this.isFinish[BoardScr.getIndexByID(GameMidlet.avatar.IDDB)])
			{
				this.autoLuot = 1;
				this.putMoneyFN();
			}
		}
		else if ((int)this.idffr != -1)
		{
			this.idffr = -1;
			this.center.caption = T.selectt;
			this.right = this.cmdskipBC;
		}
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00077DB5 File Offset: 0x000761B5
	public void setBotCmd()
	{
		this.center = BoardScr.cmdFire;
		this.right = this.cmdSkip;
		this.center.caption = T.sett;
		this.right.caption = T.finish;
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00077DEE File Offset: 0x000761EE
	private void setBotCmdTa()
	{
		this.center = BoardScr.cmdFire;
		this.center.caption = T.selectt;
		this.right = this.cmdskipBC;
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x00077E17 File Offset: 0x00076217
	private void setBotCmdReChoose()
	{
		this.right = this.cmdSkip;
		this.right.caption = T.selectAgain;
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x00077E38 File Offset: 0x00076238
	public int getSeatATmapSeat(MyVector info, int ghe)
	{
		for (int i = 0; i < info.size(); i++)
		{
			string text = (string)info.elementAt(i);
			if (text.Equals(ghe + string.Empty))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00077E88 File Offset: 0x00076288
	public new void setPosBoard()
	{
		BoardScr.me.setPosBoard();
		this.mapSeat.removeAllElements();
		for (int i = 0; i < BoardScr.avatarInfos.size(); i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != BoardScr.ownerID)
			{
				this.mapSeat.addElement(i + string.Empty);
			}
		}
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00077F04 File Offset: 0x00076304
	public void onStartGame(sbyte boardID6, sbyte roomID6, sbyte interval2)
	{
		base.start(0, (int)interval2);
		Canvas.endDlg();
		this.resetGame();
		base.resetReady();
		this.mapSeat.removeAllElements();
		this.setPosPlaying();
		for (int i = 0; i < BoardScr.avatarInfos.size(); i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != BoardScr.ownerID)
			{
				this.mapSeat.addElement(i + string.Empty);
			}
		}
		if (GameMidlet.avatar.IDDB != BoardScr.ownerID)
		{
			this.setBotCmd();
		}
		else
		{
			this.center = null;
			this.right = null;
		}
		BoardScr.isGameEnd = false;
		BoardScr.isStartGame = true;
		BoardScr.interval = (int)interval2;
		BoardScr.currentTime = (long)Canvas.getSecond();
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x00077FDE File Offset: 0x000763DE
	public void onMove(sbyte idseat)
	{
		this.seat = idseat;
		this.isFinish[(int)this.seat] = true;
		this.paintSVmoney();
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00077FFC File Offset: 0x000763FC
	public void onHaphom(sbyte seatHP, sbyte fromHP, sbyte toHP)
	{
		if ((int)fromHP != (int)toHP)
		{
			this.seat = seatHP;
			this.addfr = fromHP;
			this.addt = toHP;
			this.taOK = true;
			this.autoLuot = 3;
		}
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x0007802C File Offset: 0x0007642C
	public void onResult(sbyte[] resultSV)
	{
		this.result = resultSV;
		MyVector myVector = new MyVector();
		for (int i = 0; i < 6; i++)
		{
			PimgBC pimgBC = new PimgBC();
			if (i == (int)this.result[0])
			{
				pimgBC.moneyPut = 6;
			}
			myVector.addElement(pimgBC);
		}
		CasinoService.gI().PutMoneyOk(myVector, BoardScr.roomID, BoardScr.boardID);
		this.loadXingau();
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x00078098 File Offset: 0x00076498
	public override void setPosPlaying()
	{
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != -1)
			{
				avatar.ySat = 0;
				avatar.setAction(0);
				avatar.setFrame((int)avatar.action);
				avatar.xCur = (avatar.x = BCBoardScr.posAvatar5[BoardScr.indexPlayer[i]].x);
				avatar.yCur = (avatar.y = BCBoardScr.posAvatar5[BoardScr.indexPlayer[i]].y);
				if (BoardScr.indexPlayer[i] == 2 || BoardScr.indexPlayer[i] == 3 || BoardScr.indexPlayer[i] == 4)
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

	// Token: 0x04000F03 RID: 3843
	public new static BCBoardScr me;

	// Token: 0x04000F04 RID: 3844
	private MyVector moneyInput = new MyVector();

	// Token: 0x04000F05 RID: 3845
	private MyVector vtmoneySV = new MyVector();

	// Token: 0x04000F06 RID: 3846
	private MyVector mapSeat = new MyVector();

	// Token: 0x04000F07 RID: 3847
	private MyVector xn = new MyVector();

	// Token: 0x04000F08 RID: 3848
	private MyVector bc = new MyVector();

	// Token: 0x04000F09 RID: 3849
	private Command cmdskipBC;

	// Token: 0x04000F0A RID: 3850
	private Command cmdNextBC;

	// Token: 0x04000F0B RID: 3851
	public int xbg;

	// Token: 0x04000F0C RID: 3852
	public int xFC;

	// Token: 0x04000F0D RID: 3853
	public int ybg;

	// Token: 0x04000F0E RID: 3854
	public sbyte idffr = -1;

	// Token: 0x04000F0F RID: 3855
	public sbyte idT = -1;

	// Token: 0x04000F10 RID: 3856
	public sbyte index;

	// Token: 0x04000F11 RID: 3857
	public sbyte addfr;

	// Token: 0x04000F12 RID: 3858
	public sbyte addt;

	// Token: 0x04000F13 RID: 3859
	public sbyte seat;

	// Token: 0x04000F14 RID: 3860
	public sbyte countEnter;

	// Token: 0x04000F15 RID: 3861
	public sbyte saveTime;

	// Token: 0x04000F16 RID: 3862
	public static int rWT;

	// Token: 0x04000F17 RID: 3863
	public static int hHT;

	// Token: 0x04000F18 RID: 3864
	public static int rWSM;

	// Token: 0x04000F19 RID: 3865
	public static int hHSM;

	// Token: 0x04000F1A RID: 3866
	private bool[] isFinish = new bool[6];

	// Token: 0x04000F1B RID: 3867
	public sbyte[][] moneySV = new sbyte[5][];

	// Token: 0x04000F1C RID: 3868
	public sbyte[] result = new sbyte[3];

	// Token: 0x04000F1D RID: 3869
	private sbyte count;

	// Token: 0x04000F1E RID: 3870
	private sbyte autoLuot;

	// Token: 0x04000F1F RID: 3871
	private bool canTa;

	// Token: 0x04000F20 RID: 3872
	private bool taOK;

	// Token: 0x04000F21 RID: 3873
	private bool beginCharTa;

	// Token: 0x04000F22 RID: 3874
	private bool isStopXn;

	// Token: 0x04000F23 RID: 3875
	public bool canpointer;

	// Token: 0x04000F24 RID: 3876
	private int[] moneyP;

	// Token: 0x04000F25 RID: 3877
	public static int th = MyGraphics.TOP | MyGraphics.HCENTER;

	// Token: 0x04000F26 RID: 3878
	public static int bh = MyGraphics.BOTTOM | MyGraphics.HCENTER;

	// Token: 0x04000F27 RID: 3879
	private bool canMoveBoard;

	// Token: 0x04000F28 RID: 3880
	public static Image pointer;

	// Token: 0x04000F29 RID: 3881
	public static int rW;

	// Token: 0x04000F2A RID: 3882
	public static int hH;

	// Token: 0x04000F2B RID: 3883
	public static AvPosition[] posAvatar5;

	// Token: 0x04000F2C RID: 3884
	private MyVector listFireWork = new MyVector();

	// Token: 0x04000F2D RID: 3885
	private sbyte addY;

	// Token: 0x04000F2E RID: 3886
	private Command cmdSkip;
}
