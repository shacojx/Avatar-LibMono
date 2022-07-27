using System;

// Token: 0x020000AC RID: 172
public class PhomService
{
	// Token: 0x0600056A RID: 1386 RVA: 0x0003308C File Offset: 0x0003148C
	public static PhomService gI()
	{
		if (PhomService.instance == null)
		{
			PhomService.instance = new PhomService();
		}
		return PhomService.instance;
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x000330A7 File Offset: 0x000314A7
	public void send(Message m)
	{
		Session_ME.gI().sendMessage(m);
		m.cleanup();
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x000330BC File Offset: 0x000314BC
	public void fire(sbyte roomID, sbyte boardID, sbyte cardID)
	{
		Message message = new Message(21);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
			message.writer().writeByte(cardID);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x00033120 File Offset: 0x00031520
	public void GetCard(sbyte roomID, sbyte boardID)
	{
		Message message = new Message(63);
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

	// Token: 0x0600056E RID: 1390 RVA: 0x00033178 File Offset: 0x00031578
	public void eatCard(sbyte roomID, sbyte boardID, int[] card, sbyte index)
	{
		Message message = new Message(64);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
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

	// Token: 0x0600056F RID: 1391 RVA: 0x00033200 File Offset: 0x00031600
	public void HaPhom(int roomID, int boardID, Card[] myCard)
	{
		Message message = new Message(65);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
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

	// Token: 0x06000570 RID: 1392 RVA: 0x000332D4 File Offset: 0x000316D4
	public void doAddCard(sbyte roomID, sbyte boardID, int[] card)
	{
		Message message = new Message(67);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
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

	// Token: 0x06000571 RID: 1393 RVA: 0x00033358 File Offset: 0x00031758
	public void doResetPhomEat(sbyte roomID, sbyte boardID, int[] cardID, int cardEat)
	{
		Message message = new Message(68);
		try
		{
			message.writer().writeByte(roomID);
			message.writer().writeByte(boardID);
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

	// Token: 0x0400078F RID: 1935
	protected static PhomService instance;
}
