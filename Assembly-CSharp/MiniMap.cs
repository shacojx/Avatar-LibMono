using System;

// Token: 0x0200016C RID: 364
public class MiniMap : MyScreen
{
	// Token: 0x0600099A RID: 2458 RVA: 0x0005E280 File Offset: 0x0005C680
	public MiniMap()
	{
		MiniMap.imgArrow = new FrameImage(Image.createImagePNG(T.getPath() + "/main/up"), 13 * AvMain.hd, 11 * AvMain.hd);
		MiniMap.imgSmallIcon = Image.createImage(T.getPath() + "/effect/sIc");
		MiniMap.imgBackIcon = Image.createImagePNG(T.getPath() + "/effect/b_p");
		MiniMap.imgPopupName = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/minimapbanner"), 85 * AvMain.hd, 35 * AvMain.hd);
		this.imgClound = new Image[2];
		for (int i = 0; i < 2; i++)
		{
			this.imgClound[i] = Image.createImagePNG(T.getPath() + "/effect/clMini" + i);
		}
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0005E369 File Offset: 0x0005C769
	public static MiniMap gI()
	{
		return (MiniMap.me != null) ? MiniMap.me : (MiniMap.me = new MiniMap());
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0005E38A File Offset: 0x0005C78A
	public override void switchToMe()
	{
		base.switchToMe();
		this.isHide = true;
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0005E39C File Offset: 0x0005C79C
	public void switchToMe(MyScreen last)
	{
		this.lastScr = Canvas.currentMyScreen;
		base.switchToMe();
		if (!GlobalLogicHandler.isNewVersion)
		{
			Canvas.endDlg();
		}
		if (Session_ME.gI().isConnected() && Canvas.isInitChar)
		{
			Canvas.welcome = new Welcome();
			Canvas.welcome.initMiniMap();
		}
		if (Canvas.load == 0)
		{
			Canvas.load = 1;
		}
		this.initZoom();
		LoadMap.TYPEMAP = -1;
		Canvas.currentEffect.removeAllElements();
		FarmScr.cell = null;
		SoundManager.playSoundBG(85);
		Canvas.setPopupTime(MiniMap.nameSV);
		MapScr.idMapOld = -1;
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0005E439 File Offset: 0x0005C839
	public override void initZoom()
	{
		this.init();
		this.tran();
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0005E447 File Offset: 0x0005C847
	public override void commandTab(int index)
	{
		if (index == 1)
		{
			MapScr.gI().switchToMe();
			MiniMap.imgPopup = null;
		}
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0005E46C File Offset: 0x0005C86C
	public override void commandActionPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			GlobalService.gI().requestService(6, string.Empty);
			break;
		case 1:
			MapScr.gI().doChangePass();
			break;
		case 3:
			GlobalService.gI().requestService(3, null);
			break;
		case 4:
			OptionScr.gI().switchToMe();
			break;
		case 5:
			MapScr.gI().exitGame();
			ServerListScr.gI().switchToMe();
			break;
		}
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0005E4FC File Offset: 0x0005C8FC
	public override void doMenu()
	{
		if (Canvas.welcome != null && Welcome.isPaintArrow)
		{
			return;
		}
		MyVector myVector = new MyVector();
		myVector.addElement(new Command("Đăng ký", MiniMap.actionReg));
		Command o = new Command(T.info, 0, this);
		Command o2 = new Command(T.changePass, 1, this);
		myVector.addElement(o);
		myVector.addElement(o2);
		Command o3 = new Command(T.otherGame, 3, this);
		Command o4 = new Command(T.option, 4, this);
		myVector.addElement(o3);
		myVector.addElement(o4);
		myVector.addElement(new Command(T.chooseAnotherCity, 5, this));
		if (Canvas.currentMyScreen == PopupShop.gI())
		{
			return;
		}
		MenuCenter.gI().startAt(myVector);
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x0005E5B8 File Offset: 0x0005C9B8
	public override void close()
	{
		if (!MiniMap.isCityMap && Canvas.currentMyScreen != ServerListScr.me)
		{
			MapScr.gI().switchToMe();
			MiniMap.imgPopup = null;
		}
		else
		{
			MapScr.gI().doExitGame();
		}
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0005E5F4 File Offset: 0x0005C9F4
	public void setInfo(FrameImage img, sbyte[] map, MyVector pos, sbyte wMn, int wsmall, Command cmdCenter)
	{
		AvatarData.getImgIcon(839);
		GameMidlet.avatar.ableShow = false;
		this.wSmall = (sbyte)wsmall;
		this.map = map;
		this.listPos = pos;
		this.wMini = wMn;
		this.cmdCenter = cmdCenter;
		if (Canvas.stypeInt == 0)
		{
			this.center = cmdCenter;
		}
		this.hMini = (sbyte)(map.Length / (int)this.wMini);
		this.right = null;
		this.init();
		this.cmdUpdateKey = null;
		MiniMap.listClound.removeAllElements();
		for (int i = 0; i < 7; i++)
		{
			MiniMap.listClound.addElement(new AvPosition(i * ((int)this.wMini * (int)this.wSmall) / 10 + 50, CRes.rnd(10) * ((int)this.hMini * (int)this.wSmall / 10) + 20, CRes.rnd(2)));
		}
		MiniMap.cmtoY = (MiniMap.cmy = (MiniMap.cmx = (MiniMap.cmtoX = (float)(this.selected = 0))));
		this.tran();
		if (MiniMap.isCityMap)
		{
			MiniMap.imgPopup = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/k"), 40 * AvMain.hd, 40 * AvMain.hd);
		}
		MiniMap.imgMap = img;
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0005E744 File Offset: 0x0005CB44
	public void init()
	{
		this.x = (Canvas.w - (int)this.wMini * (int)this.wSmall) / 2;
		this.y = (Canvas.hCan - ((AvMain.hFillTab == 0) ? Canvas.hTab : AvMain.hFillTab) - (int)this.hMini * (int)this.wSmall) / 2;
		if (this.x < 0)
		{
			this.x = 0;
		}
		if (this.y < 0)
		{
			this.y = 0;
		}
		float num = (float)this.wSmall;
		MiniMap.cmxLim = (float)((int)this.wMini * (int)this.wSmall - Canvas.w);
		MiniMap.cmyLim = (float)((int)this.hMini * (int)this.wSmall - Canvas.hCan);
		if (MiniMap.cmxLim < 0f)
		{
			MiniMap.cmxLim = (MiniMap.cmx = 0f);
		}
		if (MiniMap.cmyLim < 0f)
		{
			MiniMap.cmyLim = (MiniMap.cmy = 0f);
		}
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0005E848 File Offset: 0x0005CC48
	public override void update()
	{
		for (int i = 0; i < this.listPos.size(); i++)
		{
			PositionMap positionMap = (PositionMap)this.listPos.elementAt(i);
			if (i != this.selected)
			{
				positionMap.count++;
				if (positionMap.count >= 18)
				{
					positionMap.count = 0;
				}
			}
		}
		if (this.vY != 0)
		{
			if (MiniMap.cmy < 0f || MiniMap.cmy > MiniMap.cmyLim)
			{
				this.vY -= this.vY / 4;
				MiniMap.cmy += (float)(this.vY / 20);
				if (this.vY / 10 <= 1)
				{
					this.vY = 0;
				}
			}
			MiniMap.cmy += (float)(this.vY / 20);
			MiniMap.cmtoY = MiniMap.cmy;
			this.vY -= this.vY / 20;
			if (this.vY / 10 == 0)
			{
				this.vY = 0;
			}
		}
		if (MiniMap.cmy < 0f)
		{
			MiniMap.cmtoY = 0f;
			this.vY = 0;
		}
		else if (MiniMap.cmy > MiniMap.cmyLim)
		{
			MiniMap.cmtoY = MiniMap.cmyLim;
			this.vY = 0;
		}
		if (this.vX != 0)
		{
			if (MiniMap.cmx < 0f || MiniMap.cmx > MiniMap.cmxLim)
			{
				this.vX -= this.vX / 4;
				MiniMap.cmx += (float)(this.vX / 20);
				if (this.vX / 10 <= 1)
				{
					this.vX = 0;
				}
			}
			MiniMap.cmx += (float)(this.vX / 20);
			this.vX -= this.vX / 20;
			MiniMap.cmtoX = MiniMap.cmx;
			if (this.vX / 10 == 0)
			{
				this.vX = 0;
			}
		}
		if (MiniMap.cmx < 0f)
		{
			MiniMap.cmtoX = 0f;
			this.vX = 0;
		}
		else if (MiniMap.cmx > MiniMap.cmxLim)
		{
			MiniMap.cmtoX = MiniMap.cmxLim;
			this.vX = 0;
		}
		if (!Canvas.isZoom)
		{
			if (MiniMap.cmy != MiniMap.cmtoY)
			{
				MiniMap.cmvy = (float)((int)(MiniMap.cmtoY - MiniMap.cmy) << 2);
				MiniMap.cmdy += MiniMap.cmvy;
				MiniMap.cmy += (float)((int)MiniMap.cmdy >> 4);
				MiniMap.cmdy = (float)((int)MiniMap.cmdy & 15);
			}
			if (MiniMap.cmx != MiniMap.cmtoX)
			{
				MiniMap.cmvx = (float)((int)(MiniMap.cmtoX - MiniMap.cmx) << 2);
				MiniMap.cmdx += MiniMap.cmvx;
				MiniMap.cmx += (float)((int)MiniMap.cmdx >> 4);
				MiniMap.cmdx = (float)((int)MiniMap.cmdx & 15);
			}
		}
		if (MiniMap.cmtoY < 0f || MiniMap.cmy < 0f)
		{
			MiniMap.cmtoY = (MiniMap.cmy = 0f);
		}
		if (MiniMap.cmtoY > MiniMap.cmyLim || MiniMap.cmy > MiniMap.cmyLim)
		{
			MiniMap.cmtoY = (MiniMap.cmy = MiniMap.cmyLim);
		}
		if (MiniMap.cmtoX < 0f || MiniMap.cmx < 0f)
		{
			MiniMap.cmtoX = (MiniMap.cmx = 0f);
		}
		if (MiniMap.cmtoX > MiniMap.cmxLim || MiniMap.cmx > MiniMap.cmxLim)
		{
			MiniMap.cmtoX = (MiniMap.cmx = MiniMap.cmxLim);
		}
		for (int j = 0; j < MiniMap.listClound.size(); j++)
		{
			AvPosition avPosition = (AvPosition)MiniMap.listClound.elementAt(j);
			avPosition.x -= avPosition.anchor + ((Canvas.gameTick % 5 != 1) ? 0 : 1);
			if (avPosition.x < -this.x - 50)
			{
				avPosition.x = this.x + CRes.rnd(4) * 50 + (int)this.wMini * (int)this.wSmall;
				avPosition.y = CRes.rnd(10) * ((int)this.hMini * (int)this.wSmall / 10) + 10;
				avPosition.anchor = CRes.rnd(2);
			}
		}
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0005ECD0 File Offset: 0x0005D0D0
	public override void updateKey()
	{
		this.count += 1L;
		base.updateKey();
		if (Canvas.isPointer(0, 0, Canvas.w, Canvas.h))
		{
			int num = Canvas.dx();
			int num2 = Canvas.dy();
			if (Canvas.isPointerClick)
			{
				this.ableTrans = true;
				Canvas.isPointerClick = false;
				for (int i = 0; i < this.listPos.size(); i++)
				{
					PositionMap positionMap = (PositionMap)this.listPos.elementAt(i);
					if (Canvas.isPointer((int)((float)(this.x + positionMap.x * (int)this.wSmall + (int)this.wSmall / 2) - MiniMap.cmx - (float)(48 * AvMain.hd / 2)), (int)((float)(this.y + positionMap.y * (int)this.wSmall + (int)this.wSmall / 2) - MiniMap.cmy - (float)(56 * AvMain.hd)), 48 * AvMain.hd, 56 * AvMain.hd))
					{
						this.selected = i;
						this.isHide = false;
						return;
					}
				}
			}
			if (this.ableTrans && Canvas.isPointerDown)
			{
				if (CRes.abs(num) >= 20 && CRes.abs(num2) >= 20)
				{
					this.isHide = true;
				}
				if (Canvas.gameTick % 3 == 0)
				{
					this.dyTran = Canvas.py;
					this.dxTran = Canvas.px;
					this.timePointY = this.count;
				}
				this.vY = 0;
				this.vX = 0;
				if (!this.trans)
				{
					this.trans = true;
					this.pa = MiniMap.cmx;
					this.pb = MiniMap.cmy;
				}
				MiniMap.cmtoY = this.pb + (float)num2;
				MiniMap.cmtoX = this.pa + (float)num;
				this.setLimit();
				MiniMap.cmy = MiniMap.cmtoY;
				MiniMap.cmx = MiniMap.cmtoX;
			}
			if (this.ableTrans && Canvas.isPointerRelease)
			{
				int num3 = (int)(this.count - this.timePointY);
				int num4 = this.dyTran - Canvas.py;
				if (num3 < 10)
				{
					if (MiniMap.cmtoY >= 0f && MiniMap.cmtoY < MiniMap.cmyLim)
					{
						this.vY = num4 / num3 * 10;
					}
					int num5 = this.dxTran - Canvas.px;
					if (MiniMap.cmtoX >= 0f && MiniMap.cmtoX < MiniMap.cmxLim)
					{
						this.vX = num5 / num3 * 10;
					}
				}
				this.timePointY = -1L;
				this.trans = false;
				this.ableTrans = false;
				if (CRes.abs(num) < 20 && CRes.abs(num2) < 20)
				{
					PositionMap positionMap2 = (PositionMap)this.listPos.elementAt(this.selected);
					if (Canvas.isPointer((int)((float)(this.x + positionMap2.x * (int)this.wSmall + (int)this.wSmall / 2) - MiniMap.cmx - (float)(48 * AvMain.hd / 2)), (int)((float)(this.y + positionMap2.y * (int)this.wSmall + (int)this.wSmall / 2) - MiniMap.cmy - (float)(56 * AvMain.hd)), 48 * AvMain.hd, 56 * AvMain.hd))
					{
						this.cmdCenter.perform();
						return;
					}
				}
			}
		}
		if (this.cmdUpdateKey != null)
		{
			this.cmdUpdateKey.perform();
		}
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0005F044 File Offset: 0x0005D444
	private void tran()
	{
		PositionMap positionMap = (PositionMap)this.listPos.elementAt(this.selected);
		MiniMap.cmtoX = (float)(positionMap.x * (int)this.wSmall - Canvas.w / 2);
		MiniMap.cmtoY = (float)(positionMap.y * (int)this.wSmall - Canvas.hCan / 2);
		this.setLimit();
		if (Canvas.isZoom)
		{
			MiniMap.cmx = MiniMap.cmtoX;
			MiniMap.cmy = MiniMap.cmtoY;
		}
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0005F0C4 File Offset: 0x0005D4C4
	private void setLimit()
	{
		if (MiniMap.cmtoY < 0f)
		{
			MiniMap.cmtoY = 0f;
		}
		if (MiniMap.cmtoY > MiniMap.cmyLim)
		{
			MiniMap.cmtoY = MiniMap.cmyLim;
		}
		if (MiniMap.cmtoX < 0f)
		{
			MiniMap.cmtoX = 0f;
		}
		if (MiniMap.cmtoX > MiniMap.cmxLim)
		{
			MiniMap.cmtoX = MiniMap.cmxLim;
		}
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0005F135 File Offset: 0x0005D535
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		Canvas.resetTrans(g);
		if (Canvas.welcome == null || !Welcome.isPaintArrow)
		{
			base.paint(g);
		}
		Canvas.paintPlus(g);
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0005F168 File Offset: 0x0005D568
	public override void paintMain(MyGraphics g)
	{
		Canvas.resetTrans(g);
		g.translate((float)this.x, (float)this.y);
		g.translate(-MiniMap.cmx, -MiniMap.cmy);
		if (MiniMap.imgCreateMap != null)
		{
			g.drawImage(MiniMap.imgCreateMap, 0f, 0f, 0);
		}
		else
		{
			for (int i = 0; i < this.map.Length; i++)
			{
				MiniMap.imgMap.drawFrameXY((int)this.map[i], i % (int)this.wMini * (int)this.wSmall, i / (int)this.wMini * (int)this.wSmall, 0, g);
			}
		}
		for (int j = 0; j < this.listPos.size(); j++)
		{
			PositionMap positionMap = (PositionMap)this.listPos.elementAt(j);
			if (j == this.selected && !this.isHide)
			{
				g.drawImage(MiniMap.imgBackIcon, (float)(positionMap.x * (int)this.wSmall + (int)this.wSmall / 2), (float)(positionMap.y * (int)this.wSmall), 33);
				if (MiniMap.isCityMap)
				{
					MiniMap.imgPopup.drawFrame(j, positionMap.x * (int)this.wSmall + (int)this.wSmall / 2, positionMap.y * (int)this.wSmall - 12 * AvMain.hd, 0, 33, g);
				}
				else
				{
					AvatarData.paintImg(g, (int)positionMap.idImg, positionMap.x * (int)this.wSmall + (int)this.wSmall / 2, positionMap.y * (int)this.wSmall - 12 * AvMain.hd, 33);
				}
			}
			else
			{
				g.drawImage(MiniMap.imgSmallIcon, (float)(positionMap.x * (int)this.wSmall + (int)this.wSmall / 2), (float)(positionMap.y * (int)this.wSmall - positionMap.count / 3), 33);
			}
		}
		this.paintName(g);
		this.paintClound(g);
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0005F36C File Offset: 0x0005D76C
	private void paintName(MyGraphics g)
	{
		for (int i = 0; i < this.listPos.size(); i++)
		{
			PositionMap positionMap = (PositionMap)this.listPos.elementAt(i);
			float num = (float)(positionMap.x * (int)this.wSmall);
			float num2 = (float)(positionMap.y * (int)this.wSmall);
			if (num2 < MiniMap.cmy + 50f)
			{
				num2 = MiniMap.cmy + 50f;
			}
			if (num2 > MiniMap.cmy + (float)Canvas.hCan - 20f)
			{
				num2 = MiniMap.cmy + (float)Canvas.hCan - 20f;
			}
			if (num < MiniMap.cmx + 20f)
			{
				num = MiniMap.cmx + 20f;
			}
			if (num > MiniMap.cmx + (float)Canvas.w - 47f)
			{
				num = MiniMap.cmx + (float)Canvas.w - 47f;
			}
			Canvas.borderFont.drawString(g, positionMap.text, (int)num + 10, (int)num2 - ((i != this.selected || this.isHide) ? (35 * AvMain.hd) : (70 * AvMain.hd)) - positionMap.count / 6, 2);
		}
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x0005F4A8 File Offset: 0x0005D8A8
	private void paintClound(MyGraphics g)
	{
		int num = MiniMap.listClound.size();
		for (int i = 0; i < num; i++)
		{
			AvPosition avPosition = (AvPosition)MiniMap.listClound.elementAt(i);
			if ((float)avPosition.x > MiniMap.cmx - 30f && (float)avPosition.x < MiniMap.cmx + 30f + (float)Canvas.w && (float)avPosition.y > MiniMap.cmy - 20f && (float)avPosition.y < MiniMap.cmy + 20f + (float)Canvas.h)
			{
				g.drawImage(this.imgClound[avPosition.anchor], (float)avPosition.x, (float)avPosition.y, 3);
			}
		}
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0005F570 File Offset: 0x0005D970
	public void onRegisterByEmail(sbyte step, string des, string name, string pass)
	{
		Out.println("onRegisterByEmail: " + name + "   " + pass);
		if ((int)step == 0)
		{
			MiniMap.actionReg = new MiniMap.IActionRequestReg(des);
		}
		else if ((int)step == 1)
		{
			MiniMap.actionReg = new MiniMap.IActionRequestOK(des);
		}
		else if ((int)step == 2)
		{
			LoginScr.gI().tfUser.setText(name);
			LoginScr.gI().tfPass.setText(pass);
			LoginScr.gI().saveLogin();
			Canvas.startOKDlg("Đăng ký thành công.");
			MiniMap.actionReg = null;
		}
	}

	// Token: 0x04000C5E RID: 3166
	public static MiniMap me;

	// Token: 0x04000C5F RID: 3167
	public static FrameImage imgMap;

	// Token: 0x04000C60 RID: 3168
	public static FrameImage imgArrow;

	// Token: 0x04000C61 RID: 3169
	public sbyte[] map;

	// Token: 0x04000C62 RID: 3170
	public MyVector listPos;

	// Token: 0x04000C63 RID: 3171
	public sbyte wMini;

	// Token: 0x04000C64 RID: 3172
	public sbyte hMini;

	// Token: 0x04000C65 RID: 3173
	public sbyte wSmall = 16;

	// Token: 0x04000C66 RID: 3174
	public int x;

	// Token: 0x04000C67 RID: 3175
	public int y;

	// Token: 0x04000C68 RID: 3176
	public static Image imgSmallIcon;

	// Token: 0x04000C69 RID: 3177
	public static Image imgBackIcon;

	// Token: 0x04000C6A RID: 3178
	public new int selected;

	// Token: 0x04000C6B RID: 3179
	public static float cmtoX;

	// Token: 0x04000C6C RID: 3180
	public static float cmx;

	// Token: 0x04000C6D RID: 3181
	public static float cmdx;

	// Token: 0x04000C6E RID: 3182
	public static float cmvx;

	// Token: 0x04000C6F RID: 3183
	public static float cmxLim;

	// Token: 0x04000C70 RID: 3184
	public static float cmtoY;

	// Token: 0x04000C71 RID: 3185
	public static float cmy;

	// Token: 0x04000C72 RID: 3186
	public static float cmdy;

	// Token: 0x04000C73 RID: 3187
	public static float cmvy;

	// Token: 0x04000C74 RID: 3188
	public static float cmyLim;

	// Token: 0x04000C75 RID: 3189
	public MyScreen lastScr;

	// Token: 0x04000C76 RID: 3190
	public IAction cmdUpdateKey;

	// Token: 0x04000C77 RID: 3191
	private bool trans;

	// Token: 0x04000C78 RID: 3192
	private new bool isHide;

	// Token: 0x04000C79 RID: 3193
	public static bool isCityMap = false;

	// Token: 0x04000C7A RID: 3194
	public static bool isChange = true;

	// Token: 0x04000C7B RID: 3195
	private Image[] imgClound;

	// Token: 0x04000C7C RID: 3196
	private static MyVector listClound = new MyVector();

	// Token: 0x04000C7D RID: 3197
	public static FrameImage imgPopup;

	// Token: 0x04000C7E RID: 3198
	public static FrameImage imgPopupName;

	// Token: 0x04000C7F RID: 3199
	public static string nameSV = string.Empty;

	// Token: 0x04000C80 RID: 3200
	private Command cmdCenter;

	// Token: 0x04000C81 RID: 3201
	public static Image imgCreateMap;

	// Token: 0x04000C82 RID: 3202
	private int vY;

	// Token: 0x04000C83 RID: 3203
	private int vX;

	// Token: 0x04000C84 RID: 3204
	private float pa;

	// Token: 0x04000C85 RID: 3205
	private float pb;

	// Token: 0x04000C86 RID: 3206
	public bool ableTrans;

	// Token: 0x04000C87 RID: 3207
	private int dyTran;

	// Token: 0x04000C88 RID: 3208
	private int dxTran;

	// Token: 0x04000C89 RID: 3209
	private long timePointY;

	// Token: 0x04000C8A RID: 3210
	private long count;

	// Token: 0x04000C8B RID: 3211
	public static IAction actionReg;

	// Token: 0x04000C8C RID: 3212
	public static byte iRequestReg;

	// Token: 0x0200016D RID: 365
	private class IActionRequestOK : IAction
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x0005F628 File Offset: 0x0005DA28
		public IActionRequestOK(string des)
		{
			this.des = des;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0005F637 File Offset: 0x0005DA37
		public void perform()
		{
			Canvas.startOK(this.des, new MiniMap.IActionYesRef());
		}

		// Token: 0x04000C8D RID: 3213
		private string des;
	}

	// Token: 0x0200016E RID: 366
	private class IActionRequestReg : IAction
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x0005F649 File Offset: 0x0005DA49
		public IActionRequestReg(string des)
		{
			this.des = des;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0005F658 File Offset: 0x0005DA58
		public void perform()
		{
			Canvas.startOKDlg(this.des, new MiniMap.IActionYesRef());
		}

		// Token: 0x04000C8E RID: 3214
		private string des;
	}

	// Token: 0x0200016F RID: 367
	private class IActionYesRef : IAction
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x0005F674 File Offset: 0x0005DA74
		public void perform()
		{
			TField[] array = new TField[4];
			for (int i = 0; i < 4; i++)
			{
				array[i] = new TField(string.Empty, Canvas.currentMyScreen, new MiniMap.IActionOkReg(array));
				array[i].autoScaleScreen = true;
				array[i].showSubTextField = false;
			}
			array[0].setFocus(true);
			array[0].setIputType(0);
			array[1].setIputType(2);
			array[2].setIputType(2);
			array[3].setIputType(0);
			string[][] subName = new string[][]
			{
				new string[]
				{
					"Tên:",
					string.Empty
				},
				new string[]
				{
					"Mật khẩu:",
					string.Empty
				},
				new string[]
				{
					"Nhập lại",
					"mật khẩu:"
				},
				new string[]
				{
					"Số di động",
					"hoặc email:"
				}
			};
			InputFace.gI().setInfo(array, "Đăng Ký", subName, new Command(T.finish, new MiniMap.IActionOkReg(array)), Canvas.hCan);
			InputFace.gI().show();
		}
	}

	// Token: 0x02000170 RID: 368
	private class IActionOkReg : IAction
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x0005F78C File Offset: 0x0005DB8C
		public IActionOkReg(TField[] tf)
		{
			this.tf = tf;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0005F79C File Offset: 0x0005DB9C
		public void perform()
		{
			if (this.tf[0].getText().Equals(string.Empty))
			{
				Canvas.startOKDlg("Bạn chưa nhập tên");
				return;
			}
			if (this.tf[1].getText().Equals(string.Empty) || this.tf[2].getText().Equals(string.Empty))
			{
				Canvas.startOKDlg("Bạn chưa nhập mật khẩu");
				return;
			}
			if (!this.tf[1].getText().Equals(this.tf[2].getText()))
			{
				Canvas.startOKDlg("Hai mật khẩu không giống nhau");
				return;
			}
			Canvas.currentFace = null;
			GlobalService.gI().doRegisterByEmail(this.tf[0].getText(), this.tf[1].getText(), this.tf[3].getText());
		}

		// Token: 0x04000C8F RID: 3215
		private TField[] tf;
	}
}
