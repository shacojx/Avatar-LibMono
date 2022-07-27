using System;

// Token: 0x02000171 RID: 369
public class MoneyScr : MyScreen
{
	// Token: 0x060009B7 RID: 2487 RVA: 0x0005F87C File Offset: 0x0005DC7C
	public MoneyScr()
	{
		this.cmdTrans = new Command(T.loadMoney, 0);
		this.cmdLoad = new Command(T.selectt, 0);
		this.cmdClose = new Command(T.close, 1);
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0005F954 File Offset: 0x0005DD54
	public static MoneyScr gI()
	{
		if (MoneyScr.instance == null)
		{
			MoneyScr.instance = new MoneyScr();
		}
		return MoneyScr.instance;
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0005F970 File Offset: 0x0005DD70
	public void switchToMe(MyScreen scr)
	{
		this.init();
		this.focusTap = 0;
		this.selected = 0;
		this.backScr = scr;
		this.isLoadCard = false;
		this.isHide = true;
		if (onMainMenu.isOngame)
		{
			this.right = this.cmdClose;
		}
		else
		{
			this.left = (this.right = (this.center = null));
		}
		base.switchToMe();
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x0005F9E0 File Offset: 0x0005DDE0
	public void init()
	{
		if (this.imgSell == null)
		{
			this.imgSell = Image.createImagePNG(T.getPath() + "/farm/coin");
		}
		string na = string.Empty;
		string[] array = new string[2];
		if (LoadMap.TYPEMAP == 25)
		{
			this.type = 1;
			this.max = 2;
			na = T.strName[1];
			array[0] = T.strName[1];
			array[1] = T.strName[2];
			FarmService.gI().doTransMoney(0, 0);
			Canvas.startWaitDlg();
		}
		else
		{
			na = T.strName[0];
			this.type = 0;
			array[0] = T.strName[0];
			array[1] = T.strName[2];
		}
		PaintPopup.gI().setInfo(na, this.w, this.h, 2, this.countClose, array, null);
		if (onMainMenu.isOngame)
		{
			PaintPopup.gI().y = 25 + MyScreen.ITEM_HEIGHT + 1;
		}
		this.y = PaintPopup.gI().y;
		this.initPos();
		this.setCamera();
		this.initSize();
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0005FAF0 File Offset: 0x0005DEF0
	public void initSize()
	{
		if (this.tfSeri != null)
		{
			this.tfSeri.x = Canvas.hw - 70 + 20;
			this.tfSeri.y = Canvas.hh - 60 + 40;
			this.tfPassCard.x = Canvas.hw - 70 + 20;
			this.tfPassCard.y = Canvas.hh - 60 + 85;
		}
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0005FB60 File Offset: 0x0005DF60
	public override void setSelected(int se, bool isAc)
	{
		if (isAc && this.selected == se && PaintPopup.gI().focusTab == 0)
		{
			this.doCenter();
		}
		base.setSelected(se, isAc);
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0005FB94 File Offset: 0x0005DF94
	public override void commandTab(int index)
	{
		if (index != 0)
		{
			if (index == 1)
			{
				Canvas.cameraList.close();
				this.backScr.switchToMe();
				this.imgSell = null;
			}
		}
		else if (this.type == 0)
		{
			this.doBuy();
		}
		else
		{
			this.doInputMoney();
		}
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0005FBF5 File Offset: 0x0005DFF5
	public override void closeTabAll()
	{
		Canvas.cameraList.close();
		this.backScr.switchToMe();
		this.imgSell = null;
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0005FC13 File Offset: 0x0005E013
	protected void doInputMoney()
	{
		ipKeyboard.openKeyBoard(T.number, ipKeyboard.NUMBERIC, string.Empty, new MoneyScr.IActionInputMoney(this), false);
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0005FC30 File Offset: 0x0005E030
	protected void doBuy()
	{
		MoneyInfo moneyInfo = (MoneyInfo)this.avs.elementAt(this.selected);
		if (moneyInfo.smsContent.IndexOf(T.link) != -1)
		{
			string link = Canvas.normalFont.replace(moneyInfo.smsContent, T.replaceNam, GameMidlet.avatar.name);
			Canvas.startOKDlg(T.doYouWantExitIntoRegion, new MoneyScr.IActionDoBuy1(link));
			return;
		}
		if (moneyInfo.smsContent.IndexOf("napthe:") != -1)
		{
			string searchStr = moneyInfo.smsContent.Substring(0, moneyInfo.smsContent.IndexOf("napthe:") + "napthe:".Length);
			string link2 = Canvas.normalFont.replace(moneyInfo.smsContent, searchStr, string.Empty);
			this.doLoadCard(link2, moneyInfo.info);
			return;
		}
		if (moneyInfo.smsContent.IndexOf("ServerNap:") != -1)
		{
			string searchStr2 = moneyInfo.smsContent.Substring(0, moneyInfo.smsContent.IndexOf("ServerNap:") + "ServerNap:".Length);
			string link3 = Canvas.normalFont.replace(moneyInfo.smsContent, searchStr2, string.Empty);
			AvatarService.gI().doSMSServerLoad(link3);
			Canvas.startWaitDlg();
			return;
		}
		if (moneyInfo.smsTo == "appstore")
		{
			if (moneyInfo.smsContent == "099X")
			{
				iOSPlugins.purchaseItem("com.TeaM.Avatar." + this.itemID[0][0], GameMidlet.avatar.name, GameMidlet.gameID);
			}
			if (moneyInfo.smsContent == "299X")
			{
				iOSPlugins.purchaseItem("com.TeaM.Avatar." + this.itemID[0][1], GameMidlet.avatar.name, GameMidlet.gameID);
			}
			if (moneyInfo.smsContent == "499X")
			{
				iOSPlugins.purchaseItem("com.TeaM.Avatar." + this.itemID[0][2], GameMidlet.avatar.name, GameMidlet.gameID);
			}
			if (moneyInfo.smsContent == "099L")
			{
				iOSPlugins.purchaseItem("com.TeaM.Avatar." + this.itemID[0][3], GameMidlet.avatar.name, GameMidlet.gameID);
			}
			if (moneyInfo.smsContent == "299L")
			{
				iOSPlugins.purchaseItem("com.TeaM.Avatar." + this.itemID[0][4], GameMidlet.avatar.name, GameMidlet.gameID);
			}
			if (moneyInfo.smsContent == "499L")
			{
				iOSPlugins.purchaseItem("com.TeaM.Avatar." + this.itemID[0][5], GameMidlet.avatar.name, GameMidlet.gameID);
			}
			return;
		}
		Canvas.startWaitDlg();
		GlobalService.gI().doRequestMoneyLoad(moneyInfo.strID);
		this.cmdClose.perform();
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0005FF0C File Offset: 0x0005E30C
	private void doLoadCard(string link, string info)
	{
		TField[] array = new TField[2];
		array[0] = new TField(string.Empty, Canvas.currentMyScreen, new MoneyScr.IActionLoad(link, array, MoneyScr.instance));
		array[1] = new TField(string.Empty, Canvas.currentMyScreen, new MoneyScr.IActionLoad(link, array, MoneyScr.instance));
		array[0].setIputType(ipKeyboard.TEXT);
		array[1].setIputType(ipKeyboard.TEXT);
		InputFace.gI().setInfo(array, info, T.loadCard, new Command(T.finish, new MoneyScr.IActionLoad(link, array, MoneyScr.instance)), Canvas.hCan);
		InputFace.gI().iAcClose = new MoneyScr.IActionClose(MoneyScr.instance);
		InputFace.gI().show();
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0005FFC0 File Offset: 0x0005E3C0
	public void doCenter()
	{
		if (this.type == 0)
		{
			this.doBuy();
		}
		else
		{
			this.doInputMoney();
		}
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0005FFDE File Offset: 0x0005E3DE
	public void initCanvas()
	{
		this.initPos();
		this.init();
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0005FFEC File Offset: 0x0005E3EC
	protected void sendLoadCard(string link, string seri, string pass)
	{
		if (seri.Equals(string.Empty))
		{
			Canvas.startOKDlg(T.enterCard[0]);
			return;
		}
		if (pass.Equals(string.Empty))
		{
			Canvas.startOKDlg(T.enterCard[1]);
			return;
		}
		GlobalService.gI().doLoadCard(link, seri, pass);
		this.doCenter();
		Canvas.startWaitDlg();
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0006004B File Offset: 0x0005E44B
	protected void doCloseLoadCard()
	{
		this.isLoadCard = false;
		this.tfSeri = null;
		this.tfPassCard = null;
		this.init();
		this.right = this.cmdClose;
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x00060074 File Offset: 0x0005E474
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		if (onMainMenu.isOngame)
		{
			Canvas.paint.paintDefaultBg(g);
			Canvas.paint.paintDefaultScrList(g, T.loadMoney.ToUpper(), GameMidlet.avatar.money[0] + T.xu, GameMidlet.avatar.money[2] + T.gold);
		}
		else if (this.backScr != null)
		{
			this.backScr.paintMain(g);
		}
		if (InputFace.me != null && Canvas.currentFace == InputFace.me)
		{
			return;
		}
		if (!onMainMenu.isOngame)
		{
			PaintPopup.gI().paint(g);
			g.translate(0f, (float)(this.y + (int)PaintPopup.hTab + AvMain.hDuBox));
			g.setClip((float)(this.x + 5), 0f, (float)(this.w - 10), (float)(PaintPopup.gI().h - (int)PaintPopup.hTab - 2 * AvMain.hDuBox));
		}
		else
		{
			g.translate(0f, (float)this.y);
			g.setClip((float)(this.x + 5), 0f, (float)(this.w - 10), (float)this.h);
		}
		if (this.focusTap == 1)
		{
			int num = (this.h - (int)PaintPopup.hTab + AvMain.hDuBox * 2) / 6;
			Canvas.tempFont.drawString(g, T.nameStr + GameMidlet.avatar.name, this.x + this.w / 2, num / 2, 2);
			Canvas.paint.paintMoney(g, this.x + this.w / 2 - (60 + Canvas.tempFont.getWidth(GameMidlet.avatar.money[0] + string.Empty + 5)), num / 2 + num);
			if (GameMidlet.avatar.money[1] != -1)
			{
				Canvas.tempFont.drawString(g, MapScr.strTkFarm(), this.x + this.w / 2, num / 2 + num * 2, 2);
			}
		}
		else
		{
			g.translate(0f, -CameraList.cmy);
			if (this.type == 0)
			{
				this.paintRichList(g);
			}
			else
			{
				this.paintTransMoney(g);
			}
		}
		base.paint(g);
		Canvas.paintPlus(g);
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x000602E4 File Offset: 0x0005E6E4
	private void paintLoadCard(MyGraphics g)
	{
		Canvas.resetTrans(g);
		g.translate((float)this.x, (float)this.y);
		Canvas.resetTrans(g);
		Canvas.normalFont.drawString(g, T.enterCard[2], this.tfSeri.x - 10 * AvMain.hd, this.tfSeri.y - Canvas.normalFont.getHeight() - 2, 0);
		Canvas.normalFont.drawString(g, T.enterCard[3], this.tfPassCard.x - 10 * AvMain.hd, this.tfPassCard.y - Canvas.normalFont.getHeight() - 2, 0);
		this.tfSeri.paint(g);
		this.tfPassCard.paint(g);
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x000603A8 File Offset: 0x0005E7A8
	public void setAvatarList(MyVector avatarList)
	{
		this.initPos();
		this.avs = new MyVector();
		for (int i = 0; i < avatarList.size(); i++)
		{
			MoneyInfo moneyInfo = (MoneyInfo)avatarList.elementAt(i);
			if (moneyInfo.smsContent.IndexOf(T.link) != -1 || moneyInfo.smsContent.IndexOf("napthe:") != -1 || moneyInfo.smsContent.IndexOf("ServerNap:") != -1)
			{
				this.avs.addElement(moneyInfo);
			}
		}
		this.setCamera();
		this.xTrans = 0;
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x00060448 File Offset: 0x0005E848
	private void setCamera()
	{
		if (this.avs == null)
		{
			return;
		}
		int num = this.avs.size();
		int num2 = this.avs.size() * this.hSmall;
		int size = this.avs.size();
		if (LoadMap.TYPEMAP == 25)
		{
			num2 = this.hSmall * 2;
			size = 2;
		}
		Canvas.cameraList.setInfo(this.x, this.y + (onMainMenu.isOngame ? 0 : ((int)PaintPopup.hTab + AvMain.hDuBox)), this.w, this.hSmall, this.w, num2, this.w, this.h - ((int)PaintPopup.hTab + 2 * AvMain.hDuBox) - AvMain.hDuBox, size);
		this.max = num;
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x00060514 File Offset: 0x0005E914
	private void initPos()
	{
		if (onMainMenu.isOngame)
		{
			this.w = Canvas.w + 8;
			this.h = Canvas.h - 25 - MyScreen.ITEM_HEIGHT + AvMain.hDuBox * 2;
		}
		else
		{
			this.w = LoginScr.gI().wLogin;
			this.h = 8 * MyScreen.hText;
			if (this.h > Canvas.h - MyScreen.hText)
			{
				this.h = Canvas.h - MyScreen.hText;
			}
		}
		this.hSmall = MyScreen.hText;
		this.x = Canvas.hw - this.w / 2;
		this.setCamera();
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x000605C4 File Offset: 0x0005E9C4
	private void paintTransMoney(MyGraphics g)
	{
		for (int i = 0; i < 2; i++)
		{
			if (!this.isHide && i == this.selected)
			{
				Canvas.paint.paintSelected_2(g, this.x + 3 * AvMain.hd, i * this.hSmall + 10, this.w - 6 * AvMain.hd, this.hSmall);
			}
			Canvas.normalFont.drawString(g, T.strTransMoney[i], this.x + 10 * AvMain.hd + ((this.selected != i) ? 0 : this.xTrans), i * this.hSmall + 10 + this.hSmall / 2 - Canvas.normalFont.getHeight() / 2, 0);
		}
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x00060690 File Offset: 0x0005EA90
	private void paintRichList(MyGraphics g)
	{
		int num = this.imgSell.getWidth() + 14;
		int num2 = this.avs.size();
		for (int i = 0; i < num2; i++)
		{
			if (i == this.selected && !this.isHide)
			{
				if (onMainMenu.isOngame)
				{
					g.setColor(14328855);
					g.fillRect((float)this.x, (float)(i * this.hSmall), (float)(this.w - 3 * AvMain.hd), (float)this.hSmall);
				}
				else
				{
					g.setColor(16777215);
					g.fillRect((float)(this.x + 6), (float)(i * this.hSmall), (float)(this.w - 6 * AvMain.hd), (float)this.hSmall);
				}
			}
			g.drawImage(this.imgSell, (float)(this.x + num / 2 + 4 * AvMain.hd), (float)(i * this.hSmall + this.hSmall / 2), 3);
		}
		for (int j = 0; j < num2; j++)
		{
			MoneyInfo moneyInfo = (MoneyInfo)this.avs.elementAt(j);
			g.setClip((float)(this.x + 4 * AvMain.hd + num - 3), (float)((int)CameraList.cmy), (float)(this.w - num - 2 - 4 * AvMain.hd), (float)(this.h - (onMainMenu.isOngame ? 0 : ((int)PaintPopup.hTab + 2 * AvMain.hDuBox))));
			Canvas.normalFont.drawString(g, moneyInfo.info, this.x + num + 4 * AvMain.hd, j * this.hSmall + this.hSmall / 2 - (int)AvMain.hNormal / 2, 0);
		}
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x00060850 File Offset: 0x0005EC50
	private void setTab(int dir)
	{
		this.focusTap += dir;
		string na = string.Empty;
		if (this.type == 0)
		{
			if (this.focusTap == 0)
			{
				na = T.strName[0];
				this.left = null;
			}
			else
			{
				na = T.strName[2];
			}
		}
		else if (this.focusTap == 0)
		{
			na = T.strName[1];
			this.left = null;
		}
		else
		{
			na = T.strName[2];
		}
		PaintPopup.gI().setNameAndFocus(na, this.focusTap);
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x000608E0 File Offset: 0x0005ECE0
	public override void updateKey()
	{
		base.updateKey();
		if (!onMainMenu.isOngame && !this.isLoadCard)
		{
			int num = PaintPopup.gI().setupdateTab();
			if (num != 0)
			{
				this.setTab(num);
				Canvas.isPointerClick = false;
			}
		}
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x00060928 File Offset: 0x0005ED28
	private void setTap()
	{
		string na = string.Empty;
		if (this.focusTap == 0)
		{
			this.focusTap = 1;
			this.left = null;
			na = T.strName[2];
		}
		else
		{
			if (this.type == 1)
			{
				na = T.strName[1];
			}
			else
			{
				na = T.strName[0];
			}
			this.focusTap = 0;
		}
		PaintPopup.gI().setNameAndFocus(na, this.focusTap);
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0006099C File Offset: 0x0005ED9C
	public override void keyPress(int keyCode)
	{
		if (this.isLoadCard)
		{
			if (this.tfSeri.isFocused())
			{
				this.tfSeri.keyPressed(keyCode);
			}
			else if (this.tfPassCard.isFocused())
			{
				this.tfPassCard.keyPressed(keyCode);
			}
		}
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x000609F4 File Offset: 0x0005EDF4
	public override void update()
	{
		PaintPopup.gI().update();
		if (this.backScr != null)
		{
			this.backScr.update();
		}
		if (this.isLoadCard && PaintPopup.gI().focusTab == 0)
		{
			this.tfSeri.update();
			this.tfPassCard.update();
		}
		int width;
		if (this.type == 0)
		{
			MoneyInfo moneyInfo = (MoneyInfo)this.avs.elementAt(this.selected);
			width = Canvas.normalFont.getWidth(moneyInfo.info);
		}
		else
		{
			width = Canvas.normalFont.getWidth(T.strTransMoney[this.selected]);
		}
		if (width > this.w - 20)
		{
			this.xTrans += this.dir;
			if (this.xTrans <= -(width - (this.w - 30)))
			{
				this.dir = 1;
			}
			if (this.xTrans > 0)
			{
				this.dir = -1;
			}
		}
		else
		{
			this.xTrans = 0;
		}
		if (!this.isLoadCard)
		{
			if (this.focusTap == 0)
			{
				if (LoadMap.TYPEMAP != 25)
				{
					this.center = null;
				}
				else
				{
					this.left = null;
				}
			}
			else
			{
				this.left = null;
				this.center = null;
			}
		}
	}

	// Token: 0x04000C90 RID: 3216
	public static MoneyScr instance;

	// Token: 0x04000C91 RID: 3217
	public string[][] itemID = new string[][]
	{
		new string[]
		{
			"099X",
			"299X",
			"499X",
			"099L",
			"299L",
			"499L"
		},
		new string[]
		{
			"Buy 24000 Coins ($0.99)",
			"Buy 84000 Coins ($2.99)",
			"Buy 150000 Coins ($4.99)",
			"Buy 24 Gold ($0.99)",
			"Buy 84 Gold ($2.99)",
			"Buy 150 Gold ($4.99)"
		}
	};

	// Token: 0x04000C92 RID: 3218
	private MyVector avs;

	// Token: 0x04000C93 RID: 3219
	public int type;

	// Token: 0x04000C94 RID: 3220
	public int max;

	// Token: 0x04000C95 RID: 3221
	public int focusTap;

	// Token: 0x04000C96 RID: 3222
	private MyScreen backScr;

	// Token: 0x04000C97 RID: 3223
	private Command cmdTrans;

	// Token: 0x04000C98 RID: 3224
	private Command cmdLoad;

	// Token: 0x04000C99 RID: 3225
	private Command cmdClose;

	// Token: 0x04000C9A RID: 3226
	private bool isLoadCard;

	// Token: 0x04000C9B RID: 3227
	private TField tfSeri;

	// Token: 0x04000C9C RID: 3228
	private TField tfPassCard;

	// Token: 0x04000C9D RID: 3229
	private Image imgSell;

	// Token: 0x04000C9E RID: 3230
	private int x;

	// Token: 0x04000C9F RID: 3231
	private int y;

	// Token: 0x04000CA0 RID: 3232
	private int w;

	// Token: 0x04000CA1 RID: 3233
	private int h;

	// Token: 0x04000CA2 RID: 3234
	private new int hSmall;

	// Token: 0x04000CA3 RID: 3235
	private sbyte countClose;

	// Token: 0x04000CA4 RID: 3236
	private AvPosition pTrans = new AvPosition(0, 1);

	// Token: 0x04000CA5 RID: 3237
	private int xTrans;

	// Token: 0x04000CA6 RID: 3238
	private int dir = -1;

	// Token: 0x02000172 RID: 370
	private class IActionInputMoney : IKbAction
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x00060B49 File Offset: 0x0005EF49
		public IActionInputMoney(MoneyScr p)
		{
			this.p = p;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00060B58 File Offset: 0x0005EF58
		public void perform(string text)
		{
			try
			{
				if (!text.Equals(string.Empty))
				{
					Canvas.endDlg();
					int money = int.Parse(text);
					FarmService.gI().doTransMoney(money, (this.p.selected != 0) ? 0 : 1);
					Canvas.startWaitDlg();
				}
			}
			catch (Exception e)
			{
				Out.logError(e);
			}
		}

		// Token: 0x04000CA7 RID: 3239
		private readonly MoneyScr p;
	}

	// Token: 0x02000173 RID: 371
	private class IActionDoBuy1 : IAction
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x00060BD0 File Offset: 0x0005EFD0
		public IActionDoBuy1(string link)
		{
			this.link = link;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00060BDF File Offset: 0x0005EFDF
		public void perform()
		{
			GameMidlet.flatForm(this.link);
		}

		// Token: 0x04000CA8 RID: 3240
		private readonly string link;
	}

	// Token: 0x02000174 RID: 372
	private class IActionDoBuy2 : IAction
	{
		// Token: 0x060009D7 RID: 2519 RVA: 0x00060BF4 File Offset: 0x0005EFF4
		public void perform()
		{
			Canvas.startOKDlg(T.sendSmgFinish);
		}
	}

	// Token: 0x02000175 RID: 373
	private class IActionDoBuy3 : IAction
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x00060C00 File Offset: 0x0005F000
		public IActionDoBuy3(MoneyInfo mi)
		{
			this.mi = mi;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00060C10 File Offset: 0x0005F010
		public void perform()
		{
			Canvas.startOKDlg(string.Concat(new string[]
			{
				T.notSendSmg,
				this.mi.smsContent,
				GameMidlet.avatar.name.ToUpper(),
				T.sendTo,
				this.mi.smsTo
			}));
		}

		// Token: 0x04000CA9 RID: 3241
		private readonly MoneyInfo mi;
	}

	// Token: 0x02000176 RID: 374
	private class IActionClose : IAction
	{
		// Token: 0x060009DA RID: 2522 RVA: 0x00060C6B File Offset: 0x0005F06B
		public IActionClose(MoneyScr me)
		{
			this.me = me;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00060C7A File Offset: 0x0005F07A
		public void perform()
		{
			this.me.init();
			InputFace.gI().close();
			TField.close();
		}

		// Token: 0x04000CAA RID: 3242
		private MoneyScr me;
	}

	// Token: 0x02000177 RID: 375
	private class IActionLoad : IAction
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x00060C96 File Offset: 0x0005F096
		public IActionLoad(string link, TField[] tf, MoneyScr me)
		{
			this.link = link;
			this.tf = tf;
			this.me = me;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00060CB3 File Offset: 0x0005F0B3
		public void perform()
		{
			this.me.init();
			this.me.sendLoadCard(this.link, this.tf[0].getText(), this.tf[1].getText());
		}

		// Token: 0x04000CAB RID: 3243
		private string link;

		// Token: 0x04000CAC RID: 3244
		private TField[] tf;

		// Token: 0x04000CAD RID: 3245
		private MoneyScr me;
	}

	// Token: 0x02000178 RID: 376
	private class IActionDoLoadCard1 : IAction
	{
		// Token: 0x060009DE RID: 2526 RVA: 0x00060CEB File Offset: 0x0005F0EB
		public IActionDoLoadCard1(MoneyScr p)
		{
			this.p = p;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00060CFA File Offset: 0x0005F0FA
		public void perform()
		{
			this.p.doCloseLoadCard();
		}

		// Token: 0x04000CAE RID: 3246
		private readonly MoneyScr p;
	}
}
