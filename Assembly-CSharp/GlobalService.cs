using System;
using System.IO;
using UnityEngine;

// Token: 0x020000A5 RID: 165
public class GlobalService
{
	// Token: 0x060004FD RID: 1277 RVA: 0x0002FF41 File Offset: 0x0002E341
	public static GlobalService gI()
	{
		if (GlobalService.instance == null)
		{
			GlobalService.instance = new GlobalService();
		}
		return GlobalService.instance;
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x0002FF5C File Offset: 0x0002E35C
	public void send(Message m)
	{
		Session_ME.gI().sendMessage(m);
		m.cleanup();
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0002FF70 File Offset: 0x0002E370
	public void chatToBoard(sbyte roomID, sbyte boardID, string text)
	{
		Message message = new Message(9);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
			message.writer().writeUTF(text);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x0002FFD4 File Offset: 0x0002E3D4
	public void leaveBoard(sbyte roomID, sbyte boardID)
	{
		Message message = new Message(15);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x0003002C File Offset: 0x0002E42C
	public void ready(sbyte roomID, sbyte boardID, bool isReady)
	{
		Message message = new Message(16);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
			message.writer().writeBoolean(isReady);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00030090 File Offset: 0x0002E490
	public void setMoney(sbyte roomID, sbyte boardID, int money)
	{
		Message message = new Message(19);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
			message.writer().writeInt(money);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x000300F4 File Offset: 0x0002E4F4
	public void setPassword(sbyte roomID, sbyte boardID, string pass)
	{
		Message message = new Message(18);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
			message.writer().writeUTF(pass);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x00030158 File Offset: 0x0002E558
	public void kick(sbyte roomID, sbyte boardID, int kickID)
	{
		Message message = new Message(11);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
			message.writer().writeInt(kickID);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x000301BC File Offset: 0x0002E5BC
	public void startGame(sbyte roomID, sbyte boardID)
	{
		Message message = new Message(20);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x00030214 File Offset: 0x0002E614
	public void requestService(sbyte service, string arg)
	{
		if (arg == null)
		{
			arg = string.Empty;
		}
		Message message = new Message(-55);
		try
		{
			message.writer().writeByte(service);
			message.writer().writeUTF(arg);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00030278 File Offset: 0x0002E678
	public void setProviderAndClientType()
	{
		Message message = new Message(-1);
		message.writer().writeByte(GameMidlet.CLIENT_TYPE);
		this.send(message);
		Message message2 = new Message(-17);
		try
		{
			message2.writer().writeByte(GameMidlet.PROVIDER);
			message2.writer().writeInt(10000);
			string value = "uni";
			message2.writer().writeUTF(value);
			message2.writer().writeInt(10000);
			message2.writer().writeInt(Canvas.w);
			message2.writer().writeInt(Canvas.h);
			message2.writer().writeBoolean(true);
			message2.writer().writeByte((sbyte)(AvMain.hd - 1));
			message2.writer().writeUTF("2.5.8");
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
		this.send(message2);
		Message message3 = new Message(-79);
		message3.writer().writeUTF(GameMidlet.AGENT);
		this.send(message3);
		Message message4 = new Message(-86);
		message4.writer().writeByte(2);
		if (!ScaleGUI.isAndroid)
		{
			message4.writer().writeByte(GameMidlet.VERSIONCODE);
			message4.writer().writeUTF(SystemInfo.deviceUniqueIdentifier + string.Empty);
		}
		this.send(message4);
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x000303DC File Offset: 0x0002E7DC
	public void requestInfoOf(int iDDB)
	{
		Message message = new Message(34);
		try
		{
			message.writer().writeInt(iDDB);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00030428 File Offset: 0x0002E828
	public void login(string username, string pass, string version)
	{
		Out.println(string.Concat(new string[]
		{
			"login: -",
			username,
			"------",
			pass,
			"-"
		}));
		Message message = new Message(-2);
		try
		{
			message.writer().writeUTF(username);
			message.writer().writeUTF(pass);
			message.writer().writeUTF(version);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x000304BC File Offset: 0x0002E8BC
	public void setGameType(int gameType)
	{
		Message message = new Message(61);
		try
		{
			message.writer().writeByte(gameType);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00030508 File Offset: 0x0002E908
	public void requestRegister(string username, string pass, string introductionCode, string email)
	{
		Message message = new Message(-30);
		try
		{
			message.writer().writeUTF(username);
			message.writer().writeUTF(pass);
			message.writer().writeUTF(introductionCode);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0003056C File Offset: 0x0002E96C
	public void requestRegister(string username, string pass)
	{
		Message message = new Message(-30);
		try
		{
			message.writer().writeUTF(username);
			message.writer().writeUTF(pass);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x000305C4 File Offset: 0x0002E9C4
	public void addFriend(int id)
	{
		Message message = new Message(-19);
		try
		{
			message.writer().writeInt(id);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00030610 File Offset: 0x0002EA10
	public void requestAvatar(short avatar)
	{
		Message message = new Message(38);
		try
		{
			message.writer().writeShort(avatar);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0003065C File Offset: 0x0002EA5C
	public void chatTo(int iddb, string text)
	{
		Out.println(string.Concat(new object[]
		{
			"chatTo: ",
			iddb,
			"    ",
			text
		}));
		Message message = new Message(-6);
		try
		{
			message.writer().writeInt(iddb);
			message.writer().writeUTF(text);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x000306E0 File Offset: 0x0002EAE0
	public void requestAvatarShop()
	{
		Message m = new Message(39);
		this.send(m);
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x000306FC File Offset: 0x0002EAFC
	public void requestChargeMoneyInfo()
	{
		Message m = new Message(-23);
		this.send(m);
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00030718 File Offset: 0x0002EB18
	public void sendSMSSuccess(string content, string smsTo)
	{
		Message message = new Message(57);
		try
		{
			message.writer().writeUTF(content);
			message.writer().writeUTF(smsTo);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00030770 File Offset: 0x0002EB70
	public void doRequestCreCharacter()
	{
		Message message = new Message(-35);
		try
		{
			message.writer().writeByte(GameMidlet.avatar.gender);
			int num = GameMidlet.avatar.seriPart.size();
			message.writer().writeByte((sbyte)num);
			for (int i = 0; i < num; i++)
			{
				SeriPart seriPart = (SeriPart)GameMidlet.avatar.seriPart.elementAt(i);
				message.writer().writeShort(seriPart.idPart);
			}
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x0003081C File Offset: 0x0002EC1C
	public void doRemoveItem(int part, int type)
	{
		Message message = new Message(-36);
		try
		{
			Out.println(string.Concat(new object[]
			{
				"doRemoveItem: ",
				part,
				"    ",
				type
			}));
			message.writer().writeShort((short)part);
			message.writer().writeByte((sbyte)type);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x000308A8 File Offset: 0x0002ECA8
	public void doRequestContainer(int iddb)
	{
		Message message = new Message(-47);
		try
		{
			message.writer().writeInt(iddb);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x000308F4 File Offset: 0x0002ECF4
	public void doUsingItem(short idPart, sbyte type)
	{
		Message message = new Message(-48);
		try
		{
			message.writer().writeShort(idPart);
			message.writer().writeByte(type);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0003094C File Offset: 0x0002ED4C
	public void getHandler(int index)
	{
		Message message = new Message(-1);
		try
		{
			message.writer().writeByte(index);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00030994 File Offset: 0x0002ED94
	public void doRequestSoundData(sbyte id)
	{
		Message message = new Message(-51);
		try
		{
			message.writer().writeByte(id);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x000309E0 File Offset: 0x0002EDE0
	public void requestShop(int typeShop)
	{
		Message message = new Message(-49);
		try
		{
			message.writer().writeByte(typeShop);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00030A2C File Offset: 0x0002EE2C
	public void doRequestNumSupport(int hashCode)
	{
		Message message = new Message(-52);
		try
		{
			message.writer().writeInt(hashCode);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00030A78 File Offset: 0x0002EE78
	public void doUpdateContainer(int index)
	{
		Message message = new Message(-53);
		try
		{
			message.writer().writeByte(index);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00030AC4 File Offset: 0x0002EEC4
	public void doMenuOption(int userID, sbyte idMenu, int i)
	{
		Message message = new Message(-59);
		try
		{
			message.writer().writeInt(userID);
			message.writer().writeByte(idMenu);
			message.writer().writeByte((sbyte)i);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00030B28 File Offset: 0x0002EF28
	public void doTextBox(int userID, sbyte idMenu, string text)
	{
		Message message = new Message(-60);
		try
		{
			message.writer().writeInt(userID);
			message.writer().writeByte(idMenu);
			message.writer().writeUTF(text);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00030B8C File Offset: 0x0002EF8C
	public void doCommunicate(int idUser)
	{
		Message message = new Message(-61);
		try
		{
			message.writer().writeInt(idUser);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00030BD8 File Offset: 0x0002EFD8
	public void doLoadCard(string text, string text2, string link)
	{
		Message message = new Message(-56);
		try
		{
			message.writer().writeUTF(text);
			message.writer().writeUTF(text2);
			message.writer().writeUTF(link);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00030C3C File Offset: 0x0002F03C
	public void doChangePass(string PassOld, string passNew_1)
	{
		Message message = new Message(-62);
		try
		{
			message.writer().writeUTF(PassOld);
			message.writer().writeUTF(passNew_1);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00030C94 File Offset: 0x0002F094
	public void doDialLucky(short part, int selectedNumber)
	{
		Message message = new Message(-64);
		try
		{
			message.writer().writeShort(part);
			message.writer().writeShort((short)selectedNumber);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00030CEC File Offset: 0x0002F0EC
	public void doServerKick(int iddb, string text)
	{
		Message message = new Message(-72);
		try
		{
			message.writer().writeInt(iddb);
			message.writer().writeUTF(text);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00030D44 File Offset: 0x0002F144
	public void doListCustom(int idList, sbyte page, int selected, sbyte idmenu)
	{
		Message message = new Message(-81);
		try
		{
			message.writer().writeInt(idList);
			message.writer().writeByte(page);
			message.writer().writeShort((short)selected);
			message.writer().writeByte(idmenu);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00030DB4 File Offset: 0x0002F1B4
	public void doRequestCmdRotate(int anthor, int iddb)
	{
		Message message = new Message(-83);
		try
		{
			message.writer().writeShort((short)anthor);
			message.writer().writeInt(iddb);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00030E0C File Offset: 0x0002F20C
	public void doCustomTab(sbyte idAction)
	{
		Message message = new Message(-58);
		try
		{
			message.writer().writeByte(idAction);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00030E58 File Offset: 0x0002F258
	public void doUpdateChest(int i)
	{
		Out.println("doUpdateChest: " + i);
		Message message = new Message(-90);
		try
		{
			message.writer().writeByte(i);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00030EB8 File Offset: 0x0002F2B8
	public void doEnterPass(string text, sbyte type)
	{
		Message message = new Message(-88);
		try
		{
			Out.println(string.Concat(new object[]
			{
				"enter Pass: ",
				text,
				" type: ",
				type
			}));
			message.writer().writeByte(type);
			message.writer().writeUTF(text);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x00030F3C File Offset: 0x0002F33C
	public void doRequestChest()
	{
		Message m = new Message(-87);
		this.send(m);
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x00030F58 File Offset: 0x0002F358
	public void doChangeChestPass(string text, string text2)
	{
		Message message = new Message(-88);
		try
		{
			message.writer().writeByte(1);
			message.writer().writeUTF(text);
			message.writer().writeUTF(text2);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00030FBC File Offset: 0x0002F3BC
	public void doTransChestPart(int i, int ii, short idPart)
	{
		Message message = new Message(-89);
		try
		{
			message.writer().writeByte((sbyte)i);
			message.writer().writeShort((short)ii);
			message.writer().writeShort(idPart);
			Out.println(string.Concat(new object[]
			{
				"send get chest part: bytetype: ",
				i,
				" idIndex: ",
				ii,
				" idPart: ",
				idPart
			}));
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00031064 File Offset: 0x0002F464
	public void joinAvatar()
	{
		Message m = new Message(-96);
		this.send(m);
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x00031080 File Offset: 0x0002F480
	public void requestCityMap(sbyte idMini)
	{
		Message message = new Message(-92);
		try
		{
			if ((int)idMini != -1)
			{
				message.writer().writeByte(idMini);
			}
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x000310D4 File Offset: 0x0002F4D4
	public void requestTileMap(sbyte idTileImg)
	{
		Message message = new Message(-94);
		try
		{
			message.writer().writeByte(idTileImg);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x00031120 File Offset: 0x0002F520
	public void doSelectedMiniMap(sbyte idCityMap, sbyte id)
	{
		Message message = new Message(-93);
		try
		{
			message.writer().writeByte(idCityMap);
			message.writer().writeByte(id);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x00031178 File Offset: 0x0002F578
	public void requestJoinAny(short idJoin)
	{
		Message message = new Message(-95);
		try
		{
			message.writer().writeByte(MapScr.idCityMap);
			message.writer().writeByte(MapScr.idSelectedMini);
			message.writer().writeShort(idJoin);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x000311E4 File Offset: 0x0002F5E4
	public void requestPartDynaMic(short idPart)
	{
		Message message = new Message(-97);
		try
		{
			message.writer().writeShort(idPart);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00031230 File Offset: 0x0002F630
	public void requestImagePart(short id)
	{
		Message message = new Message(-98);
		try
		{
			message.writer().writeShort(id);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x0003127C File Offset: 0x0002F67C
	public void doRequestMoneyLoad(string iD)
	{
		Message message = new Message(-91);
		try
		{
			message.writer().writeUTF(iD);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x000312C8 File Offset: 0x0002F6C8
	public void doRemoveFriend(int id)
	{
		Message message = new Message(-20);
		try
		{
			message.writer().writeInt(id);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00031314 File Offset: 0x0002F714
	public void doJoinOfflineMap(int i)
	{
		Canvas.startWaitDlg();
		Message message = new Message(-99);
		try
		{
			message.writer().writeByte(i);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00031364 File Offset: 0x0002F764
	public void doJoinRoomRace()
	{
		Canvas.startWaitDlg();
		Message m = new Message(1);
		this.send(m);
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00031384 File Offset: 0x0002F784
	public void doDatCuoc(int idPet, int money)
	{
		Canvas.startWaitDlg();
		Message message = new Message(5);
		try
		{
			message.writer().writeByte(idPet);
			message.writer().writeInt(money);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x000313E0 File Offset: 0x0002F7E0
	public void doPetInfo(int idPet)
	{
		Message message = new Message(2);
		try
		{
			message.writer().writeByte(idPet);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00031428 File Offset: 0x0002F828
	public void doHistoryRace()
	{
		Message m = new Message(8);
		this.send(m);
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00031444 File Offset: 0x0002F844
	public void chatToBoard(string text)
	{
		Message message = new Message(9);
		try
		{
			message.writer().writeUTF(text);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x00031490 File Offset: 0x0002F890
	public void doRequestSky()
	{
		Message m = new Message(92);
		this.send(m);
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x000314AC File Offset: 0x0002F8AC
	public void transXeng(int money)
	{
		Message message = new Message(-102);
		try
		{
			message.writer().writeInt(money);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x000314F8 File Offset: 0x0002F8F8
	public void doFlowerLove()
	{
		Message m = new Message(-105);
		this.send(m);
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00031514 File Offset: 0x0002F914
	public void doFlowerLoveSelected(int ii)
	{
		Message message = new Message(-106);
		try
		{
			message.writer().writeByte(ii);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x00031560 File Offset: 0x0002F960
	public void doSendOpenShopHouse(sbyte typeShop, short idItem)
	{
		Message message = new Message(-107);
		try
		{
			message.writer().writeByte(typeShop);
			message.writer().writeShort(idItem);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x000315B8 File Offset: 0x0002F9B8
	public void doRegisterByEmail(string name, string pass, string email)
	{
		Out.println(string.Concat(new string[]
		{
			"doRegisterByEmail: ",
			name,
			"   ",
			pass,
			"   ",
			email
		}));
		Message message = new Message(-25);
		try
		{
			message.writer().writeUTF(name);
			message.writer().writeUTF(pass);
			message.writer().writeUTF(email);
			message.writer().writeByte(0);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0003165C File Offset: 0x0002FA5C
	public void doLoginNewGame()
	{
		Out.println("doLoginNewGame");
		Message m = new Message(-12);
		this.send(m);
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00031684 File Offset: 0x0002FA84
	public void createCharInfo(string name, int ngay, int thang, int nam, string address, string cmnd, string ngayCap, string noiCap, string sdt)
	{
		Out.println(string.Concat(new object[]
		{
			"createCharInfo: ",
			name,
			"  ",
			ngay,
			"/",
			thang,
			"/",
			nam,
			"   ",
			address,
			"   ",
			cmnd,
			"   ",
			ngayCap,
			"   ",
			noiCap,
			"   ",
			sdt
		}));
		Message message = new Message(-106);
		try
		{
			message.writer().writeUTF(name);
			message.writer().writeByte(ngay);
			message.writer().writeByte(thang);
			message.writer().writeShort(nam);
			message.writer().writeUTF(address);
			message.writer().writeUTF(cmnd);
			message.writer().writeUTF(ngayCap);
			message.writer().writeUTF(noiCap);
			message.writer().writeUTF(sdt);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x04000789 RID: 1929
	protected static GlobalService instance;
}
