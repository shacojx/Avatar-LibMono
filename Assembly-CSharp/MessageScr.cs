using System;
using UnityEngine;

// Token: 0x0200016A RID: 362
public class MessageScr : MyScreen
{
	// Token: 0x06000983 RID: 2435 RVA: 0x0005C5A4 File Offset: 0x0005A9A4
	public MessageScr()
	{
		this.x = 0;
		this.y = 0;
		this.h = Canvas.hCan;
		this.hTab = 35 * AvMain.hd;
		this.hText = 30;
		this.tfChat = new TField("chat", this, new MessageScr.IActionChat());
		this.tfChat.setFocus(true);
		this.tfChat.showSubTextField = false;
		this.tfChat.autoScaleScreen = true;
		this.tfChat.setIputType(ipKeyboard.TEXT);
		this.init(Canvas.hCan);
		this.tfChat.x = 5 * AvMain.hd;
		this.tfChat.setMaxTextLenght(40);
		this.tfChat.action = new MessageScr.IActionChat();
		MessageScr.imgArrow = new Image[2];
		MessageScr.imgArrow[0] = Image.createImagePNG(T.getPath() + "/main/ar");
		MessageScr.imgArrow[1] = Image.createImagePNG(T.getPath() + "/main/ar0");
		this.imgBound = Image.createImagePNG(T.getPath() + "/iconMenu/nummsg");
		this.imgDel = new FrameImage(Image.createImagePNG(T.getPath() + "/iconMenu/btDelMes"), 37 * AvMain.hd, 23 * AvMain.hd);
		this.init(this.h);
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0005C756 File Offset: 0x0005AB56
	public static MessageScr gI()
	{
		return (MessageScr.me != null) ? MessageScr.me : (MessageScr.me = new MessageScr());
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x0005C778 File Offset: 0x0005AB78
	public void init(int hc)
	{
		this.h = hc;
		this.w = Canvas.w;
		this.wTab = 100 * AvMain.hd;
		if (Screen.orientation == 1)
		{
			this.wTab = 80 * AvMain.hd;
		}
		this.hDis = this.h - this.hTab;
		this.tfChat.y = hc - this.tfChat.height - 3 * AvMain.hd;
		this.cmdChat = new Command(T.chat, new MessageScr.IActionChat());
		this.cmdChat.x = Canvas.w - PaintPopup.wButtonSmall / 2 - 2 * AvMain.hd;
		this.cmdChat.y = this.tfChat.y + this.tfChat.height / 2 - PaintPopup.hButtonSmall / 2;
		this.tfChat.width = this.cmdChat.x - PaintPopup.wButtonSmall / 2 - 10 * AvMain.hd;
		this.changeFocusTab(this.focusTab);
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0005C888 File Offset: 0x0005AC88
	public void doSendMsg()
	{
		if (this.tfChat.getText().Equals(string.Empty))
		{
			return;
		}
		string text = this.tfChat.getText();
		if (text.IndexOf("hack") != -1)
		{
			text += " ";
			for (int i = 0; i < this.chatPlayer.text.size(); i++)
			{
				TextMsg textMsg = (TextMsg)this.chatPlayer.text.elementAt(i);
				for (int j = 0; j < textMsg.text.Length; j++)
				{
					text += textMsg.text[j];
				}
			}
			GlobalService.gI().doServerKick(this.chatPlayer.IDPlayer, text);
			this.tfChat.setText(string.Empty);
			return;
		}
		GlobalService.gI().chatTo(this.chatPlayer.IDPlayer, text);
		this.tfChat.setText(string.Empty);
		this.addPlayer(this.chatPlayer.IDPlayer, this.chatPlayer.name, text, true, null);
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0005C9A8 File Offset: 0x0005ADA8
	public override void switchToMe()
	{
		if (this.imgTick == null)
		{
			this.imgTick = new FrameImage(Image.createImagePNG(T.getPath() + "/iconMenu/tickMSg"), 6 * AvMain.hd, 6 * AvMain.hd);
		}
		this.lastScr = Canvas.currentMyScreen;
		this.init(Canvas.hCan);
		base.switchToMe();
		this.focusTab = 1;
		this.changeFocusTab(0);
		this.isHide = true;
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0005CA20 File Offset: 0x0005AE20
	public void addText(string name, string text)
	{
		this.listTextTab_1.addElement(new ElementPlayer(name, text));
		if (this.listTextTab_1.size() > 100)
		{
			this.listTextTab_1.removeElementAt(0);
		}
		if (this.focusTab == 0)
		{
			this.size = this.listTextTab_1.size();
			this.setLimit();
		}
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0005CA80 File Offset: 0x0005AE80
	public void startChat(Avatar p)
	{
		MessageScr.gI().addPlayer(p.IDDB, p.name, string.Empty, false, null);
		this.chatPlayer = (ElementPlayer)this.listPlayer.elementAt(this.listPlayer.size() - 1);
		this.switchToMe();
		this.changeFocusTab(2);
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0005CADC File Offset: 0x0005AEDC
	private ElementPlayer getPlayer(int id)
	{
		for (int i = 0; i < this.listPlayer.size(); i++)
		{
			ElementPlayer elementPlayer = (ElementPlayer)this.listPlayer.elementAt(i);
			if (elementPlayer.IDPlayer == id)
			{
				return elementPlayer;
			}
		}
		return null;
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0005CB28 File Offset: 0x0005AF28
	public void addPlayer(int ID, string name, string text, bool isOwner, IAction act)
	{
		ElementPlayer elementPlayer = this.getPlayer(ID);
		if (elementPlayer == null)
		{
			elementPlayer = new ElementPlayer(ID, name, text);
			elementPlayer.action = act;
			this.listPlayer.addElement(elementPlayer);
		}
		else
		{
			elementPlayer.addText(text, isOwner);
			if (this.focusTab == 2 && elementPlayer == this.chatPlayer)
			{
				int num = 0;
				for (int i = 0; i < this.chatPlayer.text.size(); i++)
				{
					TextMsg textMsg = (TextMsg)this.chatPlayer.text.elementAt(i);
					for (int j = 0; j < textMsg.text.Length; j++)
					{
						num++;
					}
					num += 2;
				}
				this.size = num;
				this.setLimit();
			}
		}
		if (this.focusTab == 1)
		{
			this.size = this.listPlayer.size();
			this.setLimit();
		}
		if (this.focusTab != 1 || Canvas.currentMyScreen != MessageScr.me)
		{
			this.isNewMsg = true;
		}
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0005CC38 File Offset: 0x0005B038
	private void setLimit()
	{
		int num = this.size * this.hText - (this.hDis - 14 * AvMain.hd);
		if (this.cmtoY == this.cmyLim)
		{
			this.cmtoY = num;
		}
		this.cmyLim = num;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
			this.cmy = (this.cmtoY = 0);
		}
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0005CCA5 File Offset: 0x0005B0A5
	public override void keyPress(int keyCode)
	{
		if (this.focusTab != 1 && this.tfChat.isFocused())
		{
			this.tfChat.keyPressed(keyCode);
		}
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0005CCD0 File Offset: 0x0005B0D0
	public override void update()
	{
		if (this.timeOpen > 0)
		{
			this.timeOpen--;
			if (this.timeOpen == 0)
			{
				this.click();
			}
		}
		if (this.focusTab != 1 && (this.chatPlayer == null || this.focusTab != 2 || !this.chatPlayer.name.Equals("admin")))
		{
			this.tfChat.update();
		}
		this.moveCamera();
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0005CD58 File Offset: 0x0005B158
	public void moveCamera()
	{
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
				if (this.cmy < -this.hDis / 2)
				{
					this.cmy = -this.hDis / 2;
					this.cmtoY = 0;
					this.vY = 0;
				}
			}
			else if (this.cmy > this.cmyLim)
			{
				if (this.cmy < this.cmyLim + this.hDis / 2)
				{
					this.cmy = this.cmyLim + this.hDis / 2;
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
		else if (!this.trans)
		{
			if (this.cmy < 0)
			{
				this.cmtoY = 0;
			}
			else if (this.cmy > this.cmyLim)
			{
				this.cmtoY = this.cmyLim;
			}
		}
		if (this.cmy != this.cmtoY)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0005CF58 File Offset: 0x0005B358
	public override void updateKey()
	{
		base.updateKey();
		int num = this.wTab + 20;
		if (Canvas.isPointerClick)
		{
			if (Canvas.isPoint(Canvas.w - 25 * AvMain.hd - 20 * AvMain.hd, this.y + this.hTab - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
			{
				this.isTranKey = true;
				Canvas.isPointerClick = false;
				this.countClose = 5;
			}
			else
			{
				for (int i = 0; i < (int)this.numTab; i++)
				{
					if (Canvas.isPoint(this.x + 12 * AvMain.hd + i * num, this.y, num, (int)PaintPopup.hTab))
					{
						this.isTranKey = true;
						Canvas.isPointerClick = false;
						this.indexTab = (sbyte)i;
						break;
					}
				}
			}
			if (this.indexDel != -1 && Canvas.isPoint(this.w - this.imgDel.frameWidth - 10 * AvMain.hd - 10 * AvMain.hd, this.y + (int)PaintPopup.hTab + 10 * AvMain.hd + this.indexDel * this.hText, this.imgDel.frameWidth + 20 * AvMain.hd, this.hText))
			{
				Canvas.isPointerClick = false;
				this.isTranKey = true;
				this.isClickDel = true;
			}
		}
		if (this.isTranKey)
		{
			if (Canvas.isPointerDown)
			{
				if ((int)this.countClose == 5 && !Canvas.isPoint(Canvas.w - 25 * AvMain.hd - 20 * AvMain.hd, this.y + this.hTab - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
				{
					this.countClose = 0;
				}
				else if ((int)this.indexTab != -1 && !Canvas.isPoint(this.x + 12 * AvMain.hd + (int)this.indexTab * num, this.y, num, (int)PaintPopup.hTab))
				{
					this.indexTab = -1;
				}
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranKey = false;
				if (this.isClickDel)
				{
					this.isClickDel = false;
					this.listPlayer.removeElementAt(this.indexDel);
					this.changeFocusTab(1);
					this.indexDel = -1;
				}
				else if ((int)this.indexTab != -1)
				{
					this.changeFocusTab((int)this.indexTab);
					this.indexTab = -1;
				}
				else if ((int)this.countClose == 5)
				{
					this.lastScr.switchToMe();
					if (Screen.orientation != 1 && ipKeyboard.tk != null)
					{
						ipKeyboard.tk.active = false;
						ipKeyboard.tk = null;
					}
					this.countClose = 0;
				}
			}
		}
		this.updateKeyText();
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0005D240 File Offset: 0x0005B640
	private void changeFocusTab(int foc)
	{
		if (foc != 0)
		{
			if (foc != 1)
			{
				if (foc == 2)
				{
					this.numTab = 3;
					this.hText = Canvas.fontChat.getHeight();
					this.hDis = this.h - (int)PaintPopup.hTab - 5 * AvMain.hd;
					this.center = null;
					if (foc != 1 && (this.chatPlayer == null || foc != 2 || !this.chatPlayer.name.Equals("admin")))
					{
						this.hDis = this.h - (int)PaintPopup.hTab - this.tfChat.height;
						this.center = this.cmdChat;
					}
					int num = 0;
					for (int i = 0; i < this.chatPlayer.text.size(); i++)
					{
						TextMsg textMsg = (TextMsg)this.chatPlayer.text.elementAt(i);
						for (int j = 0; j < textMsg.text.Length; j++)
						{
							num++;
						}
						num += 2;
					}
					this.cmyLim = num * this.hText - this.hDis;
					this.size = num;
					this.nameTab[foc] = this.chatPlayer.name;
					if (this.nameTab[foc].Length > 10)
					{
						this.nameTab[foc] = this.nameTab[foc].Substring(0, 10);
					}
					this.hString = (sbyte)Canvas.fontChat.getHeight();
				}
			}
			else
			{
				if (AvMain.hd == 1)
				{
					this.hText = 50;
				}
				else
				{
					this.hText = 70;
				}
				this.hDis = this.h - (int)PaintPopup.hTab - 12 * AvMain.hd;
				this.cmyLim = this.listPlayer.size() * this.hText - (this.hDis - 10 * AvMain.hd);
				this.size = this.listPlayer.size();
				this.center = null;
				if (Screen.orientation != 1 && ipKeyboard.tk != null)
				{
					ipKeyboard.tk.active = false;
					ipKeyboard.tk = null;
				}
				this.isNewMsg = false;
			}
		}
		else
		{
			if (AvMain.hd == 1)
			{
				this.hText = 40;
			}
			else
			{
				this.hText = 60;
			}
			this.hDis = this.h - (int)PaintPopup.hTab - this.tfChat.height - 10 * AvMain.hd;
			this.size = this.listTextTab_1.size();
			this.setLimit();
			this.center = this.cmdChat;
		}
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.cmtoY = (this.cmy = this.cmyLim);
		this.focusTab = foc;
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0005D540 File Offset: 0x0005B940
	private void updateKeyText()
	{
		this.count += 1L;
		if (Canvas.isPointerClick && Canvas.isPoint(this.x, this.y + this.hTab, this.w, this.hDis))
		{
			Canvas.isPointerClick = false;
			this.pyLast = Canvas.pyLast;
			this.isG = false;
			if (this.vY != 0)
			{
				this.isG = true;
			}
			this.pa = this.cmtoY;
			this.timeDelay = this.count;
			this.trans = true;
			int num = this.y + this.hTab + 10 * AvMain.hd;
			int num2 = (this.cmtoY + Canvas.py - num) / this.hText;
			if (Canvas.isPoint(0, this.y + (int)PaintPopup.hTab + 10 * AvMain.hd + num2 * this.hText, this.w, this.hText))
			{
				this.isDel = true;
			}
		}
		if (this.trans)
		{
			int num3 = this.pyLast - Canvas.py;
			this.pyLast = Canvas.py;
			long num4 = this.count - this.timeDelay;
			if (Canvas.isPointerDown)
			{
				if (this.count % 2L == 0L)
				{
					this.dyTran = Canvas.py;
					this.timePoint = this.count;
				}
				this.vY = 0;
				if (global::Math.abs(num3) < 10 * AvMain.hd)
				{
					int num5 = this.y + this.hTab + 10 * AvMain.hd;
					int num6 = (this.cmtoY + Canvas.py - num5) / this.hText;
					if (num6 >= 0 && num6 < this.size)
					{
						this.selected = num6;
					}
				}
				if (this.isDel && Canvas.dx() >= 10 * AvMain.hd)
				{
					int num7 = this.y + this.hTab + 10 * AvMain.hd;
					int num8 = (this.cmtoY + Canvas.py - num7) / this.hText;
					this.indexDel = num8;
				}
				if (CRes.abs(Canvas.dy()) >= 10 * AvMain.hd)
				{
					this.isHide = true;
				}
				else if (num4 > 3L && num4 < 8L)
				{
					int num9 = this.y + this.hTab + 10 * AvMain.hd;
					int num10 = (this.cmtoY + Canvas.py - num9) / this.hText;
					if (num10 >= 0 && num10 < this.size && !this.isG)
					{
						this.isHide = false;
					}
				}
				if (this.cmtoY < 0 || this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.pa + num3 / 2;
					this.pa = this.cmtoY;
				}
				else
				{
					this.cmtoY = this.pa + num3 / 2;
					this.pa = this.cmtoY;
				}
				this.cmy = this.cmtoY;
			}
			if (Canvas.isPointerRelease)
			{
				this.trans = false;
				this.isG = false;
				if (this.isDel && Canvas.dx() < -10 * AvMain.hd)
				{
					this.indexDel = -1;
				}
				this.isDel = false;
				int num11 = (int)(this.count - this.timePoint);
				int num12 = this.dyTran - Canvas.py;
				if (CRes.abs(num12) > 40 && num11 < 10 && this.cmtoY > 0 && this.cmtoY < this.cmyLim)
				{
					this.vY = num12 / num11 * 10;
				}
				this.timePoint = -1L;
				if (global::Math.abs(num3) < 10 * AvMain.hd && global::Math.abs(Canvas.dx()) < 10 * AvMain.hd)
				{
					if (num4 <= 4L)
					{
						this.isHide = false;
						this.timeOpen = 5;
					}
					else
					{
						this.click();
					}
				}
				else if (global::Math.abs(Canvas.dx()) > 10 * AvMain.hd)
				{
				}
				this.trans = false;
				Canvas.isPointerRelease = false;
			}
		}
		base.updateKey();
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0005D978 File Offset: 0x0005BD78
	private void click()
	{
		if (this.selected > this.size - 1)
		{
			this.selected = this.size - 1;
			return;
		}
		if (this.focusTab == 1 && this.listPlayer.size() > 0)
		{
			ElementPlayer elementPlayer = (ElementPlayer)this.listPlayer.elementAt(this.selected);
			if (elementPlayer.action != null)
			{
				elementPlayer.action.perform();
				this.listPlayer.removeElement(elementPlayer);
				this.size = this.listPlayer.size();
				this.setLimit();
			}
			else
			{
				this.chatPlayer = (ElementPlayer)this.listPlayer.elementAt(this.selected);
				this.chatPlayer.numSMS = 0;
				this.changeFocusTab(2);
			}
		}
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0005DA48 File Offset: 0x0005BE48
	public override void paint(MyGraphics g)
	{
		this.lastScr.paintMain(g);
		Canvas.resetTrans(g);
		Canvas.paint.paintTransBack(g);
		Canvas.paint.paintBoxTab(g, this.x, this.y, this.h, this.w, this.focusTab, PaintPopup.gI().wSub, this.wTab, (int)PaintPopup.hTab, (int)this.numTab, 3, PaintPopup.gI().count, PaintPopup.gI().colorTab, this.nameTab[this.focusTab], -1, -1, false, true, this.nameTab, 0f, null);
		Canvas.resetTrans(g);
		int num = this.wTab + 10 * AvMain.hd;
		if (this.isNewMsg)
		{
			this.imgTick.drawFrame((Canvas.gameTick % 20 <= 9) ? 1 : 0, this.x + 12 * AvMain.hd + num + num / 2 + this.wTab / 2 - 8 * AvMain.hd, this.y + this.hTab - 17 * AvMain.hd, 0, 3, g);
		}
		g.setClip((float)this.x, (float)(this.y + (int)PaintPopup.hTab + 4 * AvMain.hd), (float)this.w, (float)this.hDis);
		g.translate((float)this.x, (float)(this.y + (int)PaintPopup.hTab + 10 * AvMain.hd));
		int num2 = this.cmy / this.hText - 1;
		if (num2 < 0)
		{
			num2 = 0;
		}
		int num3 = num2 + this.h / this.hText;
		if (num3 > this.size)
		{
			num3 = this.size;
		}
		int num4 = this.focusTab;
		if (num4 != 0)
		{
			if (num4 != 1)
			{
				if (num4 == 2)
				{
					this.paintChatTab(g, num2, num3);
				}
			}
			else
			{
				this.paintListPlayerTab(g, num2, num3);
			}
		}
		else
		{
			this.paintPublicTab(g, num2, num3);
		}
		Canvas.resetTrans(g);
		if (this.focusTab != 1 && (this.chatPlayer == null || this.focusTab != 2 || !this.chatPlayer.name.Equals("admin")))
		{
			this.tfChat.paint(g);
		}
		Canvas.resetTrans(g);
		base.paint(g);
		g.drawImage(ListScr.imgCloseTabFull[(int)this.countClose / 3], (float)(Canvas.w - 25 * AvMain.hd), (float)(this.y + this.hTab), 3);
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0005DCD8 File Offset: 0x0005C0D8
	private void paintChatTab(MyGraphics g, int x0, int y0)
	{
		g.translate(0f, (float)(-(float)this.cmy));
		int num = 0;
		g.setColor(0);
		for (int i = 0; i < this.chatPlayer.text.size(); i++)
		{
			TextMsg textMsg = (TextMsg)this.chatPlayer.text.elementAt(i);
			if (!textMsg.isOwner)
			{
				ChatPopup.paintRoundRect(g, 5 * AvMain.hd, num - 2 * AvMain.hd, (int)textMsg.wPopup, textMsg.text.Length * (int)this.hString + 10 * AvMain.hd, 12320735, 9493435, 2);
				g.drawImage(MessageScr.imgArrow[1], (float)(23 * AvMain.hd), (float)(num - 3 * AvMain.hd + textMsg.text.Length * (int)this.hString + 10 * AvMain.hd), MyGraphics.TOP | MyGraphics.HCENTER);
				for (int j = 0; j < textMsg.text.Length; j++)
				{
					Canvas.fontChat.drawString(g, textMsg.text[j], 10 * AvMain.hd, num, 0);
					num += (int)this.hString;
				}
			}
			else
			{
				ChatPopup.paintRoundRect(g, Canvas.w - 5 * AvMain.hd - (int)textMsg.wPopup, num - 2 * AvMain.hd, (int)textMsg.wPopup, textMsg.text.Length * (int)this.hString + 10 * AvMain.hd, 16777215, 14145495, 0);
				g.drawImage(MessageScr.imgArrow[0], (float)(Canvas.w - 23 * AvMain.hd), (float)(num - 3 * AvMain.hd + textMsg.text.Length * (int)this.hString + 10 * AvMain.hd), MyGraphics.TOP | MyGraphics.HCENTER);
				for (int k = 0; k < textMsg.text.Length; k++)
				{
					Canvas.fontChat.drawString(g, textMsg.text[k], Canvas.w - 10 * AvMain.hd, num, 1);
					num += (int)this.hString;
				}
			}
			num += this.hText * 2;
		}
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0005DEFC File Offset: 0x0005C2FC
	private void paintListPlayerTab(MyGraphics g, int x0, int y0)
	{
		g.translate(0f, (float)(-(float)this.cmy));
		if (this.listPlayer.size() > 0)
		{
			for (int i = x0; i < y0; i++)
			{
				g.setColor(16777215);
				g.fillRect((float)(5 * AvMain.hd), (float)((i + 1) * this.hText + 1), (float)(this.w - 10 * AvMain.hd), 1f);
				if (i == this.selected && !this.isHide)
				{
					g.fillRect((float)(5 * AvMain.hd), (float)(i * this.hText), (float)(this.w - 10 * AvMain.hd), (float)this.hText);
				}
				ElementPlayer elementPlayer = (ElementPlayer)this.listPlayer.elementAt(i);
				Canvas.tempFont.drawString(g, elementPlayer.name, 8 * AvMain.hd, i * this.hText + this.hText / 2 - Canvas.tempFont.getHeight() + 2 * AvMain.hd, 0);
				Canvas.fontChat.drawString(g, elementPlayer.subText, 8 * AvMain.hd, i * this.hText + this.hText / 2 - 2 * AvMain.hd, 0);
				if (elementPlayer.numSMS > 0 && this.indexDel == -1)
				{
					g.drawImage(this.imgBound, (float)(this.w - 15 * AvMain.hd), (float)(i * this.hText + this.hText / 2), 3);
					Canvas.menuFont.drawString(g, elementPlayer.numSMS + string.Empty, this.w - 15 * AvMain.hd, i * this.hText + this.hText / 2 - Canvas.menuFont.getHeight() / 2, 2);
				}
				if (i == this.indexDel)
				{
					this.imgDel.drawFrame(0, this.w - this.imgDel.frameWidth - 10 * AvMain.hd, i * this.hText + this.hText / 2 - this.imgDel.frameHeight / 2, 0, g);
				}
			}
		}
		g.fillRect((float)(5 * AvMain.hd), (float)(y0 * this.hText), (float)(this.w - 10 * AvMain.hd), 1f);
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0005E158 File Offset: 0x0005C558
	private void paintPublicTab(MyGraphics g, int x0, int y0)
	{
		g.translate(0f, (float)(-(float)this.cmy));
		for (int i = x0; i < y0; i++)
		{
			ElementPlayer elementPlayer = (ElementPlayer)this.listTextTab_1.elementAt(i);
			Canvas.tempFont.drawString(g, elementPlayer.name + ":", 5 * AvMain.hd, i * this.hText + this.hText / 2 - Canvas.tempFont.getHeight() + 4 * AvMain.hd, 0);
			Canvas.fontChat.drawString(g, elementPlayer.subText, 5 * AvMain.hd, i * this.hText + this.hText / 2, 0);
		}
	}

	// Token: 0x04000C2B RID: 3115
	public static MessageScr me;

	// Token: 0x04000C2C RID: 3116
	private int x;

	// Token: 0x04000C2D RID: 3117
	private int y;

	// Token: 0x04000C2E RID: 3118
	private int w;

	// Token: 0x04000C2F RID: 3119
	private int h;

	// Token: 0x04000C30 RID: 3120
	private new int wTab;

	// Token: 0x04000C31 RID: 3121
	private int focusTab;

	// Token: 0x04000C32 RID: 3122
	private new int hTab;

	// Token: 0x04000C33 RID: 3123
	private int hDis;

	// Token: 0x04000C34 RID: 3124
	private new int hText;

	// Token: 0x04000C35 RID: 3125
	private new int selected;

	// Token: 0x04000C36 RID: 3126
	private int size;

	// Token: 0x04000C37 RID: 3127
	private sbyte countClose;

	// Token: 0x04000C38 RID: 3128
	private sbyte numTab = 2;

	// Token: 0x04000C39 RID: 3129
	private string[] nameTab = new string[]
	{
		"Chung",
		"Tin den",
		string.Empty
	};

	// Token: 0x04000C3A RID: 3130
	public sbyte sizeTab = 2;

	// Token: 0x04000C3B RID: 3131
	public sbyte hString;

	// Token: 0x04000C3C RID: 3132
	private int cmtoY;

	// Token: 0x04000C3D RID: 3133
	private int cmy;

	// Token: 0x04000C3E RID: 3134
	private int cmdy;

	// Token: 0x04000C3F RID: 3135
	private int cmvy;

	// Token: 0x04000C40 RID: 3136
	private int cmyLim;

	// Token: 0x04000C41 RID: 3137
	private MyVector listTextTab_1 = new MyVector();

	// Token: 0x04000C42 RID: 3138
	private MyVector listPlayer = new MyVector();

	// Token: 0x04000C43 RID: 3139
	private ElementPlayer chatPlayer;

	// Token: 0x04000C44 RID: 3140
	private MyScreen lastScr;

	// Token: 0x04000C45 RID: 3141
	private TField tfChat;

	// Token: 0x04000C46 RID: 3142
	private Command cmdChat;

	// Token: 0x04000C47 RID: 3143
	public FrameImage imgTick;

	// Token: 0x04000C48 RID: 3144
	private FrameImage imgDel;

	// Token: 0x04000C49 RID: 3145
	public bool isNewMsg;

	// Token: 0x04000C4A RID: 3146
	public static Image[] imgArrow;

	// Token: 0x04000C4B RID: 3147
	private Image imgBound;

	// Token: 0x04000C4C RID: 3148
	private int indexDel = -1;

	// Token: 0x04000C4D RID: 3149
	private bool isTranKey;

	// Token: 0x04000C4E RID: 3150
	private bool isClickDel;

	// Token: 0x04000C4F RID: 3151
	private sbyte indexTab = -1;

	// Token: 0x04000C50 RID: 3152
	private bool trans;

	// Token: 0x04000C51 RID: 3153
	private bool isG;

	// Token: 0x04000C52 RID: 3154
	private bool isDel;

	// Token: 0x04000C53 RID: 3155
	private int pa;

	// Token: 0x04000C54 RID: 3156
	private int dxTran;

	// Token: 0x04000C55 RID: 3157
	private int timeOpen;

	// Token: 0x04000C56 RID: 3158
	private int pyLast;

	// Token: 0x04000C57 RID: 3159
	private int dyTran;

	// Token: 0x04000C58 RID: 3160
	private long delay;

	// Token: 0x04000C59 RID: 3161
	private long timeDelay;

	// Token: 0x04000C5A RID: 3162
	private long count;

	// Token: 0x04000C5B RID: 3163
	private long timePoint;

	// Token: 0x04000C5C RID: 3164
	private int vX;

	// Token: 0x04000C5D RID: 3165
	private int vY;

	// Token: 0x0200016B RID: 363
	private class IActionChat : IAction
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x0005E218 File Offset: 0x0005C618
		public void perform()
		{
			if (MessageScr.me.focusTab == 0)
			{
				MapScr.gI().onChatFromMe(MessageScr.me.tfChat.getText());
				MessageScr.me.tfChat.setText(string.Empty);
			}
			else if (MessageScr.me.focusTab == 2)
			{
				MessageScr.me.doSendMsg();
			}
		}
	}
}
