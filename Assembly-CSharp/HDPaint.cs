using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class HDPaint : IPaint
{
	// Token: 0x06000252 RID: 594 RVA: 0x0001368C File Offset: 0x00011A8C
	static HDPaint()
	{
		TField.xDu = 8;
		TField.yDu = 8;
		PaintPopup.color = new int[]
		{
			3521446,
			2378578,
			8052436,
			2716523,
			16701696,
			7042560
		};
		Canvas.imagePlug = Image.createImagePNG(T.getPath() + "/12Plus");
		Avatar.imgHit = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/5"), 100, 96);
		Avatar.imgKiss = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/2"), 22, 20);
		Canvas.imgTabInfo = Image.createImage(T.getPath() + "/effect/transtab");
		MapScr.imgBar = Image.createImagePNG(T.getPath() + "/effect/bar");
		Pet.imgShadow[0] = Image.createImage(T.getPath() + "/effect/s1");
		Pet.imgShadow[1] = Image.createImage(T.getPath() + "/effect/s2");
		Menu.imgSellect = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/cmd"), 48, 48);
		MainMenu.imgGO = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/go"), 48, 48);
		MapScr.imgFocusP = Image.createImagePNG(T.getPath() + "/effect/arF");
		Avatar.imgBlog = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/dauhoathi"), 18, 18);
		HDPaint.imgEraser = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/eraser"), 26, 26);
		HDPaint.imgMSG = new Image[2];
		MyScreen.imgChat = new Image[2];
		ListScr.imgCloseTab = new Image[2];
		ListScr.imgCloseTabFull = new Image[2];
		for (int i = 0; i < 2; i++)
		{
			MyScreen.imgChat[i] = Image.createImagePNG(T.getPath() + "/iconMenu/chat" + i);
			HDPaint.imgMSG[i] = Image.createImagePNG(T.getPath() + "/iconMenu/msg" + i);
			ListScr.imgCloseTabFull[i] = Image.createImagePNG(T.getPath() + "/iconMenu/close" + i);
			ListScr.imgCloseTab[i] = Image.createImagePNG(T.getPath() + "/iconMenu/closenot" + i);
		}
		HDPaint.imgCheck = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/check"), 43, 43);
		TField.tfframe = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/tb"), 50, 56);
		PaintPopup.imgMuiIOS = new Image[2][];
		for (int j = 0; j < 2; j++)
		{
			PaintPopup.imgMuiIOS[j] = new Image[4];
			for (int k = 0; k < 4; k++)
			{
				PaintPopup.imgMuiIOS[j][k] = Image.createImagePNG(string.Concat(new object[]
				{
					T.getPath(),
					"/ios/a",
					j,
					string.Empty,
					k
				}));
			}
		}
		MsgDlg.hCell = PaintPopup.imgMuiIOS[0][2].getHeight() + 8;
		HDPaint.imgTrans = Image.createImage(T.getPath() + "/effect/trans");
		AvMain.hCmd = 58;
		HDPaint.imgPopupBack = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/popupBack"), 40, 40);
		HDPaint.imgPopupBackNum = new Image[4];
		for (int l = 0; l < 4; l++)
		{
			HDPaint.imgPopupBackNum[l] = Image.createImagePNG(T.getPath() + "/effect/popupBack" + l);
		}
		HDPaint.imgEffectBack = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/effectPopupBack"), 100, 50);
		HDPaint.imgFocusTab = new FrameImage(Image.createImagePNG(T.getPath() + "/iconMenu/tabnotfocus"), 24, 75);
		HDPaint.imgNotFocusTab = new FrameImage(Image.createImagePNG(T.getPath() + "/iconMenu/tabfocus"), 24, 61);
		HDPaint.imgFocusTab_1 = Image.createImagePNG(T.getPath() + "/iconMenu/tabnotfocus1");
		HDPaint.imgNotFocusTab_1 = Image.createImagePNG(T.getPath() + "/iconMenu/tabfocus1");
		HDPaint.imgNewMsg = Image.createImagePNG(T.getPath() + "/iconMenu/imgMessIn");
	}

	// Token: 0x06000254 RID: 596 RVA: 0x00013B48 File Offset: 0x00011F48
	public void loadImgAvatar()
	{
		if (HDPaint.iconMenu != null)
		{
			return;
		}
		HDPaint.iconMenu = new Image[2];
		HDPaint.iconFeel = new Image[2];
		HDPaint.iconAction = new Image[2];
		HDPaint.iconMenu_2 = new Image[2];
		HDPaint.iconRota = new Image[2];
		for (int i = 0; i < 2; i++)
		{
			HDPaint.iconMenu[i] = Image.createImagePNG(T.getPath() + "/iconMenu/menuAction" + i);
			HDPaint.iconAction[i] = Image.createImagePNG(T.getPath() + "/iconMenu/action" + i);
			HDPaint.iconFeel[i] = Image.createImagePNG(T.getPath() + "/iconMenu/feel" + i);
			HDPaint.iconMenu_2[i] = Image.createImagePNG(T.getPath() + "/iconMenu/menu" + i);
			HDPaint.iconRota[i] = Image.createImagePNG(T.getPath() + "/iconMenu/rota" + i);
		}
		MsgDlg.imgLoad = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/busy"), 32, 32);
		Menu.imgBackIcon = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/backcmd"), 60, 60);
		HDPaint.imgButton = new Image[2];
		for (int j = 0; j < 2; j++)
		{
			HDPaint.imgButton[j] = Image.createImagePNG(T.getPath() + "/effect/button" + j);
		}
		PaintPopup.wButtonSmall = HDPaint.imgButton[1].getWidth();
		PaintPopup.hButtonSmall = HDPaint.imgButton[1].getHeight();
	}

	// Token: 0x06000255 RID: 597 RVA: 0x00013CF2 File Offset: 0x000120F2
	public void clearImgAvatar()
	{
		HDPaint.iconMenu = null;
		Menu.imgBackIcon = null;
		HDPaint.iconAction = null;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00013D08 File Offset: 0x00012108
	public void paintTextBox(MyGraphics g, int x, int y, int width, int height, TField tf, bool isFocus, sbyte indexEraser)
	{
		Canvas.resetTrans(g);
		TField.tfframe.drawFrame(0, x, y, 0, g);
		TField.tfframe.drawFrame(1, x + width - TField.tfframe.frameWidth, y, 0, g);
		g.setColor(2720192);
		g.fillRect((float)(x + TField.tfframe.frameWidth), (float)y, (float)(width - TField.tfframe.frameWidth * 2), 2f);
		g.fillRect((float)(x + TField.tfframe.frameWidth), (float)(y + height - 2), (float)(width - TField.tfframe.frameWidth * 2), 2f);
		g.setColor(16777215);
		g.fillRect((float)(x + TField.tfframe.frameWidth), (float)(y + 2), (float)(width - TField.tfframe.frameWidth * 2), (float)(height - 4));
		if (tf.isFocused() && !tf.paintedText.Equals(string.Empty))
		{
			HDPaint.imgEraser.drawFrame((int)indexEraser, x + width - 20, y + height / 2, 0, 3, g);
		}
		g.setClip((float)(x + 3), (float)(y + 1), (float)(width - 8), (float)(height - 4));
		g.setColor(0);
		if (tf.paintedText.Equals(string.Empty))
		{
			Canvas.fontChatB.drawString(g, tf.sDefaust, TField.TEXT_GAP_X + tf.offsetX + x + 6, y + (height - Canvas.fontChatB.getHeight()) / 2 - 4, 0);
		}
		else
		{
			Canvas.fontChatB.drawString(g, tf.paintedText, TField.TEXT_GAP_X + tf.offsetX + x + 6, y + (height - Canvas.fontChatB.getHeight()) / 2 - ((tf.inputType != ipKeyboard.PASS) ? 4 : 0), 0);
		}
		if (tf.isFocused() && tf.keyInActiveState == 0 && (tf.showCaretCounter > 0 || tf.counter / 5 % 2 == 0))
		{
			g.setColor(0);
			g.fillRect((float)(TField.TEXT_GAP_X + tf.offsetX + x + Canvas.fontChatB.getWidth(tf.paintedText.Substring(0, tf.caretPos) + "a") - Canvas.fontChatB.getWidth("a") - 1 + 6), (float)(y + 12), 1f, (float)(height - 24));
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00013F80 File Offset: 0x00012380
	public void paintPopupBack(MyGraphics g, int x, int y, int w, int h, int countClose, bool isFull)
	{
		g.drawImageScale(HDPaint.imgEffectBack.imgFrame, x, y + h - 20 - HDPaint.imgEffectBack.frameHeight, w + 6, 100, 0);
		if (!isFull)
		{
			HDPaint.imgPopupBack.drawFrame(0, x, y, 0, g);
			HDPaint.imgPopupBack.drawFrame(1, x + w - HDPaint.imgPopupBack.frameWidth, y, 0, g);
			g.drawImageScale(HDPaint.imgPopupBackNum[0], x + HDPaint.imgPopupBack.frameWidth, y, w - HDPaint.imgPopupBack.frameWidth * 2, HDPaint.imgPopupBack.frameHeight, 0);
			g.drawImageScale(HDPaint.imgPopupBackNum[1], x + HDPaint.imgPopupBack.frameWidth, y + h - HDPaint.imgPopupBack.frameHeight, w - HDPaint.imgPopupBack.frameWidth * 2, HDPaint.imgPopupBack.frameHeight, 0);
			g.drawImageScale(HDPaint.imgPopupBackNum[2], x, y + HDPaint.imgPopupBack.frameHeight, HDPaint.imgPopupBack.frameWidth, h - HDPaint.imgPopupBack.frameHeight * 2, 0);
			g.drawImageScale(HDPaint.imgPopupBackNum[3], x + w - HDPaint.imgPopupBack.frameWidth, y + HDPaint.imgPopupBack.frameHeight, HDPaint.imgPopupBack.frameWidth, h - HDPaint.imgPopupBack.frameHeight * 2, 0);
			g.setColor(13495295);
			g.fillRect((float)(x + HDPaint.imgPopupBack.frameWidth), (float)(y + HDPaint.imgPopupBack.frameHeight), (float)(w - HDPaint.imgPopupBack.frameWidth * 2), (float)(h - HDPaint.imgPopupBack.frameHeight * 2));
		}
		else
		{
			g.drawImageScale(HDPaint.imgPopupBackNum[0], x, y, w, HDPaint.imgPopupBack.frameHeight, 0);
			g.setColor(13495295);
			g.fillRect((float)x, (float)(y + HDPaint.imgPopupBack.frameHeight), (float)w, (float)(h - HDPaint.imgPopupBack.frameHeight));
		}
		if (!isFull)
		{
			HDPaint.imgPopupBack.drawFrame(3, x, y + h - HDPaint.imgPopupBack.frameHeight, 0, g);
			HDPaint.imgPopupBack.drawFrame(2, x + w - HDPaint.imgPopupBack.frameWidth, y + h - HDPaint.imgPopupBack.frameHeight, 0, g);
		}
		if (countClose != -1)
		{
			if (!isFull)
			{
				g.drawImage(ListScr.imgCloseTab[countClose], (float)(x + w - 3), (float)y, 3);
			}
			else
			{
				g.drawImage(ListScr.imgCloseTabFull[countClose], (float)(x + w - 10), (float)(y + 10), 3);
			}
		}
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0001420C File Offset: 0x0001260C
	public void paintBoxTab(MyGraphics g, int x, int y, int h, int w, int focusTab, int wSub, int wTab, int hTab, int numTab, int maxTab, int[] count, int[] colorTab, string name, sbyte countCloseAll, sbyte countText, bool isMenu, bool isFull, string[] subName, float cmx, Image[][] imgIcon)
	{
		Canvas.resetTrans(g);
		int num = PaintPopup.xTab;
		int num2 = PaintPopup.wTabDu;
		num = 25;
		num2 = wTab + 20;
		g.setColor(0);
		for (int i = 0; i < numTab; i++)
		{
			if (i != focusTab)
			{
				HDPaint.imgNotFocusTab.drawFrame(0, num + x + i * num2 + num2 / 2 - wTab / 2, y + hTab - HDPaint.imgNotFocusTab.frameHeight, 0, g);
				HDPaint.imgNotFocusTab.drawFrame(1, num + x + i * num2 + num2 / 2 + wTab / 2 - HDPaint.imgNotFocusTab.frameWidth, y + hTab - HDPaint.imgNotFocusTab.frameHeight, 0, g);
				g.drawImageScale(HDPaint.imgNotFocusTab_1, num + x + i * num2 + num2 / 2 - wTab / 2 + HDPaint.imgNotFocusTab.frameWidth, y + hTab - HDPaint.imgNotFocusTab.frameHeight, wTab - HDPaint.imgNotFocusTab.frameWidth * 2, HDPaint.imgNotFocusTab.frameHeight, 0);
				if (imgIcon != null)
				{
					g.drawImage(imgIcon[i][1], (float)(num + x + i * num2 + num2 / 2), (float)(y + hTab - HDPaint.imgNotFocusTab.frameHeight / 2 + 4), 3);
				}
				else
				{
					Canvas.fontWhiteBold.drawString(g, subName[i], num + x + i * num2 + num2 / 2, y + hTab - HDPaint.imgNotFocusTab.frameHeight / 2 - Canvas.fontWhiteBold.getHeight() / 2, 2);
				}
			}
		}
		this.paintPopupBack(g, x, y + hTab, w, h - hTab, -1, isFull);
		HDPaint.imgFocusTab.drawFrame(0, num + x + focusTab * num2 + num2 / 2 - wTab / 2, y + hTab - HDPaint.imgFocusTab.frameHeight + 6, 0, g);
		HDPaint.imgFocusTab.drawFrame(1, num + x + focusTab * num2 + num2 / 2 + wTab / 2 - HDPaint.imgFocusTab.frameWidth, y + hTab - HDPaint.imgFocusTab.frameHeight + 6, 0, g);
		g.drawImageScale(HDPaint.imgFocusTab_1, num + x + focusTab * num2 + num2 / 2 - wTab / 2 + HDPaint.imgFocusTab.frameWidth, y + hTab - HDPaint.imgFocusTab.frameHeight + 6, wTab - HDPaint.imgFocusTab.frameWidth * 2, HDPaint.imgFocusTab_1.getHeight(), 0);
		if (numTab > 1)
		{
			if (imgIcon != null)
			{
				g.drawImage(imgIcon[focusTab][0], (float)(num + x + focusTab * num2 + num2 / 2), (float)(y + hTab - HDPaint.imgFocusTab.frameHeight / 2 + 2), 3);
			}
			else
			{
				Canvas.fontWhiteBold.drawString(g, subName[focusTab], num + x + focusTab * num2 + num2 / 2, y + hTab - HDPaint.imgFocusTab.frameHeight / 2 + 2 - Canvas.fontWhiteBold.getHeight() / 2, 2);
			}
		}
		else if (imgIcon != null)
		{
			g.drawImage(imgIcon[focusTab][0], (float)(num + x + focusTab * num2 + num2 / 2), (float)(y + hTab - HDPaint.imgNotFocusTab.frameHeight / 2), 3);
		}
		else
		{
			g.setClip((float)(num + x + focusTab * num2 + num2 / 2 - wTab / 2 + 4), (float)(y + hTab - HDPaint.imgFocusTab.frameHeight + 6), (float)(wTab - 8), (float)hTab);
			Canvas.fontWhiteBold.drawString(g, name, num + x + focusTab * num2 + num2 / 2 + (int)countText, y + hTab - HDPaint.imgNotFocusTab.frameHeight / 2 - Canvas.fontWhiteBold.getHeight() / 2 - 4, 2);
			Canvas.resetTrans(g);
		}
		Canvas.resetTrans(g);
		g.setClip((float)x, 0f, (float)(w + ListScr.imgCloseTab[0].w), (float)h);
		if ((int)countCloseAll != -1)
		{
			if (!isFull)
			{
				g.drawImage(ListScr.imgCloseTab[(int)countCloseAll], (float)(x + w + 5 - 5 * AvMain.hd), (float)(y + hTab + 2), 3);
			}
			else
			{
				g.drawImage(ListScr.imgCloseTabFull[(int)countCloseAll], (float)(x + w - 10 - ((!isFull) ? 0 : 25)), (float)(y + hTab + 10 - ((!isFull) ? 0 : 23)), 3);
			}
		}
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00014622 File Offset: 0x00012A22
	public void paintBGCMD(MyGraphics g, int x, int y, int w, int h)
	{
		g.setColor(0);
		g.fillRect((float)x, (float)y, (float)w, (float)h);
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0001463B File Offset: 0x00012A3B
	public void paintButton(MyGraphics g, int x, int y, int index, string text)
	{
		g.drawImage(HDPaint.imgButton[index], (float)(x - PaintPopup.wButtonSmall / 2), (float)y, 0);
		Canvas.normalFont.drawString(g, text, x, y + AvMain.hCmd / 2 - (int)AvMain.hNormal / 2 + 1, 2);
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0001467C File Offset: 0x00012A7C
	public void paintCmd(MyGraphics g, Command left, Command center, Command right)
	{
		int wTab = MyScreen.wTab;
		if (left != null && left.caption != null)
		{
			if (left.x != -1)
			{
				this.paintButton(g, left.x, left.y, (int)(AvMain.indexLeft / 3), left.caption);
			}
			else
			{
				this.paintButton(g, Canvas.posCmd[0].x + wTab / 2, Canvas.hCan - PaintPopup.hButtonSmall - 3, (int)(AvMain.indexLeft / 3), left.caption);
			}
		}
		if (center != null && center.caption != null)
		{
			if (center.x != -1)
			{
				this.paintButton(g, center.x, center.y, (int)(AvMain.indexCenter / 3), center.caption);
			}
			else
			{
				this.paintButton(g, Canvas.posCmd[1].x + wTab / 2, Canvas.hCan - PaintPopup.hButtonSmall - 3, (int)(AvMain.indexCenter / 3), center.caption);
			}
		}
		if (right != null && right.caption != null)
		{
			if (right.x != -1)
			{
				this.paintButton(g, right.x, right.y, (int)(AvMain.indexRight / 3), right.caption);
			}
			else
			{
				this.paintButton(g, Canvas.posCmd[2].x + wTab / 2, Canvas.hCan - PaintPopup.hButtonSmall - 3, (int)(AvMain.indexRight / 3), right.caption);
			}
		}
	}

	// Token: 0x0600025C RID: 604 RVA: 0x000147F0 File Offset: 0x00012BF0
	public void initImgCard()
	{
		if (HDPaint.imgCardBg != null)
		{
			return;
		}
		try
		{
			HDPaint.imgCardIcon = new Image[52];
			for (int i = 0; i < 52; i++)
			{
				HDPaint.imgCardIcon[i] = Image.createImagePNG(T.getPath() + "/card/" + i);
			}
			HDPaint.imgCardBg = Image.createImagePNG(T.getPath() + "/card/down");
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.StackTrace);
		}
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00014888 File Offset: 0x00012C88
	public void resetCasino()
	{
		HDPaint.imgCardIcon = null;
		HDPaint.imgCardBg = null;
	}

	// Token: 0x0600025E RID: 606 RVA: 0x00014898 File Offset: 0x00012C98
	public void paintHalf(MyGraphics g, Card c)
	{
		if ((int)c.cardID == -1)
		{
			g.drawImage(HDPaint.imgCardBg, (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
			return;
		}
		g.drawImage(HDPaint.imgCardIcon[c.cardMapping[(int)c.cardID / 4] * 4 + (int)c.cardID % 4], (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00014920 File Offset: 0x00012D20
	public void paintHalfBackFull(MyGraphics g, Card c)
	{
		if ((int)c.cardID == -1)
		{
			g.drawImage(HDPaint.imgCardBg, (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
			return;
		}
		g.drawImage(HDPaint.imgCardIcon[c.cardMapping[(int)c.cardID / 4] * 4 + (int)c.cardID % 4], (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
	}

	// Token: 0x06000260 RID: 608 RVA: 0x000149A8 File Offset: 0x00012DA8
	public void paintFull(MyGraphics g, Card c)
	{
		if ((int)c.cardID == -1)
		{
			g.drawImage(HDPaint.imgCardBg, (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
			return;
		}
		g.drawImage(HDPaint.imgCardIcon[c.cardMapping[(int)c.cardID / 4] * 4 + (int)c.cardID % 4], (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00014A30 File Offset: 0x00012E30
	public void paintSmall(MyGraphics g, Card c, bool isCh)
	{
		if ((int)c.cardID == -1)
		{
			g.drawImage(HDPaint.imgCardBg, (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
			return;
		}
		g.drawImage(HDPaint.imgCardIcon[c.cardMapping[(int)c.cardID / 4] * 4 + (int)c.cardID % 4], (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
	}

	// Token: 0x06000262 RID: 610 RVA: 0x00014AB8 File Offset: 0x00012EB8
	public void init()
	{
		AvMain.hDuBox = 18;
	}

	// Token: 0x06000263 RID: 611 RVA: 0x00014AC4 File Offset: 0x00012EC4
	public void initPos()
	{
		MyScreen.hText = Canvas.h / 12;
		if (MyScreen.hText < 80)
		{
			MyScreen.hText = 80;
		}
		AvMain.hFillTab = 0;
		MyScreen.hTab = 64;
		Canvas.hTab = MyScreen.hText;
		if (Canvas.instance != null)
		{
			AvMain.hFillTab = Canvas.hTab / 6;
			Canvas.h -= AvMain.hFillTab * 5;
		}
		MyScreen.wTab = 172;
		int h = Canvas.h;
		Canvas.posCmd[0] = new AvPosition(2, h - AvMain.hFillTab - 5, 2);
		Canvas.posCmd[1] = new AvPosition(Canvas.hw - MyScreen.wTab / 2, h - AvMain.hFillTab - 5, 2);
		Canvas.posCmd[2] = new AvPosition(Canvas.w - MyScreen.wTab - 2, h - AvMain.hFillTab - 5, 2);
		Canvas.posByteCOunt = new AvPosition(Canvas.w - 2, 1, 1);
		if (Canvas.instance != null)
		{
			MyScreen.hTab = 0;
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x00014BC0 File Offset: 0x00012FC0
	public int collisionCmdBar(Command left, Command center, Command right)
	{
		if (HDPaint.imgButton == null)
		{
			return -1;
		}
		if (left != null)
		{
			if (left.x != -1)
			{
				if (Canvas.isPoint(left.x - MyScreen.wTab / 2, left.y, MyScreen.wTab, AvMain.hCmd))
				{
					return 0;
				}
			}
			else if (Canvas.isPoint(Canvas.posCmd[0].x, Canvas.hCan - PaintPopup.hButtonSmall - 3, MyScreen.wTab, AvMain.hCmd))
			{
				return 0;
			}
		}
		if (center != null)
		{
			if (center.x != -1)
			{
				if (Canvas.isPoint(center.x - PaintPopup.wButtonSmall / 2, center.y, PaintPopup.wButtonSmall, PaintPopup.hButtonSmall))
				{
					return 1;
				}
			}
			else if (Canvas.isPoint(Canvas.posCmd[1].x, Canvas.hCan - PaintPopup.hButtonSmall - 3, MyScreen.wTab, AvMain.hCmd))
			{
				return 1;
			}
		}
		if (right != null)
		{
			if (right.x != -1)
			{
				if (Canvas.isPoint(right.x - PaintPopup.wButtonSmall / 2, right.y, PaintPopup.wButtonSmall, PaintPopup.hButtonSmall))
				{
					return 2;
				}
			}
			else if (Canvas.isPoint(Canvas.posCmd[2].x, Canvas.hCan - PaintPopup.hButtonSmall - 3, MyScreen.wTab, AvMain.hCmd))
			{
				return 2;
			}
		}
		return -1;
	}

	// Token: 0x06000265 RID: 613 RVA: 0x00014D2A File Offset: 0x0001312A
	public void getSound(string path, int loop)
	{
	}

	// Token: 0x06000266 RID: 614 RVA: 0x00014D2C File Offset: 0x0001312C
	public void setSoundAnimalFarm()
	{
		if (this.listAnimalSound.size() > 0)
		{
			if (this.aa == -1)
			{
				this.aa = 50 + CRes.rnd(200);
			}
			else
			{
				this.aa--;
				if (this.aa == -1)
				{
					this.getSound((string)this.listAnimalSound.elementAt(CRes.rnd(this.listAnimalSound.size())), 1);
				}
			}
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x00014DAF File Offset: 0x000131AF
	public void setAnimalSound(MyVector animalLists)
	{
	}

	// Token: 0x06000268 RID: 616 RVA: 0x00014DB1 File Offset: 0x000131B1
	public void clickSound()
	{
	}

	// Token: 0x06000269 RID: 617 RVA: 0x00014DB4 File Offset: 0x000131B4
	public void paintCheckBox(MyGraphics g, int x, int y, int focus, bool isCheck)
	{
		int idx = 0;
		HDPaint.imgCheck.drawFrame(idx, x, y + Canvas.tempFont.getHeight() / 2, 0, g);
		if (isCheck)
		{
			HDPaint.imgCheck.drawFrame(1, x, y + Canvas.tempFont.getHeight() / 2, 0, g);
		}
		Canvas.tempFont.drawString(g, T.rememPass, x + 50, y + HDPaint.imgCheck.frameHeight / 2 - 4, 0);
	}

	// Token: 0x0600026A RID: 618 RVA: 0x00014E27 File Offset: 0x00013227
	public void paintSelected(MyGraphics g, int x, int y, int w, int h)
	{
		g.setColor(7138780);
		g.fillRect((float)(x - 3), (float)y, (float)(w + 6), (float)h);
	}

	// Token: 0x0600026B RID: 619 RVA: 0x00014E48 File Offset: 0x00013248
	public void paintArrow(MyGraphics g, int index, int x, int y, int w, int indLeft, int indRight)
	{
		g.drawImage(PaintPopup.imgMuiIOS[indLeft][2], (float)x, (float)y, 3);
		g.drawImage(PaintPopup.imgMuiIOS[indRight][3], (float)(x + w), (float)y, 3);
	}

	// Token: 0x0600026C RID: 620 RVA: 0x00014E79 File Offset: 0x00013279
	public void paintNormalFont(MyGraphics g, string str, int x, int y, int anthor, bool isSe)
	{
		if (!isSe)
		{
			Canvas.arialFont.drawString(g, str, x, y, anthor);
		}
		else
		{
			Canvas.normalFont.drawString(g, str, x, y, anthor);
		}
	}

	// Token: 0x0600026D RID: 621 RVA: 0x00014EA9 File Offset: 0x000132A9
	public int getWNormalFont(string str)
	{
		return Canvas.arialFont.getWidth(str);
	}

	// Token: 0x0600026E RID: 622 RVA: 0x00014EB6 File Offset: 0x000132B6
	public void paintSelected_2(MyGraphics g, int x, int y, int w, int h)
	{
		g.setColor(5299141);
		g.fillRect((float)x, (float)y, (float)w, (float)h);
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00014ED3 File Offset: 0x000132D3
	public void paintTransBack(MyGraphics g)
	{
		g.drawImageScale(HDPaint.imgTrans, 0, 0, Canvas.w, Canvas.hCan, 0);
	}

	// Token: 0x06000270 RID: 624 RVA: 0x00014EF0 File Offset: 0x000132F0
	public void initPosLogin(LoginScr lg, int h0)
	{
		lg.wLogin = 510;
		int num = 30;
		if (lg.isReg)
		{
			num = 10;
			lg.hLogin = lg.tfUser.height * 4 + num * 5 + PaintPopup.hButtonSmall / 2;
		}
		else
		{
			lg.hLogin = 310;
		}
		lg.hCellNew = (lg.hLogin - 40) / 3;
		lg.yNew = 20;
		lg.yLogin = h0 / 2 - lg.hLogin / 2;
		lg.xLogin = Canvas.hw - lg.wLogin / 2;
		int num2 = lg.yLogin + num + 4;
		lg.tfUser.width = (lg.tfPass.width = (lg.tfReg.width = (lg.tfEmail.width = 230)));
		lg.tfUser.y = num2;
		lg.tfUser.x = (lg.tfEmail.x = (lg.tfPass.x = (lg.tfReg.x = lg.xLogin + lg.wLogin - 230 - 60)));
		num2 += lg.tfUser.height + num;
		lg.tfPass.y = num2;
		num2 += lg.tfUser.height + num;
		lg.tfReg.y = num2;
		lg.yCheck = num2 - 10;
		lg.xCheck = lg.xLogin + 130;
		num2 += lg.tfUser.height + num;
		lg.tfEmail.y = num2;
	}

	// Token: 0x06000271 RID: 625 RVA: 0x00015094 File Offset: 0x00013494
	public void paintKeyArrow(MyGraphics g, int x, int y)
	{
		Canvas.resetTrans(g);
		g.drawImage(PaintPopup.imgMuiIOS[this.ind1][2], (float)(x - 80), (float)y, 3);
		g.drawImage(PaintPopup.imgMuiIOS[this.ind0][3], (float)(x + 80), (float)y, 3);
		g.drawImage(PaintPopup.imgMuiIOS[this.ind3][0], (float)x, (float)(y - 80), 3);
		g.drawImage(PaintPopup.imgMuiIOS[this.ind2][1], (float)x, (float)(y + 80), 3);
	}

	// Token: 0x06000272 RID: 626 RVA: 0x00015118 File Offset: 0x00013518
	public int updateKeyArr(int x, int y)
	{
		if (Canvas.isPointerClick)
		{
			if (Canvas.isPoint(x + 80 - 40, y - 40, 80, 80))
			{
				this.isTranFish = true;
				Canvas.isPointerClick = false;
				this.ind0 = 1;
			}
			else if (Canvas.isPoint(x - 80 - 40, y - 40, 80, 80))
			{
				this.isTranFish = true;
				Canvas.isPointerClick = false;
				this.ind1 = 1;
			}
			else if (Canvas.isPoint(x - 40, y + 80 - 40, 80, 80))
			{
				this.isTranFish = true;
				Canvas.isPointerClick = false;
				this.ind2 = 1;
			}
			else if (Canvas.isPoint(x - 40, y - 80 - 40, 80, 80))
			{
				this.isTranFish = true;
				Canvas.isPointerClick = false;
				this.ind3 = 1;
			}
		}
		if (this.isTranFish)
		{
			if (Canvas.isPointerDown)
			{
				if (!Canvas.isPoint(x + 80 - 40, y - 40, 80, 80))
				{
					this.ind0 = 0;
				}
				if (!Canvas.isPoint(x - 80 - 40, y - 40, 80, 80))
				{
					this.ind1 = 0;
				}
				if (!Canvas.isPoint(x - 40, y + 80 - 40, 80, 80))
				{
					this.ind2 = 0;
				}
				if (!Canvas.isPoint(x - 40, y - 80 - 40, 80, 80))
				{
					this.ind3 = 0;
				}
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranFish = false;
				if (this.ind0 == 1)
				{
					this.ind0 = 0;
					return 3;
				}
				if (this.ind1 == 1)
				{
					this.ind1 = 0;
					return 1;
				}
				if (this.ind2 == 1)
				{
					this.ind2 = 0;
					return 4;
				}
				if (this.ind3 == 1)
				{
					this.ind3 = 0;
					return 2;
				}
			}
		}
		return -1;
	}

	// Token: 0x06000273 RID: 627 RVA: 0x000152F3 File Offset: 0x000136F3
	public void setVirtualKeyFish(int index)
	{
	}

	// Token: 0x06000274 RID: 628 RVA: 0x000152F8 File Offset: 0x000136F8
	public void initPosPhom()
	{
		int h = Canvas.h;
		PBoardScr.posName = new AvPosition[]
		{
			new AvPosition(Canvas.hw + 5, 5, 0),
			new AvPosition(5, h / 2, 0),
			new AvPosition(Canvas.hw + 5, h - 50, 0),
			new AvPosition(Canvas.w - 5, h / 2, 1)
		};
		PBoardScr.posFinish = new AvPosition[]
		{
			new AvPosition(Canvas.hw, 2, 3),
			new AvPosition(10, h / 2, MyGraphics.TOP | MyGraphics.LEFT),
			new AvPosition(Canvas.hw - 10, h - 75 - MyScreen.hTab, 3),
			new AvPosition(Canvas.w - 60, h / 2, 3)
		};
		int num = Canvas.h - ((AvMain.hFillTab == 0) ? 24 : AvMain.hFillTab);
		int num2 = h - 15 - Canvas.hTab;
		PBoardScr.posCardShow = new AvPosition[]
		{
			new AvPosition(Canvas.hw, BoardScr.hcard / 2, 0),
			new AvPosition(BoardScr.wCard, num / 2, 0),
			new AvPosition(Canvas.hw, PBoardScr.posCard.y - BoardScr.hcard / 3 * 2, 0),
			new AvPosition(Canvas.w - BoardScr.wCard, num / 2, 0)
		};
		PBoardScr.posCardEat = new AvPosition[]
		{
			new AvPosition(Canvas.hw, 0, 0),
			new AvPosition(BoardScr.wCard / 2, num / 2, 0),
			new AvPosition(Canvas.hw, PBoardScr.posCard.y - BoardScr.hcard / 3, 0),
			new AvPosition(Canvas.w - BoardScr.wCard / 2, num / 2, 0)
		};
		PBoardScr.posNamePlaying = new AvPosition[]
		{
			new AvPosition(Canvas.hw, BoardScr.hcard + 2, 2),
			new AvPosition(BoardScr.wCard + BoardScr.wCard / 2 + 5, num / 2 - 10, 0),
			new AvPosition(Canvas.hw, PBoardScr.posCard.y - BoardScr.hcard / 3 * 2 - BoardScr.hcard / 2 - Canvas.smallFontYellow.getHeight() - 1, 2),
			new AvPosition(Canvas.w - BoardScr.wCard / 2 - BoardScr.wCard - 5, num / 2 - 10, 1)
		};
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0001554D File Offset: 0x0001394D
	public int initShop()
	{
		return 120;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x00015554 File Offset: 0x00013954
	public void initString(int type)
	{
		if (type == 0)
		{
			HDPaint.bank = "ATM";
			HDPaint.casino = "Hội đánh cờ";
			HDPaint.shop = "Cửa hàng";
			HDPaint.park = "Công viên";
			HDPaint.caro = "khu câu cá rô";
			HDPaint.caloc = "khu câu cá lóc";
			HDPaint.camap = "khu câu cá mập";
			HDPaint.cauca = "khu câu cá";
			HDPaint.prison = "Nhà giam";
			HDPaint.slum = "Khu ngoại ô";
			HDPaint.farmroad = "Đường vào nông trại";
			HDPaint.farm = "Nông trại";
			HDPaint.farmFriend = "Nông trại bạn bè";
			HDPaint.entertaiment = "Khu giải trí";
			HDPaint.salon = "Thẩm mỹ viện";
			HDPaint.store = "Nhà kho";
			HDPaint.food = "Thức ăn";
			HDPaint.petFood = "Thức ăn thú nuôi";
			HDPaint.eatPig = "Cho heo ăn";
			HDPaint.eatDog = "Cho bò ăn";
			HDPaint.getMilk = "Lấy sữa";
			HDPaint.getEgg = "Lấy trứng";
			HDPaint.topFarm = "TOP nông trại";
			HDPaint.fishing = "Câu cá";
			HDPaint.houseRoad = "Đường vào nhà";
			HDPaint.gotoHouse = "Vào nhà";
			HDPaint.quayVe = "Quày vé";
		}
		else
		{
			HDPaint.bank = "Bank";
			HDPaint.casino = "Casino";
			HDPaint.shop = "Shop";
			HDPaint.park = "Park";
			HDPaint.caro = " Area anabas";
			HDPaint.caloc = "Area Snakehead  fish";
			HDPaint.camap = " Area shark";
			HDPaint.cauca = " Area fishing";
			HDPaint.slum = "Area suburban";
			HDPaint.prison = "Prison";
			HDPaint.farmroad = "Farm road";
			HDPaint.farm = "Farm";
			HDPaint.farmFriend = "Farm Friend";
			HDPaint.entertaiment = "Entertaiment";
			HDPaint.salon = "Thẩm mỹ viện";
			HDPaint.store = "Warehouse";
			HDPaint.food = "Food";
			HDPaint.petFood = "Pet Food";
			HDPaint.eatPig = "Feed Pig";
			HDPaint.eatDog = "Feed Dog";
			HDPaint.getMilk = "Get Milk";
			HDPaint.getEgg = "Get Egg";
			HDPaint.topFarm = "TOP Farm";
			HDPaint.fishing = "Fishing";
			HDPaint.houseRoad = "House Road";
			HDPaint.gotoHouse = "Go to House";
			HDPaint.quayVe = "ticket agent";
			HDPaint.race = "Đua thú";
		}
	}

	// Token: 0x06000277 RID: 631 RVA: 0x00015794 File Offset: 0x00013B94
	public string doJoinGo(int x, int y)
	{
		int typeMap = LoadMap.getTypeMap(x, y);
		switch (typeMap)
		{
		case 52:
			return HDPaint.shop;
		case 53:
			return HDPaint.farmFriend;
		case 54:
			return HDPaint.fishing;
		case 55:
			return HDPaint.bank;
		case 56:
			break;
		case 57:
		case 62:
			return HDPaint.shop;
		case 58:
		case 63:
			return HDPaint.salon;
		case 59:
		case 64:
			return HDPaint.shop;
		default:
			switch (typeMap + 1)
			{
			case 0:
				return T.QuytA;
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 12:
				return HDPaint.park;
			case 10:
				return HDPaint.entertaiment;
			case 13:
				return HDPaint.park;
			case 14:
				return HDPaint.cauca;
			case 15:
				return HDPaint.caro;
			case 16:
				return HDPaint.caloc;
			case 17:
				return HDPaint.camap;
			case 18:
				return HDPaint.slum;
			case 19:
				return HDPaint.prison;
			case 22:
				return HDPaint.houseRoad;
			case 25:
				return HDPaint.farm;
			case 26:
				return HDPaint.farmroad;
			case 28:
				goto IL_1F4;
			case 29:
				return HDPaint.store;
			case 30:
				return T.joinA;
			}
			if (typeMap >= -125 && typeMap < 0)
			{
				return "Vào";
			}
			return null;
		case 68:
		case 69:
		case 70:
			return HDPaint.gotoHouse;
		case 71:
			return HDPaint.quayVe;
		case 72:
			return T.nameCasino[0];
		case 73:
			return T.nameCasino[1];
		case 76:
			return T.nameCasino[2];
		case 77:
			if (Canvas.iOpenOngame == 0)
			{
				return T.nameCasino[2];
			}
			return T.nameCasino[0];
		case 78:
			return HDPaint.petFood;
		case 83:
			return T.joinA;
		case 84:
			return HDPaint.eatPig;
		case 85:
			return HDPaint.eatDog;
		case 86:
			return HDPaint.getMilk;
		case 87:
			return HDPaint.getEgg;
		case 89:
			return HDPaint.topFarm;
		case 93:
			return HDPaint.food;
		case 107:
			return HDPaint.race;
		case 108:
		case 109:
			return HDPaint.casino;
		}
		IL_1F4:
		return T.joinA;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x00015A40 File Offset: 0x00013E40
	public bool selectedPointer(int xF, int yF)
	{
		if (xF > 0 && yF > 0 && CRes.distance(xF / AvMain.hd, yF / AvMain.hd, GameMidlet.avatar.x, GameMidlet.avatar.y) <= ((Canvas.currentMyScreen != FarmScr.instance) ? 70 : (300 * AvMain.hd)))
		{
			bool[] array = new bool[3];
			array[0] = true;
			bool flag = false;
			MyVector myVector = new MyVector();
			MyVector myVector2 = new MyVector();
			for (int i = 0; i < LoadMap.playerLists.size(); i++)
			{
				Base @base = (Base)LoadMap.playerLists.elementAt(i);
				if (@base.IDDB != GameMidlet.avatar.IDDB && (int)@base.catagory != 4 && ((@base.IDDB == GameMidlet.avatar.IDDB && Canvas.currentMyScreen != FarmScr.instance) || @base.IDDB != GameMidlet.avatar.IDDB) && CRes.abs(@base.x * AvMain.hd - xF) <= 40 && @base.y * AvMain.hd - yF < 60 && @base.y * AvMain.hd - yF > 0)
				{
					LoadMap.focusObj = @base;
					if ((int)@base.catagory == 0)
					{
						MapScr.focusP = (Avatar)@base;
					}
					array[1] = true;
					flag = true;
					string text = @base.name;
					if ((int)@base.catagory == 2)
					{
						text = string.Empty;
					}
					if (text.Length > 8)
					{
						text = text.Substring(0, 8) + "..";
					}
					if (myVector2.size() >= 6)
					{
						break;
					}
					@base.ableShow = true;
					AvPosition o = new AvPosition((int)((float)(@base.x * AvMain.hd) - AvCamera.gI().xCam - (float)Canvas.transTab), (int)((float)(@base.y * AvMain.hd) - AvCamera.gI().yCam - (float)Canvas.transTab), @base.IDDB);
					myVector.addElement(o);
					myVector2.addElement(new HDPaint.CommandPointer(text, new HDPaint.IActionPointer(@base), @base));
				}
			}
			int num = LoadMap.posFocus.x / AvMain.hd;
			int num2 = LoadMap.posFocus.y / AvMain.hd;
			string text2 = this.doJoinGo(num, num2);
			if (text2 == null)
			{
				text2 = this.doJoinGo(num - 24, num2);
			}
			if (text2 == null)
			{
				text2 = this.doJoinGo(num - 24, num2);
			}
			if (text2 == null)
			{
				this.doJoinGo(num, num2 - 24);
			}
			if (text2 == null)
			{
				text2 = this.doJoinGo(num, num2 + 24);
			}
			if (text2 != null)
			{
				array[2] = true;
				flag = true;
				myVector2.addElement(new HDPaint.CommandPointerGo(text2, new HDPaint.IActionPointerGO()));
				if (myVector2.size() == 1)
				{
					return false;
				}
			}
			else if (myVector2.size() == 1 && myVector.size() > 0)
			{
				((Command)myVector2.elementAt(0)).action.perform();
				AvPosition avPosition = (AvPosition)myVector.elementAt(0);
				Avatar avatar = LoadMap.getAvatar(avPosition.anchor);
				if (avatar != null)
				{
					avatar.ableShow = false;
				}
				return true;
			}
			if (myVector2.size() == 0)
			{
				GameMidlet.avatar.task = -5;
				LoadMap.isGo = false;
				Canvas.loadMap.change();
				return true;
			}
			if (flag)
			{
				MainMenu.popFocus = new PopupName(string.Empty, (int)((float)LoadMap.posFocus.x - AvCamera.gI().xCam), (int)((float)LoadMap.posFocus.y - AvCamera.gI().yCam));
				MainMenu.popFocus.iPrivate = 1;
				LoadMap.treeLists = LoadMap.orderVector(LoadMap.treeLists);
				LoadMap.isGo = true;
				LoadMap.nPath = 0;
				LoadMap.dirFocus = -1;
				MainMenu.gI().showCircle(myVector2, myVector);
				return true;
			}
			LoadMap.isGo = false;
		}
		return false;
	}

	// Token: 0x06000279 RID: 633 RVA: 0x00015E4C File Offset: 0x0001424C
	public void paintMSG(MyGraphics g)
	{
		Canvas.resetTrans(g);
		g.setColor(0);
		int num = 0;
		if (Canvas.setShowIconMenu())
		{
			if (Canvas.currentMyScreen == MapScr.instance)
			{
				num = 140;
				g.drawImage(HDPaint.iconMenu[this.indexMenu], 65f, (float)(35 + Canvas.countTab), 3);
			}
			else
			{
				num = 100;
				g.drawImage(HDPaint.iconMenu_2[this.indexMenu], 45f, (float)(41 + Canvas.countTab), 3);
			}
		}
		if (Canvas.setShowMsg())
		{
			g.drawImage(HDPaint.imgMSG[this.indexMSG], (float)(num + HDPaint.imgMSG[0].w / 2), (float)(40 + Canvas.countTab), 3);
			if (MessageScr.gI().isNewMsg)
			{
				g.drawImage(HDPaint.imgNewMsg, (float)(num + HDPaint.imgMSG[0].w / 2 + HDPaint.imgMSG[this.indexMSG].w / 2 - 6), (float)(40 + Canvas.countTab + HDPaint.imgMSG[this.indexMSG].h / 2 - 6), 3);
			}
		}
		if (Canvas.isPaintIconVir())
		{
			g.drawImage(MyScreen.imgChat[this.indexChat], (float)(num + HDPaint.imgMSG[0].w + 45), (float)(40 + Canvas.countTab), 3);
			if (Canvas.currentMyScreen != RaceScr.me && !Bus.isRun)
			{
				g.drawImage(HDPaint.iconRota[this.indexRota], (float)(Canvas.w - 40 - 100), (float)(40 + Canvas.countTab), 3);
			}
			if (!onMainMenu.isOngame && Canvas.currentMyScreen != RaceScr.me && (Canvas.menuMain == null || Canvas.menuMain != MenuIcon.me))
			{
				if ((int)GameMidlet.avatar.action != 14)
				{
					g.drawImage(HDPaint.iconAction[this.indexAction], (float)(Canvas.w - 40), (float)(40 + Canvas.countTab), 3);
				}
				g.drawImage(HDPaint.iconFeel[this.indexFeel], (float)(Canvas.w - 40), (float)(120 + Canvas.countTab), 3);
			}
		}
	}

	// Token: 0x0600027A RID: 634 RVA: 0x00016070 File Offset: 0x00014470
	public void setBack()
	{
		int num = 0;
		if (Canvas.setShowIconMenu())
		{
			if (Canvas.currentMyScreen == MapScr.instance)
			{
				num = 140;
			}
			else
			{
				num = 100;
			}
		}
		if (Canvas.isPointerClick)
		{
			this.isTranIcon = true;
			Canvas.isPointerClick = false;
			if (Canvas.setShowIconMenu() && ((Canvas.currentMyScreen == MapScr.instance && Canvas.isPoint(0, 0, 130, 70 + Canvas.countTab)) || (Canvas.currentMyScreen != MapScr.instance && Canvas.isPoint(0, 0, 90, 70 + Canvas.countTab))))
			{
				this.indexMenu = 1;
			}
			else if (Canvas.setShowMsg() && Canvas.isPoint(num - 10, 0, 80, 80 + Canvas.countTab))
			{
				this.indexMSG = 1;
			}
			else if (Canvas.isPaintIconVir() && Canvas.isPoint(num + HDPaint.imgMSG[0].w + 6, 0, 77, 80 + Canvas.countTab))
			{
				this.indexChat = 1;
			}
			else if (Canvas.isPaintIconVir() && (int)GameMidlet.avatar.action != 14 && !onMainMenu.isOngame && Canvas.currentMyScreen != RaceScr.me && (Canvas.menuMain == null || Canvas.menuMain != MenuIcon.me) && Canvas.isPoint(Canvas.w - 80, Canvas.countTab, 80, 80))
			{
				this.indexAction = 1;
			}
			else if (Canvas.isPaintIconVir() && !onMainMenu.isOngame && Canvas.currentMyScreen != RaceScr.me && (Canvas.menuMain == null || Canvas.menuMain != MenuIcon.me) && Canvas.isPoint(Canvas.w - 80, 80 + Canvas.countTab, 80, 80))
			{
				this.indexFeel = 1;
			}
			else if (Canvas.currentMyScreen != RaceScr.me && !Bus.isRun && Canvas.isPaintIconVir() && Canvas.isPoint(Canvas.w - 40 - 150, 40 + Canvas.countTab - 50, 100, 100))
			{
				this.indexRota = 1;
			}
			else
			{
				this.isTranIcon = false;
				Canvas.isPointerClick = true;
			}
		}
		if (this.isTranIcon)
		{
			if (Canvas.isPointerDown)
			{
				if (this.indexMenu == 1 && Canvas.currentMyScreen != MapScr.instance && !Canvas.isPoint(0, 0, 90, 70 + Canvas.countTab))
				{
					this.indexMenu = 0;
				}
				if (this.indexMenu == 1 && Canvas.currentMyScreen == MapScr.instance && !Canvas.isPoint(0, 0, 130, 70 + Canvas.countTab))
				{
					this.indexMenu = 0;
				}
				else if (this.indexMSG == 1 && !Canvas.isPoint(num - 10, 0, 80, 70 + Canvas.countTab))
				{
					this.indexMSG = 0;
				}
				else if (this.indexChat == 1 && !Canvas.isPoint(num + HDPaint.imgMSG[0].w + 6, 0, 77, 70 + Canvas.countTab))
				{
					this.indexChat = 0;
				}
				else if (this.indexAction == 1 && !Canvas.isPoint(Canvas.w - 80, Canvas.countTab, 80, 80))
				{
					this.indexAction = 0;
				}
				else if (this.indexFeel == 1 && !Canvas.isPoint(Canvas.w - 80, 80 + Canvas.countTab, 80, 80))
				{
					this.indexFeel = 0;
				}
				else if (this.indexRota == 1 && !Canvas.isPoint(Canvas.w - 40 - 150, 40 + Canvas.countTab - 50, 100, 100))
				{
					this.indexRota = 0;
				}
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranIcon = false;
				if (this.indexMenu == 1)
				{
					this.indexMenu = 0;
					Out.println("SET_BACKaaaaaaaaaaaaaaaaaaaaa");
					Canvas.currentMyScreen.doMenu();
				}
				else if (this.indexMSG == 1)
				{
					this.indexMSG = 0;
					MessageScr.gI().switchToMe();
				}
				else if (this.indexChat == 1)
				{
					this.indexChat = 0;
					ChatTextField.gI().showTF();
				}
				else if (this.indexAction == 1)
				{
					this.indexAction = 0;
					MapScr.gI().doSellectAction(Canvas.w - 40, 40 + Canvas.countTab);
				}
				else if (this.indexFeel == 1)
				{
					this.indexFeel = 0;
					MapScr.gI().doFeel(Canvas.w - 40, 120 + Canvas.countTab);
				}
				else if (this.indexRota == 1)
				{
					this.indexRota = 0;
					if (Canvas.isRotateTop == 0)
					{
						Out.println("rotation: " + Screen.orientation);
						if (Screen.orientation == 1)
						{
							Screen.orientation = 3;
							Canvas.isRotateTop = 2;
						}
						else
						{
							ChatTextField.gI().showTF();
							Screen.orientation = 1;
							Canvas.isRotateTop = 1;
						}
						Out.println(string.Concat(new object[]
						{
							"111: ",
							Screen.orientation,
							"    ",
							Canvas.isRotateTop
						}));
					}
				}
			}
		}
	}

	// Token: 0x0600027B RID: 635 RVA: 0x000165F4 File Offset: 0x000149F4
	public void setDrawPointer(Command left, Command center, Command right)
	{
		if (Canvas.isPointerClick)
		{
			int num = Canvas.paint.collisionCmdBar(left, center, right);
			if (num != 0)
			{
				if (num != 1)
				{
					if (num == 2)
					{
						if (right != null)
						{
							Canvas.isPointerClick = false;
							AvMain.indexRight = 4;
							this.isTranKeyPointer = true;
						}
					}
				}
				else if (center != null)
				{
					Canvas.isPointerClick = false;
					AvMain.indexCenter = 4;
					this.isTranKeyPointer = true;
				}
			}
			else if (left != null)
			{
				Canvas.isPointerClick = false;
				AvMain.indexLeft = 4;
				this.isTranKeyPointer = true;
			}
		}
		if (this.isTranKeyPointer && Canvas.isPointerDown)
		{
			int num2 = this.collisionCmdBar(left, center, right);
			if (AvMain.indexLeft != 0 && num2 != 0)
			{
				AvMain.indexLeft = 0;
			}
			if (AvMain.indexCenter != 0 && num2 != 1)
			{
				AvMain.indexCenter = 0;
			}
			if (AvMain.indexRight != 0 && num2 != 2)
			{
				AvMain.indexRight = 0;
			}
		}
	}

	// Token: 0x0600027C RID: 636 RVA: 0x000166EF File Offset: 0x00014AEF
	public void paintList(MyGraphics g, int w, int maxW, int maxH, bool isHide, int selected, int[] listBoard)
	{
	}

	// Token: 0x0600027D RID: 637 RVA: 0x000166F1 File Offset: 0x00014AF1
	public void setLanguage()
	{
		if (OptionScr.gI().mapFocus[4] == 1)
		{
			new TE();
			Canvas.paint.initString(1);
		}
		else
		{
			new T();
			Canvas.paint.initString(0);
		}
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0001672C File Offset: 0x00014B2C
	public void paintDefaultBg(MyGraphics g)
	{
		g.drawImageScale(OnSplashScr.imgBg, 0, 0, Canvas.w, Canvas.hCan, 0);
	}

	// Token: 0x0600027F RID: 639 RVA: 0x00016746 File Offset: 0x00014B46
	public void paintLogo(MyGraphics g, int x, int y)
	{
		g.drawImage(OnSplashScr.imgLogomainMenu, (float)x, (float)y, 3);
	}

	// Token: 0x06000280 RID: 640 RVA: 0x00016758 File Offset: 0x00014B58
	public void paintDefaultScrList(MyGraphics g, string title, string subTitle, string check)
	{
		g.setClip(0f, 0f, (float)Canvas.w, (float)Canvas.h);
		Canvas.paint.paintDefaultBg(g);
		Canvas.borderFont.drawString(g, title, Canvas.w / 2, 2, 2);
		g.setColor(6192786);
		g.fillRect(0f, 25f, (float)Canvas.w, (float)MyScreen.ITEM_HEIGHT);
		Canvas.arialFont.drawString(g, subTitle, 10, 28, 0);
		Canvas.arialFont.drawString(g, check, Canvas.w - 10, 28, 1);
	}

	// Token: 0x06000281 RID: 641 RVA: 0x000167F4 File Offset: 0x00014BF4
	public void initImgBoard(int type)
	{
		string str = string.Empty;
		if (type == 0)
		{
			str = "imgBan2";
		}
		else if (type == 1)
		{
			str = "imgBan4";
		}
		else
		{
			str = "imgBan5";
		}
		BoardListOnScr.imgBoard = new FrameImage(Image.createImagePNG("hd/hd/on/" + str), 162, 128);
		BoardListOnScr.gI().imgNumPlayer = Image.createImagePNG("hd/hd/on/imgNumPlayer");
		BoardListOnScr.gI().imgPlaying = Image.createImagePNG("hd/hd/on/imgPlay");
		BoardListOnScr.imgLock = Image.createImagePNG("hd/hd/on/imgLock");
		BoardListOnScr.imgSelectBoard = Image.createImagePNG("hd/hd/on/imgSelectban");
		BoardScr.imgBoard = Image.createImagePNG("hd/hd/on/imgTable");
		BoardScr.xBoard = Canvas.w / 2;
		BoardScr.yBoard = (Canvas.hCan - PaintPopup.hButtonSmall) / 2 + 15;
		BoardScr.wBoard = Canvas.w - 200;
		if (BoardScr.wBoard > 900)
		{
			BoardScr.wBoard = 900;
		}
		BoardScr.hBoard = Canvas.hCan - PaintPopup.hButtonSmall - 100;
		BoardScr.imgReady = new Image[2];
		BoardScr.imgReady[0] = Image.createImagePNG("hd/hd/on/ready");
		BoardScr.imgReady[1] = Image.createImagePNG("hd/hd/on/owner");
		BoardScr.imgBan = Image.createImagePNG("hd/hd/on/star");
	}

	// Token: 0x06000282 RID: 642 RVA: 0x00016944 File Offset: 0x00014D44
	public void setColorBar()
	{
		int num = (int)LoadMap.map[(int)((LoadMap.Hmap - 1) * LoadMap.wMap + 1)];
		if (num != -1)
		{
			int num2 = num / LoadMap.imgMap.nFrame;
			Image image = CRes.createImgByImg(num2 * 48, num % LoadMap.imgMap.nFrame * 48, 48, 48, LoadMap.imgMap.imgFrame);
			MyScreen.color = image.texture.GetPixel(0, 0);
			MyScreen.colorBar = -1;
		}
	}

	// Token: 0x06000283 RID: 643 RVA: 0x000169BC File Offset: 0x00014DBC
	public void initOngame()
	{
		try
		{
			MenuOn.imgTab = Image.createImagePNG("hd/hd/on/imgTabMenu");
			MsgDlg.imgLoadOn = Image.createImagePNG("hd/hd/on/loadingbg");
			MsgDlg.imgLoad = new FrameImage(Image.createImagePNG("hd/hd/on/busy"), 54, 36);
			RoomListOnScr.imgRoomStat = new FrameImage(Image.createImagePNG("hd/hd/on/imgStatus"), 54, 36);
			BoardListOnScr.gI().imgTitleBoard = Image.createImagePNG("hd/hd/on/imgkhungsoban");
			OnScreen.imgButtomSmall = new FrameImage(Image.createImagePNG("hd/hd/on/buttonSmall"), 60, 60);
			OnScreen.imgIconButton = new FrameImage(Image.createImagePNG("hd/hd/on/iconButton"), 60, 60);
			HDPaint.imgButtonOn = new FrameImage(Image.createImagePNG("hd/hd/on/imgButton"), 168, 56);
			RoomListOnScr.gI().imgTitleRoom = Image.createImagePNG("hd/hd/on/imgRoomtitle");
			BCBoardScr.pointer = Image.createImagePNG("hd/hd/on/p");
			PaintPopup.wButtonSmall = HDPaint.imgButtonOn.frameWidth;
			HDPaint.imgBar = Image.createImagePNG("hd/hd/on/imgBar");
			PaintPopup.hButtonSmall = HDPaint.imgBar.h;
			HDPaint.imgBarMoney = Image.createImagePNG("hd/hd/on/barMoney");
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		onMainMenu.initImg();
	}

	// Token: 0x06000284 RID: 644 RVA: 0x00016B08 File Offset: 0x00014F08
	public void resetOngame()
	{
		OnSplashScr.imgBg = null;
		HDPaint.imgButtonOn = null;
		HDPaint.imgPopup = null;
		HDPaint.imgPopup2 = null;
		Menu.me = null;
		HDPaint.imgBarMoney = null;
	}

	// Token: 0x06000285 RID: 645 RVA: 0x00016B2E File Offset: 0x00014F2E
	public void paintRoomList(MyGraphics g, MyVector roomList, int hSmall, int cmy)
	{
	}

	// Token: 0x06000286 RID: 646 RVA: 0x00016B30 File Offset: 0x00014F30
	public void setName(int index, BoardScr board)
	{
		if (!onMainMenu.isOngame)
		{
			RoomListOnScr.title = T.nameCasino[index];
		}
		else
		{
			RoomListOnScr.title = T.nameCasinoOngame[index];
		}
		CasinoMsgHandler.curScr = board;
	}

	// Token: 0x06000287 RID: 647 RVA: 0x00016B60 File Offset: 0x00014F60
	public void paintPlayer(MyGraphics g, int index, int male, int countLeft, int countRight)
	{
		g.setColor(5299141);
		g.fillRect(8f, (float)((int)PaintPopup.hTab + 90), (float)(PaintPopup.gI().w - 16), 80f);
		this.imgRegGender.drawFrame((male != 2) ? 0 : 1, PaintPopup.gI().w / 2 - 41, (int)PaintPopup.hTab + 45, 0, 3, g);
		this.imgRegGender.drawFrame((male != 2) ? 1 : 0, PaintPopup.gI().w / 2 + 41, (int)PaintPopup.hTab + 45, 0, 3, g);
		Canvas.blackF.drawString(g, T.gender[0], PaintPopup.gI().w / 2 - 42, (int)PaintPopup.hTab + 45 - (int)AvMain.hBlack / 2, 2);
		Canvas.blackF.drawString(g, T.gender[1], PaintPopup.gI().w / 2 + 42, (int)PaintPopup.hTab + 45 - (int)AvMain.hBlack / 2, 2);
		g.drawImage(PaintPopup.imgMuiIOS[countLeft / 3][2], (float)(PaintPopup.gI().w / 2 - 90), (float)((int)PaintPopup.hTab + 130), 3);
		g.drawImage(PaintPopup.imgMuiIOS[countRight / 3][3], (float)(PaintPopup.gI().w / 2 + 90), (float)((int)PaintPopup.hTab + 130), 3);
		GameMidlet.avatar.paintIcon(g, PaintPopup.gI().w / 2 + 1, (int)PaintPopup.hTab + 160, false);
		Canvas.resetTrans(g);
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00016CFC File Offset: 0x000150FC
	public void updateKeyRegister()
	{
		if (Canvas.isPointerClick)
		{
			if (Canvas.isPoint(PaintPopup.gI().x + PaintPopup.gI().w / 2 - 41 - 40, PaintPopup.gI().y + (int)PaintPopup.hTab + 45 - 30, 80, 60))
			{
				RegisterScr.gI().male = 1;
				RegisterScr.gI().getAvatarPart();
				Canvas.isPointerClick = false;
			}
			else if (Canvas.isPoint(PaintPopup.gI().x + PaintPopup.gI().w / 2 + 41 - 40, PaintPopup.gI().y + (int)PaintPopup.hTab + 45 - 30, 80, 60))
			{
				RegisterScr.gI().male = 2;
				RegisterScr.gI().getAvatarPart();
				Canvas.isPointerClick = false;
			}
			else if (Canvas.isPoint(PaintPopup.gI().x + PaintPopup.gI().w / 2 - 130, PaintPopup.gI().y + (int)PaintPopup.hTab + 90, 80, 80))
			{
				RegisterScr.gI().index = 1;
				RegisterScr.gI().setKeyLeftRight(-1);
				RegisterScr.gI().countLeft = 5;
				Canvas.isPointerClick = false;
			}
			else if (Canvas.isPoint(PaintPopup.gI().x + PaintPopup.gI().w / 2 + 50, PaintPopup.gI().y + (int)PaintPopup.hTab + 90, 80, 80))
			{
				RegisterScr.gI().index = 1;
				RegisterScr.gI().setKeyLeftRight(1);
				RegisterScr.gI().countRight = 5;
				Canvas.isPointerClick = false;
			}
		}
	}

	// Token: 0x06000289 RID: 649 RVA: 0x00016EA8 File Offset: 0x000152A8
	public void initReg()
	{
		try
		{
			this.imgRegGender = new FrameImage(Image.createImage(T.getPath() + "/gender"), 67, 40);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600028A RID: 650 RVA: 0x00016EFC File Offset: 0x000152FC
	public void paintMoney(MyGraphics g, int x, int y)
	{
		int width = Canvas.tempFont.getWidth(GameMidlet.avatar.money[0] + string.Empty);
		int width2 = Canvas.tempFont.getWidth(GameMidlet.avatar.money[2] + string.Empty);
		Canvas.tempFont.drawString(g, GameMidlet.avatar.money[0] + string.Empty, x, y, 0);
		g.drawImage(MyInfoScr.gI().imgIcon[0], (float)(x + width + 38), (float)(y + Canvas.tempFont.getHeight() / 2), 3);
		g.drawImage(MyInfoScr.gI().imgIcon[1], (float)(x + width2 + 140 + width), (float)(y + Canvas.tempFont.getHeight() / 2), 3);
		Canvas.tempFont.drawString(g, GameMidlet.avatar.money[2] + string.Empty, x + width + 100, y, 0);
	}

	// Token: 0x0600028B RID: 651 RVA: 0x00017004 File Offset: 0x00015404
	public void paintTabSoft(MyGraphics g)
	{
		int height = HDPaint.imgBar.getHeight();
		g.drawImageScale(HDPaint.imgBar, 0, Canvas.hCan - height, Canvas.w, height, 0);
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00017038 File Offset: 0x00015438
	public void paintCmdBar(MyGraphics g, Command left, Command center, Command right)
	{
		int num = Canvas.hCan - PaintPopup.hButtonSmall / 2;
		if (HDPaint.imgButtonOn == null)
		{
			return;
		}
		if (left != null && left.caption != string.Empty)
		{
			if (left.x != -1)
			{
				HDPaint.imgButtonOn.drawFrame((int)this.indexLeft, left.x + PaintPopup.wButtonSmall / 2, num, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, left.caption, left.x + PaintPopup.wButtonSmall / 2, num - Canvas.normalWhiteFont.getHeight() / 2, 2);
			}
			else
			{
				HDPaint.imgButtonOn.drawFrame((int)this.indexLeft, 4 + PaintPopup.wButtonSmall / 2, num, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, left.caption, 4 + PaintPopup.wButtonSmall / 2, num - Canvas.normalWhiteFont.getHeight() / 2, 2);
			}
		}
		if (center != null && center.caption != string.Empty)
		{
			if (center.x != -1)
			{
				HDPaint.imgButtonOn.drawFrame((int)this.indexCenter, center.x + PaintPopup.wButtonSmall / 2, num, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, center.caption, center.x + PaintPopup.wButtonSmall / 2, num - Canvas.normalWhiteFont.getHeight() / 2, 2);
			}
			else
			{
				HDPaint.imgButtonOn.drawFrame((int)this.indexCenter, Canvas.w / 2, num, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, center.caption, Canvas.w / 2, num - Canvas.normalWhiteFont.getHeight() / 2, 2);
			}
		}
		if (right != null && right.caption != string.Empty)
		{
			if (right.x != -1)
			{
				HDPaint.imgButtonOn.drawFrame((int)this.indexRight, right.x + PaintPopup.wButtonSmall / 2, num, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, right.caption, right.x + PaintPopup.wButtonSmall / 2, num - Canvas.normalWhiteFont.getHeight() / 2, 2);
			}
			else
			{
				HDPaint.imgButtonOn.drawFrame((int)this.indexRight, Canvas.w - PaintPopup.wButtonSmall / 2 - 4, num, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, right.caption, Canvas.w - PaintPopup.wButtonSmall / 2 - 4, num - Canvas.normalWhiteFont.getHeight() / 2, 2);
			}
		}
	}

	// Token: 0x0600028D RID: 653 RVA: 0x000172BC File Offset: 0x000156BC
	public void updateKeyOn(Command left, Command center, Command right)
	{
		if (Canvas.isPointerClick)
		{
			int num = HDPaint.pointCmdBar(left, center, right);
			if (num != 1)
			{
				if (num != 2)
				{
					if (num == 3)
					{
						this.indexRight = 1;
						Canvas.isPointerClick = false;
					}
				}
				else
				{
					Out.println("updateKeyOn");
					this.indexCenter = 1;
					Canvas.isPointerClick = false;
				}
			}
			else
			{
				this.indexLeft = 1;
				Canvas.isPointerClick = false;
			}
		}
		if (Canvas.isPointerDown)
		{
			int num2 = HDPaint.pointCmdBar(left, center, right);
			if (num2 != 1)
			{
				if (num2 != 2)
				{
					if (num2 == 3)
					{
						this.indexCenter = (this.indexLeft = 0);
					}
				}
				else
				{
					this.indexLeft = (this.indexRight = 0);
				}
			}
			else
			{
				this.indexCenter = (this.indexRight = 0);
			}
		}
		if (Canvas.isPointerRelease)
		{
			int num3 = HDPaint.pointCmdBar(left, center, right);
			if (num3 != 1)
			{
				if (num3 != 2)
				{
					if (num3 == 3)
					{
						if ((int)this.indexRight == 1)
						{
							this.indexRight = 0;
							right.perform();
							Canvas.isPointerRelease = false;
						}
					}
				}
				else if ((int)this.indexCenter == 1)
				{
					this.indexCenter = 0;
					center.perform();
					Canvas.isPointerRelease = false;
				}
			}
			else if ((int)this.indexLeft == 1)
			{
				left.perform();
				Canvas.isPointerRelease = false;
				this.indexLeft = 0;
			}
		}
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00017440 File Offset: 0x00015840
	public static int pointCmdBar(Command left, Command center, Command right)
	{
		if (left != null && !left.caption.Equals(string.Empty))
		{
			if (left.x != -1)
			{
				if (Canvas.isPoint(left.x, left.y, PaintPopup.wButtonSmall, PaintPopup.hButtonSmall))
				{
					return 1;
				}
			}
			else if (Canvas.isPoint(4, Canvas.hCan - PaintPopup.hButtonSmall, PaintPopup.wButtonSmall, PaintPopup.hButtonSmall))
			{
				return 1;
			}
		}
		if (center != null && !center.caption.Equals(string.Empty))
		{
			if (center.x != -1)
			{
				if (Canvas.isPoint(center.x, center.y, PaintPopup.wButtonSmall, PaintPopup.hButtonSmall))
				{
					return 2;
				}
			}
			else if (Canvas.isPoint(Canvas.w / 2 - PaintPopup.wButtonSmall / 2, Canvas.hCan - PaintPopup.hButtonSmall, PaintPopup.wButtonSmall, PaintPopup.hButtonSmall))
			{
				return 2;
			}
		}
		if (right != null && !right.caption.Equals(string.Empty))
		{
			if (right.x != -1)
			{
				if (Canvas.isPoint(right.x, right.y, PaintPopup.wButtonSmall, PaintPopup.hButtonSmall))
				{
					return 3;
				}
			}
			else if (Canvas.isPoint(Canvas.w - 4 - PaintPopup.wButtonSmall, Canvas.hCan - PaintPopup.hButtonSmall, PaintPopup.wButtonSmall, PaintPopup.hButtonSmall))
			{
				return 3;
			}
		}
		return -1;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x000175B8 File Offset: 0x000159B8
	public void paintDefaultPopup(MyGraphics g, int x, int y, int w, int h)
	{
		HDPaint.paintBorder(g, x, y, w, h, true);
	}

	// Token: 0x06000290 RID: 656 RVA: 0x000175C8 File Offset: 0x000159C8
	public static void paintBorder(MyGraphics g, int x, int y, int w, int h, bool paintTop)
	{
		int width = HDPaint.imgPopup[0].getWidth();
		int height = HDPaint.imgPopup[0].getHeight();
		if (paintTop)
		{
			g.drawImage(HDPaint.imgPopup[0], (float)x, (float)y, 0);
			for (int i = 1; i < w / width - 1; i++)
			{
				g.drawImage(HDPaint.imgPopup[1], (float)(x + width * i), (float)y, 0);
			}
			g.drawImage(HDPaint.imgPopup[1], (float)(x + w - width * 2), (float)y, 0);
			g.drawImage(HDPaint.imgPopup[2], (float)(x + w - width), (float)y, 0);
		}
		if (h / height > 2)
		{
			for (int j = 1; j < h / height; j++)
			{
				g.drawImage(HDPaint.imgPopup[3], (float)x, (float)(y + height * j), 0);
				g.drawImage(HDPaint.imgPopup[4], (float)(x + w - width), (float)(y + height * j), 0);
			}
			g.drawImage(HDPaint.imgPopup[3], (float)x, (float)(y + h - height * 2), 0);
			g.drawImage(HDPaint.imgPopup[4], (float)(x + w - width), (float)(y + h - height * 2), 0);
		}
		if (h > height * 2 - 10 * ScaleGUI.numScale && h <= height * 3)
		{
			g.drawImage(HDPaint.imgPopup[3], (float)x, (float)(y + h / 2 - height / 2), 0);
			g.drawImage(HDPaint.imgPopup[4], (float)(x + w - width), (float)(y + h / 2 - height / 2), 0);
		}
		g.drawImage(HDPaint.imgPopup[5], (float)x, (float)(y + h - height), 0);
		for (int k = 1; k < w / width - 1; k++)
		{
			g.drawImage(HDPaint.imgPopup[6], (float)(x + width * k), (float)(y + h - height), 0);
		}
		g.drawImage(HDPaint.imgPopup[6], (float)(x + w - width * 2), (float)(y + h - height), 0);
		g.drawImage(HDPaint.imgPopup[7], (float)(x + w - width), (float)(y + h - height), 0);
		g.setColor(HDPaint.colorNormal);
		g.fillRect((float)(x + 20), (float)(y + 20), (float)(w - 40), (float)(h - 40));
	}

	// Token: 0x06000291 RID: 657 RVA: 0x000177EC File Offset: 0x00015BEC
	public void paintLineRoom(MyGraphics g, int x, int y, int xTo, int yTo)
	{
		if (ScaleGUI.numScale == 1)
		{
			g.setColor(HDPaint.colorBold);
			g.fillRect((float)x, (float)(y + 1), (float)(xTo - x), (float)(yTo - y + 1));
		}
		else
		{
			g.setColor(HDPaint.colorBold);
			g.fillRect((float)x, (float)(y + 1), (float)(xTo - x), (float)(yTo - y + 2));
		}
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0001784F File Offset: 0x00015C4F
	public void paintSelect(MyGraphics g, int x, int y, int w, int h)
	{
		g.setColor(HDPaint.colorSelect);
		g.fillRect((float)x, (float)y, (float)w, (float)h);
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0001786C File Offset: 0x00015C6C
	public void paintBorderTitle(MyGraphics g, int x, int y, int w, int h)
	{
		HDPaint.paintBorder(g, x, y, w, h, false);
		int width = HDPaint.imgPopup2[0].getWidth();
		g.drawImage(HDPaint.imgPopup2[0], (float)x, (float)y, 0);
		for (int i = 1; i < w / width - 1; i++)
		{
			g.drawImage(HDPaint.imgPopup2[1], (float)(x + width * i), (float)y, 0);
		}
		g.drawImage(HDPaint.imgPopup2[1], (float)(x + w - width * 2), (float)y, 0);
		g.drawImage(HDPaint.imgPopup2[2], (float)(x + w - width), (float)y, 0);
	}

	// Token: 0x06000294 RID: 660 RVA: 0x00017902 File Offset: 0x00015D02
	public void paintTransMoney(MyGraphics g, int x, int y, int w, int h)
	{
		g.drawImage(HDPaint.imgBarMoney, (float)(x + w / 2), (float)(y + h / 2 + 4), 3);
	}

	// Token: 0x04000304 RID: 772
	public static FrameImage imgPopupBack;

	// Token: 0x04000305 RID: 773
	public static FrameImage imgEffectBack;

	// Token: 0x04000306 RID: 774
	public static FrameImage imgNotFocusTab;

	// Token: 0x04000307 RID: 775
	public static FrameImage imgFocusTab;

	// Token: 0x04000308 RID: 776
	public static Image imgCardBg;

	// Token: 0x04000309 RID: 777
	public static Image imgCardBg1;

	// Token: 0x0400030A RID: 778
	public static Image imgCardBg2;

	// Token: 0x0400030B RID: 779
	public static Image imgBar;

	// Token: 0x0400030C RID: 780
	public static Image imgBarMoney;

	// Token: 0x0400030D RID: 781
	public static Image[] imgCardIcon;

	// Token: 0x0400030E RID: 782
	public static Image[] imgButton;

	// Token: 0x0400030F RID: 783
	public static Image[] imgPopupBackNum;

	// Token: 0x04000310 RID: 784
	public static FrameImage[] imgCardNumber;

	// Token: 0x04000311 RID: 785
	public static FrameImage imgCheck;

	// Token: 0x04000312 RID: 786
	public static FrameImage imgButtonOn;

	// Token: 0x04000313 RID: 787
	public static FrameImage imgEraser;

	// Token: 0x04000314 RID: 788
	public static Image[] iconMenu;

	// Token: 0x04000315 RID: 789
	public static Image[] iconAction;

	// Token: 0x04000316 RID: 790
	public static Image[] iconFeel;

	// Token: 0x04000317 RID: 791
	public static Image[] imgMSG;

	// Token: 0x04000318 RID: 792
	public static Image[] iconMenu_2;

	// Token: 0x04000319 RID: 793
	public static Image[] iconRota;

	// Token: 0x0400031A RID: 794
	public static Image[] imgPopup;

	// Token: 0x0400031B RID: 795
	public static Image[] imgPopup2;

	// Token: 0x0400031C RID: 796
	public static int colorSelect = 35217;

	// Token: 0x0400031D RID: 797
	public static int colorBold = 16709;

	// Token: 0x0400031E RID: 798
	public static int colorNormal = 23135;

	// Token: 0x0400031F RID: 799
	public static int colorLight = 10276804;

	// Token: 0x04000320 RID: 800
	public static int colorInfoPopup = 10461344;

	// Token: 0x04000321 RID: 801
	private static Image imgNotFocusTab_1;

	// Token: 0x04000322 RID: 802
	private static Image imgFocusTab_1;

	// Token: 0x04000323 RID: 803
	private static Image imgNewMsg;

	// Token: 0x04000324 RID: 804
	private static Image imgTrans;

	// Token: 0x04000325 RID: 805
	private int wwCard = 72;

	// Token: 0x04000326 RID: 806
	private int hhCard = 97;

	// Token: 0x04000327 RID: 807
	private int aa = -1;

	// Token: 0x04000328 RID: 808
	private MyVector listAnimalSound = new MyVector();

	// Token: 0x04000329 RID: 809
	private Player soundClick;

	// Token: 0x0400032A RID: 810
	private static int point;

	// Token: 0x0400032B RID: 811
	public int ind0;

	// Token: 0x0400032C RID: 812
	public int ind1;

	// Token: 0x0400032D RID: 813
	public int ind2;

	// Token: 0x0400032E RID: 814
	public int ind3;

	// Token: 0x0400032F RID: 815
	private bool isTranFish;

	// Token: 0x04000330 RID: 816
	public static string bank;

	// Token: 0x04000331 RID: 817
	public static string casino;

	// Token: 0x04000332 RID: 818
	public static string shop;

	// Token: 0x04000333 RID: 819
	public static string park;

	// Token: 0x04000334 RID: 820
	public static string caro;

	// Token: 0x04000335 RID: 821
	public static string caloc;

	// Token: 0x04000336 RID: 822
	public static string camap;

	// Token: 0x04000337 RID: 823
	public static string cauca;

	// Token: 0x04000338 RID: 824
	public static string prison;

	// Token: 0x04000339 RID: 825
	public static string slum;

	// Token: 0x0400033A RID: 826
	public static string farmroad;

	// Token: 0x0400033B RID: 827
	public static string farm;

	// Token: 0x0400033C RID: 828
	public static string farmFriend;

	// Token: 0x0400033D RID: 829
	public static string entertaiment;

	// Token: 0x0400033E RID: 830
	public static string salon;

	// Token: 0x0400033F RID: 831
	public static string store;

	// Token: 0x04000340 RID: 832
	public static string food;

	// Token: 0x04000341 RID: 833
	public static string petFood;

	// Token: 0x04000342 RID: 834
	public static string eatPig;

	// Token: 0x04000343 RID: 835
	public static string eatDog;

	// Token: 0x04000344 RID: 836
	public static string getMilk;

	// Token: 0x04000345 RID: 837
	public static string getEgg;

	// Token: 0x04000346 RID: 838
	public static string topFarm;

	// Token: 0x04000347 RID: 839
	public static string fishing;

	// Token: 0x04000348 RID: 840
	public static string houseRoad;

	// Token: 0x04000349 RID: 841
	public static string gotoHouse;

	// Token: 0x0400034A RID: 842
	public static string quayVe;

	// Token: 0x0400034B RID: 843
	public static string race;

	// Token: 0x0400034C RID: 844
	private int indexAction;

	// Token: 0x0400034D RID: 845
	private int indexFeel;

	// Token: 0x0400034E RID: 846
	private int indexMenu;

	// Token: 0x0400034F RID: 847
	private int indexMSG;

	// Token: 0x04000350 RID: 848
	private int indexChat;

	// Token: 0x04000351 RID: 849
	private int indexRota;

	// Token: 0x04000352 RID: 850
	private bool isTranIcon;

	// Token: 0x04000353 RID: 851
	private bool isTranKeyPointer;

	// Token: 0x04000354 RID: 852
	private FrameImage imgRegGender;

	// Token: 0x04000355 RID: 853
	private sbyte indexLeft;

	// Token: 0x04000356 RID: 854
	private sbyte indexCenter;

	// Token: 0x04000357 RID: 855
	private sbyte indexRight;

	// Token: 0x02000045 RID: 69
	private class CommandPointerGo : Command
	{
		// Token: 0x06000295 RID: 661 RVA: 0x00017920 File Offset: 0x00015D20
		public CommandPointerGo(string name, HDPaint.IActionPointerGO a) : base(name, a)
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0001792A File Offset: 0x00015D2A
		public override void paint(MyGraphics g, int x, int y)
		{
			MainMenu.imgGO.drawFrame(1, x, y, 0, 3, g);
		}
	}

	// Token: 0x02000046 RID: 70
	private class IActionPointerGO : IAction
	{
		// Token: 0x06000298 RID: 664 RVA: 0x00017944 File Offset: 0x00015D44
		public void perform()
		{
			int num = LoadMap.posFocus.x / AvMain.hd;
			int num2 = LoadMap.posFocus.y / AvMain.hd;
			if (Canvas.loadMap.doJoin(num, num2) || Canvas.loadMap.doJoin(num + 24, num2) || Canvas.loadMap.doJoin(num - 24, num2) || Canvas.loadMap.doJoin(num, num2 + 24) || Canvas.loadMap.doJoin(num, num2 - 24))
			{
			}
		}
	}

	// Token: 0x02000047 RID: 71
	private class CommandPointer : Command
	{
		// Token: 0x06000299 RID: 665 RVA: 0x000179D4 File Offset: 0x00015DD4
		public CommandPointer(string name, HDPaint.IActionPointer a, Base b) : base(name, a)
		{
			this.b = b;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000179E5 File Offset: 0x00015DE5
		public override void paint(MyGraphics g, int x, int y)
		{
			this.b.paintIcon(g, x, y + (int)(this.b.height / 2), false);
		}

		// Token: 0x04000358 RID: 856
		private Base b;
	}

	// Token: 0x02000048 RID: 72
	private class IActionPointer : IAction
	{
		// Token: 0x0600029B RID: 667 RVA: 0x00017A04 File Offset: 0x00015E04
		public IActionPointer(Base b)
		{
			this.b = b;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00017A14 File Offset: 0x00015E14
		public void perform()
		{
			LoadMap.focusObj = this.b;
			if ((int)this.b.catagory == 5)
			{
				ParkService.gI().doGetDropPart(((Drop_Part)LoadMap.focusObj).ID);
				return;
			}
			if ((int)this.b.catagory == 0)
			{
				MapScr.focusP = (Avatar)this.b;
			}
			if (LoadMap.focusObj != null)
			{
				MainMenu.gI().avaPaint = new AvPosition((int)((float)(LoadMap.focusObj.x * AvMain.hd) * AvMain.zoom - AvCamera.gI().xCam), (int)((float)(LoadMap.focusObj.y * AvMain.hd) * AvMain.zoom - AvCamera.gI().yCam));
			}
			if (Canvas.currentMyScreen == FarmScr.instance)
			{
				FarmScr.gI().commandTab(2);
			}
			else if (MapScr.focusP.IDDB > 2000000000)
			{
				Canvas.startWaitDlg();
				GlobalService.gI().doCommunicate(MapScr.focusP.IDDB);
			}
			else
			{
				MainMenu.gI().doExchange();
			}
		}

		// Token: 0x04000359 RID: 857
		private Base b;
	}
}
