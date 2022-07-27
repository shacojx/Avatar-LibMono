using System;
using UnityEngine;

// Token: 0x0200011F RID: 287
public class LoginScr : MyScreen
{
	// Token: 0x06000807 RID: 2055 RVA: 0x0004E0F4 File Offset: 0x0004C4F4
	public LoginScr()
	{
		this.tfUser = new TField(string.Empty, this, new LoginScr.IActionOkUser());
		this.tfUser.setFocus(true);
		this.tfUser.showSubTextField = false;
		this.tfUser.autoScaleScreen = true;
		this.tfUser.setIputType(ipKeyboard.TEXT);
		this.tfUser.setMaxTextLenght(20);
		this.tfPass = new TField(string.Empty, this, new LoginScr.IActionOkUser());
		this.tfReg = new TField(string.Empty, this, new LoginScr.IActionOkUser());
		this.tfEmail = new TField(string.Empty, this, new LoginScr.IActionOkUser());
		this.tfEmail.sDefaust = "Tùy chọn";
		this.tfEmail.setMaxTextLenght(20);
		this.tfPass.isUser = true;
		this.tfReg.isUser = true;
		this.tfUser.isUser = true;
		this.tfEmail.isUser = true;
		this.tfPass.showSubTextField = false;
		this.tfReg.showSubTextField = false;
		this.tfEmail.showSubTextField = false;
		this.tfPass.autoScaleScreen = true;
		this.tfUser.autoScaleScreen = true;
		this.tfReg.autoScaleScreen = true;
		this.tfEmail.autoScaleScreen = true;
		this.init(Canvas.hCan);
		this.tfPass.setIputType(ipKeyboard.PASS);
		this.tfReg.setIputType(ipKeyboard.PASS);
		this.tfEmail.setIputType(ipKeyboard.TEXT);
		this.focus = 0;
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x0004E2DA File Offset: 0x0004C6DA
	public static LoginScr gI()
	{
		if (LoginScr.me == null)
		{
			LoginScr.me = new LoginScr();
		}
		return LoginScr.me;
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x0004E2F5 File Offset: 0x0004C6F5
	public override void close()
	{
		Canvas.startOKDlg(T.doYouWantExit2, 54);
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x0004E304 File Offset: 0x0004C704
	public override void switchToMe()
	{
		LoginScr.gI().load();
		this.initCmd();
		Canvas.paint.loadImgAvatar();
		LoginScr.isLoadIP = false;
		GameMidlet.avatar = new Avatar();
		this.init(Canvas.hCan);
		base.switchToMe();
		this.indexNewGame = -1;
		LoginScr.isNewGame = true;
		if (this.nameVir.Equals(string.Empty) && this.tfUser.getText().Equals(string.Empty))
		{
			this.listStrNew = new string[]
			{
				"Chơi mới",
				"Đổi tài khoản"
			};
		}
		else
		{
			this.listStrNew = new string[]
			{
				"Chơi tiêp" + (this.tfUser.getText().Equals(string.Empty) ? string.Empty : (", " + this.tfUser.getText())),
				"Chơi mới",
				"Đổi tài khoản"
			};
		}
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x0004E40C File Offset: 0x0004C80C
	public void setTF()
	{
		this.tfPass.setFocus(false);
		this.tfReg.setFocus(false);
		this.tfUser.setFocus(true);
		if (LoginScr.isSelectedLanguage)
		{
		}
		this.tfPass.setFocus(false);
		this.tfReg.setFocus(false);
		this.tfUser.setFocus(true);
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x0004E46C File Offset: 0x0004C86C
	public void load()
	{
		onMainMenu.iChangeGame = 0;
		onMainMenu.isOngame = false;
		LoadMap.idTileImg = -1;
		this.timeOut = Canvas.getTick();
		this.resetLogo();
		this.loadLogin();
		if (LoadMap.TYPEMAP != 25)
		{
			Canvas.loadMap.load(26, true);
		}
		GameMidlet.avatar.x = (GameMidlet.avatar.xCur = (int)(LoadMap.wMap * 24 / 2 + 30));
		AvCamera.gI().xCam = (AvCamera.gI().xTo = 100f);
		this.focus = 0;
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x0004E504 File Offset: 0x0004C904
	public void initImg()
	{
		if ((int)GameMidlet.PROVIDER == 6)
		{
			MyScreen.imgLogo = Image.createImagePNG(T.getPath() + "/lgyeah");
		}
		else
		{
			MyScreen.imgLogo = Image.createImagePNG(T.getPath() + "/l");
		}
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x0004E554 File Offset: 0x0004C954
	public void initCmd()
	{
		this.cmdMenu = new Command(T.menu, 0);
		this.cmdRegister = new Command(T.register, 4, this.xLogin + this.wLogin - MyScreen.wTab / 2 - 30 * AvMain.hd, this.yLogin + this.hLogin - AvMain.hCmd / 2);
		this.cmdReg = new Command(T.register, 3, this.xLogin + MyScreen.wTab / 2 + 30 * AvMain.hd, this.yLogin + this.hLogin - AvMain.hCmd / 2);
		this.cmdLogin = new Command(T.selectt, 1, this.xLogin + MyScreen.wTab / 2 + 30 * AvMain.hd, this.yLogin + this.hLogin - AvMain.hCmd / 2);
		this.cmdRemem = new Command(T.remem, 2);
		this.cmdBack = new Command(T.back, 5, this.xLogin + this.wLogin - MyScreen.wTab / 2 - 30 * AvMain.hd, this.yLogin + this.hLogin - AvMain.hCmd / 2);
		this.cmdSelected = new Command(T.selectt, 104);
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x0004E697 File Offset: 0x0004CA97
	public void reset()
	{
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0004E69C File Offset: 0x0004CA9C
	public void init(int bh)
	{
		this.h0 = bh;
		this.defYL = bh / 2 - 80;
		this.resetLogo();
		this.wC = Canvas.w - 30;
		if (this.wC < 70)
		{
			this.wC = 70;
		}
		if (this.wC > 99)
		{
			this.wC = 99;
		}
		this.xC = (Canvas.w - this.wC >> 1) + 29;
		if (Canvas.w <= 128)
		{
			this.wC = 80;
			this.xC = (Canvas.w - this.wC >> 1) + 20;
		}
		this.xC -= (AvMain.hd - 1) * 40;
		Canvas.paint.loadImgAvatar();
		Canvas.paint.initPosLogin(this, bh);
		this.initCmd();
		this.defYL = this.yLogin / 2;
		this.yL = this.defYL;
		AvCamera.gI().followPlayer = GameMidlet.avatar;
		AvCamera.gI().update();
		if (TouchScreenKeyboard.visible)
		{
			this.defYL = this.yLogin - 100;
		}
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x0004E7C0 File Offset: 0x0004CBC0
	public override void commandActionPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 2:
			Canvas.startOKDlg(T.doYouWantExit2, 54);
			break;
		case 3:
			Canvas.startOK(T.uNeedExitGame, 55);
			break;
		case 4:
			ipKeyboard.openKeyBoard(T.nameAcc, ipKeyboard.TEXT, string.Empty, new LoginScr.actDoSettingPassword(this), false);
			break;
		case 5:
			OptionScr.gI().switchToMe();
			break;
		case 6:
			GameMidlet.flatForm("http://wap.teamobi.com/faqs.php?provider=" + GameMidlet.PROVIDER);
			break;
		case 7:
			GameMidlet.flatForm(string.Concat(new object[]
			{
				"http://wap.teamobi.com?info=checkupdate&game=8&version=2.5.8&provider=",
				GameMidlet.PROVIDER,
				"&agent=",
				GameMidlet.AGENT
			}));
			break;
		default:
			if (index != 50)
			{
			}
			break;
		case 9:
			Canvas.startOKDlg(T.alreadyDelRMS + T.delRMS);
			AvatarData.delRMS();
			break;
		}
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x0004E8CE File Offset: 0x0004CCCE
	public override void doSetting()
	{
		if (TouchScreenKeyboard.visible)
		{
			Canvas.isPointerRelease = false;
			ipKeyboard.tk = null;
		}
		OptionScr.gI().switchToMe();
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x0004E8F0 File Offset: 0x0004CCF0
	public override void doMenu()
	{
		if (TouchScreenKeyboard.visible)
		{
			Canvas.isPointerRelease = false;
			Canvas.isKeyBoard = false;
			ipKeyboard.tk.active = false;
		}
		MyVector myVector = new MyVector();
		Command command = new Command(T.exit, 2, this);
		myVector.addElement(new Command(T.support, 8, this));
		myVector.addElement(new Command(T.fogetPass, 4, this));
		myVector.addElement(new Command(T.option, 5, this));
		if (OptionScr.gI().mapFocus[4] == 0)
		{
			myVector.addElement(new Command(T.FAQs, 6, this));
		}
		myVector.addElement(new Command(T.updateGame, 7, this));
		if (OptionScr.gI().mapFocus[4] == 0)
		{
			myVector.addElement(new Command(T.delRMS, 9, this));
		}
		MenuCenter.gI().startAt(myVector);
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x0004E9CC File Offset: 0x0004CDCC
	public void doForgetPass(string user)
	{
		IAction action = new LoginScr.IActionForgetPass(user);
		action.perform();
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x0004E9E8 File Offset: 0x0004CDE8
	protected void doRememberPass()
	{
		if (!this.isCheckBox)
		{
			this.isCheckBox = true;
			this.cmdRemem.caption = T.removee;
		}
		else
		{
			this.isCheckBox = false;
			this.cmdRemem.caption = T.remem;
		}
		Out.println("remember: " + this.isCheckBox);
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x0004EA4D File Offset: 0x0004CE4D
	protected void doRegister()
	{
		this.isReg = true;
		Canvas.paint.initPosLogin(this, this.h0);
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x0004EA68 File Offset: 0x0004CE68
	protected void doReg()
	{
		if (this.tfUser.getText().Equals(string.Empty))
		{
			Canvas.startOKDlg(T.nameReg[0]);
			this.tfUser.setFocus(true);
			return;
		}
		if (this.tfPass.getText().Equals(string.Empty))
		{
			Canvas.startOKDlg(T.nameReg[1]);
			this.tfPass.setFocus(true);
			return;
		}
		if (this.tfReg.getText().Equals(string.Empty))
		{
			Canvas.startOKDlg(T.nameReg[2]);
			this.tfReg.setFocus(true);
			return;
		}
		if (!this.tfPass.getText().Equals(this.tfReg.getText()))
		{
			Canvas.startOKDlg(T.nameReg[3]);
			return;
		}
		Canvas.endDlg();
		this.timeOut = Canvas.getTick();
		if (!this.tfEmail.getText().Equals(string.Empty))
		{
			Canvas.startOKDlg("Bạn nên điền chính xác số di động hoặc email. Khi quên mật khẩu, bạn sẽ dùng nó để lấy lại. Bạn có chắc chắn đã điền số di động / email đúng chưa?", 102);
			return;
		}
		this.doSendRegisterInfo();
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x0004EB78 File Offset: 0x0004CF78
	public override void commandTab(int index)
	{
		switch (index)
		{
		case 50:
			Canvas.startOKDlg(T.youUseNumRegGetpass);
			break;
		case 51:
			this.regRequest();
			break;
		case 52:
			break;
		case 53:
			GameMidlet.flatForm("http://teamobi.com/dieukhoan.htm");
			break;
		case 54:
			this.saveLogin();
			break;
		case 55:
			LoginScr.isSelectedLanguage = false;
			this.saveLogin();
			AvatarData.delErrorRms("avatarSV");
			Application.Quit();
			break;
		default:
			switch (index)
			{
			case 1:
				LoginScr.isNewGame = true;
				this.center = (this.right = null);
				this.indexNewGame = 0;
				this.listStrNew = new string[]
				{
					"Chơi tiêp" + (this.tfUser.getText().Equals(string.Empty) ? string.Empty : (", " + this.tfUser.getText())),
					"Chơi mới",
					"Đổi tài khoản"
				};
				break;
			case 2:
				this.doRememberPass();
				break;
			case 3:
				this.doReg();
				break;
			case 4:
				this.doRegister();
				break;
			case 5:
				this.isReg = false;
				this.center = this.cmdLogin;
				Canvas.paint.initPosLogin(this, this.h0);
				break;
			default:
				switch (index)
				{
				case 100:
				{
					string text = Canvas.inputDlg.getText();
					if (text.Equals(string.Empty))
					{
						return;
					}
					this.doForgetPass(text);
					break;
				}
				case 102:
					this.doSendRegisterInfo();
					break;
				case 104:
					this.clickNewGame();
					break;
				case 105:
					this.center = (this.right = null);
					LoginScr.isNewGame = true;
					break;
				}
				break;
			}
			break;
		}
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x0004ED70 File Offset: 0x0004D170
	private void doSelectLQ()
	{
		LoginScr.isSelectedLanguage = true;
		Canvas.paint.initString(this.indexLQ);
		OptionScr.gI().mapFocus[4] = this.indexLQ;
		OptionScr.gI().save(0);
		this.initImg();
		this.resetLogo();
		this.initCmd();
		SplashScr.imgLogo = null;
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x0004EDC8 File Offset: 0x0004D1C8
	public void regRequest()
	{
		Canvas.startWaitDlg();
		Canvas.connect();
		GlobalService.gI().doRegisterByEmail(this.tfUser.getText().ToLower(), this.tfPass.getText().ToLower(), this.tfEmail.getText());
		this.passRemem = this.tfPass.getText();
		this.isReg = false;
		this.center = this.cmdLogin;
		Canvas.paint.initPosLogin(this, this.h0);
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x0004EE49 File Offset: 0x0004D249
	protected void doSendRegisterInfo()
	{
		Canvas.msgdlg.setInfoLCR(T.areYouAgreeRule, new Command(T.agree, 51), new Command(T.no, 52), new Command(T.viewRule, 53));
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x0004EE80 File Offset: 0x0004D280
	protected void doLogin()
	{
		string text = this.tfUser.getText().ToLower().Trim();
		string text2 = this.tfPass.getText();
		if (text.ToLower().Equals("showimei"))
		{
			Canvas.startOKDlg(SystemInfo.deviceUniqueIdentifier + string.Empty);
		}
		if (text.Equals(string.Empty))
		{
			return;
		}
		if (text2.Equals(string.Empty))
		{
			return;
		}
		if (GameMidlet.IP == null)
		{
			ServerListScr.gI().doUpdateServer();
		}
		else
		{
			ServerListScr.gI().switchToMe();
		}
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x0004EF20 File Offset: 0x0004D320
	public override void update()
	{
		if (!LoginScr.isNewGame && this == Canvas.currentMyScreen && Canvas.menuMain == null)
		{
			this.tfUser.update();
			this.tfPass.update();
			if (this.isReg)
			{
				this.tfReg.update();
				this.tfEmail.update();
			}
		}
		this.updateLogo();
		if (this.isReg)
		{
		}
		Canvas.loadMap.update();
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x0004EFA3 File Offset: 0x0004D3A3
	public void updateLogo()
	{
		if (this.defYL != this.yL)
		{
			this.yL += this.defYL - this.yL >> 1;
		}
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x0004EFD2 File Offset: 0x0004D3D2
	public void resetLogo()
	{
		this.yL = -50;
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x0004EFDC File Offset: 0x0004D3DC
	public override void keyPress(int keyCode)
	{
		if (this.tfUser.isFocused())
		{
			this.tfUser.keyPressed(keyCode);
		}
		else if (this.tfPass.isFocused())
		{
			this.tfPass.keyPressed(keyCode);
		}
		else if (this.tfReg.isFocused())
		{
			this.tfReg.keyPressed(keyCode);
		}
		else if (this.tfEmail.isFocused())
		{
			this.tfEmail.keyPressed(keyCode);
		}
		base.keyPress(keyCode);
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x0004F074 File Offset: 0x0004D474
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		Canvas.resetTrans(g);
		if (LoginScr.isNewGame)
		{
			this.paintNewGame(g);
		}
		else if (this == Canvas.currentMyScreen && Canvas.currentDialog == null && Canvas.menuMain == null)
		{
			this.paintLogin(g);
		}
		Canvas.resetTrans(g);
		this.paintLogo(g);
		base.paint(g);
		Canvas.paintPlus(g);
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0004F0E4 File Offset: 0x0004D4E4
	private void paintNewGame(MyGraphics g)
	{
		Canvas.paint.paintPopupBack(g, this.xLogin, this.yLogin, this.wLogin, this.hLogin, -1, false);
		g.translate((float)this.xLogin, (float)(this.yLogin + this.yNew));
		if ((int)this.indexNewGame != -1)
		{
			g.setColor(16777215);
			g.fillRect((float)(5 * AvMain.hd), (float)((int)this.indexNewGame * this.hCellNew), (float)(this.wLogin - 10 * AvMain.hd), (float)this.hCellNew);
		}
		for (int i = 0; i < this.listStrNew.Length; i++)
		{
			Canvas.normalFont.drawString(g, this.listStrNew[i], this.wLogin / 2, i * this.hCellNew + this.hCellNew / 2 - Canvas.normalFont.getHeight() / 2, 2);
		}
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x0004F1D0 File Offset: 0x0004D5D0
	public override void paintMain(MyGraphics g)
	{
		GUIUtility.ScaleAroundPivot(new Vector2(AvMain.zoom, AvMain.zoom), Vector2.zero);
		Canvas.loadMap.paint(g);
		Canvas.loadMap.paintObject(g);
		GUIUtility.ScaleAroundPivot(new Vector2(1f / AvMain.zoom, 1f / AvMain.zoom), Vector2.zero);
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x0004F234 File Offset: 0x0004D634
	private void paintLogin(MyGraphics g)
	{
		Canvas.paint.paintPopupBack(g, this.xLogin, this.yLogin, this.wLogin, this.hLogin, -1, false);
		g.setClip((float)(this.xLogin + 4), (float)(this.yLogin + 4), (float)(this.wLogin - 8), (float)(this.hLogin - 8));
		this.tfUser.paint(g);
		g.setClip((float)(this.xLogin + 4), (float)(this.yLogin + 4), (float)(this.wLogin - 8), (float)(this.hLogin - 8));
		int num = Canvas.tempFont.getWidth(T.acc + ":");
		if (num < this.tfUser.x - this.xLogin)
		{
			num = (this.tfUser.x - this.xLogin - num) / 2;
		}
		else
		{
			num = this.tfUser.x - num - 5;
		}
		Canvas.tempFont.drawString(g, T.acc, this.xLogin + num, this.tfUser.y + this.tfUser.height / 2 - Canvas.tempFont.getHeight() / 2 - 2 * AvMain.hd, 0);
		Canvas.tempFont.drawString(g, T.pass, this.xLogin + num, this.tfPass.y + this.tfUser.height / 2 - Canvas.tempFont.getHeight() / 2 - 2 * AvMain.hd, 0);
		if (!this.isReg)
		{
			Canvas.paint.paintCheckBox(g, this.xCheck, this.yCheck, this.focus, this.isCheckBox);
		}
		else
		{
			Canvas.tempFont.drawString(g, T.enterAgain, this.xLogin + num, this.tfReg.y + this.tfUser.height / 2 - Canvas.tempFont.getHeight() - 2 * AvMain.hd, 0);
			Canvas.tempFont.drawString(g, T.pass, this.xLogin + num, this.tfReg.y + this.tfUser.height / 2 - 2 * AvMain.hd, 0);
			Canvas.tempFont.drawString(g, "Số di động", this.xLogin + num, this.tfEmail.y + this.tfUser.height / 2 - Canvas.tempFont.getHeight() - 2 * AvMain.hd, 0);
			Canvas.tempFont.drawString(g, "hoặc email:", this.xLogin + num, this.tfEmail.y + this.tfUser.height / 2 - 2 * AvMain.hd, 0);
			this.tfReg.paint(g);
			this.tfEmail.paint(g);
		}
		this.tfPass.paint(g);
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x0004F507 File Offset: 0x0004D907
	public void paintLogo(MyGraphics g)
	{
		if (!TouchScreenKeyboard.visible)
		{
			g.drawImage(MyScreen.imgLogo, (float)Canvas.hw, (float)this.yL, 3);
		}
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x0004F52C File Offset: 0x0004D92C
	public override void updateKey()
	{
		if (LoginScr.isNewGame)
		{
			this.updateKeyNewGame();
			return;
		}
		if (Canvas.isPointerClick && Canvas.isPointer(this.xCheck - 10, this.yCheck, Canvas.tempFont.getWidth(T.rememPass) + 70, 35 * AvMain.hd))
		{
			this.isCheck = true;
			Canvas.isPointerClick = false;
		}
		if (this.isCheck && Canvas.isPointerRelease && Canvas.isPoint(this.xCheck - 10, this.yCheck, Canvas.tempFont.getWidth(T.rememPass) + 70, 35 * AvMain.hd))
		{
			Canvas.isPointerRelease = false;
			this.isCheck = false;
			this.doRememberPass();
		}
		if (Canvas.isPointerClick && Screen.orientation != 1 && ipKeyboard.tk != null)
		{
			ipKeyboard.close();
		}
		base.updateKey();
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x0004F61C File Offset: 0x0004DA1C
	private void updateKeyNewGame()
	{
		if (Canvas.isKeyPressed(2))
		{
			this.indexNewGame = (sbyte)((int)this.indexNewGame - 1);
			if ((int)this.indexNewGame < 0)
			{
				this.indexNewGame = (sbyte)(this.listStrNew.Length - 1);
			}
		}
		else if (Canvas.isKeyPressed(8))
		{
			this.indexNewGame = (sbyte)((int)this.indexNewGame + 1);
			if ((int)this.indexNewGame >= this.listStrNew.Length)
			{
				this.indexNewGame = 0;
			}
		}
		if (Canvas.isPointerClick)
		{
			for (int i = 0; i < this.listStrNew.Length; i++)
			{
				if (Canvas.isPoint(this.xLogin, this.yLogin + this.yNew + i * this.hCellNew, this.wLogin, this.hCellNew))
				{
					this.indexNewGame = (sbyte)i;
					Canvas.isPointerClick = false;
					this.isClickNew = true;
					Out.println("aaaaaaaaaaaaaaaa");
					break;
				}
			}
		}
		if (this.isClickNew)
		{
			if (Canvas.isPointerDown && !Canvas.isPoint(this.xLogin, this.yLogin + this.yNew + (int)this.indexNewGame * this.hCellNew, this.wLogin, this.hCellNew))
			{
				this.indexNewGame = -1;
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isClickNew = false;
				if ((int)this.indexNewGame != -1)
				{
					this.clickNewGame();
				}
			}
		}
		base.updateKey();
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x0004F79C File Offset: 0x0004DB9C
	private void clickNewGame()
	{
		Out.println(string.Concat(new object[]
		{
			"clickNewGame: ",
			LoginScr.isAccVir,
			"    ",
			this.indexNewGame
		}));
		sbyte b = this.indexNewGame;
		if (b != 0)
		{
			if (b != 1)
			{
				if (b == 2)
				{
					this.changeAcc();
				}
			}
			else if (this.listStrNew.Length == 2)
			{
				this.changeAcc();
			}
			else
			{
				IAction action = new LoginScr.IAcionNewGameOk();
				if (!this.nameVir.Equals(string.Empty) && this.tfUser.getText().Equals(string.Empty))
				{
					Canvas.startOKDlg("Tài khoản của bạn chưa được đăng kí liên kết với một tài khoản Team. Bạn sẽ mất tài khoản đang chơi nếu tiếp tục. Bạn có muốn tiếp tục ?", action);
				}
				else
				{
					action.perform();
				}
			}
		}
		else if (this.listStrNew.Length == 2)
		{
			IAction action2 = new LoginScr.IAcionNewGameOk();
			action2.perform();
		}
		else if (LoginScr.isAccVir)
		{
			ServerListScr.gI().switchToMe();
		}
		else
		{
			this.doLogin();
		}
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x0004F8BC File Offset: 0x0004DCBC
	private void changeAcc()
	{
		IAction action = new LoginScr.IActionChangeAcc();
		if (!this.nameVir.Equals(string.Empty) && this.tfUser.getText().Equals(string.Empty))
		{
			Canvas.startOKDlg("Tài khoản của bạn chưa được đăng kí liên kết với một tài khoản Team. Bạn sẽ mất tài khoản đang chơi nếu tiếp tục. Bạn có muốn tiếp tục ?", action);
		}
		else
		{
			action.perform();
		}
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x0004F914 File Offset: 0x0004DD14
	public void saveLogin()
	{
		Out.println("SAVE LOGINaaaaaaaaaaaaaaaaaaaaa");
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeUTF("2.5.8");
			dataOutputStream.writeByte((sbyte)this.selected);
			dataOutputStream.writeUTF(this.numSupport);
			dataOutputStream.writeBoolean(this.isCheckBox);
			dataOutputStream.writeUTF(this.nameVir);
			dataOutputStream.writeUTF(this.passVir);
			if (this.isCheckBox)
			{
				dataOutputStream.writeUTF(LoginScr.gI().tfUser.getText());
				dataOutputStream.writeUTF(LoginScr.gI().tfPass.getText());
			}
			dataOutputStream.writeInt(LoginScr.aa);
			dataOutputStream.writeBoolean(LoginScr.isSelectedLanguage);
			dataOutputStream.writeByte(ServerListScr.selected);
			dataOutputStream.writeBoolean(LoginScr.isAccVir);
			RMS.saveRMS("avlogin", dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x0004FA14 File Offset: 0x0004DE14
	public void loadLogin()
	{
		DataInputStream dataInputStream = AvatarData.initLoad("avlogin");
		if (dataInputStream == null)
		{
			LoginScr.isSelectedLanguage = true;
			return;
		}
		Out.println("loadLogin");
		string value = string.Empty;
		try
		{
			value = dataInputStream.readUTF();
			this.selected = (int)dataInputStream.readByte();
			this.numSupport = dataInputStream.readUTF();
			this.isCheckBox = dataInputStream.readBoolean();
			this.nameVir = dataInputStream.readUTF();
			this.passVir = dataInputStream.readUTF();
			if (this.isCheckBox)
			{
				this.tfUser.setText(dataInputStream.readUTF());
				this.tfPass.setText(dataInputStream.readUTF());
			}
			LoginScr.aa = dataInputStream.readInt();
			LoginScr.isSelectedLanguage = dataInputStream.readBoolean();
			ServerListScr.selected = (int)dataInputStream.readByte();
			LoginScr.isAccVir = dataInputStream.readBoolean();
			dataInputStream.close();
		}
		catch (Exception ex)
		{
			AvatarData.delErrorRms("avlogin");
		}
		if (!LoginScr.isSelectedLanguage)
		{
			AvatarData.delErrorRms("avatarSV");
		}
		if (!"2.5.8".Equals(value))
		{
			AvatarData.delRMS();
		}
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x0004FB3C File Offset: 0x0004DF3C
	public void onNumSupport(string numSup)
	{
		this.numSupport = numSup;
		this.saveLogin();
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0004FB4C File Offset: 0x0004DF4C
	public void login()
	{
		Canvas.connect();
		GlobalService.gI().doRequestNumSupport(LoginScr.gI().numSupport.GetHashCode());
		Out.println(string.Concat(new object[]
		{
			"login: ",
			LoginScr.isNewGame,
			"    ",
			this.indexNewGame
		}));
		if (LoginScr.isNewGame && (((int)this.indexNewGame == 0 && this.listStrNew.Length == 2) || ((int)this.indexNewGame == 1 && this.listStrNew.Length == 3)))
		{
			GlobalService.gI().doLoginNewGame();
		}
		else if (this.tfUser.getText().Equals(string.Empty))
		{
			GlobalService.gI().login(this.nameVir, this.passVir, "2.5.8");
		}
		else
		{
			GlobalService.gI().login(this.tfUser.getText().ToLower(), this.tfPass.getText(), "2.5.8");
		}
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x0004FC64 File Offset: 0x0004E064
	public void onRegisterByEmail(string name, string pass)
	{
		this.tfUser.setText(name);
		this.tfPass.setText(pass);
		Canvas.startOKDlg("Đăng ký thành công.");
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0004FC88 File Offset: 0x0004E088
	public void onLoginNewGame(string nameNewGame, string passNewGame)
	{
		Out.println("onLoginNewGame: " + nameNewGame + "   " + passNewGame);
		this.nameVir = nameNewGame;
		this.passVir = passNewGame;
		this.tfUser.setText(string.Empty);
		this.tfPass.setText(string.Empty);
		LoginScr.isAccVir = true;
		LoginScr.isNewGame = false;
		this.login();
	}

	// Token: 0x04000A63 RID: 2659
	public static LoginScr me;

	// Token: 0x04000A64 RID: 2660
	public TField tfUser;

	// Token: 0x04000A65 RID: 2661
	public TField tfPass;

	// Token: 0x04000A66 RID: 2662
	public TField tfReg;

	// Token: 0x04000A67 RID: 2663
	public TField tfEmail;

	// Token: 0x04000A68 RID: 2664
	private int focus;

	// Token: 0x04000A69 RID: 2665
	private int yL;

	// Token: 0x04000A6A RID: 2666
	private int defYL;

	// Token: 0x04000A6B RID: 2667
	private Command cmdRemem;

	// Token: 0x04000A6C RID: 2668
	private Command cmdLogin;

	// Token: 0x04000A6D RID: 2669
	private Command cmdReg;

	// Token: 0x04000A6E RID: 2670
	private Command cmdRegister;

	// Token: 0x04000A6F RID: 2671
	private Command cmdBack;

	// Token: 0x04000A70 RID: 2672
	private Command cmdSelected;

	// Token: 0x04000A71 RID: 2673
	private bool isCheckBox = true;

	// Token: 0x04000A72 RID: 2674
	private Command cmdMenu;

	// Token: 0x04000A73 RID: 2675
	public bool isReg;

	// Token: 0x04000A74 RID: 2676
	public string numSupport = "19006610";

	// Token: 0x04000A75 RID: 2677
	public string passRemem = string.Empty;

	// Token: 0x04000A76 RID: 2678
	public short[] listIDPart;

	// Token: 0x04000A77 RID: 2679
	public int xLogin;

	// Token: 0x04000A78 RID: 2680
	public int yLogin;

	// Token: 0x04000A79 RID: 2681
	public int wLogin;

	// Token: 0x04000A7A RID: 2682
	public int hLogin;

	// Token: 0x04000A7B RID: 2683
	public int xCheck;

	// Token: 0x04000A7C RID: 2684
	public int yCheck;

	// Token: 0x04000A7D RID: 2685
	public int wC;

	// Token: 0x04000A7E RID: 2686
	public int xC;

	// Token: 0x04000A7F RID: 2687
	public long timeOut;

	// Token: 0x04000A80 RID: 2688
	public static int aa;

	// Token: 0x04000A81 RID: 2689
	public static bool isSelectedLanguage;

	// Token: 0x04000A82 RID: 2690
	public static bool isNewGame;

	// Token: 0x04000A83 RID: 2691
	public static bool isAccVir;

	// Token: 0x04000A84 RID: 2692
	private string[] listStrNew = new string[]
	{
		"Chơi mới",
		"Chơi tiêp",
		"Đổi tài khoản"
	};

	// Token: 0x04000A85 RID: 2693
	public int hCellNew;

	// Token: 0x04000A86 RID: 2694
	public int yNew;

	// Token: 0x04000A87 RID: 2695
	private sbyte indexNewGame;

	// Token: 0x04000A88 RID: 2696
	private string nameVir = string.Empty;

	// Token: 0x04000A89 RID: 2697
	private string passVir = string.Empty;

	// Token: 0x04000A8A RID: 2698
	private int h0;

	// Token: 0x04000A8B RID: 2699
	public static bool isLoadIP;

	// Token: 0x04000A8C RID: 2700
	public string referral;

	// Token: 0x04000A8D RID: 2701
	public string email;

	// Token: 0x04000A8E RID: 2702
	private int indexLQ;

	// Token: 0x04000A8F RID: 2703
	private bool isCheck;

	// Token: 0x04000A90 RID: 2704
	private bool isClickNew;

	// Token: 0x02000120 RID: 288
	private class IActionOkUser : IAction
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x0004FCF5 File Offset: 0x0004E0F5
		public void perform()
		{
		}
	}

	// Token: 0x02000121 RID: 289
	private class actDoSettingPassword : IKbAction
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x0004FCF7 File Offset: 0x0004E0F7
		public actDoSettingPassword(LoginScr b)
		{
			this.bscr = b;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0004FD08 File Offset: 0x0004E108
		public void perform(string text)
		{
			string text2 = Canvas.inputDlg.getText();
			if (text.Equals(string.Empty))
			{
				return;
			}
			this.bscr.doForgetPass(text);
		}

		// Token: 0x04000A91 RID: 2705
		private LoginScr bscr;
	}

	// Token: 0x02000122 RID: 290
	private class IActionForgetPass : IAction
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x0004FD3D File Offset: 0x0004E13D
		public IActionForgetPass(string user)
		{
			this.user = user;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0004FD4C File Offset: 0x0004E14C
		public void perform()
		{
			if (!Session_ME.connected)
			{
				Canvas.startWaitDlg(T.connecting);
				Canvas.connect();
			}
			else
			{
				Canvas.startWaitDlg();
			}
			GlobalService.gI().requestService(4, this.user);
		}

		// Token: 0x04000A92 RID: 2706
		private string user;
	}

	// Token: 0x02000123 RID: 291
	private class IActionEferral : IKbAction
	{
		// Token: 0x06000838 RID: 2104 RVA: 0x0004FD8A File Offset: 0x0004E18A
		public void perform(string text)
		{
			LoginScr.gI().referral = text;
			if (GameMidlet.IP == null)
			{
				LoginScr.isLoadIP = true;
				ServerListScr.gI().doUpdateServer();
			}
			else
			{
				LoginScr.gI().regRequest();
			}
		}
	}

	// Token: 0x02000124 RID: 292
	private class IAcionNewGameOk : IAction
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x0004FDC8 File Offset: 0x0004E1C8
		public void perform()
		{
			ServerListScr.gI().switchToMe();
		}
	}

	// Token: 0x02000125 RID: 293
	private class IActionChangeAcc : IAction
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x0004FDDC File Offset: 0x0004E1DC
		public void perform()
		{
			LoginScr.isNewGame = false;
			LoginScr.gI().right = new Command(T.back, 105, LoginScr.gI().xLogin + LoginScr.gI().wLogin - MyScreen.wTab / 2 - 30 * AvMain.hd, LoginScr.gI().yLogin + LoginScr.gI().hLogin - PaintPopup.hButtonSmall / 2);
			LoginScr.me.center = LoginScr.me.cmdLogin;
		}
	}
}
