using System;
using UnityEngine;

// Token: 0x02000130 RID: 304
public class MapScr : MyScreen, IChatable
{
	// Token: 0x06000867 RID: 2151 RVA: 0x00051B3B File Offset: 0x0004FF3B
	public MapScr()
	{
		this.initCmd();
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x00051B71 File Offset: 0x0004FF71
	public static MapScr gI()
	{
		if (MapScr.instance == null)
		{
			MapScr.instance = new MapScr();
		}
		return MapScr.instance;
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00051B8C File Offset: 0x0004FF8C
	public void initCmd()
	{
		this.cmdMenu = new Command(T.menu, 0);
		this.cmdEvent = new Command(T.eventt, 1, this);
		this.cmdGoToMap = new Command(T.exit, 0, this);
		this.cmdExchangeBoss = new Command(T.exchange, 2);
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00051BDF File Offset: 0x0004FFDF
	public override void switchToMe()
	{
		base.switchToMe();
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00051BE8 File Offset: 0x0004FFE8
	public override void commandActionPointer(int index, int subIndex)
	{
		switch (index + 1)
		{
		case 1:
			this.doExit();
			break;
		case 2:
			this.doEvent();
			break;
		case 3:
			this.exitGame();
			break;
		case 4:
			this.doSelectedMiniMap();
			break;
		case 5:
		{
			MyVector myVector = new MyVector();
			if (MapScr.listCmdRotate.size() > 0)
			{
				for (int i = 0; i < MapScr.listCmdRotate.size(); i++)
				{
					StringObj stringObj = (StringObj)MapScr.listCmdRotate.elementAt(i);
					if (stringObj.type == 0)
					{
						myVector.addElement(new Command(stringObj.str, new MapScr.IActionExchange(stringObj)));
					}
				}
				MenuCenter.gI().startAt(myVector);
			}
			break;
		}
		case 6:
			MapScr.isOpenInfo = true;
			ParkService.gI().doRequestYourInfo(GameMidlet.avatar.IDDB);
			break;
		case 7:
			MainMenu.gI().isWearing = true;
			GlobalService.gI().doRequestContainer(GameMidlet.avatar.IDDB);
			break;
		case 8:
			ListScr.gI().setFriendList(false);
			break;
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00051D20 File Offset: 0x00050120
	public override void doMenu()
	{
		MyVector myVector = new MyVector();
		if (MapScr.listCmd != null && MapScr.listCmd.size() > 0)
		{
			for (int i = 0; i < MapScr.listCmd.size(); i++)
			{
				int i2 = i;
				StringObj stringObj = (StringObj)MapScr.listCmd.elementAt(i);
				myVector.addElement(new Command(stringObj.str, new MapScr.IActionMap(i2, 0, stringObj))
				{
					indexImage = 0
				});
			}
		}
		int num = 0;
		if (MapScr.listCmdRotate.size() > 0)
		{
			for (int j = 0; j < MapScr.listCmdRotate.size(); j++)
			{
				StringObj stringObj2 = (StringObj)MapScr.listCmdRotate.elementAt(j);
				if (stringObj2.type == 0)
				{
					num++;
				}
			}
		}
		if (num > 0)
		{
			myVector.addElement(new Command(T.game, 4, this)
			{
				indexImage = 1
			});
		}
		myVector.addElement(new Command(T.info, 5, this)
		{
			indexImage = 2
		});
		myVector.addElement(new Command(T.basket, 6, this)
		{
			indexImage = 3
		});
		myVector.addElement(new Command(T.friend, 7, this)
		{
			indexImage = 4
		});
		myVector.addElement(new Command(T.map, 0, this)
		{
			indexImage = 5
		});
		MenuLeft.gI().startAt(myVector);
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x00051EA0 File Offset: 0x000502A0
	public override void close()
	{
		this.doExit();
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00051EA8 File Offset: 0x000502A8
	public void doExit()
	{
		Canvas.startWaitDlg();
		MapScr.typeJoin = -1;
		MapScr.typeCasino = -1;
		if (GameMidlet.CLIENT_TYPE == 8)
		{
			this.joinCitymap();
		}
		else
		{
			GlobalService.gI().getHandler(8);
		}
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x00051EDC File Offset: 0x000502DC
	public override void initZoom()
	{
		AvCamera.gI().init((int)MapScr.roomID + 1);
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00051EF0 File Offset: 0x000502F0
	public void doEvent()
	{
		MessageScr.gI().switchToMe();
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x00051EFC File Offset: 0x000502FC
	public void doHit()
	{
		if (MapScr.focusP == null)
		{
			return;
		}
		MapScr.doGivingDefferent(100);
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00051F10 File Offset: 0x00050310
	public void doInviteToMyHome()
	{
		if (MapScr.focusP != null)
		{
			ParkService.gI().doInviteToMyHome(0, MapScr.focusP.IDDB);
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x00051F34 File Offset: 0x00050334
	public void onInviteToMyHome(sbyte type2, int idUser2)
	{
		Canvas.endDlg();
		Avatar avatar = LoadMap.getAvatar(idUser2);
		if (avatar == null)
		{
			return;
		}
		if ((int)type2 == 0)
		{
			Canvas.startOKDlg(T.youAreInvite + avatar.name + ". " + T.doYouWant, new MapScr.IActionInviteHouse(idUser2));
		}
		else if ((int)type2 == 1)
		{
			MapScr.idHouse = idUser2;
			GlobalService.gI().getHandler(11);
			Canvas.startWaitDlg();
		}
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x00051FA4 File Offset: 0x000503A4
	public static void doAction(sbyte k)
	{
		GameMidlet.avatar.doAction(k);
		AvatarService.gI().doFeel((int)k);
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x00051FBD File Offset: 0x000503BD
	public static void doSellectFeel(int focus)
	{
		GameMidlet.avatar.setFeel(focus);
		GameMidlet.avatar.firFeel = GameMidlet.avatar.feel;
		GameMidlet.avatar.numFeel = 0;
		AvatarService.gI().doFeel(focus + 100);
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x00051FF8 File Offset: 0x000503F8
	public void onFeel(int idUser, sbyte idFeel)
	{
		Avatar avatar = LoadMap.getAvatar(idUser);
		if (avatar != null)
		{
			if ((int)idFeel >= 100)
			{
				avatar.setFeel((int)idFeel - 100);
				avatar.firFeel = avatar.feel;
				avatar.numFeel = 0;
			}
			else
			{
				avatar.doAction(idFeel);
			}
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x00052045 File Offset: 0x00050445
	protected void doClose()
	{
		this.right = null;
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x00052050 File Offset: 0x00050450
	public void doSellectAction(int x0, int y0)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < 4; i++)
		{
			myVector.addElement(new MapScr.CommandAction(T.actionStr[i], new MapScr.IActionSelectAction(MapScr.ac[i]), 7 + i));
		}
		MenuIcon.gI().setInfo(myVector, x0, y0);
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x000520A4 File Offset: 0x000504A4
	public void doFeel(int x0, int y0)
	{
		sbyte[] array = new sbyte[]
		{
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11
		};
		MyVector myVector = new MyVector();
		for (int i = 0; i < array.Length; i++)
		{
			int num = i;
			myVector.addElement(new MapScr.CommandFeel(string.Empty, new MapScr.IActionFeel(num, array[num]), array[num]));
		}
		MenuIcon.gI().setInfo(myVector, x0, y0);
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x00052108 File Offset: 0x00050508
	public override void update()
	{
		Canvas.loadMap.update();
		this.updateFocus();
		this.updateWedding();
		if (MapScr.listFish.size() > 0)
		{
			for (int i = 0; i < MapScr.listFish.size(); i++)
			{
				Fish fish = (Fish)MapScr.listFish.elementAt(i);
				fish.update();
			}
		}
		this.updateChat();
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x00052174 File Offset: 0x00050574
	public void resetWedding()
	{
		MapScr.isWedding = false;
		this.iGoChaSu = 0;
		for (int i = 0; i < LoadMap.playerLists.size(); i++)
		{
			MyObject myObject = (MyObject)LoadMap.playerLists.elementAt(i);
			if ((int)myObject.catagory == 0)
			{
				Avatar avatar = (Avatar)myObject;
				avatar.v = 4;
			}
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x000521D4 File Offset: 0x000505D4
	private void updateWedding()
	{
		if (MapScr.isWedding)
		{
			if ((int)this.iGoChaSu == 1 && Canvas.load == -1)
			{
				Out.println("updateWedding1111111111111: " + this.iGoChaSu);
				this.iGoChaSu = 2;
				Avatar avatar = LoadMap.getAvatar(-100);
				Avatar avatar2 = LoadMap.getAvatar(MapScr.idUserWedding_1);
				Avatar avatar3 = LoadMap.getAvatar(MapScr.idUserWedding_2);
				if (avatar2 != null && avatar3 != null)
				{
					AvCamera.gI().followPlayer = avatar;
					avatar.addChat(150, string.Concat(new string[]
					{
						"Lễ cưới của chú rể ",
						avatar2.name,
						" và cô dâu ",
						avatar3.name,
						" chính thức bắt đầu.  mời Cô Dâu và Chú rể cùng tiến về lễ đường."
					}), 1);
				}
				else
				{
					this.resetWedding();
				}
			}
			if ((int)this.iGoChaSu == 2 && Canvas.gameTick % 4 == 2)
			{
				Avatar avatar4 = LoadMap.getAvatar(-100);
				if (avatar4.chat == null)
				{
					this.iGoChaSu = 3;
					Avatar avatar5 = LoadMap.getAvatar(MapScr.idUserWedding_1);
					Avatar avatar6 = LoadMap.getAvatar(MapScr.idUserWedding_2);
					if (avatar5 != null && avatar6 != null)
					{
						avatar6.xCur = 26 * LoadMap.w - LoadMap.w;
						avatar6.task = -5;
						avatar5.xCur = 26 * LoadMap.w - LoadMap.w * 2;
						avatar5.task = -5;
						AvCamera.gI().followPlayer = avatar5;
					}
					else
					{
						this.resetWedding();
					}
				}
			}
			if ((int)this.iGoChaSu == 3)
			{
				Avatar avatar7 = LoadMap.getAvatar(MapScr.idUserWedding_1);
				Avatar avatar8 = LoadMap.getAvatar(MapScr.idUserWedding_2);
				if (avatar7 != null && avatar8 != null && avatar7.task == 0 && avatar8.task == 0)
				{
					this.iGoChaSu = 4;
					Avatar avatar9 = LoadMap.getAvatar(-100);
					AvCamera.gI().followPlayer = avatar9;
					avatar9.addChat(200, "Hôm nay chúng ta có mặt tại đây để cùng chúc mừng cho đám cưới của chú rể " + avatar7.name + " và cô dâu " + avatar8.name, 1);
					avatar9.addChat(200, "Vượt qua bao khó khăn thử thách bằng tình yêu vĩnh cửu, sau cùng cả hai đã đạt đến bến bờ hạnh phúc.", 1);
					avatar9.addChat(150, "Kể từ hôm nay, ta tuyên bố 2 bạn chính thức trở thành vợ chồng.", 1);
					avatar9.addChat(100, "Chú rể có thể hôn Cô Dâu.", 1);
				}
			}
			if ((int)this.iGoChaSu == 4)
			{
				Avatar avatar10 = LoadMap.getAvatar(MapScr.idUserWedding_1);
				Avatar avatar11 = LoadMap.getAvatar(MapScr.idUserWedding_2);
				avatar10.v = 4;
				avatar11.v = 4;
				Avatar avatar12 = LoadMap.getAvatar(-100);
				if (avatar12.chat == null && avatar12.listChat.size() == 0)
				{
					if (MapScr.idUserWedding_1 == GameMidlet.avatar.IDDB)
					{
						ParkService.gI().doGivingDeferrent(MapScr.idUserWedding_2, 101);
					}
					this.countWeeding = 0;
					this.iGoChaSu = 5;
				}
			}
		}
		if ((int)this.iGoChaSu == 5 && (int)this.countWeeding >= 0)
		{
			this.countWeeding = (sbyte)((int)this.countWeeding + 1);
			if ((int)this.countWeeding > 20)
			{
				if ((int)this.countWeeding == 21)
				{
					AnimateEffect animateEffect = new AnimateEffect(2, true, 0);
					animateEffect.show();
					AvCamera.gI().followPlayer = GameMidlet.avatar;
					GameMidlet.avatar.v = 4;
				}
				if (GameMidlet.avatar.IDDB != MapScr.idUserWedding_1)
				{
					MapScr.isWedding = false;
					this.countWeeding = -1;
				}
				if (GameMidlet.avatar.task == 0 && GameMidlet.avatar.IDDB == MapScr.idUserWedding_1)
				{
					MapScr.isWedding = false;
					Avatar avatar13 = LoadMap.getAvatar(MapScr.idUserWedding_1);
					Avatar avatar14 = LoadMap.getAvatar(MapScr.idUserWedding_2);
					if (avatar13 != null && avatar14 != null)
					{
						avatar13.v = 4;
						avatar14.v = 4;
					}
					this.iGoChaSu = 6;
					this.countWeeding = -1;
					ParkService.gI().doGivingDeferrent(MapScr.idUserWedding_2, 102);
				}
			}
		}
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x000525B8 File Offset: 0x000509B8
	private void updateFocus()
	{
		if (Canvas.stypeInt == 0 && LoadMap.focusObj != null)
		{
			if (MapScr.focusP != null && (int)LoadMap.focusObj.catagory != 5 && MapScr.focusP.IDDB > 2000000000)
			{
				this.center = this.cmdExchangeBoss;
			}
			else
			{
				this.center = null;
			}
			this.right = LoadMap.cmdNext;
			if ((int)LoadMap.focusObj.catagory == 0)
			{
				this.right.caption = ((Avatar)LoadMap.focusObj).name;
				if (this.right.caption.Length > 8)
				{
					this.right.caption = this.right.caption.Substring(0, 8) + "..";
				}
			}
		}
		if (LoadMap.focusObj == null && this.right == LoadMap.cmdNext)
		{
			this.right = null;
			this.center = null;
		}
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x000526BA File Offset: 0x00050ABA
	public override void updateKey()
	{
		if (Canvas.welcome == null || !Welcome.isPaintArrow)
		{
			base.updateKey();
		}
		Canvas.loadMap.updateKey();
		GameMidlet.avatar.updateKey();
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x000526EA File Offset: 0x00050AEA
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		if (Canvas.welcome == null || !Welcome.isPaintArrow)
		{
			base.paint(g);
		}
		Canvas.paintPlus(g);
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00052714 File Offset: 0x00050B14
	public void paintNameCasino(MyGraphics g)
	{
		int num = T.nameCasino.Length;
		if (Canvas.iOpenOngame == 2)
		{
			num--;
		}
		for (int i = 0; i < num; i++)
		{
			Canvas.smallFontRed.drawString(g, T.nameCasino[i], (179 + ((Canvas.iOpenOngame == 0) ? 0 : 24) + ((Canvas.iOpenOngame != 2) ? 0 : 24) + i * 48 + 48) * AvMain.hd, 60 * AvMain.hd - 5, 2);
		}
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x000527A4 File Offset: 0x00050BA4
	public override void paintMain(MyGraphics g)
	{
		GUIUtility.ScaleAroundPivot(new Vector2(AvMain.zoom, AvMain.zoom), Vector2.zero);
		Canvas.resetTrans(g);
		Canvas.loadMap.paint(g);
		GUIUtility.ScaleAroundPivot(new Vector2(1f / AvMain.zoom, 1f / AvMain.zoom), Vector2.zero);
		if (MapScr.listFish.size() > 0)
		{
			for (int i = 0; i < MapScr.listFish.size(); i++)
			{
				Fish fish = (Fish)MapScr.listFish.elementAt(i);
				fish.paint(g);
			}
		}
		GUIUtility.ScaleAroundPivot(new Vector2(AvMain.zoom, AvMain.zoom), Vector2.zero);
		if (LoadMap.TYPEMAP == 108 || LoadMap.TYPEMAP == 109 || (LoadMap.idTileImg != -1 && LoadMap.isCasino))
		{
			this.paintNameCasino(g);
		}
		Canvas.loadMap.paintObject(g);
		this.paintChat(g);
		Canvas.resetTrans(g);
		GUIUtility.ScaleAroundPivot(new Vector2(1f / AvMain.zoom, 1f / AvMain.zoom), Vector2.zero);
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x000528D0 File Offset: 0x00050CD0
	public override void commandTab(int index)
	{
		if (index != 2)
		{
			if (index != 3)
			{
				if (index == 52)
				{
					if (LoadMap.TYPEMAP == -1)
					{
						Canvas.startWaitDlg();
						GlobalService.gI().getHandler(8);
					}
				}
			}
		}
		else
		{
			GlobalService.gI().doCommunicate(MapScr.focusP.IDDB);
		}
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00052936 File Offset: 0x00050D36
	public static void loadAvatar()
	{
		if (onMainMenu.isOngame)
		{
			onMainMenu.resetImg();
			onMainMenu.resetSize();
			Canvas.paint.resetOngame();
			Canvas.paint.resetCasino();
			Canvas.paint.loadImgAvatar();
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0005296C File Offset: 0x00050D6C
	public void onJoinPark(sbyte roomID1, sbyte boardID1, short x, short y, MyVector listPlayer, MyVector mapItemType1, MyVector mapItem1)
	{
		if ((int)boardID1 == -1)
		{
			Canvas.startOK(T.areaIsFull, 52);
			return;
		}
		if (LoadMap.idTileImg == -1)
		{
			LoadMap.mapItemType = mapItemType1;
			LoadMap.mapItem = mapItem1;
		}
		Canvas.paint.setVirtualKeyFish((int)roomID1);
		Canvas.clearKeyReleased();
		MapScr.roomID = roomID1;
		MapScr.boardID = boardID1;
		LoadMap.focusObj = (MapScr.focusP = null);
		GameMidlet.avatar.task = 0;
		MapScr.loadAvatar();
		if (LoadMap.imgMap == null || Canvas.isInitChar || (int)roomID1 != LoadMap.TYPEMAP || (LoadMap.idTileImg == -1 && (LoadMap.TYPEMAP == 14 || LoadMap.TYPEMAP == 15 || LoadMap.TYPEMAP == 16)))
		{
			GameMidlet.avatar.ableShow = false;
			if ((LoadMap.rememMap != -1 && (int)roomID1 == LoadMap.TYPEMAP) || x == 0 || y != 0)
			{
			}
			GameMidlet.avatar.v = 4;
			Canvas.loadMap.load((int)roomID1 + 1, true);
			AvCamera.gI().setPosFollowPlayer();
		}
		else
		{
			MapScr.listFish.removeAllElements();
			LoadMap.playerLists.removeAllElements();
			LoadMap.dynamicLists.removeAllElements();
			LoadMap.addPlayer(GameMidlet.avatar);
			if (Canvas.load == 0)
			{
				Canvas.load = 1;
			}
			AvCamera.gI().setPosFollowPlayer();
		}
		if (onMainMenu.isOngame && LoadMap.xJoinCasino != -1)
		{
			GameMidlet.avatar.setPos(LoadMap.xJoinCasino, LoadMap.yJoinCasino);
			LoadMap.xJoinCasino = (LoadMap.yJoinCasino = -1);
		}
		if (mapItemType1 != null)
		{
			Canvas.loadMap.setMapItemType();
		}
		if (Canvas.currentMyScreen != this)
		{
			this.switchToMe();
		}
		if (LoadMap.xDichChuyen != -1)
		{
			GameMidlet.avatar.x = LoadMap.xDichChuyen;
			GameMidlet.avatar.y = LoadMap.yDichChuyen;
			LoadMap.xDichChuyen = (LoadMap.yDichChuyen = -1);
			this.doMove(GameMidlet.avatar.x, GameMidlet.avatar.y, (int)GameMidlet.avatar.direct);
		}
		for (int i = 0; i < listPlayer.size(); i++)
		{
			MyObject myObject = (MyObject)listPlayer.elementAt(i);
			if ((int)myObject.catagory == 0)
			{
				Avatar avatar = (Avatar)myObject;
				avatar.xCur = avatar.x;
				avatar.yCur = avatar.y;
				avatar.dirFirst = avatar.direct;
				avatar.orderSeriesPath();
				if (avatar.IDDB != GameMidlet.avatar.IDDB)
				{
					this.setGender(avatar);
					LoadMap.addPlayer(avatar);
				}
			}
			else if ((int)myObject.catagory == 5)
			{
				Drop_Part drop_Part = (Drop_Part)myObject;
				drop_Part.x0 = drop_Part.x;
				drop_Part.y0 = drop_Part.y;
				LoadMap.playerLists.addElement(drop_Part);
			}
		}
		if (Bus.isRun)
		{
			this.doMove(Bus.posBusStop.x, Bus.posBusStop.y, (int)GameMidlet.avatar.direct);
		}
		else
		{
			GameMidlet.avatar.y++;
			this.move();
		}
		MapScr.doSellectFeel((int)GameMidlet.avatar.feel);
		if (LoadMap.TYPEMAP == 108)
		{
			AvCamera.gI().notTrans();
		}
		if (Canvas.stypeInt == 0)
		{
			this.left = this.cmdMenu;
		}
		MapScr.focusP = null;
		onMainMenu.isOngame = false;
		Canvas.currentFace = null;
		Canvas.instance.setSize((int)ScaleGUI.WIDTH, (int)ScaleGUI.HEIGHT);
		if (Canvas.isInitChar)
		{
			if (LoadMap.TYPEMAP == 9 && Welcome.indexMapScr != 0)
			{
				Canvas.welcome = new Welcome();
				Canvas.welcome.initMapScr();
			}
			else if (!Bus.isRun && LoadMap.TYPEMAP == 23)
			{
				Canvas.welcome = new Welcome();
				Canvas.welcome.initKhuMuaSam();
			}
			else if (LoadMap.TYPEMAP == 25 && Welcome.indexFarmPath > 0)
			{
				Canvas.welcome = new Welcome();
				Canvas.welcome.initFarmPath(MapScr.instance);
			}
			this.left = null;
			this.center = null;
		}
		else
		{
			AvCamera.gI().setPosFollowPlayer();
			AvCamera.gI().xCam = AvCamera.gI().xTo;
			if (AvCamera.gI().xCam > AvCamera.gI().xLimit)
			{
				AvCamera.gI().xCam = (AvCamera.gI().xTo = AvCamera.gI().xLimit);
			}
		}
		SoundManager.stop();
		if (LoadMap.TYPEMAP == 13)
		{
			SoundManager.playSoundBG(81);
		}
		Canvas.endDlg();
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x00052E0C File Offset: 0x0005120C
	public void doGetHandlerCasino(int i)
	{
		Canvas.startWaitDlg();
		this.move();
		GlobalService.gI().getHandler(3);
		MapScr.typeCasino = (sbyte)i;
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00052E2B File Offset: 0x0005122B
	public void doJoinCasino()
	{
		ParkService.gI().doJoinPark(10, -1);
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x00052E3C File Offset: 0x0005123C
	public void onJoinCasino()
	{
		int gameType = 0;
		switch (MapScr.typeCasino)
		{
		case 0:
			if (onMainMenu.isOngame || Canvas.iOpenOngame == 0)
			{
				gameType = 3;
			}
			else
			{
				gameType = 21;
			}
			break;
		case 1:
			if (onMainMenu.isOngame || Canvas.iOpenOngame == 0)
			{
				gameType = 7;
			}
			else
			{
				gameType = 22;
			}
			break;
		case 2:
			gameType = 21;
			break;
		case 3:
			gameType = 22;
			break;
		}
		GlobalService.gI().setGameType(gameType);
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x00052ECD File Offset: 0x000512CD
	public void doJoinShop(sbyte type)
	{
		if ((int)MapScr.typeJoin != -1)
		{
			return;
		}
		this.move();
		Canvas.startWaitDlg();
		MapScr.typeJoin = type;
		GlobalService.gI().getHandler(8);
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00052EF8 File Offset: 0x000512F8
	public void doMove(int x, int y, int direct)
	{
		if ((GameMidlet.CLIENT_TYPE != 9 && GameMidlet.CLIENT_TYPE != 11) || MapScr.isWedding)
		{
			return;
		}
		GameMidlet.avatar.xCur = x;
		GameMidlet.avatar.yCur = y;
		ParkService.gI().doMove(x, y, direct);
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00052F4B File Offset: 0x0005134B
	public void move()
	{
		this.doMove(GameMidlet.avatar.x, GameMidlet.avatar.y, (int)GameMidlet.avatar.direct);
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00052F74 File Offset: 0x00051374
	public void onMovePark(int id, int xM, int yM, int direct)
	{
		Avatar avatar = LoadMap.getAvatar(id);
		if (id == GameMidlet.avatar.IDDB || MapScr.isWedding)
		{
			return;
		}
		if (avatar != null)
		{
			if (avatar.ableShow && (avatar.task == 0 || Canvas.currentMyScreen == MainMenu.gI() || Canvas.currentMyScreen == ListScr.gI()))
			{
				avatar.ableShow = false;
				avatar.setPos(xM, yM);
			}
			if ((int)avatar.action == -3)
			{
				avatar.action = 0;
			}
			avatar.isJumps = -1;
			if (avatar.task == 0)
			{
				avatar.moveList.addElement(new AvPosition(xM, yM, direct));
			}
		}
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x00053028 File Offset: 0x00051428
	public void onPlayerJoinPark(Avatar player)
	{
		player.x = GameMidlet.avatar.x;
		player.y = GameMidlet.avatar.y;
		this.setGender(player);
		player.orderSeriesPath();
		player.ableShow = true;
		Avatar avatar = LoadMap.getAvatar(player.IDDB);
		if (avatar != null)
		{
			LoadMap.playerLists.removeElement(avatar);
		}
		LoadMap.addPlayer(player);
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0005308C File Offset: 0x0005148C
	public void setGender(Avatar player)
	{
		APartInfo partByZ = AvatarData.getPartByZ(player.seriPart, 50);
		if (partByZ != null)
		{
			player.gender = partByZ.gender;
		}
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x000530BC File Offset: 0x000514BC
	public void onPlayerLeave(int id)
	{
		Avatar avatar = LoadMap.getAvatar(id);
		if (avatar != null)
		{
			avatar.resetTypeChair();
			avatar.isLeave = true;
			Fish fish = FishingScr.getFish(id);
			if (fish != null)
			{
				MapScr.listFish.removeElement(fish);
			}
		}
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x000530FB File Offset: 0x000514FB
	public override void keyPress(int keyCode)
	{
		ChatTextField.gI().startChat(keyCode, this);
		base.keyPress(keyCode);
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00053110 File Offset: 0x00051510
	public void onChatFromMe(string text)
	{
		if (text.Trim().Equals(string.Empty))
		{
			return;
		}
		if (text.IndexOf("dmw") != -1)
		{
			if (MapScr.focusP != null)
			{
				GlobalService.gI().doServerKick(MapScr.focusP.IDDB, text);
			}
			return;
		}
		if (text.IndexOf("ptw") == 0 && MapScr.focusP != null && MapScr.focusP.chat != null && MapScr.focusP.chat.chats != null)
		{
			string text2 = text + " (";
			for (int i = 0; i < MapScr.focusP.chat.chats.Length; i++)
			{
				text2 = text2 + " " + MapScr.focusP.chat.chats[i];
			}
			text2 += ").";
			GlobalService.gI().doServerKick(MapScr.focusP.IDDB, text2);
			return;
		}
		this.onChatFrom(GameMidlet.avatar.IDDB, text);
		ParkService.gI().chatToBoard(text);
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x0005322C File Offset: 0x0005162C
	public void onChatFrom(int idFrom, string text)
	{
		if (LoadMap.TYPEMAP == 24 || LoadMap.TYPEMAP == 53)
		{
			return;
		}
		Avatar avatar = LoadMap.getAvatar(idFrom);
		if (avatar == null)
		{
			return;
		}
		if (avatar.chat == null)
		{
			avatar.chat = new ChatPopup(100, text, (idFrom < 2000000000) ? 0 : 1);
			avatar.chat.setPos(avatar.x, avatar.y - 45);
		}
		else
		{
			avatar.chat.prepareData(100, text);
		}
		if (idFrom == GameMidlet.avatar.idTo)
		{
			Canvas.text = "done";
		}
		if (idFrom < 2000000000)
		{
			MessageScr.gI().addText(avatar.name, text);
		}
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x000532EF File Offset: 0x000516EF
	public void doKiss()
	{
		if (MapScr.focusP == null || MapScr.focusP.task != 0)
		{
			return;
		}
		ParkService.gI().doGivingDeferrent(MapScr.focusP.IDDB, 101);
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00053324 File Offset: 0x00051724
	public void doGiving(int ID)
	{
		if (MapScr.focusP == null)
		{
			return;
		}
		APartInfo apartInfo = (APartInfo)AvatarData.getPart((short)ID);
		Canvas.getTypeMoney(apartInfo.price[0], apartInfo.price[1], new MapScr.IActionGiving(apartInfo, 1), new MapScr.IActionGiving(apartInfo, 2), null);
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0005336D File Offset: 0x0005176D
	protected static void doGivingDefferent(int id)
	{
		if (MapScr.focusP != null)
		{
			ParkService.gI().doGivingDeferrent(MapScr.focusP.IDDB, id);
		}
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0005338E File Offset: 0x0005178E
	public void onGivingDefferent(int idFrom1, int idTo, int idGift1, string text, int time)
	{
		if (idGift1 == -1)
		{
			Canvas.startOKDlg(text);
			return;
		}
		this.translates(1, idFrom1, idTo, idGift1, time);
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x000533AB File Offset: 0x000517AB
	public void onGiftGiving(int idFrom, int idTo, int idGift, string des, int curMoney, int typeBuy, int xu, int luong, int luongK)
	{
		if (idGift == -1)
		{
			Canvas.startOKDlg(des);
			return;
		}
		if (idFrom == GameMidlet.avatar.IDDB)
		{
			GameMidlet.avatar.updateMoney(xu, luong, luongK);
		}
		this.translates(0, idFrom, idTo, idGift, 0);
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x000533E8 File Offset: 0x000517E8
	public void hit(Avatar ava1, Avatar ava2)
	{
		if (ava2.task != 0)
		{
			return;
		}
		ava1.task = -2;
		ava2.task = -2;
		ava1.moveList.removeAllElements();
		ava2.moveList.removeAllElements();
		ava1.focus = ava2;
		ava1.setPosTo(ava2.x, ava2.y + 5);
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00053444 File Offset: 0x00051844
	public void kiss(Avatar ava1, Avatar ava2)
	{
		if (ava2.task != 0)
		{
			return;
		}
		ava1.task = 11;
		ava2.task = 11;
		ava1.moveList.removeAllElements();
		ava2.moveList.removeAllElements();
		ava1.focus = ava2;
		if (ava1.x < ava2.x)
		{
			ava1.setPosTo(ava2.x - 20, ava2.y + 2);
		}
		else
		{
			ava1.setPosTo(ava2.x + 20, ava2.y + 2);
		}
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x000534D0 File Offset: 0x000518D0
	public void translates(int i, int idFrom, int idTo, int idGift, int time)
	{
		Out.println("translates: " + idGift);
		Avatar avatar = LoadMap.getAvatar(idFrom);
		Avatar avatar2 = LoadMap.getAvatar(idTo);
		if (avatar == null || avatar2 == null)
		{
			return;
		}
		if (avatar.task != 0 || avatar2.task != 0)
		{
			return;
		}
		avatar.idTo = avatar2.IDDB;
		avatar.idFrom = avatar.IDDB;
		avatar2.idFrom = avatar.IDDB;
		avatar2.idTo = avatar2.IDDB;
		if (idFrom == GameMidlet.avatar.IDDB)
		{
			GameMidlet.avatar.yCur = avatar2.y;
			int num;
			if (GameMidlet.avatar.x < avatar2.x)
			{
				num = avatar2.x - 15;
			}
			else
			{
				num = avatar2.x + 15;
			}
			GameMidlet.avatar.xCur = num;
			this.doMove(num, avatar2.y, (int)GameMidlet.avatar.direct);
		}
		if (idTo == GameMidlet.avatar.IDDB)
		{
			this.doMove(GameMidlet.avatar.x, GameMidlet.avatar.y, (int)(((int)avatar.direct != (int)Base.RIGHT) ? Base.RIGHT : Base.LEFT));
		}
		if (i == 1)
		{
			avatar2.isJumps = -1;
			switch (idGift)
			{
			case 100:
				this.hit(avatar, avatar2);
				break;
			case 101:
				this.kiss(avatar, avatar2);
				break;
			case 102:
			case 103:
				avatar2.task = (avatar.task = 12);
				avatar2.timeTask = (avatar.timeTask = (short)time);
				this.showChat(string.Concat(new string[]
				{
					avatar.name,
					" ",
					T.giveGift,
					" ",
					avatar2.name
				}));
				break;
			default:
				if (idGift != 0)
				{
					this.showChat(avatar.name + " tặng quà " + avatar2.name);
				}
				else
				{
					avatar2.task = (avatar.task = -3);
					this.showChat(avatar.name + " " + T.giveGiftFlower + avatar2.name);
				}
				break;
			}
		}
		else
		{
			avatar.task = 9;
			avatar2.task = 8;
			avatar2.isJumps = -1;
			avatar2.idGift = idGift;
			Part part = AvatarData.getPart((short)idGift);
			this.showChat(string.Concat(new string[]
			{
				avatar.name,
				" ",
				T.dunation,
				" ",
				part.name,
				" ",
				T.cho,
				" ",
				avatar2.name
			}));
		}
		avatar2.firFeel = avatar2.feel;
		avatar2.numFeel = 0;
		avatar.firFeel = avatar.feel;
		avatar.numFeel = 0;
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x000537D0 File Offset: 0x00051BD0
	public void setGifts(Avatar p)
	{
		APartInfo apartInfo = (APartInfo)AvatarData.getPart((short)p.idGift);
		SeriPart seriByZ = AvatarData.getSeriByZ((int)apartInfo.zOrder, p.seriPart);
		if (seriByZ == null)
		{
			p.addSeri(new SeriPart((short)p.idGift));
			p.orderSeriesPath();
		}
		else
		{
			seriByZ.idPart = (short)p.idGift;
		}
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00053834 File Offset: 0x00051C34
	public void doRequestAddFriend(Avatar p)
	{
		if (p == null)
		{
			return;
		}
		ParkService.gI().doRequestAddFriend(p.IDDB);
		Canvas.startOKDlg(string.Concat(new string[]
		{
			T.pleaseWait,
			" ",
			p.name,
			"  ",
			T.agree
		}));
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00053891 File Offset: 0x00051C91
	public void onRequestAddFriend(Avatar p, string text)
	{
		Out.println("onRequestAddFriend: " + p.name);
		MessageScr.gI().addPlayer(p.IDDB, p.name, "Lời mời kết bạn", false, new MapScr.IActionAddFriend5(p, text));
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x000538CB File Offset: 0x00051CCB
	public void onAddFriend(Avatar p, bool tr, string text)
	{
		Canvas.startOKDlg(text);
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x000538D3 File Offset: 0x00051CD3
	public void doRequestYourInfo()
	{
		if (MapScr.focusP != null)
		{
			Canvas.startWaitCancelDlg(T.pleaseWait);
			ParkService.gI().doRequestYourInfo(MapScr.focusP.IDDB);
		}
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00053900 File Offset: 0x00051D00
	public void onRemoveItem(int idUser, int idPart)
	{
		if (idUser == GameMidlet.avatar.IDDB)
		{
			return;
		}
		Avatar avatar = LoadMap.getAvatar(idUser);
		if (avatar != null)
		{
			SeriPart seriByIdPart = AvatarData.getSeriByIdPart(avatar.seriPart, idPart);
			if (seriByIdPart != null)
			{
				avatar.seriPart.removeElement(seriByIdPart);
			}
		}
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0005394A File Offset: 0x00051D4A
	public void onParkList(int[] num)
	{
		ParkListSrc.gI().setList(num);
		ParkListSrc.gI().switchToMe(this);
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00053962 File Offset: 0x00051D62
	public void onContainer(MyVector listCon)
	{
		GameMidlet.listContainer = listCon;
		if (MainMenu.gI().isWearing)
		{
			MainMenu.gI().doWearing();
		}
		else
		{
			this.doStore();
		}
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00053990 File Offset: 0x00051D90
	public void onUsingPart(int idUser, short idP)
	{
		Avatar avatar = LoadMap.getAvatar(idUser);
		if (avatar == null)
		{
			return;
		}
		Part part = AvatarData.getPart(idP);
		if ((int)part.zOrder == -1)
		{
			if (avatar.idPet == idP)
			{
				Pet pet = LoadMap.getPet(avatar.IDDB);
				if (pet != null)
				{
					LoadMap.playerLists.removeElement(pet);
					avatar.idPet = -1;
				}
			}
			else
			{
				avatar.changePet(idP);
				AvatarService.gI().doRequestExpicePet(avatar.IDDB);
			}
		}
		else
		{
			SeriPart seriByIdPart = AvatarData.getSeriByIdPart(avatar.seriPart, (int)idP);
			if (seriByIdPart != null)
			{
				avatar.seriPart.removeElement(seriByIdPart);
			}
			else
			{
				avatar.addSeriPart(new SeriPart(idP));
				avatar.orderSeriesPath();
			}
		}
		if (idUser == GameMidlet.avatar.IDDB)
		{
			if (Canvas.currentMyScreen == PopupShop.gI())
			{
				PopupShop.gI().close();
			}
			GameMidlet.listContainer = null;
			Canvas.endDlg();
		}
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00053A7A File Offset: 0x00051E7A
	public Command cmdDellPart(MyVector list, int type, int typeScr, bool isMenu)
	{
		return new Command(T.removee, new MapScr.IActionDellPart(list, type, typeScr));
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x00053A90 File Offset: 0x00051E90
	protected void doStore()
	{
		if (Canvas.currentMyScreen == MainMenu.me)
		{
			return;
		}
		PopupShop.gI().isFull = true;
		PopupShop.gI().addElement(new string[]
		{
			T.basket,
			T.wearing
		}, new MyVector[]
		{
			this.getListCmdDoUsing(GameMidlet.listContainer, GameMidlet.avatar.IDDB, 1, T.use, true),
			MapScr.gI().getListYourPart(GameMidlet.avatar, 0, true)
		}, null, null);
		PopupShop.gI().setCmdLeft(MapScr.gI().cmdDellPart(GameMidlet.avatar.seriPart, 0, 0, false), 1);
		PopupShop.gI().setCmdLeft(this.cmdDellPart(GameMidlet.listContainer, 1, 0, false), 0);
		if (Canvas.currentMyScreen != PopupShop.gI())
		{
			PopupShop.gI().switchToMe();
			PopupShop.gI().setHorizontal(true);
			PopupShop.isQuaTrang = true;
			PopupShop.gI().setCmyLim();
		}
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00053B84 File Offset: 0x00051F84
	public MyVector getListYourPart(Avatar ava, int type, bool isCmd)
	{
		Avatar avatar = new Avatar();
		avatar.name = ava.name;
		avatar.setMoney(ava.getMoney());
		avatar.IDDB = ava.IDDB;
		avatar.idPet = ava.idPet;
		avatar.hungerPet = ava.hungerPet;
		for (int i = 0; i < ava.seriPart.size(); i++)
		{
			SeriPart seriPart = (SeriPart)ava.seriPart.elementAt(i);
			Part part = AvatarData.getPart(seriPart.idPart);
			if (part != null && (int)part.zOrder != 30 && (int)part.zOrder != 40)
			{
				avatar.addSeri(seriPart);
			}
		}
		if (avatar.idPet != -1 && type == 0)
		{
			SeriPart seriPart2 = new SeriPart(avatar.idPet);
			seriPart2.time = (sbyte)(100 - avatar.hungerPet);
			avatar.seriPart.addElement(seriPart2);
		}
		MyVector myVector = new MyVector();
		return this.getListCmdDoUsing(avatar.seriPart, avatar.IDDB, 0, T.catdo, isCmd);
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00053C98 File Offset: 0x00052098
	public MyVector getListCmdDoUsing(MyVector list, int id, int type, string na, bool isCmd)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < list.size(); i++)
		{
			int num = i;
			SeriPart seriPart = (SeriPart)list.elementAt(num);
			Part part = AvatarData.getPart(seriPart.idPart);
			string name = null;
			IAction action = null;
			if (isCmd && id == GameMidlet.avatar.IDDB && (!AvatarData.isZOrderMain((int)part.zOrder) || type != 0))
			{
				name = na;
				action = new MapScr.IAction111(type, seriPart, id, num);
			}
			Command command = new MapScr.CommandUsingPart(name, action, seriPart, num, type);
			if (command != null)
			{
				myVector.addElement(command);
			}
		}
		return myVector;
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00053D40 File Offset: 0x00052140
	public static string strTkFarm()
	{
		return T.tkChinh + ": " + Canvas.getMoneys(GameMidlet.avatar.money[0]) + T.dola;
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x00053D68 File Offset: 0x00052168
	public void doOpenIceDream(string name, int type)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < AvatarData.listItemInfo.size(); i++)
		{
			Item item = (Item)AvatarData.listItemInfo.elementAt(i);
			if ((int)item.shopType == type)
			{
				myVector.addElement(item);
			}
		}
		MyVector myVector2 = new MyVector();
		for (int j = 0; j < myVector.size(); j++)
		{
			int i2 = j;
			Item it = (Item)myVector.elementAt(i2);
			myVector2.addElement(new MapScr.CommandIceDream(T.buy, new MapScr.IActionIceDream(it), it, i2));
		}
		PopupShop.gI().switchToMe();
		PopupShop.gI().addElement(new string[]
		{
			name
		}, new MyVector[]
		{
			myVector2
		}, null, null);
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x00053E34 File Offset: 0x00052234
	protected static void doBuyIceDream(Item item)
	{
		Canvas.startOKDlg(T.doYouWantBuy, new MapScr.IActionBuyDream(item));
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x00053E48 File Offset: 0x00052248
	public void onBuyIceDream(short idItem, int price)
	{
		Canvas.endDlg();
		PopupShop.isTransFocus = true;
		Item itemByList = Item.getItemByList(AvatarData.listItemInfo, (int)idItem);
		if (itemByList != null)
		{
			if ((int)itemByList.shopType == 5)
			{
				AvatarService.gI().doRequestExpicePet(GameMidlet.avatar.IDDB);
			}
			GameMidlet.avatar.setMoney(price);
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x00053EA0 File Offset: 0x000522A0
	public void onOpenShop(sbyte typeShop, int idShop, string nameShop, short[] listPark, int idBoss1, string[] textDes, string[] textContent)
	{
		if (Canvas.currentMyScreen == PopupShop.gI())
		{
			return;
		}
		if (TouchScreenKeyboard.visible)
		{
			ipKeyboard.close();
			ipKeyboard.tk = null;
		}
		Out.println("onOpenShop: " + idShop);
		this.setAvatarShop(GameMidlet.avatar);
		if (idShop == 26)
		{
			if (MapScr.focusP == null)
			{
				return;
			}
			this.setAvatarShop(MapScr.focusP);
		}
		else
		{
			this.setAvatarShop(GameMidlet.avatar);
		}
		MyVector myVector = new MyVector();
		if ((int)typeShop == 0)
		{
			if (listPark == null || listPark.Length == 0)
			{
				for (int i = 0; i < AvatarData.listPart.Length; i++)
				{
					Part part = AvatarData.listPart[i];
					if (part != null && (part.price[0] > 0 || part.price[1] > 0) && idShop == (int)part.sell)
					{
						myVector.addElement(part);
					}
				}
			}
			else
			{
				for (int j = 0; j < listPark.Length; j++)
				{
					myVector.addElement(AvatarData.getPart(listPark[j]));
				}
				for (int k = 0; k < myVector.size(); k++)
				{
					Part part2 = (Part)myVector.elementAt(k);
				}
			}
			int num = 0;
			if (idShop == 26)
			{
				MyVector[] array = new MyVector[6];
				for (int l = 0; l < 6; l++)
				{
					array[l] = new MyVector();
				}
				int[] array2 = new int[6];
				for (int m = 0; m < myVector.size(); m++)
				{
					Part part3 = (Part)myVector.elementAt(m);
					string textDes2 = string.Empty;
					if (textDes != null && textDes.Length > 0)
					{
						textDes2 = textDes[m];
					}
					string name = "Tặng";
					if ((int)part3.zOrder == 20)
					{
						array[0].addElement(new MapScr.CommandOpenShop(name, new MapScr.IActionOpenShop(part3, (listPark == null) ? -1 : listPark[m], idShop, textDes2, idBoss1, array2[0]), part3, (listPark == null) ? -1 : listPark[m], array2[0], idBoss1, idShop));
						array2[0]++;
					}
					else if ((int)part3.zOrder == 10)
					{
						array[1].addElement(new MapScr.CommandOpenShop(name, new MapScr.IActionOpenShop(part3, (listPark == null) ? -1 : listPark[m], idShop, textDes2, idBoss1, array2[1]), part3, (listPark == null) ? -1 : listPark[m], array2[1], idBoss1, idShop));
						array2[1]++;
					}
					else if ((int)part3.zOrder == 52 || (int)part3.zOrder == 53 || (int)part3.zOrder == 5)
					{
						array[2].addElement(new MapScr.CommandOpenShop(name, new MapScr.IActionOpenShop(part3, (listPark == null) ? -1 : listPark[m], idShop, textDes2, idBoss1, array2[2]), part3, (listPark == null) ? -1 : listPark[m], array2[2], idBoss1, idShop));
						array2[2]++;
					}
					else if ((int)part3.zOrder == 60)
					{
						array[3].addElement(new MapScr.CommandOpenShop(name, new MapScr.IActionOpenShop(part3, (listPark == null) ? -1 : listPark[m], idShop, textDes2, idBoss1, array2[3]), part3, (listPark == null) ? -1 : listPark[m], array2[3], idBoss1, idShop));
						array2[3]++;
					}
					else if ((int)part3.zOrder == 70)
					{
						array[4].addElement(new MapScr.CommandOpenShop(name, new MapScr.IActionOpenShop(part3, (listPark == null) ? -1 : listPark[m], idShop, textDes2, idBoss1, array2[4]), part3, (listPark == null) ? -1 : listPark[m], array2[4], idBoss1, idShop));
						array2[4]++;
					}
					else
					{
						array[5].addElement(new MapScr.CommandOpenShop(name, new MapScr.IActionOpenShop(part3, (listPark == null) ? -1 : listPark[m], idShop, textDes2, idBoss1, array2[5]), part3, (listPark == null) ? -1 : listPark[m], array2[5], idBoss1, idShop));
						array2[5]++;
					}
				}
				int num2 = 0;
				for (int n = 0; n < array.Length; n++)
				{
					if (array[n].size() > 0)
					{
						num2++;
					}
				}
				Out.println("size: " + array[5].size());
				string[] array3 = new string[]
				{
					"Áo",
					"Quần",
					"Trang sức",
					"Nón",
					"Cầm tay",
					"Khác"
				};
				sbyte[] array4 = new sbyte[]
				{
					0,
					1,
					2,
					3,
					4,
					5
				};
				MyVector[] array5 = new MyVector[num2];
				sbyte[] array6 = new sbyte[num2];
				string[] array7 = new string[num2];
				int num3 = 0;
				for (int num4 = 0; num4 < array.Length; num4++)
				{
					if (array[num4].size() > 0 || num4 == 5)
					{
						if (num4 == 5)
						{
							int num5 = array[5].size();
							for (int num6 = 0; num6 < MapScr.listItemEffect.size(); num6++)
							{
								ItemEffectInfo itemEffectInfo = (ItemEffectInfo)MapScr.listItemEffect.elementAt(num6);
								array[5].addElement(new MapScr.CommandGiftDef(T.giveGift, new MapScr.IActionGiftDef(num6, itemEffectInfo.IDAction), num6, itemEffectInfo, num5));
							}
						}
						array5[num3] = array[num4];
						array6[num3] = array4[num4];
						array7[num3] = array3[num4];
						num3++;
					}
				}
				PopupShop.gI().switchToMe();
				PopupShop.isHorizontal = true;
				PopupShop.gI().addElement(array7, array5, null, array6);
				Canvas.endDlg();
			}
			else
			{
				MyVector myVector2 = new MyVector();
				for (int num7 = 0; num7 < myVector.size(); num7++)
				{
					Part p = (Part)myVector.elementAt(num7);
					string textDes3 = string.Empty;
					if (textDes != null && textDes.Length > 0)
					{
						textDes3 = textDes[num7];
					}
					string name2 = string.Empty;
					if (idShop == 100)
					{
						name2 = T.dial;
					}
					else if (idShop == 26)
					{
						name2 = "Tặng";
					}
					else
					{
						name2 = T.buy;
					}
					myVector2.addElement(new MapScr.CommandOpenShop(name2, new MapScr.IActionOpenShop(p, (listPark == null) ? -1 : listPark[num7], idShop, textDes3, idBoss1, num7), p, (listPark == null) ? -1 : listPark[num7], num7, idBoss1, idShop));
					num++;
				}
				if (myVector2.size() > 0)
				{
					PopupShop.gI().switchToMe();
					PopupShop.isHorizontal = true;
					PopupShop.gI().addElement(new string[]
					{
						nameShop
					}, new MyVector[]
					{
						myVector2
					}, null, null);
				}
				Canvas.endDlg();
			}
		}
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x000545C4 File Offset: 0x000529C4
	public void onRequestExpicePet(int idUser3, sbyte expice1)
	{
		if (idUser3 == GameMidlet.avatar.IDDB)
		{
			GameMidlet.avatar.hungerPet = (short)expice1;
		}
		else
		{
			Avatar avatar = LoadMap.getAvatar(idUser3);
			if (avatar != null)
			{
				avatar.hungerPet = (short)expice1;
			}
		}
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00054608 File Offset: 0x00052A08
	public void onCustomPopup(int idBoss, int idPopup, string text5, string[] subText)
	{
		int num = subText.Length;
		if (num != 1)
		{
			if (num != 2)
			{
				if (num == 3)
				{
					Canvas.msgdlg.setInfoLCR(text5, new Command(subText[0], new MapScr.IActionCustomPopup(idBoss, idPopup, 0)), new Command(subText[1], new MapScr.IActionCustomPopup(idBoss, idPopup, 1)), new Command(subText[2], new MapScr.IActionCustomPopup(idBoss, idPopup, 2)));
				}
			}
			else
			{
				Canvas.msgdlg.setInfoLR(text5, new Command(subText[0], new MapScr.IActionCustomPopup(idBoss, idPopup, 0)), new Command(subText[1], new MapScr.IActionCustomPopup(idBoss, idPopup, 1)));
			}
		}
		else
		{
			Canvas.msgdlg.setInfoC(text5, new Command(subText[0], new MapScr.IActionCustomPopup(idBoss, idPopup, 0)));
		}
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x000546CC File Offset: 0x00052ACC
	public void onChangeClan(int idUser8, short idImg)
	{
		Avatar avatar = LoadMap.getAvatar(idUser8);
		if (avatar != null)
		{
			avatar.idImg = idImg;
		}
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x000546ED File Offset: 0x00052AED
	public void showChat(string text)
	{
		this.chatList.addElement(text);
		if (this.chatDelay == 0)
		{
			this.chatDelay = this.MAX_CHAT_DELAY;
		}
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00054714 File Offset: 0x00052B14
	public void updateChat()
	{
		if (this.chatDelay > 0)
		{
			this.chatDelay--;
			if (this.chatDelay == 0)
			{
				if (this.chatList.size() > 0)
				{
					this.chatList.removeElementAt(0);
				}
				if (this.chatList.size() > 0)
				{
					this.chatDelay = this.MAX_CHAT_DELAY;
				}
			}
		}
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x00054780 File Offset: 0x00052B80
	public void paintChat(MyGraphics g)
	{
		Canvas.resetTrans(g);
		if (this.chatList.size() == 0)
		{
			return;
		}
		string st = (string)this.chatList.elementAt(0);
		int num = this.MAX_CHAT_DELAY - this.chatDelay;
		if (num > 10)
		{
			num = 10;
		}
		int num2 = Canvas.w;
		for (int i = 0; i < num; i++)
		{
			num2 >>= 1;
		}
		Canvas.borderFont.drawString(g, st, 3 + num2, Canvas.hCan - Canvas.borderFont.getHeight() - 5 * AvMain.hd, 0);
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x00054814 File Offset: 0x00052C14
	public void onMenuRotate(MyVector lstCmd)
	{
		if (lstCmd.size() == 0)
		{
			return;
		}
		MyVector myVector = new MyVector();
		for (int i = 0; i < lstCmd.size(); i++)
		{
			StringObj stringObj = (StringObj)lstCmd.elementAt(i);
			myVector.addElement(new MapScr.CommandMenuRotate(stringObj.str, new MapScr.IActionMenuRotate(stringObj), stringObj.dis));
		}
		MainMenu.gI().setInfo(myVector);
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x00054880 File Offset: 0x00052C80
	public void onDropPark(sbyte typeDrop, int idPlayer, short idDrop1, int id, short xDrop, short yDrop)
	{
		Drop_Part drop_Part = new Drop_Part(typeDrop, idDrop1, id);
		drop_Part.startDropFrom(idPlayer, xDrop, yDrop);
		LoadMap.playerLists.addElement(drop_Part);
		LoadMap.orderVector(LoadMap.treeLists);
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x000548B8 File Offset: 0x00052CB8
	public void onGetPart(int id, int idUser)
	{
		Drop_Part dropPart = MapScr.getDropPart(id);
		if (dropPart != null)
		{
			dropPart.startFlyTo(idUser);
		}
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x000548DC File Offset: 0x00052CDC
	public static Drop_Part getDropPart(int id)
	{
		for (int i = 0; i < LoadMap.playerLists.size(); i++)
		{
			MyObject myObject = (MyObject)LoadMap.playerLists.elementAt(i);
			if ((int)myObject.catagory == 5)
			{
				Drop_Part drop_Part = (Drop_Part)myObject;
				if (drop_Part.ID == id)
				{
					return drop_Part;
				}
			}
		}
		return null;
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x00054938 File Offset: 0x00052D38
	public void onEffect(EffectManager effObj)
	{
		if (LoadMap.effManager == null)
		{
			LoadMap.effManager = new MyVector();
		}
		LoadMap.effManager.addElement(effObj);
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0005495C File Offset: 0x00052D5C
	public void onEmotionList(int idUser, MyVector listE)
	{
		Avatar avatar = LoadMap.getAvatar(idUser);
		if (avatar != null)
		{
			avatar.emotionList = listE;
			avatar.timeEmotion = 0;
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x00054984 File Offset: 0x00052D84
	public void doJoin()
	{
		if (!this.isTour)
		{
			return;
		}
		this.isTour = true;
		if (MiniMap.gI().selected == 2)
		{
			GlobalService.gI().requestCityMap(-1);
			return;
		}
		sbyte[] array = new sbyte[]
		{
			0,
			13,
			20,
			9,
			23,
			11,
			17
		};
		ParkService.gI().doJoinPark((int)array[MiniMap.gI().selected], -1);
		Canvas.startWaitDlg();
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x000549F0 File Offset: 0x00052DF0
	public void joinCitymap()
	{
		Out.println("joinCitymap");
		Out.println("joinCitymap11111111111111111: " + RegisterInfoScr.isCreate);
		if (RegisterInfoScr.isCreate)
		{
			Out.println("aaaaaaaaaaaaaaaaaa");
			RegisterInfoScr.isCreate = false;
			RegisterInfoScr.gI().start(RegisterInfoScr.isTrue);
			return;
		}
		if (Session_ME.gI().isConnected() && (int)GameMidlet.avatar.gender == 0)
		{
			if (GlobalLogicHandler.isNewVersion)
			{
				return;
			}
			RegisterScr.gI().switchToMe();
			Canvas.endDlg();
			return;
		}
		else
		{
			if (Canvas.currentMyScreen != OptionScr.instance)
			{
				Canvas.load = 0;
			}
			if (!this.isTour)
			{
				GlobalService.gI().getHandler(9);
				GlobalService.gI().requestCityMap(0);
				return;
			}
			if (MiniMap.isChange)
			{
				MiniMap.isChange = false;
				int num = 16 * AvMain.hd;
				LoadMap.idTileImg = -1;
				MiniMap.imgCreateMap = Image.createImagePNG(T.getPath() + "/minimap");
				MyVector myVector = new MyVector();
				int num2 = 50;
				int num3 = 26;
				sbyte[] array = new sbyte[num2 * num3];
				int num4 = 0;
				DataInputStream resourceAsStream = DataInputStream.getResourceAsStream("citiMap");
				for (int i = 0; i < num3; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						array[i * num2 + j] = resourceAsStream.readByte();
						if ((int)array[i * num2 + j] == 69)
						{
							myVector.addElement(new PositionMap
							{
								x = (int)((sbyte)j),
								y = (int)((sbyte)i),
								idImg = (short)(819 + num4),
								text = T.nameRegion[num4]
							});
							num4++;
						}
					}
				}
				resourceAsStream.close();
				LoadMap.TYPEMAP = -1;
				MiniMap.isCityMap = true;
				MiniMap.gI().setInfo(null, array, myVector, (sbyte)num2, 16 * AvMain.hd, new Command(T.selectt, 3, this));
				MiniMap.gI().cmdUpdateKey = new MapScr.IActionMiniMapKey();
			}
			MiniMap.gI().selected = 3;
			MiniMap.gI().switchToMe(this);
			Canvas.endDlg();
			if (MiniMap.actionReg != null && MiniMap.iRequestReg == 0 && !Canvas.isInitChar)
			{
				MiniMap.actionReg.perform();
				MiniMap.iRequestReg = 1;
			}
			return;
		}
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00054C44 File Offset: 0x00053044
	private void doSelectedMiniMap()
	{
		string beingOn = T.beingOn;
		switch (MiniMap.gI().selected)
		{
		case 0:
			GlobalService.gI().getHandler(11);
			break;
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			GlobalService.gI().getHandler(9);
			break;
		case 7:
			GlobalService.gI().getHandler(10);
			break;
		}
		Canvas.startWaitDlg(beingOn + T.nameRegion[MiniMap.gI().selected] + "...");
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00054CE0 File Offset: 0x000530E0
	public void doChangePass()
	{
		TField[] array = new TField[3];
		for (int i = 0; i < 3; i++)
		{
			array[i] = new TField(string.Empty, Canvas.currentMyScreen, new MapScr.IActionChangePass(array));
			array[i].autoScaleScreen = true;
			array[i].showSubTextField = false;
			array[i].setIputType(ipKeyboard.PASS);
		}
		array[0].setFocus(true);
		Command cmd = new Command(T.finish, new MapScr.IActionChangePass(array));
		InputFace.gI().setInfo(array, T.changePass, T.nameChangePass, cmd, Canvas.hCan);
		InputFace.gI().show();
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00054D7C File Offset: 0x0005317C
	public static bool setEnterPass(TField[] tf)
	{
		int num = -1;
		for (int i = 0; i < 3; i++)
		{
			if (tf[i].getText().Equals(string.Empty))
			{
				num = i;
			}
		}
		if (tf[1].Equals(string.Empty) && tf[1].Equals(string.Empty) && !tf[1].getText().Equals(tf[2].getText()))
		{
			num = 3;
		}
		if (tf[0].Equals(string.Empty) && tf[1].Equals(string.Empty) && tf[0].getText().Equals(tf[1].getText()))
		{
			num = 4;
		}
		if (num != -1)
		{
			Canvas.startOKDlg(T.enterPass[num]);
			return false;
		}
		return true;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00054E4C File Offset: 0x0005324C
	public void onSelectedMiniMap(sbyte[] map, sbyte idMap, sbyte idTileImg, sbyte wMap, Image img, short[] idImg, MyVector mapItemType, MyVector mapItem)
	{
		MapScr.idImg = idImg;
		Canvas.load = 0;
		MapScr.roomID = idMap;
		LoadMap.mapItemType = mapItemType;
		LoadMap.mapItem = mapItem;
		DataInputStream dataInputStream = new DataInputStream(map);
		LoadMap.map = new short[map.Length];
		LoadMap.wMap = (short)wMap;
		LoadMap.Hmap = (short)(map.Length / (int)wMap);
		LoadMap.imgBG = img;
		if (img != null)
		{
			LoadMap.colorBackGr = LoadMap.imgBG.texture.GetPixel(0, LoadMap.imgBG.getHeight() - 1);
		}
		try
		{
			for (int i = 0; i < LoadMap.map.Length; i++)
			{
				LoadMap.map[i] = (short)((byte)dataInputStream.readByte());
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		if ((int)idTileImg != LoadMap.idTileImg)
		{
			GlobalService.gI().requestTileMap(idTileImg);
		}
		else
		{
			Canvas.loadMap.setMapAny();
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00054F40 File Offset: 0x00053340
	public void doExitGame()
	{
		Canvas.startOKDlg(T.doYouWantExit2, 2, this);
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00054F50 File Offset: 0x00053350
	public void exitGame()
	{
		if (GameMidlet.avatar.seriPart != null)
		{
			GameMidlet.avatar.seriPart.removeAllElements();
		}
		Session_ME.gI().close();
		if (onMainMenu.isOngame)
		{
			onMainMenu.isOngame = false;
			onMainMenu.resetImg();
			onMainMenu.resetSize();
			Canvas.paint.resetOngame();
			Canvas.paint.resetCasino();
			Resources.UnloadUnusedAssets();
			Canvas.paint.loadImgAvatar();
		}
		GlobalMessageHandler.gI().miniGameMessageHandler = null;
		LoginScr.gI().load();
		ListScr.friendL = null;
		LoadMap.playerLists.removeAllElements();
		GameMidlet.avatar = new Avatar();
		GameMidlet.myIndexP = new IndexPlayer();
		Canvas.listInfoSV.removeAllElements();
		if (Canvas.menuMain != null)
		{
			Canvas.menuMain = null;
		}
		if (ipKeyboard.tk != null)
		{
			ipKeyboard.tk.active = false;
		}
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0005502C File Offset: 0x0005342C
	private void setAvatarShop(Avatar player)
	{
		MapScr.avatarShop = new Avatar();
		MapScr.avatarShop.seriPart = new MyVector();
		MapScr.avatarShop.direct = Base.RIGHT;
		MapScr.avatarShop.gender = player.gender;
		MapScr.avatarShop.lvMain = player.lvMain;
		for (int i = 0; i < player.seriPart.size(); i++)
		{
			SeriPart seriPart = new SeriPart();
			seriPart.idPart = ((SeriPart)player.seriPart.elementAt(i)).idPart;
			MapScr.avatarShop.addSeri(seriPart);
		}
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x000550CC File Offset: 0x000534CC
	public void doOpenShopOffline(Avatar player, int focus)
	{
		this.setAvatarShop(player);
		sbyte[] array = null;
		sbyte[] array2 = new sbyte[2];
		if ((int)MapScr.typeJoin == 3)
		{
			array2[0] = 3;
			array2[1] = 8;
		}
		Out.println("typeJoin: " + MapScr.typeJoin);
		MyVector[] array3;
		string[] name;
		sbyte[] array4;
		switch (MapScr.typeJoin)
		{
		case 1:
		case 6:
			array = new sbyte[]
			{
				10,
				20
			};
			array3 = new MyVector[]
			{
				new MyVector(),
				new MyVector()
			};
			name = new string[]
			{
				T.pant,
				T.cloth
			};
			array2[0] = 1;
			array2[1] = 6;
			array4 = new sbyte[2];
			goto IL_157;
		case 2:
		case 7:
			array = new sbyte[]
			{
				40,
				50
			};
			array3 = new MyVector[]
			{
				new MyVector(),
				new MyVector()
			};
			name = new string[]
			{
				T.eye,
				T.hair
			};
			array2[0] = 2;
			array2[1] = 7;
			array4 = new sbyte[2];
			goto IL_157;
		}
		array3 = new MyVector[]
		{
			new MyVector()
		};
		name = new string[]
		{
			T.gift
		};
		array4 = new sbyte[1];
		array2 = new sbyte[]
		{
			3,
			8
		};
		IL_157:
		for (int i = 0; i < AvatarData.listPart.Length; i++)
		{
			if (AvatarData.listPart[i].follow != -2)
			{
				Part part = AvatarData.listPart[i];
				int num;
				if (part.follow >= 0)
				{
					num = (int)((APartInfo)AvatarData.listPart[(int)part.follow]).gender;
				}
				else
				{
					num = (int)((APartInfo)part).gender;
				}
				if (part != null && (part.price[0] > 0 || part.price[1] > 0) && ((int)player.gender == num || num == 0) && ((int)array2[0] == (int)part.sell || (int)array2[1] == (int)part.sell) && part.follow > -2)
				{
					if (array == null)
					{
						sbyte b = array4[0];
						array3[0].addElement(new MapScr.CommandShopOffline(T.buy, new MapScr.IActionShopOffline(part.IDPart), part, (int)b));
						sbyte[] array5 = array4;
						int num2 = 0;
						array5[num2] = (sbyte)((int)array5[num2] + 1);
					}
					else
					{
						for (int j = 0; j < array3.Length; j++)
						{
							if ((int)array[j] == (int)part.zOrder)
							{
								sbyte b2 = array4[j];
								array3[j].addElement(new MapScr.CommandShopOffline(T.buy, new MapScr.IActionShopOffline(part.IDPart), part, (int)b2));
								sbyte[] array6 = array4;
								int num3 = j;
								array6[num3] = (sbyte)((int)array6[num3] + 1);
							}
						}
					}
				}
			}
		}
		PopupShop.gI().switchToMe();
		PopupShop.gI().setHorizontal(true);
		PopupShop.gI().addElement(name, array3, null, null);
		PopupShop.focusTap = focus;
		PopupShop.isQuaTrang = true;
		PopupShop.gI().setCmyLim();
		Canvas.endDlg();
		if (LoadMap.TYPEMAP == 57 && Canvas.isInitChar)
		{
			Canvas.welcome = new Welcome();
			Canvas.welcome.initShop(PopupShop.me);
		}
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x00055428 File Offset: 0x00053828
	public static void setAvatarShop(Part part)
	{
		MapScr.avatarShop = new Avatar();
		MapScr.avatarShop.direct = Base.RIGHT;
		MapScr.avatarShop.seriPart = new MyVector();
		bool flag = false;
		for (int i = 0; i < GameMidlet.avatar.seriPart.size(); i++)
		{
			SeriPart seriPart = new SeriPart();
			seriPart.idPart = ((SeriPart)GameMidlet.avatar.seriPart.elementAt(i)).idPart;
			Part part2 = AvatarData.getPart(seriPart.idPart);
			if ((int)part2.zOrder == (int)part.zOrder)
			{
				seriPart.idPart = part.IDPart;
				flag = true;
			}
			MapScr.avatarShop.addSeri(seriPart);
		}
		if (!flag)
		{
			SeriPart seriPart2 = new SeriPart();
			seriPart2.idPart = part.IDPart;
			MapScr.avatarShop.addSeri(seriPart2);
		}
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x00055503 File Offset: 0x00053903
	public static void doBuyItem(int idPart)
	{
		MapScr.doSelectMoneyBuyItem(AvatarData.getPart((short)idPart));
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x00055511 File Offset: 0x00053911
	public static void doSelectMoneyBuyItem(Part ava)
	{
		Canvas.getTypeMoney(ava.price[0], ava.price[1], new MapScr.IActionSelectedMoney(ava.IDPart, 1), new MapScr.IActionSelectedMoney(ava.IDPart, 2), null);
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x00055544 File Offset: 0x00053944
	public void onBuyItem(short idPart, int newMoney, sbyte typeBuy, string text, int xu, int luong, int luongKhoa)
	{
		Canvas.startOKDlg(text);
		if ((int)typeBuy == 1)
		{
			GameMidlet.avatar.setMoney(xu);
		}
		GameMidlet.avatar.setGold(luong);
		GameMidlet.avatar.luongKhoa = luongKhoa;
		Part part = AvatarData.getPart(idPart);
		if (part.follow != -2)
		{
			SeriPart seriByZ = AvatarData.getSeriByZ((int)part.zOrder, GameMidlet.avatar.seriPart);
			if (seriByZ != null)
			{
				seriByZ.idPart = idPart;
			}
			else if ((int)part.zOrder == -1 && GameMidlet.avatar.idPet != -1)
			{
				GameMidlet.avatar.changePet(idPart);
				AvatarService.gI().doRequestExpicePet(GameMidlet.avatar.IDDB);
			}
			else
			{
				GameMidlet.avatar.addSeri(new SeriPart(idPart));
				GameMidlet.avatar.orderSeriesPath();
			}
			GameMidlet.avatar.setFeel(11);
			if ((int)part.zOrder == -1 && GameMidlet.avatar.idPet == -1)
			{
				GameMidlet.avatar.setPet();
				AvatarService.gI().doRequestExpicePet(GameMidlet.avatar.IDDB);
			}
		}
		GameMidlet.listContainer = null;
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x00055669 File Offset: 0x00053A69
	public void doSetHandlerSuccess()
	{
		ParkService.gI().doJoinPark((int)MapScr.roomID, -1);
		MapScr.typeJoin = -1;
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x00055682 File Offset: 0x00053A82
	public override void commandAction(int index)
	{
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x00055684 File Offset: 0x00053A84
	public void onJoinOfflineMap(sbyte idMap1, MyVector listAvatar, MyVector mapItemType1, MyVector mapItem1)
	{
		sbyte[] array = new sbyte[]
		{
			59,
			60,
			58,
			104,
			105,
			101,
			102
		};
		LoadMap.mapItemType = mapItemType1;
		LoadMap.mapItem = mapItem1;
		Canvas.loadMap.load((int)array[(int)idMap1], true);
		if (mapItemType1 != null)
		{
			Canvas.loadMap.setMapItemType();
		}
		for (int i = 0; i < listAvatar.size(); i++)
		{
			MyObject myObject = (MyObject)listAvatar.elementAt(i);
			if ((int)myObject.catagory == 0)
			{
				Avatar avatar = (Avatar)myObject;
				avatar.xCur = avatar.x;
				avatar.yCur = avatar.y;
				avatar.dirFirst = avatar.direct;
				avatar.orderSeriesPath();
				if (avatar.IDDB != GameMidlet.avatar.IDDB)
				{
					this.setGender(avatar);
					LoadMap.addPlayer(avatar);
				}
			}
			else if ((int)myObject.catagory == 5)
			{
				Drop_Part drop_Part = (Drop_Part)myObject;
				drop_Part.x0 = drop_Part.x;
				drop_Part.y0 = drop_Part.y;
				LoadMap.playerLists.addElement(drop_Part);
			}
		}
		if (Bus.isRun)
		{
			this.doMove(Bus.posBusStop.x, Bus.posBusStop.y, (int)GameMidlet.avatar.direct);
		}
		else
		{
			GameMidlet.avatar.y++;
			this.move();
		}
		MapScr.doSellectFeel((int)GameMidlet.avatar.feel);
		if (Canvas.isInitChar && (int)array[(int)idMap1] == 101)
		{
			Canvas.welcome = new Welcome();
			Canvas.welcome.initTash();
		}
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0005581A File Offset: 0x00053C1A
	public void doJoinMapOffline(int i)
	{
		MapScr.idMapOffline = i;
		MapScr.idMapOld = LoadMap.TYPEMAP;
		MapScr.gI().move();
		GlobalService.gI().getHandler(8);
		Canvas.startWaitDlg();
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00055848 File Offset: 0x00053C48
	public void onWeddingStart(int userIDBoy, int userIDGirl)
	{
		if (Canvas.currentMyScreen == PopupShop.me)
		{
			PopupShop.gI().close();
		}
		SoundManager.playSoundBG(82);
		Out.println("onWeddingStart 1111111111111");
		Canvas.load = 1;
		MapScr.idUserWedding_1 = userIDBoy;
		MapScr.idUserWedding_2 = userIDGirl;
		MapScr.isWedding = true;
		this.iGoChaSu = 0;
		for (int i = 0; i < MapScr.listChair.size() - 1; i++)
		{
			AvPosition avPosition = (AvPosition)MapScr.listChair.elementAt(i);
			for (int j = i + 1; j < MapScr.listChair.size(); j++)
			{
				AvPosition avPosition2 = (AvPosition)MapScr.listChair.elementAt(j);
				if (avPosition.index > avPosition2.index)
				{
					MapScr.listChair.setElementAt(avPosition2, i);
					MapScr.listChair.setElementAt(avPosition, j);
					avPosition = avPosition2;
				}
			}
		}
		for (int k = 0; k < LoadMap.playerLists.size() - 1; k++)
		{
			MyObject myObject = (MyObject)LoadMap.playerLists.elementAt(k);
			if ((int)myObject.catagory == 0)
			{
				for (int l = k + 1; l < LoadMap.playerLists.size(); l++)
				{
					MyObject myObject2 = (MyObject)LoadMap.playerLists.elementAt(l);
					if ((int)myObject2.catagory == 0 && ((Avatar)myObject).IDDB > ((Avatar)myObject2).IDDB)
					{
						LoadMap.playerLists.setElementAt(myObject2, k);
						LoadMap.playerLists.setElementAt(myObject, l);
						myObject = myObject2;
					}
				}
			}
		}
		for (int m = 0; m < LoadMap.playerLists.size(); m++)
		{
			MyObject myObject3 = (MyObject)LoadMap.playerLists.elementAt(m);
			if ((int)myObject3.catagory == 0)
			{
				Avatar avatar = (Avatar)myObject3;
				avatar.moveList.removeAllElements();
				if (avatar.IDDB == userIDGirl)
				{
					avatar.x = (avatar.xCur = 0);
					avatar.y = (avatar.yCur = 8 * LoadMap.w + LoadMap.w / 2 - LoadMap.w / 2);
					avatar.v = 2;
					this.iGoChaSu = 1;
					avatar.addPart(2475, 20);
					avatar.addPart(2476, 10);
					avatar.addPart(300, 60);
					avatar.addPart(302, 70);
					avatar.orderSeriesPath();
				}
				else if (avatar.IDDB == userIDBoy)
				{
					avatar.x = (avatar.xCur = 0);
					avatar.y = (avatar.yCur = 8 * LoadMap.w + LoadMap.w / 2 + LoadMap.w / 2);
					avatar.v = 2;
					this.iGoChaSu = 1;
					avatar.addPart(2477, 20);
					avatar.addPart(2478, 10);
					avatar.orderSeriesPath();
				}
			}
		}
		Avatar avatar2 = LoadMap.getAvatar(userIDBoy);
		Avatar avatar3 = LoadMap.getAvatar(userIDGirl);
		LoadMap.playerLists.removeElement(avatar2);
		LoadMap.playerLists.removeElement(avatar3);
		int num = 0;
		for (int n = 0; n < LoadMap.playerLists.size(); n++)
		{
			MyObject myObject4 = (MyObject)LoadMap.playerLists.elementAt(n);
			if ((int)myObject4.catagory == 0)
			{
				Avatar avatar4 = (Avatar)myObject4;
				if (avatar4.IDDB != -100)
				{
					AvPosition avPosition3 = (AvPosition)MapScr.listChair.elementAt(num / 2);
					Canvas.px = (Canvas.pxLast = (int)((float)avPosition3.x - AvCamera.gI().xCam + (float)(LoadMap.w / 2)));
					Canvas.py = (Canvas.pyLast = (int)((float)avPosition3.y - AvCamera.gI().yCam + (float)(LoadMap.w / 2) + (float)(n % 2 * (LoadMap.w - 5))));
					num++;
					avatar4.setPos((int)((float)Canvas.px + AvCamera.gI().xCam), (int)((float)Canvas.py + AvCamera.gI().yCam));
				}
			}
		}
		LoadMap.playerLists.addElement(avatar2);
		LoadMap.playerLists.addElement(avatar3);
		LoadMap.orderVector(LoadMap.playerLists);
		Canvas.endDlg();
		Out.println(string.Concat(new object[]
		{
			"onWeddingStart 2222222222222222222: ",
			MapScr.isWedding,
			"     ",
			this.iGoChaSu
		}));
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00055CDC File Offset: 0x000540DC
	public void doShowChat()
	{
		ChatTextField.gI().showTF();
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00055CE8 File Offset: 0x000540E8
	public override void doChat(string text)
	{
		if (text.Trim().Equals(string.Empty))
		{
			return;
		}
		if (text.IndexOf("dmw") != -1)
		{
			if (MapScr.focusP != null)
			{
				GlobalService.gI().doServerKick(MapScr.focusP.IDDB, text);
			}
			return;
		}
		if (text.IndexOf("ptw") == 0 && MapScr.focusP != null && MapScr.focusP.chat != null && MapScr.focusP.chat.chats != null)
		{
			string text2 = text + " (";
			for (int i = 0; i < MapScr.focusP.chat.chats.Length; i++)
			{
				text2 = text2 + " " + MapScr.focusP.chat.chats[i];
			}
			text2 += ").";
			GlobalService.gI().doServerKick(MapScr.focusP.IDDB, text2);
			return;
		}
		ParkService.gI().chatToBoard(text);
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00055DF4 File Offset: 0x000541F4
	public void onInfoPlayer(Avatar ava, Avatar friend, string sologan, short idImage, sbyte lv, sbyte perLv, string tenQuanHe, short idActionWedding, string nameAction)
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new MapScr.CommandInfo(string.Empty, new MapScr.IActionInfo(), ava, friend, tenQuanHe, lv, perLv, (int)idImage));
		myVector.addElement(null);
		PopupShop.gI().isFull = true;
		PopupShop.gI().addElement(T.nameTab, new MyVector[]
		{
			null,
			MapScr.gI().getListYourPart(ava, 0, false)
		}, myVector, null);
		PopupShop.gI().setCmdLeft(new Command(nameAction, new MapScr.IActionWedding(idActionWedding)), 0);
		if (Canvas.currentMyScreen != PopupShop.gI())
		{
			PopupShop.gI().switchToMe();
			PopupShop.gI().setHorizontal(true);
			PopupShop.isQuaTrang = true;
			PopupShop.gI().setCmyLim();
			PopupShop.gI().setTap(0);
		}
		Canvas.endDlg();
	}

	// Token: 0x04000AD1 RID: 2769
	public static MapScr instance;

	// Token: 0x04000AD2 RID: 2770
	public static sbyte roomID;

	// Token: 0x04000AD3 RID: 2771
	public static sbyte boardID;

	// Token: 0x04000AD4 RID: 2772
	public static Image imgFocusP;

	// Token: 0x04000AD5 RID: 2773
	public static Image imgHeart;

	// Token: 0x04000AD6 RID: 2774
	public Command cmdMenu;

	// Token: 0x04000AD7 RID: 2775
	public Command cmdEvent;

	// Token: 0x04000AD8 RID: 2776
	public Command cmdGoToMap;

	// Token: 0x04000AD9 RID: 2777
	public Command cmdExchangeBoss;

	// Token: 0x04000ADA RID: 2778
	public static sbyte typeJoin = -1;

	// Token: 0x04000ADB RID: 2779
	public static Avatar focusP;

	// Token: 0x04000ADC RID: 2780
	public static sbyte typeCasino = -1;

	// Token: 0x04000ADD RID: 2781
	public static int indexMenu = 0;

	// Token: 0x04000ADE RID: 2782
	public static int indexAction;

	// Token: 0x04000ADF RID: 2783
	public static int indexFeel;

	// Token: 0x04000AE0 RID: 2784
	public static int indexExchange;

	// Token: 0x04000AE1 RID: 2785
	public static Image imgBar;

	// Token: 0x04000AE2 RID: 2786
	public static MyVector listFish = new MyVector();

	// Token: 0x04000AE3 RID: 2787
	public static int indexMap = -1;

	// Token: 0x04000AE4 RID: 2788
	public static MyVector listCmd;

	// Token: 0x04000AE5 RID: 2789
	public static MyVector listCmdRotate;

	// Token: 0x04000AE6 RID: 2790
	public static MyVector listChair;

	// Token: 0x04000AE7 RID: 2791
	public static MyVector listItemEffect;

	// Token: 0x04000AE8 RID: 2792
	public static bool isWedding = false;

	// Token: 0x04000AE9 RID: 2793
	public static bool isNewVersion = false;

	// Token: 0x04000AEA RID: 2794
	public static int idHouse = -1;

	// Token: 0x04000AEB RID: 2795
	private static sbyte[] ac = new sbyte[]
	{
		10,
		4,
		3,
		5
	};

	// Token: 0x04000AEC RID: 2796
	private int dir = 1;

	// Token: 0x04000AED RID: 2797
	public static Avatar avatarShop;

	// Token: 0x04000AEE RID: 2798
	private sbyte countWeeding = -1;

	// Token: 0x04000AEF RID: 2799
	public static bool isOpenInfo = false;

	// Token: 0x04000AF0 RID: 2800
	private int count;

	// Token: 0x04000AF1 RID: 2801
	private MyVector chatList = new MyVector();

	// Token: 0x04000AF2 RID: 2802
	private int chatDelay;

	// Token: 0x04000AF3 RID: 2803
	private int MAX_CHAT_DELAY = 120;

	// Token: 0x04000AF4 RID: 2804
	public bool isTour = true;

	// Token: 0x04000AF5 RID: 2805
	public static sbyte idSelectedMini;

	// Token: 0x04000AF6 RID: 2806
	public static sbyte idCityMap;

	// Token: 0x04000AF7 RID: 2807
	public static short[] idImg;

	// Token: 0x04000AF8 RID: 2808
	public static int idMapOffline = -1;

	// Token: 0x04000AF9 RID: 2809
	public static int idMapOld = -1;

	// Token: 0x04000AFA RID: 2810
	private sbyte iGoChaSu;

	// Token: 0x04000AFB RID: 2811
	public static int idUserWedding_1;

	// Token: 0x04000AFC RID: 2812
	public static int idUserWedding_2;

	// Token: 0x02000131 RID: 305
	private class IActionExchange : IAction
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x00055F25 File Offset: 0x00054325
		public IActionExchange(StringObj strObj)
		{
			this.str = strObj;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00055F34 File Offset: 0x00054334
		public void perform()
		{
			GlobalService.gI().doRequestCmdRotate(this.str.anthor, (MapScr.focusP == null) ? -1 : MapScr.focusP.IDDB);
		}

		// Token: 0x04000AFD RID: 2813
		private StringObj str;
	}

	// Token: 0x02000132 RID: 306
	private class IActionMap : IAction
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x00055F65 File Offset: 0x00054365
		public IActionMap(int i, int type, StringObj str)
		{
			this.ii = i;
			this.str = str;
			this.type = type;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00055F84 File Offset: 0x00054384
		public void perform()
		{
			if (this.type == 0)
			{
				GlobalService.gI().doCommunicate(this.ii);
			}
			else
			{
				GlobalService.gI().doRequestCmdRotate(this.str.anthor, (MapScr.focusP == null) ? -1 : MapScr.focusP.IDDB);
			}
		}

		// Token: 0x04000AFE RID: 2814
		private int ii;

		// Token: 0x04000AFF RID: 2815
		private int type;

		// Token: 0x04000B00 RID: 2816
		private readonly StringObj str;
	}

	// Token: 0x02000133 RID: 307
	private class IActionInviteHouse : IAction
	{
		// Token: 0x060008D3 RID: 2259 RVA: 0x00055FE0 File Offset: 0x000543E0
		public IActionInviteHouse(int id)
		{
			this.idUser2 = id;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00055FEF File Offset: 0x000543EF
		public void perform()
		{
			ParkService.gI().doInviteToMyHome(1, this.idUser2);
			Canvas.startWaitDlg();
		}

		// Token: 0x04000B01 RID: 2817
		private int idUser2;
	}

	// Token: 0x02000134 RID: 308
	private class CommandGift : Command
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x00056007 File Offset: 0x00054407
		public CommandGift(string name, MapScr.IActionGift ac, Part part, int i) : base(name, ac)
		{
			this.part = part;
			this.ii = i;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00056020 File Offset: 0x00054420
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus)
			{
				PopupShop.resetIsTrans();
				string text = string.Empty;
				if ((int)this.part.zOrder == 20)
				{
					text = T.cloth;
				}
				else if ((int)this.part.zOrder == 10)
				{
					text = T.pant;
				}
				text += this.part.name;
				PopupShop.addStr(text);
				PopupShop.addStr(Canvas.getPriceMoney(this.part.price[0], this.part.price[1], false));
				PopupShop.addStr(T.level[0] + ((APartInfo)this.part).level);
				PopupShop.addStr(T.moneyStr + GameMidlet.avatar.strMoney);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00056105 File Offset: 0x00054505
		public override void paint(MyGraphics g, int x, int y)
		{
			this.part.paint(g, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
		}

		// Token: 0x04000B02 RID: 2818
		private Part part;

		// Token: 0x04000B03 RID: 2819
		private int ii;
	}

	// Token: 0x02000135 RID: 309
	private class IActionGift : IAction
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x00056126 File Offset: 0x00054526
		public IActionGift(short id)
		{
			this.idPart = id;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00056135 File Offset: 0x00054535
		public void perform()
		{
			Canvas.endDlg();
			MapScr.gI().doGiving((int)this.idPart);
			PopupShop.gI().close();
		}

		// Token: 0x04000B04 RID: 2820
		private short idPart;
	}

	// Token: 0x02000136 RID: 310
	private class CommandGiftDef : Command
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x00056156 File Offset: 0x00054556
		public CommandGiftDef(string name, MapScr.IActionGiftDef ac, int i, ItemEffectInfo item, int count) : base(name, ac)
		{
			this.ii = i;
			this.item = item;
			this.count = count;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00056178 File Offset: 0x00054578
		public override void update()
		{
			if (PopupShop.isTransFocus && PopupShop.focus - this.count == this.ii)
			{
				PopupShop.resetIsTrans();
				PopupShop.addStr(T.nameStr + this.item.name);
				PopupShop.addStr(T.priceStr + this.item.money + (((int)this.item.typeMoney != 0) ? T.gold : T.xu));
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00056204 File Offset: 0x00054604
		public override void paint(MyGraphics g, int x, int y)
		{
			AvatarData.paintImg(g, (int)this.item.IDIcon, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
		}

		// Token: 0x04000B05 RID: 2821
		private int ii;

		// Token: 0x04000B06 RID: 2822
		private int count;

		// Token: 0x04000B07 RID: 2823
		private string price;

		// Token: 0x04000B08 RID: 2824
		private ItemEffectInfo item;
	}

	// Token: 0x02000137 RID: 311
	private class IActionGiftDef : IAction
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0005622A File Offset: 0x0005462A
		public IActionGiftDef(int i, short id)
		{
			this.ii = i;
			this.id = id;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00056240 File Offset: 0x00054640
		public void perform()
		{
			if (this.ii != 0 || (int)LoadMap.weather == -1)
			{
				MapScr.doGivingDefferent((int)this.id);
			}
			PopupShop.gI().close();
		}

		// Token: 0x04000B09 RID: 2825
		private int ii;

		// Token: 0x04000B0A RID: 2826
		private short id;
	}

	// Token: 0x02000138 RID: 312
	private class CommandAction : Command
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x0005626E File Offset: 0x0005466E
		public CommandAction(string name, MapScr.IActionSelectAction action, int index) : base(name, action)
		{
			this.index = index;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00056280 File Offset: 0x00054680
		public override void paint(MyGraphics g, int x, int y)
		{
			int idx = 0;
			int idy = this.index;
			if (this.index >= Menu.imgSellect.nFrame)
			{
				idx = 1;
				idy = this.index % Menu.imgSellect.nFrame;
			}
			Menu.imgSellect.drawFrameXY(idx, idy, x, y, 3, g);
		}

		// Token: 0x04000B0B RID: 2827
		public int index;
	}

	// Token: 0x02000139 RID: 313
	private class IActionSelectAction : IAction
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x000562CF File Offset: 0x000546CF
		public IActionSelectAction(sbyte id)
		{
			this.id = id;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000562DE File Offset: 0x000546DE
		public void perform()
		{
			if ((int)GameMidlet.avatar.action != 2)
			{
				MapScr.doAction(this.id);
			}
		}

		// Token: 0x04000B0C RID: 2828
		private sbyte id;
	}

	// Token: 0x0200013A RID: 314
	private class CommandFeel : Command
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x000562FC File Offset: 0x000546FC
		public CommandFeel(string name, MapScr.IActionFeel a, sbyte id) : base(name, a)
		{
			this.id = id;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00056310 File Offset: 0x00054710
		public override void paint(MyGraphics g, int x, int y)
		{
			APartInfo apartInfo = (APartInfo)AvatarData.getPart(0);
			apartInfo.paint(g, x + 2 + (int)apartInfo.dx[0] * AvMain.hd, y + 21 + 20 * (AvMain.hd - 1) + (int)apartInfo.dy[0] * AvMain.hd, 0);
			APartInfo apartInfo2 = (APartInfo)AvatarData.getPart((short)this.id);
			apartInfo2.paint(g, x + 2 + (int)apartInfo2.dx[0] * AvMain.hd, y + 21 + 20 * (AvMain.hd - 1) + (int)apartInfo2.dy[0] * AvMain.hd, 0);
		}

		// Token: 0x04000B0D RID: 2829
		private sbyte id;
	}

	// Token: 0x0200013B RID: 315
	private class IActionFeel : IAction
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x000563AF File Offset: 0x000547AF
		public IActionFeel(int ii, sbyte id)
		{
			this.id = id;
			this.ii = ii;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000563C5 File Offset: 0x000547C5
		public void perform()
		{
			if (this.ii == 0)
			{
				MapScr.doSellectFeel(4);
			}
			else
			{
				MapScr.doSellectFeel((int)this.id);
			}
		}

		// Token: 0x04000B0E RID: 2830
		private int ii;

		// Token: 0x04000B0F RID: 2831
		private sbyte id;
	}

	// Token: 0x0200013C RID: 316
	private class IActionGiving : IAction
	{
		// Token: 0x060008E7 RID: 2279 RVA: 0x000563E9 File Offset: 0x000547E9
		public IActionGiving(APartInfo a, int t)
		{
			this.ava = a;
			this.type = t;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000563FF File Offset: 0x000547FF
		public void perform()
		{
			if (MapScr.focusP != null)
			{
				ParkService.gI().doGiftGiving(MapScr.focusP.IDDB, (int)this.ava.IDPart, this.type);
			}
		}

		// Token: 0x04000B10 RID: 2832
		private APartInfo ava;

		// Token: 0x04000B11 RID: 2833
		private int type;
	}

	// Token: 0x0200013D RID: 317
	private class IActionAddFriend5 : IAction
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x00056430 File Offset: 0x00054830
		public IActionAddFriend5(Avatar a, string text)
		{
			this.ava = a;
			this.text = text;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00056446 File Offset: 0x00054846
		public void perform()
		{
			Canvas.msgdlg.setInfoLR(this.text, new Command(T.agree, new MapScr.IActionAddFriend(this.ava)), new Command(T.refused, new MapScr.IActionAddFriend1(this.ava)));
		}

		// Token: 0x04000B12 RID: 2834
		private Avatar ava;

		// Token: 0x04000B13 RID: 2835
		private string text;
	}

	// Token: 0x0200013E RID: 318
	private class IActionAddFriend1 : IAction
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x00056482 File Offset: 0x00054882
		public IActionAddFriend1(Avatar p)
		{
			this.p = p;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00056491 File Offset: 0x00054891
		public void perform()
		{
			ParkService.gI().doAddFriend(this.p.IDDB, false);
		}

		// Token: 0x04000B14 RID: 2836
		private Avatar p;
	}

	// Token: 0x0200013F RID: 319
	private class IActionAddFriend : IAction
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x000564A9 File Offset: 0x000548A9
		public IActionAddFriend(Avatar p)
		{
			this.p = p;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x000564B8 File Offset: 0x000548B8
		public void perform()
		{
			if (ListScr.friendL != null)
			{
				ListScr.gI().removeList();
			}
			ParkService.gI().doAddFriend(this.p.IDDB, true);
			Canvas.startOKDlg(T.addFriend + T.with + this.p.name + ".");
		}

		// Token: 0x04000B15 RID: 2837
		private Avatar p;
	}

	// Token: 0x02000140 RID: 320
	private class IActionMenuUpdateChest : IAction
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x00056513 File Offset: 0x00054913
		public IActionMenuUpdateChest(MyVector list, int type, int typeScr)
		{
			this.list = list;
			this.type = type;
			this.typeScr = typeScr;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00056530 File Offset: 0x00054930
		public void perform()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(T.removee, new MapScr.IActionDellPart(this.list, this.type, this.typeScr)));
			myVector.addElement(new Command(T.upgradeChest, new MapScr.IactionUpdateChest()));
			MenuCenter.gI().startAt(myVector);
		}

		// Token: 0x04000B16 RID: 2838
		private MyVector list;

		// Token: 0x04000B17 RID: 2839
		private int type;

		// Token: 0x04000B18 RID: 2840
		private int typeScr;
	}

	// Token: 0x02000141 RID: 321
	private class IactionUpdateChest : IAction
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x00056592 File Offset: 0x00054992
		public void perform()
		{
			Canvas.startOKDlg(T.doYouWantUpgradeCoffer, new MapScr.IActionUpgrade());
		}
	}

	// Token: 0x02000142 RID: 322
	private class IActionDellPart : IAction
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x000565A3 File Offset: 0x000549A3
		public IActionDellPart(MyVector list, int type, int typeScr)
		{
			this.list = list;
			this.typeScr = typeScr;
			this.type = type;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000565C0 File Offset: 0x000549C0
		public void perform()
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < this.list.size(); i++)
			{
				SeriPart seriPart = (SeriPart)this.list.elementAt(i);
				Part part = AvatarData.getPart(seriPart.idPart);
				if (part != null && (int)part.zOrder != 30 && (int)part.zOrder != 40)
				{
					myVector.addElement(seriPart);
				}
			}
			if (PopupShop.focus >= myVector.size())
			{
				return;
			}
			SeriPart seriPart2 = (SeriPart)myVector.elementAt(PopupShop.focus);
			Part part2 = AvatarData.getPart(seriPart2.idPart);
			if (!AvatarData.isZOrderMain((int)part2.zOrder) || this.type == 1)
			{
				Canvas.startOKDlg(T.doYouWantDel, new MapScr.IActionDel(this.list, this.type, this.typeScr, part2, myVector, seriPart2, MapScr.instance));
			}
		}

		// Token: 0x04000B19 RID: 2841
		private MyVector list;

		// Token: 0x04000B1A RID: 2842
		private int type;

		// Token: 0x04000B1B RID: 2843
		private int typeScr;
	}

	// Token: 0x02000143 RID: 323
	private class IActionDel : IAction
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x000566B0 File Offset: 0x00054AB0
		public IActionDel(MyVector list, int type, int typeScr, Part p, MyVector lis, SeriPart seri, MapScr me)
		{
			this.me = me;
			this.se = seri;
			this.part = p;
			this.lis = lis;
			this.list = list;
			this.type = type;
			this.typeScr = typeScr;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000566F0 File Offset: 0x00054AF0
		public void perform()
		{
			GlobalService.gI().doRemoveItem((int)this.part.IDPart, this.type);
			this.lis.removeElementAt(PopupShop.focus);
			this.list.removeElement(this.se);
			if (this.typeScr == 0)
			{
				if (MainMenu.gI().isWearing)
				{
					MainMenu.gI().doWearing();
				}
				else
				{
					this.me.doStore();
				}
			}
			else
			{
				HouseScr.gI().restartPopup();
			}
		}

		// Token: 0x04000B1C RID: 2844
		private Part part;

		// Token: 0x04000B1D RID: 2845
		private MyVector list;

		// Token: 0x04000B1E RID: 2846
		private int type;

		// Token: 0x04000B1F RID: 2847
		private int typeScr;

		// Token: 0x04000B20 RID: 2848
		private MyVector lis;

		// Token: 0x04000B21 RID: 2849
		private SeriPart se;

		// Token: 0x04000B22 RID: 2850
		private MapScr me;
	}

	// Token: 0x02000144 RID: 324
	private class IAction111 : IAction
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0005677C File Offset: 0x00054B7C
		public IAction111(int type, SeriPart se, int id, int ii)
		{
			this.type = type;
			this.seri = se;
			this.id = id;
			this.ii = ii;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000567A1 File Offset: 0x00054BA1
		public void perform()
		{
			Canvas.startOKDlg(T.trasContainter[this.type], new MapScr.IAction222(this.type, this.seri, this.id, this.ii));
		}

		// Token: 0x04000B23 RID: 2851
		private int type;

		// Token: 0x04000B24 RID: 2852
		private int id;

		// Token: 0x04000B25 RID: 2853
		private int ii;

		// Token: 0x04000B26 RID: 2854
		private SeriPart seri;
	}

	// Token: 0x02000145 RID: 325
	private class IAction222 : IAction
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x000567D1 File Offset: 0x00054BD1
		public IAction222(int type, SeriPart se, int id, int ii)
		{
			this.type = type;
			this.seri = se;
			this.id = id;
			this.ii = ii;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000567F8 File Offset: 0x00054BF8
		public void perform()
		{
			Part part = AvatarData.getPart(this.seri.idPart);
			if (this.id == GameMidlet.avatar.IDDB && (!AvatarData.isZOrderMain((int)part.zOrder) || this.type != 0))
			{
				if (this.type == 2)
				{
					GlobalService.gI().doTransChestPart(1, this.ii, this.seri.idPart);
				}
				else if (this.type == 3)
				{
					GlobalService.gI().doTransChestPart(0, this.ii, this.seri.idPart);
				}
				else
				{
					GlobalService.gI().doUsingItem(this.seri.idPart, (sbyte)this.type);
				}
				Canvas.startWaitDlg();
			}
		}

		// Token: 0x04000B27 RID: 2855
		private int type;

		// Token: 0x04000B28 RID: 2856
		private int id;

		// Token: 0x04000B29 RID: 2857
		private int ii;

		// Token: 0x04000B2A RID: 2858
		private SeriPart seri;
	}

	// Token: 0x02000146 RID: 326
	private class IActionTransItem : IAction
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x000568C2 File Offset: 0x00054CC2
		public IActionTransItem(int ty, SeriPart seri)
		{
			this.type = ty;
			this.se = seri;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000568D8 File Offset: 0x00054CD8
		public void perform()
		{
			Canvas.startOKDlg(T.trasContainter[this.type], new MapScr.IActionDoUsingItem(this.type, this.se));
		}

		// Token: 0x04000B2B RID: 2859
		private SeriPart se;

		// Token: 0x04000B2C RID: 2860
		private int type;
	}

	// Token: 0x02000147 RID: 327
	private class IActionDoUsingItem : IAction
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x000568FC File Offset: 0x00054CFC
		public IActionDoUsingItem(int ty, SeriPart seri)
		{
			this.type = ty;
			this.se = seri;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00056912 File Offset: 0x00054D12
		public void perform()
		{
			GlobalService.gI().doUsingItem(this.se.idPart, (sbyte)this.type);
			Canvas.startWaitDlg();
		}

		// Token: 0x04000B2D RID: 2861
		private SeriPart se;

		// Token: 0x04000B2E RID: 2862
		private int type;
	}

	// Token: 0x02000148 RID: 328
	private class CommandUsingPart : Command
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x00056935 File Offset: 0x00054D35
		public CommandUsingPart(string name, IAction ac, SeriPart seri, int i, int ty) : base(name, ac)
		{
			this.se = seri;
			this.ii = i;
			this.type = ty;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00056958 File Offset: 0x00054D58
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus)
			{
				Part part = AvatarData.getPart(this.se.idPart);
				PopupShop.resetIsTrans();
				string text = string.Empty;
				text += AvatarData.getName(part);
				PopupShop.addStr(text);
				PopupShop.addStr(T.doBen + (100 - (int)this.se.time) + "%");
				if (this.se.expireString != null && !this.se.expireString.Equals(string.Empty))
				{
					PopupShop.addStr(this.se.expireString);
				}
				if (this.type == 0)
				{
					PopupShop.addStr(T.level[2] + ": " + AvatarData.getLevel(part));
				}
				else if (part.follow != -2)
				{
					int num;
					if (part.follow >= 0)
					{
						num = (int)((APartInfo)AvatarData.getPart(part.follow)).level;
					}
					else
					{
						num = (int)((APartInfo)part).level;
					}
					PopupShop.addStr(T.level[2] + ": " + num);
				}
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00056AA4 File Offset: 0x00054EA4
		public override void paint(MyGraphics g, int x, int y)
		{
			Part part = AvatarData.getPart(this.se.idPart);
			part.paint(g, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
			int num = (100 - (int)this.se.time) * PopupShop.imgTimeUse[0].w / 100;
			g.drawImage(PopupShop.imgTimeUse[0], (float)(x + 6 * AvMain.hd), (float)(y + PopupShop.wCell - 6 * AvMain.hd - 4 * AvMain.hd), 0);
			int num2 = num;
			if (num2 >= PopupShop.imgTimeUsePer.frameWidth * 2)
			{
				PopupShop.imgTimeUsePer.drawFrame(0, x + 6 * AvMain.hd, y + PopupShop.wCell - 6 * AvMain.hd - 4 * AvMain.hd, 0, g);
				PopupShop.imgTimeUsePer.drawFrame(1, x + 6 * AvMain.hd + num2 - PopupShop.imgTimeUsePer.frameWidth, y + PopupShop.wCell - 6 * AvMain.hd - 4 * AvMain.hd, 0, g);
				g.drawImageScaleClip(PopupShop.imgTimeUse[1], (float)(x + 6 * AvMain.hd + PopupShop.imgTimeUsePer.frameWidth), (float)(y + PopupShop.wCell - 6 * AvMain.hd - 4 * AvMain.hd), (float)(num2 - PopupShop.imgTimeUsePer.frameWidth * 2), (float)PopupShop.imgTimeUse[1].getHeight(), 0);
			}
		}

		// Token: 0x04000B2F RID: 2863
		private SeriPart se;

		// Token: 0x04000B30 RID: 2864
		private int ii;

		// Token: 0x04000B31 RID: 2865
		private int type;
	}

	// Token: 0x02000149 RID: 329
	private class IActionUsingPart : IAction
	{
		// Token: 0x06000902 RID: 2306 RVA: 0x00056BFE File Offset: 0x00054FFE
		public IActionUsingPart(int ii, int type, SeriPart seri, int id)
		{
			this.ii = ii;
			this.type = type;
			this.se = seri;
			this.id = id;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00056C24 File Offset: 0x00055024
		public void perform()
		{
			Part part = AvatarData.getPart(this.se.idPart);
			if (this.id == GameMidlet.avatar.IDDB && (!AvatarData.isZOrderMain((int)part.zOrder) || this.type != 0))
			{
				Canvas.startOKDlg(T.trasContainter[this.type], new MapScr.IActionTransContainer(this.ii, this.se, this.type));
			}
		}

		// Token: 0x04000B32 RID: 2866
		private int ii;

		// Token: 0x04000B33 RID: 2867
		private int type;

		// Token: 0x04000B34 RID: 2868
		private SeriPart se;

		// Token: 0x04000B35 RID: 2869
		private int id;
	}

	// Token: 0x0200014A RID: 330
	private class IActionTransContainer : IAction
	{
		// Token: 0x06000904 RID: 2308 RVA: 0x00056C9B File Offset: 0x0005509B
		public IActionTransContainer(int i, SeriPart seri, int type)
		{
			this.ii = i;
			this.se = seri;
			this.type = type;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00056CB8 File Offset: 0x000550B8
		public void perform()
		{
			if (this.type == 2)
			{
				GlobalService.gI().doTransChestPart(1, this.ii, this.se.idPart);
			}
			else if (this.type == 3)
			{
				GlobalService.gI().doTransChestPart(0, this.ii, this.se.idPart);
			}
			else
			{
				GlobalService.gI().doUsingItem(this.se.idPart, (sbyte)this.type);
			}
			Canvas.startWaitDlg();
		}

		// Token: 0x04000B36 RID: 2870
		private int ii;

		// Token: 0x04000B37 RID: 2871
		private int type;

		// Token: 0x04000B38 RID: 2872
		private SeriPart se;
	}

	// Token: 0x0200014B RID: 331
	private class IActionEmpty : IAction
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x00056D48 File Offset: 0x00055148
		public void perform()
		{
		}
	}

	// Token: 0x0200014C RID: 332
	private class IActionUpgrade : IAction
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x00056D52 File Offset: 0x00055152
		public void perform()
		{
			GlobalService.gI().doUpdateContainer(0);
			Canvas.startWaitDlg();
		}
	}

	// Token: 0x0200014D RID: 333
	private class CommandIceDream : Command
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x00056D64 File Offset: 0x00055164
		public CommandIceDream(string name, MapScr.IActionIceDream a, Item it, int i) : base(name, a)
		{
			this.item = it;
			this.ii = i;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00056D80 File Offset: 0x00055180
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus)
			{
				PopupShop.resetIsTrans();
				PopupShop.addStr(this.item.name);
				PopupShop.addStr(T.priceStr + Canvas.getMoneys(this.item.price[0]) + T.dola);
				PopupShop.addStr(T.have + GameMidlet.avatar.strMoney);
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00056DFB File Offset: 0x000551FB
		public override void paint(MyGraphics g, int x, int y)
		{
			AvatarData.listImgInfo[(int)this.item.idIcon].paintPart(g, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
		}

		// Token: 0x04000B39 RID: 2873
		private Item item;

		// Token: 0x04000B3A RID: 2874
		private int ii;
	}

	// Token: 0x0200014E RID: 334
	private class IActionIceDream : IAction
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x00056E27 File Offset: 0x00055227
		public IActionIceDream(Item it)
		{
			this.item = it;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00056E36 File Offset: 0x00055236
		public void perform()
		{
			MapScr.doBuyIceDream(this.item);
		}

		// Token: 0x04000B3B RID: 2875
		private readonly Item item;
	}

	// Token: 0x0200014F RID: 335
	private class IActionBuyDream : IAction
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x00056E43 File Offset: 0x00055243
		public IActionBuyDream(Item it)
		{
			this.item = it;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00056E52 File Offset: 0x00055252
		public void perform()
		{
			ParkService.gI().doBuyItem(this.item.ID);
			Canvas.startWaitDlg();
		}

		// Token: 0x04000B3C RID: 2876
		private Item item;
	}

	// Token: 0x02000150 RID: 336
	private class CommandOpenShop : Command
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x00056E6E File Offset: 0x0005526E
		public CommandOpenShop(string name, MapScr.IActionOpenShop a, Part p, short idPart, int i, int idB, int idShop) : base(name, a)
		{
			this.ava = p;
			this.IDPart = idPart;
			this.ii = i;
			this.idBoss1 = idB;
			this.idShop = (short)idShop;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00056EA0 File Offset: 0x000552A0
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus)
			{
				Part part = this.ava;
				if (this.ava.IDPart == -1)
				{
					part = AvatarData.getPart(this.IDPart);
				}
				if (part.IDPart != -1)
				{
					MapScr.setAvatarShop(part);
					PopupShop.resetIsTrans();
					PopupShop.addStr(part.name);
					if (this.idBoss1 == -1)
					{
						PopupShop.addStr(Canvas.getPriceMoney(part.price[0], part.price[1], false));
					}
					if (part.follow == -1)
					{
						PopupShop.addStr(T.level[0] + ((APartInfo)part).level);
					}
				}
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00056F64 File Offset: 0x00055364
		public override void paint(MyGraphics g, int x, int y)
		{
			Part part = this.ava;
			if (this.ava.IDPart == -1)
			{
				part = AvatarData.getPart(this.IDPart);
			}
			if (part.IDPart != -1)
			{
				part.paint(g, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
			}
		}

		// Token: 0x04000B3D RID: 2877
		private readonly Part ava;

		// Token: 0x04000B3E RID: 2878
		private readonly short IDPart;

		// Token: 0x04000B3F RID: 2879
		private readonly short idShop;

		// Token: 0x04000B40 RID: 2880
		private readonly int ii;

		// Token: 0x04000B41 RID: 2881
		private readonly int idBoss1;
	}

	// Token: 0x02000151 RID: 337
	private class CommandOpenShop2 : Command
	{
		// Token: 0x06000914 RID: 2324 RVA: 0x00056FBB File Offset: 0x000553BB
		public CommandOpenShop2(string name, MapScr.IActionOpenShop2 a, int i, short idP, string text) : base(name, a)
		{
			this.ii = i;
			this.idPart = idP;
			this.textDes = text;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00056FDC File Offset: 0x000553DC
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus)
			{
				PopupShop.resetIsTrans();
				PopupShop.addStr(this.textDes);
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00057008 File Offset: 0x00055408
		public override void paint(MyGraphics g, int x, int y)
		{
			AvatarData.paintImg(g, (int)this.idPart, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
		}

		// Token: 0x04000B42 RID: 2882
		private int ii;

		// Token: 0x04000B43 RID: 2883
		private short idPart;

		// Token: 0x04000B44 RID: 2884
		private string textDes;
	}

	// Token: 0x02000152 RID: 338
	private class IActionOpenShop2 : IAction
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x00057029 File Offset: 0x00055429
		public IActionOpenShop2(int idB, int idShop, int i)
		{
			this.idBoss1 = idB;
			this.idShop = idShop;
			this.ii = i;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00057046 File Offset: 0x00055446
		public void perform()
		{
			ParkService.gI().doBossShop(this.idBoss1, this.idShop, this.ii);
			Canvas.endDlg();
		}

		// Token: 0x04000B45 RID: 2885
		private readonly int idBoss1;

		// Token: 0x04000B46 RID: 2886
		private readonly int idShop;

		// Token: 0x04000B47 RID: 2887
		private readonly int ii;
	}

	// Token: 0x02000153 RID: 339
	private class IActionOpenShop : IAction
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x00057069 File Offset: 0x00055469
		public IActionOpenShop(Part p, short listPar, int idShop, string textDes, int idB, int i)
		{
			this.ava = p;
			this.IDPart = listPar;
			this.idShop = idShop;
			this.textDes = textDes;
			this.idBoss = idB;
			this.ii = i;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000570A0 File Offset: 0x000554A0
		public void perform()
		{
			if (this.idShop == 100)
			{
				Canvas.startOKDlg(T.doYouWantDial, new MapScr.IActionDial(this.IDPart));
			}
			else if (this.idShop == 26)
			{
				Canvas.endDlg();
				MapScr.gI().doGiving((int)this.IDPart);
				PopupShop.gI().close();
			}
			else
			{
				Part part = this.ava;
				if (this.ava.IDPart == -1)
				{
					part = AvatarData.getPart(this.IDPart);
				}
				short idpart = part.IDPart;
				if (this.idBoss == -1 || this.idShop == 17 || this.idShop == 18)
				{
					MapScr.doSelectMoneyBuyItem(part);
				}
				else
				{
					Canvas.startOKDlg(this.textDes, new MapScr.IActionTextDes(this.idBoss, this.idShop, this.ii));
				}
			}
		}

		// Token: 0x04000B48 RID: 2888
		private readonly Part ava;

		// Token: 0x04000B49 RID: 2889
		private readonly short IDPart;

		// Token: 0x04000B4A RID: 2890
		private readonly int idBoss;

		// Token: 0x04000B4B RID: 2891
		private readonly string textDes;

		// Token: 0x04000B4C RID: 2892
		private readonly int idShop;

		// Token: 0x04000B4D RID: 2893
		private readonly int ii;
	}

	// Token: 0x02000154 RID: 340
	private class IActionTextDes : IAction
	{
		// Token: 0x0600091B RID: 2331 RVA: 0x00057183 File Offset: 0x00055583
		public IActionTextDes(int idB, int idShop, int i)
		{
			this.idBoss1 = idB;
			this.idShop = idShop;
			this.ii = i;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000571A0 File Offset: 0x000555A0
		public void perform()
		{
			ParkService.gI().doBossShop(this.idBoss1, this.idShop, this.ii);
		}

		// Token: 0x04000B4E RID: 2894
		private readonly int idBoss1;

		// Token: 0x04000B4F RID: 2895
		private readonly int idShop;

		// Token: 0x04000B50 RID: 2896
		private readonly int ii;
	}

	// Token: 0x02000155 RID: 341
	private class IActionDial : IAction
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x000571BE File Offset: 0x000555BE
		public IActionDial(short id)
		{
			this.id = id;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000571CD File Offset: 0x000555CD
		public void perform()
		{
			PopupShop.gI().close();
			DialLuckyScr.gI().switchToMe(Canvas.currentMyScreen, this.id);
		}

		// Token: 0x04000B51 RID: 2897
		private readonly short id;
	}

	// Token: 0x02000156 RID: 342
	private class IActionPopup1 : IAction
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x000571EE File Offset: 0x000555EE
		public IActionPopup1(string text5, string[] subText, int idBoss, int idPopup, int type)
		{
			this.text5 = text5;
			this.subText = subText;
			this.idBoss = idBoss;
			this.idPopup = idPopup;
			this.type = type;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0005721C File Offset: 0x0005561C
		public void perform()
		{
			int num = this.type;
			if (num != 1)
			{
				if (num != 2)
				{
					if (num == 3)
					{
						Canvas.msgdlg.setInfoLCR(this.text5, new Command(this.subText[0], new MapScr.IActionCustomPopup(this.idBoss, this.idPopup, 0)), new Command(this.subText[1], new MapScr.IActionCustomPopup(this.idBoss, this.idPopup, 1)), new Command(this.subText[2], new MapScr.IActionCustomPopup(this.idBoss, this.idPopup, 2)));
					}
				}
				else
				{
					Canvas.msgdlg.setInfoLR(this.text5, new Command(this.subText[0], new MapScr.IActionCustomPopup(this.idBoss, this.idPopup, 0)), new Command(this.subText[1], new MapScr.IActionCustomPopup(this.idBoss, this.idPopup, 1)));
				}
			}
			else
			{
				Canvas.msgdlg.setInfoC(this.text5, new Command(this.subText[0], new MapScr.IActionCustomPopup(this.idBoss, this.idPopup, 0)));
			}
		}

		// Token: 0x04000B52 RID: 2898
		private string text5;

		// Token: 0x04000B53 RID: 2899
		private int idBoss;

		// Token: 0x04000B54 RID: 2900
		private int idPopup;

		// Token: 0x04000B55 RID: 2901
		private int type;

		// Token: 0x04000B56 RID: 2902
		private string[] subText;
	}

	// Token: 0x02000157 RID: 343
	private class IActionCustomPopup : IAction
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x00057343 File Offset: 0x00055743
		public IActionCustomPopup(int idB, int idPopup, int i)
		{
			this.idBoss = idB;
			this.idPopup = idPopup;
			this.ii = i;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00057360 File Offset: 0x00055760
		public void perform()
		{
			ParkService.gI().doCustomPopup(this.idBoss, this.idPopup, this.ii);
		}

		// Token: 0x04000B57 RID: 2903
		private readonly int idBoss;

		// Token: 0x04000B58 RID: 2904
		private readonly int idPopup;

		// Token: 0x04000B59 RID: 2905
		private readonly int ii;
	}

	// Token: 0x02000158 RID: 344
	private class CommandMenuRotate : Command
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x0005737E File Offset: 0x0005577E
		public CommandMenuRotate(string name, MapScr.IActionMenuRotate a, int id) : base(name, a)
		{
			this.idIcon = id;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0005738F File Offset: 0x0005578F
		public override void paint(MyGraphics g, int x, int y)
		{
			AvatarData.paintImg(g, this.idIcon, x, y, 3);
		}

		// Token: 0x04000B5A RID: 2906
		private readonly int idIcon;
	}

	// Token: 0x02000159 RID: 345
	private class IActionMenuRotate : IAction
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x000573A0 File Offset: 0x000557A0
		public IActionMenuRotate(StringObj str)
		{
			this.str2 = str;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000573B0 File Offset: 0x000557B0
		public void perform()
		{
			if (MapScr.focusP != null)
			{
				GlobalService.gI().doRequestCmdRotate(this.str2.anthor, MapScr.focusP.IDDB);
			}
			else
			{
				GlobalService.gI().doRequestCmdRotate(this.str2.anthor, -1);
			}
		}

		// Token: 0x04000B5B RID: 2907
		private readonly StringObj str2;
	}

	// Token: 0x0200015A RID: 346
	private class IActionMiniMapKey : IAction
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x00057409 File Offset: 0x00055809
		public void perform()
		{
		}
	}

	// Token: 0x0200015B RID: 347
	private class IActionChangePass : IAction
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x0005740B File Offset: 0x0005580B
		public IActionChangePass(TField[] tf)
		{
			this.tf_P = tf;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0005741C File Offset: 0x0005581C
		public void perform()
		{
			if (MapScr.setEnterPass(this.tf_P))
			{
				GlobalService.gI().doChangePass(this.tf_P[0].getText(), this.tf_P[1].getText());
				Canvas.startWaitDlg();
				InputFace.gI().close();
			}
		}

		// Token: 0x04000B5C RID: 2908
		private readonly TField[] tf_P;
	}

	// Token: 0x0200015C RID: 348
	private class CommandShopOffline : Command
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x0005746C File Offset: 0x0005586C
		public CommandShopOffline(string name, MapScr.IActionShopOffline ac, Part ava, int count) : base(name, ac)
		{
			this.ava = ava;
			this.cou = count;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00057485 File Offset: 0x00055885
		public override void paint(MyGraphics g, int x, int y)
		{
			this.ava.paintIcon(g, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 0, 3);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x000574A8 File Offset: 0x000558A8
		public override void update()
		{
			if (this.cou == PopupShop.focus)
			{
				PopupShop.resetIsTrans();
				MapScr.setAvatarShop(this.ava);
				string text = string.Empty;
				text += AvatarData.getName(this.ava);
				PopupShop.addStr(text);
				PopupShop.addStr(Canvas.getPriceMoney(this.ava.price[0], this.ava.price[1], false));
				PopupShop.addStr(T.levelRequest + AvatarData.getLevel(this.ava));
				PopupShop.addStr(T.level[0] + GameMidlet.myIndexP.level);
			}
		}

		// Token: 0x04000B5D RID: 2909
		private Part ava;

		// Token: 0x04000B5E RID: 2910
		private int cou;
	}

	// Token: 0x0200015D RID: 349
	private class IActionShopOffline : IAction
	{
		// Token: 0x0600092E RID: 2350 RVA: 0x00057557 File Offset: 0x00055957
		public IActionShopOffline(short idPart)
		{
			this.idPart = idPart;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00057566 File Offset: 0x00055966
		public void perform()
		{
			MapScr.doBuyItem((int)this.idPart);
		}

		// Token: 0x04000B5F RID: 2911
		private short idPart;
	}

	// Token: 0x0200015E RID: 350
	private class IActionSelectedMoney : IAction
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x00057573 File Offset: 0x00055973
		public IActionSelectedMoney(short idPart, int type)
		{
			this.idPart = idPart;
			this.type = type;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00057589 File Offset: 0x00055989
		public void perform()
		{
			AvatarService.gI().doBuyItem((int)this.idPart, this.type);
		}

		// Token: 0x04000B60 RID: 2912
		private short idPart;

		// Token: 0x04000B61 RID: 2913
		private int type;
	}

	// Token: 0x0200015F RID: 351
	private class IActionChat : IKbAction
	{
		// Token: 0x06000933 RID: 2355 RVA: 0x000575A9 File Offset: 0x000559A9
		public void perform(string text)
		{
			if (text.Equals(string.Empty))
			{
				return;
			}
			Canvas.currentMyScreen.doChat(text);
		}
	}

	// Token: 0x02000160 RID: 352
	private class IActionWedding : IAction
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x000575C7 File Offset: 0x000559C7
		public IActionWedding(short idActionWedding)
		{
			this.idActionWedding = idActionWedding;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000575D6 File Offset: 0x000559D6
		public void perform()
		{
			GlobalService.gI().doRequestCmdRotate((int)this.idActionWedding, -1);
			PopupShop.gI().close();
			Canvas.startWaitDlg();
		}

		// Token: 0x04000B62 RID: 2914
		private short idActionWedding;
	}

	// Token: 0x02000161 RID: 353
	private class IActionInfo : IAction
	{
		// Token: 0x06000937 RID: 2359 RVA: 0x00057600 File Offset: 0x00055A00
		public void perform()
		{
		}
	}

	// Token: 0x02000162 RID: 354
	private class CommandInfo : Command
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x00057604 File Offset: 0x00055A04
		public CommandInfo(string caption, IAction action, Avatar avatar, Avatar friend, string tenQuanHe, sbyte lv, sbyte perLV, int idImage) : base(caption, action)
		{
			this.avatar = avatar;
			this.avatar.direct = Base.RIGHT;
			this.friend = friend;
			this.idImage = idImage;
			this.tenQuanHe = tenQuanHe;
			this.level = lv;
			this.perLevel = perLV;
			this.w = PopupShop.w;
			this.h = PopupShop.h;
			this.imgIcon = new Image[5];
			for (int i = 0; i < 5; i++)
			{
				this.imgIcon[i] = Image.createImagePNG(T.getPath() + "/myinfo/icon" + i);
			}
			this.imgShadow = Image.createImagePNG(T.getPath() + "/myinfo/shadow");
			this.imgBack = Image.createImagePNG(T.getPath() + "/myinfo/back");
			this.imgThanh = new Image[3];
			for (int j = 0; j < 3; j++)
			{
				this.imgThanh[j] = Image.createImagePNG(T.getPath() + "/myinfo/thanh" + j);
			}
			this.hIndex = this.imgThanh[2].h + 10 * AvMain.hd;
			if (AvMain.hd == 1)
			{
				this.yLine = this.h - (this.hIndex * 3 + 48);
			}
			else
			{
				this.yLine = this.h - (this.hIndex * 3 + 84);
			}
			for (int k = 0; k < 3; k++)
			{
				string st = string.Empty;
				if (k == 0 && avatar.lvFarm != -1)
				{
					st = "Lv NT " + avatar.lvFarm;
				}
				else
				{
					st = T.myIndex[k] + ((k != 0) ? string.Empty : (string.Empty + avatar.lvMain));
				}
				int width = Canvas.fontBlu.getWidth(st);
				if (width > this.xLeft)
				{
					this.xLeft = width;
				}
				width = Canvas.fontBlu.getWidth(avatar.indexP[2 + k] + string.Empty);
				if (width > this.xRight)
				{
					this.xRight = width;
				}
			}
			this.xLeft -= 4 * AvMain.hd;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00057874 File Offset: 0x00055C74
		public override void update()
		{
			if (PopupShop.gI().left != null)
			{
				PopupShop.gI().left.x = PopupShop.x + this.w - this.imgBack.w / 2 - 20 * AvMain.hd;
				PopupShop.gI().left.y = PopupShop.y + this.yBackG + this.imgBack.getHeight() + PaintPopup.hButtonSmall + 20;
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000578F4 File Offset: 0x00055CF4
		public override void paint(MyGraphics g, int x, int y)
		{
			g.translate((float)(-8 * AvMain.hd), (float)(-40 * AvMain.hd));
			g.setClip(0f, 0f, (float)this.w, (float)this.h);
			int num = 23 * AvMain.hd;
			int num2 = 20 * AvMain.hd;
			for (int i = 0; i < 2; i++)
			{
				g.drawImage(this.imgIcon[i], (float)(16 * AvMain.hd + num / 2), (float)(20 * AvMain.hd + num / 2 + i * num), 3);
			}
			g.drawImage(this.imgIcon[4], (float)(16 * AvMain.hd + num / 2), (float)(20 * AvMain.hd + num / 2 + 2 * num), 3);
			g.drawImage(this.imgIcon[2], (float)(16 * AvMain.hd + num / 2), (float)(20 * AvMain.hd + num / 2 + 3 * num), 3);
			g.drawImage(this.imgIcon[3], (float)(16 * AvMain.hd + num / 2), (float)(20 * AvMain.hd + num / 2 + 4 * num), 3);
			Canvas.normalFont.drawString(g, this.avatar.money[0] + string.Empty, 16 * AvMain.hd + num, 20 * AvMain.hd + num / 2 - Canvas.normalFont.getHeight() / 2, 0);
			Canvas.normalFont.drawString(g, this.avatar.money[2] + string.Empty, 16 * AvMain.hd + num, 20 * AvMain.hd + num / 2 + num - Canvas.normalFont.getHeight() / 2, 0);
			Canvas.normalFont.drawString(g, this.avatar.luongKhoa + string.Empty, 16 * AvMain.hd + num, 20 * AvMain.hd + num / 2 + num * 2 - Canvas.normalFont.getHeight() / 2, 0);
			Canvas.normalFont.drawString(g, this.avatar.money[3] + string.Empty, 16 * AvMain.hd + num, 20 * AvMain.hd + num / 2 + num * 3 - Canvas.normalFont.getHeight() / 2, 0);
			Canvas.normalFont.drawString(g, string.Concat(new object[]
			{
				this.avatar.lvMain,
				"+",
				this.avatar.perLvMain,
				"%"
			}), 16 * AvMain.hd + num, 20 * AvMain.hd + num / 2 + num * 4 - Canvas.normalFont.getHeight() / 2, 0);
			g.drawImage(this.imgBack, (float)(this.w - this.imgBack.w - 20 * AvMain.hd), (float)(this.yBackG + 3 * AvMain.hd), 0);
			this.avatar.paintIcon(g, this.w - this.imgBack.w - 30 * AvMain.hd + 40 * AvMain.hd, this.yBackG + this.imgBack.h - 10 * AvMain.hd, true);
			if (this.friend == null)
			{
				g.drawImage(this.imgShadow, (float)(this.w - 20 * AvMain.hd - this.imgShadow.w / 2 - 20 * AvMain.hd), (float)(this.yBackG + this.imgBack.h - 10 * AvMain.hd - this.imgShadow.h / 2), 3);
			}
			else
			{
				this.friend.paintIcon(g, this.w - 10 * AvMain.hd - this.imgShadow.w / 2 - 20 * AvMain.hd, this.yBackG + this.imgBack.h - 10 * AvMain.hd, true);
				Canvas.fontBlu.drawString(g, this.tenQuanHe, this.w - this.imgBack.w / 2 - 20 * AvMain.hd, this.yBackG + this.imgBack.h - 5 * AvMain.hd - this.imgShadow.h / 2 - 11 * AvMain.hd - Canvas.fontBlu.getHeight(), 2);
				AvatarData.paintImg(g, this.idImage, this.w - this.imgBack.w / 2 - 20 * AvMain.hd, this.yBackG + this.imgBack.h - 5 * AvMain.hd - this.imgShadow.h / 2, 3);
				Canvas.fontBlu.drawString(g, string.Concat(new object[]
				{
					this.level,
					"+",
					this.perLevel,
					"%"
				}), this.w - this.imgBack.w / 2 - 20 * AvMain.hd + Canvas.fontBlu.getWidth("%") / 2, this.yBackG + this.imgBack.h - 5 * AvMain.hd - this.imgShadow.h / 2 + 10 * AvMain.hd, 2);
			}
			g.translate(0f, (float)(this.yLine + ((AvMain.hd != 1) ? 0 : 10)));
			g.setColor(29068);
			g.fillRect((float)(20 * AvMain.hd), 0f, (float)(this.w - 40 * AvMain.hd), 1f);
			g.setColor(12255224);
			g.fillRect((float)(20 * AvMain.hd), 1f, (float)(this.w - 40 * AvMain.hd), 1f);
			int num3 = (this.h - this.yLine - (int)PaintPopup.hTab - this.hIndex * 3) / 2 - 2;
			for (int j = 0; j < 3; j++)
			{
				g.drawImage(this.imgThanh[2], (float)(num2 + this.xLeft + this.imgThanh[2].w / 2), (float)(num3 + this.hIndex / 2 + this.hIndex * j), 3);
				g.drawImage(this.imgThanh[2], (float)(this.w - num2 - this.xRight - this.imgThanh[2].w / 2), (float)(num3 + this.hIndex / 2 + this.hIndex * j), 3);
				if (j == 0)
				{
					if (this.avatar.lvFarm != -1)
					{
						Canvas.fontBlu.drawString(g, "Lv NT " + this.avatar.lvFarm, num2 + this.xLeft - 2 * AvMain.hd, num3 + this.hIndex / 2 + this.hIndex * j - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 1);
					}
					else
					{
						Canvas.fontBlu.drawString(g, T.myIndex[j] + ((j != 0) ? string.Empty : (string.Empty + this.avatar.lvMain)), num2 + this.xLeft - 2 * AvMain.hd, num3 + this.hIndex / 2 + this.hIndex * j - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 1);
					}
				}
				else
				{
					Canvas.fontBlu.drawString(g, T.myIndex[j] + ((j != 0) ? string.Empty : (string.Empty + this.avatar.lvMain)), num2 + this.xLeft - 2 * AvMain.hd, num3 + this.hIndex / 2 + this.hIndex * j - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 1);
				}
				Canvas.fontBlu.drawString(g, T.myIndex[3 + j], this.w - num2 - this.xRight - this.imgThanh[2].w - 2 * AvMain.hd, num3 + this.hIndex / 2 + this.hIndex * j - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 1);
			}
			if (this.avatar.lvFarm != -1)
			{
				Canvas.fontBlu.drawString(g, this.avatar.perLvFarm + string.Empty, num2 + this.xLeft + this.imgThanh[2].w + 2 * AvMain.hd, num3 + this.hIndex / 2 - Canvas.fontBlu.getHeight() / 2 - 3 * AvMain.hd, 0);
			}
			else
			{
				Canvas.fontBlu.drawString(g, this.avatar.perLvMain + string.Empty, num2 + this.xLeft + this.imgThanh[2].w + 2 * AvMain.hd, num3 + this.hIndex / 2 - Canvas.fontBlu.getHeight() / 2 - 3 * AvMain.hd, 0);
			}
			for (int k = 0; k < 3; k++)
			{
				if (k > 0)
				{
					Canvas.fontBlu.drawString(g, this.avatar.indexP[k - 1] + string.Empty, num2 + this.xLeft + this.imgThanh[2].w + 2 * AvMain.hd, num3 + this.hIndex / 2 + this.hIndex * k - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 0);
				}
				Canvas.fontBlu.drawString(g, this.avatar.indexP[2 + k] + string.Empty, this.w - num2 - this.xRight + 2 * AvMain.hd, num3 + this.hIndex / 2 + this.hIndex * k - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 0);
			}
			int num4 = 0;
			if (AvMain.hd == 2)
			{
				num4 = 1;
			}
			for (int l = 0; l < 3; l++)
			{
				int num5;
				if (l == 0)
				{
					if (this.avatar.lvFarm != -1)
					{
						num5 = (int)this.avatar.perLvFarm * this.imgThanh[1].w / 100;
					}
					else
					{
						num5 = (int)this.avatar.perLvMain * this.imgThanh[1].w / 100;
					}
				}
				else
				{
					num5 = (int)this.avatar.indexP[l - 1] * this.imgThanh[1].w / 100;
				}
				g.setClip((float)(num2 + this.xLeft + 1), (float)(num3 + this.hIndex * l), (float)num5, (float)this.hIndex);
				g.drawImage(this.imgThanh[1], (float)(num2 + this.xLeft + this.imgThanh[2].w / 2 + 1), (float)(num3 + this.hIndex / 2 + this.hIndex * l - num4), 3);
				num5 = (int)this.avatar.indexP[2 + l] * this.imgThanh[1].w / 100;
				g.setClip((float)(this.w - num2 - this.xRight - this.imgThanh[2].w + 2), (float)(num3 + this.hIndex * l), (float)num5, (float)this.hIndex);
				g.drawImage(this.imgThanh[1], (float)(this.w - num2 - this.xRight - this.imgThanh[2].w / 2 + 1), (float)(num3 + this.hIndex / 2 + this.hIndex * l - num4), 3);
			}
			g.setClip(0f, 0f, (float)this.w, (float)(this.h - (int)PaintPopup.hTab - this.yLine));
			for (int m = 0; m < 3; m++)
			{
				g.drawImage(this.imgThanh[0], (float)(num2 + this.xLeft + this.imgThanh[2].w / 2 + 1), (float)(num3 + this.hIndex / 2 + this.hIndex * m - 2 * AvMain.hd), 3);
				g.drawImage(this.imgThanh[0], (float)(this.w - num2 - this.xRight - this.imgThanh[2].w / 2 + 1), (float)(num3 + this.hIndex / 2 + this.hIndex * m - 2 * AvMain.hd), 3);
			}
		}

		// Token: 0x04000B63 RID: 2915
		private Image[] imgIcon;

		// Token: 0x04000B64 RID: 2916
		private Image[] imgThanh;

		// Token: 0x04000B65 RID: 2917
		private Image imgShadow;

		// Token: 0x04000B66 RID: 2918
		private Image imgBack;

		// Token: 0x04000B67 RID: 2919
		private Avatar avatar;

		// Token: 0x04000B68 RID: 2920
		private Avatar friend;

		// Token: 0x04000B69 RID: 2921
		private string tenQuanHe;

		// Token: 0x04000B6A RID: 2922
		private int w;

		// Token: 0x04000B6B RID: 2923
		private int h;

		// Token: 0x04000B6C RID: 2924
		private int yLine;

		// Token: 0x04000B6D RID: 2925
		private int hIndex;

		// Token: 0x04000B6E RID: 2926
		private int xLeft;

		// Token: 0x04000B6F RID: 2927
		private int xRight;

		// Token: 0x04000B70 RID: 2928
		private int yBackG = 25 * AvMain.hd;

		// Token: 0x04000B71 RID: 2929
		private sbyte level;

		// Token: 0x04000B72 RID: 2930
		private sbyte perLevel;

		// Token: 0x04000B73 RID: 2931
		private int idImage;
	}
}
