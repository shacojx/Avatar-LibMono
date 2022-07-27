using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class MediumPaint : IPaint
{
	// Token: 0x06000304 RID: 772 RVA: 0x00018C88 File Offset: 0x00017088
	static MediumPaint()
	{
		MediumPaint.cardIconInfo[0] = new sbyte[]
		{
			4,
			6,
			17,
			0,
			27,
			14,
			0,
			27,
			36
		};
		MediumPaint.cardIconInfo[1] = new sbyte[]
		{
			4,
			6,
			17,
			0,
			17,
			13,
			0,
			37,
			13
		};
		MediumPaint.cardIconInfo[2] = new sbyte[]
		{
			4,
			6,
			17,
			0,
			17,
			13,
			0,
			37,
			13,
			0,
			27,
			36
		};
		MediumPaint.cardIconInfo[3] = new sbyte[]
		{
			4,
			6,
			17,
			0,
			17,
			13,
			0,
			37,
			13,
			0,
			17,
			36,
			0,
			37,
			36
		};
		MediumPaint.cardIconInfo[4] = new sbyte[]
		{
			4,
			6,
			17,
			0,
			17,
			13,
			0,
			37,
			13,
			0,
			17,
			36,
			0,
			37,
			36,
			0,
			27,
			30
		};
		MediumPaint.cardIconInfo[5] = new sbyte[]
		{
			4,
			6,
			17,
			0,
			17,
			13,
			0,
			37,
			13,
			0,
			17,
			28,
			0,
			37,
			28
		};
		MediumPaint.cardIconInfo[6] = new sbyte[]
		{
			4,
			6,
			17,
			0,
			17,
			13,
			0,
			37,
			13,
			0,
			17,
			28,
			0,
			37,
			28,
			0,
			27,
			36
		};
		MediumPaint.cardIconInfo[7] = new sbyte[]
		{
			4,
			6,
			17,
			0,
			17,
			13,
			0,
			37,
			13,
			0,
			17,
			28,
			0,
			37,
			28,
			0,
			27,
			20
		};
		MediumPaint.cardIconInfo[8] = new sbyte[]
		{
			4,
			6,
			17,
			8,
			27,
			36
		};
		MediumPaint.cardIconInfo[9] = new sbyte[]
		{
			4,
			6,
			17,
			9,
			27,
			36
		};
		MediumPaint.cardIconInfo[10] = new sbyte[]
		{
			4,
			6,
			17,
			10,
			27,
			36
		};
		MediumPaint.cardIconInfo[11] = new sbyte[]
		{
			4,
			6,
			17,
			0,
			27,
			36
		};
		MediumPaint.cardIconInfo[12] = new sbyte[]
		{
			4,
			6,
			17,
			0,
			27,
			14
		};
		TField.xDu = 8;
		TField.yDu = 8;
		PaintPopup.color = new int[]
		{
			3521446,
			2378578,
			8052436,
			2716523,
			13621272,
			7042560
		};
		Canvas.imagePlug = Image.createImagePNG(T.getPath() + "/12Plus");
		Avatar.imgHit = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/5"), 50, 48);
		Avatar.imgKiss = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/2"), 11, 10);
		Canvas.imgTabInfo = Image.createImage(T.getPath() + "/effect/transtab");
		MapScr.imgBar = Image.createImagePNG(T.getPath() + "/effect/bar");
		Pet.imgShadow[0] = Image.createImage(T.getPath() + "/effect/s1");
		Pet.imgShadow[1] = Image.createImage(T.getPath() + "/effect/s2");
		Menu.imgSellect = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/cmd"), 24, 24);
		MainMenu.imgGO = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/go"), 24, 24);
		MapScr.imgFocusP = Image.createImagePNG(T.getPath() + "/effect/arF");
		Avatar.imgBlog = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/dauhoathi"), 9, 9);
		MediumPaint.imgEraser = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/eraser"), 13, 13);
		MediumPaint.imgCheck = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/check"), 22, 22);
		TField.tfframe = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/tb"), 25, 28);
		MyScreen.imgChat = new Image[2];
		MediumPaint.imgMSG = new Image[2];
		ListScr.imgCloseTab = new Image[2];
		ListScr.imgCloseTabFull = new Image[2];
		for (int i = 0; i < 2; i++)
		{
			MyScreen.imgChat[i] = Image.createImagePNG(T.getPath() + "/iconMenu/chat" + i);
			MediumPaint.imgMSG[i] = Image.createImagePNG(T.getPath() + "/iconMenu/msg" + i);
			ListScr.imgCloseTabFull[i] = Image.createImagePNG(T.getPath() + "/iconMenu/close" + i);
			ListScr.imgCloseTab[i] = Image.createImagePNG(T.getPath() + "/iconMenu/closenot" + i);
		}
		MediumPaint.imgTrans = Image.createImage(T.getPath() + "/effect/trans");
		MyScreen.imgChat = new Image[2];
		for (int j = 0; j < 2; j++)
		{
			MyScreen.imgChat[j] = Image.createImagePNG(T.getPath() + "/iconMenu/chat" + j);
		}
		PaintPopup.imgMuiIOS = new Image[2][];
		for (int k = 0; k < 2; k++)
		{
			PaintPopup.imgMuiIOS[k] = new Image[4];
			for (int l = 0; l < 4; l++)
			{
				PaintPopup.imgMuiIOS[k][l] = Image.createImagePNG(string.Concat(new object[]
				{
					T.getPath(),
					"/ios/a",
					k,
					string.Empty,
					l
				}));
			}
		}
		MsgDlg.hCell = PaintPopup.imgMuiIOS[0][2].getHeight() + 4;
		AvMain.hCmd = 29;
		MediumPaint.imgPopupBack = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/popupBack"), 20, 20);
		MediumPaint.imgPopupBackNum = new Image[4];
		for (int m = 0; m < 4; m++)
		{
			MediumPaint.imgPopupBackNum[m] = Image.createImagePNG(T.getPath() + "/effect/popupBack" + m);
		}
		MediumPaint.imgEffectBack = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/effectPopupBack"), 100, 50);
		MediumPaint.imgFocusTab = new FrameImage(Image.createImagePNG(T.getPath() + "/iconMenu/tabnotfocus"), 12, 42);
		MediumPaint.imgNotFocusTab = new FrameImage(Image.createImagePNG(T.getPath() + "/iconMenu/tabfocus"), 12, 35);
		MediumPaint.imgFocusTab_1 = Image.createImagePNG(T.getPath() + "/iconMenu/tabnotfocus1");
		MediumPaint.imgNotFocusTab_1 = Image.createImagePNG(T.getPath() + "/iconMenu/tabfocus1");
		MediumPaint.imgNewMsg = Image.createImagePNG(T.getPath() + "/iconMenu/imgMessIn");
	}

	// Token: 0x06000306 RID: 774 RVA: 0x000192D8 File Offset: 0x000176D8
	public void loadImgAvatar()
	{
		MediumPaint.iconMenu = new Image[2];
		MediumPaint.iconFeel = new Image[2];
		MediumPaint.iconAction = new Image[2];
		MediumPaint.iconMenu_2 = new Image[2];
		MediumPaint.iconRota = new Image[2];
		for (int i = 0; i < 2; i++)
		{
			MediumPaint.iconMenu[i] = Image.createImagePNG(T.getPath() + "/iconMenu/menuAction" + i);
			MediumPaint.iconAction[i] = Image.createImagePNG(T.getPath() + "/iconMenu/action" + i);
			MediumPaint.iconFeel[i] = Image.createImagePNG(T.getPath() + "/iconMenu/feel" + i);
			MediumPaint.iconMenu_2[i] = Image.createImagePNG(T.getPath() + "/iconMenu/menu" + i);
			MediumPaint.iconRota[i] = Image.createImagePNG(T.getPath() + "/iconMenu/rota" + i);
		}
		MsgDlg.imgLoad = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/busy"), 16, 16);
		Menu.imgBackIcon = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/backcmd"), 30, 30);
		MediumPaint.imgMenuTab = Image.createImagePNG(T.getPath() + "/temp/menuTab");
		MediumPaint.imgButton = new Image[2];
		for (int j = 0; j < 2; j++)
		{
			MediumPaint.imgButton[j] = Image.createImagePNG(T.getPath() + "/effect/button" + j);
		}
		PaintPopup.wButtonSmall = MediumPaint.imgButton[1].getWidth();
		PaintPopup.hButtonSmall = MediumPaint.imgButton[1].getHeight();
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00019490 File Offset: 0x00017890
	public void clearImgAvatar()
	{
		MediumPaint.iconMenu = null;
		Menu.imgBackIcon = null;
		MediumPaint.imgMenuTab = null;
		MediumPaint.imgButton = null;
		MediumPaint.iconAction = (MediumPaint.iconFeel = null);
	}

	// Token: 0x06000308 RID: 776 RVA: 0x000194B8 File Offset: 0x000178B8
	public void paintTextBox(MyGraphics g, int x, int y, int width, int height, TField tf, bool isFocus, sbyte indexEraser)
	{
		Canvas.resetTrans(g);
		TField.tfframe.drawFrame(0, x, y, 0, g);
		TField.tfframe.drawFrame(1, x + width - TField.tfframe.frameWidth, y, 0, g);
		g.setColor(16777215);
		g.fillRect((float)(x + TField.tfframe.frameWidth), (float)(y + 1), (float)(width - TField.tfframe.frameWidth * 2), (float)(height - 2));
		g.setColor(2720192);
		g.fillRect((float)(x + TField.tfframe.frameWidth), (float)y, (float)(width - TField.tfframe.frameWidth * 2), 1f);
		g.fillRect((float)(x + TField.tfframe.frameWidth), (float)(y + height - 1), (float)(width - TField.tfframe.frameWidth * 2), 1f);
		if (tf.isFocused() && !tf.paintedText.Equals(string.Empty))
		{
			MediumPaint.imgEraser.drawFrame((int)indexEraser, x + width - 10, y + height / 2, 0, 3, g);
		}
		g.setClip((float)(x + 2), (float)(y + 1), (float)(width - 4), (float)(height - 2));
		g.setColor(0);
		if (tf.paintedText.Equals(string.Empty))
		{
			Canvas.fontChatB.drawString(g, tf.sDefaust, TField.TEXT_GAP_X + tf.offsetX + x + 3, y + (height - Canvas.fontChatB.getHeight()) / 2 - 2, 0);
		}
		else
		{
			Canvas.fontChatB.drawString(g, tf.paintedText, TField.TEXT_GAP_X + tf.offsetX + x, y + (height - Canvas.fontChatB.getHeight()) / 2 - ((tf.inputType != ipKeyboard.PASS) ? 4 : 0), 0);
		}
		if (tf.isFocused() && tf.keyInActiveState == 0 && (tf.showCaretCounter > 0 || tf.counter / 5 % 2 == 0))
		{
			g.setColor(0);
			g.fillRect((float)(TField.TEXT_GAP_X + tf.offsetX + x + Canvas.fontChatB.getWidth(tf.paintedText.Substring(0, tf.caretPos) + "a") - Canvas.fontChatB.getWidth("a") - 1 + 1), (float)(y + 6), 1f, (float)(height - 12));
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0001972C File Offset: 0x00017B2C
	public void paintBoxTab(MyGraphics g, int x, int y, int h, int w, int focusTab, int wSub, int wTab, int hTab, int numTab, int maxTab, int[] count, int[] colorTab, string name, sbyte countCloseAll, sbyte countText, bool isMenu, bool isFull, string[] subName, float cmx, Image[][] imgIcon)
	{
		Canvas.resetTrans(g);
		int num = PaintPopup.xTab;
		int num2 = PaintPopup.wTabDu;
		num = 12;
		num2 = wTab + 10;
		g.setColor(0);
		for (int i = 0; i < numTab; i++)
		{
			if (i != focusTab)
			{
				MediumPaint.imgNotFocusTab.drawFrame(0, num + x + i * num2 + num2 / 2 - wTab / 2, y + hTab - MediumPaint.imgNotFocusTab.frameHeight, 0, g);
				MediumPaint.imgNotFocusTab.drawFrame(1, num + x + i * num2 + num2 / 2 + wTab / 2 - MediumPaint.imgNotFocusTab.frameWidth, y + hTab - MediumPaint.imgNotFocusTab.frameHeight, 0, g);
				g.drawImageScale(MediumPaint.imgNotFocusTab_1, num + x + i * num2 + num2 / 2 - wTab / 2 + MediumPaint.imgNotFocusTab.frameWidth, y + hTab - MediumPaint.imgNotFocusTab.frameHeight, wTab - MediumPaint.imgNotFocusTab.frameWidth * 2, MediumPaint.imgNotFocusTab.frameHeight, 0);
				if (imgIcon != null)
				{
					g.drawImage(imgIcon[i][1], (float)(num + x + i * num2 + num2 / 2), (float)(y + hTab - MediumPaint.imgNotFocusTab.frameHeight / 2 + 4), 3);
				}
				else
				{
					Canvas.fontWhiteBold.drawString(g, subName[i], num + x + i * num2 + num2 / 2, y + hTab - MediumPaint.imgNotFocusTab.frameHeight / 2 - Canvas.fontWhiteBold.getHeight() / 2, 2);
				}
			}
		}
		this.paintPopupBack(g, x, y + hTab, w, h - hTab, -1, isFull);
		MediumPaint.imgFocusTab.drawFrame(0, num + x + focusTab * num2 + num2 / 2 - wTab / 2, y + hTab - MediumPaint.imgFocusTab.frameHeight + 2, 0, g);
		MediumPaint.imgFocusTab.drawFrame(1, num + x + focusTab * num2 + num2 / 2 + wTab / 2 - MediumPaint.imgFocusTab.frameWidth, y + hTab - MediumPaint.imgFocusTab.frameHeight + 2, 0, g);
		g.drawImageScale(MediumPaint.imgFocusTab_1, num + x + focusTab * num2 + num2 / 2 - wTab / 2 + MediumPaint.imgFocusTab.frameWidth, y + hTab - MediumPaint.imgFocusTab.frameHeight + 2, wTab - MediumPaint.imgFocusTab.frameWidth * 2, MediumPaint.imgFocusTab_1.getHeight(), 0);
		if (numTab > 1)
		{
			if (imgIcon != null)
			{
				g.drawImage(imgIcon[focusTab][0], (float)(num + x + focusTab * num2 + num2 / 2), (float)(y + hTab - MediumPaint.imgFocusTab.frameHeight / 2 + 2), 3);
			}
			else
			{
				Canvas.fontWhiteBold.drawString(g, subName[focusTab], num + x + focusTab * num2 + num2 / 2, y + hTab - MediumPaint.imgFocusTab.frameHeight / 2 + 2 - Canvas.fontWhiteBold.getHeight() / 2, 2);
			}
		}
		else if (imgIcon != null)
		{
			g.drawImage(imgIcon[focusTab][0], (float)(num + x + focusTab * num2 + num2 / 2), (float)(y + hTab - MediumPaint.imgNotFocusTab.frameHeight / 2), 3);
		}
		else
		{
			g.setClip((float)(num + x + focusTab * num2 + num2 / 2 - wTab / 2 + 2), (float)(y + hTab - MediumPaint.imgFocusTab.frameHeight + 6), (float)(wTab - 4), (float)hTab);
			Canvas.fontWhiteBold.drawString(g, name, num + x + focusTab * num2 + num2 / 2 + (int)countText, y + hTab - MediumPaint.imgNotFocusTab.frameHeight / 2 - Canvas.fontWhiteBold.getHeight() / 2 - 4, 2);
			Canvas.resetTrans(g);
		}
		Canvas.resetTrans(g);
		g.setClip((float)x, 0f, (float)(w + ListScr.imgCloseTab[0].w), (float)h);
		if ((int)countCloseAll != -1)
		{
			if (!isFull)
			{
				g.drawImage(ListScr.imgCloseTab[(int)countCloseAll], (float)(x + w), (float)(y + hTab - 3), 3);
			}
			else
			{
				g.drawImage(ListScr.imgCloseTabFull[(int)countCloseAll], (float)(x + w - 5 - ((!isFull) ? 0 : 25)), (float)(y + hTab + 5 - ((!isFull) ? 0 : 23)), 3);
			}
		}
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00019B36 File Offset: 0x00017F36
	public void paintBGCMD(MyGraphics g, int x, int y, int w, int h)
	{
	}

	// Token: 0x0600030B RID: 779 RVA: 0x00019B38 File Offset: 0x00017F38
	public void paintButton(MyGraphics g, int x, int y, int index, string text)
	{
		g.drawImage(MediumPaint.imgButton[index], (float)(x - PaintPopup.wButtonSmall / 2), (float)y, 0);
		Canvas.normalFont.drawString(g, text, x, y + AvMain.hCmd / 2 - (int)AvMain.hNormal / 2 + 1, 2);
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00019B78 File Offset: 0x00017F78
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

	// Token: 0x0600030D RID: 781 RVA: 0x00019CEC File Offset: 0x000180EC
	public void paintHalf(MyGraphics g, Card c)
	{
		if ((int)c.cardID == -1)
		{
			g.drawImage(MediumPaint.imgCardBg, (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
			return;
		}
		g.drawImage(MediumPaint.imgCardIcon[c.cardMapping[(int)c.cardID / 4] * 4 + (int)c.cardID % 4], (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
	}

	// Token: 0x0600030E RID: 782 RVA: 0x00019D74 File Offset: 0x00018174
	public void paintHalfBackFull(MyGraphics g, Card c)
	{
		if ((int)c.cardID == -1)
		{
			g.drawImage(MediumPaint.imgCardBg, (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
			return;
		}
		g.drawImage(MediumPaint.imgCardIcon[c.cardMapping[(int)c.cardID / 4] * 4 + (int)c.cardID % 4], (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00019DFC File Offset: 0x000181FC
	public void paintFull(MyGraphics g, Card c)
	{
		if ((int)c.cardID == -1)
		{
			g.drawImage(MediumPaint.imgCardBg, (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
			return;
		}
		g.drawImage(MediumPaint.imgCardIcon[c.cardMapping[(int)c.cardID / 4] * 4 + (int)c.cardID % 4], (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00019E84 File Offset: 0x00018284
	public void paintSmall(MyGraphics g, Card c, bool isCh)
	{
		if ((int)c.cardID == -1)
		{
			g.drawImage(MediumPaint.imgCardBg, (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
			return;
		}
		g.drawImage(MediumPaint.imgCardIcon[c.cardMapping[(int)c.cardID / 4] * 4 + (int)c.cardID % 4], (float)(c.x - this.wwCard), (float)(c.y - this.hhCard), 0);
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00019F0C File Offset: 0x0001830C
	public void init()
	{
		AvMain.hDuBox = 5;
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00019F14 File Offset: 0x00018314
	public void initPos()
	{
		MyScreen.hText = Canvas.h / 12;
		if (MyScreen.hText < 50)
		{
			MyScreen.hText = 50;
		}
		if (MyScreen.hText > 70)
		{
			MyScreen.hText = 70;
		}
		AvMain.hFillTab = 0;
		MyScreen.hTab = 40;
		Canvas.hTab = MyScreen.hText;
		if (Canvas.instance != null)
		{
			AvMain.hFillTab = Canvas.hTab / 6;
			Canvas.h -= AvMain.hFillTab * 5;
		}
		MyScreen.wTab = 86;
		int h = Canvas.h;
		Canvas.posCmd[0] = new AvPosition(2, Canvas.hCan - 36, 2);
		Canvas.posCmd[1] = new AvPosition(Canvas.hw - MyScreen.wTab / 2, Canvas.hCan - 36, 2);
		Canvas.posCmd[2] = new AvPosition(Canvas.w - MyScreen.wTab - 2, Canvas.hCan - 36, 2);
		Canvas.posByteCOunt = new AvPosition(Canvas.w - 2, 1, 1);
		if (Canvas.instance != null)
		{
			MyScreen.hTab = 0;
		}
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0001A020 File Offset: 0x00018420
	public int collisionCmdBar(Command left, Command center, Command right)
	{
		if (MediumPaint.imgButton == null)
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
				if (Canvas.isPoint(center.x - MyScreen.wTab / 2, center.y, MyScreen.wTab, AvMain.hCmd))
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
				if (Canvas.isPoint(right.x - MyScreen.wTab / 2, right.y, MyScreen.wTab, AvMain.hCmd))
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

	// Token: 0x06000314 RID: 788 RVA: 0x0001A18A File Offset: 0x0001858A
	public void getSound(string path, int loop)
	{
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0001A18C File Offset: 0x0001858C
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

	// Token: 0x06000316 RID: 790 RVA: 0x0001A20F File Offset: 0x0001860F
	public void setAnimalSound(MyVector animalLists)
	{
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0001A211 File Offset: 0x00018611
	public void clickSound()
	{
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0001A214 File Offset: 0x00018614
	public void paintCheckBox(MyGraphics g, int x, int y, int focus, bool isCheck)
	{
		int idx = 0;
		MediumPaint.imgCheck.drawFrame(idx, x, y + Canvas.tempFont.getHeight() / 2, 0, g);
		if (isCheck)
		{
			MediumPaint.imgCheck.drawFrame(1, x, y + Canvas.tempFont.getHeight() / 2, 0, g);
		}
		Canvas.tempFont.drawString(g, T.rememPass, x + 25, y + MediumPaint.imgCheck.frameHeight / 2 - 2, 0);
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0001A287 File Offset: 0x00018687
	public void paintSelected(MyGraphics g, int x, int y, int w, int h)
	{
		g.setColor(7138780);
		g.fillRect((float)(x - 3), (float)y, (float)(w + 6), (float)h);
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0001A2A8 File Offset: 0x000186A8
	public void paintArrow(MyGraphics g, int index, int x, int y, int w, int indLeft, int indRight)
	{
		g.drawImage(PaintPopup.imgMuiIOS[indLeft][2], (float)x, (float)y, 3);
		g.drawImage(PaintPopup.imgMuiIOS[indRight][3], (float)(x + w + 5), (float)y, 3);
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0001A2DB File Offset: 0x000186DB
	public int getWNormalFont(string str)
	{
		return Canvas.arialFont.getWidth(str);
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0001A2E8 File Offset: 0x000186E8
	public void paintSelected_2(MyGraphics g, int x, int y, int w, int h)
	{
		g.setColor(5299141);
		g.fillRect((float)x, (float)y, (float)w, (float)h);
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0001A305 File Offset: 0x00018705
	public void paintTransBack(MyGraphics g)
	{
		g.drawImageScale(MediumPaint.imgTrans, 0, 0, Canvas.w, Canvas.hCan, 0);
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0001A320 File Offset: 0x00018720
	public void initPosLogin(LoginScr lg, int h0)
	{
		lg.wLogin = 255;
		int num = 15;
		if (lg.isReg)
		{
			num = 5;
			lg.hLogin = lg.tfUser.height * 4 + num * 5 + PaintPopup.hButtonSmall / 2;
		}
		else
		{
			lg.hLogin = 155;
		}
		lg.hCellNew = (lg.hLogin - 20) / 3;
		lg.yNew = 10;
		lg.yLogin = h0 / 2 - lg.hLogin / 2;
		lg.xLogin = Canvas.hw - lg.wLogin / 2;
		int num2 = lg.yLogin + num + 4;
		lg.tfUser.width = (lg.tfPass.width = (lg.tfReg.width = (lg.tfEmail.width = 115)));
		lg.tfUser.y = num2;
		lg.tfUser.x = (lg.tfEmail.x = (lg.tfPass.x = (lg.tfReg.x = lg.xLogin + lg.wLogin - 115 - 30)));
		num2 += lg.tfUser.height + num;
		lg.tfPass.y = num2;
		num2 += lg.tfUser.height + num;
		lg.tfReg.y = num2;
		lg.yCheck = num2 - 10;
		lg.xCheck = lg.xLogin + 65;
		num2 += lg.tfUser.height + num;
		lg.tfEmail.y = num2;
	}

	// Token: 0x0600031F RID: 799 RVA: 0x0001A4BC File Offset: 0x000188BC
	public void paintKeyArrow(MyGraphics g, int x, int y)
	{
		Canvas.resetTrans(g);
		g.drawImage(PaintPopup.imgMuiIOS[this.ind1 / 5][2], (float)(x - 40), (float)y, 3);
		g.drawImage(PaintPopup.imgMuiIOS[this.ind0 / 5][3], (float)(x + 40), (float)y, 3);
		g.drawImage(PaintPopup.imgMuiIOS[this.ind3 / 5][0], (float)x, (float)(y - 40), 3);
		g.drawImage(PaintPopup.imgMuiIOS[this.ind2 / 5][1], (float)x, (float)(y + 40), 3);
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0001A548 File Offset: 0x00018948
	public int updateKeyArr(int x, int y)
	{
		if (Canvas.isPointerClick)
		{
			if (Canvas.isPoint(x + 40 - 20, y - 20, 40, 40))
			{
				this.isTranFish = true;
				Canvas.isPointerClick = false;
				this.ind0 = 1;
			}
			else if (Canvas.isPoint(x - 40 - 20, y - 20, 40, 40))
			{
				this.isTranFish = true;
				Canvas.isPointerClick = false;
				this.ind1 = 1;
			}
			else if (Canvas.isPoint(x - 20, y + 40 - 20, 40, 40))
			{
				this.isTranFish = true;
				Canvas.isPointerClick = false;
				this.ind2 = 1;
			}
			else if (Canvas.isPoint(x - 20, y - 40 - 20, 40, 40))
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
				if (!Canvas.isPoint(x + 40 - 20, y - 20, 40, 40))
				{
					this.ind0 = 0;
				}
				if (!Canvas.isPoint(x - 40 - 20, y - 20, 40, 40))
				{
					this.ind1 = 0;
				}
				if (!Canvas.isPoint(x - 20, y + 40 - 20, 40, 40))
				{
					this.ind2 = 0;
				}
				if (!Canvas.isPoint(x - 20, y - 40 - 20, 40, 40))
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

	// Token: 0x06000321 RID: 801 RVA: 0x0001A723 File Offset: 0x00018B23
	public void setVirtualKeyFish(int index)
	{
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0001A728 File Offset: 0x00018B28
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
		int num2 = h - AvMain.hFillTab;
		PBoardScr.posCardShow = new AvPosition[]
		{
			new AvPosition(Canvas.hw, BoardScr.hcard / 2, 0),
			new AvPosition(BoardScr.wCard / 2, num / 2, 0),
			new AvPosition(Canvas.hw, num2 - BoardScr.hcard, 0),
			new AvPosition(Canvas.w - BoardScr.wCard / 2, num / 2, 0)
		};
		PBoardScr.posCardEat = new AvPosition[]
		{
			new AvPosition(Canvas.hw, 0, 0),
			new AvPosition(BoardScr.wCard / 4 * 3, num / 2, 0),
			new AvPosition(Canvas.hw, num2 - BoardScr.hcard / 2, 0),
			new AvPosition(Canvas.w - BoardScr.wCard / 4, num / 2, 0)
		};
		PBoardScr.posNamePlaying = new AvPosition[]
		{
			new AvPosition(Canvas.hw, BoardScr.hcard + 2, 2),
			new AvPosition(BoardScr.wCard / 4 * 3 + BoardScr.wCard / 2 + 5, num / 2 - 10, 0),
			new AvPosition(Canvas.hw, num2 - BoardScr.hcard - BoardScr.hcard / 2 - Canvas.smallFontYellow.getHeight() - 1, 2),
			new AvPosition(Canvas.w - BoardScr.wCard - 5, num / 2 - 10, 1)
		};
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0001A959 File Offset: 0x00018D59
	public int initShop()
	{
		return 60;
	}

	// Token: 0x06000324 RID: 804 RVA: 0x0001A960 File Offset: 0x00018D60
	public void initString(int type)
	{
		Out.println("initString: " + type);
		if (type == 0)
		{
			MediumPaint.bank = "Ngân hàng";
			MediumPaint.casino = "Hội đánh cờ";
			MediumPaint.shop = "Cửa hàng";
			MediumPaint.park = "Công viên";
			MediumPaint.caro = "khu câu cá rô";
			MediumPaint.caloc = "khu câu cá lóc";
			MediumPaint.camap = "khu câu cá mập";
			MediumPaint.cauca = "khu câu cá";
			MediumPaint.prison = "Nhà giam";
			MediumPaint.slum = "Khu ngoại ô";
			MediumPaint.farmroad = "Đường vào nông trại";
			MediumPaint.farm = "Nông trại";
			MediumPaint.farmFriend = "Nông trại bạn bè";
			MediumPaint.entertaiment = "Khu giải trí";
			MediumPaint.salon = "Thẩm mỹ viện";
			MediumPaint.store = "Nhà kho";
			MediumPaint.food = "Thức ăn";
			MediumPaint.petFood = "Thức ăn thú nuôi";
			MediumPaint.eatPig = "Cho heo ăn";
			MediumPaint.eatDog = "Cho bò ăn";
			MediumPaint.getMilk = "Lấy sữa";
			MediumPaint.getEgg = "Lấy trứng";
			MediumPaint.topFarm = "TOP nông trại";
			MediumPaint.fishing = "Câu cá";
			MediumPaint.houseRoad = "Đường vào nhà";
			MediumPaint.gotoHouse = "Vào nhà";
			MediumPaint.quayVe = "Quày vé";
		}
		else
		{
			MediumPaint.bank = "Bank";
			MediumPaint.casino = "Casino";
			MediumPaint.shop = "Shop";
			MediumPaint.park = "Park";
			MediumPaint.caro = " Area anabas";
			MediumPaint.caloc = "Area Snakehead  fish";
			MediumPaint.camap = " Area shark";
			MediumPaint.cauca = " Area fishing";
			MediumPaint.slum = "Area suburban";
			MediumPaint.prison = "Prison";
			MediumPaint.farmroad = "Farm road";
			MediumPaint.farm = "Farm";
			MediumPaint.farmFriend = "Farm Friend";
			MediumPaint.entertaiment = "Entertaiment";
			MediumPaint.salon = "Thẩm mỹ viện";
			MediumPaint.store = "Warehouse";
			MediumPaint.food = "Food";
			MediumPaint.petFood = "Pet Food";
			MediumPaint.eatPig = "Feed Pig";
			MediumPaint.eatDog = "Feed Dog";
			MediumPaint.getMilk = "Get Milk";
			MediumPaint.getEgg = "Get Egg";
			MediumPaint.topFarm = "TOP Farm";
			MediumPaint.fishing = "Fishing";
			MediumPaint.houseRoad = "House Road";
			MediumPaint.gotoHouse = "Go to House";
			MediumPaint.quayVe = "ticket agent";
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0001ABAC File Offset: 0x00018FAC
	public string doJoinGo(int x, int y)
	{
		int typeMap = LoadMap.getTypeMap(x, y);
		switch (typeMap)
		{
		case 52:
			return MediumPaint.shop;
		case 53:
			return MediumPaint.farmFriend;
		case 54:
			return MediumPaint.fishing;
		case 55:
			return MediumPaint.bank;
		case 56:
			break;
		case 57:
		case 62:
			return MediumPaint.shop;
		case 58:
		case 63:
			return MediumPaint.salon;
		case 59:
		case 64:
			return MediumPaint.shop;
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
				return MediumPaint.park;
			case 10:
				return MediumPaint.entertaiment;
			case 13:
				return MediumPaint.park;
			case 14:
				return MediumPaint.cauca;
			case 15:
				return MediumPaint.caro;
			case 16:
				return MediumPaint.caloc;
			case 17:
				return MediumPaint.camap;
			case 18:
				return MediumPaint.slum;
			case 19:
				return MediumPaint.prison;
			case 22:
				return MediumPaint.houseRoad;
			case 25:
				return MediumPaint.farm;
			case 26:
				return MediumPaint.farmroad;
			case 28:
				goto IL_1F4;
			case 29:
				return MediumPaint.store;
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
			return MediumPaint.gotoHouse;
		case 71:
			return MediumPaint.quayVe;
		case 72:
			return T.nameCasino[0];
		case 73:
			return T.nameCasino[1];
		case 74:
			return T.nameCasino[2];
		case 75:
			return T.nameCasino[3];
		case 76:
			return T.nameCasino[4];
		case 77:
			return T.nameCasino[5];
		case 78:
			return MediumPaint.petFood;
		case 83:
			return T.joinA;
		case 84:
			return MediumPaint.eatPig;
		case 85:
			return MediumPaint.eatDog;
		case 86:
			return MediumPaint.getMilk;
		case 87:
			return MediumPaint.getEgg;
		case 89:
			return MediumPaint.topFarm;
		case 93:
			return MediumPaint.food;
		case 108:
		case 109:
			return MediumPaint.casino;
		}
		IL_1F4:
		return T.joinA;
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0001AE50 File Offset: 0x00019250
	public bool selectedPointer(int xF, int yF)
	{
		if (xF > 0 && yF > 0 && CRes.distance(xF / AvMain.hd, yF / AvMain.hd, GameMidlet.avatar.x, GameMidlet.avatar.y) <= ((LoadMap.TYPEMAP != 24) ? 35 : 300))
		{
			bool[] array = new bool[3];
			array[0] = true;
			bool flag = false;
			MyVector myVector = new MyVector();
			MyVector myVector2 = new MyVector();
			for (int i = 0; i < LoadMap.playerLists.size(); i++)
			{
				Base @base = (Base)LoadMap.playerLists.elementAt(i);
				if (@base.IDDB != GameMidlet.avatar.IDDB && (int)@base.catagory != 4 && ((@base.IDDB == GameMidlet.avatar.IDDB && LoadMap.TYPEMAP != 24) || @base.IDDB != GameMidlet.avatar.IDDB) && CRes.abs(@base.x * AvMain.hd - xF) <= 20 && @base.y * AvMain.hd - yF < 30 && @base.y * AvMain.hd - yF > 0)
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
					myVector2.addElement(new MediumPaint.CommandPointer(text, new MediumPaint.IActionPointer(@base), @base));
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
				myVector2.addElement(new MediumPaint.CommandPointerGo(text2, new MediumPaint.IActionPointerGO()));
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

	// Token: 0x06000327 RID: 807 RVA: 0x0001B24F File Offset: 0x0001964F
	public void paintNormalFont(MyGraphics g, string str, int x, int y, int anthor, bool isSelect)
	{
		if (!isSelect)
		{
			Canvas.arialFont.drawString(g, str, x, y, anthor);
		}
		else
		{
			Canvas.normalFont.drawString(g, str, x, y, anthor);
		}
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0001B280 File Offset: 0x00019680
	public void paintMSG(MyGraphics g)
	{
		Canvas.resetTrans(g);
		g.setColor(0);
		int num = 0;
		if (Canvas.setShowIconMenu())
		{
			if (Canvas.currentMyScreen == MapScr.instance)
			{
				num = 70;
				g.drawImage(MediumPaint.iconMenu[this.indexMenu], 33f, (float)(18 + Canvas.countTab), 3);
			}
			else
			{
				num = 50;
				g.drawImage(MediumPaint.iconMenu_2[this.indexMenu], 23f, (float)(18 + Canvas.countTab), 3);
			}
		}
		if (Canvas.setShowMsg())
		{
			g.drawImage(MediumPaint.imgMSG[this.indexMSG], (float)(num + MediumPaint.imgMSG[0].w / 2), (float)(18 + Canvas.countTab), 3);
			if (MessageScr.gI().isNewMsg)
			{
				g.drawImage(MediumPaint.imgNewMsg, (float)(num + MediumPaint.imgMSG[0].w / 2 + MediumPaint.imgMSG[this.indexMSG].w / 2 - 3), (float)(18 + Canvas.countTab + MediumPaint.imgMSG[this.indexMSG].h / 2 - 3), 3);
			}
		}
		if (Canvas.isPaintIconVir())
		{
			g.drawImage(MyScreen.imgChat[this.indexChat], (float)(num + MediumPaint.imgMSG[0].w + 23), (float)(18 + Canvas.countTab), 3);
			if (!ScaleGUI.isAndroid && Canvas.currentMyScreen != RaceScr.me && !Bus.isRun)
			{
				g.drawImage(MediumPaint.iconRota[this.indexRota], (float)(Canvas.w - 20 - 50), (float)(20 + Canvas.countTab), 3);
			}
			if (!onMainMenu.isOngame && Canvas.currentMyScreen != RaceScr.me && (Canvas.menuMain == null || Canvas.menuMain != MenuIcon.me))
			{
				if ((int)GameMidlet.avatar.action != 14)
				{
					g.drawImage(MediumPaint.iconAction[this.indexAction], (float)(Canvas.w - 20), (float)(20 + Canvas.countTab), 3);
				}
				g.drawImage(MediumPaint.iconFeel[this.indexFeel], (float)(Canvas.w - 20), (float)(60 + Canvas.countTab), 3);
			}
		}
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0001B4AC File Offset: 0x000198AC
	public void setBack()
	{
		int num = 0;
		if (Canvas.setShowIconMenu())
		{
			if (Canvas.currentMyScreen == MapScr.instance)
			{
				num = 70;
			}
			else
			{
				num = 50;
			}
		}
		if (Canvas.isPointerClick)
		{
			this.isTranIcon = true;
			Canvas.isPointerClick = false;
			if ((Canvas.currentMyScreen == MapScr.instance && Canvas.isPoint(0, 0, 65, 35 + Canvas.countTab)) || (Canvas.currentMyScreen != MapScr.instance && Canvas.isPoint(0, 0, 45, 35 + Canvas.countTab)))
			{
				this.indexMenu = 1;
			}
			else if (Canvas.setShowMsg() && Canvas.isPoint(num - 5, 0, 40, 35 + Canvas.countTab))
			{
				this.indexMSG = 1;
			}
			else if (Canvas.isPaintIconVir() && Canvas.isPoint(num + MediumPaint.imgMSG[0].w + 3, 0, 39, 35 + Canvas.countTab))
			{
				this.indexChat = 1;
			}
			else if (Canvas.isPaintIconVir() && (int)GameMidlet.avatar.action != 14 && !onMainMenu.isOngame && Canvas.currentMyScreen != RaceScr.me && (Canvas.menuMain == null || Canvas.menuMain != MenuIcon.me) && Canvas.isPoint(Canvas.w - 40, Canvas.countTab, 40, 40))
			{
				this.indexAction = 1;
			}
			else if (Canvas.isPaintIconVir() && !onMainMenu.isOngame && Canvas.currentMyScreen != RaceScr.me && (Canvas.menuMain == null || Canvas.menuMain != MenuIcon.me) && Canvas.isPoint(Canvas.w - 40, 40 + Canvas.countTab, 40, 40))
			{
				this.indexFeel = 1;
			}
			else if (Canvas.currentMyScreen != RaceScr.me && !Bus.isRun && Canvas.isPaintIconVir() && Canvas.isPoint(Canvas.w - 20 - 75, 20 + Canvas.countTab - 25, 50, 50))
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
				if (this.indexMenu == 1 && Canvas.currentMyScreen != MapScr.instance && !Canvas.isPoint(0, 0, 45, 35 + Canvas.countTab))
				{
					this.indexMenu = 0;
				}
				if (this.indexMenu == 1 && Canvas.currentMyScreen == MapScr.instance && !Canvas.isPoint(0, 0, 65, 35 + Canvas.countTab))
				{
					this.indexMenu = 0;
				}
				else if (this.indexMSG == 1 && !Canvas.isPoint(num - 5, 0, 40, 35 + Canvas.countTab))
				{
					this.indexMSG = 0;
				}
				else if (this.indexChat == 1 && !Canvas.isPoint(num + MediumPaint.imgMSG[0].w + 3, 0, 39, 35 + Canvas.countTab))
				{
					this.indexChat = 0;
				}
				else if (this.indexAction == 1 && !Canvas.isPoint(Canvas.w - 40, Canvas.countTab, 40, 40))
				{
					this.indexAction = 0;
				}
				else if (this.indexFeel == 1 && !Canvas.isPoint(Canvas.w - 40, 40 + Canvas.countTab, 40, 40))
				{
					this.indexFeel = 0;
				}
				else if (this.indexRota == 1 && !Canvas.isPoint(Canvas.w - 20 - 75, 20 + Canvas.countTab - 25, 50, 50))
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
					MapScr.gI().doSellectAction(Canvas.w - 20, 20 + Canvas.countTab);
				}
				else if (this.indexFeel == 1)
				{
					this.indexFeel = 0;
					MapScr.gI().doFeel(Canvas.w - 20, 60 + Canvas.countTab);
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

	// Token: 0x0600032A RID: 810 RVA: 0x0001BA0C File Offset: 0x00019E0C
	public void setDrawPointer(Command left, Command center, Command right)
	{
		if (Canvas.isPointerDown)
		{
			int num = Canvas.paint.collisionCmdBar(left, center, right);
			if (num != 0)
			{
				if (num != 1)
				{
					if (num == 2)
					{
						AvMain.indexCenter = (AvMain.indexLeft = 0);
					}
				}
				else
				{
					AvMain.indexLeft = (AvMain.indexRight = 0);
				}
			}
			else
			{
				AvMain.indexCenter = (AvMain.indexRight = 0);
			}
		}
		if (Canvas.isPointerClick)
		{
			int num2 = Canvas.paint.collisionCmdBar(left, center, right);
			if (num2 != 0)
			{
				if (num2 != 1)
				{
					if (num2 == 2)
					{
						if (right != null)
						{
							Canvas.isPointerClick = false;
							AvMain.indexRight = 4;
						}
					}
				}
				else if (center != null)
				{
					Canvas.isPointerClick = false;
					AvMain.indexCenter = 4;
				}
			}
			else if (left != null)
			{
				Canvas.isPointerClick = false;
				AvMain.indexLeft = 4;
			}
		}
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0001BAF3 File Offset: 0x00019EF3
	public void paintList(MyGraphics g, int w, int maxW, int maxH, bool isHide, int selected, int[] listBoard)
	{
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0001BAF5 File Offset: 0x00019EF5
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

	// Token: 0x0600032D RID: 813 RVA: 0x0001BB30 File Offset: 0x00019F30
	public void paintDefaultBg(MyGraphics g)
	{
		g.drawImageScale(OnSplashScr.imgBg, 0, 0, Canvas.w, Canvas.hCan, 0);
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0001BB4A File Offset: 0x00019F4A
	public void paintLogo(MyGraphics g, int x, int y)
	{
		g.drawImage(OnSplashScr.imgLogomainMenu, (float)x, (float)y, 3);
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0001BB5C File Offset: 0x00019F5C
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

	// Token: 0x06000330 RID: 816 RVA: 0x0001BBF8 File Offset: 0x00019FF8
	public void initImgBoard(int type)
	{
		if (BoardListOnScr.imgBoard != null)
		{
			return;
		}
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
		BoardListOnScr.imgBoard = new FrameImage(Image.createImagePNG("medium/hd/on/" + str), 81, 64);
		BoardListOnScr.gI().imgNumPlayer = Image.createImagePNG("medium/hd/on/imgNumPlayer");
		BoardListOnScr.gI().imgPlaying = Image.createImagePNG("medium/hd/on/imgPlay");
		BoardListOnScr.imgLock = Image.createImagePNG("medium/hd/on/imgLock");
		BoardListOnScr.imgSelectBoard = Image.createImagePNG("medium/hd/on/imgSelectban");
		BoardScr.imgBoard = Image.createImagePNG("medium/hd/on/imgTable");
		BoardScr.xBoard = Canvas.w / 2;
		BoardScr.yBoard = (Canvas.hCan - PaintPopup.hButtonSmall) / 2 + 15;
		BoardScr.wBoard = Canvas.w - 100;
		if (BoardScr.wBoard > 600)
		{
			BoardScr.wBoard = 600;
		}
		BoardScr.hBoard = Canvas.hCan - PaintPopup.hButtonSmall - 50;
		if (BoardScr.hBoard > 400)
		{
			BoardScr.hBoard = 400;
		}
		BoardScr.imgReady = new Image[2];
		BoardScr.imgReady[0] = Image.createImagePNG("medium/hd/on/ready");
		BoardScr.imgReady[1] = Image.createImagePNG("medium/hd/on/owner");
		BoardScr.imgBan = Image.createImagePNG("medium/hd/on/star");
	}

	// Token: 0x06000331 RID: 817 RVA: 0x0001BD64 File Offset: 0x0001A164
	public void setColorBar()
	{
		int num = (int)LoadMap.map[(int)((LoadMap.Hmap - 1) * LoadMap.wMap + 1)];
		if (num != -1)
		{
			int num2 = num / LoadMap.imgMap.nFrame;
			Image image = CRes.createImgByImg(num2 * 24, num % LoadMap.imgMap.nFrame * 24, 24, 24, LoadMap.imgMap.imgFrame);
			MyScreen.color = image.texture.GetPixel(0, 0);
			MyScreen.colorBar = -1;
		}
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0001BDDC File Offset: 0x0001A1DC
	public void initOngame()
	{
		try
		{
			MenuOn.imgTab = Image.createImagePNG("medium/hd/on/imgTabMenu");
			MsgDlg.imgLoadOn = Image.createImagePNG("medium/hd/on/loadingbg");
			MsgDlg.imgLoad = new FrameImage(Image.createImagePNG("medium/hd/on/busy"), 27, 18);
			RoomListOnScr.imgRoomStat = new FrameImage(Image.createImagePNG("medium/hd/on/imgStatus"), 27, 18);
			BoardListOnScr.gI().imgTitleBoard = Image.createImagePNG("medium/hd/on/imgkhungsoban");
			OnScreen.imgButtomSmall = new FrameImage(Image.createImagePNG("medium/hd/on/buttonSmall"), 30, 30);
			OnScreen.imgIconButton = new FrameImage(Image.createImagePNG("medium/hd/on/iconButton"), 30, 30);
			MediumPaint.imgButtonOn = new FrameImage(Image.createImagePNG("medium/hd/on/imgButton"), 84, 28);
			RoomListOnScr.gI().imgTitleRoom = Image.createImagePNG("medium/hd/on/imgRoomtitle");
			BCBoardScr.pointer = Image.createImagePNG("hd/hd/on/p");
			PaintPopup.wButtonSmall = MediumPaint.imgButtonOn.frameWidth;
			MediumPaint.imgBarMoney = Image.createImagePNG("medium/hd/on/barMoney");
			MediumPaint.imgBar = Image.createImagePNG("medium/hd/on/imgBar");
			PaintPopup.hButtonSmall = 40;
			MediumPaint.imgPopup = new Image[7];
			for (int i = 0; i < 7; i++)
			{
				MediumPaint.imgPopup[i] = Image.createImage("medium/hd/on/imgPopupn" + i);
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		onMainMenu.initImg();
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0001BF58 File Offset: 0x0001A358
	public void resetOngame()
	{
		OnSplashScr.imgBg = null;
		MediumPaint.imgButtonOn = null;
		MediumPaint.imgPopup = null;
		MediumPaint.imgPopup2 = null;
		Menu.me = null;
		MediumPaint.imgBarMoney = null;
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0001BF7E File Offset: 0x0001A37E
	public void paintRoomList(MyGraphics g, MyVector roomList, int hSmall, int cmy)
	{
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0001BF80 File Offset: 0x0001A380
	public void setName(int index, BoardScr board)
	{
		RoomListOnScr.index = index;
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

	// Token: 0x06000336 RID: 822 RVA: 0x0001BFB8 File Offset: 0x0001A3B8
	public void paintPlayer(MyGraphics g, int index, int male, int countLeft, int countRight)
	{
		g.setColor(5299141);
		g.fillRect(4f, (float)((int)PaintPopup.hTab + 20 + (int)AvMain.hBlack / 2 + 15), (float)(PaintPopup.gI().w - 8), 50f);
		this.imgRegGender.drawFrame((male != 2) ? 0 : 1, PaintPopup.gI().w / 2 - 21, (int)PaintPopup.hTab + 20, 0, 3, g);
		this.imgRegGender.drawFrame((male != 2) ? 1 : 0, PaintPopup.gI().w / 2 + 21, (int)PaintPopup.hTab + 20, 0, 3, g);
		Canvas.blackF.drawString(g, T.gender[0], PaintPopup.gI().w / 2 - 22, (int)PaintPopup.hTab + 20 - (int)AvMain.hBlack / 2, 2);
		Canvas.blackF.drawString(g, T.gender[1], PaintPopup.gI().w / 2 + 21, (int)PaintPopup.hTab + 20 - (int)AvMain.hBlack / 2, 2);
		g.drawImage(PaintPopup.imgMuiIOS[countLeft / 3][2], (float)(PaintPopup.gI().w / 2 - 45 - countLeft / 2), (float)((int)PaintPopup.hTab + (int)AvMain.hBlack / 2 + 60), 3);
		g.drawImage(PaintPopup.imgMuiIOS[countRight / 3][3], (float)(PaintPopup.gI().w / 2 + 45 + countRight / 2), (float)((int)PaintPopup.hTab + (int)AvMain.hBlack / 2 + 60), 3);
		GameMidlet.avatar.paintIcon(g, PaintPopup.gI().w / 2 + 1, (int)PaintPopup.hTab + 87, false);
		Canvas.normalFont.drawString(g, T.nameStr + GameMidlet.avatar.name, PaintPopup.gI().w / 2, (int)PaintPopup.hTab + 100, 2);
		Canvas.normalFont.drawString(g, T.moneyStr + GameMidlet.avatar.strMoney, PaintPopup.gI().w / 2, (int)PaintPopup.hTab + 115, 2);
		Canvas.resetTrans(g);
		g.setColor(0);
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0001C1E4 File Offset: 0x0001A5E4
	public void updateKeyRegister()
	{
		if (Canvas.isPointerClick)
		{
			if (Canvas.isPoint(PaintPopup.gI().x + PaintPopup.gI().w / 2 - 42, PaintPopup.gI().y + (int)PaintPopup.hTab + 5, 40, 30))
			{
				RegisterScr.gI().male = 1;
				RegisterScr.gI().getAvatarPart();
				Canvas.isPointerClick = false;
			}
			else if (Canvas.isPoint(PaintPopup.gI().x + PaintPopup.gI().w / 2, PaintPopup.gI().y + (int)PaintPopup.hTab + 5, 40, 30))
			{
				RegisterScr.gI().male = 2;
				RegisterScr.gI().getAvatarPart();
				Canvas.isPointerClick = false;
			}
			else if (Canvas.isPoint(PaintPopup.gI().x + PaintPopup.gI().w / 2 - 20 - 45, PaintPopup.gI().y + (int)PaintPopup.hTab + 20 + (int)AvMain.hBlack / 2 + 15, 40, 50))
			{
				RegisterScr.gI().index = 1;
				RegisterScr.gI().setKeyLeftRight(-1);
				RegisterScr.gI().countLeft = 5;
				Canvas.isPointerClick = false;
			}
			else if (Canvas.isPoint(PaintPopup.gI().x + PaintPopup.gI().w / 2 - 20 + 45, PaintPopup.gI().y + (int)PaintPopup.hTab + 20 + (int)AvMain.hBlack / 2 + 15, 40, 50))
			{
				RegisterScr.gI().index = 1;
				RegisterScr.gI().setKeyLeftRight(1);
				RegisterScr.gI().countRight = 5;
				Canvas.isPointerClick = false;
			}
		}
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0001C398 File Offset: 0x0001A798
	public void initReg()
	{
		try
		{
			this.imgRegGender = new FrameImage(Image.createImage(T.getPath() + "/gender"), 32, 20);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000339 RID: 825 RVA: 0x0001C3EC File Offset: 0x0001A7EC
	public void paintPopupBack(MyGraphics g, int x, int y, int w, int h, int countClose, bool isFull)
	{
		g.drawImageScale(MediumPaint.imgEffectBack.imgFrame, x, y + h - 20 - MediumPaint.imgEffectBack.frameHeight, w + 6, 100, 0);
		if (!isFull)
		{
			MediumPaint.imgPopupBack.drawFrame(0, x, y, 0, g);
			MediumPaint.imgPopupBack.drawFrame(1, x + w - MediumPaint.imgPopupBack.frameWidth, y, 0, g);
			g.drawImageScale(MediumPaint.imgPopupBackNum[0], x + MediumPaint.imgPopupBack.frameWidth, y, w - MediumPaint.imgPopupBack.frameWidth * 2, MediumPaint.imgPopupBack.frameHeight, 0);
			g.drawImageScale(MediumPaint.imgPopupBackNum[1], x + MediumPaint.imgPopupBack.frameWidth, y + h - MediumPaint.imgPopupBack.frameHeight, w - MediumPaint.imgPopupBack.frameWidth * 2, MediumPaint.imgPopupBack.frameHeight, 0);
			g.drawImageScale(MediumPaint.imgPopupBackNum[2], x, y + MediumPaint.imgPopupBack.frameHeight, MediumPaint.imgPopupBack.frameWidth, h - MediumPaint.imgPopupBack.frameHeight * 2, 0);
			g.drawImageScale(MediumPaint.imgPopupBackNum[3], x + w - MediumPaint.imgPopupBack.frameWidth, y + MediumPaint.imgPopupBack.frameHeight, MediumPaint.imgPopupBack.frameWidth, h - MediumPaint.imgPopupBack.frameHeight * 2, 0);
			g.setColor(13495295);
			g.fillRect((float)(x + MediumPaint.imgPopupBack.frameWidth), (float)(y + MediumPaint.imgPopupBack.frameHeight), (float)(w - MediumPaint.imgPopupBack.frameWidth * 2), (float)(h - MediumPaint.imgPopupBack.frameHeight * 2));
		}
		else
		{
			g.drawImageScale(MediumPaint.imgPopupBackNum[0], x, y, w, MediumPaint.imgPopupBack.frameHeight, 0);
			g.setColor(13495295);
			g.fillRect((float)x, (float)(y + MediumPaint.imgPopupBack.frameHeight), (float)w, (float)(h - MediumPaint.imgPopupBack.frameHeight));
		}
		if (!isFull)
		{
			MediumPaint.imgPopupBack.drawFrame(3, x, y + h - MediumPaint.imgPopupBack.frameHeight, 0, g);
			MediumPaint.imgPopupBack.drawFrame(2, x + w - MediumPaint.imgPopupBack.frameWidth, y + h - MediumPaint.imgPopupBack.frameHeight, 0, g);
		}
		if (countClose != -1)
		{
			if (!isFull)
			{
				g.drawImage(ListScr.imgCloseTab[countClose], (float)(x + w), (float)(y - 3), 3);
			}
			else
			{
				g.drawImage(ListScr.imgCloseTabFull[countClose], (float)(x + w - 10), (float)(y + 10), 3);
			}
		}
	}

	// Token: 0x0600033A RID: 826 RVA: 0x0001C678 File Offset: 0x0001AA78
	public void initImgCard()
	{
		if (MediumPaint.imgCardBg != null)
		{
			return;
		}
		try
		{
			MediumPaint.imgCardIcon = new Image[52];
			for (int i = 0; i < 52; i++)
			{
				MediumPaint.imgCardIcon[i] = Image.createImagePNG(T.getPath() + "/card/" + i);
			}
			MediumPaint.imgCardBg = Image.createImagePNG(T.getPath() + "/card/down");
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.StackTrace);
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0001C710 File Offset: 0x0001AB10
	public void resetCasino()
	{
		MediumPaint.imgCardIcon = null;
		MediumPaint.imgCardBg = null;
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0001C720 File Offset: 0x0001AB20
	public void paintMoney(MyGraphics g, int x, int y)
	{
		int width = Canvas.tempFont.getWidth(GameMidlet.avatar.money[0] + string.Empty);
		int width2 = Canvas.tempFont.getWidth(GameMidlet.avatar.money[2] + string.Empty);
		Canvas.tempFont.drawString(g, GameMidlet.avatar.money[0] + string.Empty, x, y, 0);
		g.drawImage(MyInfoScr.gI().imgIcon[0], (float)(x + width + 19), (float)(y + Canvas.tempFont.getHeight() / 2), 3);
		g.drawImage(MyInfoScr.gI().imgIcon[1], (float)(x + width2 + 70 + width), (float)(y + Canvas.tempFont.getHeight() / 2), 3);
		Canvas.tempFont.drawString(g, GameMidlet.avatar.money[2] + string.Empty, x + width + 50, y, 0);
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0001C824 File Offset: 0x0001AC24
	public void paintTabSoft(MyGraphics g)
	{
		int num = 40;
		g.drawImageScale(MediumPaint.imgBar, 0, Canvas.hCan - num, Canvas.w, Canvas.hCan - num, 0);
	}

	// Token: 0x0600033E RID: 830 RVA: 0x0001C854 File Offset: 0x0001AC54
	public void paintCmdBar(MyGraphics g, Command left, Command center, Command right)
	{
		int num = Canvas.hCan - PaintPopup.hButtonSmall / 2 + 2;
		if (left != null && left.caption != string.Empty)
		{
			if (left.x != -1)
			{
				MediumPaint.imgButtonOn.drawFrame((int)this.indexLeft, left.x + PaintPopup.wButtonSmall / 2, left.y + PaintPopup.hButtonSmall / 2, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, left.caption, left.x + PaintPopup.wButtonSmall / 2, left.y + PaintPopup.hButtonSmall / 2 - Canvas.normalWhiteFont.getHeight() / 2, 2);
			}
			else
			{
				MediumPaint.imgButtonOn.drawFrame((int)this.indexLeft, 4 + PaintPopup.wButtonSmall / 2, num, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, left.caption, 4 + PaintPopup.wButtonSmall / 2, num - Canvas.normalWhiteFont.getHeight() / 2 - 1, 2);
			}
		}
		if (center != null && center.caption != string.Empty)
		{
			if (center.x != -1)
			{
				MediumPaint.imgButtonOn.drawFrame((int)this.indexCenter, center.x + PaintPopup.wButtonSmall / 2, center.y + PaintPopup.hButtonSmall / 2, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, center.caption, center.x + PaintPopup.wButtonSmall / 2, center.y + PaintPopup.hButtonSmall / 2 - Canvas.normalWhiteFont.getHeight() / 2, 2);
			}
			else
			{
				MediumPaint.imgButtonOn.drawFrame((int)this.indexCenter, Canvas.w / 2, num, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, center.caption, Canvas.w / 2, num - Canvas.normalWhiteFont.getHeight() / 2 - 1, 2);
			}
		}
		if (right != null && right.caption != string.Empty)
		{
			if (right.x != -1)
			{
				MediumPaint.imgButtonOn.drawFrame((int)this.indexRight, right.x + PaintPopup.wButtonSmall / 2, right.y + PaintPopup.hButtonSmall / 2, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, right.caption, right.x + PaintPopup.wButtonSmall / 2, right.y + PaintPopup.hButtonSmall / 2 - Canvas.normalWhiteFont.getHeight() / 2, 2);
			}
			else
			{
				MediumPaint.imgButtonOn.drawFrame((int)this.indexRight, Canvas.w - PaintPopup.wButtonSmall / 2 - 4, num, 0, 3, g);
				Canvas.normalWhiteFont.drawString(g, right.caption, Canvas.w - PaintPopup.wButtonSmall / 2 - 4, num - Canvas.normalWhiteFont.getHeight() / 2 - 1, 2);
			}
		}
	}

	// Token: 0x0600033F RID: 831 RVA: 0x0001CB24 File Offset: 0x0001AF24
	public void updateKeyOn(Command left, Command center, Command right)
	{
		if (Canvas.isPointerClick)
		{
			int num = MediumPaint.pointCmdBar(left, center, right);
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
			int num2 = MediumPaint.pointCmdBar(left, center, right);
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
			int num3 = MediumPaint.pointCmdBar(left, center, right);
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

	// Token: 0x06000340 RID: 832 RVA: 0x0001CC9C File Offset: 0x0001B09C
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

	// Token: 0x06000341 RID: 833 RVA: 0x0001CE14 File Offset: 0x0001B214
	public void paintDefaultPopup(MyGraphics g, int x, int y, int w, int h)
	{
		MediumPaint.paintBorder(g, x, y, w, h, true);
	}

	// Token: 0x06000342 RID: 834 RVA: 0x0001CE24 File Offset: 0x0001B224
	public static void paintBorder(MyGraphics g, int x, int y, int w, int h, bool paintTop)
	{
		int width = MediumPaint.imgPopup[0].getWidth();
		int height = MediumPaint.imgPopup[0].getHeight();
		if (paintTop)
		{
			g.drawImage(MediumPaint.imgPopup[0], (float)x, (float)y, 0);
			for (int i = 1; i < w / width - 1; i++)
			{
				g.drawImage(MediumPaint.imgPopup[1], (float)(x + width * i), (float)y, 0);
			}
			g.drawImage(MediumPaint.imgPopup[1], (float)(x + w - width * 2), (float)y, 0);
			g.drawImage(MediumPaint.imgPopup[2], (float)(x + w - width), (float)y, 0);
		}
		if (h / height > 2)
		{
			for (int j = 1; j < h / height; j++)
			{
				g.drawImage(MediumPaint.imgPopup[3], (float)x, (float)(y + height * j), 0);
				g.drawImage(MediumPaint.imgPopup[4], (float)(x + w - width), (float)(y + height * j), 0);
			}
			g.drawImage(MediumPaint.imgPopup[3], (float)x, (float)(y + h - height * 2), 0);
			g.drawImage(MediumPaint.imgPopup[4], (float)(x + w - width), (float)(y + h - height * 2), 0);
		}
		if (h > height * 2 - 10 * ScaleGUI.numScale && h <= height * 3)
		{
			g.drawImage(MediumPaint.imgPopup[3], (float)x, (float)(y + h / 2 - height / 2), 0);
			g.drawImage(MediumPaint.imgPopup[4], (float)(x + w - width), (float)(y + h / 2 - height / 2), 0);
		}
		g.drawImage(MediumPaint.imgPopup[5], (float)x, (float)(y + h - height), 0);
		for (int k = 1; k < w / width - 1; k++)
		{
			g.drawImage(MediumPaint.imgPopup[6], (float)(x + width * k), (float)(y + h - height), 0);
		}
		g.drawImage(MediumPaint.imgPopup[6], (float)(x + w - width * 2), (float)(y + h - height), 0);
		g.drawImage(MediumPaint.imgPopup[7], (float)(x + w - width), (float)(y + h - height), 0);
		g.setColor(MediumPaint.colorNormal);
		g.fillRect((float)(x + 20), (float)(y + 20), (float)(w - 40), (float)(h - 40));
	}

	// Token: 0x06000343 RID: 835 RVA: 0x0001D048 File Offset: 0x0001B448
	public void paintLineRoom(MyGraphics g, int x, int y, int xTo, int yTo)
	{
		if (ScaleGUI.numScale == 1)
		{
			g.setColor(MediumPaint.colorBold);
			g.fillRect((float)x, (float)(y + 1), (float)(xTo - x), (float)(yTo - y + 1));
		}
		else
		{
			g.setColor(MediumPaint.colorBold);
			g.fillRect((float)x, (float)(y + 1), (float)(xTo - x), (float)(yTo - y + 2));
		}
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0001D0AB File Offset: 0x0001B4AB
	public void paintSelect(MyGraphics g, int x, int y, int w, int h)
	{
		g.setColor(MediumPaint.colorSelect);
		g.fillRect((float)x, (float)y, (float)w, (float)h);
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0001D0C8 File Offset: 0x0001B4C8
	public void paintBorderTitle(MyGraphics g, int x, int y, int w, int h)
	{
		MediumPaint.paintBorder(g, x, y, w, h, false);
		int width = MediumPaint.imgPopup2[0].getWidth();
		g.drawImage(MediumPaint.imgPopup2[0], (float)x, (float)y, 0);
		for (int i = 1; i < w / width - 1; i++)
		{
			g.drawImage(MediumPaint.imgPopup2[1], (float)(x + width * i), (float)y, 0);
		}
		g.drawImage(MediumPaint.imgPopup2[1], (float)(x + w - width * 2), (float)y, 0);
		g.drawImage(MediumPaint.imgPopup2[2], (float)(x + w - width), (float)y, 0);
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0001D15E File Offset: 0x0001B55E
	public void paintTransMoney(MyGraphics g, int x, int y, int w, int h)
	{
		g.drawImage(MediumPaint.imgBarMoney, (float)(x + w / 2), (float)(y + h / 2), 3);
	}

	// Token: 0x040003A4 RID: 932
	public static FrameImage imgPopupBack;

	// Token: 0x040003A5 RID: 933
	public static FrameImage imgEffectBack;

	// Token: 0x040003A6 RID: 934
	public static FrameImage imgNotFocusTab;

	// Token: 0x040003A7 RID: 935
	public static FrameImage imgFocusTab;

	// Token: 0x040003A8 RID: 936
	public static Image imgCardBg;

	// Token: 0x040003A9 RID: 937
	public static Image imgCardBg1;

	// Token: 0x040003AA RID: 938
	public static Image imgCardBg2;

	// Token: 0x040003AB RID: 939
	public static Image imgMenuTab;

	// Token: 0x040003AC RID: 940
	public static Image imgBar;

	// Token: 0x040003AD RID: 941
	public static Image imgBarMoney;

	// Token: 0x040003AE RID: 942
	public static Image imgCloseSmall;

	// Token: 0x040003AF RID: 943
	public static Image[] imgCardIcon;

	// Token: 0x040003B0 RID: 944
	public static Image[] imgPopup;

	// Token: 0x040003B1 RID: 945
	public static Image[] imgPopup2;

	// Token: 0x040003B2 RID: 946
	public static Image[] imgPopupBackNum;

	// Token: 0x040003B3 RID: 947
	public static FrameImage[] imgCardNumber;

	// Token: 0x040003B4 RID: 948
	public static FrameImage imgCheck;

	// Token: 0x040003B5 RID: 949
	public static FrameImage imgButtonOn;

	// Token: 0x040003B6 RID: 950
	public static FrameImage imgEraser;

	// Token: 0x040003B7 RID: 951
	public static Image[] iconMenu;

	// Token: 0x040003B8 RID: 952
	public static Image[] iconAction;

	// Token: 0x040003B9 RID: 953
	public static Image[] iconFeel;

	// Token: 0x040003BA RID: 954
	public static Image[] imgMSG;

	// Token: 0x040003BB RID: 955
	public static Image[] iconMenu_2;

	// Token: 0x040003BC RID: 956
	public static Image[] imgButton;

	// Token: 0x040003BD RID: 957
	public static Image[] iconRota;

	// Token: 0x040003BE RID: 958
	private static Image imgNotFocusTab_1;

	// Token: 0x040003BF RID: 959
	private static Image imgFocusTab_1;

	// Token: 0x040003C0 RID: 960
	private static Image imgNewMsg;

	// Token: 0x040003C1 RID: 961
	public static sbyte[][] cardIconInfo = new sbyte[13][];

	// Token: 0x040003C2 RID: 962
	public static int colorSelect = 35217;

	// Token: 0x040003C3 RID: 963
	public static int colorBold = 16709;

	// Token: 0x040003C4 RID: 964
	public static int colorNormal = 23135;

	// Token: 0x040003C5 RID: 965
	public static int colorLight = 10276804;

	// Token: 0x040003C6 RID: 966
	public static int colorInfoPopup = 10461344;

	// Token: 0x040003C7 RID: 967
	private static Image imgTrans;

	// Token: 0x040003C8 RID: 968
	private int wwCard = 36;

	// Token: 0x040003C9 RID: 969
	private int hhCard = 49;

	// Token: 0x040003CA RID: 970
	private int aa = -1;

	// Token: 0x040003CB RID: 971
	private MyVector listAnimalSound = new MyVector();

	// Token: 0x040003CC RID: 972
	private Player soundClick;

	// Token: 0x040003CD RID: 973
	public int ind0;

	// Token: 0x040003CE RID: 974
	public int ind1;

	// Token: 0x040003CF RID: 975
	public int ind2;

	// Token: 0x040003D0 RID: 976
	public int ind3;

	// Token: 0x040003D1 RID: 977
	private bool isTranFish;

	// Token: 0x040003D2 RID: 978
	public static string bank;

	// Token: 0x040003D3 RID: 979
	public static string casino;

	// Token: 0x040003D4 RID: 980
	public static string shop;

	// Token: 0x040003D5 RID: 981
	public static string park;

	// Token: 0x040003D6 RID: 982
	public static string caro;

	// Token: 0x040003D7 RID: 983
	public static string caloc;

	// Token: 0x040003D8 RID: 984
	public static string camap;

	// Token: 0x040003D9 RID: 985
	public static string cauca;

	// Token: 0x040003DA RID: 986
	public static string prison;

	// Token: 0x040003DB RID: 987
	public static string slum;

	// Token: 0x040003DC RID: 988
	public static string farmroad;

	// Token: 0x040003DD RID: 989
	public static string farm;

	// Token: 0x040003DE RID: 990
	public static string farmFriend;

	// Token: 0x040003DF RID: 991
	public static string entertaiment;

	// Token: 0x040003E0 RID: 992
	public static string salon;

	// Token: 0x040003E1 RID: 993
	public static string store;

	// Token: 0x040003E2 RID: 994
	public static string food;

	// Token: 0x040003E3 RID: 995
	public static string petFood;

	// Token: 0x040003E4 RID: 996
	public static string eatPig;

	// Token: 0x040003E5 RID: 997
	public static string eatDog;

	// Token: 0x040003E6 RID: 998
	public static string getMilk;

	// Token: 0x040003E7 RID: 999
	public static string getEgg;

	// Token: 0x040003E8 RID: 1000
	public static string topFarm;

	// Token: 0x040003E9 RID: 1001
	public static string fishing;

	// Token: 0x040003EA RID: 1002
	public static string houseRoad;

	// Token: 0x040003EB RID: 1003
	public static string gotoHouse;

	// Token: 0x040003EC RID: 1004
	public static string quayVe;

	// Token: 0x040003ED RID: 1005
	private int indexAction;

	// Token: 0x040003EE RID: 1006
	private int indexFeel;

	// Token: 0x040003EF RID: 1007
	private int indexMenu;

	// Token: 0x040003F0 RID: 1008
	private int indexMSG;

	// Token: 0x040003F1 RID: 1009
	private int indexChat;

	// Token: 0x040003F2 RID: 1010
	private int indexRota;

	// Token: 0x040003F3 RID: 1011
	private bool isTranIcon;

	// Token: 0x040003F4 RID: 1012
	private FrameImage imgRegGender;

	// Token: 0x040003F5 RID: 1013
	private sbyte indexLeft;

	// Token: 0x040003F6 RID: 1014
	private sbyte indexCenter;

	// Token: 0x040003F7 RID: 1015
	private sbyte indexRight;

	// Token: 0x02000058 RID: 88
	private class CommandPointerGo : Command
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0001D17A File Offset: 0x0001B57A
		public CommandPointerGo(string name, MediumPaint.IActionPointerGO a) : base(name, a)
		{
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001D184 File Offset: 0x0001B584
		public override void paint(MyGraphics g, int x, int y)
		{
			MainMenu.imgGO.drawFrame(1, x, y, 0, 3, g);
		}
	}

	// Token: 0x02000059 RID: 89
	private class IActionPointerGO : IAction
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0001D1A0 File Offset: 0x0001B5A0
		public void perform()
		{
			int num = LoadMap.posFocus.x / AvMain.hd;
			int num2 = LoadMap.posFocus.y / AvMain.hd;
			if (Canvas.loadMap.doJoin(num, num2) || Canvas.loadMap.doJoin(num + 24, num2) || Canvas.loadMap.doJoin(num - 24, num2) || Canvas.loadMap.doJoin(num, num2 + 24) || Canvas.loadMap.doJoin(num, num2 - 24))
			{
			}
		}
	}

	// Token: 0x0200005A RID: 90
	private class CommandPointer : Command
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0001D230 File Offset: 0x0001B630
		public CommandPointer(string name, MediumPaint.IActionPointer a, Base b) : base(name, a)
		{
			this.b = b;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001D241 File Offset: 0x0001B641
		public override void paint(MyGraphics g, int x, int y)
		{
			this.b.paintIcon(g, x, y + (int)(this.b.height / 2), false);
		}

		// Token: 0x040003F8 RID: 1016
		private Base b;
	}

	// Token: 0x0200005B RID: 91
	private class IActionPointer : IAction
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0001D260 File Offset: 0x0001B660
		public IActionPointer(Base b)
		{
			this.b = b;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001D270 File Offset: 0x0001B670
		public void perform()
		{
			LoadMap.focusObj = this.b;
			if ((int)this.b.catagory == 0)
			{
				MapScr.focusP = (Avatar)this.b;
			}
			if (LoadMap.focusObj != null)
			{
				MainMenu.gI().avaPaint = new AvPosition((int)((float)(LoadMap.focusObj.x * AvMain.hd) - AvCamera.gI().xCam), (int)((float)(LoadMap.focusObj.y * AvMain.hd) - AvCamera.gI().yCam));
			}
			if (LoadMap.TYPEMAP == 24)
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

		// Token: 0x040003F9 RID: 1017
		private Base b;
	}
}
