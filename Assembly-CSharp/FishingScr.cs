using System;

// Token: 0x0200010B RID: 267
public class FishingScr : MyScreen
{
	// Token: 0x06000753 RID: 1875 RVA: 0x00043868 File Offset: 0x00041C68
	public FishingScr()
	{
		this.cmdQuanCau = new Command(T.toss, 0, this);
		this.cmdXong = new Command(T.finish, 1, this);
		this.cmdClose = new Command(T.close, 2, this);
		this.center = this.cmdQuanCau;
		this.wTime = 530;
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x000438D3 File Offset: 0x00041CD3
	public static FishingScr gI()
	{
		return (FishingScr.me != null) ? FishingScr.me : (FishingScr.me = new FishingScr());
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x000438F4 File Offset: 0x00041CF4
	public override void commandActionPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if ((int)GameMidlet.avatar.action != 2 && (int)GameMidlet.avatar.action != 13)
			{
				MapScr.gI().switchToMe();
			}
			ParkService.gI().doQuanCau();
			Canvas.startWaitDlg();
			this.center = null;
			break;
		case 1:
			ParkService.gI().doCauCaXong();
			Canvas.startWaitDlg();
			break;
		case 2:
			this.doClose();
			ParkService.gI().doCauCaXong();
			break;
		case 4:
			this.doClose();
			break;
		}
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0004399C File Offset: 0x00041D9C
	protected void doClose()
	{
		GameMidlet.avatar.resetTypeChair();
		if ((int)GameMidlet.avatar.direct == (int)Base.RIGHT)
		{
			GameMidlet.avatar.x -= 18;
		}
		else
		{
			GameMidlet.avatar.x += 18;
		}
		GameMidlet.avatar.y -= 10;
		AvCamera.setDistance(Canvas.w / 10);
		MapScr.listFish.removeElement(this.fish);
		MapScr.gI().switchToMe();
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00043A30 File Offset: 0x00041E30
	protected void doQuanCau(Avatar ava)
	{
		Fish fish = FishingScr.getFish(ava.IDDB);
		if (fish != null)
		{
			MapScr.listFish.removeElement(fish);
		}
		Fish fish2 = new Fish();
		if (ava.IDDB == GameMidlet.avatar.IDDB)
		{
			Canvas.endDlg();
			this.fish = fish2;
			this.finish = this.wTime / 2;
		}
		else
		{
			fish2 = new Fish();
		}
		MapScr.listFish.addElement(fish2);
		if ((int)ava.action != 2)
		{
			if (ava.IDDB != GameMidlet.avatar.IDDB)
			{
				fish2.ava = ava;
				fish2.isWait = true;
			}
			return;
		}
		fish2.doQuanCau(ava);
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00043AE0 File Offset: 0x00041EE0
	public bool doSat(int x, int y)
	{
		this.yKeyArr = Canvas.h / 2;
		if (this.yKeyArr > Canvas.h - 70 * AvMain.hd)
		{
			this.yKeyArr = Canvas.h - 70 * AvMain.hd;
		}
		this.xKeyArr = 60;
		if (this.xKeyArr < (Canvas.w - (int)(LoadMap.wMap * 24)) / 2 + 50 * AvMain.hd)
		{
			this.xKeyArr = (Canvas.w - (int)(LoadMap.wMap * 24)) / 2 + 50 * AvMain.hd;
		}
		this.index = 0;
		int num = LoadMap.getposMap(x, y);
		if (LoadMap.map[num + 1] == 100 || LoadMap.map[num + 1] == 16 || LoadMap.map[num + 1] == 13)
		{
			GameMidlet.avatar.direct = Base.RIGHT;
			this.xKeyArr = Canvas.w - this.xKeyArr;
		}
		else
		{
			GameMidlet.avatar.direct = Base.LEFT;
		}
		GameMidlet.avatar.setLayPLayer(x, y);
		this.isAble = false;
		ParkService.gI().doStartFishing();
		Canvas.startWaitDlg();
		this.right = this.cmdClose;
		Canvas.clearKeyHold();
		return true;
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00043C1C File Offset: 0x0004201C
	public override void update()
	{
		MapScr.gI().update();
		if (this.fish.isCanCau && !this.fish.isSuccess)
		{
			if (this.index < this.arrIndex.Length && (long)Environment.TickCount - this.cTime > (long)this.timeDelay)
			{
				this.setIndex(0);
			}
			if ((int)GameMidlet.avatar.action == 2)
			{
				this.iCancau--;
				if (this.iCancau < 0)
				{
					this.iCancau = 0;
					this.fish.setPosDay(1);
				}
			}
		}
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00043CC4 File Offset: 0x000420C4
	public override void keyPress(int keyCode)
	{
		if (this.fish.isCanCau && !this.fish.isSuccess)
		{
			switch (keyCode)
			{
			case 50:
			case 52:
			case 54:
			case 56:
				Canvas.keyPressed[keyCode - 48] = true;
				break;
			}
		}
		else
		{
			MapScr.gI().keyPress(keyCode);
		}
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00043D3C File Offset: 0x0004213C
	public override void updateKey()
	{
		if (!this.fish.isCanCau || !this.fish.isSuccess)
		{
		}
		int num = Canvas.paint.updateKeyArr(this.xKeyArr, this.yKeyArr);
		if (num != -1 && this.fish.isCanCau && !this.fish.isSuccess)
		{
			Canvas.isPointerClick = false;
			this.setIndex(num);
		}
		base.updateKey();
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00043DBC File Offset: 0x000421BC
	private void setIndex(int key)
	{
		this.cTime = (long)Environment.TickCount;
		if (this.index < this.arrIndex.Length)
		{
			Canvas.test2 = Canvas.test2 + key + ", ";
			this.arrIndex[this.index] = (sbyte)key;
		}
		this.index++;
		if ((int)GameMidlet.avatar.action != 2)
		{
			this.fish.setPosDay(0);
			this.iCancau = 2;
		}
		if (this.index >= this.arrIndex.Length)
		{
			this.fish.setPosDay(0);
			this.fish.isSuccess = true;
			ParkService.gI().doFinishFishing(true, this.arrIndex);
			Canvas.startWaitDlg();
		}
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00043E84 File Offset: 0x00042284
	public override void paint(MyGraphics g)
	{
		MapScr.gI().paintMain(g);
		if (this.fish.isCanCau && !this.fish.isSuccess)
		{
			this.paintTime(g);
		}
		Canvas.paint.paintKeyArrow(g, this.xKeyArr, this.yKeyArr);
		base.paint(g);
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00043EE4 File Offset: 0x000422E4
	private void paintTime(MyGraphics g)
	{
		Canvas.resetTrans(g);
		g.translate(-AvCamera.gI().xCam, -AvCamera.gI().yCam);
		g.setColor(8575990);
		if (this.imgArrow != null && this.index < this.imgArrow.Length)
		{
			if ((long)Environment.TickCount - this.cTime > 50L)
			{
				g.setColor(1423411);
			}
			else
			{
				g.setColor(15612731);
			}
			g.drawImage(this.imgArrow[this.index], (float)this.xTime, (float)(this.yTime * AvMain.hd), 0);
		}
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x00043F94 File Offset: 0x00042394
	public void onQuanCau(int idUser)
	{
		Avatar avatar = LoadMap.getAvatar(idUser);
		if (avatar != null)
		{
			this.doQuanCau(avatar);
		}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00043FB8 File Offset: 0x000423B8
	public void onCaCanCau(int idUser3, int idFish, short timeDelay, sbyte[][] arrImg)
	{
		Fish fish = FishingScr.getFish(idUser3);
		if (fish != null && fish.isQuan != 0)
		{
			if (((int)fish.ava.action != 13 && (int)fish.ava.action != 2) || fish.isCanCau)
			{
				return;
			}
			fish.isCanCau = true;
			fish.setPosDay(0);
			fish.ava.action = 2;
			fish.idFish = idFish;
			Canvas.addFlyTextSmall(T.bite, fish.ava.x, fish.ava.y - 60, -1, 1, -1);
			SoundManager.playSound(0);
			if (idUser3 == GameMidlet.avatar.IDDB)
			{
				this.cTime = (long)Environment.TickCount;
				this.index = 0;
				this.iCancau = 2;
				this.imgArrow = new Image[arrImg.Length];
				this.arrIndex = new sbyte[arrImg.Length];
				for (int i = 0; i < this.imgArrow.Length; i++)
				{
					this.imgArrow[i] = Image.createImage(ArrayCast.cast(arrImg[i]));
				}
				this.timeDelay = timeDelay;
				this.xTemp = this.fish.posDay[(int)this.fish.size - 2].x + this.wTime / 20;
				this.xTime = this.fish.posTemp[(int)this.fish.size - 2].x;
				this.yTime = this.fish.posTemp[(int)this.fish.size - 2].y - 30;
			}
		}
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00044158 File Offset: 0x00042558
	public void onFinish(int idUser4, int idFish)
	{
		Fish fish = FishingScr.getFish(idUser4);
		if (fish != null)
		{
			if ((int)fish.ava.action != 2 && (int)fish.ava.action != 13)
			{
				MapScr.listFish.removeElement(fish);
				return;
			}
			if (idFish < 0)
			{
				Canvas.addFlyTextSmall(T.miss, fish.ava.x, fish.ava.y - 60, -1, 1, -1);
			}
			else
			{
				SoundManager.playSound(1);
			}
			fish.idFish = idFish;
			fish.isSuccess = true;
			fish.setPosDay(0);
			if (fish.ava.IDDB == GameMidlet.avatar.IDDB)
			{
				this.right = this.cmdXong;
				Canvas.endDlg();
			}
		}
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x0004421C File Offset: 0x0004261C
	public static Fish getFish(int id)
	{
		for (int i = 0; i < MapScr.listFish.size(); i++)
		{
			Fish fish = (Fish)MapScr.listFish.elementAt(i);
			if (fish.ava.IDDB == id)
			{
				return fish;
			}
		}
		return null;
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x0004426C File Offset: 0x0004266C
	public void onCauCaXong(int idUser5)
	{
		Fish fish = FishingScr.getFish(idUser5);
		if (idUser5 == GameMidlet.avatar.IDDB)
		{
			this.finish = this.wTime / 2;
			this.center = this.cmdQuanCau;
			this.right = this.cmdClose;
			Canvas.endDlg();
		}
		if (fish != null)
		{
			if (fish.idFish > 0)
			{
				PartSmall partSmall = (PartSmall)AvatarData.getPart((short)fish.idFish);
				if (partSmall != null)
				{
					ImageInfo imageInfo = AvatarData.listImgInfo[(int)partSmall.idIcon];
					Image img = Image.createImage(AvatarData.getBigImgInfo((int)imageInfo.bigID).img, (int)imageInfo.x0 * AvMain.hd, (int)imageInfo.y0 * AvMain.hd, (int)imageInfo.w * AvMain.hd, (int)imageInfo.h * AvMain.hd, 0);
					Canvas.addFlyText(1, fish.ava.x, fish.ava.y + (int)fish.ava.ySat - 50, -1, img, -1);
				}
			}
			MapScr.listFish.removeElement(fish);
		}
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00044378 File Offset: 0x00042778
	public void onStartFishing(bool iss, string error)
	{
		if (iss)
		{
			this.fish.doSetDayCau();
			this.center = this.cmdQuanCau;
			this.switchToMe();
			AvCamera.setDistance(Canvas.w / 3);
			Canvas.endDlg();
		}
		else
		{
			Canvas.msgdlg.setInfoC(error, new Command(T.OK, 4, this));
		}
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x000443D8 File Offset: 0x000427D8
	public void onStatus(int idUser6, int status)
	{
		Avatar avatar = LoadMap.getAvatar(idUser6);
		if (avatar != null && ((int)avatar.action == 2 || (int)avatar.action == 13))
		{
			Fish fish = new Fish();
			MapScr.listFish.addElement(fish);
			fish.doQuanCau(avatar);
			fish.doQuanDay();
			fish.posDay[(int)fish.size - 1].x = avatar.x + 70 + (AvMain.hd - 1) * 35 + CRes.rnd(25);
			fish.posDay[(int)fish.size - 1].y = avatar.y;
			fish.isQuan = 1;
			fish.countQuan = -1;
			fish.setPosDay(1);
			if (status == 2)
			{
				fish.isCanCau = true;
			}
			else if (status == 3)
			{
				fish.isCanCau = true;
				fish.isSuccess = true;
				fish.distant = 2;
			}
		}
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x000444BC File Offset: 0x000428BC
	public void onInfo(int idUser7, sbyte lv, sbyte perLv, int numFish, short idPart)
	{
		Avatar avatar = LoadMap.getAvatar(idUser7);
		if (avatar == null && ListScr.tempList != null)
		{
			for (int i = 0; i < ListScr.tempList.size(); i++)
			{
				Avatar avatar2 = (Avatar)ListScr.tempList.elementAt(i);
				if (avatar2.IDDB == idUser7)
				{
					avatar = avatar2;
				}
			}
		}
		Avatar avatar3 = avatar;
		if (avatar3 != null)
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new FishingScr.CommandInfo(string.Empty, new FishingScr.IActionNothing(), avatar3, perLv, idPart, numFish, lv));
			PopupShop.gI().addElement(new string[]
			{
				T.info
			}, new MyVector[1], myVector, null);
			PopupShop.gI().switchToMe();
		}
		Canvas.endDlg();
	}

	// Token: 0x04000951 RID: 2385
	public static FishingScr me;

	// Token: 0x04000952 RID: 2386
	private Command cmdQuanCau;

	// Token: 0x04000953 RID: 2387
	private Command cmdClose;

	// Token: 0x04000954 RID: 2388
	private Command cmdXong;

	// Token: 0x04000955 RID: 2389
	public static Image imgPhao;

	// Token: 0x04000956 RID: 2390
	public static FrameImage imgCa;

	// Token: 0x04000957 RID: 2391
	public Fish fish = new Fish();

	// Token: 0x04000958 RID: 2392
	public int finish;

	// Token: 0x04000959 RID: 2393
	public int xTime;

	// Token: 0x0400095A RID: 2394
	public int yTime;

	// Token: 0x0400095B RID: 2395
	public int xTemp;

	// Token: 0x0400095C RID: 2396
	private int wTime;

	// Token: 0x0400095D RID: 2397
	public bool isSuccess;

	// Token: 0x0400095E RID: 2398
	public bool isAble;

	// Token: 0x0400095F RID: 2399
	private Image[] imgArrow;

	// Token: 0x04000960 RID: 2400
	private int index;

	// Token: 0x04000961 RID: 2401
	private sbyte[] arrIndex;

	// Token: 0x04000962 RID: 2402
	private long cTime;

	// Token: 0x04000963 RID: 2403
	private short timeDelay;

	// Token: 0x04000964 RID: 2404
	private int iCancau;

	// Token: 0x04000965 RID: 2405
	private int xKeyArr;

	// Token: 0x04000966 RID: 2406
	private int yKeyArr;

	// Token: 0x0200010C RID: 268
	private class IActionNothing : IAction
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x0004457E File Offset: 0x0004297E
		public void perform()
		{
		}
	}

	// Token: 0x0200010D RID: 269
	private class CommandInfo : Command
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x00044580 File Offset: 0x00042980
		public CommandInfo(string s, FishingScr.IActionNothing nothing, Avatar ava1, sbyte perLv, short idPart, int numFish, sbyte lv) : base(s, nothing)
		{
			this.ava1 = ava1;
			this.perLv = perLv;
			this.idPart = idPart;
			this.numFish = numFish;
			this.lv = lv;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000445B4 File Offset: 0x000429B4
		public override void paint(MyGraphics g, int x, int y)
		{
			Canvas.resetTrans(g);
			int num = (int)PaintPopup.hTab + AvMain.hDuBox * 2 + 10 * AvMain.hd + 30 * (AvMain.hd - 1) + PopupShop.y;
			int height = Canvas.normalFont.getHeight();
			this.ava1.paintIcon(g, Canvas.w / 2, num, false);
			Canvas.normalFont.drawString(g, T.nameStr + this.ava1.name, Canvas.w / 2, num + height, 2);
			Canvas.normalFont.drawString(g, string.Concat(new object[]
			{
				T.level[3],
				this.lv,
				" (",
				this.perLv,
				"%)"
			}), Canvas.w / 2, num + height * 2, 2);
			Canvas.normalFont.drawString(g, T.numberFish + this.numFish, Canvas.w / 2, num + height * 3, 2);
			Canvas.normalFont.drawString(g, T.achieve + ": ", Canvas.w / 2, num + height * 4, 2);
			if (this.idPart != -1)
			{
				PartSmall partSmall = (PartSmall)AvatarData.getPart(this.idPart);
				partSmall.paint(g, Canvas.w / 2, num + height * 6, 3);
			}
		}

		// Token: 0x04000967 RID: 2407
		private readonly Avatar ava1;

		// Token: 0x04000968 RID: 2408
		private readonly sbyte perLv;

		// Token: 0x04000969 RID: 2409
		private readonly short idPart;

		// Token: 0x0400096A RID: 2410
		private readonly int numFish;

		// Token: 0x0400096B RID: 2411
		private readonly sbyte lv;
	}
}
