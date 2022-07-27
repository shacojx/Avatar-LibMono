using System;

// Token: 0x020001C0 RID: 448
public class RoomListOnScr : OnScreen
{
	// Token: 0x06000C1C RID: 3100 RVA: 0x000795E0 File Offset: 0x000779E0
	public RoomListOnScr()
	{
		this.init();
		this.initCmd();
		this.x = 20 * AvMain.hd;
		this.right = new Command(T.menu, 0);
		this.center = new Command(T.playNow, 1);
		base.addCmd(2, 1);
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00079642 File Offset: 0x00077A42
	public static RoomListOnScr gI()
	{
		if (RoomListOnScr.instance == null)
		{
			RoomListOnScr.instance = new RoomListOnScr();
		}
		return RoomListOnScr.instance;
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00079660 File Offset: 0x00077A60
	public override void switchToMe()
	{
		base.switchToMe();
		this.selected = 1;
		BoardListOnScr.gI().boardList.removeAllElements();
		if (AvMain.hd > 0)
		{
			this.isHide = true;
		}
		this.init();
		Canvas.paint.clearImgAvatar();
		Canvas.loadMap.resetImg();
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x000796B8 File Offset: 0x00077AB8
	public override void commandActionPointer(int index, int subIndex)
	{
		if (index != 1)
		{
			if (index != 2)
			{
				if (index == 3)
				{
					Canvas.startWaitDlg();
					GlobalService.gI().requestInfoOf(GameMidlet.avatar.IDDB);
				}
			}
			else
			{
				Canvas.startWaitDlg();
				CasinoService.gI().requestRoomList();
			}
		}
		else
		{
			Canvas.startWaitDlg();
			CasinoService.gI().joinAnyBoard();
		}
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x00079728 File Offset: 0x00077B28
	public override void commandTab(int index)
	{
		if (index != 0)
		{
			if (index != 1)
			{
				if (index == 2)
				{
					GlobalService.gI().getHandler(9);
					Canvas.startWaitDlg();
				}
			}
			else
			{
				this.doSelectRoom();
			}
		}
		else
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(T.playNow, 1, this));
			myVector.addElement(new Command(T.updateList, 2, this));
			if (AvMain.hd == 0)
			{
				myVector.addElement(MapScr.gI().cmdEvent);
			}
			myVector.addElement(new Command(T.viewMyInfo, 3, this));
			MenuCenter.gI().startAt(myVector);
		}
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x000797D8 File Offset: 0x00077BD8
	public void initCmd()
	{
		this.cmdMenu = new Command(T.menu, 0);
		this.cmdSellect = new Command(T.selectt, 1);
		this.cmdClose = new Command(T.close, 2);
		this.cmdClose.indexImage = 1;
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x00079824 File Offset: 0x00077C24
	public static void setName(int index, BoardScr board)
	{
		if (!onMainMenu.isOngame)
		{
			RoomListOnScr.title = T.nameCasino[index];
		}
		else
		{
			RoomListOnScr.title = T.nameMenuOn[index];
		}
		CasinoMsgHandler.curScr = board;
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x00079853 File Offset: 0x00077C53
	public override void closeTabAll()
	{
		this.commandTab(2);
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x0007985C File Offset: 0x00077C5C
	public override void doMenuTab()
	{
		if (this.left != null)
		{
			this.left.perform();
		}
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x00079874 File Offset: 0x00077C74
	public void init()
	{
		this.hSmall = MyScreen.hText;
		this.dis = Canvas.hCan - Canvas.hTab / 3 * 2;
		if (this.roomList == null)
		{
			return;
		}
		if (this.hSmall != 0)
		{
			int num = this.roomList.size();
			this.cmyLim = (num + 50 * AvMain.hd / this.hSmall + 1) * this.hSmall - this.dis;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.y = 50 * AvMain.hd;
		}
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x0007990C File Offset: 0x00077D0C
	public override void initTabTrans()
	{
		this.init();
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x00079914 File Offset: 0x00077D14
	protected void doSelectRoom()
	{
		sbyte id = ((RoomInfo)this.roomList.elementAt(this.selected)).id;
		if ((int)id == -1)
		{
			return;
		}
		CasinoService.gI().requestBoardList(id);
		Canvas.load = 0;
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x00079957 File Offset: 0x00077D57
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		base.paint(g);
		Canvas.paintPlus2(g);
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0007996D File Offset: 0x00077D6D
	public override void paintMain(MyGraphics g)
	{
		Canvas.paint.paintDefaultBg(g);
		onMainMenu.paintTitle(g, T.room + RoomListOnScr.title, Canvas.w / 2, 30 * AvMain.hd - this.cmy);
		this.paintRoomList(g);
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x000799AC File Offset: 0x00077DAC
	private void paintRoomList(MyGraphics g)
	{
		Canvas.resetTrans(g);
		if (this.roomList == null || this.roomList.size() == 0 || Canvas.load != -1)
		{
			return;
		}
		g.translate(0f, (float)this.y);
		g.translate(0f, (float)(-(float)this.cmy));
		int num = 4;
		int num2 = (this.hSmall - (int)AvMain.hBorder) / 2;
		int num3 = this.cmy / this.hSmall - 1;
		if (num3 < 0)
		{
			num3 = 0;
		}
		int num4 = num3 + Canvas.hCan / this.hSmall + 2;
		if (num4 > this.roomList.size())
		{
			num4 = this.roomList.size();
		}
		num += num3 * this.hSmall;
		int num5 = num3;
		while (num5 < num4 && num5 < this.roomList.size())
		{
			RoomInfo roomInfo = (RoomInfo)this.roomList.elementAt(num5);
			if (!this.isHide && num5 == this.selected && (int)roomInfo.id != -1)
			{
				Canvas.paint.paintSelect(g, this.x + 50 * AvMain.hd + 1, num + 1, Canvas.w - this.x * 2 - 50 * AvMain.hd, this.hSmall - 2);
			}
			if ((int)roomInfo.id == -1)
			{
				g.drawImage(this.imgTitleRoom, (float)this.x, (float)(num + this.hSmall / 2 - 12 * AvMain.hd), 0);
				Canvas.menuFont.drawString(g, T.roomLevelText[(int)roomInfo.lv], this.x + 30 * AvMain.hd, num + this.hSmall / 2 - Canvas.menuFont.getHeight() / 2, 0);
				num += this.hSmall;
			}
			else
			{
				g.setColor(15196756);
				g.drawRect((float)this.x, (float)num, (float)(Canvas.w - this.x * 2), (float)this.hSmall);
				g.fillRect((float)(this.x + 50 * AvMain.hd + 1), (float)num, 1f, (float)this.hSmall);
				g.setColor(7607603);
				g.fillRect((float)(this.x + 1), (float)(num + 1), (float)(50 * AvMain.hd), (float)(this.hSmall - 1));
				Canvas.normalWhiteFont.drawString(g, roomInfo.id + string.Empty, this.x + 25 * AvMain.hd, num + this.hSmall / 2 - Canvas.normalWhiteFont.getHeight() / 2, 2);
				RoomListOnScr.imgRoomStat.drawFrame((int)roomInfo.roomFree, this.x + (Canvas.w - this.x * 2 - RoomListOnScr.imgRoomStat.frameWidth / 2) - (this.hSmall - RoomListOnScr.imgRoomStat.frameHeight) / 2, num + this.hSmall / 2, 0, 3, g);
				num += this.hSmall;
			}
			num5++;
		}
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00079CC0 File Offset: 0x000780C0
	public void setRoomList(MyVector roomList)
	{
		for (int i = 0; i < roomList.size(); i++)
		{
			RoomInfo roomInfo = (RoomInfo)roomList.elementAt(i);
			for (int j = i; j < roomList.size(); j++)
			{
				RoomInfo roomInfo2 = (RoomInfo)roomList.elementAt(j);
				if ((int)roomInfo2.lv < (int)roomInfo.lv)
				{
					roomList.setElementAt(roomInfo, j);
					roomList.setElementAt(roomInfo2, i);
					roomInfo = roomInfo2;
				}
			}
		}
		this.roomList = new MyVector();
		int num = -1;
		for (int k = 0; k < roomList.size(); k++)
		{
			RoomInfo roomInfo3 = (RoomInfo)roomList.elementAt(k);
			if (num == -1 || (int)roomInfo3.lv != num)
			{
				this.roomList.addElement(new RoomInfo(-1, 0, 0, roomInfo3.lv));
			}
			this.roomList.addElement(roomInfo3);
			num = (int)roomInfo3.lv;
		}
		this.selected = 1;
		this.init();
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00079DC8 File Offset: 0x000781C8
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
				if (global::Math.abs(num) < 10 * AvMain.hd)
				{
					int num3 = (Canvas.py - 50 * AvMain.hd + this.cmtoY) / this.hSmall;
					if (num3 >= 0 && num3 < this.roomList.size())
					{
						this.selected = num3;
					}
				}
				if (CRes.abs(Canvas.dy()) >= 10 * AvMain.hd)
				{
					this.isHide = true;
				}
				else if (num2 > 3L && num2 < 8L)
				{
					int num4 = (this.cmtoY + Canvas.py - 50 * AvMain.hd) / this.hSmall;
					if (num4 >= 0 && num4 < this.roomList.size() && !this.isG)
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
				int num5 = this.count - this.timePoint;
				int num6 = this.dyTran - Canvas.py;
				if (CRes.abs(num6) > 40 && num5 < 10 && this.cmtoY > 0 && this.cmtoY < this.cmyLim)
				{
					this.vY = num6 / num5 * 10;
				}
				this.timePoint = -1;
				if (global::Math.abs(num) < 10 * AvMain.hd)
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

	// Token: 0x06000C2D RID: 3117 RVA: 0x0007A0C4 File Offset: 0x000784C4
	private void click()
	{
		this.doSelectRoom();
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x0007A0CC File Offset: 0x000784CC
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
		this.moveCamera();
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x0007A100 File Offset: 0x00078500
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

	// Token: 0x04000F79 RID: 3961
	public static RoomListOnScr instance;

	// Token: 0x04000F7A RID: 3962
	public Image imgTitleRoom;

	// Token: 0x04000F7B RID: 3963
	public static FrameImage imgRoomStat;

	// Token: 0x04000F7C RID: 3964
	public MyVector roomList = new MyVector();

	// Token: 0x04000F7D RID: 3965
	public Image imgBG;

	// Token: 0x04000F7E RID: 3966
	public static string title;

	// Token: 0x04000F7F RID: 3967
	public int dis;

	// Token: 0x04000F80 RID: 3968
	public new int selected;

	// Token: 0x04000F81 RID: 3969
	public new int hSmall;

	// Token: 0x04000F82 RID: 3970
	private Command cmdSellect;

	// Token: 0x04000F83 RID: 3971
	private Command cmdMenu;

	// Token: 0x04000F84 RID: 3972
	private Command cmdClose;

	// Token: 0x04000F85 RID: 3973
	public int cmtoY;

	// Token: 0x04000F86 RID: 3974
	public int cmy;

	// Token: 0x04000F87 RID: 3975
	public int cmdy;

	// Token: 0x04000F88 RID: 3976
	public int cmvy;

	// Token: 0x04000F89 RID: 3977
	public int cmyLim;

	// Token: 0x04000F8A RID: 3978
	public int numW;

	// Token: 0x04000F8B RID: 3979
	public int numH;

	// Token: 0x04000F8C RID: 3980
	public int y;

	// Token: 0x04000F8D RID: 3981
	public int x;

	// Token: 0x04000F8E RID: 3982
	public Image imgTab;

	// Token: 0x04000F8F RID: 3983
	public byte countClose;

	// Token: 0x04000F90 RID: 3984
	public static int index;

	// Token: 0x04000F91 RID: 3985
	public static int indexTemp;

	// Token: 0x04000F92 RID: 3986
	private int count;

	// Token: 0x04000F93 RID: 3987
	private long timeDelay;

	// Token: 0x04000F94 RID: 3988
	private int pa;

	// Token: 0x04000F95 RID: 3989
	private int vY;

	// Token: 0x04000F96 RID: 3990
	private int dyTran;

	// Token: 0x04000F97 RID: 3991
	private int timePoint;

	// Token: 0x04000F98 RID: 3992
	private bool transY;

	// Token: 0x04000F99 RID: 3993
	private bool isGO;

	// Token: 0x04000F9A RID: 3994
	private int timeOpen;

	// Token: 0x04000F9B RID: 3995
	private int pyLast;

	// Token: 0x04000F9C RID: 3996
	private bool trans;

	// Token: 0x04000F9D RID: 3997
	private bool isG;
}
