using System;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class MainMenu : MyScreen
{
	// Token: 0x0600083D RID: 2109 RVA: 0x0004FE5C File Offset: 0x0004E25C
	public MainMenu()
	{
		this.cmdCenter = new Command(T.selectt, 1);
		this.wGo = 23;
		if (Canvas.stypeInt > 0)
		{
			this.wGo = 60 * Canvas.stypeInt;
		}
		MainMenu.imgLoading = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/loading"), 24 * AvMain.hd, 24 * AvMain.hd);
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x0004FEFE File Offset: 0x0004E2FE
	public static MainMenu gI()
	{
		return (MainMenu.me != null) ? MainMenu.me : (MainMenu.me = new MainMenu());
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0004FF1F File Offset: 0x0004E31F
	public override void switchToMe()
	{
		if (Canvas.currentMyScreen != this)
		{
			this.lastScr = Canvas.currentMyScreen;
		}
		base.switchToMe();
		this.isHide = true;
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x0004FF44 File Offset: 0x0004E344
	public void initCmd()
	{
		this.isAble = true;
		this.cmdCenter = new Command(T.selectt, 1);
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x0004FF60 File Offset: 0x0004E360
	public override void commandTab(int index)
	{
		if (index != 0)
		{
			if (index == 1)
			{
				this.closeGo();
				Command command = (Command)this.list.elementAt(this.selected);
				if (command.action != null)
				{
					command.action.perform();
				}
				else
				{
					this.commandAction((int)command.indexMenu);
				}
			}
		}
		else
		{
			this.closeGo();
		}
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x0004FFD4 File Offset: 0x0004E3D4
	public override void commandAction(int index)
	{
		switch (index)
		{
		case 3:
			this.doExchange();
			break;
		case 6:
			this.isWearing = false;
			GlobalService.gI().doRequestContainer(GameMidlet.avatar.IDDB);
			break;
		case 7:
			MapScr.gI().doRequestAddFriend(MapScr.focusP);
			break;
		case 8:
			GlobalService.gI().requestShop(26);
			Canvas.startWaitDlg();
			break;
		case 9:
			MapScr.gI().doHit();
			break;
		case 10:
			MapScr.gI().doKiss();
			break;
		case 11:
			MapScr.gI().doRequestYourInfo();
			break;
		case 12:
			if (MapScr.focusP != null)
			{
				MessageScr.gI().startChat(MapScr.focusP);
			}
			break;
		case 13:
			MapScr.gI().doInviteToMyHome();
			break;
		case 16:
		{
			MyVector myVector = new MyVector();
			if (MapScr.listCmdRotate.size() > 0)
			{
				for (int i = 0; i < MapScr.listCmdRotate.size(); i++)
				{
					StringObj stringObj = (StringObj)MapScr.listCmdRotate.elementAt(i);
					if (stringObj.type == 1)
					{
						myVector.addElement(new MainMenu.CommandExchange(stringObj.str, new MainMenu.IActionExchange(stringObj), stringObj));
					}
				}
			}
			this.setInfo(myVector);
			break;
		}
		case 17:
			MapScr.isOpenInfo = true;
			MapScr.gI().doRequestYourInfo();
			break;
		}
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x00050160 File Offset: 0x0004E560
	public void doWearing()
	{
		if (Canvas.currentMyScreen == MainMenu.me)
		{
			return;
		}
		PopupShop.gI().isFull = true;
		PopupShop.gI().addElement(new string[]
		{
			T.wearing,
			T.container
		}, new MyVector[]
		{
			MapScr.gI().getListYourPart(GameMidlet.avatar, 0, true),
			MapScr.gI().getListCmdDoUsing(GameMidlet.listContainer, GameMidlet.avatar.IDDB, 1, T.use, true)
		}, null, null);
		PopupShop.gI().setCmdLeft(MapScr.gI().cmdDellPart(GameMidlet.avatar.seriPart, 0, 0, false), 0);
		PopupShop.gI().setCmdLeft(MapScr.gI().cmdDellPart(GameMidlet.listContainer, 1, 0, true), 1);
		PopupShop.gI().setCmdRight(new Command(T.update, new MainMenu.IActionUpdateContainerYesNo()), 1);
		PopupShop.gI().setTap(1);
		if (Canvas.currentMyScreen != PopupShop.gI())
		{
			PopupShop.gI().switchToMe();
			PopupShop.gI().setHorizontal(true);
			PopupShop.isQuaTrang = true;
			PopupShop.gI().setCmyLim();
		}
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x00050280 File Offset: 0x0004E680
	public void closeGo()
	{
		this.lastScr.switchToMe();
		if (this.isCircle)
		{
			for (int i = 0; i < this.listObj.size(); i++)
			{
				AvPosition avPosition = (AvPosition)this.listObj.elementAt(i);
				Avatar avatar = LoadMap.getAvatar(avPosition.anchor);
				if (avatar != null)
				{
					avatar.ableShow = false;
				}
			}
			this.listObj = null;
		}
		else if (MapScr.focusP != null)
		{
			MapScr.focusP.ableShow = false;
		}
		this.indexCircle = -1;
		this.indexTemp = -1;
		this.isCircle = false;
		MainMenu.popFocus = null;
		this.center = null;
		this.trans = false;
		this.isTran = false;
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x0005033C File Offset: 0x0004E73C
	public void setInfo(MyVector list)
	{
		this.list = list;
		this.wSmall = 40 * AvMain.hd + (AvMain.hd - 1) * 30;
		if (Canvas.stypeInt == 1 && Canvas.w > 300)
		{
			this.wSmall += 20;
		}
		this.disSmall = this.wSmall + 2 * AvMain.hd;
		for (int i = 0; i < list.size(); i++)
		{
			Command command = (Command)list.elementAt(i);
			int num = Canvas.smallFontRed.getWidth(command.caption) + 10;
			if (num > this.disSmall)
			{
				this.disSmall = num;
			}
		}
		this.x = 0;
		this.numW = Canvas.w / this.disSmall;
		if (list.size() * this.disSmall < Canvas.w)
		{
			this.x = (Canvas.w - list.size() * this.disSmall) / 2;
		}
		else
		{
			this.x = 5;
		}
		if (this.selected >= list.size())
		{
			this.selected = 0;
		}
		this.isCircle = false;
		if (MapScr.focusP != null)
		{
			MapScr.focusP.ableShow = true;
		}
		this.y = Canvas.hCan - 30 * AvMain.hd - this.disSmall / 2;
		if (this.avaPaint != null && LoadMap.focusObj != null)
		{
			this.xCenter = (int)((float)(LoadMap.focusObj.x * AvMain.hd) - AvCamera.gI().xCam);
			this.yCenter = (int)((float)(LoadMap.focusObj.y * AvMain.hd) - AvCamera.gI().yCam);
		}
		else
		{
			this.xCenter = (int)((float)(GameMidlet.avatar.x * AvMain.hd) - AvCamera.gI().xCam);
			this.yCenter = (int)((float)(GameMidlet.avatar.y * AvMain.hd) - AvCamera.gI().yCam);
		}
		this.maxRadius = 60 * AvMain.hd;
		if (AvMain.zoom > 1f)
		{
			this.maxRadius += (int)(30f * AvMain.zoom);
		}
		if ((float)this.yCenter * AvMain.zoom > (float)(Canvas.hCan - this.maxRadius - 25 * AvMain.hd))
		{
			this.yCenter = (int)((float)(Canvas.hCan - this.maxRadius - 25 * AvMain.hd) / AvMain.zoom);
		}
		if ((float)this.xCenter * AvMain.zoom < (float)(this.maxRadius + 25 * AvMain.hd))
		{
			this.xCenter = (int)((float)(this.maxRadius + 25 * AvMain.hd) / AvMain.zoom);
		}
		else if ((float)this.xCenter * AvMain.zoom > (float)(Canvas.w - this.maxRadius - 25 * AvMain.hd))
		{
			this.xCenter = (int)((float)(Canvas.w - this.maxRadius - 25 * AvMain.hd) / AvMain.zoom);
		}
		this.distant = (float)(20 * AvMain.hd);
		this.v = 2f;
		Canvas.isPointerClick = false;
		Canvas.isPointerRelease = false;
		Canvas.isPointerDown = false;
		this.switchToMe();
		if (Canvas.stypeInt == 0)
		{
			this.center = this.cmdCenter;
		}
		this.isName = false;
		this.disX = Canvas.w - this.x * 2;
		this.cmxLim = list.size() * this.disSmall - this.disX;
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x000506CC File Offset: 0x0004EACC
	public override void update()
	{
		try
		{
			this.lastScr.update();
			if (this.timeOpen > 0)
			{
				this.timeOpen--;
				if (this.timeOpen == 0)
				{
					this.click();
				}
			}
			if (!this.isGO)
			{
				if (this.isCircle)
				{
					for (int i = 0; i < this.listObj.size(); i++)
					{
						AvPosition avPosition = (AvPosition)this.listObj.elementAt(i);
						int num = this.maxW * CRes.cos(CRes.fixangle(this.angleCircle * i)) >> 10;
						int num2 = -(this.maxW * CRes.sin(CRes.fixangle(this.angleCircle * i))) >> 10;
						int num3 = this.x + num;
						int num4 = this.y + num2;
						if (CRes.distance(avPosition.x, avPosition.y, num3, num4) > 10 * AvMain.hd)
						{
							int num5 = CRes.angle(num3 - avPosition.x, -(num4 - avPosition.y));
							int num6 = 10 * AvMain.hd * CRes.cos(CRes.fixangle(num5)) >> 10;
							int num7 = -(10 * AvMain.hd * CRes.sin(CRes.fixangle(num5))) >> 10;
							avPosition.x += num6;
							avPosition.y += num7;
						}
						else
						{
							avPosition.x = num3;
							avPosition.y = num4;
						}
					}
					if (this.wCircle < this.maxW)
					{
						this.wCircle += 10 * AvMain.hd;
					}
				}
				else
				{
					if (this.distant < (float)this.maxRadius)
					{
						this.v += this.v / 2f;
						this.distant += this.v;
					}
					for (int j = 0; j < this.list.size(); j++)
					{
						Command command = (Command)this.list.elementAt(j);
						command.update();
					}
				}
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		this.moveCamera();
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00050920 File Offset: 0x0004ED20
	public void moveCamera()
	{
		if (this.vX != 0)
		{
			if (this.cmx < 0 || this.cmx > this.cmxLim)
			{
				this.vX -= this.vX / 4;
				this.cmx += this.vX / 20;
				if (this.vX / 10 <= 1)
				{
					this.vX = 0;
				}
			}
			if (this.cmx < 0)
			{
				if (this.cmx < -this.disX / 2)
				{
					this.cmx = -this.disX / 2;
					this.cmtoX = 0;
					this.vX = 0;
				}
			}
			else if (this.cmx > this.cmxLim)
			{
				if (this.cmx < this.cmxLim + this.disX / 2)
				{
					this.cmx = this.cmxLim + this.disX / 2;
					this.cmtoX = this.cmxLim;
					this.vX = 0;
				}
			}
			else
			{
				this.cmx += this.vX / 10;
			}
			this.cmtoX = this.cmx;
			this.vX -= this.vX / 10;
			if (this.vX / 10 == 0)
			{
				this.vX = 0;
			}
		}
		else if (this.cmx < 0)
		{
			this.cmtoX = 0;
		}
		else if (this.cmx > this.cmxLim)
		{
			this.cmtoX = this.cmxLim;
		}
		if (this.cmx != this.cmtoX)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 4;
			this.cmdx &= 15;
		}
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x00050B14 File Offset: 0x0004EF14
	public override void updateKey()
	{
		this.count += 1L;
		if (this.isCircle)
		{
			if (Canvas.isPointerClick)
			{
				Canvas.isPointerClick = false;
				this.isClick = true;
				for (int i = 0; i < this.list.size(); i++)
				{
					int num = this.wCircle * CRes.cos(CRes.fixangle(this.angleCircle * i)) >> 10;
					int num2 = -(this.wCircle * CRes.sin(CRes.fixangle(this.angleCircle * i))) >> 10;
					if (Canvas.isPoint((int)((float)(this.x + num) * AvMain.zoom - (float)(20 * AvMain.hd)), (int)((float)(this.y + num2) * AvMain.zoom - (float)(30 * AvMain.hd) + (float)Canvas.transTab), 40 * AvMain.hd, 50 * AvMain.hd))
					{
						this.indexCircle = i;
						this.trans = true;
						break;
					}
				}
			}
			if (this.trans)
			{
				if (this.indexCircle != -1 && Canvas.isPointerDown)
				{
					int num3 = this.wCircle * CRes.cos(CRes.fixangle(this.angleCircle * this.indexCircle)) >> 10;
					int num4 = -(this.wCircle * CRes.sin(CRes.fixangle(this.angleCircle * this.indexCircle))) >> 10;
					if (!Canvas.isPoint((int)((float)(this.x + num3) * AvMain.zoom - (float)(20 * AvMain.hd)), (int)((float)(this.y + num4) * AvMain.zoom - (float)(30 * AvMain.hd) + (float)Canvas.transTab), 40 * AvMain.hd, 50 * AvMain.hd))
					{
						this.indexCircle = -1;
					}
				}
				if (Canvas.isPointerRelease)
				{
					Canvas.isPointerRelease = false;
					this.trans = false;
					this.isClick = false;
					if (this.indexCircle != -1)
					{
						Command command = (Command)this.list.elementAt(this.indexCircle);
						this.closeGo();
						command.action.perform();
					}
				}
			}
			if (this.isClick && Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isClick = false;
				this.closeGo();
			}
		}
		else
		{
			if (Canvas.isPointerClick)
			{
				this.isClick = true;
				Canvas.isPointerClick = false;
				for (int j = 0; j < this.list.size(); j++)
				{
					int num5 = (int)(this.distant * (float)CRes.cos(CRes.fixangle(this.angle * j + this.angle / 2))) >> 10;
					int num6 = -(int)(this.distant * (float)CRes.sin(CRes.fixangle(this.angle * j + this.angle / 2))) >> 10;
					if (Canvas.isPointer((int)((float)this.xCenter * AvMain.zoom + (float)num5) - 15 * AvMain.hd, (int)((float)this.yCenter * AvMain.zoom + (float)num6) - 15 * AvMain.hd, 30 * AvMain.hd, 30 * AvMain.hd))
					{
						this.isTran = true;
						this.selected = j;
						this.indexTemp = j;
						this.timeDelay = this.count;
						break;
					}
				}
			}
			if (this.isTran)
			{
				long num7 = this.count - this.timeDelay;
				int a = Canvas.dx();
				int a2 = Canvas.dy();
				if (Canvas.isPointerDown)
				{
					if (this.indexTemp != -1)
					{
						int num8 = (int)(this.distant * (float)CRes.cos(CRes.fixangle(this.angle * this.indexTemp + this.angle / 2))) >> 10;
						int num9 = -(int)(this.distant * (float)CRes.sin(CRes.fixangle(this.angle * this.indexTemp + this.angle / 2))) >> 10;
						if (!Canvas.isPointer((int)((float)this.xCenter * AvMain.zoom + (float)num8) - 15 * AvMain.hd, (int)((float)this.yCenter * AvMain.zoom + (float)num9) - 15 * AvMain.hd, 30 * AvMain.hd, 30 * AvMain.hd))
						{
							this.indexTemp = -1;
						}
					}
					if (CRes.abs(a) >= 10 * AvMain.hd || CRes.abs(a2) > 10 * AvMain.hd)
					{
						this.isHide = true;
					}
					else if (num7 > 3L && num7 < 8L)
					{
						this.isHide = false;
					}
				}
				if (Canvas.isPointerRelease)
				{
					this.isClick = false;
					this.isTran = false;
					if (this.indexTemp != -1)
					{
						if (num7 <= 4L)
						{
							this.isHide = false;
							this.timeOpen = 5;
						}
						else if (!this.isHide)
						{
							this.click();
						}
					}
					Canvas.isPointerRelease = false;
				}
			}
			if (this.isClick && Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isClick = false;
				this.closeGo();
			}
		}
		base.updateKey();
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x00051020 File Offset: 0x0004F420
	private void click()
	{
		if (this.indexCircle != -1)
		{
			Command command = (Command)this.list.elementAt(this.indexCircle);
			command.perform();
		}
		else if (this.indexTemp != -1)
		{
			this.cmdCenter.perform();
		}
		this.indexTemp = (this.indexCircle = -1);
		this.trans = false;
		this.isClick = false;
		this.isHide = true;
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x00051098 File Offset: 0x0004F498
	public override void paint(MyGraphics g)
	{
		this.lastScr.paintMain(g);
		Canvas.resetTransNotZoom(g);
		Canvas.paint.paintTransBack(g);
		if (this.isCircle)
		{
			this.paintCircle(g);
		}
		else
		{
			this.paintNormal(g);
		}
		base.paint(g);
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x000510E8 File Offset: 0x0004F4E8
	private void paintCircle(MyGraphics g)
	{
		GUIUtility.ScaleAroundPivot(new Vector2(AvMain.zoom, AvMain.zoom), Vector2.zero);
		for (int i = 0; i < this.list.size(); i++)
		{
			Command command = (Command)this.list.elementAt(i);
			int num;
			int num2;
			if (i < this.listObj.size())
			{
				AvPosition avPosition = (AvPosition)this.listObj.elementAt(i);
				num = avPosition.x;
				num2 = avPosition.y;
			}
			else
			{
				int num3 = this.wCircle * CRes.cos(CRes.fixangle(this.angleCircle * i)) >> 10;
				int num4 = -(this.wCircle * CRes.sin(CRes.fixangle(this.angleCircle * i))) >> 10;
				num = this.x + num3;
				num2 = this.y + num4;
			}
			command.paint(g, num, num2);
			Canvas.smallFontYellow.drawString(g, command.caption, num, num2 - 25 * AvMain.hd - Canvas.arialFont.getHeight(), 2);
		}
		GUIUtility.ScaleAroundPivot(new Vector2(1f / AvMain.zoom, 1f / AvMain.zoom), Vector2.zero);
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x00051220 File Offset: 0x0004F620
	private void paintNormal(MyGraphics g)
	{
		if (this.lastScr != MiniMap.me)
		{
			GUIUtility.ScaleAroundPivot(new Vector2(AvMain.zoom, AvMain.zoom), Vector2.zero);
			if (MapScr.focusP != null && this.avaPaint != null)
			{
				MapScr.focusP.paintIcon(g, this.xCenter, this.yCenter + (int)((float)(35 * AvMain.hd / 2) + 5f * AvMain.zoom), false);
				Canvas.smallFontRed.drawString(g, MapScr.focusP.name, this.xCenter, this.yCenter - 35 * AvMain.hd / 2 - (int)AvMain.hSmall, 2);
			}
			if (this.avaPaint == null)
			{
				GameMidlet.avatar.paintIcon(g, this.xCenter, this.yCenter + 35 * AvMain.hd / 2, false);
				Canvas.smallFontRed.drawString(g, GameMidlet.avatar.name, this.xCenter, this.yCenter - 35 * AvMain.hd / 2 - (int)AvMain.hSmall, 2);
			}
			GUIUtility.ScaleAroundPivot(new Vector2(1f / AvMain.zoom, 1f / AvMain.zoom), Vector2.zero);
		}
		Canvas.resetTransNotZoom(g);
		for (int i = 0; i < this.list.size(); i++)
		{
			int num = (int)(this.distant * (float)CRes.cos(CRes.fixangle(this.angle * i + this.angle / 2))) >> 10;
			int num2 = -(int)(this.distant * (float)CRes.sin(CRes.fixangle(this.angle * i + this.angle / 2))) >> 10;
			Command command = (Command)this.list.elementAt(i);
			if (!this.isHide && i == this.selected)
			{
				Menu.imgBackIcon.drawFrame(1, (int)((float)this.xCenter * AvMain.zoom + (float)num), (int)((float)this.yCenter * AvMain.zoom + (float)num2), 0, 3, g);
			}
			else
			{
				Menu.imgBackIcon.drawFrame(0, (int)((float)this.xCenter * AvMain.zoom + (float)num), (int)((float)this.yCenter * AvMain.zoom + (float)num2), 0, 3, g);
			}
			Canvas.smallFontYellow.drawString(g, command.caption, (int)((float)this.xCenter * AvMain.zoom + (float)num), (int)((float)this.yCenter * AvMain.zoom + (float)num2 - (float)(20 * AvMain.hd) - (float)((int)AvMain.hSmall / 2)), 2);
			g.setColor(0);
			command.paint(g, (int)((float)this.xCenter * AvMain.zoom + (float)num), (int)((float)this.yCenter * AvMain.zoom + (float)num2));
		}
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x000514D0 File Offset: 0x0004F8D0
	public void showCircle(MyVector list2, MyVector listObj)
	{
		this.listObj = listObj;
		this.list = list2;
		this.isCircle = true;
		this.angleCircle = 360 / list2.size();
		this.wCircle = 5;
		this.x = (int)((float)LoadMap.posFocus.x - AvCamera.gI().xCam);
		this.y = (int)((float)LoadMap.posFocus.y - AvCamera.gI().yCam);
		if (this.x < this.maxW + 35 * AvMain.hd / 2)
		{
			this.x = this.maxW + 35 * AvMain.hd / 2;
		}
		else if ((float)this.x * AvMain.zoom > (float)Canvas.w - (float)this.maxW * AvMain.zoom - (float)(35 * AvMain.hd / 2) * AvMain.zoom)
		{
			this.x = (int)(((float)Canvas.w - (float)this.maxW * AvMain.zoom - (float)(35 * AvMain.hd / 2) * AvMain.zoom) / AvMain.zoom);
		}
		if ((float)this.y * AvMain.zoom < (float)this.maxW * AvMain.zoom + (float)(35 * AvMain.hd / 2) * AvMain.zoom)
		{
			this.y = this.maxW + 35 * AvMain.hd / 2;
		}
		if ((float)this.y * AvMain.zoom > (float)Canvas.hCan - (float)this.maxW * AvMain.zoom - (float)(35 * AvMain.hd / 2) * AvMain.zoom)
		{
			this.y = (int)(((float)Canvas.hCan - (float)this.maxW * AvMain.zoom - (float)(35 * AvMain.hd / 2) * AvMain.zoom) / AvMain.zoom);
		}
		this.switchToMe();
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x000516A4 File Offset: 0x0004FAA4
	public void doExchange()
	{
		if (MapScr.focusP == null)
		{
			return;
		}
		if (LoadMap.focusObj != null && (int)LoadMap.focusObj.catagory == 0 && ((Avatar)LoadMap.focusObj).IDDB == -100)
		{
			Canvas.startOKDlg("Bạn có muốn bất đầu lể cưới không ?", new MainMenu.IActionOkWedding());
			return;
		}
		this.isCircle = false;
		MyVector myVector = new MyVector();
		myVector.addElement(this.setCommandMenu(T.hit, 9, 13));
		myVector.addElement(this.setCommandMenu(T.privateMsg, 12, 2));
		myVector.addElement(this.setCommandMenu(T.addFriend, 7, 11));
		myVector.addElement(this.setCommandMenu(T.giveGift, 8, 12));
		myVector.addElement(this.setCommandMenu(T.kiss, 10, 21));
		myVector.addElement(this.setCommandMenu(T.inviteMyHouse, 13, 22));
		myVector.addElement(this.setCommandMenu(T.info, 17, 19));
		myVector.addElement(this.setCommandMenu(T.other, 16, 6));
		this.setInfo(myVector);
		this.isName = true;
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x000517BC File Offset: 0x0004FBBC
	public void doMenuMiniMap()
	{
		if (Canvas.welcome != null && Welcome.isPaintArrow)
		{
			return;
		}
		MyVector myVector = new MyVector();
		Command o = this.setCommandMenu(T.info, 18, 3);
		Command o2 = this.setCommandMenu(T.changePass, 19, 4);
		myVector.addElement(o);
		myVector.addElement(o2);
		Command o3 = this.setCommandMenu(T.otherGame, 20, 6);
		Command o4 = this.setCommandMenu(T.option, 21, 23);
		myVector.addElement(o3);
		myVector.addElement(o4);
		if (Canvas.currentMyScreen == PopupShop.gI())
		{
			return;
		}
		this.setInfo(myVector);
		this.avaPaint = null;
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0005185D File Offset: 0x0004FC5D
	public Command setCommandMenu(string text, int type, int index)
	{
		return new MainMenu.CommandMenu(text, type, index);
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00051867 File Offset: 0x0004FC67
	public Command setCommandMenu(string text, IAction action, int index)
	{
		return new MainMenu.CommandMenu(text, action, index);
	}

	// Token: 0x04000A93 RID: 2707
	public static MainMenu me;

	// Token: 0x04000A94 RID: 2708
	public new int selected;

	// Token: 0x04000A95 RID: 2709
	public int x;

	// Token: 0x04000A96 RID: 2710
	public int y;

	// Token: 0x04000A97 RID: 2711
	public int wSmall;

	// Token: 0x04000A98 RID: 2712
	public int wGo;

	// Token: 0x04000A99 RID: 2713
	public int disSmall;

	// Token: 0x04000A9A RID: 2714
	public int numW;

	// Token: 0x04000A9B RID: 2715
	public int dis;

	// Token: 0x04000A9C RID: 2716
	public int angleNormal;

	// Token: 0x04000A9D RID: 2717
	public int disTran;

	// Token: 0x04000A9E RID: 2718
	public bool isGO;

	// Token: 0x04000A9F RID: 2719
	public bool isName;

	// Token: 0x04000AA0 RID: 2720
	public bool isAble;

	// Token: 0x04000AA1 RID: 2721
	private MyVector list;

	// Token: 0x04000AA2 RID: 2722
	public static FrameImage imgIconFlower;

	// Token: 0x04000AA3 RID: 2723
	public static FrameImage imgGO;

	// Token: 0x04000AA4 RID: 2724
	private MyScreen lastScr;

	// Token: 0x04000AA5 RID: 2725
	private Command cmdCenter;

	// Token: 0x04000AA6 RID: 2726
	public bool isCircle;

	// Token: 0x04000AA7 RID: 2727
	public new bool isHide;

	// Token: 0x04000AA8 RID: 2728
	private int angleCircle;

	// Token: 0x04000AA9 RID: 2729
	private int wCircle;

	// Token: 0x04000AAA RID: 2730
	private int maxW = 50 * AvMain.hd;

	// Token: 0x04000AAB RID: 2731
	private MyVector listObj;

	// Token: 0x04000AAC RID: 2732
	public AvPosition avaPaint;

	// Token: 0x04000AAD RID: 2733
	public static PopupName popFocus;

	// Token: 0x04000AAE RID: 2734
	public int cmtoX;

	// Token: 0x04000AAF RID: 2735
	public int cmx;

	// Token: 0x04000AB0 RID: 2736
	public int cmdx;

	// Token: 0x04000AB1 RID: 2737
	public int cmvx;

	// Token: 0x04000AB2 RID: 2738
	public int cmxLim;

	// Token: 0x04000AB3 RID: 2739
	public int disX;

	// Token: 0x04000AB4 RID: 2740
	public bool isWearing;

	// Token: 0x04000AB5 RID: 2741
	private static FrameImage imgLoading;

	// Token: 0x04000AB6 RID: 2742
	private int angle = 45;

	// Token: 0x04000AB7 RID: 2743
	private int xCenter;

	// Token: 0x04000AB8 RID: 2744
	private int yCenter;

	// Token: 0x04000AB9 RID: 2745
	private int maxRadius;

	// Token: 0x04000ABA RID: 2746
	private float distant;

	// Token: 0x04000ABB RID: 2747
	private float v = 5f;

	// Token: 0x04000ABC RID: 2748
	public int g;

	// Token: 0x04000ABD RID: 2749
	private bool trans;

	// Token: 0x04000ABE RID: 2750
	private bool isClick;

	// Token: 0x04000ABF RID: 2751
	private int dxTran;

	// Token: 0x04000AC0 RID: 2752
	private int timeOpen;

	// Token: 0x04000AC1 RID: 2753
	private long timeDelay;

	// Token: 0x04000AC2 RID: 2754
	private long count;

	// Token: 0x04000AC3 RID: 2755
	private long timePoint;

	// Token: 0x04000AC4 RID: 2756
	private int vX;

	// Token: 0x04000AC5 RID: 2757
	private int indexCircle = -1;

	// Token: 0x04000AC6 RID: 2758
	private int indexTemp = -1;

	// Token: 0x02000127 RID: 295
	private class IActionUpdateContainerYesNo : IAction
	{
		// Token: 0x06000853 RID: 2131 RVA: 0x00051879 File Offset: 0x0004FC79
		public void perform()
		{
			Canvas.startOKDlg(T.doYouWantUpgradeCoffer, new MainMenu.IActionUpdateContainer());
		}
	}

	// Token: 0x02000128 RID: 296
	private class IActionUpdateContainer : IAction
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x00051892 File Offset: 0x0004FC92
		public void perform()
		{
			GlobalService.gI().doUpdateContainer(0);
			Canvas.startWaitDlg();
		}
	}

	// Token: 0x02000129 RID: 297
	private class IActionOkWedding : IAction
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x000518AC File Offset: 0x0004FCAC
		public void perform()
		{
			ParkService.gI().doRequestWedding((int)MapScr.roomID, (int)MapScr.boardID);
			Canvas.startWaitDlg();
		}
	}

	// Token: 0x0200012A RID: 298
	private class IActionExchange : IAction
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x000518C9 File Offset: 0x0004FCC9
		public IActionExchange(StringObj strObj)
		{
			this.str = strObj;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000518D8 File Offset: 0x0004FCD8
		public void perform()
		{
			GlobalService.gI().doRequestCmdRotate(this.str.anthor, (MapScr.focusP == null) ? -1 : MapScr.focusP.IDDB);
		}

		// Token: 0x04000AC7 RID: 2759
		private StringObj str;
	}

	// Token: 0x0200012B RID: 299
	private class CommandExchange : Command
	{
		// Token: 0x0600085A RID: 2138 RVA: 0x00051909 File Offset: 0x0004FD09
		public CommandExchange(string name, MainMenu.IActionExchange ac, StringObj strObj) : base(name, ac)
		{
			this.str = strObj;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0005191C File Offset: 0x0004FD1C
		public override void paint(MyGraphics g, int x, int y)
		{
			if (AvatarData.getImgIcon((short)this.str.dis).count != -1)
			{
				AvatarData.paintImg(g, this.str.dis, x, y, 3);
			}
			else
			{
				MainMenu.imgLoading.drawFrame((int)this.count / 3, x, y, 0, 3, g);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00051976 File Offset: 0x0004FD76
		public override void update()
		{
			this.count = (sbyte)((int)this.count + 1);
			if ((int)this.count >= 9)
			{
				this.count = 0;
			}
		}

		// Token: 0x04000AC8 RID: 2760
		private StringObj str;

		// Token: 0x04000AC9 RID: 2761
		private sbyte count;
	}

	// Token: 0x0200012C RID: 300
	private class CommandMap : Command
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x0005199D File Offset: 0x0004FD9D
		public CommandMap(string name, MainMenu.IActionMap ac, StringObj str) : base(name, ac)
		{
			this.str = str;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000519B0 File Offset: 0x0004FDB0
		public override void paint(MyGraphics g, int x, int y)
		{
			if (AvatarData.getImgIcon((short)this.str.dis).count != -1)
			{
				AvatarData.paintImg(g, this.str.dis, x, y, 3);
			}
			else
			{
				MainMenu.imgLoading.drawFrame((int)this.count / 3, x, y, 0, 3, g);
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00051A0A File Offset: 0x0004FE0A
		public override void update()
		{
			this.count = (sbyte)((int)this.count + 1);
			if ((int)this.count >= 9)
			{
				this.count = 0;
			}
		}

		// Token: 0x04000ACA RID: 2762
		private StringObj str;

		// Token: 0x04000ACB RID: 2763
		private sbyte count;
	}

	// Token: 0x0200012D RID: 301
	private class IActionMap : IAction
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x00051A31 File Offset: 0x0004FE31
		public IActionMap(int i, int type, StringObj str)
		{
			this.ii = i;
			this.str = str;
			this.type = type;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00051A50 File Offset: 0x0004FE50
		public void perform()
		{
			if (this.type == 0)
			{
				GlobalService.gI().doCommunicate(this.ii);
			}
			else
			{
				GlobalService.gI().doRequestCmdRotate(this.str.anthor, (MapScr.focusP == null) ? -1 : MapScr.focusP.IDDB);
			}
		}

		// Token: 0x04000ACC RID: 2764
		private int ii;

		// Token: 0x04000ACD RID: 2765
		private int type;

		// Token: 0x04000ACE RID: 2766
		private readonly StringObj str;
	}

	// Token: 0x0200012E RID: 302
	private class CommandMenu : Command
	{
		// Token: 0x06000862 RID: 2146 RVA: 0x00051AAC File Offset: 0x0004FEAC
		public CommandMenu(string name, int type, int index) : base(name, type)
		{
			this.index = index;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00051ABD File Offset: 0x0004FEBD
		public CommandMenu(string name, IAction ac, int index) : base(name, ac)
		{
			this.index = index;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00051AD0 File Offset: 0x0004FED0
		public override void paint(MyGraphics g, int x, int y)
		{
			int idx = 0;
			int idy = this.index;
			if (this.index >= Menu.imgSellect.nFrame)
			{
				idx = 1;
				idy = this.index % Menu.imgSellect.nFrame;
			}
			Menu.imgSellect.drawFrameXY(idx, idy, x, y, 3, g);
		}

		// Token: 0x04000ACF RID: 2767
		private int index;
	}

	// Token: 0x0200012F RID: 303
	private class IActionMenu : IAction
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x00051B1F File Offset: 0x0004FF1F
		public IActionMenu(IAction ac)
		{
			this.ac = ac;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00051B2E File Offset: 0x0004FF2E
		public void perform()
		{
			this.ac.perform();
		}

		// Token: 0x04000AD0 RID: 2768
		private IAction ac;
	}
}
