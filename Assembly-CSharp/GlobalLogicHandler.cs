using System;
using UnityEngine;

// Token: 0x02000090 RID: 144
public class GlobalLogicHandler
{
	// Token: 0x060004B5 RID: 1205 RVA: 0x0002B920 File Offset: 0x00029D20
	public void onConnectFail()
	{
		Out.println("onConnectFail");
		if (Canvas.currentMyScreen != LoginScr.me)
		{
			if (!this.isCon)
			{
				Canvas.msgdlg.setInfoLR(T.connectFail, new Command(T.yes, new GlobalLogicHandler.IActionConnectFailYes()), new Command(T.no, new GlobalLogicHandler.IActionDisconnect()));
			}
			else
			{
				Canvas.startOK(T.cityIsOffLine, new GlobalLogicHandler.IActionDisconnect());
			}
		}
		this.isCon = true;
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0002B999 File Offset: 0x00029D99
	public void onConnectOK()
	{
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x0002B99C File Offset: 0x00029D9C
	public static void onDisconnect()
	{
		Out.println("onDisconnect");
		try
		{
			GameMidlet.CLIENT_TYPE = 8;
			Canvas.menuMain = null;
			HouseScr.me = null;
			MessageScr.me = null;
			Canvas.currentPopup.removeAllElements();
			Canvas.currentFace = null;
			LoadMap.w = 24;
			LoadMap.rememMap = -1;
			ChatTextField.isShow = false;
			Canvas.endDlg();
			FarmData.init();
			if (ipKeyboard.tk != null)
			{
				ipKeyboard.tk.active = false;
				ipKeyboard.tk = null;
			}
			if (Canvas.menuMain != null)
			{
				Canvas.menuMain = null;
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		if (Canvas.currentMyScreen != LoginScr.me && Canvas.currentMyScreen != ServerListScr.me)
		{
			Canvas.setPopupTime(T.disConnect);
			if (Canvas.currentMyScreen != RegisterScr.gI())
			{
				Canvas.listAc.addElement(new GlobalLogicHandler.IActionDis());
			}
		}
		GlobalLogicHandler.isAutoLogin = false;
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x0002BA90 File Offset: 0x00029E90
	public void onLoginFail(string reason)
	{
		Canvas.startOKDlg(reason);
		Canvas.startOK(reason, new GlobalLogicHandler.IAciton33());
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0002BAA4 File Offset: 0x00029EA4
	public void onLoginSuccess()
	{
		GlobalLogicHandler.isAutoLogin = false;
		AvatarData.listImgIcon = new MyHashTable();
		AvatarMsgHandler.onHandler();
		Out.println(string.Concat(new object[]
		{
			"onLoginSuccess: ",
			AvatarData.playing,
			"   ",
			Canvas.currentMyScreen
		}));
		if (AvatarData.playing == -1)
		{
			AvatarService.gI().getBigData();
		}
		else
		{
			MapScr.gI().joinCitymap();
		}
		AvatarService.gI().doRequestExpicePet(GameMidlet.avatar.IDDB);
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0002BB34 File Offset: 0x00029F34
	public void onRegisterInfo(string username, bool available, string smsPrefix, string smsTo)
	{
		Canvas.endDlg();
		if (available)
		{
			string text = smsPrefix + username + " " + LoginScr.gI().passRemem;
			GameMidlet.sendSMS(text, string.Empty + smsTo, new GlobalLogicHandler.IActionRegisterOK(smsPrefix, username, smsTo), new GlobalLogicHandler.IActionRegisterFail(text, smsTo));
		}
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0002BB8B File Offset: 0x00029F8B
	public void onChatFrom(int id, string name, string info)
	{
		MessageScr.gI().addPlayer(id, name, info, false, null);
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0002BB9C File Offset: 0x00029F9C
	public void onServerInfo(string info)
	{
		Canvas.instance.setInfoSV(info);
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0002BBA9 File Offset: 0x00029FA9
	public void onServerMessage(string msg)
	{
		Canvas.startOKDlg(msg);
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0002BBB4 File Offset: 0x00029FB4
	public void onVersion(string info, string url)
	{
		IAction action = new GlobalLogicHandler.IActionVersionDown(url);
		Canvas.msgdlg.setIsWaiting(false);
		Canvas.msgdlg.setInfoLR(info, new Command(T.OK, action), new Command(T.close, -1, MapScr.instance));
		GlobalLogicHandler.isNewVersion = true;
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0002BBFF File Offset: 0x00029FFF
	public void onAdminCommandResponse(string responseText)
	{
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0002BC01 File Offset: 0x0002A001
	public void onSetMoneyError(string error, bool boo)
	{
		Out.println("onSetMoneyError: " + boo);
		if (boo)
		{
			Canvas.startOK(error, new GlobalLogicHandler.IActionDisconnect());
		}
		else
		{
			Canvas.startOKDlg(error);
		}
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0002BC34 File Offset: 0x0002A034
	public void onTransferMoney(int newMoney, string info)
	{
		GameMidlet.avatar.setMoney(newMoney);
		if (Canvas.currentDialog == null)
		{
			Canvas.startOKDlg(info);
		}
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0002BC51 File Offset: 0x0002A051
	public void onMoneyInfo(MyVector mni)
	{
		MoneyScr.gI().setAvatarList(mni);
		MoneyScr.gI().switchToMe(Canvas.currentMyScreen);
		Canvas.endDlg();
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x0002BC74 File Offset: 0x0002A074
	public void doGetHandler(sbyte index)
	{
		if (GameMidlet.CLIENT_TYPE == 9)
		{
			GlobalLogicHandler.isNewVersion = false;
		}
		if (GlobalMessageHandler.gI().miniGameMessageHandler != null)
		{
			switch (index)
			{
			case 8:
				AvatarMsgHandler.onHandler();
				if (MapScr.idMapOffline != -1)
				{
					GlobalService.gI().doJoinOfflineMap(MapScr.idMapOffline);
					MapScr.idMapOffline = -1;
				}
				else if ((int)MapScr.typeJoin != -1)
				{
					Canvas.loadMap.load(57 + (int)MapScr.typeJoin, true);
					if (Canvas.isInitChar && LoadMap.TYPEMAP == 57)
					{
						Canvas.welcome = new Welcome();
						Canvas.welcome.initShop(MapScr.instance);
					}
					GameMidlet.avatar.setFeel(4);
					Canvas.endDlg();
				}
				else
				{
					MapScr.gI().joinCitymap();
					Canvas.endDlg();
				}
				break;
			case 9:
				ParkMsgHandler.onHandler();
				if (LoadMap.xDichChuyen == -1)
				{
					if (!onMainMenu.isOngame)
					{
						if (GameMidlet.CLIENT_TYPE == 12)
						{
							LoadMap.w = 24;
							Canvas.load = 0;
							LoadMap.rememMap = -1;
							ParkService.gI().doJoinPark(MapScr.indexMap, -1);
						}
						else if (GameMidlet.CLIENT_TYPE == 3)
						{
							ParkService.gI().doJoinPark((int)MapScr.roomID, -1);
						}
						else if ((int)MapScr.typeJoin != -1)
						{
							MapScr.gI().doSetHandlerSuccess();
						}
						else if (MapScr.idMapOld != -1)
						{
							Canvas.startWaitDlg();
							ParkService.gI().doJoinPark(MapScr.idMapOld, -1);
							MapScr.idMapOld = -1;
						}
						else
						{
							MapScr.gI().doJoin();
						}
					}
					else
					{
						onMainMenu.gI().switchToMe();
						Canvas.endDlg();
					}
				}
				else
				{
					LoadMap.idTileImg = -1;
				}
				break;
			case 10:
				FarmMsgHandler.onHandler();
				if (FarmData.playing == -1)
				{
					FarmService.gI().setBigData();
				}
				else if (FarmScr.itemProduct == null)
				{
					FarmService.gI().getInventory();
				}
				else
				{
					ParkService.gI().doJoinPark(25, 0);
					FarmScr.init();
					FarmScr.gI().doJoinFarm(GameMidlet.avatar.IDDB, false);
				}
				break;
			case 11:
				HomeMsgHandler.onHandler();
				LoadMap.TYPEMAP = -1;
				ParkService.gI().doJoinPark(21, 0);
				if (MapScr.idHouse != -1)
				{
					Canvas.startWaitDlg();
					AvatarService.gI().getTypeHouse(0);
				}
				break;
			case 12:
				RaceMsgHandler.onHandler();
				GlobalService.gI().doJoinRoomRace();
				break;
			default:
				if (index == 3)
				{
					CasinoMsgHandler.onHandler();
					MapScr.gI().onJoinCasino();
				}
				break;
			}
		}
		GameMidlet.CLIENT_TYPE = (int)index;
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0002BF20 File Offset: 0x0002A320
	public void updateMoney(int moneyUpdate, int typeMoney)
	{
		if (moneyUpdate != 0)
		{
			Canvas.addFlyText(moneyUpdate, GameMidlet.avatar.x, GameMidlet.avatar.y, -1, -1);
		}
		if (typeMoney == 1)
		{
			if (onMainMenu.isOngame)
			{
				GameMidlet.avatar.setMoneyNew(GameMidlet.avatar.getMoneyNew() + moneyUpdate);
			}
			else
			{
				GameMidlet.avatar.setMoney(GameMidlet.avatar.getMoney() + moneyUpdate);
			}
		}
		else if (typeMoney == 2)
		{
			GameMidlet.avatar.money[2] += moneyUpdate;
		}
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x0002BFB2 File Offset: 0x0002A3B2
	public void onUpdateContainer(sbyte index, string str)
	{
		if ((int)index == 0)
		{
			Canvas.startOKDlg(str, new GlobalLogicHandler.IActionUpdateContainer());
		}
		else
		{
			Canvas.startOKDlg(str);
		}
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0002BFD1 File Offset: 0x0002A3D1
	public void onSms(string info1, string syst, string number1)
	{
		Canvas.startOKDlg(info1, new GlobalLogicHandler.IActionOnSMS(syst, number1));
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0002BFE0 File Offset: 0x0002A3E0
	public void onMenuOption(int userID, sbyte IDMenu, string[] listStr, short[] idImg, string nameNPC, string textChat, bool[] isMenu)
	{
		if (Canvas.menuMain != null)
		{
			Canvas.menuMain = null;
		}
		Canvas.endDlg();
		MyVector myVector = new MyVector();
		for (int i = 0; i < listStr.Length; i++)
		{
			int ii = i;
			myVector.addElement(new Command(listStr[i], new GlobalLogicHandler.IActionMenuOption(ii, userID, IDMenu)));
		}
		if (nameNPC != null)
		{
			MenuNPC.gI().setInfo(myVector, userID, nameNPC, textChat, isMenu);
		}
		else
		{
			MenuCenter.gI().startAt(myVector);
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0002C05D File Offset: 0x0002A45D
	public void onTextBox(int userID, sbyte idMenu, string nameText, int typeInput)
	{
		Canvas.inputDlg.setInfoIA(nameText, new GlobalLogicHandler.IActionTextBox(userID, idMenu), typeInput, Canvas.currentMyScreen);
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0002C078 File Offset: 0x0002A478
	public void onUpdateCHest(sbyte type, sbyte index2, string str2)
	{
		if ((int)index2 == 0)
		{
			Canvas.startOKDlg(str2, new GlobalLogicHandler.IActionChest());
		}
		else
		{
			Canvas.startOKDlg(str2);
		}
	}

	// Token: 0x04000773 RID: 1907
	private bool isCon;

	// Token: 0x04000774 RID: 1908
	public static bool isAutoLogin;

	// Token: 0x04000775 RID: 1909
	public static bool isNewVersion;

	// Token: 0x02000091 RID: 145
	private class IActionConnectFailYes : IAction
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x0002C0A1 File Offset: 0x0002A4A1
		public void perform()
		{
			ServerListScr.gI().doUpdateServer();
			if (Canvas.currentMyScreen == MiniMap.me)
			{
				Canvas.listAc.addElement(new GlobalLogicHandler.IAciton22());
			}
		}
	}

	// Token: 0x02000092 RID: 146
	private class IActionConnectFailNo : IAction
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x0002C0D3 File Offset: 0x0002A4D3
		public void perform()
		{
			if (Canvas.currentMyScreen == MiniMap.me)
			{
				Canvas.listAc.addElement(new GlobalLogicHandler.IAciton22());
			}
		}
	}

	// Token: 0x02000093 RID: 147
	private class IActionDis : IAction
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x0002C0FC File Offset: 0x0002A4FC
		public void perform()
		{
			AvCamera.gI().xCam = (AvCamera.gI().xTo = 100f);
			MapScr.gI().exitGame();
			LoginScr.gI().switchToMe();
			if (Screen.orientation != 1)
			{
				LoginScr.gI().init(Screen.height);
			}
		}
	}

	// Token: 0x02000094 RID: 148
	private class IAciton22 : IAction
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0002C15B File Offset: 0x0002A55B
		public void perform()
		{
			Session_ME.gI().close();
			LoginScr.gI().switchToMe();
		}
	}

	// Token: 0x02000095 RID: 149
	public class IActionDisconnect : IAction
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x0002C17C File Offset: 0x0002A57C
		public void perform()
		{
			if (Canvas.currentMyScreen != RegisterScr.gI())
			{
				AvCamera.gI().xCam = (AvCamera.gI().xTo = 100f);
				MapScr.gI().exitGame();
				LoginScr.gI().switchToMe();
			}
		}
	}

	// Token: 0x02000096 RID: 150
	private class IAciton33 : IAction
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x0002C1D0 File Offset: 0x0002A5D0
		public void perform()
		{
			LoginScr.gI().switchToMe();
		}
	}

	// Token: 0x02000097 RID: 151
	private class IActionRegisterOK : IAction
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0002C1DC File Offset: 0x0002A5DC
		public IActionRegisterOK(string smsPrefix, string username, string smsTo)
		{
			this.smsPrefix = smsPrefix;
			this.username = username;
			this.smsTo = smsTo;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0002C1FC File Offset: 0x0002A5FC
		public void perform()
		{
			Canvas.startOKDlg(T.registerSuccess);
			GlobalService.gI().sendSMSSuccess(this.smsPrefix + this.username + " " + LoginScr.gI().passRemem, this.smsTo);
			LoginScr.gI().passRemem = string.Empty;
		}

		// Token: 0x04000776 RID: 1910
		private readonly string smsPrefix;

		// Token: 0x04000777 RID: 1911
		private readonly string username;

		// Token: 0x04000778 RID: 1912
		private readonly string smsTo;
	}

	// Token: 0x02000098 RID: 152
	private class IActionRegisterFail : IAction
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x0002C252 File Offset: 0x0002A652
		public IActionRegisterFail(string i, string t)
		{
			this.info = i;
			this.to = t;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0002C268 File Offset: 0x0002A668
		public void perform()
		{
			Canvas.startOKDlg(string.Concat(new string[]
			{
				T.cannotRegister,
				" Soạn tin nhắn: ",
				this.info,
				" gửi đến ",
				this.to,
				" để đăng ký!"
			}));
		}

		// Token: 0x04000779 RID: 1913
		private string info;

		// Token: 0x0400077A RID: 1914
		private string to;
	}

	// Token: 0x02000099 RID: 153
	private class IActionVersionDown : IAction
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0002C2B7 File Offset: 0x0002A6B7
		public IActionVersionDown(string url)
		{
			this.url = url;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0002C2C6 File Offset: 0x0002A6C6
		public void perform()
		{
			GameMidlet.flatForm(this.url);
		}

		// Token: 0x0400077B RID: 1915
		private readonly string url;
	}

	// Token: 0x0200009A RID: 154
	private class IActionUpdateContainer : IAction
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x0002C2DB File Offset: 0x0002A6DB
		public void perform()
		{
			GlobalService.gI().doUpdateContainer(1);
			Canvas.startWaitDlg();
		}
	}

	// Token: 0x0200009B RID: 155
	private class IActionOnSMS : IAction
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x0002C2ED File Offset: 0x0002A6ED
		public IActionOnSMS(string syst, string number1)
		{
			this.syst = syst;
			this.number1 = number1;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0002C303 File Offset: 0x0002A703
		public void perform()
		{
			GameMidlet.sendSMS(this.syst, string.Empty + this.number1, new GlobalLogicHandler.IActionOnSMS1(), new GlobalLogicHandler.IActionOnSMS2());
		}

		// Token: 0x0400077C RID: 1916
		private readonly string syst;

		// Token: 0x0400077D RID: 1917
		private readonly string number1;
	}

	// Token: 0x0200009C RID: 156
	private class IActionOnSMS1 : IAction
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x0002C332 File Offset: 0x0002A732
		public void perform()
		{
			Canvas.startOKDlg(T.sentMsg);
		}
	}

	// Token: 0x0200009D RID: 157
	private class IActionOnSMS2 : IAction
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0002C346 File Offset: 0x0002A746
		public void perform()
		{
			Canvas.startOKDlg(T.canNotSendMsg);
		}
	}

	// Token: 0x0200009E RID: 158
	private class IActionMenuOption : IAction
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x0002C352 File Offset: 0x0002A752
		public IActionMenuOption(int ii, int userID, sbyte IDMenu)
		{
			this.ii = ii;
			this.userID = userID;
			this.IDMenu = IDMenu;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0002C370 File Offset: 0x0002A770
		public void perform()
		{
			if (!ListScr.gI().setList(string.Concat(new object[]
			{
				this.userID,
				"-",
				this.IDMenu,
				"-",
				this.ii
			})))
			{
				GlobalService.gI().doMenuOption(this.userID, this.IDMenu, this.ii);
			}
		}

		// Token: 0x0400077E RID: 1918
		private int userID;

		// Token: 0x0400077F RID: 1919
		private sbyte IDMenu;

		// Token: 0x04000780 RID: 1920
		private int ii;
	}

	// Token: 0x0200009F RID: 159
	private class IActionTextBox : IAction
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x0002C3ED File Offset: 0x0002A7ED
		public IActionTextBox(int userID, sbyte idMenu)
		{
			this.userID = userID;
			this.idMenu = idMenu;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0002C403 File Offset: 0x0002A803
		public void perform()
		{
			GlobalService.gI().doTextBox(this.userID, this.idMenu, Canvas.inputDlg.getText());
			Canvas.endDlg();
		}

		// Token: 0x04000781 RID: 1921
		private readonly int userID;

		// Token: 0x04000782 RID: 1922
		private readonly sbyte idMenu;
	}

	// Token: 0x020000A0 RID: 160
	private class IActionChest : IAction
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x0002C432 File Offset: 0x0002A832
		public void perform()
		{
			GlobalService.gI().doUpdateChest(1);
			Canvas.startWaitDlg();
		}
	}
}
