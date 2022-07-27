using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class Canvas
{
	// Token: 0x06000126 RID: 294 RVA: 0x00007A28 File Offset: 0x00005E28
	public Canvas()
	{
		Canvas.w = (int)ScaleGUI.WIDTH;
		Canvas.h = (int)ScaleGUI.HEIGHT;
		Canvas.hCan = Canvas.h;
		this.initFont();
		Canvas.initResource();
		this.setSize(Canvas.w, Canvas.h);
		Canvas.hw = Canvas.w / 2;
		Canvas.hh = Canvas.h / 2;
		Canvas.instance = this;
		CRes.init();
		Canvas.currentPopup = new MyVector();
		Canvas.msgdlg = new MsgDlg();
		Canvas.avataData = new AvatarData();
		Canvas.inputDlg = new InputDlg();
		Canvas.loadMap = new LoadMap();
		Canvas.cameraList = new CameraList();
		if (Canvas.currentMyScreen != null && Canvas.currentMyScreen == OptionScr.instance)
		{
			OptionScr.gI().initSize();
		}
		Canvas.paint.initPos();
		Canvas.listPoint = new MyVector();
		MiniMap.gI();
		Canvas.paint.initString(0);
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00007B1D File Offset: 0x00005F1D
	public static void setPopupTime(string text)
	{
		Canvas.nameSV = text;
		Canvas.timeNameSV = 100;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00007B2C File Offset: 0x00005F2C
	public void initFont()
	{
		Canvas.t = new T();
		if (Main.hdtype == 2)
		{
			Canvas.normalFont = new HDFont(0, "vo", 24, 2720192, 2720192);
			Canvas.stypeInt = 2;
			Canvas.borderFont = new HDFont(1);
			Canvas.arialFont = new HDFont(2);
			Canvas.blackF = new HDFont(3);
			Canvas.numberFont = new HDFont(4, "Colossalis", 15131223, 0);
			Canvas.smallFontRed = new HDFont(5, "vo", 16, 16746118, 0);
			Canvas.smallFontYellow = new HDFont(6, "vo", 16, 15848992, 8344576);
			Canvas.menuFont = new HDFont(9);
			Canvas.tempFont = new HDFont(10, "temp");
			Canvas.normalWhiteFont = new HDFont(11, "normalW", 16777215, 5921884);
			Canvas.fontChat = new HDFont(15, "UTM Swiss Condensed_1", 2720192, 2720192);
			Canvas.fontChatB = new HDFont(16, "UTM Swiss Condensed_1", 0, 0);
			Canvas.fontBlu = new HDFont(17, "UTM Swiss Condensed_1", 22, 29068, 29068);
			Canvas.fontWhiteBold = new HDFont(18, "temp");
			Canvas.paint = new HDPaint();
		}
		if (Main.hdtype == 1)
		{
			Canvas.stypeInt = 1;
			Canvas.normalFont = new HDFont(0, "vo", 14, 2720192, 2720192);
			Canvas.borderFont = new HDFont(1);
			Canvas.arialFont = new HDFont(2);
			Canvas.blackF = new HDFont(3);
			Canvas.numberFont = new HDFont(4);
			Canvas.smallFontRed = new HDFont(5);
			Canvas.smallFontRed = new HDFont(5, "vo", 12, 16746118, 0);
			Canvas.smallFontYellow = new HDFont(6, "vo", 12, 15848992, 8344576);
			Canvas.menuFont = new HDFont(9);
			Canvas.tempFont = new HDFont(10, "temp");
			Canvas.normalWhiteFont = new HDFont(11, "normalW", 16777215, 5921884);
			Canvas.fontChat = new HDFont(15, "UTM Swiss Condensed_1", 2720192, 2720192);
			Canvas.fontChatB = new HDFont(16, "UTM Swiss Condensed_1", 0, 0);
			Canvas.fontBlu = new HDFont(17, "UTM Swiss Condensed_1", 14, 29068, 29068);
			Canvas.fontWhiteBold = new HDFont(18, "temp");
			Canvas.paint = new MediumPaint();
			Debug.Log("MEDIUM");
		}
		MyScreen.ITEM_HEIGHT = Canvas.normalFont.getHeight() + 6;
		AvMain.hBlack = (sbyte)Canvas.blackF.getHeight();
		AvMain.hBorder = (sbyte)Canvas.borderFont.getHeight();
		AvMain.hNormal = (sbyte)Canvas.normalFont.getHeight();
		AvMain.hSmall = (sbyte)Canvas.smallFontRed.getHeight();
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00007E0A File Offset: 0x0000620A
	public void sizeChanged(int w, int h)
	{
		if (!TField.isOpenTextBox)
		{
			this.setSize(w, h);
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00007E1E File Offset: 0x0000621E
	public static void paintPlus(MyGraphics g)
	{
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00007E20 File Offset: 0x00006220
	public static void paintPlus2(MyGraphics g)
	{
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00007E24 File Offset: 0x00006224
	public void setSize(int wd, int hd)
	{
		Out.println(string.Concat(new object[]
		{
			"setSize: ",
			wd,
			"   ",
			hd
		}));
		Canvas.w = wd;
		Canvas.rw = wd;
		Canvas.rh = (Canvas.h = hd - Canvas.transTab);
		Canvas.rh = hd;
		Canvas.hCan = hd;
		AvMain.duPopup = 20;
		Canvas.isVirHorizontal = false;
		Canvas.hw = Canvas.w / 2;
		Canvas.hh = Canvas.h / 2;
		Canvas.paint.initPos();
		Canvas.paint.init();
		Canvas.menuMain = null;
		if (Canvas.currentMyScreen != null && Canvas.currentMyScreen == LoginScr.me)
		{
			LoginScr.gI().init(hd);
		}
		AvCamera.gI().init(LoadMap.TYPEMAP);
		if (PopupShop.me != null)
		{
			PopupShop.init();
		}
		if (PaintPopup.me != null)
		{
			PaintPopup.gI().init();
		}
		if (BoardScr.me != null)
		{
			BoardScr.me.init();
		}
		if (Canvas.msgdlg != null)
		{
			Canvas.msgdlg.init();
		}
		if (RoomListOnScr.instance != null && RoomListOnScr.instance == Canvas.currentMyScreen)
		{
			RoomListOnScr.gI().init();
		}
		if (Canvas.inputDlg != null)
		{
			Canvas.inputDlg.init(Canvas.hCan);
		}
		if (Canvas.currentMyScreen != null && Canvas.currentMyScreen == MoneyScr.instance)
		{
			MoneyScr.gI().initCanvas();
		}
		if (ChatTextField.instance != null)
		{
			ChatTextField.gI().init(Canvas.hCan);
		}
		if (CustomTab.me != null && CustomTab.me == Canvas.currentFace)
		{
			CustomTab.gI().init();
		}
		if (Canvas.currentMyScreen != null)
		{
			if (Canvas.currentMyScreen == RaceScr.me)
			{
				RaceScr.gI().initPos();
			}
			if (RegisterScr.instance == Canvas.currentMyScreen)
			{
				RegisterScr.gI().init();
			}
			if (BoardListOnScr.me == Canvas.currentMyScreen)
			{
				BoardListOnScr.gI().setCam();
			}
			if (Canvas.currentMyScreen == OptionScr.instance)
			{
				OptionScr.gI().initSize();
			}
			if (Canvas.currentMyScreen == ServerListScr.me)
			{
				ServerListScr.gI().init();
			}
			if (Canvas.currentMyScreen == ListScr.gI())
			{
				ListScr.gI().reSize();
			}
			if (Canvas.currentMyScreen == MoneyScr.gI())
			{
				MoneyScr.gI().init();
			}
			if (Canvas.currentFace != null)
			{
				Canvas.currentFace.init(Canvas.hCan);
			}
		}
		Canvas.cmdEndDlg = new Command(T.no, -1);
	}

	// Token: 0x0600012D RID: 301 RVA: 0x000080C8 File Offset: 0x000064C8
	public void setInfoSV(string info)
	{
		Out.println("setInfoSV: " + info);
		if (onMainMenu.isOngame || info.Equals(string.Empty))
		{
			return;
		}
		StringObj stringObj = new StringObj(info, -Canvas.arialFont.getWidth(info));
		stringObj.x = Canvas.w + 10;
		Canvas.listInfoSV.addElement(stringObj);
		if (Canvas.countTab == 0)
		{
			Canvas.countTab = 1;
		}
		if (Canvas.isPaint18)
		{
			Canvas.countTab = 15 * AvMain.hd;
		}
		Canvas.transTab = 0;
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000815C File Offset: 0x0000655C
	public static void connect()
	{
		if (!Session_ME.gI().isConnected())
		{
			int num = ServerListScr.selected - 1;
			if (num < 0)
			{
			}
			string text;
			int num2;
			if (GameMidlet.isEnglish)
			{
				text = GameMidlet.IPEng;
				num2 = GameMidlet.PORTEng;
				GameMidlet.gameID = "14";
			}
			else
			{
				text = GameMidlet.IP[OptionScr.gI().mapFocus[4]][ServerListScr.indexSV][ServerListScr.index];
				num2 = GameMidlet.PORT[OptionScr.gI().mapFocus[4]][ServerListScr.indexSV][ServerListScr.index];
				MiniMap.nameSV = GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][ServerListScr.indexSV][ServerListScr.index + 1];
				if (ServerListScr.indexSV > 0)
				{
					GameMidlet.gameID = "13";
				}
				else
				{
					GameMidlet.gameID = "12";
				}
			}
			Session_ME.gI().setHandler(GlobalMessageHandler.gI());
			Out.println(string.Concat(new object[]
			{
				"connect: ",
				text,
				":",
				num2,
				"     ",
				ServerListScr.indexSV,
				"    ",
				ServerListScr.index
			}));
			Session_ME.gI().connect(text, num2);
			GlobalService.gI().setProviderAndClientType();
		}
	}

	// Token: 0x0600012F RID: 303 RVA: 0x000082B1 File Offset: 0x000066B1
	public void start()
	{
		Canvas.isStart = true;
		Session_ME.gI().close();
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000082C4 File Offset: 0x000066C4
	public void setLimitMap()
	{
		Canvas.currentMyScreen.initZoom();
		AvCamera.gI().timeDelay = 0L;
		AvCamera.gI().vY = 0f;
		AvCamera.gI().vX = 0f;
		AvCamera.gI().xTo = Canvas.xZoom - (float)(Canvas.w / 2) / AvMain.zoom;
		AvCamera.gI().yTo = Canvas.yZoom - (float)(Canvas.hCan / 2) / AvMain.zoom;
		AvCamera.gI().update();
		if (AvCamera.gI().yTo > AvCamera.gI().yLimit)
		{
			AvCamera.gI().yTo = (AvCamera.gI().yCam = AvCamera.gI().yLimit);
		}
		if (Canvas.w < (int)LoadMap.wMap * AvCamera.w)
		{
			if (AvCamera.gI().xTo < 0f)
			{
				AvCamera.gI().xTo = 0f;
			}
			else if (AvCamera.gI().xTo > AvCamera.gI().xLimit)
			{
				AvCamera.gI().xTo = AvCamera.gI().xLimit;
			}
		}
		AvCamera.gI().xCam = AvCamera.gI().xTo;
		AvCamera.gI().yCam = AvCamera.gI().yTo;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00008418 File Offset: 0x00006818
	public void initKeyBoard(int ih, bool isRe)
	{
		Out.println(string.Concat(new object[]
		{
			"initKeyBoard: ",
			ih,
			"   ",
			Canvas.currentMyScreen,
			"     ",
			MessageScr.me
		}));
		if (ChatTextField.isShow || isRe || Screen.orientation == 1)
		{
			this.setSize(Canvas.w, ih);
		}
		else
		{
			if (Canvas.currentMyScreen == LoginScr.me)
			{
				LoginScr.gI().init(ih);
			}
			if (Canvas.currentFace != null)
			{
				Canvas.currentFace.init(ih);
			}
			if (Canvas.currentDialog != null)
			{
				Canvas.currentDialog.init(ih);
			}
		}
		if (Canvas.currentMyScreen == MessageScr.me)
		{
			MessageScr.gI().init(ih);
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x000084F0 File Offset: 0x000068F0
	public void update()
	{
		try
		{
			if (Canvas.isRotateTop == 1 && Screen.orientation == 1)
			{
				Canvas.isRotateTop = 0;
				TouchScreenKeyboard.hideInput = true;
				Screen.orientation = 1;
				ScaleGUI.initScaleGUI();
				this.setSize((int)ScaleGUI.WIDTH, (int)ScaleGUI.HEIGHT);
				Out.println(string.Concat(new object[]
				{
					"isRotateTop: ",
					1,
					"    :",
					ipKeyboard.tk
				}));
				ChatTextField.isShow = false;
			}
			else if (Canvas.isRotateTop == 2 && Screen.orientation != 1)
			{
				Out.println("isRotateTop: " + 2);
				Canvas.isRotateTop = 0;
				Screen.orientation = 3;
				ChatTextField.isShow = false;
				ipKeyboard.tk.active = false;
				ipKeyboard.isReset = true;
				ipKeyboard.tk = null;
				ScaleGUI.initScaleGUI();
				this.setSize((int)ScaleGUI.WIDTH, (int)ScaleGUI.HEIGHT);
			}
			if (Session_ME.isStart && (Canvas.getTick() - Session_ME.timeStart) / 1000L > 20L && Session_ME.messageHandler != null)
			{
				Canvas.startOK(T.canNotConnect, new GlobalLogicHandler.IActionDisconnect());
				Session_ME.gI().close();
			}
			if (Canvas.listAc.size() > 0 && (Canvas.currentDialog == null || Canvas.msgdlg.isWaiting))
			{
				IAction action = (IAction)Canvas.listAc.elementAt(0);
				Canvas.listAc.removeElement(action);
				action.perform();
			}
			if (Canvas.currentMyScreen != MiniMap.me)
			{
				if (Input.touchCount == 0)
				{
					if (Canvas.isZoom)
					{
						Canvas.isPointerRelease = false;
						if ((double)AvMain.zoom > 2.0 || (AvMain.zoom > 1f && (double)AvMain.zoom < 1.25) || ((double)AvMain.zoom > 1.5 && AvMain.zoom < 2f))
						{
							Canvas.dirZoom = -1;
						}
						else
						{
							Canvas.dirZoom = 1;
						}
					}
					float num = AvMain.zoom * 1000f;
					int num2 = (int)num;
					if (num2 % 500 != 0)
					{
						float num3 = (float)(num2 % 500);
						if ((int)Canvas.dirZoom == 1)
						{
							num3 = 500f - num3;
						}
						float num4 = num3 / 1000f;
						if (num4 < 0.05f)
						{
							int num5 = num2 % 500;
							float num6 = (float)(num2 - num5);
							if ((int)Canvas.dirZoom == 1)
							{
								num6 += 500f;
							}
							float zoom = num6 / 1000f;
							AvMain.zoom = zoom;
							Canvas.isZoom = false;
						}
						else
						{
							AvMain.zoom += num4 / 5f * (float)Canvas.dirZoom;
						}
						this.setLimitMap();
					}
				}
				if (Input.touchCount == 1)
				{
					Canvas.isZoom = false;
				}
				if (Input.touchCount > 1)
				{
					if (Canvas.currentDialog == null && Canvas.menuMain == null && Canvas.currentMyScreen != ListScr.instance && Canvas.currentMyScreen != PopupShop.me)
					{
						Touch touch = Input.GetTouch(0);
						Touch touch2 = Input.GetTouch(1);
						if (!Canvas.isZoom)
						{
							Canvas.xZoom = AvCamera.gI().xCam + (float)(Canvas.w / 2) / AvMain.zoom;
							Canvas.yZoom = AvCamera.gI().yCam + (float)(Canvas.hCan / 2) / AvMain.zoom;
							AvCamera.gI().vY = 0f;
							AvCamera.gI().vX = 0f;
						}
						if (touch.phase == 1 || touch2.phase == 1)
						{
							float num7 = Vector2.Distance(touch.position, touch2.position);
							float num8 = num7 - Canvas.disStart;
							if (Canvas.isZoom && Canvas.disStart != 0f)
							{
								Canvas.temp = num7 - Canvas.disStartZoom;
								AvMain.zoom += num8 / 800f;
								if (AvMain.zoom < 1f)
								{
									AvMain.zoom = 1f;
								}
								else if (AvMain.zoom > 2.2f)
								{
									AvMain.zoom = 2.2f;
								}
								this.setLimitMap();
							}
							else
							{
								Canvas.disStartZoom = Vector2.Distance(touch.position, touch2.position);
							}
							Canvas.disStart = Vector2.Distance(touch.position, touch2.position);
						}
						Canvas.isZoom = true;
					}
				}
				else
				{
					Canvas.disStart = 0f;
				}
			}
			if (Screen.orientation != 1)
			{
				if (Input.deviceOrientation == 3 && Screen.orientation != 3)
				{
					Screen.orientation = 3;
				}
				if (Input.deviceOrientation == 4 && Screen.orientation != 4)
				{
					Screen.orientation = 4;
				}
			}
			if (ipKeyboard.isReset)
			{
				if (TouchScreenKeyboard.visible)
				{
					if (TField.currentTField.autoScaleScreen)
					{
						int num9 = (int)ScaleGUI.WIDTH;
						int num10 = (int)(ScaleGUI.HEIGHT - TouchScreenKeyboard.area.height);
						if (Screen.orientation != 1)
						{
							if (ScaleGUI.WIDTH >= 960f)
							{
								num10 = (int)(ScaleGUI.HEIGHT - TouchScreenKeyboard.area.height - 162f);
								if (ScaleGUI.isAndroid && ScaleGUI.scaleAndroid == 2)
								{
									num10 -= 115;
								}
							}
							if (ScaleGUI.WIDTH == 1024f)
							{
								num10 = (int)(ScaleGUI.HEIGHT - TouchScreenKeyboard.area.height);
							}
							num10 = (int)(ScaleGUI.HEIGHT - TouchScreenKeyboard.area.height);
						}
						else
						{
							if (ScaleGUI.HEIGHT >= 960f)
							{
								num10 = (int)(ScaleGUI.HEIGHT - TouchScreenKeyboard.area.height - 220f);
								if (ScaleGUI.isAndroid && ScaleGUI.scaleAndroid == 2)
								{
									num10 -= 115;
								}
							}
							if (ScaleGUI.HEIGHT == 1024f)
							{
								num10 = (int)(ScaleGUI.HEIGHT - TouchScreenKeyboard.area.height);
							}
							num10 = (int)(ScaleGUI.HEIGHT - TouchScreenKeyboard.area.height);
						}
						Canvas.hKeyBoard = num10;
						if (num10 != Canvas.rh)
						{
							Canvas.isKeyBoard = true;
							Canvas.aTran = true;
							if (Canvas.aTran)
							{
								Canvas.aTran = false;
								this.initKeyBoard(num10, false);
							}
							else
							{
								Canvas.aTran = true;
							}
							Canvas.rh = num10;
						}
					}
				}
				else if ((float)Canvas.rw != ScaleGUI.WIDTH || (float)Canvas.rh != ScaleGUI.HEIGHT || (float)Canvas.hCan != ScaleGUI.HEIGHT)
				{
					if (Canvas.isKeyBoard)
					{
						Canvas.isKeyBoard = false;
						this.initKeyBoard((int)ScaleGUI.HEIGHT, true);
					}
					if (Canvas.isKeyBoard && Canvas.rh == Canvas.hKeyBoard)
					{
						Canvas.isKeyBoard = false;
						Canvas.hKeyBoard = 0;
					}
					if (ChatTextField.isShow)
					{
						ChatTextField.isShow = false;
					}
				}
			}
			Canvas.gameTick++;
			if (Canvas.gameTick > 10000)
			{
				Canvas.gameTick = 0;
			}
			if (Canvas.timeNameSV >= 0)
			{
				Canvas.timeNameSV--;
			}
			if (Canvas.load != -1)
			{
				this.updateOpenScr();
			}
			if (Canvas.load != 0 || onMainMenu.isOngame)
			{
				if (Canvas.load != 0 && Canvas.welcome != null && Canvas.currentDialog == null)
				{
					Canvas.welcome.updateKey();
				}
				if (Canvas.currentEffect.size() > 0)
				{
					for (int i = 0; i < Canvas.currentEffect.size(); i++)
					{
						Effect effect = (Effect)Canvas.currentEffect.elementAt(i);
						effect.update();
					}
				}
				if (Canvas.currentMyScreen != null)
				{
					if (Canvas.load != 0 && Canvas.currentDialog != null)
					{
						Canvas.currentDialog.updateKey();
					}
					else if (Canvas.load != 0 && ChatTextField.isShow)
					{
						ChatTextField.gI().updateKey();
					}
					this.updateInfoSV();
					Canvas.currentMyScreen.update();
					if (Canvas.cameraList.isShow)
					{
						Canvas.cameraList.moveCamera();
					}
					if (Canvas.load != 0 && Canvas.currentFace != null)
					{
						Canvas.currentFace.updateKey();
					}
					if (Canvas.load != 0 && Canvas.currentDialog != null)
					{
						Canvas.currentDialog.updateKey();
					}
					else if (Canvas.menuMain != null)
					{
						Canvas.menuMain.updateKey();
						if (Canvas.menuMain != null)
						{
							Canvas.menuMain.update();
						}
					}
					else
					{
						if (Canvas.load != 0 && Canvas.currentFace == null && !Canvas.isZoom && !ChatTextField.isShow)
						{
							Canvas.currentMyScreen.updateKey();
						}
						if (Canvas.cameraList.isShow && Canvas.currentFace == null)
						{
							Canvas.cameraList.updateKey();
						}
					}
					if (Canvas.gameTick % 20 == 10)
					{
						AvatarData.setLimitImage();
					}
				}
				Canvas.isPointerClick = false;
				Canvas.isPointerRelease = false;
				this.updateFlyTexts();
				if (SoundManager.isStop)
				{
					Main.main.GetComponent<AudioSource>().volume -= 0.02f;
					if (Main.main.GetComponent<AudioSource>().volume == 0f)
					{
						SoundManager.isStop = false;
						SoundManager.stopALLBGSound();
					}
				}
				if (SoundManager.isOpen)
				{
					Main.main.GetComponent<AudioSource>().volume += 0.02f;
					if (Main.main.GetComponent<AudioSource>().volume * 100f >= (float)OptionScr.gI().volume)
					{
						SoundManager.isOpen = false;
					}
				}
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00008EDC File Offset: 0x000072DC
	public void updateInfoSV()
	{
		if (Canvas.listInfoSV.size() <= 0)
		{
			if (Canvas.countTab > ((!Canvas.isPaint18) ? 0 : ((AvMain.hd != 2) ? 20 : 30)))
			{
				Canvas.countTab--;
				Canvas.currentMyScreen.initTabTrans();
			}
			return;
		}
		int num = Canvas.menuFont.getHeight() + 2;
		if (Canvas.isPaint18)
		{
			num = 35;
		}
		if (Canvas.countTab < num)
		{
			Canvas.countTab++;
			Canvas.currentMyScreen.initTabTrans();
		}
		StringObj stringObj = (StringObj)Canvas.listInfoSV.elementAt(0);
		stringObj.x -= 2;
		if (stringObj.x < stringObj.w2)
		{
			Canvas.listInfoSV.removeElementAt(0);
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00008FB4 File Offset: 0x000073B4
	public void paintInfoSV(MyGraphics g)
	{
		Canvas.resetTransNotZoom(g);
		if (Canvas.isPaint18)
		{
			g.drawImageScale(Canvas.imgTabInfo, 0, Canvas.countTab - 40, Canvas.w, 40 + ((AvMain.hd != 2 || Canvas.listInfoSV.size() <= 0) ? 0 : 20), 0);
		}
		else
		{
			g.drawImageScale(Canvas.imgTabInfo, 0, Canvas.countTab - 30, Canvas.w, 30, 0);
		}
		if (Canvas.listInfoSV.size() > 0)
		{
			int num = Canvas.countTab / 2 - (int)AvMain.hBlack / 2 + ((AvMain.hd != 2) ? -2 : 1);
			StringObj stringObj = (StringObj)Canvas.listInfoSV.elementAt(0);
			Canvas.menuFont.drawString(g, stringObj.str, stringObj.x, num + ((!Canvas.isPaint18) ? -12 : 8) + ((AvMain.hd != 2 || Canvas.listInfoSV.size() <= 0) ? 0 : 14), 0);
			Canvas.resetTrans(g);
		}
	}

	// Token: 0x06000135 RID: 309 RVA: 0x000090D0 File Offset: 0x000074D0
	private void updateFlyTexts()
	{
		for (int i = 0; i < Canvas.flyTexts.size(); i++)
		{
			FlyTextInfo flyTextInfo = (FlyTextInfo)Canvas.flyTexts.elementAt(i);
			flyTextInfo.update();
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00009110 File Offset: 0x00007510
	private void paintFlyTexts(MyGraphics g)
	{
		Canvas.resetTrans(g);
		g.translate(-AvCamera.gI().xCam, -AvCamera.gI().yCam);
		for (int i = 0; i < Canvas.flyTexts.size(); i++)
		{
			FlyTextInfo flyTextInfo = (FlyTextInfo)Canvas.flyTexts.elementAt(i);
			flyTextInfo.paint(g);
		}
		Canvas.resetTrans(g);
	}

	// Token: 0x06000137 RID: 311 RVA: 0x00009178 File Offset: 0x00007578
	public static bool isKeyPressed(int index)
	{
		if (Canvas.keyPressed[index])
		{
			Canvas.keyPressed[index] = false;
			return true;
		}
		return false;
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00009191 File Offset: 0x00007591
	public void pointerDragged(int x, int y)
	{
		Canvas.px = x;
		Canvas.py = y;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000919F File Offset: 0x0000759F
	public void pointerPressed(int x, int y)
	{
		Canvas.isPointerClick = true;
		Canvas.isPointerDown = true;
		Canvas.pxLast = x;
		Canvas.pyLast = y;
		Canvas.px = x;
		Canvas.py = y;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x000091C5 File Offset: 0x000075C5
	public void pointerReleased(int x, int y)
	{
		Canvas.isPointerDown = false;
		Canvas.isPointerRelease = true;
		Canvas.px = x;
		Canvas.py = y;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x000091E0 File Offset: 0x000075E0
	public static void clearKeyPressed()
	{
		Canvas.isPointerRelease = false;
		for (int i = 0; i < 14; i++)
		{
			Canvas.keyPressed[i] = false;
		}
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00009210 File Offset: 0x00007610
	public static void clearKeyHold()
	{
		Canvas.isPointerRelease = false;
		Canvas.isPointerDown = false;
		for (int i = 0; i < 14; i++)
		{
			Canvas.keyHold[i] = false;
		}
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00009244 File Offset: 0x00007644
	public static void clearKeyReleased()
	{
		Canvas.isPointerDown = false;
		for (int i = 0; i < 14; i++)
		{
			Canvas.keyReleased[i] = false;
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00009274 File Offset: 0x00007674
	public static void addFlyText(int text, int x, int y, int dir, int delay, int imgIDFarm, int imgIDAvatar)
	{
		Canvas.flyTexts.addElement(new FlyTextInfo(x, y, text, dir, null, delay, imgIDFarm, imgIDAvatar));
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000929C File Offset: 0x0000769C
	public static void addFlyText(int text, int x, int y, int dir, int delay)
	{
		Canvas.flyTexts.addElement(new FlyTextInfo(x, y, text, dir, null, delay, -1, -1));
	}

	// Token: 0x06000140 RID: 320 RVA: 0x000092C4 File Offset: 0x000076C4
	public static void addFlyText(int text, int x, int y, int dir, Image img, int delay)
	{
		Canvas.flyTexts.addElement(new FlyTextInfo(x, y, text, dir, img, delay, -1, -1));
	}

	// Token: 0x06000141 RID: 321 RVA: 0x000092EA File Offset: 0x000076EA
	public static void addFlyTextSmall(string text, int x, int y, int dir, int type, int delay)
	{
		Canvas.flyTexts.addElement(new FlyTextInfo(x, y, text, dir, type, delay));
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00009304 File Offset: 0x00007704
	public void onPaint(MyGraphics g)
	{
		if (ScaleGUI.scaleAndroid == 2)
		{
			GUIUtility.ScaleAroundPivot(new Vector2(2f, 2f), Vector2.zero);
		}
		try
		{
			long tick = Canvas.getTick();
			Canvas.resetTrans(g);
			g.translate(0f, 0f);
			if (Canvas.load != 0 || onMainMenu.isOngame)
			{
				if (Canvas.currentMyScreen != null)
				{
					Canvas.currentMyScreen.paint(g);
				}
				if (Canvas.currentEffect.size() > 0)
				{
					for (int i = 0; i < Canvas.currentEffect.size(); i++)
					{
						Effect effect = (Effect)Canvas.currentEffect.elementAt(i);
						effect.paint(g);
					}
				}
				if (ChatTextField.isShow)
				{
					ChatTextField.gI().paint(g);
				}
				if (Canvas.currentFace != null)
				{
					Canvas.currentFace.paint(g);
				}
				if (Canvas.currentDialog != null)
				{
					Canvas.currentDialog.paint(g);
				}
				else if (Canvas.menuMain != null)
				{
					Canvas.menuMain.paint(g);
				}
				if (Canvas.currentMyScreen != MessageScr.me)
				{
					Canvas.paint.paintMSG(g);
				}
				if (Canvas.welcome != null)
				{
					Canvas.welcome.paint(g);
				}
				this.paintFlyTexts(g);
			}
			this.paintInfoSV(g);
			if (Canvas.load != -1)
			{
				this.paintOpenScr(g);
			}
			if (Canvas.timeNameSV > 0)
			{
				this.paintPopupTime(g);
			}
			Canvas.resetTransNotZoom(g);
			if (Canvas.isPaint18)
			{
				g.drawImage(Canvas.imagePlug, (float)(3 * AvMain.hd), (float)((AvMain.hd != 2) ? 10 : 14), 2);
				Canvas.menuFont.drawString(g, "Chơi quá 180 phút một ngày sẽ ảnh hưởng xấu đến sức khỏe.", 4 * AvMain.hd + Canvas.imagePlug.getWidth(), 1, 0);
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		if (ScaleGUI.scaleAndroid == 2)
		{
			GUIUtility.ScaleAroundPivot(new Vector2(0f, 0f), Vector2.zero);
		}
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00009528 File Offset: 0x00007928
	public void paintPopupTime(MyGraphics g)
	{
		MiniMap.imgPopupName.drawFrame(0, Canvas.hw - 85 * AvMain.hd / 2, 15 * AvMain.hd + 30 * AvMain.hd / 2, 0, 3, g);
		MiniMap.imgPopupName.drawFrame(0, Canvas.hw + 85 * AvMain.hd / 2 + ((AvMain.hd != 1) ? 0 : 1), 15 * AvMain.hd + 30 * AvMain.hd / 2, 2, 3, g);
		Canvas.arialFont.drawString(g, Canvas.nameSV, Canvas.hw, 15 * AvMain.hd + 30 * AvMain.hd / 2 - Canvas.arialFont.getHeight() / 2, 2);
	}

	// Token: 0x06000144 RID: 324 RVA: 0x000095E0 File Offset: 0x000079E0
	private void paintEffect(MyGraphics g)
	{
		if (Canvas.listPoint.size() > 0)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setColor(16627970);
			for (int i = 0; i < Canvas.listPoint.size(); i++)
			{
				AvPosition avPosition = (AvPosition)Canvas.listPoint.elementAt(i);
				avPosition.anchor++;
				g.fillRect((float)(avPosition.x - 2), (float)(avPosition.y - 2), 4f, 4f);
				if (avPosition.anchor > 16)
				{
					Canvas.listPoint.removeElement(avPosition);
					i--;
				}
			}
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00009694 File Offset: 0x00007A94
	public static void resetTrans(MyGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		int num = (int)((float)Canvas.xTran / AvMain.zoom);
		g.translate((float)num, (float)Canvas.transTab);
		g.setClip(0f, 0f, (float)Canvas.w, (float)(Canvas.hCan + Canvas.hTab));
	}

	// Token: 0x06000146 RID: 326 RVA: 0x000096F4 File Offset: 0x00007AF4
	public static void resetTransNotZoom(MyGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.translate((float)Canvas.xTran, (float)Canvas.transTab);
		g.setClip(0f, 0f, (float)Canvas.w, (float)(Canvas.hCan + Canvas.hTab));
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000974C File Offset: 0x00007B4C
	public void updateOpenScr()
	{
		if (Canvas.load == 1)
		{
			this.countDown += 15;
		}
		else
		{
			this.num++;
			if (this.num >= MsgDlg.imgLoad.nFrame * ((!onMainMenu.isOngame) ? 1 : 2))
			{
				this.num = 0;
			}
		}
		if (this.countDown >= Canvas.hh)
		{
			this.countDown = 0;
			Canvas.load = -1;
		}
	}

	// Token: 0x06000148 RID: 328 RVA: 0x000097D4 File Offset: 0x00007BD4
	public void paintOpenScr(MyGraphics g)
	{
		Canvas.resetTransNotZoom(g);
		if (!onMainMenu.isOngame)
		{
			g.setClip(0f, 0f, (float)((int)ScaleGUI.WIDTH), (float)((int)ScaleGUI.HEIGHT + 50));
			g.setColor(0);
			g.fillRect(0f, 0f, (float)Canvas.w, (float)(Canvas.hh - this.countDown));
			g.fillRect(0f, (float)(Canvas.hh + this.countDown), (float)Canvas.w, (float)(Canvas.hh - this.countDown));
		}
		if (Canvas.load != 1)
		{
			MsgDlg.imgLoad.drawFrame(this.num / 2, (int)ScaleGUI.WIDTH / 2, (int)ScaleGUI.HEIGHT / 2, 0, 3, g);
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00009897 File Offset: 0x00007C97
	public static void endDlg()
	{
		Canvas.msgdlg.setIsWaiting(false);
		Canvas.currentDialog = null;
	}

	// Token: 0x0600014A RID: 330 RVA: 0x000098AA File Offset: 0x00007CAA
	public static void startOKDlg(string info)
	{
		Canvas.msgdlg.setInfoC(info, new Command(T.OK, new Canvas.IActionOk()));
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000098C6 File Offset: 0x00007CC6
	public static void startOKDlg(string info, IAction yes)
	{
		Canvas.msgdlg.setInfoLR(info, new Command(T.yes, yes), Canvas.cmdEndDlg);
	}

	// Token: 0x0600014C RID: 332 RVA: 0x000098E3 File Offset: 0x00007CE3
	public static void startOK(string info, IAction ok)
	{
		Canvas.msgdlg.setInfoC(info, new Command(T.OK, ok));
	}

	// Token: 0x0600014D RID: 333 RVA: 0x000098FB File Offset: 0x00007CFB
	public static void startOKDlg(string info, int index)
	{
		Canvas.msgdlg.setInfoLR(info, new Command(T.yes, index), Canvas.cmdEndDlg);
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00009918 File Offset: 0x00007D18
	public static void startOKDlg(string info, int index, AvMain pointer)
	{
		Canvas.msgdlg.setInfoLR(info, new Command(T.yes, index, pointer), Canvas.cmdEndDlg);
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00009936 File Offset: 0x00007D36
	public static void startOK(string info, int index)
	{
		Canvas.msgdlg.setInfoC(info, new Command(T.OK, index));
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000994E File Offset: 0x00007D4E
	public static void startWaitDlg(string info)
	{
		Canvas.msgdlg.setInfoC(info, null);
		Canvas.msgdlg.setIsWaiting(true);
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00009967 File Offset: 0x00007D67
	public static void startWaitCancelDlg(string info)
	{
		Canvas.msgdlg.setInfoC(info, new Command(T.cancel, -1));
	}

	// Token: 0x06000152 RID: 338 RVA: 0x0000997F File Offset: 0x00007D7F
	public static void startWaitDlg()
	{
		Canvas.startWaitDlg(T.pleaseWait);
	}

	// Token: 0x06000153 RID: 339 RVA: 0x0000998C File Offset: 0x00007D8C
	public static string getPriceMoney(int xu, int gold, bool iss)
	{
		string text = string.Empty;
		if (xu > 0)
		{
			text = text + Canvas.getMoneys(xu) + T.xu;
		}
		if (gold > 0)
		{
			if (xu > 0)
			{
				text += " - ";
			}
			text = text + Canvas.getMoneys(gold) + T.gold;
		}
		return text;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x000099E8 File Offset: 0x00007DE8
	public static string getMoneys(int m)
	{
		string text = string.Empty;
		int num = m / 1000 + 1;
		for (int i = 0; i < num; i++)
		{
			if (m < 1000)
			{
				text = m + text;
				break;
			}
			int num2 = m % 1000;
			if (num2 == 0)
			{
				text = ".000" + text;
			}
			else if (num2 < 10)
			{
				text = ".00" + num2 + text;
			}
			else if (num2 < 100)
			{
				text = ".0" + num2 + text;
			}
			else
			{
				text = "." + num2 + text;
			}
			m /= 1000;
		}
		return text;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00009AB2 File Offset: 0x00007EB2
	public static bool isPointer(int x, int y, int w, int h)
	{
		return (Canvas.isPointerDown || Canvas.isPointerRelease) && Canvas.isPoint(x, y, w, h);
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00009AD3 File Offset: 0x00007ED3
	public static bool isPoint(int x, int y, int w, int h)
	{
		return Canvas.px >= x && Canvas.px <= x + w && Canvas.py >= y && Canvas.py <= y + h;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00009B08 File Offset: 0x00007F08
	public static void getTypeMoney(int xu, int luong, IAction iXu, IAction iLuong, IAction iaEnd)
	{
		string text = string.Empty;
		MyVector myVector = new MyVector();
		if (xu > 0)
		{
			myVector.addElement(new Command((luong > 0) ? T.xu : T.yes, iXu));
			text = " " + xu + T.xu;
		}
		if (luong > 0)
		{
			myVector.addElement(new Command((xu > 0) ? T.gold : T.yes, iLuong));
			text = " " + luong + T.gold;
		}
		if (myVector.size() == 1)
		{
			text = string.Concat(new string[]
			{
				T.doYouWanBuyPrice,
				text,
				" ",
				T.no,
				" ?"
			});
		}
		else
		{
			text = string.Concat(new object[]
			{
				T.selectMoney,
				" \n",
				xu,
				T.xu,
				" - ",
				luong,
				" ",
				T.gold
			});
		}
		if (iaEnd == null)
		{
			myVector.addElement(Canvas.cmdEndDlg);
		}
		else
		{
			myVector.addElement(new Command(T.no, iaEnd));
		}
		int num = myVector.size();
		if (num != 1)
		{
			if (num != 2)
			{
				if (num == 3)
				{
					Canvas.msgdlg.setInfoLCR(text, (Command)myVector.elementAt(0), (Command)myVector.elementAt(1), (Command)myVector.elementAt(2));
				}
			}
			else
			{
				Canvas.msgdlg.setInfoLR(text, (Command)myVector.elementAt(0), (Command)myVector.elementAt(1));
			}
		}
		else
		{
			Canvas.msgdlg.setInfoC(text, (Command)myVector.elementAt(0));
		}
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00009CF0 File Offset: 0x000080F0
	public void onKeyPressed(int keyCode)
	{
		this.lastTimePress = DateTime.Now.Ticks;
		Canvas.mapKeyPress(keyCode);
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00009D18 File Offset: 0x00008118
	public static void mapKeyPress(int keyCode)
	{
		if (Canvas.currentFace != null)
		{
			Canvas.currentFace.keyPress(keyCode);
		}
		else if (Canvas.currentDialog != null)
		{
			Canvas.currentDialog.keyPress(keyCode);
		}
		else if (Canvas.menuMain == null)
		{
			if (ChatTextField.isShow)
			{
				ChatTextField.gI().keyPressed(keyCode);
			}
			else
			{
				Canvas.currentMyScreen.keyPress(keyCode);
			}
		}
		switch (keyCode + 7)
		{
		case 0:
			goto IL_FD;
		case 1:
			break;
		case 2:
			goto IL_110;
		case 3:
			Canvas.keyHold[6] = true;
			Canvas.keyPressed[6] = true;
			return;
		case 4:
			Canvas.keyHold[4] = true;
			Canvas.keyPressed[4] = true;
			return;
		case 5:
			goto IL_132;
		case 6:
			goto IL_121;
		default:
			if (keyCode == -39)
			{
				goto IL_132;
			}
			if (keyCode == -38)
			{
				goto IL_121;
			}
			if (keyCode == -22)
			{
				goto IL_FD;
			}
			if (keyCode != -21)
			{
				if (keyCode == 10)
				{
					goto IL_110;
				}
				if (keyCode == 35)
				{
					Canvas.keyHold[11] = true;
					Canvas.keyPressed[11] = true;
					return;
				}
				if (keyCode != 42)
				{
					return;
				}
				Canvas.keyHold[10] = true;
				Canvas.keyPressed[10] = true;
				return;
			}
			break;
		}
		Canvas.keyHold[12] = true;
		Canvas.keyPressed[12] = true;
		return;
		IL_FD:
		Canvas.keyHold[13] = true;
		Canvas.keyPressed[13] = true;
		return;
		IL_110:
		Canvas.keyHold[5] = true;
		Canvas.keyPressed[5] = true;
		return;
		IL_121:
		Canvas.keyHold[2] = true;
		Canvas.keyPressed[2] = true;
		return;
		IL_132:
		Canvas.keyHold[8] = true;
		Canvas.keyPressed[8] = true;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00009E8A File Offset: 0x0000828A
	public void onKeyReleased(int keyCode)
	{
		this.mapKeyRelease(keyCode);
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00009E94 File Offset: 0x00008294
	public void mapKeyRelease(int keyCode)
	{
		switch (keyCode + 7)
		{
		case 0:
			goto IL_9A;
		case 1:
			break;
		case 2:
			goto IL_AD;
		case 3:
			Canvas.keyHold[6] = false;
			Canvas.keyReleased[6] = true;
			return;
		case 4:
			Canvas.keyHold[4] = false;
			Canvas.keyReleased[4] = true;
			return;
		case 5:
			goto IL_CF;
		case 6:
			goto IL_BE;
		default:
			if (keyCode == -39)
			{
				goto IL_CF;
			}
			if (keyCode == -38)
			{
				goto IL_BE;
			}
			if (keyCode == -22)
			{
				goto IL_9A;
			}
			if (keyCode != -21)
			{
				if (keyCode == 10)
				{
					goto IL_AD;
				}
				if (keyCode == 35)
				{
					Canvas.keyHold[11] = false;
					Canvas.keyReleased[11] = true;
					return;
				}
				if (keyCode != 42)
				{
					return;
				}
				Canvas.keyHold[10] = false;
				Canvas.keyReleased[10] = true;
				return;
			}
			break;
		}
		Canvas.keyHold[12] = false;
		Canvas.keyReleased[12] = true;
		return;
		IL_9A:
		Canvas.keyHold[13] = false;
		Canvas.keyReleased[13] = true;
		return;
		IL_AD:
		Canvas.keyHold[5] = false;
		Canvas.keyReleased[5] = true;
		return;
		IL_BE:
		Canvas.keyHold[2] = false;
		Canvas.keyReleased[2] = true;
		return;
		IL_CF:
		Canvas.keyHold[8] = false;
		Canvas.keyReleased[8] = true;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00009FA4 File Offset: 0x000083A4
	public static void initResource()
	{
		Bus.imgBus = Image.createImagePNG(T.getPath() + "/home/839");
		ChatPopup.wPop = (short)(13 * AvMain.hd);
		ChatPopup.imgPopup[0] = new FrameImage(Image.createImagePNG(T.getPath() + "/main/c"), (int)ChatPopup.wPop, (int)ChatPopup.wPop);
		ChatPopup.imgPopup[1] = new FrameImage(Image.createImagePNG(T.getPath() + "/main/cB"), (int)ChatPopup.wPop, (int)ChatPopup.wPop);
		ChatPopup.imgPopup[2] = new FrameImage(Image.createImagePNG(T.getPath() + "/main/c0"), (int)ChatPopup.wPop, (int)ChatPopup.wPop);
		ChatPopup.imgArrow[0] = Image.createImagePNG(T.getPath() + "/main/aryou0");
		ChatPopup.imgArrow[1] = Image.createImagePNG(T.getPath() + "/main/aryou1");
		MiniMap.imgSmallIcon = Image.createImage(T.getPath() + "/effect/sIc");
		DialLuckyScr.imgCau = Image.createImage(T.getPath() + "/dialLucky/c");
		DialLuckyScr.imgDo = Image.createImage(T.getPath() + "/dialLucky/sq");
		DialLuckyScr.imgDauHoi = Image.createImage(T.getPath() + "/dialLucky/q");
		DialLuckyScr.imgFireWork = new FrameImage(Image.createImage(T.getPath() + "/dialLucky/st"), 11 * AvMain.hd, 11 * AvMain.hd);
		DialLuckyScr.imgDot = Image.createImage(T.getPath() + "/dialLucky/dot");
		DialLuckyScr.imgCau_back = Image.createImage(T.getPath() + "/dialLucky/cb");
		FishingScr.imgPhao = Image.createImagePNG(T.getPath() + "/effect/cucphao");
		FishingScr.imgCa = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/ca"), 14 * AvMain.hd, 14 * AvMain.hd);
		LoadMap.imgDen = Image.createImagePNG(T.getPath() + "/effect/den");
		LoadMap.imgShadow = Image.createImage(T.getPath() + "/effect/s0");
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000A1CC File Offset: 0x000085CC
	public static bool setShowMsg()
	{
		return Canvas.currentMyScreen != LoginScr.me && Canvas.currentMyScreen != MoneyScr.instance && Canvas.currentMyScreen != PopupShop.gI() && Canvas.currentMyScreen != ListScr.instance && Canvas.currentMyScreen != RoomListOnScr.instance && Canvas.currentMyScreen != OptionScr.instance && Canvas.currentFace == null && Canvas.currentMyScreen != ParkListSrc.instance && Canvas.currentMyScreen != MyInfoScr.me && Canvas.currentDialog == null && !onMainMenu.isOngame && Canvas.currentMyScreen != SplashScr.me && !HouseScr.isDuyChuyen && !HouseScr.isTranItemBuy && !HouseScr.isBuyTileMap && Canvas.menuMain != Menu.gI();
	}

	// Token: 0x0600015E RID: 350 RVA: 0x0000A2B0 File Offset: 0x000086B0
	public static bool setShowIconMenu()
	{
		return Canvas.menuMain != Menu.gI() && (Canvas.menuMain != Menu.gI() && !HouseScr.isDuyChuyen && !HouseScr.isTranItemBuy && !HouseScr.isBuyTileMap && Canvas.currentMyScreen != SplashScr.me && Canvas.currentFace == null && !LoginScr.gI().isReg && Canvas.currentDialog == null && Canvas.currentMyScreen != ListScr.instance && (Canvas.currentMyScreen == MapScr.instance || Canvas.currentMyScreen == FarmScr.instance || (Canvas.currentMyScreen == HouseScr.me && !HouseScr.isSelectObj && !HouseScr.isChange) || Canvas.currentMyScreen == MiniMap.me || Canvas.currentMyScreen == LoginScr.me || Canvas.currentMyScreen == ServerListScr.me || Canvas.currentMyScreen == RegisterScr.instance || Canvas.currentMyScreen == RaceScr.me || !onMainMenu.isOngame));
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0000A3D8 File Offset: 0x000087D8
	public static bool isPaintIconVir()
	{
		return Canvas.currentDialog == null && Canvas.currentMyScreen != ServerListScr.me && Canvas.currentMyScreen != LoginScr.me && Canvas.currentMyScreen != PopupShop.gI() && Canvas.currentMyScreen != ListScr.instance && Canvas.currentMyScreen != MainMenu.me && Canvas.currentDialog == null && Canvas.currentMyScreen != onMainMenu.me && Canvas.currentMyScreen != MoneyScr.instance && Canvas.currentMyScreen != MiniMap.me && Canvas.currentMyScreen != OptionScr.instance && Canvas.currentMyScreen != FarmScr.instance && Canvas.currentFace == null && !HouseScr.isSelectObj && !HouseScr.isChange && GameMidlet.CLIENT_TYPE != 8 && Canvas.currentMyScreen != ParkListSrc.instance && Canvas.currentMyScreen != MyInfoScr.me && !onMainMenu.isOngame && Canvas.currentMyScreen != SplashScr.me && !HouseScr.isDuyChuyen && !HouseScr.isTranItemBuy && !HouseScr.isBuyTileMap && Canvas.menuMain != Menu.gI();
	}

	// Token: 0x06000160 RID: 352 RVA: 0x0000A51F File Offset: 0x0000891F
	public static int dx()
	{
		return Canvas.pxLast - Canvas.px;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000A52C File Offset: 0x0000892C
	public static int dy()
	{
		return Canvas.pyLast - Canvas.py;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000A539 File Offset: 0x00008939
	public static int getSecond()
	{
		return Environment.TickCount / 1000;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0000A546 File Offset: 0x00008946
	public static long getTick()
	{
		return (long)Environment.TickCount;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000A550 File Offset: 0x00008950
	public static string trim(string str)
	{
		string text = str.Substring(0, 1);
		if (text.Equals("\n") || text.Equals(" "))
		{
			str = str.Substring(1);
		}
		string text2 = str.Substring(str.Length - 1);
		if (text2.Equals(" ") || text.Equals("\n"))
		{
			str = str.Substring(0, str.Length - 1);
		}
		return str;
	}

	// Token: 0x040000D0 RID: 208
	public static Canvas instance;

	// Token: 0x040000D1 RID: 209
	public static bool bRun;

	// Token: 0x040000D2 RID: 210
	public static bool[] keyPressed = new bool[14];

	// Token: 0x040000D3 RID: 211
	public static bool[] keyReleased = new bool[14];

	// Token: 0x040000D4 RID: 212
	public static bool[] keyHold = new bool[14];

	// Token: 0x040000D5 RID: 213
	public static bool isPointerDown;

	// Token: 0x040000D6 RID: 214
	public static bool isPointerRelease;

	// Token: 0x040000D7 RID: 215
	public static bool isPointerClick;

	// Token: 0x040000D8 RID: 216
	public static int px;

	// Token: 0x040000D9 RID: 217
	public static int py;

	// Token: 0x040000DA RID: 218
	public static int pxLast;

	// Token: 0x040000DB RID: 219
	public static int pyLast;

	// Token: 0x040000DC RID: 220
	public static int gameTick;

	// Token: 0x040000DD RID: 221
	public static int w = 0;

	// Token: 0x040000DE RID: 222
	public static int h;

	// Token: 0x040000DF RID: 223
	public static int hw;

	// Token: 0x040000E0 RID: 224
	public static int hh;

	// Token: 0x040000E1 RID: 225
	public static int rw;

	// Token: 0x040000E2 RID: 226
	public static int rh;

	// Token: 0x040000E3 RID: 227
	public static int hCan;

	// Token: 0x040000E4 RID: 228
	public static MyScreen currentMyScreen;

	// Token: 0x040000E5 RID: 229
	public static MsgDlg msgdlg;

	// Token: 0x040000E6 RID: 230
	public static MenuMain menuMain;

	// Token: 0x040000E7 RID: 231
	public static InputDlg inputDlg;

	// Token: 0x040000E8 RID: 232
	public static Dialog currentDialog;

	// Token: 0x040000E9 RID: 233
	public static MyVector currentPopup;

	// Token: 0x040000EA RID: 234
	public static int count0;

	// Token: 0x040000EB RID: 235
	public static AvatarData avataData;

	// Token: 0x040000EC RID: 236
	public static LoadMap loadMap;

	// Token: 0x040000ED RID: 237
	public static CameraList cameraList;

	// Token: 0x040000EE RID: 238
	public static Face currentFace;

	// Token: 0x040000EF RID: 239
	public static MyVector currentEffect = new MyVector();

	// Token: 0x040000F0 RID: 240
	private static long[] timeBB;

	// Token: 0x040000F1 RID: 241
	public static MyVector listInfoSV = new MyVector();

	// Token: 0x040000F2 RID: 242
	public static bool isVirHorizontal;

	// Token: 0x040000F3 RID: 243
	public static bool isInitChar;

	// Token: 0x040000F4 RID: 244
	public static bool isKeyBoard = false;

	// Token: 0x040000F5 RID: 245
	public static bool isDoubleImage = true;

	// Token: 0x040000F6 RID: 246
	public static int load = -1;

	// Token: 0x040000F7 RID: 247
	public static FontX normalFont;

	// Token: 0x040000F8 RID: 248
	public static FontX normalWhiteFont;

	// Token: 0x040000F9 RID: 249
	public static FontX borderFont;

	// Token: 0x040000FA RID: 250
	public static FontX arialFont;

	// Token: 0x040000FB RID: 251
	public static FontX blackF;

	// Token: 0x040000FC RID: 252
	public static FontX numberFont;

	// Token: 0x040000FD RID: 253
	public static FontX smallFontRed;

	// Token: 0x040000FE RID: 254
	public static FontX smallFontYellow;

	// Token: 0x040000FF RID: 255
	public static FontX menuFont;

	// Token: 0x04000100 RID: 256
	public static FontX tempFont;

	// Token: 0x04000101 RID: 257
	public static FontX smallWhite;

	// Token: 0x04000102 RID: 258
	public static FontX fontChat;

	// Token: 0x04000103 RID: 259
	public static FontX fontChatB;

	// Token: 0x04000104 RID: 260
	public static FontX fontBlu;

	// Token: 0x04000105 RID: 261
	public static FontX fontWhiteBold;

	// Token: 0x04000106 RID: 262
	public static IPaint paint;

	// Token: 0x04000107 RID: 263
	public static int hTab = 0;

	// Token: 0x04000108 RID: 264
	public static int transTab = 0;

	// Token: 0x04000109 RID: 265
	public static int iOpenOngame;

	// Token: 0x0400010A RID: 266
	public static int xTran;

	// Token: 0x0400010B RID: 267
	public static int hKeyBoard;

	// Token: 0x0400010C RID: 268
	public static int tran18;

	// Token: 0x0400010D RID: 269
	public static Welcome welcome;

	// Token: 0x0400010E RID: 270
	public static MyVector listAc = new MyVector();

	// Token: 0x0400010F RID: 271
	public static string pass;

	// Token: 0x04000110 RID: 272
	public static string user;

	// Token: 0x04000111 RID: 273
	public static T t;

	// Token: 0x04000112 RID: 274
	public static int timeNameSV = -1;

	// Token: 0x04000113 RID: 275
	public static string nameSV = string.Empty;

	// Token: 0x04000114 RID: 276
	public static Image imagePlug;

	// Token: 0x04000115 RID: 277
	public static bool isPaint18 = false;

	// Token: 0x04000116 RID: 278
	public static int stypeInt = 1;

	// Token: 0x04000117 RID: 279
	public static string text = string.Empty;

	// Token: 0x04000118 RID: 280
	private static bool isStart = false;

	// Token: 0x04000119 RID: 281
	public static float disStart = 0f;

	// Token: 0x0400011A RID: 282
	public static float disStartZoom;

	// Token: 0x0400011B RID: 283
	public static float temp;

	// Token: 0x0400011C RID: 284
	public static float xZoom;

	// Token: 0x0400011D RID: 285
	public static float yZoom;

	// Token: 0x0400011E RID: 286
	public static bool isZoom = false;

	// Token: 0x0400011F RID: 287
	public static sbyte dirZoom = 1;

	// Token: 0x04000120 RID: 288
	public static int isRotateTop = 0;

	// Token: 0x04000121 RID: 289
	public static int iOpenBoard = -1;

	// Token: 0x04000122 RID: 290
	public static bool aTran = false;

	// Token: 0x04000123 RID: 291
	public static Image imgTabInfo;

	// Token: 0x04000124 RID: 292
	public static int countTab = 30;

	// Token: 0x04000125 RID: 293
	private long lastTimePress;

	// Token: 0x04000126 RID: 294
	public static MyVector flyTexts = new MyVector();

	// Token: 0x04000127 RID: 295
	private float xTouch;

	// Token: 0x04000128 RID: 296
	private float yTouch;

	// Token: 0x04000129 RID: 297
	public static string test = string.Empty;

	// Token: 0x0400012A RID: 298
	public static string test1 = string.Empty;

	// Token: 0x0400012B RID: 299
	public static string test2 = string.Empty;

	// Token: 0x0400012C RID: 300
	private int countDown;

	// Token: 0x0400012D RID: 301
	private int num;

	// Token: 0x0400012E RID: 302
	public static MyVector listPoint;

	// Token: 0x0400012F RID: 303
	public static Command cmdEndDlg;

	// Token: 0x04000130 RID: 304
	public static AvPosition[] posCmd = new AvPosition[3];

	// Token: 0x04000131 RID: 305
	public static AvPosition posByteCOunt;

	// Token: 0x02000021 RID: 33
	private class IActionOk : IAction
	{
		// Token: 0x06000167 RID: 359 RVA: 0x0000A6D9 File Offset: 0x00008AD9
		public void perform()
		{
			Canvas.endDlg();
		}
	}
}
