using System;
using System.IO;

// Token: 0x02000089 RID: 137
public class CasinoService
{
	// Token: 0x06000457 RID: 1111 RVA: 0x00028376 File Offset: 0x00026776
	public static CasinoService gI()
	{
		if (CasinoService.instance == null)
		{
			CasinoService.instance = new CasinoService();
		}
		return CasinoService.instance;
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x00028391 File Offset: 0x00026791
	public void send(Message m)
	{
		Session_ME.gI().sendMessage(m);
		m.cleanup();
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x000283A4 File Offset: 0x000267A4
	public void requestRoomList()
	{
		Message m = new Message(6);
		this.send(m);
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x000283C0 File Offset: 0x000267C0
	public void requestBoardList(sbyte id)
	{
		Message message = new Message(7);
		try
		{
			message.writer().writeByte(id);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00028408 File Offset: 0x00026808
	public void setMaxPlayer(int max)
	{
		Message message = new Message(56);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeByte((sbyte)max);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x00028474 File Offset: 0x00026874
	public void joinBoard(sbyte roomID, sbyte boardID, string pass)
	{
		Message message = new Message(8);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
			message.writer().writeUTF(pass);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x000284D4 File Offset: 0x000268D4
	public void joinAnyBoard()
	{
		Message m = new Message(28);
		this.send(m);
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x000284F0 File Offset: 0x000268F0
	public void requestStrongest(int page)
	{
		Message message = new Message(30);
		try
		{
			message.writer().writeByte(page);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x0002853C File Offset: 0x0002693C
	public void requestFriendList()
	{
		Message m = new Message(-18);
		this.send(m);
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x00028558 File Offset: 0x00026958
	public void requestRichest(int page)
	{
		Message message = new Message(31);
		try
		{
			message.writer().writeByte(page);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x000285A4 File Offset: 0x000269A4
	public void move(sbyte roomID, sbyte boardID, sbyte[] cards)
	{
		Message message = new Message(21);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
			message.writer().writeByte((sbyte)cards.Length);
			for (int i = 0; i < cards.Length; i++)
			{
				message.writer().writeByte(cards[i]);
			}
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0002862C File Offset: 0x00026A2C
	public void move(sbyte[] cards)
	{
		Message message = new Message(21);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeByte((sbyte)cards.Length);
			for (int i = 0; i < cards.Length; i++)
			{
				message.writer().writeByte(cards[i]);
			}
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x000286BC File Offset: 0x00026ABC
	public void skip()
	{
		Message message = new Message(49);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x0002871C File Offset: 0x00026B1C
	public void forceFinish()
	{
		Message message = new Message(53);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x0002877C File Offset: 0x00026B7C
	public void moveCo(sbyte x, sbyte y)
	{
		Message message = new Message(21);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeByte(x);
			message.writer().writeByte(y);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x000287F4 File Offset: 0x00026BF4
	public void moveAndWin(int winx, int winy, int windx, int windy, int x, int y)
	{
		Message message = new Message(22);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeByte((sbyte)x);
			message.writer().writeByte((sbyte)y);
			message.writer().writeByte((sbyte)winx);
			message.writer().writeByte((sbyte)winy);
			message.writer().writeByte((sbyte)windx);
			message.writer().writeByte((sbyte)windy);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x000288A4 File Offset: 0x00026CA4
	public void firePhom(sbyte cardID)
	{
		Message message = new Message(21);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeByte(cardID);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00028910 File Offset: 0x00026D10
	public void GetCardPhom()
	{
		Message message = new Message(63);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00028970 File Offset: 0x00026D70
	public void eatCardPhom(int[] card, sbyte index)
	{
		Message message = new Message(64);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			for (int i = 0; i < card.Length; i++)
			{
				message.writer().writeByte((sbyte)card[i]);
			}
			message.writer().writeByte(index);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x000289FC File Offset: 0x00026DFC
	public void HaPhomPhom(Card[] myCard)
	{
		Message message = new Message(65);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			int num = -1;
			for (int i = 0; i < 10; i++)
			{
				if ((int)myCard[i].phom != 0)
				{
					if ((int)myCard[i].phom != num && num != -1)
					{
						message.writer().writeByte(-1);
					}
					num = (int)myCard[i].phom;
					message.writer().writeByte(myCard[i].cardID);
				}
				else if (num != -1)
				{
					message.writer().writeByte(-1);
					num = -1;
				}
			}
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00028AD8 File Offset: 0x00026ED8
	public void doAddCardPhom(int[] card)
	{
		Message message = new Message(67);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			for (int i = 0; i < card.Length; i++)
			{
				if (card[i] != -1)
				{
					message.writer().writeByte((sbyte)card[i]);
				}
			}
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00028B64 File Offset: 0x00026F64
	public void doResetPhomEatPhom(int[] cardID, int cardEat)
	{
		Message message = new Message(68);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeByte((sbyte)cardEat);
			for (int i = 0; i < 5; i++)
			{
				if (cardID[i] == -1)
				{
					break;
				}
				message.writer().writeByte((sbyte)cardID[i]);
			}
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00028C00 File Offset: 0x00027000
	public void chatToBoard(string text)
	{
		Message message = new Message(9);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeUTF(text);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00028C6C File Offset: 0x0002706C
	public void leaveBoard(sbyte roomID, sbyte boardID)
	{
		Message message = new Message(15);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00028CCC File Offset: 0x000270CC
	public void ready(sbyte roomID, sbyte boardID, bool isReady)
	{
		Message message = new Message(16);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeBoolean(isReady);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x00028D38 File Offset: 0x00027138
	public void setMoney(sbyte roomID, sbyte boardID, int money)
	{
		Message message = new Message(19);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeInt(money);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00028DA4 File Offset: 0x000271A4
	public void setPassword(sbyte roomID, sbyte boardID, string pass)
	{
		Message message = new Message(18);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeUTF(pass);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00028E10 File Offset: 0x00027210
	public void kick(sbyte roomID, sbyte boardID, int kickID)
	{
		Message message = new Message(11);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeInt(kickID);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00028E7C File Offset: 0x0002727C
	public void startGame(sbyte roomID, sbyte boardID)
	{
		Message message = new Message(20);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00028EDC File Offset: 0x000272DC
	public void createCell(Point[][] array)
	{
		int num = 0;
		Message message = new Message(64);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (array[i][j].isRemove)
					{
						array[i][j].isRemove = false;
						message.writer().writeByte((sbyte)(i * 8 + j));
						num++;
					}
				}
			}
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x00028F90 File Offset: 0x00027390
	public void doMoveDiamond(int iSelected, int selected)
	{
		Message message = new Message(21);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeByte((sbyte)iSelected);
			message.writer().writeByte((sbyte)selected);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x00029008 File Offset: 0x00027408
	public void doSkipDaimond()
	{
		Message message = new Message(49);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00029068 File Offset: 0x00027468
	public void doOutPath()
	{
		Message message = new Message(24);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x000290C8 File Offset: 0x000274C8
	public void PutMoneyOk(MyVector info, sbyte room, sbyte board)
	{
		Message message = new Message(21);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			if (info.size() > 0)
			{
				for (int i = 0; i < info.size(); i++)
				{
					PimgBC pimgBC = (PimgBC)info.elementAt(i);
					message.writer().writeByte((sbyte)pimgBC.moneyPut);
					pimgBC.moneyPut = 0;
				}
			}
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e, "PutMoneyOkzz()");
		}
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00029174 File Offset: 0x00027574
	public void ta(sbyte room, sbyte board, sbyte indexFrom, sbyte indexTo)
	{
		Message message = new Message(65);
		try
		{
			message.writer().writeByte(BoardScr.roomID);
			message.writer().writeByte(BoardScr.boardID);
			message.writer().writeByte(room);
			message.writer().writeByte(board);
			message.writer().writeByte(indexFrom);
			message.writer().writeByte(indexTo);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e, "ta()");
		}
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x00029208 File Offset: 0x00027608
	public void skip(sbyte roomID, sbyte boardID)
	{
		Message m = new Message(49);
		this.send(m);
	}

	// Token: 0x0400076C RID: 1900
	protected static CasinoService instance;
}
