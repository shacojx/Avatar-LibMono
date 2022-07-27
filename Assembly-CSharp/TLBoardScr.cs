using System;

// Token: 0x020001C3 RID: 451
public class TLBoardScr : BoardScr
{
	// Token: 0x06000C94 RID: 3220 RVA: 0x000802F4 File Offset: 0x0007E6F4
	public TLBoardScr()
	{
		this.cmdSkip = new Command(T.skip, 7);
		this.initYShow();
		if (Canvas.w < 200)
		{
			TLBoardScr.wCard = 26;
			TLBoardScr.hcard = 32;
		}
		else
		{
			TLBoardScr.wCard = 72;
			TLBoardScr.hcard = 97;
		}
		if (AvMain.hd == 2)
		{
			TLBoardScr.wCard = 144;
			TLBoardScr.hcard = 194;
		}
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x00080374 File Offset: 0x0007E774
	public static TLBoardScr gI()
	{
		return (TLBoardScr.instance != null) ? TLBoardScr.instance : (TLBoardScr.instance = new TLBoardScr());
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x00080398 File Offset: 0x0007E798
	public override void resetCard()
	{
		this.currentCards = new MyVector();
		this.currentCardsType = -1;
		this.currentCardsValue = new sbyte[0];
		this.selectedCard = -1;
		this.selectedCards = new sbyte[0];
		this.selectedCardsType = -1;
		this.currentPlayer = -1;
		this.cardShows = new MyVector();
		base.resetCard();
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x000803F8 File Offset: 0x0007E7F8
	private static void swap(MyVector actors, int dex1, int dex2)
	{
		object o = actors.elementAt(dex2);
		actors.setElementAt(actors.elementAt(dex1), dex2);
		actors.setElementAt(o, dex1);
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x00080424 File Offset: 0x0007E824
	private void sort(MyVector cards)
	{
		int num = cards.size();
		for (int i = 0; i < num - 1; i++)
		{
			for (int j = i + 1; j < num; j++)
			{
				Card card = (Card)cards.elementAt(i);
				Card card2 = (Card)cards.elementAt(j);
				if ((int)card.cardID > (int)card2.cardID)
				{
					TLBoardScr.swap(cards, i, j);
				}
			}
		}
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x00080497 File Offset: 0x0007E897
	public override void commandTab(int index)
	{
		if (index != 7)
		{
			base.commandTab(index);
		}
		else
		{
			this.doSkip();
		}
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x000804BC File Offset: 0x0007E8BC
	public void initYShow()
	{
		this.yShow = MyScreen.getHTF();
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x000804C9 File Offset: 0x0007E8C9
	public override void init()
	{
		base.init();
		this.initYShow();
		if (BoardScr.isStartGame)
		{
			this.setPosCard(false);
		}
		this.currentCards = null;
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x000804EF File Offset: 0x0007E8EF
	public override void doContinue()
	{
		this.resetCard();
		base.doContinue();
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x000804FD File Offset: 0x0007E8FD
	protected void doSkip()
	{
		Canvas.msgdlg.setInfoLR(T.doYouWantSkip, new Command(T.yes, 0, this), Canvas.cmdEndDlg);
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x0008051F File Offset: 0x0007E91F
	public override void commandActionPointer(int index, int subIndex)
	{
		if (index != 0)
		{
			base.commandActionPointer(index, subIndex);
		}
		else
		{
			this.currentPlayer = -1;
			this.forceMove3Bich = false;
			CasinoService.gI().skip();
			Canvas.endDlg();
		}
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x0008055C File Offset: 0x0007E95C
	protected void doSelect()
	{
		((Card)this.cards.elementAt(this.selectedCard)).isSelected = !((Card)this.cards.elementAt(this.selectedCard)).isSelected;
		this.selectedCards = this.getSelectedCardsValue();
		this.selectedCardsType = CardUtils.getType(this.selectedCards);
		if ((int)this.selectedCardsType != -1)
		{
			BoardScr.addInfo(T.cardTypeName[(int)this.selectedCardsType], 10, -1);
		}
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x000805E4 File Offset: 0x0007E9E4
	public void setCurrentCards(sbyte[] cardIDs, int fromSeat)
	{
		int num = 0;
		int y = 0;
		switch (fromSeat)
		{
		case 0:
			num = Canvas.hw;
			y = -27;
			break;
		case 1:
			num = -10;
			y = (Canvas.h + Canvas.hTab) / 2 - 20;
			break;
		case 2:
			num = Canvas.hw;
			y = Canvas.h + Canvas.hTab - 20;
			for (int i = this.cards.size() - 1; i >= 0; i--)
			{
				Card card = (Card)this.cards.elementAt(i);
				for (int j = 0; j < cardIDs.Length; j++)
				{
					if ((int)card.cardID == (int)cardIDs[j])
					{
						num = card.x;
						y = card.y;
						break;
					}
				}
			}
			break;
		case 3:
			num = Canvas.w + 10;
			y = (Canvas.h + Canvas.hTab) / 2 - 20;
			break;
		}
		int num2 = Canvas.hw + CRes.r.nextInt(20);
		int num3 = Canvas.hh - 20 * AvMain.hd + CRes.r.nextInt(25);
		if (Canvas.w < 150)
		{
			num3 += 20;
		}
		int num4 = cardIDs.Length;
		int num5 = (Canvas.w - 60) / num4 + 1;
		if (num5 > 12)
		{
			num5 = 12;
		}
		int num6 = (num5 * num4 >> 1) + 6;
		this.isFlying = true;
		this.currentCards = new MyVector();
		this.currentCardsValue = cardIDs;
		for (int k = 0; k < num4; k++)
		{
			Card card2 = new Card(cardIDs[k]);
			card2.x = num + k * this.disCard;
			card2.y = y;
			card2.xTo = num2 - num6;
			card2.yTo = num3;
			num6 -= num5 * AvMain.hd;
			this.currentCards.addElement(card2);
		}
		this.currentCardsType = CardUtils.getType(this.currentCardsValue);
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x000807E4 File Offset: 0x0007EBE4
	public override void doFire()
	{
		base.doFire();
		if (this.forceMove3Bich)
		{
			bool flag = false;
			for (int i = 0; i < this.selectedCards.Length; i++)
			{
				if ((int)this.selectedCards[i] == 0)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				BoardScr.showChat(GameMidlet.avatar.IDDB, T.youMustFire3Bich);
				return;
			}
			this.forceMove3Bich = false;
		}
		if (this.currentCards != null && this.currentCards.size() != 0 && !CardUtils.available(this.selectedCards, this.selectedCardsType, this.currentCardsValue, this.currentCardsType))
		{
			BoardScr.showChat(GameMidlet.avatar.IDDB, T.notSameOrSmaller);
			return;
		}
		CasinoService.gI().move(BoardScr.roomID, BoardScr.boardID, this.selectedCards);
		this.currentPlayer = -1;
		this.right = null;
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x000808CC File Offset: 0x0007ECCC
	private void setUpDown(bool iss)
	{
		((Card)this.cards.elementAt(this.selectedCard)).isSelected = iss;
		this.selectedCards = this.getSelectedCardsValue();
		this.selectedCardsType = CardUtils.getType(this.selectedCards);
		this.setPosCard(false);
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x0008091C File Offset: 0x0007ED1C
	public override void updateKey()
	{
		base.updateKey();
		if (Canvas.isPointerClick)
		{
			this.indexTran = -1;
			if (BoardScr.isStartGame && this.cards != null && this.cards.size() > 0)
			{
				for (int i = this.cards.size() - 1; i >= 0; i--)
				{
					Card card = (Card)this.cards.elementAt(i);
					if (card.setCollision())
					{
						this.isTran = true;
						this.indexTran = i;
						this.duX = Canvas.px - card.x;
						this.duY = Canvas.py - card.y;
						break;
					}
				}
			}
			Canvas.isPointerClick = false;
		}
		if (this.isTran)
		{
			if (Canvas.isPointerDown)
			{
				if (CRes.abs(Canvas.dx()) > 5 * AvMain.hd && CRes.abs(Canvas.dy()) > 5 * AvMain.hd && this.indexTran != -1)
				{
					Card card2 = (Card)this.cards.elementAt(this.indexTran);
					card2.x = Canvas.px - this.duX;
					card2.y = Canvas.py - this.duY;
				}
			}
			else if (Canvas.isPointerRelease)
			{
				if (CRes.abs(Canvas.dx()) < 10 * AvMain.hd && CRes.abs(Canvas.dy()) < 10 * AvMain.hd)
				{
					if (this.indexTran != -1)
					{
						this.selectedCard = this.indexTran;
						Card card3 = (Card)this.cards.elementAt(this.indexTran);
						this.setUpDown(!((Card)this.cards.elementAt(this.selectedCard)).isSelected);
						if (card3.isSelected)
						{
							card3.yTo = this.yShow - 15 * AvMain.hd;
						}
						else
						{
							card3.yTo = this.yShow;
						}
					}
				}
				else if (this.indexTran != -1 && this.cards != null && this.cards.size() > 0)
				{
					Card card4 = (Card)this.cards.elementAt(this.indexTran);
					for (int j = this.cards.size() - 1; j >= 0; j--)
					{
						Card card5 = (Card)this.cards.elementAt(j);
						if (j != this.indexTran && card5.setCollision())
						{
							Card card6 = (Card)this.cards.elementAt(this.indexTran);
							card6.x = Canvas.px - this.duX;
							card6.y = Canvas.py - this.duY;
							this.setChangeMyCard(this.indexTran, j);
							break;
						}
					}
				}
				this.indexTran = -1;
				this.isTran = false;
				Canvas.isPointerRelease = false;
			}
		}
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x00080C20 File Offset: 0x0007F020
	private void setChangeMyCard(int indexStart, int indexEnd)
	{
		if (indexStart < indexEnd)
		{
			Card card = (Card)this.cards.elementAt(indexStart);
			int x = card.x;
			int y = card.y;
			Card card2 = (Card)this.cards.elementAt(indexEnd);
			Card card3 = new Card(-1);
			Card.copyData(card3, card);
			for (int i = indexStart; i < indexEnd; i++)
			{
				Card card4 = (Card)this.cards.elementAt(i);
				int xTo = card4.xTo;
				int yTo = card4.yTo;
				Card card5 = (Card)this.cards.elementAt(i + 1);
				int x2 = card5.x;
				int y2 = card5.y;
				Card.copyData(card4, card5);
				card4.xTo = xTo;
				card4.yTo = yTo;
				card4.x = x2;
				card4.y = y2;
			}
			Card.copyData(card2, card3);
			card2.x = x;
			card2.y = y;
		}
		else if (indexStart > indexEnd)
		{
			Card card6 = (Card)this.cards.elementAt(indexStart);
			int x3 = card6.x;
			int y3 = card6.y;
			Card card7 = (Card)this.cards.elementAt(indexEnd);
			Card card8 = new Card(-1);
			Card.copyData(card8, card6);
			for (int j = indexStart; j > indexEnd; j--)
			{
				Card card9 = (Card)this.cards.elementAt(j);
				int xTo2 = card9.xTo;
				int yTo2 = card9.yTo;
				Card card10 = (Card)this.cards.elementAt(j - 1);
				int x4 = card10.x;
				int y4 = card10.y;
				Card.copyData(card9, card10);
				card9.xTo = xTo2;
				card9.yTo = yTo2;
				card9.x = x4;
				card9.y = y4;
			}
			Card.copyData(card7, card8);
			card7.x = x3;
			card7.y = y3;
		}
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x00080E20 File Offset: 0x0007F220
	public override void update()
	{
		base.update();
		if (BoardScr.isStartGame && this.cards != null && this.cards.size() > 0)
		{
			for (int i = this.cards.size() - 1; i >= 0; i--)
			{
				Card card = (Card)this.cards.elementAt(i);
				int num = card.translate();
				if (num == 1)
				{
					break;
				}
				if (num == -1)
				{
					card.isShow = false;
				}
			}
		}
		if (BoardScr.dieTime != 0L)
		{
			BoardScr.currentTime = (long)Environment.TickCount;
			if (BoardScr.currentTime > BoardScr.dieTime)
			{
				if (this.currentPlayer == GameMidlet.avatar.IDDB)
				{
					CasinoService.gI().skip();
					this.currentPlayer = -1;
				}
				BoardScr.dieTime = 0L;
			}
		}
		if (BoardScr.isStartGame || BoardScr.disableReady)
		{
			if (BoardScr.isGameEnd)
			{
				this.left = null;
				this.center = BoardScr.cmdBack;
				this.right = null;
			}
			else if (this.currentPlayer == GameMidlet.avatar.IDDB)
			{
				this.right = this.cmdSkip;
				if (this.getSelectedCardsValue().Length > 0)
				{
					if ((int)this.selectedCardsType != -1)
					{
						this.center = BoardScr.cmdFire;
					}
					else
					{
						this.center = null;
					}
				}
				else
				{
					this.center = null;
				}
			}
			else
			{
				this.right = null;
				this.center = null;
			}
			this.updateCurCard();
		}
		else
		{
			this.updateReady();
			this.right = null;
		}
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00080FC4 File Offset: 0x0007F3C4
	private void setPosCard(bool isTran)
	{
		if (this.cards.size() > 0 && !isTran)
		{
			int num = (Canvas.w - TLBoardScr.wCard / 2) / this.cards.size();
			if (num > TLBoardScr.wCard / 3 * 2)
			{
				num = TLBoardScr.wCard / 3 * 2;
			}
			this.disCard = (Canvas.w - 60) / this.cards.size() + 1;
			if (this.disCard > num)
			{
				this.disCard = num;
			}
			if (this.disCard < 9)
			{
				this.disCard = 9;
			}
			this.disCard = num;
			this.xShow = (Canvas.w - (this.disCard * this.cards.size() + (TLBoardScr.wCard - this.disCard)) >> 1) + TLBoardScr.wCard / 2;
			if (this.xShow < TLBoardScr.wCard / 2)
			{
				this.xShow = TLBoardScr.wCard / 2;
			}
		}
		int num2 = this.cards.size();
		int num3 = this.xShow;
		for (int i = 0; i < num2; i++)
		{
			Card card = (Card)this.cards.elementAt(i);
			int num4 = 0;
			if (card.isSelected)
			{
				num4 = -15 * AvMain.hd;
			}
			card.setPosTo(num3, this.yShow + num4);
			num3 += this.disCard;
			if (isTran)
			{
				card.x = card.xTo;
				card.y = card.yTo;
			}
		}
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x0008114C File Offset: 0x0007F54C
	public void updateCurCard()
	{
		if (this.currentCards != null && this.isFlying)
		{
			int num = 0;
			for (int i = 0; i < this.currentCards.size(); i++)
			{
				Card card = (Card)this.currentCards.elementAt(i);
				if (card.translate() == -1)
				{
					num++;
				}
			}
			if (num == this.currentCards.size())
			{
				this.isFlying = false;
			}
		}
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x000811C7 File Offset: 0x0007F5C7
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		base.paint(g);
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x000811D8 File Offset: 0x0007F5D8
	public override void paintMain(MyGraphics g)
	{
		base.paintMain(g);
		Canvas.resetTrans(g);
		this.paintCurrentCards(g);
		this.paintNamePlayers(g);
		if (BoardScr.isStartGame || BoardScr.disableReady)
		{
			this.paintMoney(g);
		}
		if (BoardScr.isStartGame)
		{
			this.paintCard(g);
		}
		if (BoardScr.isStartGame || BoardScr.disableReady)
		{
			this.paintShowCards(g);
		}
		base.paintChat(g);
		Canvas.resetTrans(g);
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00081254 File Offset: 0x0007F654
	private void paintMoney(MyGraphics g)
	{
		for (int i = 0; i < 4; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != -1)
			{
				int num = 0;
				int num2 = 0;
				if (BoardScr.indexPlayer[i] == 2)
				{
					num = -60;
				}
				if (BoardScr.indexPlayer[i] == 1)
				{
					num2 = -10;
				}
				else if (BoardScr.indexPlayer[i] == 3)
				{
					num2 = 10;
				}
				if (Canvas.w > 160)
				{
					Canvas.smallFontYellow.drawString(g, avatar.getMoneyNew() + " " + T.getMoney(), BoardScr.posAvatar[BoardScr.indexPlayer[i]].x + num2, BoardScr.posAvatar[BoardScr.indexPlayer[i]].y + 5 + num, BoardScr.posAvatar[BoardScr.indexPlayer[i]].anchor);
				}
				if (avatar.IDDB == this.currentPlayer && this.center != BoardScr.cmdBack)
				{
					string text = string.Empty;
					if (BoardScr.dieTime != 0L)
					{
						long num3 = (BoardScr.currentTime - BoardScr.dieTime) / 1000L;
						text += -num3;
					}
					int x = BoardScr.posAvatar[BoardScr.indexPlayer[i]].x;
					int num4 = BoardScr.posAvatar[BoardScr.indexPlayer[i]].y + (int)AvMain.hBlack;
					if (BoardScr.indexPlayer[i] == 2)
					{
						num4 = this.yShow - TLBoardScr.hcard / 2 - (Canvas.blackF.getHeight() + 4);
					}
					PaintPopup.fill(x - 10 * AvMain.hd, num4, 22 * AvMain.hd, Canvas.blackF.getHeight() + 2, 16776365, g);
					g.setColor(332544);
					g.drawRect((float)(x - 10 * AvMain.hd), (float)num4, (float)(22 * AvMain.hd), (float)(Canvas.blackF.getHeight() + 2));
					Canvas.blackF.drawString(g, text, x, num4 + 1, 2);
				}
			}
		}
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00081468 File Offset: 0x0007F868
	public void paintCard(MyGraphics g)
	{
		if (BoardScr.isStartGame && this.cards != null && this.cards.size() > 0)
		{
			int num = this.cards.size();
			for (int i = 0; i < num; i++)
			{
				Card card = (Card)this.cards.elementAt(i);
				Card card2 = new Card(-1, false);
				card2.x = card.x;
				card2.y = card.y;
				if (!card.isShow)
				{
					card2 = (Card)this.cards.elementAt(i);
				}
				if (Canvas.w < 150)
				{
					card2.paintSmall(g, false);
				}
				else if (i == num - 1 || card.isSelected || (card2 != null && card2.isSelected))
				{
					card2.paintFull(g);
				}
				else if (this.disCard > 14 || card2.x != card2.xTo || card2.y != card2.yTo)
				{
					card2.paintHalfBackFull(g);
				}
				else
				{
					card2.paintHalf(g);
				}
				if (i == this.selectedCard)
				{
					int num2 = card2.y - TLBoardScr.hcard / 2 - 2 + ((Canvas.gameTick % 10 <= 4) ? 0 : 2);
					int num3 = card2.x - TLBoardScr.wCard / 2 + 5 * AvMain.hd;
				}
			}
		}
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x000815FC File Offset: 0x0007F9FC
	private void paintShowCards(MyGraphics g)
	{
		if (this.cardShows == null || this.cardShows.size() == 0)
		{
			return;
		}
		int num = this.cardShows.size();
		int num2 = (Canvas.w - 60) / num + 1;
		if (num2 > 12)
		{
			num2 = 12;
		}
		int num3 = Canvas.hw - (num2 * num >> 1) + 6;
		int y = (Canvas.h + Canvas.hTab) / 2;
		for (int i = 0; i < num; i++)
		{
			Card card = (Card)this.cardShows.elementAt(i);
			card.x = num3;
			card.y = y;
			num3 += num2 * AvMain.hd;
			if (Canvas.w < 150)
			{
				card.paintSmall(g, false);
			}
			else if (i == num - 1)
			{
				card.paintFull(g);
			}
			else
			{
				card.paintHalf(g);
			}
		}
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x000816E5 File Offset: 0x0007FAE5
	private void paintCurrentCards(MyGraphics g)
	{
		if (this.currentCards == null || this.currentCards.size() == 0)
		{
			return;
		}
		this.paintCCard(g);
		if (!this.isFlying)
		{
		}
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00081718 File Offset: 0x0007FB18
	private void paintCCard(MyGraphics g)
	{
		int num = this.currentCards.size();
		for (int i = 0; i < num; i++)
		{
			Card card = (Card)this.currentCards.elementAt(i);
			if (Canvas.w < 150)
			{
				card.paintSmall(g, false);
			}
			else if (i == num - 1)
			{
				card.paintFull(g);
			}
			else
			{
				card.paintHalf(g);
			}
		}
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x00081790 File Offset: 0x0007FB90
	public void start(int whoMoveFirst, sbyte interval1, MyVector myCards)
	{
		base.start(whoMoveFirst, (int)interval1);
		BoardScr.isStartGame = true;
		this.forceMove3Bich = false;
		if (this.isFirstMatch && whoMoveFirst == GameMidlet.avatar.IDDB)
		{
			for (int i = 0; i < myCards.size(); i++)
			{
				Card card = (Card)myCards.elementAt(i);
				if ((int)card.cardID == 0)
				{
					this.forceMove3Bich = true;
					break;
				}
			}
		}
		this.cardShows = null;
		this.currentCards = new MyVector();
		this.currentCardsType = -1;
		this.currentCardsValue = new sbyte[0];
		BoardScr.isGameEnd = false;
		this.cards = myCards;
		this.sort(myCards);
		for (int j = 0; j < this.cards.size(); j++)
		{
			Card card2 = (Card)this.cards.elementAt(j);
			card2.x = Canvas.hw;
			card2.y = (Canvas.h + Canvas.hTab) / 2;
			card2.isShow = true;
		}
		int num = 0;
		for (int k = 0; k < BoardScr.numPlayer; k++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(k);
			if (avatar.IDDB != -1)
			{
				num++;
			}
		}
		BoardScr.interval = (int)interval1;
		BoardScr.dieTime = (long)(Environment.TickCount + (int)interval1 * 1000);
		if (whoMoveFirst == GameMidlet.avatar.IDDB)
		{
			this.right = this.cmdSkip;
		}
		Avatar avatarByID = BoardScr.getAvatarByID(whoMoveFirst);
		BoardScr.addInfo(avatarByID.name + T.firstFire, 20, avatarByID.IDDB);
		this.currentCardsType = -1;
		this.currentCardsValue = new sbyte[0];
		this.selectedCard = 2;
		this.currentPlayer = whoMoveFirst;
		this.setPosPlaying();
		this.setPosCard(false);
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00081968 File Offset: 0x0007FD68
	public void move(int whoMove, sbyte[] movedCards, int nextMove)
	{
		this.forceMove3Bich = false;
		if (whoMove != -1)
		{
			int indexByID = BoardScr.getIndexByID(whoMove);
			this.setCurrentCards(movedCards, BoardScr.indexPlayer[indexByID]);
		}
		if (whoMove == GameMidlet.avatar.IDDB)
		{
			this.removeCards(movedCards);
			this.selectedCard = 0;
			this.setPosCard(false);
		}
		this.currentPlayer = nextMove;
		if (this.currentPlayer == GameMidlet.avatar.IDDB)
		{
			if (this.getSelectedCardsValue().Length == 0)
			{
				this.right = this.cmdSkip;
			}
			else
			{
				this.right = BoardScr.cmdFire;
			}
		}
		else
		{
			this.right = null;
		}
		if (BoardScr.interval == 0)
		{
			BoardScr.interval = 30;
		}
		BoardScr.dieTime = (long)(Environment.TickCount + BoardScr.interval * 1000);
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00081A38 File Offset: 0x0007FE38
	public void skip(int whoSkip, int nextMove, bool isClearCurrentCards)
	{
		if (isClearCurrentCards)
		{
			base.repaint();
		}
		Avatar avatarByID = BoardScr.getAvatarByID(whoSkip);
		string info = string.Empty;
		if (avatarByID.name.Equals(string.Empty))
		{
			info = T.exitBoard;
		}
		else
		{
			info = T.skip;
		}
		BoardScr.addInfo(info, 60, avatarByID.IDDB);
		this.currentPlayer = nextMove;
		if (isClearCurrentCards)
		{
			this.currentCards = new MyVector();
			this.currentCardsType = -1;
			this.currentCardsValue = new sbyte[0];
		}
		if (this.currentPlayer == GameMidlet.avatar.IDDB)
		{
			if (this.getSelectedCardsValue().Length == 0)
			{
				this.right = this.cmdSkip;
			}
			else
			{
				this.right = BoardScr.cmdFire;
			}
		}
		else
		{
			this.right = null;
		}
		BoardScr.dieTime = (long)(Environment.TickCount + BoardScr.interval * 1000);
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00081B20 File Offset: 0x0007FF20
	public void showCards(int whoShow, sbyte[] card)
	{
		Avatar avatarByID = BoardScr.getAvatarByID(whoShow);
		CardUtils.sort(card);
		this.cardShows = new MyVector();
		for (int i = 0; i < card.Length; i++)
		{
			this.cardShows.addElement(new Card(card[i]));
		}
		if (avatarByID != null && avatarByID.IDDB == whoShow && this.cards != null)
		{
			this.cards.removeAllElements();
		}
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x00081B94 File Offset: 0x0007FF94
	public void finish(int whoFinish, sbyte finishPosition, int dMoney, int dExp)
	{
		Avatar avatarByID = BoardScr.getAvatarByID(whoFinish);
		if (avatarByID != null)
		{
			avatarByID.isReady = false;
			int num = avatarByID.exp + dExp;
			if (num < 0)
			{
				num = 0;
			}
			avatarByID.setExp(num);
			avatarByID.setMoneyNew(avatarByID.getMoneyNew() + dMoney);
			if (avatarByID.IDDB == GameMidlet.avatar.IDDB)
			{
				GameMidlet.avatar.setMoneyNew(avatarByID.getMoneyNew());
			}
		}
		BoardScr.showChat(whoFinish, T.goad + ((int)finishPosition + 1));
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00081C1B File Offset: 0x0008001B
	public void stopGame()
	{
		BoardScr.isGameEnd = true;
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x00081C23 File Offset: 0x00080023
	public void moveError(string info)
	{
		BoardScr.addInfo(info, 100, GameMidlet.avatar.IDDB);
		this.currentPlayer = GameMidlet.avatar.IDDB;
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00081C47 File Offset: 0x00080047
	public void setMode(bool hasStartGame)
	{
		base.repaint();
		BoardScr.isStartGame = hasStartGame;
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00081C58 File Offset: 0x00080058
	public void removeCards(sbyte[] removeCards)
	{
		int num = this.cards.size();
		for (int i = num - 1; i >= 0; i--)
		{
			Card card = (Card)this.cards.elementAt(i);
			for (int j = 0; j < removeCards.Length; j++)
			{
				if ((int)card.cardID == (int)removeCards[j])
				{
					this.cards.removeElementAt(i);
				}
			}
		}
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x00081CC8 File Offset: 0x000800C8
	public sbyte[] getSelectedCardsValue()
	{
		MyVector myVector = new MyVector();
		int num = this.cards.size();
		for (int i = 0; i < num; i++)
		{
			Card card = (Card)this.cards.elementAt(i);
			if (card.isSelected)
			{
				myVector.addElement(card);
			}
		}
		int num2 = myVector.size();
		sbyte[] array = new sbyte[num2];
		for (int j = 0; j < num2; j++)
		{
			array[j] = ((Card)myVector.elementAt(j)).cardID;
		}
		CardUtils.sort(array);
		return array;
	}

	// Token: 0x04001000 RID: 4096
	public static TLBoardScr instance;

	// Token: 0x04001001 RID: 4097
	public MyVector currentCards;

	// Token: 0x04001002 RID: 4098
	public sbyte[] currentCardsValue;

	// Token: 0x04001003 RID: 4099
	public sbyte currentCardsType;

	// Token: 0x04001004 RID: 4100
	public MyVector cardShows;

	// Token: 0x04001005 RID: 4101
	private sbyte[] selectedCards;

	// Token: 0x04001006 RID: 4102
	private sbyte selectedCardsType;

	// Token: 0x04001007 RID: 4103
	public MyVector cards;

	// Token: 0x04001008 RID: 4104
	private Command cmdSkip;

	// Token: 0x04001009 RID: 4105
	private bool isFlying;

	// Token: 0x0400100A RID: 4106
	public new static int wCard;

	// Token: 0x0400100B RID: 4107
	public new static int hcard;

	// Token: 0x0400100C RID: 4108
	private int xShow;

	// Token: 0x0400100D RID: 4109
	private int yShow;

	// Token: 0x0400100E RID: 4110
	private bool isDown;

	// Token: 0x0400100F RID: 4111
	private bool trans;

	// Token: 0x04001010 RID: 4112
	private int pa;

	// Token: 0x04001011 RID: 4113
	private new bool isTran;

	// Token: 0x04001012 RID: 4114
	private int duX;

	// Token: 0x04001013 RID: 4115
	private int duY;

	// Token: 0x04001014 RID: 4116
	private int indexTran;

	// Token: 0x04001015 RID: 4117
	private bool forceMove3Bich;

	// Token: 0x04001016 RID: 4118
	public bool isFirstMatch = true;
}
