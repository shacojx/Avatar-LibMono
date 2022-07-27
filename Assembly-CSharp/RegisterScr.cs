using System;

// Token: 0x02000189 RID: 393
public class RegisterScr : MyScreen
{
	// Token: 0x06000A57 RID: 2647 RVA: 0x00066CE1 File Offset: 0x000650E1
	public static RegisterScr gI()
	{
		if (RegisterScr.instance == null)
		{
			RegisterScr.instance = new RegisterScr();
		}
		return RegisterScr.instance;
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x00066CFC File Offset: 0x000650FC
	public override void commandTab(int index)
	{
		if (index == 0)
		{
			this.doFinish();
		}
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00066D14 File Offset: 0x00065114
	public override void switchToMe()
	{
		GameMidlet.avatar.seriPart = new MyVector();
		GameMidlet.avatar.direct = Base.RIGHT;
		this.getAvatarPart();
		this.center = new Command(T.success, 0);
		SeriPart seriPart = new SeriPart();
		int i = CRes.r.nextInt(this.listQ.size());
		seriPart.idPart = ((APartInfo)this.listQ.elementAt(i)).IDPart;
		GameMidlet.avatar.addSeri(seriPart);
		SeriPart seriPart2 = new SeriPart();
		int i2 = CRes.r.nextInt(this.listClothing.size());
		seriPart2.idPart = ((APartInfo)this.listClothing.elementAt(i2)).IDPart;
		GameMidlet.avatar.addSeri(seriPart2);
		SeriPart seriPart3 = new SeriPart();
		seriPart3.idPart = 4;
		GameMidlet.avatar.addSeri(seriPart3);
		SeriPart seriPart4 = new SeriPart();
		int i3 = CRes.r.nextInt(this.listHair.size());
		seriPart4.idPart = ((APartInfo)this.listHair.elementAt(i3)).IDPart;
		GameMidlet.avatar.addSeri(seriPart4);
		GameMidlet.avatar.addSeri(new SeriPart(0));
		GameMidlet.avatar.orderSeriesPath();
		this.init();
		Canvas.paint.initReg();
		base.switchToMe();
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x00066E6F File Offset: 0x0006526F
	public override void doMenu()
	{
		LoginScr.gI().doMenu();
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00066E7B File Offset: 0x0006527B
	public void init()
	{
		PaintPopup.gI().setInfo(T.createChar, 150 * AvMain.hd, 170 + ((AvMain.hd != 2) ? 0 : 160), 1, -1, null, null);
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00066EB8 File Offset: 0x000652B8
	public void getAvatarPart()
	{
		GameMidlet.avatar.gender = this.male;
		if (this.listHair != null)
		{
			this.listHair.removeAllElements();
			this.listClothing.removeAllElements();
			this.listQ.removeAllElements();
		}
		this.listHair = new MyVector();
		this.listClothing = new MyVector();
		this.listQ = new MyVector();
		for (int i = 0; i < AvatarData.listPart.Length; i++)
		{
			if (AvatarData.listPart[i] is APartInfo)
			{
				APartInfo apartInfo = (APartInfo)AvatarData.listPart[i];
				if (apartInfo != null && ((int)apartInfo.gender == (int)this.male || (int)apartInfo.gender == 0) && (int)apartInfo.level == 0)
				{
					if ((int)apartInfo.zOrder == 50)
					{
						this.listHair.addElement(apartInfo);
					}
					else if ((int)apartInfo.zOrder == 20)
					{
						this.listClothing.addElement(apartInfo);
					}
					else if ((int)apartInfo.zOrder == 10)
					{
						this.listQ.addElement(apartInfo);
					}
				}
			}
		}
		this.selected = 0;
		this.getId();
		GameMidlet.avatar.orderSeriesPath();
		if ((int)GameMidlet.avatar.action != 10)
		{
			GameMidlet.avatar.setAction(1);
		}
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x00067015 File Offset: 0x00065415
	protected void doFinish()
	{
		Canvas.isInitChar = true;
		Canvas.startWaitDlg(T.createChar + "...");
		GlobalService.gI().doRequestCreCharacter();
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0006703B File Offset: 0x0006543B
	public override void keyPress(int keyCode)
	{
		base.keyPress(keyCode);
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x00067044 File Offset: 0x00065444
	public override void update()
	{
		if (this.countLeft > 0)
		{
			this.countLeft--;
		}
		if (this.countRight > 0)
		{
			this.countRight--;
		}
		this.time++;
		if (this.time > 50)
		{
			this.time = 0;
			int num = CRes.r.nextInt(3);
			if ((int)GameMidlet.avatar.action != 10)
			{
				if (num == 0)
				{
					GameMidlet.avatar.setAction(1);
				}
				else
				{
					GameMidlet.avatar.setAction(0);
				}
			}
		}
		GameMidlet.avatar.updateAvatar();
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x000670F0 File Offset: 0x000654F0
	public void setKeyUpDown(int ind)
	{
		this.index = ind;
		if (this.index < 0)
		{
			this.index = 1;
		}
		if (this.index > 1)
		{
			this.index = 0;
		}
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x00067120 File Offset: 0x00065520
	public void setKeyLeftRight(int ind)
	{
		this.selected += ind;
		if (this.selected < 0)
		{
			this.selected = 1;
		}
		if (this.selected > 1)
		{
			this.selected = 0;
		}
		if (this.index == 0)
		{
			if ((int)this.male == 1)
			{
				this.male = 2;
			}
			else
			{
				this.male = 1;
			}
			this.getAvatarPart();
		}
		else
		{
			this.getId();
		}
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0006719D File Offset: 0x0006559D
	public override void updateKey()
	{
		Canvas.paint.updateKeyRegister();
		base.updateKey();
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x000671B0 File Offset: 0x000655B0
	private void getId()
	{
		for (int i = 0; i < GameMidlet.avatar.seriPart.size(); i++)
		{
			SeriPart seriPart = (SeriPart)GameMidlet.avatar.seriPart.elementAt(i);
			APartInfo apartInfo = (APartInfo)AvatarData.getPart(seriPart.idPart);
			if ((int)apartInfo.zOrder == 50 && this.listHair.size() != 0 && this.selected < this.listHair.size())
			{
				seriPart.idPart = ((APartInfo)this.listHair.elementAt(this.selected)).IDPart;
			}
			if ((int)apartInfo.zOrder == 20 && this.listClothing.size() != 0 && this.selected < this.listClothing.size())
			{
				seriPart.idPart = ((APartInfo)this.listClothing.elementAt(this.selected)).IDPart;
			}
			if ((int)apartInfo.zOrder == 10 && this.listQ.size() != 0 && this.selected < this.listQ.size())
			{
				seriPart.idPart = ((APartInfo)this.listQ.elementAt(this.selected)).IDPart;
			}
		}
		GameMidlet.avatar.orderSeriesPath();
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x00067310 File Offset: 0x00065710
	public override void paint(MyGraphics g)
	{
		Canvas.loadMap.paint(g);
		Canvas.loadMap.paintObject(g);
		Canvas.resetTrans(g);
		PaintPopup.gI().paint(g);
		Canvas.resetTrans(g);
		g.translate((float)PaintPopup.gI().x, (float)PaintPopup.gI().y);
		Canvas.paint.paintPlayer(g, this.index, (int)this.male, this.countLeft, this.countRight);
		base.paint(g);
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x00067391 File Offset: 0x00065791
	public void onCreaCharacter(bool isCreaCha)
	{
		Canvas.endDlg();
		if (isCreaCha)
		{
			MapScr.gI().joinCitymap();
			return;
		}
		Canvas.startOKDlg(T.createCharFail);
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x000673B3 File Offset: 0x000657B3
	public void onRegister(string userName, string pass)
	{
		Canvas.user = userName;
		Canvas.pass = pass;
		AvatarData.saveMyAccount();
		GlobalMessageHandler.gI().miniGameMessageHandler = null;
		ServerListScr.gI().login();
	}

	// Token: 0x04000D8F RID: 3471
	public static RegisterScr instance;

	// Token: 0x04000D90 RID: 3472
	public static bool isCreateChar;

	// Token: 0x04000D91 RID: 3473
	public sbyte male = 1;

	// Token: 0x04000D92 RID: 3474
	public int index;

	// Token: 0x04000D93 RID: 3475
	public new int selected;

	// Token: 0x04000D94 RID: 3476
	public int countLeft;

	// Token: 0x04000D95 RID: 3477
	public int countRight;

	// Token: 0x04000D96 RID: 3478
	private MyVector listHair;

	// Token: 0x04000D97 RID: 3479
	private MyVector listClothing;

	// Token: 0x04000D98 RID: 3480
	private MyVector listQ;

	// Token: 0x04000D99 RID: 3481
	private int time;

	// Token: 0x0200018A RID: 394
	private class IActionOkUser : IAction
	{
		// Token: 0x06000A69 RID: 2665 RVA: 0x000673E5 File Offset: 0x000657E5
		public void perform()
		{
			RegisterScr.instance.doFinish();
		}
	}
}
