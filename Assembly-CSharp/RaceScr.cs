using System;

// Token: 0x02000182 RID: 386
public class RaceScr : MyScreen, IChatable
{
	// Token: 0x06000A2B RID: 2603 RVA: 0x00063978 File Offset: 0x00061D78
	public RaceScr()
	{
		RaceScr.FRAME = new sbyte[3][];
		RaceScr.FRAME[0] = new sbyte[]
		{
			0,
			0,
			0,
			1,
			1,
			1,
			0,
			0,
			0,
			1,
			1,
			1
		};
		RaceScr.FRAME[1] = new sbyte[]
		{
			2,
			2,
			2,
			3,
			3,
			3,
			2,
			2,
			2,
			3,
			3,
			3
		};
		RaceScr.FRAME[2] = new sbyte[]
		{
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4
		};
		this.cmdChangeFocus = new Command(T.next, 3, this);
		this.cmdExit = new Command(T.exit, 2, this);
		this.wPopup = 280 * AvMain.hd;
		this.hPopup = 240 * AvMain.hd;
		this.xInfo = 9 * AvMain.hd;
		this.yInfo = (this.yDC = 23 * AvMain.hd);
		this.wInfo = 138 * AvMain.hd;
		this.Hinfo = (this.hDC = 211 * AvMain.hd);
		this.wDC = 120 * AvMain.hd;
		this.xDC = 150 * AvMain.hd;
		this.wSelectDC = 210 * AvMain.hd + 10 * AvMain.hd + 14 * AvMain.hd;
		this.hSelectDC = 130 * AvMain.hd;
		this.xSelectDC = (Canvas.w - this.wSelectDC) / 2;
		this.ySelectDC = (Canvas.h - this.hSelectDC) / 2;
		this.initPos();
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x00063B8A File Offset: 0x00061F8A
	public static RaceScr gI()
	{
		return (RaceScr.me != null) ? RaceScr.me : (RaceScr.me = new RaceScr());
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00063BAB File Offset: 0x00061FAB
	public void initPos()
	{
		this.xPopup = (Canvas.w - this.wPopup) / 2;
		this.yPopup = (Canvas.hCan - this.hPopup) / 2;
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x00063BD5 File Offset: 0x00061FD5
	public override void switchToMe()
	{
		base.switchToMe();
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x00063BE0 File Offset: 0x00061FE0
	public void doOpenRace(RaceMsgHandler.PetRace[] pet, short timeRemain, bool isSta, bool isRace)
	{
		this.isEnd = false;
		this.nWin = 1;
		this.idPet = -1;
		Canvas.currentDialog = null;
		Canvas.currentFace = null;
		if (RaceScr.imgWater == null)
		{
			try
			{
				this.imgInfo = new FrameImage(Image.createImagePNG(T.getPath() + "/race/popup/tile1"), 20 * AvMain.hd, 20 * AvMain.hd);
				this.imgBackpet = new FrameImage(Image.createImagePNG(T.getPath() + "/race/popup/bt1"), 31 * AvMain.hd, 31 * AvMain.hd);
				this.imgBackMoney = new FrameImage(Image.createImagePNG(T.getPath() + "/race/popup/bt0"), 70 * AvMain.hd, 25 * AvMain.hd);
				this.imgTime = new FrameImage(Image.createImagePNG(T.getPath() + "/race/popup/time"), 14 * AvMain.hd, 14 * AvMain.hd);
				RaceScr.imgWater = Image.createImage(T.getPath() + "/race/28");
				RaceScr.imgFire = Image.createImage(T.getPath() + "/race/29");
				RaceScr.imgBui = new Image[5];
				for (int i = 0; i < 5; i++)
				{
					RaceScr.imgBui[i] = Image.createImage(string.Concat(new object[]
					{
						T.getPath(),
						"/race/bui/d0",
						i,
						string.Empty
					}));
				}
				RaceScr.imgTe = new Image[3];
				for (int j = 0; j < 3; j++)
				{
					RaceScr.imgTe[j] = Image.createImage(string.Concat(new object[]
					{
						T.getPath(),
						"/race/bui/w",
						j,
						string.Empty
					}));
				}
			}
			catch (Exception e)
			{
				Out.logError(e);
			}
		}
		if (!isSta)
		{
			if (isRace)
			{
				for (int k = 0; k < LoadMap.playerLists.size(); k++)
				{
					MyObject myObject = (MyObject)LoadMap.playerLists.elementAt(k);
					if ((int)myObject.catagory == 10)
					{
						LoadMap.removePlayer(myObject);
					}
				}
			}
			if (RaceScr.me != Canvas.currentMyScreen)
			{
				LoadMap.orderVector(LoadMap.playerLists);
				RaceScr.gI().switchToMe();
				LoadMap.rememMap = -1;
				this.randomPlayer(1);
				this.randomPlayer(2);
				Canvas.loadMap.load(108, true);
				LoadMap.removePlayer(GameMidlet.avatar);
				RaceScr.gI().init();
				AvCamera.isFollow = false;
			}
			this.listPet = null;
			this.listPet = pet;
			if (pet != null)
			{
				for (int l = 0; l < 6; l++)
				{
					this.listPet[l].x = 20;
					this.listPet[l].y = 80 + l * 12;
					LoadMap.playerLists.addElement(this.listPet[l]);
				}
				AvCamera.gI().followPlayer = this.listPet[2];
				this.indexFocus = 3;
			}
			GameMidlet.avatar.x = (GameMidlet.avatar.xCur = 0);
		}
		GameMidlet.avatar.y = (GameMidlet.avatar.yCur = 96 * AvMain.hd);
		this.isStart = isSta;
		this.isRace = isRace;
		this.timeRemain = timeRemain;
		if (pet == null)
		{
			if (!this.isStart)
			{
			}
			this.center = null;
		}
		else if (isSta || !isRace)
		{
			this.center = null;
		}
		this.curTime = (long)Environment.TickCount;
		if (isSta)
		{
			this.countStart = 48;
			this.center = null;
			this.right = this.cmdChangeFocus;
		}
		else
		{
			this.right = null;
			if (!isRace)
			{
				this.right = this.cmdChangeFocus;
				for (int m = 0; m < 6; m++)
				{
					int num = 0;
					for (int n = 0; n < this.listPet[m].numTick.Length; n++)
					{
						num += (int)this.listPet[m].numTick[n];
						this.listPet[m].x += (int)(this.listPet[m].vTick[n] * this.listPet[m].numTick[n]);
						RaceMsgHandler.PetRace petRace = this.listPet[m];
						petRace.count = (sbyte)((int)petRace.count + 1);
						if (num >= (int)((timeRemain - 4) * 20))
						{
							break;
						}
					}
				}
			}
			else
			{
				GlobalService.gI().doPetInfo(this.listPet[0].IDDB);
			}
		}
		this.wPetInfo = (short)(200 * AvMain.hd);
		this.hPetInfo = (short)((int)AvMain.hNormal * 7 + (int)AvMain.hBorder * 3 + 20);
		this.myChat = new ChatPopup();
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x000640EC File Offset: 0x000624EC
	private void randomPlayer(int gender)
	{
		MyVector myVector = new MyVector();
		MyVector myVector2 = new MyVector();
		MyVector myVector3 = new MyVector();
		MyVector myVector4 = new MyVector();
		MyVector myVector5 = new MyVector();
		for (int i = 0; i < AvatarData.listPart.Length; i++)
		{
			Part part = AvatarData.listPart[i];
			if (part.follow == -1 && part.IDPart < 2000 && (int)part.sell > 0)
			{
				APartInfo apartInfo = (APartInfo)part;
				if ((int)apartInfo.gender == gender || (int)apartInfo.gender == 0)
				{
					if ((int)apartInfo.zOrder == 10)
					{
						myVector.addElement(apartInfo);
					}
					else if ((int)part.zOrder == 20)
					{
						myVector2.addElement(apartInfo);
					}
					else if ((int)part.zOrder == 30)
					{
						myVector3.addElement(apartInfo);
					}
					else if ((int)part.zOrder == 40)
					{
						myVector4.addElement(apartInfo);
					}
					else if ((int)part.zOrder == 50)
					{
						myVector5.addElement(apartInfo);
					}
				}
			}
		}
		for (int j = 0; j < 10; j++)
		{
			Avatar avatar = new Avatar();
			avatar.gender = (sbyte)gender;
			avatar.addSeri(new SeriPart
			{
				idPart = ((Part)myVector.elementAt(CRes.rnd(myVector.size()))).IDPart
			});
			avatar.addSeri(new SeriPart
			{
				idPart = ((Part)myVector2.elementAt(CRes.rnd(myVector2.size()))).IDPart
			});
			avatar.addSeri(new SeriPart
			{
				idPart = ((Part)myVector3.elementAt(CRes.rnd(myVector3.size()))).IDPart
			});
			avatar.addSeri(new SeriPart
			{
				idPart = ((Part)myVector4.elementAt(CRes.rnd(myVector4.size()))).IDPart
			});
			avatar.addSeri(new SeriPart
			{
				idPart = ((Part)myVector5.elementAt(CRes.rnd(myVector5.size()))).IDPart
			});
			avatar.orderSeriesPath();
			this.listPlayer.addElement(avatar);
		}
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0006434D File Offset: 0x0006274D
	public void init()
	{
		AvCamera.gI().init(LoadMap.TYPEMAP);
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00064360 File Offset: 0x00062760
	public override void doMenu()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command("Lịch sử", 0, this));
		myVector.addElement(new Command(T.exit, 2, this));
		MenuCenter.gI().startAt(myVector);
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x000643A4 File Offset: 0x000627A4
	public override void commandActionPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			GlobalService.gI().doHistoryRace();
			Canvas.startWaitDlg();
			break;
		case 2:
		{
			IAction yes = new RaceScr.IACctionOut();
			if (this.isStart)
			{
				Canvas.startOKDlg("Bạn có muốn thoát không ?", yes);
			}
			else
			{
				Canvas.startOKDlg("Bạn có muốn thoát không (nếu có đặt cược bạn sẽ mất tiền cược) ?", yes);
			}
			break;
		}
		case 3:
		{
			AvCamera avCamera = AvCamera.gI();
			RaceMsgHandler.PetRace[] array = this.listPet;
			sbyte b;
			this.indexFocus = (sbyte)((int)(b = this.indexFocus) + 1);
			avCamera.followPlayer = array[(int)b];
			if ((int)this.indexFocus >= 6)
			{
				this.indexFocus = 0;
			}
			break;
		}
		case 5:
			if (this.isStart || !this.isRace)
			{
				this.right = this.cmdChangeFocus;
			}
			if (!this.isStart && this.isRace)
			{
				this.focus = 0;
			}
			break;
		}
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x000644A0 File Offset: 0x000628A0
	public override void update()
	{
		if ((int)this.timeOpen >= 0)
		{
			this.timeOpen = (sbyte)((int)this.timeOpen - 1);
			if ((int)this.timeOpen == 0)
			{
				this.click();
			}
		}
		if ((this.isStart || !this.isRace) && (long)Environment.TickCount - this.curTimeStart >= 1000L)
		{
			this.curTimeStart = (long)Environment.TickCount;
			this.timeStart -= 1;
			if (this.timeStart < 0)
			{
				this.timeStart = 0;
			}
		}
		GameMidlet.avatar.setPos((int)(AvCamera.gI().xCam + (float)Canvas.hw), (int)(AvCamera.gI().yCam + (float)Canvas.h - (float)(40 * AvMain.hd)));
		if ((long)Environment.TickCount - this.curTime >= 1000L)
		{
			this.curTime = (long)Environment.TickCount;
			this.timeRemain -= 1;
			if (this.timeRemain < 0)
			{
				this.timeRemain = 0;
			}
			else
			{
				this.countChangePetInfo++;
			}
		}
		if (this.listPet != null)
		{
			int num = 0;
			for (int i = 0; i < 6; i++)
			{
				if ((this.isStart || !this.isRace) && (int)this.listPet[i].count >= this.listPet[i].vTick.Length)
				{
					num++;
				}
			}
			if (!this.isEnd && num == 6)
			{
				this.isEnd = true;
				for (int j = 0; j < 6; j++)
				{
					LoadMap.removePlayer(this.listPet[j]);
				}
			}
			if (this.isEnd && this.diaWin != null)
			{
				this.isEnd = false;
				Canvas.currentFace = this.diaWin;
				GameMidlet.avatar.money[0] += this.diaWin.tienNhanDuoc;
				Canvas.addFlyText(this.diaWin.tienNhanDuoc, Canvas.hw, Canvas.h - 30 * AvMain.hd, -1, -1);
				this.diaWin = null;
			}
		}
		Canvas.loadMap.update();
		if (this.isStart && (int)this.countStart > 0)
		{
			this.countStart = (sbyte)((int)this.countStart - 1);
		}
		if (this.myChat != null && this.myChat.setOut())
		{
			this.myChat.chats = null;
		}
		if (this.isStart || !this.isRace)
		{
			for (int k = 0; k < LoadMap.playerLists.size(); k++)
			{
				Base @base = (Base)LoadMap.playerLists.elementAt(k);
				if ((int)@base.catagory == 11)
				{
					Avatar avatar = (Avatar)@base;
					if (Environment.TickCount / 1000 - avatar.exp > (int)avatar.defence)
					{
						avatar.exp = Environment.TickCount / 1000;
						avatar.defence = (short)(CRes.rnd(10) + 6);
						int num2 = CRes.rnd(6);
						if (num2 == 1)
						{
							avatar.setAction(0);
						}
						else if (num2 == 3)
						{
							avatar.setAction(0);
							avatar.doJumps();
						}
						else if (num2 == 2)
						{
							avatar.setAction(7);
						}
						else
						{
							avatar.setAction(2);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x0006481B File Offset: 0x00062C1B
	public override void keyPress(int keyCode)
	{
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00064820 File Offset: 0x00062C20
	public override void updateKey()
	{
		this.count += 1L;
		if (Canvas.welcome == null || !Welcome.isPaintArrow)
		{
			base.updateKey();
		}
		if (Canvas.isKeyPressed(2))
		{
			this.focus--;
			if (this.focus < 0)
			{
				this.focus = 0;
			}
		}
		else if (Canvas.isKeyPressed(8))
		{
			this.focus++;
			if (this.focus > 6)
			{
				this.focus = 6;
			}
		}
		if (Canvas.isPointerClick && this.listPet != null && !this.isStart && this.isRace)
		{
			if (this.isDC)
			{
				if (Canvas.isPoint(this.xSelectDC + this.wSelectDC - 23 * AvMain.hd, this.ySelectDC - 18 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
				{
					Canvas.isPointerClick = false;
					this.countCloseDC = 5;
					this.isTran = true;
					this.timeDelay = this.count;
				}
				else
				{
					for (int i = 0; i < 9; i++)
					{
						if (Canvas.isPoint(this.xSelectDC + 7 * AvMain.hd + i % 3 * (5 * AvMain.hd + this.imgBackMoney.frameWidth), this.ySelectDC + (this.hSelectDC - 29 * AvMain.hd * 3) + i / 3 * 29 * AvMain.hd - AvMain.hd, 70 * AvMain.hd, 26 * AvMain.hd))
						{
							this.indexDC = i;
							Canvas.isPointerClick = false;
							this.isTran = true;
							this.timeDelay = this.count;
							break;
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < 6; j++)
				{
					if (Canvas.isPoint(this.xPopup + this.xDC + 6 * AvMain.hd, this.yPopup + this.yDC + 3 * AvMain.hd + 35 * AvMain.hd * j, 31 * AvMain.hd, 31 * AvMain.hd))
					{
						this.indexPet = j;
						this.isTran = true;
						Canvas.isPointerClick = false;
						this.timeDelay = this.count;
						break;
					}
					if (Canvas.isPoint(this.xPopup + this.xDC + this.wDC - 6 * AvMain.hd - this.imgBackMoney.frameWidth, this.yPopup + this.yDC + 3 * AvMain.hd + 35 * AvMain.hd * j, 70 * AvMain.hd, 31 * AvMain.hd))
					{
						this.indexMoney = j;
						this.isTran = true;
						Canvas.isPointerClick = false;
						this.timeDelay = this.count;
						break;
					}
				}
			}
		}
		if (this.isTran)
		{
			if (Canvas.isPointerDown)
			{
				if (this.indexDC != -1)
				{
					if (!Canvas.isPoint(this.xSelectDC + 7 * AvMain.hd + this.indexDC % 3 * (5 * AvMain.hd + this.imgBackMoney.frameWidth), this.ySelectDC + (this.hSelectDC - 29 * AvMain.hd * 3) + this.indexDC / 3 * 29 * AvMain.hd - AvMain.hd, 70 * AvMain.hd, 26 * AvMain.hd))
					{
						this.indexDC = -1;
					}
				}
				else if ((int)this.countCloseDC != 0)
				{
					if (!Canvas.isPoint(this.xSelectDC + this.wSelectDC - 23 * AvMain.hd, this.ySelectDC - 18 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
					{
						this.countCloseDC = 0;
					}
				}
				else if (this.indexPet != -1)
				{
					if (!Canvas.isPoint(this.xPopup + this.xDC + 6 * AvMain.hd, this.yPopup + this.yDC + 3 * AvMain.hd + 35 * AvMain.hd * this.indexPet, 31 * AvMain.hd, 31 * AvMain.hd))
					{
						this.indexPet = -1;
					}
				}
				else if (this.indexMoney != -1 && !this.isDC && !Canvas.isPoint(this.xPopup + this.xDC + this.wDC - 6 * AvMain.hd - this.imgBackMoney.frameWidth, this.yPopup + this.yDC + 3 * AvMain.hd + 35 * AvMain.hd * this.indexMoney, 70 * AvMain.hd, 31 * AvMain.hd))
				{
					this.indexMoney = -1;
				}
			}
			if (Canvas.isPointerRelease)
			{
				long num = this.count - this.timeDelay;
				if (num <= 4L)
				{
					this.timeOpen = 5;
				}
				else
				{
					this.click();
				}
				this.isTran = false;
				Canvas.isPointerRelease = false;
			}
		}
		Canvas.loadMap.updateKey();
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x00064D38 File Offset: 0x00063138
	private void click()
	{
		if (this.indexDC != -1)
		{
			GlobalService.gI().doDatCuoc(this.listPet[this.indexMoney].IDDB, this.iMoney[this.indexDC]);
			this.indexDC = -1;
			this.indexMoney = -1;
			this.isDC = false;
		}
		else if ((int)this.countCloseDC == 5)
		{
			this.countCloseDC = 0;
			this.isDC = false;
			this.indexMoney = -1;
		}
		else if (this.indexPet != -1)
		{
			this.focus = this.indexPet;
			GlobalService.gI().doPetInfo(this.listPet[this.indexPet].IDDB);
			this.indexPet = -1;
		}
		else if (this.indexMoney != -1)
		{
			this.isDC = true;
		}
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x00064E10 File Offset: 0x00063210
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		Canvas.resetTrans(g);
		if (this.isRace)
		{
			if (this.isDC)
			{
				this.paintDC(g);
			}
			else if (Canvas.currentDialog == null)
			{
				Canvas.paint.paintPopupBack(g, this.xPopup, this.yPopup, this.wPopup, this.hPopup, -1, false);
				g.translate((float)this.xPopup, (float)this.yPopup);
				Canvas.normalFont.drawString(g, T.datCuoc, this.wPopup / 2, 6 * AvMain.hd, 2);
				MenuNPC.paintPopupTilte(g, this.xInfo, this.yInfo, this.wInfo, this.Hinfo, this.imgInfo, -1);
				MenuNPC.paintPopupTilte(g, this.xDC, this.yDC, this.wDC, this.hDC, MenuNPC.imgDc, 10409727);
				for (int i = 0; i < 6; i++)
				{
					this.imgBackpet.drawFrame((this.indexPet != i) ? 0 : 1, this.xDC + 6 * AvMain.hd, this.yDC + 3 * AvMain.hd + 35 * AvMain.hd * i, 0, g);
					Canvas.smallFontYellow.drawString(g, "X" + this.listPet[i].rate, this.xDC + 6 * AvMain.hd + 31 * AvMain.hd, this.yDC + 3 * AvMain.hd + 35 * AvMain.hd * i + this.imgBackpet.frameHeight - (int)AvMain.hSmall, 1);
					AvatarData.paintImg(g, (int)this.listPet[i].idIcon, this.xDC + 6 * AvMain.hd + 31 * AvMain.hd / 2, this.yDC + 3 * AvMain.hd + 35 * AvMain.hd * i + 31 * AvMain.hd / 2, 3);
					this.imgBackMoney.drawFrame((this.indexMoney != i || this.isDC) ? 0 : 1, this.xDC + this.wDC - 6 * AvMain.hd - this.imgBackMoney.frameWidth, this.yDC + 7 * AvMain.hd + 35 * AvMain.hd * i, 0, g);
					if (this.listPet[i].money > 0)
					{
						Canvas.normalFont.drawString(g, string.Empty + this.listPet[i].money, this.xDC + this.wDC - 6 * AvMain.hd - this.imgBackMoney.frameWidth / 2, this.yDC + 7 * AvMain.hd + 24 * AvMain.hd / 2 + 35 * AvMain.hd * i - (int)AvMain.hNormal / 2 - 2 * AvMain.hd - ((AvMain.hd != 1) ? 0 : 2), 2);
					}
					else
					{
						Canvas.normalFont.drawString(g, T.datCuoc, this.xDC + this.wDC - 6 * AvMain.hd - this.imgBackMoney.frameWidth / 2, this.yDC + 7 * AvMain.hd + 24 * AvMain.hd / 2 + 35 * AvMain.hd * i - (int)AvMain.hNormal / 2 - 2 * AvMain.hd - ((AvMain.hd != 1) ? 0 : 2), 2);
					}
				}
				if (this.isPetInfo && this.listPet != null)
				{
					this.paintPetInfo(g);
				}
			}
		}
		else if (this.isStart && (int)this.countStart > 0)
		{
			ImageIcon imgIcon = AvatarData.getImgIcon(1065);
			if (imgIcon.count != -1)
			{
				int num = (int)(imgIcon.h / 4);
				g.drawRegion(imgIcon.img, 0f, (float)((3 - (int)this.countStart / 12) * num), (int)imgIcon.w, num, 0, (float)(Canvas.w / 2), (float)(Canvas.h / 2), 3);
			}
		}
		Canvas.resetTrans(g);
		if (this.myChat != null && this.myChat.chats != null)
		{
			this.myChat.paintAnimal(g);
		}
		if (Canvas.welcome == null || !Welcome.isPaintArrow)
		{
			base.paint(g);
		}
		if ((this.isStart || !this.isRace) && Canvas.currentDialog == null && this.isEnd)
		{
			Canvas.borderFont.drawString(g, this.timeStart + string.Empty, Canvas.hw, 5, 2);
		}
		Canvas.paintPlus(g);
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x000652D0 File Offset: 0x000636D0
	private void paintDC(MyGraphics g)
	{
		Canvas.resetTrans(g);
		Canvas.paint.paintPopupBack(g, this.xSelectDC, this.ySelectDC, this.wSelectDC, this.hSelectDC, (int)this.countCloseDC / 3, false);
		g.translate((float)this.xSelectDC, (float)this.ySelectDC);
		Canvas.normalFont.drawString(g, "Bản đặt cược", this.wSelectDC / 2, 10 * AvMain.hd, 2);
		for (int i = 0; i < 9; i++)
		{
			this.imgBackMoney.drawFrame((this.indexDC != i) ? 0 : 1, 7 * AvMain.hd + i % 3 * (5 * AvMain.hd + this.imgBackMoney.frameWidth), this.hSelectDC - 29 * AvMain.hd * 3 + i / 3 * 29 * AvMain.hd, 0, g);
			Canvas.normalFont.drawString(g, this.iMoney[i] + string.Empty, 7 * AvMain.hd + i % 3 * (5 * AvMain.hd + this.imgBackMoney.frameWidth) + this.imgBackMoney.frameWidth / 2, this.hSelectDC - 29 * AvMain.hd * 3 + i / 3 * 29 * AvMain.hd + this.imgBackMoney.frameHeight / 2 - (int)AvMain.hNormal / 2 - 2 * AvMain.hd, 2);
		}
		Canvas.resetTrans(g);
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00065448 File Offset: 0x00063848
	private void paintPetInfo(MyGraphics g)
	{
		Canvas.normalFont.drawString(g, this.namePetInfo, this.xInfo + this.wInfo / 2, this.yInfo + 6 * AvMain.hd, 2);
		AvatarData.paintImg(g, (int)this.idImgPeInfo, this.xInfo + this.wInfo / 2, this.yInfo + 40 * AvMain.hd, 3);
		int num = this.yInfo + 70 * AvMain.hd;
		Canvas.normalFont.drawString(g, "Thắng", this.xInfo + 8 * AvMain.hd, num, 0);
		Canvas.normalFont.drawString(g, string.Empty + this.numWin + "%", this.xInfo + this.wInfo - 8 * AvMain.hd, num + (int)AvMain.hNormal / 2 - (int)AvMain.hNormal / 2, 1);
		num += (int)AvMain.hNormal + 2 * AvMain.hd;
		Canvas.normalFont.drawString(g, "Tỉ lệ", this.xInfo + 8 * AvMain.hd, num, 0);
		Canvas.normalFont.drawString(g, "X" + this.ratePetInfo, this.xInfo + this.wInfo - 8 * AvMain.hd, num + (int)AvMain.hNormal / 2 - (int)AvMain.hNormal / 2, 1);
		num += (int)AvMain.hNormal + 2 * AvMain.hd;
		Canvas.normalFont.drawString(g, "Phong độ", this.xInfo + 8 * AvMain.hd, num, 0);
		Canvas.normalFont.drawString(g, string.Empty + this.phongDo[(int)this.phongDoPetInfo], this.xInfo + this.wInfo - 8 * AvMain.hd, num + (int)AvMain.hNormal / 2 - (int)AvMain.hNormal / 2, 1);
		num += (int)AvMain.hNormal + 2 * AvMain.hd;
		Canvas.normalFont.drawString(g, "Sức khỏe", this.xInfo + 8 * AvMain.hd, num, 0);
		Canvas.normalFont.drawString(g, string.Empty + this.sucKhoe[(int)this.sucKhoePetInfo], this.xInfo + this.wInfo - 8 * AvMain.hd, num + (int)AvMain.hNormal / 2 - (int)AvMain.hNormal / 2, 1);
		this.imgTime.drawFrame(0, this.xInfo + this.imgTime.frameWidth / 2 + 8 * AvMain.hd, this.yInfo + this.Hinfo - (int)AvMain.hBorder - this.imgTime.frameHeight - 8 * AvMain.hd, 0, 3, g);
		Canvas.normalFont.drawString(g, this.timeRemain + string.Empty, this.xInfo + 8 * AvMain.hd + this.imgTime.frameWidth + 2 * AvMain.hd, this.yInfo + this.Hinfo - (int)AvMain.hBorder - this.imgTime.frameHeight - 8 * AvMain.hd - Canvas.normalFont.getHeight() / 2 - ((AvMain.hd != 1) ? 0 : 1), 0);
		this.imgTime.drawFrame(1, this.xInfo + this.imgTime.frameWidth / 2 + 8 * AvMain.hd, this.yInfo + this.Hinfo - (int)AvMain.hBorder - AvMain.hd, 0, 3, g);
		Canvas.normalFont.drawString(g, GameMidlet.avatar.money[0] + string.Empty, this.xInfo + 8 * AvMain.hd + this.imgTime.frameWidth + 2 * AvMain.hd, this.yInfo + this.Hinfo - (int)AvMain.hBorder - AvMain.hd - (int)AvMain.hNormal / 2 - ((AvMain.hd != 1) ? 0 : 1), 0);
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x00065844 File Offset: 0x00063C44
	private void paintTextTfChat(MyGraphics g, TField tf)
	{
		g.setClip((float)tf.x, (float)tf.y, (float)(tf.width + 1), (float)(tf.height + 1));
		g.setColor(16579834);
		g.fillRect((float)tf.x, (float)tf.y, (float)tf.width, (float)tf.height);
		g.setColor(2598571);
		g.drawRect((float)tf.x, (float)tf.y, (float)tf.width, (float)tf.height);
		g.setClip((float)(tf.x + 3), (float)(tf.y + 1), (float)(tf.width - 8), (float)(tf.height - 2));
		g.setColor(0);
		if (tf.paintedText.Equals(string.Empty))
		{
			Canvas.normalFont.drawString(g, tf.sDefaust, TField.TEXT_GAP_X + tf.offsetX + tf.x, tf.y + (tf.height - (int)AvMain.hBlack) / 2, 0);
		}
		else
		{
			Canvas.blackF.drawString(g, tf.paintedText, TField.TEXT_GAP_X + tf.offsetX + tf.x, tf.y + (tf.height - (int)AvMain.hBlack) / 2, 0);
		}
		if (tf.isFocused() && tf.keyInActiveState == 0 && (tf.showCaretCounter > 0 || tf.counter / 5 % 2 == 0))
		{
			g.setColor(0);
			g.fillRect((float)(TField.TEXT_GAP_X + tf.offsetX + tf.x + Canvas.arialFont.getWidth(tf.paintedText.Substring(0, tf.caretPos)) - 1 + 1), (float)(tf.y + 2), 1f, (float)(tf.height - 4));
		}
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00065A1C File Offset: 0x00063E1C
	public override void paintMain(MyGraphics g)
	{
		Canvas.resetTrans(g);
		Canvas.loadMap.paint(g);
		Canvas.loadMap.paintObject(g);
		Canvas.resetTrans(g);
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00065A40 File Offset: 0x00063E40
	public void onChatFromMe(string text)
	{
		if (text.Equals(string.Empty))
		{
			return;
		}
		this.myChat = new ChatPopup(50, text, 0);
		this.myChat.xc = Canvas.hw / AvMain.hd;
		this.myChat.yc = (Canvas.h - this.myChat.h - MyScreen.hTab - ChatTextField.gI().tfChat.height) / AvMain.hd;
		GlobalService.gI().chatToBoard(text);
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x00065AC6 File Offset: 0x00063EC6
	public void onPetInfo(short idImg, string namePet, short numWin, sbyte tile, sbyte phongDo, sbyte sucKhoe)
	{
		this.isPetInfo = true;
		this.idImgPeInfo = idImg;
		this.namePetInfo = namePet;
		this.numWin = numWin;
		this.ratePetInfo = tile;
		this.phongDoPetInfo = phongDo;
		this.sucKhoePetInfo = sucKhoe;
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x00065AFC File Offset: 0x00063EFC
	public void onChat(string text)
	{
		MyVector myVector = new MyVector();
		int num = (int)AvCamera.gI().xTo;
		if (this.isStart || !this.isRace)
		{
			num += Canvas.w / 3;
		}
		for (int i = 0; i < LoadMap.playerLists.size(); i++)
		{
			Base @base = (Base)LoadMap.playerLists.elementAt(i);
			if ((int)@base.catagory == 11 && @base.x * AvMain.hd > num && @base.x * AvMain.hd < num + Canvas.w)
			{
				myVector.addElement(@base);
			}
		}
		if (myVector.size() > 0)
		{
			int i2 = CRes.rnd(myVector.size());
			Avatar avatar = (Avatar)myVector.elementAt(i2);
			avatar.chat = new ChatPopup(50, text, 0);
		}
	}

	// Token: 0x04000D23 RID: 3363
	public static RaceScr me;

	// Token: 0x04000D24 RID: 3364
	public Command cmdChat;

	// Token: 0x04000D25 RID: 3365
	public Command cmdChangeFocus;

	// Token: 0x04000D26 RID: 3366
	public Command cmdExit;

	// Token: 0x04000D27 RID: 3367
	private int focus;

	// Token: 0x04000D28 RID: 3368
	private MyVector listChat = new MyVector();

	// Token: 0x04000D29 RID: 3369
	public RaceMsgHandler.PetRace[] listPet;

	// Token: 0x04000D2A RID: 3370
	private short timeRemain;

	// Token: 0x04000D2B RID: 3371
	private short wPetInfo;

	// Token: 0x04000D2C RID: 3372
	private short hPetInfo;

	// Token: 0x04000D2D RID: 3373
	public bool isRace;

	// Token: 0x04000D2E RID: 3374
	public bool isStart;

	// Token: 0x04000D2F RID: 3375
	public bool isEnd;

	// Token: 0x04000D30 RID: 3376
	private long curTime;

	// Token: 0x04000D31 RID: 3377
	public sbyte countStart;

	// Token: 0x04000D32 RID: 3378
	public sbyte nWin = 1;

	// Token: 0x04000D33 RID: 3379
	public sbyte indexFocus;

	// Token: 0x04000D34 RID: 3380
	public static Image imgWater;

	// Token: 0x04000D35 RID: 3381
	public static Image imgFire;

	// Token: 0x04000D36 RID: 3382
	public static Image[] imgBui;

	// Token: 0x04000D37 RID: 3383
	public static Image[] imgTe;

	// Token: 0x04000D38 RID: 3384
	private ChatPopup myChat;

	// Token: 0x04000D39 RID: 3385
	public RaceMsgHandler.dialogWin diaWin;

	// Token: 0x04000D3A RID: 3386
	public static sbyte[][] FRAME;

	// Token: 0x04000D3B RID: 3387
	public static string test = string.Empty;

	// Token: 0x04000D3C RID: 3388
	public int wPopup;

	// Token: 0x04000D3D RID: 3389
	public int hPopup;

	// Token: 0x04000D3E RID: 3390
	public int xPopup;

	// Token: 0x04000D3F RID: 3391
	public int yPopup;

	// Token: 0x04000D40 RID: 3392
	public int xInfo;

	// Token: 0x04000D41 RID: 3393
	public int yInfo;

	// Token: 0x04000D42 RID: 3394
	public int wInfo;

	// Token: 0x04000D43 RID: 3395
	public int Hinfo;

	// Token: 0x04000D44 RID: 3396
	public int xDC;

	// Token: 0x04000D45 RID: 3397
	public int yDC;

	// Token: 0x04000D46 RID: 3398
	public int wDC;

	// Token: 0x04000D47 RID: 3399
	public int hDC;

	// Token: 0x04000D48 RID: 3400
	public int xSelectDC;

	// Token: 0x04000D49 RID: 3401
	public int ySelectDC;

	// Token: 0x04000D4A RID: 3402
	public int wSelectDC;

	// Token: 0x04000D4B RID: 3403
	public int hSelectDC;

	// Token: 0x04000D4C RID: 3404
	public FrameImage imgInfo;

	// Token: 0x04000D4D RID: 3405
	public FrameImage imgBackpet;

	// Token: 0x04000D4E RID: 3406
	public FrameImage imgBackMoney;

	// Token: 0x04000D4F RID: 3407
	public short timeStart;

	// Token: 0x04000D50 RID: 3408
	public long curTimeStart;

	// Token: 0x04000D51 RID: 3409
	private FrameImage imgTime;

	// Token: 0x04000D52 RID: 3410
	private bool isDC;

	// Token: 0x04000D53 RID: 3411
	public sbyte countCloseDC;

	// Token: 0x04000D54 RID: 3412
	public MyVector listPlayer = new MyVector();

	// Token: 0x04000D55 RID: 3413
	private int idPet;

	// Token: 0x04000D56 RID: 3414
	private int countChangePetInfo;

	// Token: 0x04000D57 RID: 3415
	private int indexPet = -1;

	// Token: 0x04000D58 RID: 3416
	private int indexMoney = -1;

	// Token: 0x04000D59 RID: 3417
	private int indexDC = -1;

	// Token: 0x04000D5A RID: 3418
	private sbyte timeOpen;

	// Token: 0x04000D5B RID: 3419
	private new bool isTran;

	// Token: 0x04000D5C RID: 3420
	private long count;

	// Token: 0x04000D5D RID: 3421
	private long timeDelay;

	// Token: 0x04000D5E RID: 3422
	private int[] iMoney = new int[]
	{
		100,
		500,
		1000,
		2000,
		5000,
		10000,
		20000,
		30000,
		50000
	};

	// Token: 0x04000D5F RID: 3423
	private string[] phongDo = new string[]
	{
		"Thấp",
		"Thường",
		"Cao"
	};

	// Token: 0x04000D60 RID: 3424
	private string[] sucKhoe = new string[]
	{
		"Thấp",
		"Thường",
		"Cao"
	};

	// Token: 0x04000D61 RID: 3425
	private bool isPetInfo;

	// Token: 0x04000D62 RID: 3426
	private short idImgPeInfo;

	// Token: 0x04000D63 RID: 3427
	private short numWin;

	// Token: 0x04000D64 RID: 3428
	private string namePetInfo;

	// Token: 0x04000D65 RID: 3429
	private sbyte ratePetInfo;

	// Token: 0x04000D66 RID: 3430
	private sbyte phongDoPetInfo;

	// Token: 0x04000D67 RID: 3431
	private sbyte sucKhoePetInfo;

	// Token: 0x02000183 RID: 387
	private class IActionOkChat : IAction
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x00065BF2 File Offset: 0x00063FF2
		public void perform()
		{
		}
	}

	// Token: 0x02000184 RID: 388
	private class IActionOkListTF : IAction
	{
		// Token: 0x06000A44 RID: 2628 RVA: 0x00065BFC File Offset: 0x00063FFC
		public void perform()
		{
			RaceScr.gI().commandActionPointer(1, -1);
		}
	}

	// Token: 0x02000185 RID: 389
	private class IACctionOut : IAction
	{
		// Token: 0x06000A46 RID: 2630 RVA: 0x00065C12 File Offset: 0x00064012
		public void perform()
		{
			Canvas.startWaitDlg();
			GlobalService.gI().getHandler(9);
		}
	}
}
