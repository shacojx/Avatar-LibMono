using System;

// Token: 0x020000AD RID: 173
public class RaceMsgHandler : IMiniGameMsgHandler
{
	// Token: 0x06000573 RID: 1395 RVA: 0x000333F4 File Offset: 0x000317F4
	public static void onHandler()
	{
		if (RaceMsgHandler.instance == null)
		{
			RaceMsgHandler.instance = new RaceMsgHandler();
		}
		GlobalMessageHandler.gI().miniGameMessageHandler = RaceMsgHandler.instance;
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0003341C File Offset: 0x0003181C
	public void onMessage(Message msg)
	{
		try
		{
			switch (msg.command)
			{
			case 1:
			{
				sbyte b = msg.reader().readByte();
				if ((int)b == 0)
				{
					RaceMsgHandler.PetRace[] array = new RaceMsgHandler.PetRace[6];
					for (int i = 0; i < 6; i++)
					{
						array[i] = new RaceMsgHandler.PetRace();
						array[i].money = 0;
						array[i].IDDB = (int)msg.reader().readByte();
						array[i].rate = msg.reader().readByte();
						array[i].idImg = msg.reader().readShort();
						array[i].idIcon = msg.reader().readShort();
					}
					short timeRemain = msg.reader().readShort();
					RaceScr.gI().doOpenRace(array, timeRemain, false, true);
				}
				else if (!msg.reader().readBoolean())
				{
					RaceMsgHandler.PetRace[] array2 = new RaceMsgHandler.PetRace[6];
					for (int j = 0; j < 6; j++)
					{
						array2[j] = new RaceMsgHandler.PetRace();
						array2[j].money = 0;
						array2[j].IDDB = (int)msg.reader().readByte();
						array2[j].idImg = msg.reader().readShort();
						sbyte b2 = msg.reader().readByte();
						array2[j].numTick = new short[(int)b2];
						array2[j].vTick = new short[(int)b2];
						int num = 0;
						for (int k = 0; k < (int)b2; k++)
						{
							array2[j].numTick[k] = msg.reader().readShort();
							array2[j].vTick[k] = msg.reader().readShort();
							num += (int)array2[j].numTick[k];
						}
					}
					short timeRemain2 = msg.reader().readShort();
					RaceScr.gI().timeStart = msg.reader().readShort();
					RaceScr.gI().curTimeStart = (long)Environment.TickCount;
					RaceScr.gI().doOpenRace(array2, timeRemain2, false, false);
				}
				else
				{
					for (int l = 0; l < 6; l++)
					{
						sbyte b3 = msg.reader().readByte();
						RaceScr.gI().listPet[l].numTick = new short[(int)b3];
						RaceScr.gI().listPet[l].vTick = new short[(int)b3];
						int num2 = 0;
						for (int m = 0; m < (int)b3; m++)
						{
							RaceScr.gI().listPet[l].numTick[m] = msg.reader().readShort();
							RaceScr.gI().listPet[l].vTick[m] = msg.reader().readShort();
							num2 += (int)RaceScr.gI().listPet[l].numTick[m];
						}
					}
					short timeRemain3 = msg.reader().readShort();
					RaceScr.gI().timeStart = msg.reader().readShort();
					RaceScr.gI().curTimeStart = (long)Environment.TickCount;
					RaceScr.gI().doOpenRace(null, timeRemain3, true, false);
				}
				break;
			}
			case 2:
			{
				short idImg = msg.reader().readShort();
				string namePet = msg.reader().readUTF();
				short numWin = msg.reader().readShort();
				sbyte tile = msg.reader().readByte();
				sbyte phongDo = msg.reader().readByte();
				sbyte sucKhoe = msg.reader().readByte();
				RaceScr.gI().onPetInfo(idImg, namePet, numWin, tile, phongDo, sucKhoe);
				break;
			}
			case 5:
			{
				sbyte b4 = msg.reader().readByte();
				int money = msg.reader().readInt();
				for (int n = 0; n < RaceScr.gI().listPet.Length; n++)
				{
					if ((int)b4 == RaceScr.gI().listPet[n].IDDB)
					{
						RaceScr.gI().listPet[n].money = money;
						RaceScr.gI().indexFocus = (sbyte)n;
						break;
					}
				}
				Canvas.endDlg();
				break;
			}
			case 8:
			{
				sbyte b5 = msg.reader().readByte();
				short[] array3 = new short[(int)b5];
				string[] array4 = new string[(int)b5];
				for (int num3 = 0; num3 < (int)b5; num3++)
				{
					array3[num3] = msg.reader().readShort();
					array4[num3] = msg.reader().readUTF();
				}
				if ((int)b5 > 0)
				{
					Canvas.currentDialog = new RaceMsgHandler.HistoryPopup(array3, array4);
				}
				else
				{
					Canvas.endDlg();
				}
				break;
			}
			case 9:
			{
				string text = msg.reader().readUTF();
				RaceScr.gI().onChat(text);
				break;
			}
			case 10:
				Canvas.currentDialog = null;
				RaceScr.gI().diaWin = new RaceMsgHandler.dialogWin();
				RaceScr.gI().diaWin.idPet = msg.reader().readByte();
				RaceScr.gI().diaWin.name = msg.reader().readUTF();
				RaceScr.gI().diaWin.ratePet = msg.reader().readByte();
				RaceScr.gI().diaWin.tienCuoc = msg.reader().readInt();
				RaceScr.gI().diaWin.tienAn = msg.reader().readInt();
				RaceScr.gI().diaWin.tienThue = msg.reader().readInt();
				RaceScr.gI().diaWin.tienNhanDuoc = msg.reader().readInt();
				break;
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x04000790 RID: 1936
	public static RaceMsgHandler instance;

	// Token: 0x020000AE RID: 174
	public class dialogWin : Face
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x000339F4 File Offset: 0x00031DF4
		public dialogWin()
		{
			this.wPopupWin = (int)((short)(200 * AvMain.hd));
			this.hPopupWin = (int)((short)((int)AvMain.hNormal * 11));
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00033A1E File Offset: 0x00031E1E
		public override void commandActionPointer(int index, int subIndex)
		{
			if (index == 0)
			{
				RaceScr.gI().left = null;
				RaceScr.gI().listPet = null;
				Canvas.currentFace = null;
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00033A4C File Offset: 0x00031E4C
		public override void paint(MyGraphics g)
		{
			if (Canvas.currentDialog != null)
			{
				return;
			}
			Canvas.paint.paintPopupBack(g, (Canvas.w - this.wPopupWin) / 2, (Canvas.h - this.hPopupWin) / 2, this.wPopupWin, this.hPopupWin, -1, false);
			g.translate((float)((Canvas.w - this.wPopupWin) / 2), (float)((Canvas.h - this.hPopupWin) / 2));
			int num = 0;
			Canvas.normalFont.drawString(g, RaceScr.gI().timeStart + string.Empty, this.wPopupWin / 2, (num += (int)AvMain.hNormal) - (int)AvMain.hNormal / 2 - AvMain.hd, 2);
			Canvas.normalFont.drawString(g, "Thú đua chiến thắng", this.wPopupWin / 2, (num += (int)AvMain.hNormal) - (int)AvMain.hNormal / 2, 2);
			Canvas.borderFont.drawString(g, this.name, this.wPopupWin / 2, num += (int)AvMain.hNormal - 3 * AvMain.hd, 2);
			num += (int)AvMain.hNormal * 2;
			for (int i = 0; i < 6; i++)
			{
				if ((int)this.idPet == RaceScr.gI().listPet[i].IDDB)
				{
					ImageIcon imgIcon = AvatarData.getImgIcon(RaceScr.gI().listPet[i].idImg);
					if (imgIcon.count != -1)
					{
						int num2 = (int)(imgIcon.h / 5);
						g.drawRegion(imgIcon.img, 0f, (float)((int)RaceScr.FRAME[0][0] * num2), (int)imgIcon.w, num2, 0, (float)(this.wPopupWin / 2), (float)(num + (int)AvMain.hNormal / 2), 3);
					}
				}
			}
			num += (int)AvMain.hNormal / 2;
			Canvas.normalFont.drawString(g, "Tiền cược: ", 10 * AvMain.hd, num += (int)AvMain.hNormal, 0);
			Canvas.normalFont.drawString(g, string.Empty + this.tienCuoc, this.wPopupWin - 20 * AvMain.hd, num, 1);
			Canvas.normalFont.drawString(g, "Tiền ăn: ", 10 * AvMain.hd, num += (int)AvMain.hNormal, 0);
			Canvas.normalFont.drawString(g, string.Empty + this.tienAn, this.wPopupWin - 20 * AvMain.hd, num, 1);
			Canvas.normalFont.drawString(g, "Tiền thuế: ", 10 * AvMain.hd, num += (int)AvMain.hNormal, 0);
			Canvas.normalFont.drawString(g, string.Empty + this.tienThue, this.wPopupWin - 20 * AvMain.hd, num, 1);
			Canvas.normalFont.drawString(g, "Tiền nhận được: ", 10 * AvMain.hd, num += (int)AvMain.hNormal, 0);
			Canvas.normalFont.drawString(g, string.Empty + this.tienNhanDuoc, this.wPopupWin - 20 * AvMain.hd, num, 1);
			base.paint(g);
		}

		// Token: 0x04000791 RID: 1937
		public string name;

		// Token: 0x04000792 RID: 1938
		public sbyte idPet;

		// Token: 0x04000793 RID: 1939
		public sbyte ratePet;

		// Token: 0x04000794 RID: 1940
		private int wPopupWin;

		// Token: 0x04000795 RID: 1941
		private int hPopupWin;

		// Token: 0x04000796 RID: 1942
		public int tienCuoc;

		// Token: 0x04000797 RID: 1943
		public int tienAn;

		// Token: 0x04000798 RID: 1944
		public int tienThue;

		// Token: 0x04000799 RID: 1945
		public int tienNhanDuoc;
	}

	// Token: 0x020000AF RID: 175
	public class HistoryPopup : Dialog
	{
		// Token: 0x06000578 RID: 1400 RVA: 0x00033D60 File Offset: 0x00032160
		public HistoryPopup(short[] idPet, string[] time)
		{
			this.idPet = idPet;
			this.time = time;
			this.h = 140 * AvMain.hd;
			this.w = 0;
			for (int i = 0; i < time.Length; i++)
			{
				int num = Canvas.normalFont.getWidth(time[i]) + 40 * AvMain.hd;
				if (num > this.w)
				{
					this.w = num + 10 * AvMain.hd;
				}
			}
			this.hCell = (int)AvMain.hBorder + 5 * AvMain.hd;
			this.cmyLim = idPet.Length * this.hCell - (this.h - 10 * AvMain.hd);
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00033E27 File Offset: 0x00032227
		public override void show()
		{
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00033E2C File Offset: 0x0003222C
		public override void updateKey()
		{
			this.count++;
			bool flag = false;
			if (Canvas.isPointerClick && Canvas.isPoint((Canvas.w - this.w) / 2 + this.w - 8 * AvMain.hd, (Canvas.hCan - this.h) / 2 - 24 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
			{
				Canvas.isPointerClick = false;
				this.countClose = 5;
				this.isTranKey = true;
			}
			if (this.isTranKey)
			{
				if ((int)this.countClose == 5 && Canvas.isPointerDown && !Canvas.isPoint((Canvas.w - this.w) / 2 + this.w - 8 * AvMain.hd, (Canvas.hCan - this.h) / 2 - 24 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
				{
					this.countClose = 0;
				}
				if (Canvas.isPointerRelease)
				{
					if ((int)this.countClose == 5)
					{
						this.countClose = 0;
						Canvas.currentDialog = null;
					}
					Canvas.isPointerRelease = false;
				}
			}
			if (Canvas.isPointerClick && Canvas.isPointer((Canvas.w - this.w) / 2, (Canvas.h - this.h) / 2, this.w, this.h))
			{
				Canvas.isPointerClick = false;
				if (!this.trans)
				{
					this.pa = this.cmy;
					this.trans = true;
					this.vY = 0;
				}
			}
			if (this.trans)
			{
				int num = Canvas.dy();
				if (Canvas.isPointerDown)
				{
					if (Canvas.gameTick % 3 == 0)
					{
						this.dyTran = Canvas.py;
						this.timePoint = this.count;
					}
					this.cmtoY = this.pa + num;
					this.vY = 0;
					if (this.cmtoY < 0 || this.cmtoY > this.cmyLim)
					{
						this.cmtoY = this.pa + num / 2;
					}
					this.cmy = this.cmtoY;
				}
				if (Canvas.isPointerRelease)
				{
					this.trans = false;
					int num2 = this.count - this.timePoint;
					int num3 = this.dyTran - Canvas.py;
					if (CRes.abs(num3) > 40 && num2 < 10 && this.cmtoY > 0 && this.cmtoY < this.cmyLim)
					{
						this.vY = num3 / num2 * 10;
					}
					this.timePoint = -1;
					if (global::Math.abs(num) < 10)
					{
						this.cmtoY = this.pa + num;
					}
				}
			}
			if (Canvas.keyHold[2])
			{
				this.cmtoY -= (int)AvMain.hBorder;
				flag = true;
			}
			else if (Canvas.keyHold[8])
			{
				flag = true;
				this.cmtoY += (int)AvMain.hBorder;
			}
			if (flag)
			{
				if (this.cmtoY < 0)
				{
					this.cmtoY = 0;
				}
				if (this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
			}
			if (this.vY != 0)
			{
				if (this.cmy < 0 || this.cmy > this.cmyLim)
				{
					this.vY -= this.vY / 4;
					this.cmy += this.vY / 20;
					if (this.vY / 10 <= 1)
					{
						this.vY = 0;
					}
				}
				if (this.cmy < 0)
				{
					if (this.cmy < -this.h / 2)
					{
						this.cmy = -this.h / 2;
						this.cmtoY = 0;
						this.vY = 0;
					}
				}
				else if (this.cmy > this.cmyLim)
				{
					if (this.cmy < this.cmyLim + this.h / 2)
					{
						this.cmy = this.cmyLim + this.h / 2;
						this.cmtoY = this.cmyLim;
						this.vY = 0;
					}
				}
				else
				{
					this.cmy += this.vY / 10;
				}
				this.cmtoY = this.cmy;
				this.vY -= this.vY / 10;
				if (this.vY / 10 == 0)
				{
					this.vY = 0;
				}
			}
			else if (this.cmy < 0)
			{
				this.cmtoY = 0;
			}
			else if (this.cmy > this.cmyLim)
			{
				this.cmtoY = this.cmyLim;
			}
			if (this.cmy != this.cmtoY)
			{
				this.cmvy = this.cmtoY - this.cmy << 2;
				this.cmdy += this.cmvy;
				this.cmy += this.cmdy >> 4;
				this.cmdy &= 15;
			}
			base.updateKey();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00034340 File Offset: 0x00032740
		public override void paint(MyGraphics g)
		{
			Canvas.resetTrans(g);
			Canvas.paint.paintPopupBack(g, (Canvas.w - (this.w + 10 * AvMain.hd)) / 2, (Canvas.hCan - (this.h + 10 * AvMain.hd)) / 2, this.w + 20 * AvMain.hd, this.h + 20 * AvMain.hd, (int)this.countClose / 3, false);
			g.translate((float)((Canvas.w - this.w) / 2), (float)((Canvas.hCan - this.h) / 2));
			g.setClip(0f, (float)(7 * AvMain.hd), (float)this.w, (float)(this.h - 10 * AvMain.hd));
			g.translate(0f, (float)(-(float)this.cmy));
			for (int i = 0; i < this.idPet.Length; i++)
			{
				AvatarData.paintImg(g, (int)this.idPet[i], 15 * AvMain.hd, 15 * AvMain.hd + i * this.hCell, 3);
				Canvas.normalFont.drawString(g, this.time[i], 30 * AvMain.hd, 15 * AvMain.hd + i * this.hCell - (int)AvMain.hBorder / 2, 0);
			}
			base.paint(g);
		}

		// Token: 0x0400079A RID: 1946
		private short[] idPet;

		// Token: 0x0400079B RID: 1947
		private string[] time;

		// Token: 0x0400079C RID: 1948
		private int w;

		// Token: 0x0400079D RID: 1949
		private int h;

		// Token: 0x0400079E RID: 1950
		private int hCell;

		// Token: 0x0400079F RID: 1951
		private int cmtoY;

		// Token: 0x040007A0 RID: 1952
		private int cmy;

		// Token: 0x040007A1 RID: 1953
		private int cmdy;

		// Token: 0x040007A2 RID: 1954
		private int cmvy;

		// Token: 0x040007A3 RID: 1955
		private int cmyLim;

		// Token: 0x040007A4 RID: 1956
		private int pa;

		// Token: 0x040007A5 RID: 1957
		private bool trans;

		// Token: 0x040007A6 RID: 1958
		private int vY;

		// Token: 0x040007A7 RID: 1959
		private int count;

		// Token: 0x040007A8 RID: 1960
		private int timePoint;

		// Token: 0x040007A9 RID: 1961
		private int dyTran;

		// Token: 0x040007AA RID: 1962
		private bool isTranKey;

		// Token: 0x040007AB RID: 1963
		private sbyte countClose;
	}

	// Token: 0x020000B0 RID: 176
	public class PetRace : Base
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x000344C4 File Offset: 0x000328C4
		public override void update()
		{
			this.indexBui = (sbyte)((int)this.indexBui + 1);
			if ((int)this.indexBui >= 10)
			{
				this.indexBui = 0;
			}
			if ((int)this.indexTe < 9)
			{
				this.indexTe = (sbyte)((int)this.indexTe + 1);
			}
			this.numF++;
			if (this.numF >= 6)
			{
				this.numF = 0;
			}
			this.frame++;
			if (this.frame == 24)
			{
				this.frame = 0;
			}
			if (this.x < (int)(LoadMap.wMap + 1) * LoadMap.w)
			{
				if (this.numTick != null && (int)this.count < this.numTick.Length && (int)RaceScr.gI().countStart <= 0)
				{
					this.x += (int)this.vTick[(int)this.count];
					if (this.vTick[(int)this.count] == 0)
					{
						this.action = 2;
					}
					else
					{
						this.action = 1;
					}
					short[] array = this.numTick;
					int num = (int)this.count;
					array[num] -= 1;
					if (this.numTick[(int)this.count] <= 0)
					{
						this.count = (sbyte)((int)this.count + 1);
						if ((int)this.count < this.vTick.Length)
						{
							if ((int)this.indexTe == 9 && this.vTick[(int)this.count] == 0)
							{
								this.indexTe = 0;
							}
							else if ((int)this.countWater == -1 && this.vTick[(int)this.count] == 2)
							{
								this.countWater = 20;
							}
							else if ((int)this.countFire == -1 && this.vTick[(int)this.count] == 5)
							{
								this.countFire = 20;
							}
						}
					}
				}
				else
				{
					this.action = 0;
					if (this.vTick != null && (int)RaceScr.gI().countStart <= 0)
					{
						this.x += (int)this.vTick[this.vTick.Length - 1];
					}
					if ((int)this.win == 10 && this.numTick != null && (int)this.count >= this.numTick.Length)
					{
						RaceScr raceScr = RaceScr.gI();
						sbyte nWin;
						raceScr.nWin = (sbyte)((int)(nWin = raceScr.nWin) + 1);
						this.win = nWin;
					}
				}
				if ((int)this.countWater >= 0)
				{
					this.countWater = (sbyte)((int)this.countWater - 1);
				}
				if ((int)this.countFire >= 0)
				{
					this.countFire = (sbyte)((int)this.countFire - 1);
				}
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00034780 File Offset: 0x00032B80
		public override void paint(MyGraphics g)
		{
			ImageIcon imgIcon = AvatarData.getImgIcon(this.idImg);
			if (imgIcon.count != -1)
			{
				int num = (int)(imgIcon.h / 5);
				g.drawRegion(imgIcon.img, 0f, (float)((int)RaceScr.FRAME[(int)this.action][this.frame / 2] * num), (int)imgIcon.w, num, 0, (float)(this.x * MyObject.hd), (float)(this.y * MyObject.hd), MyGraphics.HCENTER | MyGraphics.BOTTOM);
				if (RaceScr.gI().isStart && this.money > 0)
				{
					Canvas.arialFont.drawString(g, string.Empty + this.money, this.x * MyObject.hd - (int)(imgIcon.w / 2) - 12 * MyObject.hd, this.y * MyObject.hd - (int)AvMain.hBlack / 2 - MyObject.hd, 1);
				}
				if ((int)this.countWater >= 0)
				{
					g.drawImage(RaceScr.imgWater, (float)(this.x * MyObject.hd + (int)(imgIcon.w / 2)), (float)(this.y * MyObject.hd - num), MyGraphics.BOTTOM | MyGraphics.HCENTER);
				}
				if ((int)this.indexTe < 9)
				{
					g.drawImage(RaceScr.imgTe[(int)this.indexTe / 3], (float)(this.x * MyObject.hd), (float)(this.y * MyObject.hd), 3);
				}
				if ((int)this.countFire >= 0)
				{
					g.drawImage(RaceScr.imgFire, (float)(this.x * MyObject.hd + (int)(imgIcon.w / 2)), (float)(this.y * MyObject.hd - num), MyGraphics.BOTTOM | MyGraphics.HCENTER);
					g.drawImage(RaceScr.imgBui[(int)this.indexBui / 2], (float)(this.x * MyObject.hd - (int)(imgIcon.w / 2)), (float)(this.y * MyObject.hd), 3);
				}
				if (this.IDDB == AvCamera.gI().followPlayer.IDDB)
				{
					g.drawImage(MapScr.imgFocusP, (float)(this.x * MyObject.hd), (float)(this.y * MyObject.hd - num - this.numF / 2 - 10 * MyObject.hd), 3);
				}
			}
		}

		// Token: 0x040007AC RID: 1964
		public sbyte rate;

		// Token: 0x040007AD RID: 1965
		public sbyte count;

		// Token: 0x040007AE RID: 1966
		public sbyte win = 10;

		// Token: 0x040007AF RID: 1967
		public sbyte countWater = -1;

		// Token: 0x040007B0 RID: 1968
		public sbyte countFire = -1;

		// Token: 0x040007B1 RID: 1969
		public sbyte indexBui;

		// Token: 0x040007B2 RID: 1970
		public sbyte indexTe = 6;

		// Token: 0x040007B3 RID: 1971
		public short idImg;

		// Token: 0x040007B4 RID: 1972
		public short idIcon;

		// Token: 0x040007B5 RID: 1973
		public short[] numTick;

		// Token: 0x040007B6 RID: 1974
		public short[] vTick;

		// Token: 0x040007B7 RID: 1975
		public new string name = string.Empty;

		// Token: 0x040007B8 RID: 1976
		public int money;

		// Token: 0x040007B9 RID: 1977
		private int numF;
	}
}
