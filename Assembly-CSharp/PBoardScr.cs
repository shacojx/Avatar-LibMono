using System;

// Token: 0x020001C2 RID: 450
public class PBoardScr : BoardScr
{
	// Token: 0x06000C43 RID: 3139 RVA: 0x0007AEC4 File Offset: 0x000792C4
	public PBoardScr()
	{
		this.cardShow = new Card[4][];
		for (int i = 0; i < this.cardShow.Length; i++)
		{
			this.cardShow[i] = new Card[4];
		}
		this.ShowHaPhom = new int[4][];
		for (int j = 0; j < this.ShowHaPhom.Length; j++)
		{
			this.ShowHaPhom[j] = new int[12];
		}
		this.showCardEat = new Card[4][];
		for (int k = 0; k < this.showCardEat.Length; k++)
		{
			this.showCardEat[k] = new Card[3];
		}
		this.cardRac = new int[4][];
		for (int l = 0; l < this.cardRac.Length; l++)
		{
			this.cardRac[l] = new int[11];
		}
		this.reset();
		this.cmdEat = new Command(T.eat, 7);
		this.cmdGet = new Command(T.gett, 8);
		this.cmdHaPhom = new Command(T.haPhom, 9);
		Canvas.paint.initPosPhom();
		this.distantCard = 35 * AvMain.hd;
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x0007B08D File Offset: 0x0007948D
	public static PBoardScr gI()
	{
		if (PBoardScr.instance == null)
		{
			PBoardScr.instance = new PBoardScr();
		}
		return PBoardScr.instance;
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x0007B0A8 File Offset: 0x000794A8
	public override void commandTab(int index)
	{
		switch (index)
		{
		case 7:
			this.doEat();
			break;
		case 8:
			this.doGet();
			break;
		case 9:
			this.doHaPhom();
			break;
		default:
			base.commandTab(index);
			break;
		}
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x0007B0FB File Offset: 0x000794FB
	public override void resetCard()
	{
		this.reset();
		base.resetCard();
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x0007B10C File Offset: 0x0007950C
	public void reset()
	{
		for (int i = 0; i < 10; i++)
		{
			if (i < 3)
			{
				this.cardEat[i] = new Card(-1, true);
			}
		}
		this.cardShow = new Card[4][];
		for (int j = 0; j < this.cardShow.Length; j++)
		{
			this.cardShow[j] = new Card[4];
		}
		this.selectedCard = 0;
		this.cardCurrent = -1;
		for (int k = 0; k < 4; k++)
		{
			for (int l = 0; l < 4; l++)
			{
				if (l < 3)
				{
					this.showCardEat[k][l] = null;
				}
			}
			for (int m = 0; m < 12; m++)
			{
				this.ShowHaPhom[k][m] = -1;
				if (m < 11)
				{
					this.cardRac[k][m] = -1;
				}
			}
			this.scorePlayer[k] = -1;
			this.numC[k] = 0;
			this.numCardEat[k] = 0;
			this.numCardPhom[k] = 0;
			this.numCardRac[k] = 0;
			this.numberCard[k] = 0;
		}
		this.numPhom = 0;
		this.phomRandom = 3;
		this.phomHa = 0;
		this.hCard = new Card(-1, true);
		this.assetChange = -1;
		this.key = 1;
		this.finish = false;
		this.winer = -1;
		this.isU = false;
		this.isHaPhom = false;
		this.pos = -2;
		this.pause = false;
		this.denBai = -1;
		this.firstHa = -1;
		this.isEatCard = false;
		this.getC = new AvPosition(Canvas.hw, Canvas.hh, 3);
		this.cardE = new Card(-1, true);
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x0007B2BA File Offset: 0x000796BA
	public override void init()
	{
		base.init();
		this.initP();
		if (BoardScr.isStartGame)
		{
			this.setPosCard(false);
		}
		this.getC = new AvPosition(Canvas.hw, Canvas.hh, 3);
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x0007B2F0 File Offset: 0x000796F0
	public void initP()
	{
		int h = Canvas.h;
		PBoardScr.posCard = new AvPosition();
		this.arror = new AvPosition();
		PBoardScr.posCard.x = Canvas.hw - 27;
		PBoardScr.posCard.y = MyScreen.getHTF() - AvMain.hFillTab;
		this.arror.x = PBoardScr.posCard.x - 24;
		this.arror.y = PBoardScr.posCard.y - BoardScr.hcard / 2 - 4;
		Canvas.paint.initPosPhom();
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x0007B380 File Offset: 0x00079780
	public void setCmdEatAndGet()
	{
		this.center = this.cmdEat;
		this.right = this.cmdGet;
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x0007B39A File Offset: 0x0007979A
	public void setCmdFire()
	{
		this.center = BoardScr.cmdFire;
		this.right = null;
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x0007B3AE File Offset: 0x000797AE
	public void resetCmd()
	{
		this.center = null;
		this.right = null;
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0007B3BE File Offset: 0x000797BE
	public void setcmdHaPhom()
	{
		this.center = this.cmdHaPhom;
		this.right = null;
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0007B3D3 File Offset: 0x000797D3
	public void setContinue()
	{
		this.center = BoardScr.cmdBack;
		this.right = null;
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x0007B3E7 File Offset: 0x000797E7
	public override void doContinue()
	{
		this.finish = false;
		this.resetCard();
		base.doContinue();
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0007B3FC File Offset: 0x000797FC
	private void resetChangeCard()
	{
		if (this.assetChange == -1 || (int)this.hCard.cardID == -1)
		{
			return;
		}
		this.cleanCard(this.selectedCard);
		this.cleanUp(this.assetChange);
		this.myCard[this.assetChange] = this.hCard;
		this.hCard = new Card(-1, true);
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x0007B460 File Offset: 0x00079860
	private void resetCell(int id)
	{
		this.myCard[id] = new Card(-1, true);
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x0007B471 File Offset: 0x00079871
	private void resetCurrentTime()
	{
		BoardScr.currentTime = (long)(Environment.TickCount / 1000);
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0007B484 File Offset: 0x00079884
	public void resetOrderCard()
	{
		if (this.trans && this.remem == 2)
		{
			this.remem = 0;
			this.trans = false;
			if ((int)this.hCard.cardID != -1)
			{
				this.checkCardChange();
			}
			this.resetUpMyCard(this.selectedCard);
			Canvas.isPointerDown = false;
		}
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0007B4E0 File Offset: 0x000798E0
	public override void updateKey()
	{
		base.updateKey();
		if (Canvas.isPointerClick)
		{
			this.indexTran = -1;
			if (BoardScr.isStartGame && this.myCard != null)
			{
				for (int i = this.myCard.Length - 1; i >= 0; i--)
				{
					if (this.myCard[i] != null && (int)this.myCard[i].cardID != -1 && this.myCard[i].setCollision())
					{
						this.isTran = true;
						this.indexTran = i;
						this.duX = Canvas.px - this.myCard[i].x;
						this.duY = Canvas.py - this.myCard[i].y;
						break;
					}
				}
			}
			Canvas.isPointerClick = false;
		}
		if (this.isTran)
		{
			if (Canvas.isPointerDown && CRes.abs(Canvas.dx()) > 5 * AvMain.hd && CRes.abs(Canvas.dy()) > 5 * AvMain.hd && this.indexTran != -1)
			{
				this.myCard[this.indexTran].x = Canvas.px - this.duX;
				this.myCard[this.indexTran].y = Canvas.py - this.duY;
			}
			if (Canvas.isPointerRelease)
			{
				if (CRes.abs(Canvas.dx()) < 10 * AvMain.hd && CRes.abs(Canvas.dy()) < 10 * AvMain.hd)
				{
					if (this.indexTran != -1)
					{
						if (!this.myCard[this.indexTran].isUp)
						{
							this.myCard[this.indexTran].isUp = true;
							this.myCard[this.indexTran].yTo = PBoardScr.posCard.y - 15 * AvMain.hd;
						}
						else
						{
							this.myCard[this.indexTran].isUp = false;
							this.myCard[this.indexTran].yTo = PBoardScr.posCard.y;
						}
					}
				}
				else if (this.indexTran != -1 && this.myCard != null)
				{
					for (int j = this.myCard.Length - 1; j >= 0; j--)
					{
						if (j != this.indexTran && (int)this.myCard[j].cardID != -1 && this.myCard[j].setCollision())
						{
							this.setChangeMyCard(this.indexTran, j);
							this.cleanPhomRandom();
							this.findPhom();
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

	// Token: 0x06000C55 RID: 3157 RVA: 0x0007B7A0 File Offset: 0x00079BA0
	private void setChangeMyCard(int indexStart, int indexEnd)
	{
		if (indexStart < indexEnd)
		{
			Card card = this.myCard[indexStart];
			int x = this.myCard[indexEnd].x;
			int y = this.myCard[indexEnd].y;
			int x2 = card.x;
			int y2 = card.y;
			this.myCard[indexStart].x = this.myCard[indexStart].xTo;
			this.myCard[indexStart].y = this.myCard[indexStart].yTo;
			for (int i = indexStart; i < indexEnd; i++)
			{
				int x3 = this.myCard[i].x;
				int y3 = this.myCard[i].y;
				this.myCard[i] = this.myCard[i + 1];
				this.myCard[i].xTo = x3;
				this.myCard[i].yTo = y3;
			}
			this.myCard[indexEnd] = card;
			this.myCard[indexEnd].x = x2;
			this.myCard[indexEnd].y = y2;
			this.myCard[indexEnd].yTo = y;
			this.myCard[indexEnd].xTo = x;
		}
		else if (indexStart > indexEnd)
		{
			Card card2 = this.myCard[indexStart];
			int x4 = this.myCard[indexEnd].x;
			int y4 = this.myCard[indexEnd].y;
			int x5 = card2.x;
			int y5 = card2.y;
			this.myCard[indexStart].x = this.myCard[indexStart].xTo;
			this.myCard[indexStart].y = this.myCard[indexStart].yTo;
			for (int j = indexStart; j > indexEnd; j--)
			{
				int x6 = this.myCard[j].x;
				int y6 = this.myCard[j].y;
				this.myCard[j] = this.myCard[j - 1];
				this.myCard[j].xTo = x6;
				this.myCard[j].yTo = y6;
			}
			this.myCard[indexEnd] = card2;
			this.myCard[indexEnd].x = x5;
			this.myCard[indexEnd].y = y5;
			this.myCard[indexEnd].yTo = y4;
			this.myCard[indexEnd].xTo = x4;
		}
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x0007B9F4 File Offset: 0x00079DF4
	public void resetUpMyCard(int index)
	{
		if ((int)this.myCard[index].cardID != -1 && !this.myCard[index].isUp)
		{
			this.myCard[index].yTo = PBoardScr.posCard.y;
		}
		this.myCard[index].isUp = false;
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x0007BA4C File Offset: 0x00079E4C
	public override void update()
	{
		base.update();
		if (BoardScr.isStartGame || BoardScr.disableReady)
		{
			if (BoardScr.isStartGame && !this.isTran && this.myCard != null)
			{
				for (int i = this.myCard.Length - 1; i >= 0; i--)
				{
					if (this.myCard[i] != null)
					{
						int num = this.myCard[i].translate();
						if (num == -1)
						{
							this.myCard[i].isShow = false;
						}
					}
				}
			}
			for (int j = 0; j < 4; j++)
			{
				if (this.cardShow[j] != null)
				{
					for (int k = 0; k < this.cardShow[j].Length; k++)
					{
						if (this.cardShow[j][k] != null)
						{
							this.cardShow[j][k].translate();
						}
					}
				}
			}
			for (int l = 0; l < this.showCardEat.Length; l++)
			{
				for (int m = 0; m < this.showCardEat[l].Length; m++)
				{
					if (this.showCardEat[l][m] != null)
					{
						this.showCardEat[l][m].translate();
					}
				}
			}
			this.checkTimeLimit();
		}
		else
		{
			this.updateReady();
		}
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0007BBA8 File Offset: 0x00079FA8
	private void setPosCard(bool isT)
	{
		if (BoardScr.disableReady)
		{
			return;
		}
		int num = this.getAssetCard();
		if (num == -1)
		{
			num = 10;
		}
		if ((BoardScr.isStartGame || BoardScr.disableReady) && num != 0)
		{
			PBoardScr.disCard = (Canvas.w - BoardScr.wCard / 2) / num;
			if (PBoardScr.disCard > BoardScr.wCard / 3 * 2)
			{
				PBoardScr.disCard = BoardScr.wCard / 3 * 2;
			}
		}
		PBoardScr.disShow = PBoardScr.disCard;
		if (PBoardScr.disShow > BoardScr.wCard / 4)
		{
			PBoardScr.disShow = BoardScr.wCard / 4;
		}
		if (!isT)
		{
			this.xShow = (Canvas.w - (PBoardScr.disCard * num + (BoardScr.wCard - PBoardScr.disCard)) >> 1) + BoardScr.wCard / 2;
			if (this.xShow < BoardScr.wCard / 2)
			{
				this.xShow = BoardScr.wCard / 2;
			}
		}
		for (int i = 0; i < 10; i++)
		{
			int num2 = 0;
			if (this.myCard[i].isUp)
			{
				num2 = 15 * AvMain.hd;
			}
			this.myCard[i].setPosTo(this.xShow + i * PBoardScr.disCard, PBoardScr.posCard.y - num2);
			this.yShow = this.myCard[i].yTo - BoardScr.hcard / 2 - 20;
			if (isT)
			{
				this.myCard[i].x = this.myCard[i].xTo;
				this.myCard[i].y = this.myCard[i].yTo;
			}
		}
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x0007BD44 File Offset: 0x0007A144
	public void checkTimeLimit()
	{
		BoardScr.dieTime = (long)((int)((long)(Environment.TickCount / 1000) - BoardScr.currentTime));
		if ((long)BoardScr.interval - BoardScr.dieTime < 0L)
		{
			if (this.center == this.cmdEat && this.right == this.cmdGet)
			{
				this.doGet();
				this.resetCmd();
			}
			else if (this.center == BoardScr.cmdFire)
			{
				int num = 0;
				for (int i = 1; i < 10; i++)
				{
					if ((int)this.myCard[i].phom == 0 && (int)this.myCard[i].cardID > (int)this.myCard[num].cardID)
					{
						num = i;
					}
				}
				this.resetChangeCard();
				CasinoService.gI().firePhom(this.myCard[num].cardID);
			}
			else if (this.center == this.cmdHaPhom)
			{
				this.resetChangeCard();
				this.cmdHaPhom.action.perform();
			}
		}
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x0007BE54 File Offset: 0x0007A254
	private void checkCardChange()
	{
		int num = -1;
		for (int i = 0; i < 3; i++)
		{
			if ((int)this.hCard.phom == (int)this.cardEat[i].phom)
			{
				num = (int)this.cardEat[i].cardID;
			}
		}
		this.myCard[this.selectedCard] = this.hCard;
		if (num != -1)
		{
			if (!this.checkCardToEat(this.selectedCard))
			{
				this.hCard = this.myCard[this.selectedCard];
				this.resetCell(this.selectedCard);
				this.resetChangeCard();
				return;
			}
			this.resetCell(this.selectedCard);
		}
		if ((int)this.hCard.phom != 0 && ((this.selectedCard > 0 && (int)this.myCard[this.selectedCard - 1].phom == (int)this.hCard.phom) || (this.selectedCard < 9 && (int)this.myCard[this.selectedCard + 1].phom == (int)this.hCard.phom)))
		{
			this.myCard[this.selectedCard] = this.hCard;
			this.hCard = new Card(-1, true);
			return;
		}
		if (num != -1 && (int)this.hCard.phom != 0)
		{
			if (num != -1)
			{
				this.pos = -1;
				if (this.assetChange != this.selectedCard)
				{
					this.doResetPhomEat(this.hCard, num);
				}
			}
			return;
		}
		if (this.selectedCard < 9)
		{
			int j = 0;
			while (j < 3)
			{
				if ((int)this.cardEat[j].phom != 0 && (int)this.myCard[this.selectedCard + 1].phom == (int)this.cardEat[j].phom)
				{
					int[] array = new int[10];
					int num2 = 0;
					for (int k = 0; k < 10; k++)
					{
						if ((int)this.myCard[k].phom == (int)this.cardEat[j].phom)
						{
							array[k] = (int)this.myCard[k].cardID;
						}
						else
						{
							array[k] = -1;
							if (num2 == 0)
							{
								num2 = 1;
								array[k] = (int)this.hCard.cardID;
							}
						}
					}
					array = this.orderArrayIncrease(array);
					if (this.checkPhomBaLa(array) || this.checkPhomSanh(array))
					{
						this.pos = (int)this.hCard.phom;
						this.hCard.phom = this.cardEat[j].phom;
						this.myCard[this.selectedCard] = this.hCard;
						if (this.assetChange != this.selectedCard)
						{
							this.doResetPhomEat(this.cardEat[j], (int)this.cardEat[j].cardID);
						}
						return;
					}
					this.resetChangeCard();
					return;
				}
				else
				{
					j++;
				}
			}
		}
		int num3 = this.setCardEaTrue();
		if (this.selectedCard >= num3 && num3 != -1)
		{
			this.resetChangeCard();
			return;
		}
		this.myCard[this.selectedCard] = this.hCard;
		this.hCard = new Card(-1, true);
		if (this.assetChange != this.selectedCard)
		{
			this.myCard[this.selectedCard].phom = 0;
			this.cleanPhomRandom();
			this.findPhom();
		}
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0007C1B8 File Offset: 0x0007A5B8
	private int setCardEaTrue()
	{
		for (int i = 0; i < 10; i++)
		{
			if ((int)this.myCard[i].cardID == -1)
			{
				return i;
			}
			if (this.searchCardEat((int)this.myCard[i].phom))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0007C20C File Offset: 0x0007A60C
	private void doResetPhomEat(Card cardID, int cardeat)
	{
		this.resetOrderCard();
		this.pause = true;
		int[] array = new int[5];
		for (int i = 0; i < 5; i++)
		{
			array[i] = -1;
		}
		int[] array2 = new int[6];
		for (int j = 0; j < 6; j++)
		{
			array2[j] = -1;
		}
		int num = 0;
		for (int k = 0; k < 10; k++)
		{
			if ((int)this.myCard[k].phom == (int)cardID.phom)
			{
				array2[num] = (int)this.myCard[k].cardID;
				num++;
			}
		}
		if (array2[5] != -1)
		{
			this.orderArrayIncrease(array2);
			int num2 = 0;
			for (int l = 0; l < array2.Length; l++)
			{
				if (array2[l] == cardeat)
				{
					num2 = l;
				}
			}
			int num3 = 0;
			if (num2 < 3)
			{
				for (int m = 0; m < array2.Length; m++)
				{
					if (m > 2)
					{
						for (int n = 0; n < 10; n++)
						{
							if (array2[m] == (int)this.myCard[n].cardID)
							{
								this.myCard[n].phom = 0;
							}
						}
					}
					else
					{
						array[num3] = array2[m];
						num3++;
						for (int num4 = 0; num4 < 10; num4++)
						{
							if (array2[m] == (int)this.myCard[num4].cardID)
							{
								Card card = this.myCard[num4];
								this.cleanCard(num4);
								this.myCard[this.getAssetCard()] = card;
							}
						}
					}
				}
			}
			else
			{
				for (int num5 = 0; num5 < array2.Length; num5++)
				{
					if (num5 < 3)
					{
						for (int num6 = 0; num6 < 10; num6++)
						{
							if (array2[num5] == (int)this.myCard[num6].cardID)
							{
								this.myCard[num6].phom = 0;
							}
						}
					}
					else
					{
						array[num3] = array2[num5];
						num3++;
						for (int num7 = 0; num7 < 10; num7++)
						{
							if (array2[num5] == (int)this.myCard[num7].cardID)
							{
								Card card2 = this.myCard[num7];
								this.cleanCard(num7);
								this.myCard[this.getAssetCard()] = card2;
							}
						}
					}
				}
			}
		}
		else
		{
			int num8 = 0;
			for (int num9 = 0; num9 < 10; num9++)
			{
				if ((int)this.myCard[num9].phom == (int)cardID.phom)
				{
					array[num8] = (int)this.myCard[num9].cardID;
					num8++;
				}
			}
		}
		this.orderArrayIncrease(array);
		CasinoService.gI().doResetPhomEatPhom(array, cardeat);
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x0007C4E4 File Offset: 0x0007A8E4
	public void onResetPhomEat(sbyte isReset)
	{
		this.resetOrderCard();
		this.pause = false;
		if ((int)isReset == 0)
		{
			if (this.pos == -1)
			{
				this.myCard[this.selectedCard] = this.hCard;
				this.myCard[this.selectedCard].phom = 0;
				this.hCard = new Card(-1, true);
				if (this.assetChange != this.selectedCard)
				{
					this.cleanPhomRandom();
					this.findPhom();
				}
				this.assetChange = -1;
			}
			else if (this.pos >= 0)
			{
				this.hCard = new Card(-1, true);
				if (this.assetChange != this.selectedCard)
				{
					this.myCard[this.selectedCard].phom = 0;
					this.cleanPhomRandom();
					this.findPhom();
				}
			}
		}
		else if (this.pos == -1)
		{
			this.resetCell(this.selectedCard);
			this.resetChangeCard();
		}
		else if (this.pos >= 0)
		{
			this.resetCell(this.selectedCard);
			this.resetChangeCard();
			this.myCard[this.assetChange].phom = (sbyte)this.pos;
		}
		this.pos = -2;
		this.setPosCard(false);
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x0007C624 File Offset: 0x0007AA24
	private void cleanUp(int id)
	{
		for (int i = 9; i > id; i--)
		{
			this.myCard[i] = this.myCard[i - 1];
		}
		this.resetCell(id);
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x0007C660 File Offset: 0x0007AA60
	public void cleanCard(int card)
	{
		for (int i = card; i < 9; i++)
		{
			this.myCard[i] = this.myCard[i + 1];
		}
		this.resetCell(9);
		this.resetUpMyCard(card);
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x0007C6A1 File Offset: 0x0007AAA1
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		base.paint(g);
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x0007C6B4 File Offset: 0x0007AAB4
	public override void paintMain(MyGraphics g)
	{
		base.paintMain(g);
		this.paintNamePlayers(g);
		this.paintTime(g);
		if (BoardScr.isStartGame)
		{
			this.paintMoneys(g);
			this.paintMyCard(g);
			this.paintEatCard(g);
			if (!this.finish)
			{
				int indexByID = BoardScr.getIndexByID(this.firstHa);
				switch (BoardScr.indexPlayer[indexByID])
				{
				case 0:
					g.drawImage(BoardScr.imgBan, (float)PBoardScr.posNamePlaying[0].x, (float)(PBoardScr.posNamePlaying[0].y + 13 * AvMain.hd + (int)AvMain.hSmall), 3);
					break;
				case 1:
					g.drawImage(BoardScr.imgBan, (float)PBoardScr.posNamePlaying[1].x, (float)(PBoardScr.posNamePlaying[1].y + (int)AvMain.hSmall), 0);
					break;
				case 2:
					g.drawImage(BoardScr.imgBan, (float)PBoardScr.posNamePlaying[2].x, (float)(PBoardScr.posNamePlaying[2].y - 13 * AvMain.hd), 3);
					break;
				case 3:
					g.drawImage(BoardScr.imgBan, (float)(PBoardScr.posNamePlaying[3].x - 13 * AvMain.hd), (float)(PBoardScr.posNamePlaying[3].y + 13 * AvMain.hd + (int)AvMain.hSmall), 3);
					break;
				}
			}
		}
		base.paintChat(g);
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x0007C81C File Offset: 0x0007AC1C
	public void paintMoneys(MyGraphics g)
	{
		for (int i = 0; i < 4; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar.IDDB != -1 && (Canvas.w >= 160 || avatar.IDDB == this.currentPlayer))
			{
				int indexByID = BoardScr.getIndexByID(this.currentPlayer);
				if (i != indexByID || (Canvas.gameTick % 20 > 5 && i == indexByID) || this.finish)
				{
					Canvas.smallFontYellow.drawString(g, avatar.getMoneyNew() + " " + T.getMoney(), PBoardScr.posNamePlaying[BoardScr.indexPlayer[i]].x, PBoardScr.posNamePlaying[BoardScr.indexPlayer[i]].y, PBoardScr.posNamePlaying[BoardScr.indexPlayer[i]].anchor);
				}
			}
		}
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x0007C90C File Offset: 0x0007AD0C
	private void paintTime(MyGraphics g)
	{
		if (!BoardScr.isStartGame || BoardScr.isGameEnd)
		{
			return;
		}
		long num = (long)BoardScr.interval - BoardScr.dieTime;
		if (num > 0L && PBoardScr.posNamePlaying != null && PBoardScr.posNamePlaying[0] != null)
		{
			int indexByID = BoardScr.getIndexByID(this.currentPlayer);
			Canvas.numberFont.drawString(g, num + string.Empty, Canvas.hw, PBoardScr.posCardShow[0].y + (PBoardScr.posCardShow[2].y - PBoardScr.posCardShow[0].y) / 2 - Canvas.numberFont.getHeight() / 2, 2);
		}
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x0007C9BB File Offset: 0x0007ADBB
	private void paintEatCard(MyGraphics g)
	{
		if (!this.isEatCard)
		{
			return;
		}
		if (Canvas.w > 176)
		{
			this.cardE.paintFull(g);
		}
		else
		{
			this.cardE.paintSmall(g, false);
		}
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x0007C9F8 File Offset: 0x0007ADF8
	private void paintMyCard(MyGraphics g)
	{
		this.card = new Card(-1, true);
		int num = PBoardScr.disShow;
		if (num <= 12 && Canvas.w > 200)
		{
			num = 20;
		}
		for (int i = 0; i < 4; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			if (avatar != null && avatar.IDDB != -1)
			{
				int num2 = 0;
				for (int j = 0; j < 3; j++)
				{
					if (this.showCardEat[i][j] == null)
					{
						break;
					}
					num2++;
				}
				if (BoardScr.indexPlayer[i] == 1)
				{
					for (int k = 0; k < 3; k++)
					{
						if (this.showCardEat[i][k] == null)
						{
							break;
						}
						Card card = this.showCardEat[i][k];
						card.phom = 1;
						card.paintFull(g);
						this.PaintLineColor(1, card.x, card.y, BoardScr.indexPlayer[i], g);
					}
				}
			}
		}
		for (int l = 0; l < 4; l++)
		{
			if (BoardScr.indexPlayer[l] == 1)
			{
				int num3 = 0;
				int num4 = 0;
				int num5 = this.distantCard / 2;
				for (int m = 0; m < 12; m++)
				{
					if (this.ShowHaPhom[l][0] == -1)
					{
						break;
					}
					if (this.ShowHaPhom[l][m] == -1)
					{
						if (num4 == 1)
						{
							num3--;
							break;
						}
						num4 = 1;
					}
					else
					{
						num4 = 0;
					}
					num3++;
				}
				if (num3 > 0)
				{
					num5 = Canvas.hh / num3;
					if (num5 > this.distantCard)
					{
						num5 = this.distantCard;
					}
					else if (num5 < this.distantCard / 2)
					{
						num5 = this.distantCard / 2;
					}
				}
				for (int n = 0; n < 12; n++)
				{
					if (this.ShowHaPhom[l][0] == -1)
					{
						break;
					}
					if (this.ShowHaPhom[l][n] != -1)
					{
						this.card = new Card((sbyte)this.ShowHaPhom[l][n], true);
						this.card.x = PBoardScr.posCardEat[BoardScr.indexPlayer[l]].x;
						this.card.y = PBoardScr.posCardEat[BoardScr.indexPlayer[l]].y - (num3 - 1) * num5 / 2 + n * num5;
						this.card.paintFull(g);
					}
				}
			}
		}
		this.paintcardShow(g);
		for (int num6 = 0; num6 < 4; num6++)
		{
			Avatar avatar2 = (Avatar)BoardScr.avatarInfos.elementAt(num6);
			if (avatar2 != null && avatar2.IDDB != -1)
			{
				int num7 = 0;
				for (int num8 = 0; num8 < 11; num8++)
				{
					if (this.cardRac[num6][num8] == -1)
					{
						break;
					}
					num7++;
				}
				if (BoardScr.indexPlayer[num6] == 1 || BoardScr.indexPlayer[num6] == 3)
				{
					for (int num9 = 0; num9 < 11; num9++)
					{
						if (this.cardRac[num6][num9] == -1)
						{
							break;
						}
						this.card = new Card((sbyte)this.cardRac[num6][num9], true);
						this.card.x = PBoardScr.posCardShow[BoardScr.indexPlayer[num6]].x;
						this.card.y = PBoardScr.posCardShow[BoardScr.indexPlayer[num6]].y - (num7 - 1) * num / 2 + num9 * num;
						this.card.paintFull(g);
					}
				}
				if (BoardScr.indexPlayer[num6] == 0)
				{
					for (int num10 = 0; num10 < 11; num10++)
					{
						if (this.cardRac[num6][num10] == -1)
						{
							break;
						}
						this.card = new Card((sbyte)this.cardRac[num6][num10], true);
						this.card.x = PBoardScr.posCardShow[BoardScr.indexPlayer[num6]].x + (num7 - 1) * PBoardScr.disShow / 2 - num10 * PBoardScr.disShow;
						this.card.y = PBoardScr.posCardShow[BoardScr.indexPlayer[num6]].y;
						this.card.paintFull(g);
					}
				}
			}
		}
		for (int num11 = 0; num11 < 4; num11++)
		{
			Avatar avatar3 = (Avatar)BoardScr.avatarInfos.elementAt(num11);
			if (avatar3 != null && avatar3.IDDB != -1)
			{
				int num12 = 0;
				for (int num13 = 0; num13 < 3; num13++)
				{
					if (this.showCardEat[num11][num13] == null)
					{
						break;
					}
					num12++;
				}
				if (BoardScr.indexPlayer[num11] == 0)
				{
					for (int num14 = 0; num14 < 3; num14++)
					{
						if (this.showCardEat[num11][num14] == null)
						{
							break;
						}
						Card card2 = this.showCardEat[num11][num14];
						card2.phom = 1;
						card2.paintFull(g);
						this.PaintLineColor(1, card2.x, card2.y, 0, g);
					}
				}
			}
		}
		for (int num15 = 0; num15 < 4; num15++)
		{
			int num16 = 0;
			int num17 = 0;
			int num18 = this.distantCard / 2;
			for (int num19 = 0; num19 < 12; num19++)
			{
				if (this.ShowHaPhom[num15][0] == -1)
				{
					break;
				}
				if (this.ShowHaPhom[num15][num19] == -1)
				{
					if (num17 == 1)
					{
						num16--;
						break;
					}
					num17 = 1;
				}
				else
				{
					num17 = 0;
				}
				num16++;
			}
			if (num16 > 0)
			{
				num18 = Canvas.hw / num16;
				if (num18 > this.distantCard)
				{
					num18 = this.distantCard;
				}
				else if (num18 < this.distantCard / 2)
				{
					num18 = this.distantCard / 2;
				}
			}
			if (BoardScr.indexPlayer[num15] == 0)
			{
				if (this.ShowHaPhom[num15][0] == -1)
				{
					break;
				}
				for (int num20 = 0; num20 < 12; num20++)
				{
					if (this.ShowHaPhom[num15][num20] != -1)
					{
						this.card = new Card((sbyte)this.ShowHaPhom[num15][num20], true);
						this.card.x = PBoardScr.posCardEat[BoardScr.indexPlayer[num15]].x + (num16 - 1) * num18 / 2 - num20 * num18;
						this.card.y = PBoardScr.posCardEat[BoardScr.indexPlayer[num15]].y;
						this.card.paintFull(g);
					}
				}
			}
		}
		for (int num21 = 0; num21 < 4; num21++)
		{
			Avatar avatar4 = (Avatar)BoardScr.avatarInfos.elementAt(num21);
			if (avatar4 != null && avatar4.IDDB != -1)
			{
				if (BoardScr.indexPlayer[num21] == 3)
				{
					int num22 = 0;
					for (int num23 = 0; num23 < 3; num23++)
					{
						if (this.showCardEat[num21][num23] == null)
						{
							break;
						}
						num22++;
					}
					for (int num24 = 0; num24 < 3; num24++)
					{
						if (this.showCardEat[num21][num24] == null)
						{
							break;
						}
						Card card3 = this.showCardEat[num21][num24];
						card3.phom = 1;
						card3.paintFull(g);
						this.PaintLineColor(1, card3.x, card3.y, BoardScr.indexPlayer[num21], g);
					}
				}
			}
		}
		for (int num25 = 0; num25 < 4; num25++)
		{
			Avatar avatar5 = (Avatar)BoardScr.avatarInfos.elementAt(num25);
			if (avatar5 != null && avatar5.IDDB != -1)
			{
				if (BoardScr.indexPlayer[num25] == 3)
				{
					int num26 = 0;
					int num27 = 0;
					int num28 = this.distantCard / 2;
					for (int num29 = 0; num29 < 12; num29++)
					{
						if (this.ShowHaPhom[num25][0] == -1)
						{
							break;
						}
						if (this.ShowHaPhom[num25][num29] == -1)
						{
							if (num27 == 1)
							{
								num26--;
								break;
							}
							num27 = 1;
						}
						else
						{
							num27 = 0;
						}
						num26++;
					}
					if (num26 > 0)
					{
						num28 = Canvas.hh / num26;
						if (num28 > this.distantCard)
						{
							num28 = this.distantCard;
						}
						else if (num28 < this.distantCard / 2)
						{
							num28 = this.distantCard / 2;
						}
					}
					for (int num30 = 0; num30 < 12; num30++)
					{
						if (this.ShowHaPhom[num25][0] == -1)
						{
							break;
						}
						if (this.ShowHaPhom[num25][num30] != -1)
						{
							this.card = new Card((sbyte)this.ShowHaPhom[num25][num30], true);
							this.card.y = PBoardScr.posCardEat[BoardScr.indexPlayer[num25]].y - (num26 - 1) * num28 / 2 + num30 * num28;
							this.card.x = PBoardScr.posCardEat[BoardScr.indexPlayer[num25]].x;
							this.card.paintFull(g);
						}
					}
				}
			}
		}
		for (int num31 = 0; num31 < 4; num31++)
		{
			if (BoardScr.indexPlayer[num31] == 2)
			{
				int num32 = 0;
				int num33 = 0;
				int num34 = this.distantCard / 2;
				for (int num35 = 0; num35 < 12; num35++)
				{
					if (this.ShowHaPhom[num31][0] == -1)
					{
						break;
					}
					if (this.ShowHaPhom[num31][num35] == -1)
					{
						if (num33 == 1)
						{
							num32--;
							break;
						}
						num33 = 1;
					}
					else
					{
						num33 = 0;
					}
					num32++;
				}
				if (num32 > 0)
				{
					num34 = Canvas.hw / num32;
					if (num34 > this.distantCard)
					{
						num34 = this.distantCard;
					}
					else if (num34 < this.distantCard / 2)
					{
						num34 = this.distantCard / 2;
					}
				}
				for (int num36 = 0; num36 < 12; num36++)
				{
					if (this.ShowHaPhom[num31][0] == -1)
					{
						break;
					}
					if (this.ShowHaPhom[num31][num36] != -1)
					{
						this.card = new Card((sbyte)this.ShowHaPhom[num31][num36], true);
						if (BoardScr.indexPlayer[num31] == 2)
						{
							this.card.x = PBoardScr.posCardEat[BoardScr.indexPlayer[num31]].x - (num32 - 1) * num34 / 2 + num36 * num34;
							this.card.y = PBoardScr.posCardEat[BoardScr.indexPlayer[num31]].y;
						}
						this.card.paintFull(g);
					}
				}
			}
		}
		if (this.myCard != null)
		{
			this.paintCard(g);
		}
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x0007D548 File Offset: 0x0007B948
	private void paintcardShow(MyGraphics g)
	{
		if (!this.finish)
		{
			for (int i = 0; i < 4; i++)
			{
				Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
				if (avatar != null && avatar.IDDB != -1)
				{
					int num = 0;
					if (this.cardShow[i] != null)
					{
						for (int j = 0; j < 4; j++)
						{
							if (this.cardShow[i][j] != null)
							{
								num++;
							}
						}
					}
					if (BoardScr.indexPlayer[i] == 0 || BoardScr.indexPlayer[i] == 2)
					{
						for (int k = 0; k < 4; k++)
						{
							if (this.cardShow[i] == null || this.cardShow[i][k] == null)
							{
								break;
							}
							if (BoardScr.indexPlayer[i] == 2)
							{
								this.cardShow[i][k].paintFull(g);
							}
							else
							{
								this.cardShow[i][k].paintFull(g);
							}
						}
					}
					else
					{
						for (int l = 0; l < 4; l++)
						{
							if (this.cardShow[i][l] == null)
							{
								break;
							}
							this.cardShow[i][l].paintFull(g);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0007D694 File Offset: 0x0007BA94
	private void paintCard(MyGraphics g)
	{
		for (int i = 0; i < 10; i++)
		{
			int num = 0;
			if (this.myCard[i] != null && (int)this.myCard[i].cardID != -1)
			{
				this.ca = new Card(-1, true);
				this.ca.x = this.myCard[i].x;
				this.ca.y = this.myCard[i].y;
				if (!this.myCard[i].isShow)
				{
					this.ca = this.myCard[i];
				}
				if (num == 0 && i < 9 && (int)this.myCard[i + 1].cardID != -1 && i != this.selectedCard)
				{
					if (PBoardScr.disCard > 14 || this.myCard[i + 1].x != this.myCard[i + 1].xTo)
					{
						this.ca.paintHalfBackFull(g);
					}
					else
					{
						this.ca.paintHalf(g);
					}
					if ((int)this.myCard[i].phom != 0)
					{
						this.PaintLineColor((int)this.myCard[i].phom, this.myCard[i].x, this.myCard[i].y, 2, g);
					}
				}
				else
				{
					this.ca.paintFull(g);
					if ((int)this.myCard[i].phom != 0)
					{
						this.PaintLineColor((int)this.myCard[i].phom, this.myCard[i].x, this.myCard[i].y, 2, g);
					}
				}
			}
			if (i == this.selectedCard)
			{
				this.paintCardChange(g);
			}
		}
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x0007D85C File Offset: 0x0007BC5C
	private void PaintLineColor(int phom, int x, int y, int i, MyGraphics g)
	{
		int color = 0;
		switch (phom)
		{
		case 1:
		case 4:
			color = this.colorPhom_1;
			break;
		case 2:
		case 5:
			color = this.colorPhom_2;
			break;
		case 3:
		case 6:
			color = this.colorPhom_3;
			break;
		}
		g.setColor(color);
		g.fillRect((float)(x - BoardScr.wCard / 2 + 2), (float)(y - BoardScr.hcard / 2 + 22), 7f, 2f);
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x0007D8E8 File Offset: 0x0007BCE8
	private void paintCardChange(MyGraphics g)
	{
		if ((int)this.hCard.cardID == -1)
		{
			return;
		}
		this.hCard.x = this.xShow + this.selectedCard * PBoardScr.disCard;
		this.hCard.y = PBoardScr.posCard.y + ((!this.trans) ? 10 : -5);
		if (Canvas.w > 176)
		{
			if (this.selectedCard < 9)
			{
				this.hCard.paintFull(g);
			}
			else
			{
				this.hCard.paintFull(g);
			}
		}
		else
		{
			this.hCard.paintSmall(g, false);
		}
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x0007D99C File Offset: 0x0007BD9C
	private void detectPhom()
	{
		int num = -1;
		for (int i = 0; i < 8; i++)
		{
			if (this.searchCardEat((int)this.myCard[i].phom))
			{
				return;
			}
			int[] array = new int[6];
			for (int j = 0; j < 6; j++)
			{
				array[j] = -1;
			}
			for (int k = 0; k < 3; k++)
			{
				if ((int)this.myCard[i + k].phom != 0)
				{
					num = k + 1;
					return;
				}
				array[k] = (int)this.myCard[i + k].cardID;
			}
			if (num != -1)
			{
				i = num;
				num = -1;
			}
			else if (this.checkPhomSanh(array) || this.checkPhomBaLa(array))
			{
				this.phomRandom++;
				for (int l = 0; l < 3; l++)
				{
					this.myCard[i + l].phom = (sbyte)this.phomRandom;
				}
				i += 2;
			}
		}
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x0007DAA4 File Offset: 0x0007BEA4
	private void addCard(int index)
	{
		if ((int)this.myCard[index].phom != 0 || (int)this.myCard[index].cardID == -1)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		if (index > 0 && (int)this.myCard[index - 1].phom != 0 && (int)this.myCard[index - 1].cardID != -1)
		{
			int[] array = new int[10];
			for (int i = 0; i < 10; i++)
			{
				if ((int)this.myCard[i].phom == (int)this.myCard[index - 1].phom)
				{
					array[i] = (int)this.myCard[i].cardID;
					num += array[i] / 4 + 1;
				}
				else
				{
					array[i] = -1;
				}
			}
			array = this.orderArrayIncrease(array);
			array[9] = (int)this.myCard[index].cardID;
			array = this.orderArrayIncrease(array);
			if (!this.checkPhomSanh(array) && !this.checkPhomBaLa(array))
			{
				num = 0;
			}
		}
		if (index < 9 && (index == 0 || (index != 0 && (int)this.myCard[index + 1].phom != (int)this.myCard[index - 1].phom)) && (int)this.myCard[index + 1].phom != 0 && (int)this.myCard[index + 1].cardID != -1)
		{
			int[] array2 = new int[10];
			for (int j = 0; j < 10; j++)
			{
				if ((int)this.myCard[j].phom == (int)this.myCard[index + 1].phom)
				{
					array2[j] = (int)this.myCard[j].cardID;
					num2 += array2[j] / 4 + 1;
				}
				else
				{
					array2[j] = -1;
				}
			}
			array2 = this.orderArrayIncrease(array2);
			array2[9] = (int)this.myCard[index].cardID;
			array2 = this.orderArrayIncrease(array2);
			if (!this.checkPhomSanh(array2) && !this.checkPhomBaLa(array2))
			{
				num2 = 0;
			}
		}
		if (num < num2)
		{
			this.myCard[index].phom = this.myCard[index + 1].phom;
		}
		else if (num > 0)
		{
			this.myCard[index].phom = this.myCard[index - 1].phom;
		}
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x0007DD10 File Offset: 0x0007C110
	private void findPhom()
	{
		this.phomRandom = 3;
		this.detectPhom();
		for (int i = 0; i < 10; i++)
		{
			if (!this.searchCardEat((int)this.myCard[i].phom) && (int)this.myCard[i].phom == 0 && (int)this.myCard[i].cardID != -1)
			{
				this.addCard(i);
			}
		}
		this.checkU();
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x0007DD8C File Offset: 0x0007C18C
	private void cleanPhomRandom()
	{
		int num = 0;
		for (int i = 0; i < 10; i++)
		{
			if (this.searchCardEat((int)this.myCard[i].phom))
			{
				return;
			}
			if ((int)this.myCard[i].phom != 0 && num != (int)this.myCard[i].phom)
			{
				num = (int)this.myCard[i].phom;
				this.phomRandom--;
			}
			this.myCard[i].phom = 0;
		}
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x0007DE1C File Offset: 0x0007C21C
	private bool searchCardEat(int phom)
	{
		for (int i = 0; i < 3; i++)
		{
			if ((int)this.cardEat[i].phom == 0)
			{
				return false;
			}
			if ((int)this.cardEat[i].phom == phom)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x0007DE68 File Offset: 0x0007C268
	public void start(sbyte interval2, MyVector myCard, int firstMove, int firHa)
	{
		base.start(firstMove, (int)interval2);
		this.reset();
		this.resetCurrentTime();
		this.currentPlayer = firstMove;
		this.firstPlayer = this.currentPlayer;
		BoardScr.interval = (int)interval2;
		this.firstHa = firHa;
		BoardScr.isStartGame = true;
		int num = myCard.size();
		for (int i = 0; i < 10; i++)
		{
			this.myCard[i] = new Card(-1, true);
			this.myCard[i].x = Canvas.hw;
			this.myCard[i].y = Canvas.hh;
			this.myCard[i].isShow = true;
		}
		for (int j = 0; j < num; j++)
		{
			Card card = (Card)myCard.elementAt(j);
			this.myCard[j] = new Card(card.cardID, true);
			this.myCard[j].x = Canvas.hw;
			this.myCard[j].y = Canvas.hh;
			this.myCard[j].isShow = true;
		}
		this.orderCardIncrease(this.myCard);
		this.findPhom();
		if (this.currentPlayer != GameMidlet.avatar.IDDB)
		{
			this.resetCmd();
		}
		this.setPosPlaying();
		this.setPosCard(false);
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x0007DFB4 File Offset: 0x0007C3B4
	public override void doFire()
	{
		this.resetOrderCard();
		base.doFire();
		this.resetChangeCard();
		int num = 0;
		int num2 = -1;
		for (int i = 0; i < 10; i++)
		{
			if ((int)this.myCard[i].cardID != -1 && this.myCard[i].isUp)
			{
				num++;
				num2 = i;
			}
		}
		if (num > 1)
		{
			Canvas.startOKDlg(T.canYouOnceOnly);
			return;
		}
		if (num == 0)
		{
			Canvas.startOKDlg(T.yetSellectCard);
			return;
		}
		if (!this.checkCardToFire(num2))
		{
			return;
		}
		this.resetCmd();
		base.setCmdWaiting();
		CasinoService.gI().firePhom(this.myCard[num2].cardID);
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x0007E069 File Offset: 0x0007C469
	private bool checkCardToFire(int sellectAsset)
	{
		if ((int)this.myCard[sellectAsset].phom == 0)
		{
			return true;
		}
		if (!this.checkCardToEat(sellectAsset))
		{
			Canvas.startOKDlg(T.ifFireBreakPhom);
			return false;
		}
		return true;
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x0007E09C File Offset: 0x0007C49C
	public void setPosCardFire(int sNum)
	{
		int num = 0;
		if (this.cardShow[sNum] != null)
		{
			for (int i = 0; i < 4; i++)
			{
				if (this.cardShow[sNum][i] != null)
				{
					num++;
				}
			}
		}
		for (int j = 0; j < 4; j++)
		{
			if (this.cardShow[sNum][j] != null)
			{
				int num2 = BoardScr.indexPlayer[sNum];
				if (num2 != 0)
				{
					if (num2 != 2)
					{
						this.cardShow[sNum][j].xTo = PBoardScr.posCardShow[BoardScr.indexPlayer[sNum]].x;
						this.cardShow[sNum][j].yTo = PBoardScr.posCardShow[BoardScr.indexPlayer[sNum]].y - (num - 1) * this.distantCard / 2 + j * this.distantCard;
					}
					else
					{
						this.cardShow[sNum][j].xTo = PBoardScr.posCardShow[BoardScr.indexPlayer[sNum]].x - (num - 1) * this.distantCard / 2 + j * this.distantCard;
						this.cardShow[sNum][j].yTo = PBoardScr.posCardShow[BoardScr.indexPlayer[sNum]].y;
					}
				}
				else
				{
					this.cardShow[sNum][j].xTo = PBoardScr.posCardShow[BoardScr.indexPlayer[sNum]].x + (num - 1) * this.distantCard / 2 - j * this.distantCard;
					this.cardShow[sNum][j].yTo = PBoardScr.posCardShow[BoardScr.indexPlayer[sNum]].y;
				}
			}
		}
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x0007E230 File Offset: 0x0007C630
	public void onFire(int currentP, int firstP, int cardFire, sbyte numberCard)
	{
		if (cardFire == -1)
		{
			this.setCmdFire();
			Canvas.startOKDlg(T.cardToMiss);
			return;
		}
		int num = PBoardScr.disShow;
		if (num < 35 * AvMain.hd)
		{
			num = 35 * AvMain.hd;
		}
		if (currentP == GameMidlet.avatar.IDDB)
		{
			this.resetOrderCard();
		}
		this.resetCmd();
		this.resetCurrentTime();
		int indexByID = BoardScr.getIndexByID(firstP);
		if (indexByID == -1)
		{
			return;
		}
		if (currentP == GameMidlet.avatar.IDDB)
		{
			if (this.getAssetCard(this.cardShow[(int)BoardScr.indexOfMe]) != -1 && this.getAssetCard(this.cardShow[(int)BoardScr.indexOfMe]) <= 3)
			{
				this.setCmdEatAndGet();
			}
			this.cleanPhomRandom();
			this.findPhom();
		}
		this.cardCurrent = cardFire;
		this.currentPlayer = currentP;
		this.firstPlayer = firstP;
		this.numberCard[indexByID] = numberCard;
		this.numC[indexByID]++;
		this.cardShow[indexByID][(int)this.numberCard[indexByID]] = new Card((sbyte)this.cardCurrent, true);
		int num2 = 0;
		if (this.cardShow[indexByID] != null)
		{
			for (int i = 0; i < 4; i++)
			{
				if (this.cardShow[indexByID][i] != null)
				{
					num2++;
				}
			}
		}
		for (int j = 0; j < 4; j++)
		{
			if (this.cardShow[indexByID][j] != null)
			{
				int num3 = BoardScr.indexPlayer[indexByID];
				if (num3 != 0)
				{
					if (num3 != 2)
					{
						if (BoardScr.indexPlayer[indexByID] == 1)
						{
							this.cardShow[indexByID][(int)this.numberCard[indexByID]].x = -BoardScr.wCard / 2;
						}
						else
						{
							this.cardShow[indexByID][(int)this.numberCard[indexByID]].x = Canvas.w + BoardScr.wCard / 2;
						}
						this.cardShow[indexByID][(int)this.numberCard[indexByID]].y = PBoardScr.posCardShow[BoardScr.indexPlayer[indexByID]].y;
					}
				}
				else
				{
					this.cardShow[indexByID][(int)this.numberCard[indexByID]].x = PBoardScr.posCardShow[BoardScr.indexPlayer[indexByID]].x;
					this.cardShow[indexByID][(int)this.numberCard[indexByID]].y = -BoardScr.hcard / 2;
				}
			}
		}
		this.setPosCardFire(indexByID);
		if (this.firstPlayer == GameMidlet.avatar.IDDB)
		{
			for (int k = 0; k < 10; k++)
			{
				if ((int)this.myCard[k].cardID == this.cardCurrent)
				{
					this.cardShow[indexByID][(int)this.numberCard[indexByID]].x = this.myCard[k].x;
					this.cardShow[indexByID][(int)this.numberCard[indexByID]].y = this.myCard[k].y;
					this.cleanCard(k);
					break;
				}
			}
			if ((int)this.myCard[this.selectedCard].cardID == -1)
			{
				this.selectedCard = this.getAssetCard() - 1;
			}
		}
		this.resetUpCard();
		this.setPosCard(false);
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x0007E56C File Offset: 0x0007C96C
	public void onSkipPlayer(int curPlayer, int firHa)
	{
		int indexByID = BoardScr.getIndexByID(curPlayer);
		if (indexByID == -1)
		{
			return;
		}
		this.firstHa = firHa;
		if (curPlayer == GameMidlet.avatar.IDDB && this.currentPlayer != curPlayer)
		{
			this.setCmdEatAndGet();
			this.setPosCard(false);
		}
		this.currentPlayer = curPlayer;
		this.resetCurrentTime();
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x0007E5C8 File Offset: 0x0007C9C8
	protected void doHaPhom()
	{
		this.resetOrderCard();
		if (GameMidlet.avatar.IDDB != this.currentPlayer)
		{
			Canvas.startOKDlg(T.waitToCurrent);
			return;
		}
		this.resetChangeCard();
		int[] array = new int[12];
		int num = -1;
		for (int i = 0; i < 10; i++)
		{
			if ((int)this.myCard[i].phom != 0 && (num == -1 || num == (int)this.myCard[i].phom))
			{
				num = (int)this.myCard[i].phom;
				array[i] = (int)this.myCard[i].cardID;
			}
			else
			{
				array[i] = -1;
			}
		}
		this.resetCmd();
		base.setCmdWaiting();
		CasinoService.gI().HaPhomPhom(this.myCard);
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x0007E694 File Offset: 0x0007CA94
	private void resetUpCard()
	{
		if (!BoardScr.disableReady)
		{
			for (int i = 0; i < 10; i++)
			{
				if ((int)this.myCard[i].cardID != -1 && this.myCard[i].isUp)
				{
					this.resetUpMyCard(i);
				}
			}
		}
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x0007E6EC File Offset: 0x0007CAEC
	public void onHaPhom(bool isHa, int[] mCard, bool isU2, int[] orderMoney, int curID)
	{
		int indexByID = BoardScr.getIndexByID(curID);
		if (!isHa)
		{
			Canvas.startOKDlg(T.notSamePhom);
			return;
		}
		if (curID == GameMidlet.avatar.IDDB)
		{
			this.resetOrderCard();
		}
		this.resetUpCard();
		this.isU = isU2;
		int num = 1;
		for (int i = 0; i < this.ShowHaPhom[indexByID].Length; i++)
		{
			if (this.ShowHaPhom[indexByID][i] == -1)
			{
				if (num == 1)
				{
					num = i;
					break;
				}
				num = 1;
			}
			else
			{
				num = 0;
			}
		}
		for (int j = num; j < mCard.Length; j++)
		{
			if (mCard[j - num] != -1)
			{
				this.ShowHaPhom[indexByID][j] = mCard[j - num];
			}
		}
		if (GameMidlet.avatar.IDDB == curID)
		{
			this.isHaPhom = true;
			for (int k = 0; k < 10; k++)
			{
				for (int l = 0; l < mCard.Length; l++)
				{
					if ((int)this.myCard[k].cardID == mCard[l])
					{
						this.resetCell(k);
						break;
					}
				}
			}
			this.setCmdFire();
			this.myCard = this.orderCardIncrease(this.myCard);
			if ((int)this.myCard[this.selectedCard].cardID == -1)
			{
				this.selectedCard = this.getAssetCard() - 1;
			}
			this.setPosCard(false);
		}
		if (this.isU)
		{
			this.finish = true;
			this.setContinue();
		}
		this.numCardPhom[indexByID] = 0;
		for (int m = 0; m < this.ShowHaPhom[indexByID].Length; m++)
		{
			if (this.ShowHaPhom[indexByID][m] != -1)
			{
				this.numCardPhom[indexByID]++;
			}
		}
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x0007E8BC File Offset: 0x0007CCBC
	public void onFinish(int[] score, int[][] cardRac, int[] moneyPlayers)
	{
		this.resetOrderCard();
		int num = 1000;
		for (int i = 0; i < 4; i++)
		{
			if (score[i] >= 0 && score[i] < num)
			{
				num = score[i];
				this.winer = i;
			}
			this.scorePlayer[i] = score[i];
		}
		this.cardRac = cardRac;
		for (int j = 0; j < 4; j++)
		{
			this.numCardRac[j] = (sbyte)this.getAssetArray(cardRac[j]);
		}
		this.finish = true;
		this.setContinue();
		for (int k = 0; k < 4; k++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(k);
			if (avatar != null)
			{
				if (avatar.IDDB != -1 && this.scorePlayer[k] != -1)
				{
					if (this.scorePlayer[k] == -2)
					{
						BoardScr.showChat(avatar.IDDB, "Cháy");
					}
					else if (k == this.winer)
					{
						BoardScr.showChat(avatar.IDDB, "Thắng");
					}
					else
					{
						BoardScr.showChat(avatar.IDDB, "Thua");
					}
					avatar.isReady = false;
				}
			}
		}
		GameMidlet.avatar.isReady = false;
		this.setPosCard(false);
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x0007EA04 File Offset: 0x0007CE04
	public void onOnceWin(int orderMoney3)
	{
		this.finish = true;
		this.setContinue();
		if (!BoardScr.disableReady)
		{
			this.winer = (int)BoardScr.indexOfMe;
			BoardScr.showChat(((Avatar)BoardScr.avatarInfos.elementAt(this.winer)).IDDB, "Thắng");
		}
		BoardScr.isStartGame = true;
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x0007EA60 File Offset: 0x0007CE60
	public void onDenBai(int winID, int[] orderMoney)
	{
		this.finish = true;
		this.setContinue();
		this.winer = winID;
		this.isU = true;
		this.denBai = BoardScr.getIndexByID(this.firstPlayer);
		BoardScr.showChat(this.winer, "Ù");
		BoardScr.showChat(this.firstPlayer, "Đền ù");
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x0007EAB9 File Offset: 0x0007CEB9
	private void checkU()
	{
		if (this.numPhom + (this.phomRandom - 3) + this.phomHa == 3)
		{
			this.center = this.cmdHaPhom;
		}
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x0007EAE4 File Offset: 0x0007CEE4
	private bool checkCardToEat(int id)
	{
		int num = -1;
		for (int i = 0; i < 3; i++)
		{
			if ((int)this.cardEat[i].phom != 0 && (int)this.cardEat[i].phom == (int)this.myCard[id].phom)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			if ((int)this.myCard[id].phom != 0)
			{
				int[] array = new int[10];
				for (int j = 0; j < 10; j++)
				{
					if ((int)this.myCard[j].phom == (int)this.myCard[id].phom && !this.myCard[j].isUp && j != id)
					{
						array[j] = (int)this.myCard[j].cardID;
					}
					else
					{
						array[j] = -1;
					}
				}
				array = this.orderArrayIncrease(array);
				if (!this.checkPhomSanh(array) && !this.checkPhomBaLa(array))
				{
					for (int k = 0; k < 10; k++)
					{
						if (k != id && (int)this.myCard[k].phom == (int)this.myCard[id].phom)
						{
							this.myCard[k].phom = 0;
						}
					}
					this.myCard[id].phom = 0;
				}
			}
			return true;
		}
		int[] array2 = new int[10];
		for (int l = 0; l < 10; l++)
		{
			if ((int)this.myCard[l].phom == (int)this.cardEat[num].phom && !this.myCard[l].isUp && l != id)
			{
				array2[l] = (int)this.myCard[l].cardID;
			}
			else
			{
				array2[l] = -1;
			}
		}
		array2 = this.orderArrayIncrease(array2);
		int num2 = -1;
		int num3 = 0;
		for (int m = 0; m < 10; m++)
		{
			if (array2[m] == (int)this.cardEat[num].cardID)
			{
				num2 = m;
			}
		}
		for (int n = 0; n < 9; n++)
		{
			if (array2[n + 1] == -1)
			{
				break;
			}
			if (array2[n + 1] / 4 != array2[n] / 4 && (array2[n + 1] / 4 - array2[n] / 4 != 1 || array2[n] % 4 != array2[n + 1] % 4))
			{
				break;
			}
			num3 = n + 1;
		}
		if (num2 > num3 && num3 > 1)
		{
			return false;
		}
		if (num3 > 1)
		{
			for (int num4 = num3 + 1; num4 < 10; num4++)
			{
				for (int num5 = 0; num5 < 10; num5++)
				{
					if (array2[num4] == (int)this.myCard[num5].cardID)
					{
						this.myCard[num5].phom = 0;
					}
				}
			}
			return true;
		}
		int[] array3 = new int[3];
		for (int num6 = 0; num6 < 3; num6++)
		{
			array3[num6] = -1;
		}
		for (int num7 = 0; num7 <= num3; num7++)
		{
			array3[num7] = array2[num7];
			array2[num7] = -1;
		}
		array2 = this.orderArrayIncrease(array2);
		if (this.checkPhomSanh(array2) || this.checkPhomBaLa(array2))
		{
			for (int num8 = 0; num8 < 3; num8++)
			{
				for (int num9 = 0; num9 < 10; num9++)
				{
					if (array3[num8] == (int)this.myCard[num9].cardID)
					{
						this.myCard[num9].phom = 0;
					}
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x0007EEC0 File Offset: 0x0007D2C0
	private void doEat()
	{
		this.resetOrderCard();
		this.resetChangeCard();
		for (int i = 0; i < this.cardToEat.Length; i++)
		{
			this.cardToEat[i] = -1;
		}
		int num = 0;
		for (int j = 0; j < 10; j++)
		{
			if ((int)this.myCard[j].cardID != -1 && this.myCard[j].isUp)
			{
				if (num == 5)
				{
					Canvas.startOKDlg(T.youSelect);
					return;
				}
				this.cardToEat[num] = (int)this.myCard[j].cardID;
				num++;
			}
		}
		if (num < 2)
		{
			Canvas.startOKDlg(T.upTwoCard);
			return;
		}
		int[] array = new int[6];
		for (int k = 0; k < 5; k++)
		{
			if (this.cardToEat[k] != -1)
			{
				array[k] = this.cardToEat[k];
			}
			else
			{
				array[k] = -1;
			}
		}
		array[5] = this.cardCurrent;
		sbyte b = -1;
		if (this.checkPhomSanh(array))
		{
			b = 1;
		}
		if (this.checkPhomBaLa(array))
		{
			b = 0;
		}
		if ((int)b == -1)
		{
			Canvas.startOKDlg(T.notPhom);
			return;
		}
		for (int l = 0; l < array.Length; l++)
		{
			if (array[l] != -1)
			{
				int m = 0;
				while (m < 10)
				{
					if (array[l] == (int)this.myCard[m].cardID)
					{
						if (!this.checkCardToEat(m))
						{
							Canvas.startOKDlg(T.notPhom);
							return;
						}
						break;
					}
					else
					{
						m++;
					}
				}
			}
		}
		this.resetCmd();
		base.setCmdWaiting();
		CasinoService.gI().eatCardPhom(this.cardToEat, b);
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x0007F084 File Offset: 0x0007D484
	public void onEatCard(bool isEat, int numEat, int firHa, sbyte nCard)
	{
		int indexByID = BoardScr.getIndexByID(this.currentPlayer);
		if (indexByID == -1)
		{
			return;
		}
		if (this.currentPlayer == GameMidlet.avatar.IDDB)
		{
			this.resetOrderCard();
		}
		if (isEat)
		{
			int indexByID2 = BoardScr.getIndexByID(this.firstPlayer);
			int num = 0;
			int num2 = 0;
			if (indexByID2 != -1)
			{
				num = this.cardShow[indexByID2][(int)this.numberCard[indexByID2]].x;
				num2 = this.cardShow[indexByID2][(int)this.numberCard[indexByID2]].y;
				int indexByID3 = BoardScr.getIndexByID(this.firstHa);
				if (indexByID2 != indexByID3)
				{
					this.cardShow[indexByID2][(int)this.numberCard[indexByID2]] = this.cardShow[indexByID3][(int)this.numberCard[indexByID3]];
					this.cardShow[indexByID2][(int)this.numberCard[indexByID2]].xTo = num;
					this.cardShow[indexByID2][(int)this.numberCard[indexByID2]].yTo = num2;
					this.cardShow[indexByID3][(int)this.numberCard[indexByID3]] = null;
				}
				this.cardShow[indexByID3][(int)this.numberCard[indexByID3]] = null;
				this.firstHa = firHa;
				this.numberCard[indexByID3] = nCard;
				this.numC[indexByID] = (int)this.numberCard[indexByID];
				this.numC[indexByID3] = (int)this.numberCard[indexByID3];
			}
			this.numCardEat[indexByID]++;
			if (this.currentPlayer == GameMidlet.avatar.IDDB)
			{
				this.numPhom++;
				if (this.getAssetCard(this.cardShow[(int)BoardScr.indexOfMe]) == 3)
				{
					this.setcmdHaPhom();
				}
				else
				{
					this.setCmdFire();
				}
				for (int i = numEat - 1; i >= 0; i--)
				{
					for (int j = 0; j < 10; j++)
					{
						if ((int)this.myCard[j].cardID == this.cardToEat[i])
						{
							this.myCard[j].phom = (sbyte)this.numPhom;
							this.myCard[this.getAssetCard()] = this.myCard[j];
							this.cleanCard(j);
						}
					}
				}
				int assetCard = this.getAssetCard();
				this.myCard[assetCard] = new Card((sbyte)this.cardCurrent, true);
				this.myCard[assetCard].phom = (sbyte)this.numPhom;
				this.myCard[assetCard].x = num;
				this.myCard[assetCard].y = num2;
				for (int k = 0; k < 3; k++)
				{
					if ((int)this.cardEat[k].cardID == -1)
					{
						this.cardEat[k] = this.myCard[assetCard];
						break;
					}
				}
				this.cleanPhomRandom();
				this.findPhom();
			}
			int assetArray = this.getAssetArray(this.showCardEat[indexByID]);
			this.showCardEat[indexByID][assetArray] = new Card((sbyte)this.cardCurrent, true);
			this.showCardEat[indexByID][assetArray].x = num;
			this.showCardEat[indexByID][assetArray].y = num2;
			this.setPosCardEat(indexByID);
			this.resetUpCard();
		}
		else if (this.currentPlayer == GameMidlet.avatar.IDDB)
		{
			Canvas.startOKDlg(T.notPhom);
			this.setCmdEatAndGet();
		}
		if (GameMidlet.avatar.IDDB == this.currentPlayer || GameMidlet.avatar.IDDB == this.firstPlayer)
		{
			this.setPosCard(false);
		}
		for (int l = 0; l < 4; l++)
		{
			this.setPosCardFire(l);
		}
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x0007F424 File Offset: 0x0007D824
	private void setPosCardEat(int sNum)
	{
		int num = 0;
		for (int i = 0; i < 3; i++)
		{
			if (this.showCardEat[sNum][i] == null)
			{
				break;
			}
			num++;
		}
		for (int j = 0; j < 3; j++)
		{
			if (this.showCardEat[sNum][j] != null)
			{
				int num2 = BoardScr.indexPlayer[sNum];
				if (num2 != 0)
				{
					this.showCardEat[sNum][j].xTo = PBoardScr.posCardEat[BoardScr.indexPlayer[sNum]].x;
					this.showCardEat[sNum][j].yTo = PBoardScr.posCardEat[BoardScr.indexPlayer[sNum]].y - (num - 1) * this.distantCard / 2 + j * this.distantCard;
				}
				else
				{
					this.showCardEat[sNum][j].xTo = PBoardScr.posCardEat[BoardScr.indexPlayer[sNum]].x + (num - 1) * this.distantCard / 2 - j * this.distantCard;
					this.showCardEat[sNum][j].yTo = PBoardScr.posCardEat[BoardScr.indexPlayer[sNum]].y;
				}
			}
		}
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x0007F54C File Offset: 0x0007D94C
	private void resetCardEat()
	{
		this.isEatCard = true;
		this.cardE = new Card((sbyte)this.cardCurrent, true);
		int indexByID = BoardScr.getIndexByID(this.firstPlayer);
		this.cardE.x = BoardScr.posAvatar[BoardScr.indexPlayer[indexByID]].x;
		this.cardE.y = BoardScr.posAvatar[BoardScr.indexPlayer[indexByID]].y;
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x0007F5BC File Offset: 0x0007D9BC
	private void eatCard()
	{
		if (!this.isEatCard)
		{
			return;
		}
		int indexByID = BoardScr.getIndexByID(this.currentPlayer);
		int num = (BoardScr.posAvatar[BoardScr.indexPlayer[indexByID]].x - this.cardE.x) / 2;
		int num2 = (BoardScr.posAvatar[BoardScr.indexPlayer[indexByID]].y - this.cardE.y) / 2;
		this.cardE.x += num;
		this.cardE.y += num2;
		if (global::Math.abs(num) <= 1 && global::Math.abs(num2) <= 1)
		{
			this.isEatCard = false;
		}
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0007F668 File Offset: 0x0007DA68
	private void doGet()
	{
		this.resetOrderCard();
		this.resetChangeCard();
		this.resetCmd();
		base.setCmdWaiting();
		CasinoService.gI().GetCardPhom();
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x0007F68C File Offset: 0x0007DA8C
	public void onGetCard(int cardGet)
	{
		this.resetOrderCard();
		int num = this.setAssetCard();
		this.myCard[num] = new Card((sbyte)cardGet, true);
		this.setPosCard(false);
		this.myCard[num].x = Canvas.hw;
		this.myCard[num].y = PBoardScr.posCardShow[0].y + (PBoardScr.posCardShow[2].y - PBoardScr.posCardShow[0].y) / 2 - Canvas.numberFont.getHeight() / 2;
		if (this.getAssetCard(this.cardShow[(int)BoardScr.indexOfMe]) == 3)
		{
			if (GameMidlet.avatar.IDDB == this.currentPlayer)
			{
				this.setcmdHaPhom();
			}
		}
		else if (GameMidlet.avatar.IDDB == this.currentPlayer)
		{
			this.setCmdFire();
		}
		if (!this.isHaPhom)
		{
			this.cleanPhomRandom();
			this.findPhom();
		}
		else
		{
			this.addCardToHaPhom((int)this.myCard[num].cardID);
		}
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x0007F798 File Offset: 0x0007DB98
	private void addCardToHaPhom(int id)
	{
		int[] array = new int[6];
		for (int i = 0; i < 5; i++)
		{
			array[i] = -1;
		}
		int num = 0;
		for (int j = 0; j < 12; j++)
		{
			if (this.ShowHaPhom[(int)BoardScr.indexOfMe][j] != -1)
			{
				array[num] = this.ShowHaPhom[(int)BoardScr.indexOfMe][j];
				num++;
			}
			else
			{
				num = 0;
				array[5] = id;
				this.orderArrayIncrease(array);
				if (this.checkPhomSanh(array) || this.checkPhomBaLa(array))
				{
					this.doAddCardToPhom(array);
					break;
				}
				for (int k = 0; k < 6; k++)
				{
					array[k] = -1;
				}
			}
		}
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x0007F854 File Offset: 0x0007DC54
	private void doAddCardToPhom(int[] card)
	{
		this.resetOrderCard();
		CasinoService.gI().doAddCardPhom(card);
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x0007F868 File Offset: 0x0007DC68
	public void onAddCardToPhom(bool isAdd, sbyte card1)
	{
		int indexByID = BoardScr.getIndexByID(this.currentPlayer);
		if (indexByID == -1)
		{
			return;
		}
		if (this.currentPlayer == GameMidlet.avatar.IDDB)
		{
			this.resetOrderCard();
		}
		if (isAdd)
		{
			if (GameMidlet.avatar.IDDB == this.currentPlayer)
			{
				for (int i = 0; i < 10; i++)
				{
					if ((int)this.myCard[i].cardID == -1)
					{
						break;
					}
					if ((int)card1 == (int)this.myCard[i].cardID)
					{
						this.resetCell(i);
						break;
					}
				}
				this.setPosCard(false);
			}
			int num = 0;
			int[] array = new int[6];
			for (int j = 0; j < 6; j++)
			{
				array[j] = -1;
			}
			for (int k = 0; k < this.ShowHaPhom[indexByID].Length; k++)
			{
				if (this.ShowHaPhom[indexByID][k] == -1)
				{
					num = 0;
					array[5] = (int)card1;
					this.orderArrayIncrease(array);
					if (this.checkPhomSanh(array) || this.checkPhomBaLa(array))
					{
						for (int l = 11; l > k; l--)
						{
							if (l - 1 >= 0)
							{
								this.ShowHaPhom[indexByID][l] = this.ShowHaPhom[indexByID][l - 1];
							}
						}
						this.ShowHaPhom[indexByID][k] = (int)card1;
					}
					for (int m = 0; m < 6; m++)
					{
						array[m] = -1;
					}
				}
				else
				{
					array[num] = this.ShowHaPhom[indexByID][k];
					num++;
				}
			}
		}
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0007FA08 File Offset: 0x0007DE08
	private int setAssetCard()
	{
		for (int i = 0; i < 10; i++)
		{
			if ((int)this.myCard[i].cardID == -1)
			{
				return i;
			}
			if (this.searchCardEat((int)this.myCard[i].phom))
			{
				for (int j = 9; j > i; j--)
				{
					this.myCard[j] = this.myCard[j - 1];
				}
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0007FA80 File Offset: 0x0007DE80
	private int getAssetCard(Card[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == null)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x0007FAAC File Offset: 0x0007DEAC
	private int getAssetCard()
	{
		for (int i = 0; i < 10; i++)
		{
			if ((int)this.hCard.cardID == -1 || i != this.selectedCard)
			{
				if ((int)this.myCard[i].cardID == -1)
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x0007FB08 File Offset: 0x0007DF08
	private int getAssetArray(Card[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == null)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x0007FB34 File Offset: 0x0007DF34
	private int getAssetArray(int[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == -1)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x0007FB61 File Offset: 0x0007DF61
	public override void setPlayers(sbyte roomID, sbyte boardID, int ownerID, int money, MyVector playerInfos)
	{
		base.setPlayers(roomID, boardID, ownerID, money, playerInfos);
		GameMidlet.avatar.isReady = false;
		BoardScr.notReadyDelay = 0;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x0007FB84 File Offset: 0x0007DF84
	private int[] orderArrayIncrease(int[] array)
	{
		for (int i = 0; i < array.Length - 1; i++)
		{
			for (int j = i + 1; j < array.Length; j++)
			{
				if (array[j] != -1)
				{
					int num = array[i];
					if (num > array[j] || num == -1)
					{
						array[i] = array[j];
						array[j] = num;
					}
				}
			}
		}
		return array;
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x0007FBE8 File Offset: 0x0007DFE8
	private Card[] orderCardIncrease(Card[] array)
	{
		for (int i = 0; i < array.Length - 1; i++)
		{
			for (int j = i + 1; j < array.Length; j++)
			{
				if ((int)array[j].cardID != -1)
				{
					Card card = array[i];
					if ((int)card.cardID > (int)array[j].cardID || (int)card.cardID == -1)
					{
						array[i] = array[j];
						array[j] = card;
					}
				}
			}
		}
		return array;
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x0007FC64 File Offset: 0x0007E064
	private bool checkPhomSanh(int[] vCard)
	{
		int[] array = this.orderArrayIncrease(vCard);
		int num = 0;
		for (int i = 0; i < array.Length - 1; i++)
		{
			if (array[i + 1] == -1)
			{
				break;
			}
			if (array[i] - array[i + 1] == 0 || array[i + 1] / 4 - array[i] / 4 != 1 || array[i] % 4 - array[i + 1] % 4 != 0)
			{
				return false;
			}
			num++;
		}
		return num > 1;
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x0007FCEC File Offset: 0x0007E0EC
	private bool checkPhomBaLa(int[] vCard)
	{
		int[] array = this.orderArrayIncrease(vCard);
		int num = 0;
		for (int i = 0; i < array.Length - 1; i++)
		{
			if (array[i + 1] == -1)
			{
				break;
			}
			if (array[i] / 4 - array[i + 1] / 4 != 0 || array[i] - array[i + 1] == 0)
			{
				return false;
			}
			num++;
		}
		return num > 1;
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x0007FD60 File Offset: 0x0007E160
	public void onPlaying(int interval1, int curPlayer, int firPlayer, int[][] cardShow2, int[][] cardEat2, int firHa)
	{
		this.reset();
		BoardScr.disableReady = true;
		this.resetCurrentTime();
		for (int i = 0; i < cardShow2.Length; i++)
		{
			int num = 0;
			if (cardShow2[i] != null)
			{
				for (int j = 0; j < 4; j++)
				{
					num++;
				}
			}
			for (int k = 0; k < cardShow2[i].Length; k++)
			{
				if (cardShow2[i][k] != -1)
				{
					this.cardShow[i][k] = new Card((sbyte)cardShow2[i][k], true);
					int num2 = BoardScr.indexPlayer[i];
					if (num2 != 0)
					{
						if (num2 != 2)
						{
							this.cardShow[i][k].x = (this.cardShow[i][k].xTo = PBoardScr.posCardShow[BoardScr.indexPlayer[i]].x);
							this.cardShow[i][k].y = (this.cardShow[i][k].yTo = PBoardScr.posCardShow[BoardScr.indexPlayer[i]].y - (num - 1) * this.distantCard / 2 + k * this.distantCard);
						}
						else
						{
							this.cardShow[i][k].y = (this.cardShow[i][k].yTo = PBoardScr.posCardShow[BoardScr.indexPlayer[i]].y);
							this.cardShow[i][k].x = (this.cardShow[i][k].xTo = PBoardScr.posCardShow[BoardScr.indexPlayer[i]].x - (num - 1) * this.distantCard / 2 + k * this.distantCard);
						}
					}
					else
					{
						this.cardShow[i][k].y = (this.cardShow[i][k].yTo = PBoardScr.posCardShow[BoardScr.indexPlayer[i]].y);
						this.cardShow[i][k].x = (this.cardShow[i][k].xTo = PBoardScr.posCardShow[BoardScr.indexPlayer[i]].x + (num - 1) * this.distantCard / 2 - k * this.distantCard);
					}
				}
			}
		}
		for (int l = 0; l < cardEat2.Length; l++)
		{
			int num3 = 0;
			for (int m = 0; m < 3; m++)
			{
				num3++;
			}
			for (int n = 0; n < cardEat2[l].Length; n++)
			{
				if (cardEat2[l][n] != -1)
				{
					this.showCardEat[l][n] = new Card((sbyte)cardEat2[l][n], true);
					int num4 = BoardScr.indexPlayer[l];
					if (num4 != 0)
					{
						this.showCardEat[l][n].xTo = (this.showCardEat[l][n].x = PBoardScr.posCardEat[BoardScr.indexPlayer[l]].x);
						this.showCardEat[l][n].yTo = (this.showCardEat[l][n].y = PBoardScr.posCardEat[BoardScr.indexPlayer[l]].y - (num3 - 1) * this.distantCard / 2 + n * this.distantCard);
					}
					else
					{
						this.showCardEat[l][n].xTo = (this.showCardEat[l][n].x = PBoardScr.posCardEat[BoardScr.indexPlayer[l]].x + (num3 - 1) * this.distantCard / 2 - n * this.distantCard);
						this.showCardEat[l][n].yTo = (this.showCardEat[l][n].y = PBoardScr.posCardEat[BoardScr.indexPlayer[l]].y);
					}
				}
			}
		}
		BoardScr.interval = interval1;
		this.currentPlayer = curPlayer;
		this.firstPlayer = firPlayer;
		this.firstHa = firHa;
		BoardScr.isStartGame = true;
		for (int num5 = 0; num5 < 4; num5++)
		{
			this.cardShow[(int)BoardScr.indexOfMe][num5] = null;
		}
		for (int num6 = 0; num6 < 3; num6++)
		{
			this.showCardEat[(int)BoardScr.indexOfMe][num6] = null;
		}
		for (int num7 = 0; num7 < this.ShowHaPhom.Length; num7++)
		{
			this.ShowHaPhom[(int)BoardScr.indexOfMe][num7] = -1;
		}
		for (int num8 = 0; num8 < this.cardRac.Length; num8++)
		{
			this.cardRac[(int)BoardScr.indexOfMe][num8] = -1;
		}
		this.setPosPlaying();
		base.repaint();
		PBoardScr.disCard = (Canvas.w - BoardScr.wCard / 2) / 10;
		if (PBoardScr.disCard > BoardScr.wCard / 3 * 2)
		{
			PBoardScr.disCard = BoardScr.wCard / 3 * 2;
		}
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x00080260 File Offset: 0x0007E660
	public void setAvatar(MyVector listAva)
	{
		BoardScr.avatarInfos = listAva;
		for (int i = 0; i < BoardScr.numPlayer; i++)
		{
			Avatar avatar = (Avatar)listAva.elementAt(i);
			if (avatar.IDDB == GameMidlet.avatar.IDDB)
			{
				avatar.seriPart = GameMidlet.avatar.seriPart;
			}
			avatar.direct = Base.RIGHT;
			avatar.setAction(2);
			avatar.setFrame((int)avatar.action);
		}
	}

	// Token: 0x04000FC3 RID: 4035
	public static PBoardScr instance;

	// Token: 0x04000FC4 RID: 4036
	public static int distant = 16;

	// Token: 0x04000FC5 RID: 4037
	private int[] numC = new int[4];

	// Token: 0x04000FC6 RID: 4038
	private Card[][] cardShow;

	// Token: 0x04000FC7 RID: 4039
	public static AvPosition posCard;

	// Token: 0x04000FC8 RID: 4040
	public static AvPosition[] posName;

	// Token: 0x04000FC9 RID: 4041
	public static AvPosition[] posNamePlaying;

	// Token: 0x04000FCA RID: 4042
	public static AvPosition[] posFinish;

	// Token: 0x04000FCB RID: 4043
	public static AvPosition[] posCardShow;

	// Token: 0x04000FCC RID: 4044
	public static AvPosition[] posCardEat;

	// Token: 0x04000FCD RID: 4045
	private AvPosition arror;

	// Token: 0x04000FCE RID: 4046
	private Card card;

	// Token: 0x04000FCF RID: 4047
	private Card[] myCard = new Card[10];

	// Token: 0x04000FD0 RID: 4048
	private Card[] cardEat = new Card[3];

	// Token: 0x04000FD1 RID: 4049
	private int[][] ShowHaPhom;

	// Token: 0x04000FD2 RID: 4050
	private Card[][] showCardEat;

	// Token: 0x04000FD3 RID: 4051
	private int[] numCardEat = new int[4];

	// Token: 0x04000FD4 RID: 4052
	private int[] numCardPhom = new int[4];

	// Token: 0x04000FD5 RID: 4053
	private int[][] cardRac;

	// Token: 0x04000FD6 RID: 4054
	private sbyte[] numCardRac = new sbyte[4];

	// Token: 0x04000FD7 RID: 4055
	private sbyte[] numberCard = new sbyte[4];

	// Token: 0x04000FD8 RID: 4056
	private int numPhom;

	// Token: 0x04000FD9 RID: 4057
	private int phomRandom;

	// Token: 0x04000FDA RID: 4058
	private int phomHa;

	// Token: 0x04000FDB RID: 4059
	private int firstPlayer;

	// Token: 0x04000FDC RID: 4060
	private int cardCurrent;

	// Token: 0x04000FDD RID: 4061
	private int firstHa;

	// Token: 0x04000FDE RID: 4062
	private Card hCard;

	// Token: 0x04000FDF RID: 4063
	private int assetChange;

	// Token: 0x04000FE0 RID: 4064
	private bool finish;

	// Token: 0x04000FE1 RID: 4065
	private int winer;

	// Token: 0x04000FE2 RID: 4066
	private int denBai;

	// Token: 0x04000FE3 RID: 4067
	private bool isU;

	// Token: 0x04000FE4 RID: 4068
	private bool isHaPhom;

	// Token: 0x04000FE5 RID: 4069
	private int[] scorePlayer = new int[4];

	// Token: 0x04000FE6 RID: 4070
	private int key;

	// Token: 0x04000FE7 RID: 4071
	private bool pause;

	// Token: 0x04000FE8 RID: 4072
	private bool isEatCard;

	// Token: 0x04000FE9 RID: 4073
	private AvPosition getC;

	// Token: 0x04000FEA RID: 4074
	private Card cardE;

	// Token: 0x04000FEB RID: 4075
	private int colorPhom_1 = 473848;

	// Token: 0x04000FEC RID: 4076
	private int colorPhom_2 = 517368;

	// Token: 0x04000FED RID: 4077
	private int colorPhom_3 = 522270;

	// Token: 0x04000FEE RID: 4078
	public new static int disCard = 12;

	// Token: 0x04000FEF RID: 4079
	public static int disShow = 12;

	// Token: 0x04000FF0 RID: 4080
	public int distantCard;

	// Token: 0x04000FF1 RID: 4081
	private Command cmdEat;

	// Token: 0x04000FF2 RID: 4082
	private Command cmdGet;

	// Token: 0x04000FF3 RID: 4083
	private Command cmdHaPhom;

	// Token: 0x04000FF4 RID: 4084
	private int xShow;

	// Token: 0x04000FF5 RID: 4085
	private int yShow;

	// Token: 0x04000FF6 RID: 4086
	private int remem;

	// Token: 0x04000FF7 RID: 4087
	private bool trans;

	// Token: 0x04000FF8 RID: 4088
	private new bool isTran;

	// Token: 0x04000FF9 RID: 4089
	private int duX;

	// Token: 0x04000FFA RID: 4090
	private int duY;

	// Token: 0x04000FFB RID: 4091
	private int indexTran;

	// Token: 0x04000FFC RID: 4092
	private int pos = -2;

	// Token: 0x04000FFD RID: 4093
	private int count;

	// Token: 0x04000FFE RID: 4094
	private Card ca;

	// Token: 0x04000FFF RID: 4095
	private int[] cardToEat = new int[5];
}
