using System;

// Token: 0x020001B8 RID: 440
public class BoardListOnScr : OnScreen
{
	// Token: 0x06000BF9 RID: 3065 RVA: 0x00078674 File Offset: 0x00076A74
	public BoardListOnScr()
	{
		this.defX = 0;
		this.cmdSellect = new Command(T.selectt, 1);
		this.center = new Command(T.playNow, 5);
		this.right = new Command("Đến bàn", 4);
		base.addCmd(2, 1);
		this.wSmall = (short)(110 * AvMain.hd);
		this.nBoardPerLine = Canvas.w / (int)this.wSmall + 1;
		if (this.nBoardPerLine * (int)this.wSmall > Canvas.w - (int)(this.wSmall / 2))
		{
			this.nBoardPerLine--;
		}
		this.xStart = (int)(this.wSmall / 2);
		this.yStart = (int)(this.wSmall / 2) + 20 * AvMain.hd;
		this.yStart += 10;
		if (Canvas.w > this.nBoardPerLine * (int)this.wSmall)
		{
			this.xStart = (Canvas.w - this.nBoardPerLine * (int)this.wSmall) / 2 + (int)(this.wSmall / 2);
		}
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x00078794 File Offset: 0x00076B94
	public static BoardListOnScr gI()
	{
		return (BoardListOnScr.me != null) ? BoardListOnScr.me : (BoardListOnScr.me = new BoardListOnScr());
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x000787B5 File Offset: 0x00076BB5
	public override void switchToMe()
	{
		base.repaint();
		this.selected = 0;
		this.loadImgBoard();
		this.isHide = true;
		RoomListOnScr.gI().roomList.removeAllElements();
		this.countPaint = 0;
		base.switchToMe();
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x000787ED File Offset: 0x00076BED
	public void loadImgBoard()
	{
		Canvas.paint.initImgBoard((int)BoardListOnScr.type);
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x000787FF File Offset: 0x00076BFF
	public new void close()
	{
		Canvas.startWaitDlg();
		this.doExitBoardList();
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x0007880C File Offset: 0x00076C0C
	public override void commandTab(int index)
	{
		switch (index)
		{
		case 1:
			this.doJoinBoard();
			break;
		case 2:
			this.doExitBoardList();
			break;
		case 3:
			this.commandAction(1);
			break;
		case 4:
			this.doAskForBoardToGo();
			break;
		case 5:
			Canvas.startWaitDlg();
			CasinoService.gI().joinAnyBoard();
			break;
		}
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0007887C File Offset: 0x00076C7C
	public override void commandAction(int index)
	{
		if (index != 1)
		{
			if (index != 3)
			{
				if (index == 4)
				{
					this.doExitBoardList();
				}
			}
			else
			{
				Canvas.startWaitDlg();
				GlobalService.gI().requestInfoOf(GameMidlet.avatar.IDDB);
			}
		}
		else
		{
			Canvas.startWaitDlg(T.pleaseWait);
			CasinoService.gI().requestBoardList(this.roomID);
		}
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x000788EB File Offset: 0x00076CEB
	private void doAskForBoardToGo()
	{
		ipKeyboard.openKeyBoard(T.goToBoard, ipKeyboard.NUMBERIC, string.Empty, new BoardListOnScr.IActionToGo(this), false);
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x00078908 File Offset: 0x00076D08
	protected void doAskForPass()
	{
		ipKeyboard.openKeyBoard(T.ifPassword, ipKeyboard.PASS, string.Empty, new BoardListOnScr.IActionPass(), false);
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x00078924 File Offset: 0x00076D24
	public void setXeng()
	{
		Canvas.startOKDlg("Hiện tại bạn không đủ Xèng để tham gia màn chơi, bạn có muốn nạp thêm Xèng không?", new BoardListOnScr.IActionXeng());
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x00078938 File Offset: 0x00076D38
	protected void doJoinBoard()
	{
		BoardInfo boardInfo = (BoardInfo)this.boardList.elementAt(this.selected);
		if (MapScr.isNewVersion && boardInfo.money > GameMidlet.avatar.money[3])
		{
			BoardListOnScr.gI().setXeng();
			return;
		}
		if (boardInfo.isPass)
		{
			ipKeyboard.openKeyBoard(T.ifPassword, ipKeyboard.NUMBERIC, string.Empty, new BoardListOnScr.IActionJoinBoard(), false);
		}
		else
		{
			CasinoService.gI().joinBoard(this.roomID, boardInfo.boardID, string.Empty);
			Canvas.load = 0;
		}
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x000789D3 File Offset: 0x00076DD3
	private void doExitBoardList()
	{
		Canvas.cameraList.close();
		CasinoService.gI().requestRoomList();
		Canvas.load = 0;
		RoomListOnScr.gI().switchToMe();
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x000789FC File Offset: 0x00076DFC
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		Canvas.paint.paintDefaultBg(g);
		onMainMenu.paintTitle(g, string.Concat(new object[]
		{
			T.room,
			RoomListOnScr.title,
			" ",
			this.roomID
		}), Canvas.w / 2, 30 * AvMain.hd - this.cmy);
		if (this.boardList.size() > 0 && Canvas.load == -1)
		{
			this.paintBoardList(g);
		}
		base.paint(g);
		Canvas.paintPlus2(g);
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00078A98 File Offset: 0x00076E98
	public void paintBoardList(MyGraphics g)
	{
		g.translate((float)this.xStart, (float)this.yStart);
		g.translate(0f, (float)(-(float)this.cmy));
		int num = this.cmy / (int)this.wSmall * this.nBoardPerLine - this.nBoardPerLine;
		if (num < 0)
		{
			num = 0;
		}
		int num2 = num + Canvas.h / (int)this.wSmall * this.nBoardPerLine + this.nBoardPerLine * 2 + this.nBoardPerLine;
		if (num2 > this.boardList.size())
		{
			num2 = this.boardList.size();
		}
		int width = this.imgTitleBoard.getWidth();
		int height = this.imgTitleBoard.getHeight();
		int num3 = num;
		while (num3 < num2 && num3 < this.boardList.size() && num3 < this.countPaint)
		{
			int num4 = num3 % this.nBoardPerLine * (int)this.wSmall;
			int num5 = num3 / this.nBoardPerLine * (int)this.wSmall;
			BoardInfo boardInfo = (BoardInfo)this.boardList.elementAt(num3);
			if (num3 == this.selected && !this.isHide)
			{
				g.drawImage(BoardListOnScr.imgSelectBoard, (float)num4, (float)(num5 + 10 * AvMain.hd), 3);
			}
			BoardListOnScr.imgBoard.drawFrame((int)boardInfo.nPlayer, num4, num5 + 10 * AvMain.hd, 0, 3, g);
			g.drawImage(this.imgTitleBoard, (float)num4, (float)(num5 - BoardListOnScr.imgBoard.frameHeight / 2 - 5 * AvMain.hd), 3);
			Canvas.menuFont.drawString(g, string.Empty + boardInfo.boardID, num4 - width / 2 + 10 * AvMain.hd, num5 - BoardListOnScr.imgBoard.frameHeight / 2 - 5 * AvMain.hd - Canvas.menuFont.getHeight() / 2, 2);
			Canvas.numberFont.drawString(g, boardInfo.strMoney, num4 + 8 * AvMain.hd, num5 - BoardListOnScr.imgBoard.frameHeight / 2 - 5 * AvMain.hd - Canvas.numberFont.getHeight() / 2, 2);
			if (((int)BoardListOnScr.type == (int)BoardListOnScr.STYLE_4PLAYER && (int)boardInfo.maxPlayer < 4) || ((int)BoardListOnScr.type == (int)BoardListOnScr.STYLE_5PLAYER && (int)boardInfo.maxPlayer < 5))
			{
				g.drawImage(this.imgNumPlayer, (float)(num4 + width / 2), (float)(num5 - BoardListOnScr.imgBoard.frameHeight / 2 - 5 * AvMain.hd), 3);
				Canvas.menuFont.drawString(g, string.Empty + boardInfo.maxPlayer, num4 + width / 2, num5 - BoardListOnScr.imgBoard.frameHeight / 2 - 5 * AvMain.hd - Canvas.menuFont.getHeight() / 2 + 1, 2);
			}
			if (boardInfo.isPlaying)
			{
				g.drawImage(this.imgPlaying, (float)num4, (float)(num5 + 10 * AvMain.hd), 3);
			}
			if (boardInfo.isPass)
			{
				g.drawImage(BoardListOnScr.imgLock, (float)(num4 + (int)(this.wSmall / 2) - 10 * AvMain.hd), (float)(num5 + (int)(this.wSmall / 2) - 14 * AvMain.hd), 3);
			}
			num3++;
		}
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00078DF0 File Offset: 0x000771F0
	public override void updateKey()
	{
		base.updateKey();
		this.count++;
		if (Canvas.isPointerClick)
		{
			this.pyLast = Canvas.pyLast;
			this.isG = false;
			if (Canvas.isPoint(0, 0, Canvas.w, this.dis))
			{
				if (this.vY != 0)
				{
					this.isG = true;
				}
				this.pa = this.cmtoY;
				this.timeDelay = (long)this.count;
				this.trans = true;
			}
			Canvas.isPointerClick = false;
		}
		if (this.trans)
		{
			int num = this.pyLast - Canvas.py;
			int i = this.pyLast - Canvas.py;
			this.pyLast = Canvas.py;
			long num2 = (long)this.count - this.timeDelay;
			if (Canvas.isPointerDown)
			{
				if (this.count % 2 == 0)
				{
					this.dyTran = Canvas.py;
					this.timePoint = this.count;
				}
				this.vY = 0;
				if (global::Math.abs(num) < 5 && global::Math.abs(i) < 5)
				{
					int num3 = (Canvas.py - (this.yStart - (int)(this.wSmall / 2)) + this.cmtoY) / (int)this.wSmall;
					int num4 = (Canvas.px - (this.xStart - (int)(this.wSmall / 2))) / (int)this.wSmall;
					if (num3 >= 0 && num3 < this.boardList.size() && Canvas.isPoint(this.xStart - (int)(this.wSmall / 2), 0, Canvas.w - (this.xStart - (int)(this.wSmall / 2)) * 2, this.dis))
					{
						this.selected = num3 * this.nBoardPerLine + num4;
					}
				}
				if (CRes.abs(Canvas.dy()) >= 5 && CRes.abs(Canvas.dx()) >= 5)
				{
					this.isHide = true;
				}
				else if (num2 > 3L && num2 < 8L)
				{
					int num5 = (Canvas.py - (this.yStart - (int)(this.wSmall / 2)) + this.cmtoY) / (int)this.wSmall;
					if (num5 >= 0 && num5 < this.boardList.size() && !this.isG)
					{
						this.isHide = false;
					}
				}
				if (this.cmtoY < 0 || this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.pa + num / 2;
					this.pa = this.cmtoY;
				}
				else
				{
					this.cmtoY = this.pa + num / 2;
					this.pa = this.cmtoY;
				}
				this.cmy = this.cmtoY;
			}
			if (Canvas.isPointerRelease && Canvas.isPoint(0, 0, Canvas.w, this.dis))
			{
				this.isG = false;
				int num6 = this.count - this.timePoint;
				int num7 = this.dyTran - Canvas.py;
				if (CRes.abs(num7) > 40 && num6 < 10 && this.cmtoY > 0 && this.cmtoY < this.cmyLim)
				{
					this.vY = num7 / num6 * 10;
				}
				this.timePoint = -1;
				if (global::Math.abs(num) < 5 && global::Math.abs(i) < 5)
				{
					if (num2 <= 4L)
					{
						this.isHide = false;
						this.timeOpen = 5;
					}
					else if (!this.isHide)
					{
						this.click();
					}
				}
				this.trans = false;
				Canvas.isPointerRelease = false;
			}
		}
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x0007917C File Offset: 0x0007757C
	private void click()
	{
		this.cmdSellect.perform();
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x0007918C File Offset: 0x0007758C
	public void setBoardList(MyVector boardList)
	{
		this.boardList = boardList;
		this.dis = Canvas.hCan - PaintPopup.hButtonSmall;
		int num = boardList.size() / this.nBoardPerLine;
		if (boardList.size() % this.nBoardPerLine > 0)
		{
			num++;
		}
		this.cmyLim = num * (int)this.wSmall - this.dis;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00079200 File Offset: 0x00077600
	public void setCam()
	{
		int num = this.boardList.size() / this.nBoardPerLine;
		if (this.boardList.size() % this.nBoardPerLine != 0)
		{
			num++;
		}
		this.yStart = 110 * AvMain.hd;
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x0007924C File Offset: 0x0007764C
	public override void update()
	{
		if (this.timeOpen > 0)
		{
			this.timeOpen--;
			if (this.timeOpen == 0)
			{
				this.click();
			}
		}
		if (this.countPaint < this.boardList.size())
		{
			this.countPaint++;
		}
		this.moveCamera();
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x000792B0 File Offset: 0x000776B0
	public void moveCamera()
	{
		if (this.vY != 0)
		{
			if (this.cmy < 0 || this.cmy > this.cmyLim)
			{
				this.vY -= this.vY / 4;
				this.cmy += this.vY / 20;
				if (this.vY / 10 <= 1)
				{
					this.vY = 0;
				}
			}
			if (this.cmy < 0)
			{
				if (this.cmy < -this.dis / 2)
				{
					this.cmy = -this.dis / 2;
					this.cmtoY = 0;
					this.vY = 0;
				}
			}
			else if (this.cmy > this.cmyLim)
			{
				if (this.cmy < this.cmyLim + this.dis / 2)
				{
					this.cmy = this.cmyLim + this.dis / 2;
					this.cmtoY = this.cmyLim;
					this.vY = 0;
				}
			}
			else
			{
				this.cmy += this.vY / 10;
			}
			this.cmtoY = this.cmy;
			this.vY -= this.vY / 10;
			if (this.vY / 10 == 0)
			{
				this.vY = 0;
			}
		}
		else if (this.cmy < 0)
		{
			this.cmtoY = 0;
		}
		else if (this.cmy > this.cmyLim)
		{
			this.cmtoY = this.cmyLim;
		}
		if (this.cmy != this.cmtoY)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
	}

	// Token: 0x04000F4D RID: 3917
	public static BoardListOnScr me;

	// Token: 0x04000F4E RID: 3918
	public static sbyte STYLE_2PLAYER = 0;

	// Token: 0x04000F4F RID: 3919
	public static sbyte STYLE_4PLAYER = 1;

	// Token: 0x04000F50 RID: 3920
	public static sbyte STYLE_5PLAYER = 2;

	// Token: 0x04000F51 RID: 3921
	public static sbyte type = BoardListOnScr.STYLE_4PLAYER;

	// Token: 0x04000F52 RID: 3922
	public static FrameImage imgBoard;

	// Token: 0x04000F53 RID: 3923
	public static Image imgSoBan;

	// Token: 0x04000F54 RID: 3924
	public static Image imgLock;

	// Token: 0x04000F55 RID: 3925
	public Image imgTitleBoard;

	// Token: 0x04000F56 RID: 3926
	public Image imgNumPlayer;

	// Token: 0x04000F57 RID: 3927
	public Image imgPlaying;

	// Token: 0x04000F58 RID: 3928
	private int nBoardPerLine;

	// Token: 0x04000F59 RID: 3929
	private int defX;

	// Token: 0x04000F5A RID: 3930
	public MyVector boardList = new MyVector();

	// Token: 0x04000F5B RID: 3931
	public int xStart;

	// Token: 0x04000F5C RID: 3932
	public int yStart;

	// Token: 0x04000F5D RID: 3933
	public int countPaint;

	// Token: 0x04000F5E RID: 3934
	public sbyte roomID;

	// Token: 0x04000F5F RID: 3935
	private short wSmall;

	// Token: 0x04000F60 RID: 3936
	private Command cmdSellect;

	// Token: 0x04000F61 RID: 3937
	private Command cmdMenu;

	// Token: 0x04000F62 RID: 3938
	private Command cmdClose;

	// Token: 0x04000F63 RID: 3939
	public int cmtoY;

	// Token: 0x04000F64 RID: 3940
	public int cmy;

	// Token: 0x04000F65 RID: 3941
	public int cmdy;

	// Token: 0x04000F66 RID: 3942
	public int cmvy;

	// Token: 0x04000F67 RID: 3943
	public int cmyLim;

	// Token: 0x04000F68 RID: 3944
	public static Image imgSelectBoard;

	// Token: 0x04000F69 RID: 3945
	private int boardIDToGo;

	// Token: 0x04000F6A RID: 3946
	private int dis;

	// Token: 0x04000F6B RID: 3947
	private long timeDelay;

	// Token: 0x04000F6C RID: 3948
	private int pa;

	// Token: 0x04000F6D RID: 3949
	private int vY;

	// Token: 0x04000F6E RID: 3950
	private int dyTran;

	// Token: 0x04000F6F RID: 3951
	private int timePoint;

	// Token: 0x04000F70 RID: 3952
	private int count;

	// Token: 0x04000F71 RID: 3953
	private bool transY;

	// Token: 0x04000F72 RID: 3954
	private bool isGO;

	// Token: 0x04000F73 RID: 3955
	private int timeOpen;

	// Token: 0x04000F74 RID: 3956
	private int pyLast;

	// Token: 0x04000F75 RID: 3957
	private bool trans;

	// Token: 0x04000F76 RID: 3958
	private bool isG;

	// Token: 0x020001B9 RID: 441
	private class IActionBL1 : IAction
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x000794CA File Offset: 0x000778CA
		public void perform()
		{
			Canvas.startWaitDlg();
			CasinoService.gI().joinAnyBoard();
		}
	}

	// Token: 0x020001BA RID: 442
	private class IActionBL3 : IAction
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x000794DB File Offset: 0x000778DB
		public IActionBL3(BoardListOnScr p)
		{
			this.p = p;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000794EA File Offset: 0x000778EA
		public void perform()
		{
			this.p.doAskForBoardToGo();
		}

		// Token: 0x04000F77 RID: 3959
		private BoardListOnScr p;
	}

	// Token: 0x020001BB RID: 443
	private class IActionBL4 : IAction
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x000794FF File Offset: 0x000778FF
		public void perform()
		{
			Canvas.startWaitDlg();
			GlobalService.gI().requestInfoOf(GameMidlet.avatar.IDDB);
		}
	}

	// Token: 0x020001BC RID: 444
	private class IActionToGo : IKbAction
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x0007951A File Offset: 0x0007791A
		public IActionToGo(BoardListOnScr b)
		{
			this.bscr = b;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00079529 File Offset: 0x00077929
		public void perform(string text)
		{
			BoardListOnScr.me.boardIDToGo = int.Parse(text);
			BoardListOnScr.me.doAskForPass();
		}

		// Token: 0x04000F78 RID: 3960
		private BoardListOnScr bscr;
	}

	// Token: 0x020001BD RID: 445
	private class IActionPass : IKbAction
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x0007954D File Offset: 0x0007794D
		public void perform(string text)
		{
			CasinoService.gI().joinBoard(BoardListOnScr.me.roomID, (sbyte)BoardListOnScr.me.boardIDToGo, text);
			Canvas.endDlg();
		}
	}

	// Token: 0x020001BE RID: 446
	private class IActionXeng : IAction
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x0007957C File Offset: 0x0007797C
		public void perform()
		{
			TransMoneyDlg.gI().show();
		}
	}

	// Token: 0x020001BF RID: 447
	private class IActionJoinBoard : IKbAction
	{
		// Token: 0x06000C1B RID: 3099 RVA: 0x00079590 File Offset: 0x00077990
		public void perform(string text)
		{
			BoardInfo boardInfo = (BoardInfo)BoardListOnScr.me.boardList.elementAt(BoardListOnScr.me.selected);
			CasinoService.gI().joinBoard(BoardListOnScr.me.roomID, boardInfo.boardID, text);
			Canvas.load = 0;
		}
	}
}
