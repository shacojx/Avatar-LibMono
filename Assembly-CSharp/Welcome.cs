using System;

// Token: 0x02000077 RID: 119
public class Welcome : AvMain
{
	// Token: 0x060003E1 RID: 993 RVA: 0x00024BE0 File Offset: 0x00022FE0
	public Welcome()
	{
		Welcome.isOut = false;
		Welcome.isPaintArrow = true;
		this.x = 10;
		this.next = 0;
		this.center = new Command("Tiếp", new Welcome.IActionClick());
		this.left = new Command(T.close, new Welcome.IActionLeft());
		Welcome.wText = (int)(ScaleGUI.WIDTH - (float)(this.x * 2) - (float)(100 * AvMain.hd));
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00024C57 File Offset: 0x00023057
	public void update()
	{
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00024C5C File Offset: 0x0002305C
	public override void updateKey()
	{
		if (Welcome.isPaintArrow)
		{
			Canvas.paint.setDrawPointer(this.left, this.center, this.right);
			Canvas.paint.setBack();
			base.updateKey();
		}
		if (Welcome.isPaintArrow && Welcome.lastScr == Canvas.currentMyScreen && Canvas.menuMain == null && Canvas.currentDialog == null && this.chats != null)
		{
			Canvas.keyHold[2] = (Canvas.keyHold[4] = (Canvas.keyHold[6] = (Canvas.keyHold[8] = false)));
		}
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x00024D00 File Offset: 0x00023100
	private void click()
	{
		if (this.next < this.chats.Length - 1)
		{
			this.next++;
			Welcome.isPaintArrow = true;
			this.setNext();
			if (LoadMap.TYPEMAP == 23)
			{
				if (Welcome.indexKhuMuaSam == 1 && this.next == this.chats.Length - 1)
				{
					AvCamera.gI().setToPos((float)((int)Welcome.posArrayPopupX[0] * AvMain.hd), (float)(20 * AvMain.hd));
					AvCamera.isFollow = true;
					SubObject o = new SubObject(-9, (int)Welcome.posArrayPopupX[Welcome.indexKhuMuaSam - 1], 50, 20);
					LoadMap.treeLists.addElement(o);
					LoadMap.orderVector(LoadMap.treeLists);
				}
			}
			else if (LoadMap.TYPEMAP == 9 && Welcome.indexMapScr == 1 && this.next == this.chats.Length - 1)
			{
				this.initMapScr();
			}
		}
		else if (this.next == this.chats.Length - 1)
		{
			AvCamera.isFollow = false;
			if (LoadMap.TYPEMAP == 100)
			{
				Canvas.welcome = null;
				return;
			}
			if (Canvas.currentMyScreen == MiniMap.me && this.textMiniMap != null && Welcome.indexMiniMap == this.textMiniMap.Length)
			{
				this.initMiniMap();
				return;
			}
			if (LoadMap.TYPEMAP == 24)
			{
				if (Welcome.indexFarm == 3 || Welcome.indexFarm == 4 || Welcome.indexFarm == 5 || Welcome.indexFarm == 6)
				{
					this.removeArrow();
					Canvas.welcome = new Welcome();
					Canvas.welcome.initFarm();
					Welcome.isPaintArrow = true;
					return;
				}
				if (Welcome.indexFarm == 7 && Welcome.isPaintArrow && !Welcome.isOut)
				{
					this.close(FarmScr.posName.x + 27 + 50, 178);
					return;
				}
			}
			else if (LoadMap.TYPEMAP == 25)
			{
				if (Welcome.indexFarmPath == this.textFarmPath.Length - 1)
				{
					Canvas.welcome = null;
				}
			}
			else if (LoadMap.TYPEMAP == 13)
			{
				this.next = 0;
				if (!Welcome.isOut)
				{
					this.initFish();
					return;
				}
			}
			this.y = Canvas.transTab + 5;
			Welcome.isPaintArrow = false;
		}
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x00024F58 File Offset: 0x00023358
	private void setNext()
	{
		this.wPopup = this.chats[this.next].Length * (int)AvMain.hBlack + AvMain.hDuBox * 2;
		if (this.wPopup < (int)AvMain.hBlack * 2 + AvMain.hDuBox * 2)
		{
			this.wPopup = (int)AvMain.hBlack * 2 + AvMain.hDuBox * 2;
		}
		this.y = 5;
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00024FC4 File Offset: 0x000233C4
	public override void paint(MyGraphics g)
	{
		if (Welcome.lastScr == Canvas.currentMyScreen && Canvas.currentDialog == null && Canvas.menuMain == null)
		{
			Canvas.resetTrans(g);
			g.translate(0f, (float)Canvas.countTab);
			if (Welcome.isPaintArrow || Canvas.gameTick % 20 > 2)
			{
				ChatPopup.paintRoundRect(g, this.x, this.y, Canvas.w - this.x * 2, this.wPopup, 16777215, 13940870, 0);
				if (this.chats != null && this.chats[this.next] != null)
				{
					int num = 0;
					if (this.chats[this.next].Length == 1)
					{
						num = 2;
					}
					for (int i = 0; i < this.chats[this.next].Length; i++)
					{
						Canvas.blackF.drawString(g, this.chats[this.next][i], this.x + (Canvas.w - this.x * 2) / 2, this.y + this.wPopup / 2 - this.chats[this.next].Length * (int)AvMain.hBlack / 2 + i * (int)AvMain.hBlack - num, 2);
					}
					this.num += 1;
					if (this.num >= 8)
					{
						this.num = 0;
					}
					if (Canvas.currentMyScreen == MiniMap.me)
					{
						g.translate((float)((int)(-MiniMap.cmx + (float)MiniMap.gI().x)), (float)((int)(-MiniMap.cmy + (float)MiniMap.gI().y)));
					}
					else
					{
						g.translate((float)(-(float)((int)AvCamera.gI().xCam)), (float)(-(float)((int)AvCamera.gI().yCam)));
					}
				}
			}
			if (Welcome.isPaintArrow)
			{
				base.paint(g);
			}
		}
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x000251A4 File Offset: 0x000235A4
	public void initMiniMap()
	{
		if (Welcome.indexMiniMap == Welcome.indexWelcomeMiniMap.Length + 1)
		{
			Canvas.welcome = null;
			Canvas.isInitChar = false;
			return;
		}
		if (this.textMiniMap == null)
		{
			this.textMiniMap = Canvas.t.getTextMiniMap();
		}
		Welcome.lastScr = MiniMap.me;
		Welcome.isPaintArrow = true;
		if (Welcome.indexMiniMap < Welcome.indexWelcomeMiniMap.Length)
		{
			MiniMap.gI().selected = (int)Welcome.indexWelcomeMiniMap[Welcome.indexMiniMap];
		}
		Canvas.welcome.setText(this.textMiniMap[Welcome.indexMiniMap]);
		Welcome.indexMiniMap++;
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00025244 File Offset: 0x00023644
	public void setText(string[] text)
	{
		this.chats = new string[text.Length][];
		for (int i = 0; i < this.chats.Length; i++)
		{
			this.chats[i] = Canvas.blackF.splitFontBStrInLine(text[i], Welcome.wText);
		}
		this.setNext();
		Welcome.isPaintArrow = true;
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x000252A0 File Offset: 0x000236A0
	public void initMapScr()
	{
		if (this.textMapScr == null)
		{
			this.textMapScr = Canvas.t.getTextMapScr();
		}
		Welcome.lastScr = MapScr.instance;
		Welcome.posArrayPopupX = new short[3];
		Welcome.posArrayPopupX[0] = 180;
		Welcome.posArrayPopupX[1] = 312;
		Welcome.posArrayPopupX[2] = 720;
		Welcome.joinOrder = new sbyte[]
		{
			10,
			100,
			107
		};
		if (Welcome.indexMapScr != 0)
		{
			if (Welcome.indexMapScr == Welcome.posArrayPopupX.Length)
			{
				this.close(288, 150);
				return;
			}
			AvCamera.gI().setToPos((float)((int)Welcome.posArrayPopupX[Welcome.indexMapScr] * AvMain.hd), (float)(20 * AvMain.hd));
			AvCamera.isFollow = true;
		}
		if (Welcome.indexMapScr != 0)
		{
			SubObject o = new SubObject(-9, (int)Welcome.posArrayPopupX[Welcome.indexMapScr], 50, 20);
			LoadMap.treeLists.addElement(o);
			LoadMap.orderVector(LoadMap.treeLists);
		}
		Canvas.welcome.setText(this.textMapScr[Welcome.indexMapScr]);
		Welcome.indexMapScr++;
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x000253CC File Offset: 0x000237CC
	public void initKhuMuaSam()
	{
		if (this.textKhuMuaSam == null)
		{
			this.textKhuMuaSam = Canvas.t.getTextMuaSam();
		}
		Welcome.lastScr = MapScr.instance;
		Welcome.posArrayPopupX = new short[3];
		Welcome.posArrayPopupX[0] = 865;
		Welcome.posArrayPopupX[1] = 445;
		Welcome.posArrayPopupX[2] = 95;
		Welcome.joinOrder = new sbyte[]
		{
			57,
			104,
			58,
			100,
			107
		};
		if (Welcome.indexKhuMuaSam != 0)
		{
			if (Welcome.indexKhuMuaSam == Welcome.posArrayPopupX.Length)
			{
				this.close(640, 150);
				return;
			}
			AvCamera.gI().setToPos((float)((int)Welcome.posArrayPopupX[Welcome.indexKhuMuaSam] * AvMain.hd), (float)(20 * AvMain.hd));
			AvCamera.isFollow = true;
			SubObject o = new SubObject(-9, (int)Welcome.posArrayPopupX[Welcome.indexKhuMuaSam], 50, 20);
			LoadMap.treeLists.addElement(o);
			LoadMap.orderVector(LoadMap.treeLists);
		}
		Canvas.welcome.setText(this.textKhuMuaSam[Welcome.indexKhuMuaSam]);
		Welcome.indexKhuMuaSam++;
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x000254EC File Offset: 0x000238EC
	public bool isJoinMapScr(int pos)
	{
		if (Welcome.isOut)
		{
			return true;
		}
		int typemap = LoadMap.TYPEMAP;
		switch (typemap)
		{
		case 23:
			if (Welcome.indexKhuMuaSam - 1 < Welcome.joinOrder.Length && pos == (int)Welcome.joinOrder[Welcome.indexKhuMuaSam - 1])
			{
				return true;
			}
			break;
		default:
			if (typemap != 9)
			{
				if (typemap == 57)
				{
					if (Welcome.indexShop <= Welcome.joinOrder.Length && pos == (int)Welcome.joinOrder[Welcome.indexShop - 1])
					{
						return true;
					}
				}
			}
			else if (Welcome.indexMapScr - 1 < Welcome.joinOrder.Length && pos == (int)Welcome.joinOrder[Welcome.indexMapScr - 1])
			{
				return true;
			}
			break;
		case 25:
			if (Welcome.indexFarmPath <= Welcome.joinOrder.Length && pos == (int)Welcome.joinOrder[Welcome.indexFarmPath - 1])
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x000255EC File Offset: 0x000239EC
	public void initFarmPath(MyScreen last)
	{
		if (this.textFarmPath == null)
		{
			this.textFarmPath = Canvas.t.getTextFarmPath();
		}
		Welcome.lastScr = last;
		if (Welcome.indexFarmPath == 0)
		{
			Welcome.posArrayPopupX = new short[]
			{
				372,
				-1,
				-1,
				220
			};
			Welcome.posArrayPopupY = new short[]
			{
				25,
				-1,
				-1,
				25
			};
			Welcome.joinOrder = new sbyte[]
			{
				52,
				-1,
				-1,
				24
			};
		}
		else if (Welcome.indexFarmPath == this.textFarmPath.Length)
		{
			this.close(170, 150);
			return;
		}
		if (Welcome.indexFarmPath == 1)
		{
			this.removeArrow();
		}
		SubObject o = new SubObject(-9, (int)Welcome.posArrayPopupX[Welcome.indexFarmPath], (int)Welcome.posArrayPopupY[Welcome.indexFarmPath], 20);
		LoadMap.treeLists.addElement(o);
		LoadMap.orderVector(LoadMap.treeLists);
		AvCamera.gI().setToPos((float)((int)Welcome.posArrayPopupX[Welcome.indexFarmPath] * AvMain.hd), (float)(20 * AvMain.hd));
		AvCamera.isFollow = true;
		Canvas.welcome.setText(this.textFarmPath[Welcome.indexFarmPath]);
		Welcome.indexFarmPath++;
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00025724 File Offset: 0x00023B24
	public void initTash()
	{
		if (this.textTask == null)
		{
			this.textTask = Canvas.t.getTextToaThiChinh();
		}
		if (Welcome.indexTask == 0)
		{
		}
		Canvas.welcome.setText(this.textTask[Welcome.indexTask]);
		Welcome.indexTask++;
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00025778 File Offset: 0x00023B78
	public void initFarm()
	{
		if (this.textFarm == null)
		{
			this.textFarm = Canvas.t.getTextFarm();
		}
		Welcome.lastScr = FarmScr.instance;
		if (Welcome.indexFarm == 0)
		{
			Welcome.posArrayPopupX = new short[]
			{
				(short)(FarmScr.gI().posTree[0].x * LoadMap.w + 12),
				(short)(FarmScr.posBarn.x + 12),
				(short)FarmScr.xPosCook,
				(short)FarmScr.starFruil.x,
				(short)(FarmScr.posPond.x + 12)
			};
			short[] array = new short[]
			{
				36,
				36,
				0,
				36,
				36
			};
			array[2] = (short)(FarmScr.yPosCook + 15);
			Welcome.posArrayPopupY = array;
		}
		int num = Welcome.indexFarm;
		if (num < 3)
		{
			num = 0;
		}
		else if (num == 3)
		{
			num = 1;
		}
		else if (num == 4)
		{
			num = 2;
		}
		else if (num == 5)
		{
			num = 3;
		}
		else if (num == 6)
		{
			num = 4;
		}
		if (Welcome.indexFarm < 3 || Welcome.indexFarm == 4 || Welcome.indexFarm == 5)
		{
			SubObject o = new SubObject(-9, (int)Welcome.posArrayPopupX[num], (int)Welcome.posArrayPopupY[num], 20);
			LoadMap.treeLists.addElement(o);
			LoadMap.orderVector(LoadMap.treeLists);
		}
		AvCamera.gI().setToPos((float)((int)Welcome.posArrayPopupX[num] * AvMain.hd), (float)(36 * AvMain.hd));
		AvCamera.isFollow = true;
		Canvas.welcome.setText(this.textFarm[Welcome.indexFarm]);
		Welcome.indexFarm++;
		FarmScr.gI().left = null;
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00025924 File Offset: 0x00023D24
	public void initShop(MyScreen last)
	{
		if (this.textShop == null)
		{
			this.textShop = Canvas.t.getTextShop();
		}
		Welcome.lastScr = last;
		if (Welcome.indexShop == 0)
		{
			Welcome.posArrayPopupX = new short[]
			{
				192
			};
			Welcome.joinOrder = new sbyte[]
			{
				56
			};
			SubObject o = new SubObject(-9, (int)(Welcome.posArrayPopupX[Welcome.indexShop] + 12), 135, 20);
			LoadMap.treeLists.addElement(o);
			LoadMap.orderVector(LoadMap.treeLists);
			AvCamera.gI().setToPos((float)(Welcome.posArrayPopupX[Welcome.indexShop] + 12), (float)(130 * AvMain.hd));
		}
		else
		{
			if (Welcome.indexShop == this.textShop.Length)
			{
				this.close(180, 240);
				return;
			}
			AvCamera.isFollow = true;
		}
		Canvas.welcome.setText(this.textShop[Welcome.indexShop]);
		Welcome.indexShop++;
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00025A28 File Offset: 0x00023E28
	public void initFish()
	{
		if (this.textFish == null)
		{
			this.textFish = Canvas.t.getTextFish();
		}
		Welcome.lastScr = MapScr.instance;
		if (Welcome.indexFish == 0)
		{
			Welcome.joinOrder = new sbyte[]
			{
				56
			};
		}
		else
		{
			if (Welcome.indexFish == this.textFish.Length)
			{
				this.close(170, 170);
				return;
			}
			if (Welcome.indexFish < 4)
			{
				Welcome.posArrayPopupX = new short[]
				{
					12,
					480,
					230
				};
				Welcome.posArrayPopupY = new short[]
				{
					110,
					110,
					12
				};
				AvCamera.gI().setToPos((float)((int)Welcome.posArrayPopupX[Welcome.indexFish - 1] * AvMain.hd), (float)((int)Welcome.posArrayPopupY[Welcome.indexFish - 1] * AvMain.hd));
				AvCamera.isFollow = true;
				SubObject o = new SubObject(-9, (int)Welcome.posArrayPopupX[Welcome.indexFish - 1], (int)Welcome.posArrayPopupY[Welcome.indexFish - 1], 20);
				LoadMap.treeLists.addElement(o);
				LoadMap.orderVector(LoadMap.treeLists);
			}
			else
			{
				AvCamera.isFollow = false;
			}
		}
		Canvas.welcome.setText(this.textFish[Welcome.indexFish]);
		Welcome.indexFish++;
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00025B74 File Offset: 0x00023F74
	private void removeArrow()
	{
		for (int i = 0; i < LoadMap.treeLists.size(); i++)
		{
			MyObject myObject = (MyObject)LoadMap.treeLists.elementAt(i);
			if ((int)myObject.catagory == 1 && ((SubObject)myObject).type == -9)
			{
				LoadMap.treeLists.removeElement(myObject);
				i--;
			}
		}
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00025BDC File Offset: 0x00023FDC
	public void close(int x, int y)
	{
		this.next = 0;
		Welcome.isOut = true;
		this.removeArrow();
		SubObject o = new SubObject(-9, x, y, 20);
		LoadMap.treeLists.addElement(o);
		LoadMap.orderVector(LoadMap.treeLists);
		AvCamera.gI().setToPos((float)(x * AvMain.hd), (float)(y * AvMain.hd));
		AvCamera.isFollow = true;
		string[] textOut = Canvas.t.getTextOut();
		Canvas.welcome.setText(textOut);
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00025C58 File Offset: 0x00024058
	public static void goFarm()
	{
		int num = Welcome.indexFarm;
		if (num < 3)
		{
			num = 0;
		}
		else if (num == 3)
		{
			num = 1;
		}
		else if (num == 4)
		{
			num = 2;
		}
		if (num < Welcome.posArrayPopupX.Length)
		{
			Canvas.welcome = new Welcome();
			Canvas.welcome.initFarm();
		}
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00025CB1 File Offset: 0x000240B1
	public static void restart()
	{
		Canvas.isInitChar = true;
		Welcome.indexFarm = 0;
		Welcome.indexFarmPath = 0;
		Welcome.indexFish = 0;
		Welcome.indexMapScr = 0;
		Welcome.indexMiniMap = 0;
		Welcome.indexShop = 0;
		Welcome.isOut = false;
		Welcome.isPaintArrow = false;
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00025CEC File Offset: 0x000240EC
	public void initCasino()
	{
		if (this.textCasino == null)
		{
			this.textCasino = new string[1][];
			this.textCasino[0] = new string[]
			{
				"Chào mừng bạn vào hội đánh cờ.",
				"Bạn hảy di chuyển đến chổ mũi tên để vào trò chơi."
			};
		}
		Welcome.lastScr = MapScr.instance;
		this.posPopup = null;
		if (Welcome.indexCasino == 0)
		{
			Welcome.posArrayPopupX = new short[]
			{
				230
			};
			Welcome.joinOrder = new sbyte[]
			{
				72
			};
			SubObject o = new SubObject(-9, (int)Welcome.posArrayPopupX[Welcome.indexShop], 120, 20);
			LoadMap.treeLists.addElement(o);
			AvCamera.isFollow = true;
			LoadMap.orderVector(LoadMap.treeLists);
			AvCamera.gI().setToPos((float)((int)Welcome.posArrayPopupX[Welcome.indexCasino] * AvMain.hd + 12), (float)(100 * AvMain.hd));
		}
		else
		{
			if (Welcome.indexCasino == this.textCasino.Length)
			{
				this.close(345, 250);
				return;
			}
			AvCamera.isFollow = true;
		}
		Canvas.welcome.setText(this.textCasino[Welcome.indexCasino]);
		Welcome.indexCasino++;
	}

	// Token: 0x04000648 RID: 1608
	private int x;

	// Token: 0x04000649 RID: 1609
	private int y;

	// Token: 0x0400064A RID: 1610
	private int wPopup;

	// Token: 0x0400064B RID: 1611
	private int next;

	// Token: 0x0400064C RID: 1612
	private string[][] chats;

	// Token: 0x0400064D RID: 1613
	public static int index = 0;

	// Token: 0x0400064E RID: 1614
	public byte num;

	// Token: 0x0400064F RID: 1615
	public static MyScreen lastScr;

	// Token: 0x04000650 RID: 1616
	public static int indexFish = 0;

	// Token: 0x04000651 RID: 1617
	private string[][] textFish;

	// Token: 0x04000652 RID: 1618
	public static int indexShop = 0;

	// Token: 0x04000653 RID: 1619
	private string[][] textShop;

	// Token: 0x04000654 RID: 1620
	public static bool isPaintArrow = false;

	// Token: 0x04000655 RID: 1621
	private static int indexMiniMap = 0;

	// Token: 0x04000656 RID: 1622
	private string[][] textMiniMap;

	// Token: 0x04000657 RID: 1623
	public static int indexMapScr = 0;

	// Token: 0x04000658 RID: 1624
	private static short[] posArrayPopupX;

	// Token: 0x04000659 RID: 1625
	private static short[] posArrayPopupY;

	// Token: 0x0400065A RID: 1626
	private string[][] textMapScr;

	// Token: 0x0400065B RID: 1627
	public static int indexFarmPath = 0;

	// Token: 0x0400065C RID: 1628
	private string[][] textFarmPath;

	// Token: 0x0400065D RID: 1629
	public static sbyte[] joinOrder;

	// Token: 0x0400065E RID: 1630
	public static int indexFarm = 0;

	// Token: 0x0400065F RID: 1631
	public static int wText;

	// Token: 0x04000660 RID: 1632
	private string[][] textFarm;

	// Token: 0x04000661 RID: 1633
	public static bool isOut = false;

	// Token: 0x04000662 RID: 1634
	public static byte[] indexWelcomeMiniMap = new byte[]
	{
		3,
		7,
		4,
		1,
		5
	};

	// Token: 0x04000663 RID: 1635
	private string[][] textKhuMuaSam;

	// Token: 0x04000664 RID: 1636
	private static int indexKhuMuaSam = 0;

	// Token: 0x04000665 RID: 1637
	private SubObject subPath;

	// Token: 0x04000666 RID: 1638
	private string[][] textTask;

	// Token: 0x04000667 RID: 1639
	public static int indexTask = 0;

	// Token: 0x04000668 RID: 1640
	public static int indexCasino = 0;

	// Token: 0x04000669 RID: 1641
	private string[][] textCasino;

	// Token: 0x0400066A RID: 1642
	public AvPosition posPopup;

	// Token: 0x02000078 RID: 120
	private class IActionLeft : IAction
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x00025E8B File Offset: 0x0002428B
		public void perform()
		{
			Canvas.startOKDlg(T.usureStop, new Welcome.IActionLeft1());
		}
	}

	// Token: 0x02000079 RID: 121
	private class IActionLeft1 : IAction
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00025EA4 File Offset: 0x000242A4
		public void perform()
		{
			Canvas.isInitChar = false;
			Canvas.welcome = null;
			AvCamera.isFollow = false;
			MapScr.gI().initCmd();
			Canvas.endDlg();
		}
	}

	// Token: 0x0200007A RID: 122
	private class IActionClick : IAction
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x00025ECF File Offset: 0x000242CF
		public void perform()
		{
			Canvas.welcome.click();
		}
	}
}
