using System;
using System.IO;

// Token: 0x020000AA RID: 170
public class ParkService
{
	// Token: 0x0600054F RID: 1359 RVA: 0x00032294 File Offset: 0x00030694
	public static ParkService gI()
	{
		if (ParkService.instance == null)
		{
			ParkService.instance = new ParkService();
		}
		return ParkService.instance;
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x000322AF File Offset: 0x000306AF
	public void send(Message m)
	{
		Session_ME.gI().sendMessage(m);
		m.cleanup();
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x000322C4 File Offset: 0x000306C4
	public void doJoinPark(int roomID, int boardID)
	{
		Canvas.startWaitDlg();
		Message message = new Message(50);
		try
		{
			message.writer().writeByte((sbyte)roomID);
			message.writer().writeByte((sbyte)boardID);
			message.writer().writeShort((short)LoadMap.xJoinCasino);
			message.writer().writeShort((short)LoadMap.yJoinCasino);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x00032344 File Offset: 0x00030744
	public void doMove(int x, int y, int direct)
	{
		Message message = new Message(54);
		try
		{
			message.writer().writeShort((short)x);
			message.writer().writeShort((short)y);
			message.writer().writeByte((sbyte)direct);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x000323A8 File Offset: 0x000307A8
	public void chatToBoard(string text)
	{
		Message message;
		if (GameMidlet.CLIENT_TYPE == 10)
		{
			message = new Message(77);
		}
		else
		{
			message = new Message(55);
		}
		try
		{
			message.writer().writeUTF(text);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0003240C File Offset: 0x0003080C
	public void doRequestAddFriend(int idUser)
	{
		Message message = new Message(-21);
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

	// Token: 0x06000555 RID: 1365 RVA: 0x00032458 File Offset: 0x00030858
	public void doAddFriend(int iddb, bool b)
	{
		Message message = new Message(-19);
		try
		{
			message.writer().writeInt(iddb);
			message.writer().writeBoolean(b);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x000324B0 File Offset: 0x000308B0
	public void doGiftGiving(int iddb, int i, int typeBuy)
	{
		Message message = new Message(58);
		try
		{
			message.writer().writeInt(iddb);
			message.writer().writeShort((short)i);
			message.writer().writeByte((sbyte)typeBuy);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00032514 File Offset: 0x00030914
	public void doGivingDeferrent(int idTo, int id)
	{
		Out.println("doGivingDeferrent: " + id);
		Message message = new Message(59);
		try
		{
			message.writer().writeInt(idTo);
			message.writer().writeShort((short)id);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00032580 File Offset: 0x00030980
	public void doRequestYourInfo(int iddb)
	{
		Message message = new Message(-22);
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

	// Token: 0x06000559 RID: 1369 RVA: 0x000325CC File Offset: 0x000309CC
	public void doRequestBoardList(sbyte roomId)
	{
		Message message = new Message(60);
		try
		{
			message.writer().writeByte(roomId);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00032618 File Offset: 0x00030A18
	public void doBuyItem(short id)
	{
		Message message = new Message(-38);
		try
		{
			message.writer().writeShort(id);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00032664 File Offset: 0x00030A64
	public void doKick(sbyte roomId, sbyte boardId, int iddb)
	{
		Message message = new Message(78);
		try
		{
			message.writer().writeByte(roomId);
			message.writer().writeByte(boardId);
			message.writer().writeInt(iddb);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x000326C8 File Offset: 0x00030AC8
	public void doQuanCau()
	{
		Message m = new Message(82);
		this.send(m);
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x000326E4 File Offset: 0x00030AE4
	public void doFinishFishing(bool isF, sbyte[] index)
	{
		Message message = new Message(84);
		try
		{
			message.writer().writeBoolean(isF);
			message.writer().writeByte((sbyte)index.Length);
			for (int i = 0; i < index.Length; i++)
			{
				Canvas.test1 = Canvas.test1 + index[i] + ", ";
				message.writer().writeByte(index[i]);
			}
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0003277C File Offset: 0x00030B7C
	public void doCauCaXong()
	{
		Message m = new Message(85);
		this.send(m);
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x00032798 File Offset: 0x00030B98
	public void doStartFishing()
	{
		Message m = new Message(86);
		this.send(m);
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x000327B4 File Offset: 0x00030BB4
	public void doInviteToMyHome(int type, int iddb)
	{
		Message message = new Message(-68);
		try
		{
			message.writer().writeByte((sbyte)type);
			message.writer().writeInt(iddb);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0003280C File Offset: 0x00030C0C
	public void doCustomPopup(int idBoss, int idPopup, int ii)
	{
		Message message = new Message(-77);
		try
		{
			message.writer().writeInt(idBoss);
			message.writer().writeByte((sbyte)idPopup);
			message.writer().writeByte((sbyte)ii);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00032870 File Offset: 0x00030C70
	public void doBossShop(int idBoss1, int idShop, int ii)
	{
		Message message = new Message(-78);
		try
		{
			message.writer().writeInt(idBoss1);
			message.writer().writeByte((sbyte)idShop);
			message.writer().writeShort((short)ii);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x000328D4 File Offset: 0x00030CD4
	public void doGetDropPart(int idDrop)
	{
		Message message = new Message(89);
		try
		{
			message.writer().writeByte(0);
			message.writer().writeInt(idDrop);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0003292C File Offset: 0x00030D2C
	public void doRequestWedding(int roomID, int boardID)
	{
		Message message = new Message(93);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0400078D RID: 1933
	protected static ParkService instance;
}
