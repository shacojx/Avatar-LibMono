using System;
using UnityEngine;

// Token: 0x0200018D RID: 397
public class ServerListScr : MyScreen, HTTPHandler
{
	// Token: 0x06000A73 RID: 2675 RVA: 0x000675A4 File Offset: 0x000659A4
	public ServerListScr()
	{
		this.imgArr = Image.createImagePNG(T.getPath() + "/ios/i1");
		this.imgF = Image.createImage(T.getPath() + "/effect/tp");
		this.initCmd();
		ServerListScr.w = 200 * AvMain.hd;
		ServerListScr.h = 250 * AvMain.hd;
		ServerListScr.hDis = ServerListScr.h - 20;
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x00067629 File Offset: 0x00065A29
	public static ServerListScr gI()
	{
		return (ServerListScr.me != null) ? ServerListScr.me : (ServerListScr.me = new ServerListScr());
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0006764A File Offset: 0x00065A4A
	public override void switchToMe()
	{
		base.switchToMe();
		this.init();
		this.indexUSer = 0;
		if (this.center == null)
		{
			this.initCmd();
		}
		this.chans();
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x00067676 File Offset: 0x00065A76
	public override void doMenu()
	{
		LoginScr.gI().doMenu();
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x00067684 File Offset: 0x00065A84
	public override void commandAction(int index)
	{
		switch (index)
		{
		case 2:
			Canvas.startOKDlg(T.doYouWantExit2, 54);
			break;
		case 3:
			Canvas.startOK(T.uNeedExitGame, 55);
			break;
		case 4:
			ipKeyboard.openKeyBoard(T.nameAcc, ipKeyboard.TEXT, string.Empty, new ServerListScr.actDoSettingPassword(), false);
			break;
		case 5:
			OptionScr.gI().switchToMe();
			break;
		case 6:
			GameMidlet.flatForm("http://wap.teamobi.com/faqs.php?provider=" + GameMidlet.PROVIDER);
			break;
		case 7:
			GameMidlet.flatForm("http://wap.teamobi.com?info=checkupdate&game=8&version=2.5.8&provider=" + GameMidlet.PROVIDER);
			break;
		default:
			if (index != 50)
			{
			}
			break;
		case 9:
			Canvas.startOKDlg(T.alreadyDelRMS + T.delRMS);
			AvatarData.delRMS();
			break;
		case 10:
			this.closeTabAll();
			break;
		}
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x00067784 File Offset: 0x00065B84
	public void login()
	{
		GameMidlet.avatar = new Avatar();
		Out.println("login");
		LoginScr.gI().timeOut = Canvas.getTick();
		int num = 0;
		for (int i = 0; i < GameMidlet.nameSV[OptionScr.gI().mapFocus[4]].Length; i++)
		{
			num += GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][i].Length;
			if (num >= ServerListScr.selected)
			{
				ServerListScr.indexSV = i;
				int num2 = ServerListScr.selected - (num - GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][i].Length) - 1;
				if (num2 >= 0 && num2 < GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][i].Length - 1)
				{
					ServerListScr.index = num2;
				}
				break;
			}
		}
		Canvas.startWaitCancelDlg(T.logging);
		Session_ME.gI().close();
		LoginScr.gI().login();
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x00067874 File Offset: 0x00065C74
	public override void commandTab(int index)
	{
		if (index != 0)
		{
			if (index == 1)
			{
				this.doUpdateServer();
			}
		}
		else
		{
			this.login();
		}
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0006789E File Offset: 0x00065C9E
	public void initCmd()
	{
		if (T.selectt == null)
		{
			return;
		}
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x000678AC File Offset: 0x00065CAC
	public void doUpdateServer()
	{
		Canvas.startWaitDlg();
		MsgDlg.isBack = false;
		if (this.indexUSer >= GameMidlet.linkGetHost.Length)
		{
			Canvas.startOKDlg(T.canNotConnect);
			this.indexUSer = 0;
			return;
		}
		Net.connectHTTP(GameMidlet.linkGetHost[OptionScr.gI().mapFocus[4]][this.indexUSer], this);
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x00067908 File Offset: 0x00065D08
	public void init()
	{
		ServerListScr.x = (Canvas.w - ServerListScr.w) / 2;
		ServerListScr.y = (Canvas.hCan - ServerListScr.h) / 2;
		this.isHide = true;
		int num = 0;
		for (int i = 0; i < GameMidlet.nameSV[OptionScr.gI().mapFocus[4]].Length; i++)
		{
			num += GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][i].Length;
		}
		ServerListScr.cmyLim = num * MyScreen.hText - ServerListScr.hDis;
		ServerListScr.cmy = (ServerListScr.cmtoY = 0);
		if (ServerListScr.cmyLim < 0)
		{
			ServerListScr.cmyLim = 0;
		}
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x000679B0 File Offset: 0x00065DB0
	private void click()
	{
		int num = (ServerListScr.cmtoY + Canvas.py - (ServerListScr.y + 10)) / MyScreen.hText;
		if (ServerListScr.index < GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][ServerListScr.indexSV].Length)
		{
			this.commandTab(0);
		}
		this.isHide = true;
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x00067A0C File Offset: 0x00065E0C
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
		if (this.vY != 0)
		{
			if (ServerListScr.cmy < 0 || ServerListScr.cmy > ServerListScr.cmyLim)
			{
				if (this.vY > 500)
				{
					this.vY = 500;
				}
				else if (this.vY < -500)
				{
					this.vY = -500;
				}
				this.vY -= this.vY / 5;
				if (CRes.abs(this.vY / 10) <= 10)
				{
					this.vY = 0;
				}
			}
			ServerListScr.cmy += this.vY / 15;
			ServerListScr.cmtoY = ServerListScr.cmy;
			this.vY -= this.vY / 20;
		}
		else if (ServerListScr.cmy < 0)
		{
			ServerListScr.cmtoY = 0;
		}
		else if (ServerListScr.cmy > ServerListScr.cmyLim)
		{
			ServerListScr.cmtoY = ServerListScr.cmyLim;
		}
		if (ServerListScr.cmy != ServerListScr.cmtoY)
		{
			ServerListScr.cmvy = ServerListScr.cmtoY - ServerListScr.cmy << 2;
			ServerListScr.cmdy += ServerListScr.cmvy;
			ServerListScr.cmy += ServerListScr.cmdy >> 4;
			ServerListScr.cmdy &= 15;
		}
		Canvas.loadMap.update();
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x00067B98 File Offset: 0x00065F98
	private void setIndex(int index)
	{
		ServerListScr.indexSV = index;
		if (ServerListScr.indexSV >= GameMidlet.nameSV[OptionScr.gI().mapFocus[4]].Length)
		{
			ServerListScr.indexSV = 0;
		}
		if (ServerListScr.indexSV < 0)
		{
			ServerListScr.indexSV = GameMidlet.nameSV[OptionScr.gI().mapFocus[4]].Length - 1;
		}
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x00067BF8 File Offset: 0x00065FF8
	public override void setSelected(int se, bool isAction)
	{
		ServerListScr.selected = se;
		if (ServerListScr.selected >= GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][ServerListScr.indexSV].Length || ServerListScr.selected <= 0)
		{
			ServerListScr.selected = 0;
			if (isAction)
			{
				this.isSelected = false;
				this.init();
			}
		}
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x00067C53 File Offset: 0x00066053
	public override void closeTabAll()
	{
		this.isSelected = false;
		ServerListScr.indexSV = 0;
		ServerListScr.selected = 0;
		LoginScr.gI().switchToMe();
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x00067C74 File Offset: 0x00066074
	public override void updateKey()
	{
		this.count += 1L;
		if (Canvas.isPointerClick)
		{
			if (Canvas.isPoint(ServerListScr.x + ServerListScr.w - 3 * AvMain.hd - 20 * AvMain.hd, ServerListScr.y + AvMain.hd - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
			{
				this.isTranKey = true;
				Canvas.isPointerClick = false;
				this.countClose = 5;
			}
			else if (Canvas.isPoint(ServerListScr.x, ServerListScr.y, ServerListScr.w, ServerListScr.h))
			{
				this.isTran = true;
				this.isG = false;
				if (this.vY != 0)
				{
					this.isG = true;
				}
				this.pyLast = Canvas.pyLast;
				Canvas.isPointerClick = false;
				this.pa = ServerListScr.cmy;
				this.transY = true;
				this.timeDelay = this.count;
				this.isFire = false;
				int num = (ServerListScr.cmtoY + Canvas.py - (ServerListScr.y + 10)) / MyScreen.hText;
				ServerListScr.selected = num;
				int num2 = 0;
				for (int i = 0; i < GameMidlet.nameSV[OptionScr.gI().mapFocus[4]].Length; i++)
				{
					num2 += GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][i].Length;
					if (num2 >= ServerListScr.selected)
					{
						ServerListScr.indexSV = i;
						int num3 = ServerListScr.selected - (num2 - GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][i].Length) - 1;
						if (num3 >= 0 && num3 < GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][i].Length - 1)
						{
							ServerListScr.index = num3;
							this.isFire = true;
						}
						break;
					}
				}
			}
		}
		if (this.isTranKey)
		{
			if (Canvas.isPointerDown && !Canvas.isPoint(ServerListScr.x + ServerListScr.w - 3 * AvMain.hd - 20 * AvMain.hd, ServerListScr.y + AvMain.hd - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
			{
				this.countClose = 0;
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranKey = false;
				if ((int)this.countClose > 0)
				{
					LoginScr.gI().switchToMe();
				}
				this.countClose = 0;
			}
		}
		if (this.transY)
		{
			long num4 = this.count - this.timeDelay;
			int num5 = this.pyLast - Canvas.py;
			this.pyLast = Canvas.py;
			if (Canvas.isPointerDown)
			{
				if (this.count % 2L == 0L)
				{
					this.dyTran = Canvas.py;
					this.timePoint = this.count;
				}
				this.vY = 0;
				if (CRes.abs(Canvas.dy()) >= 10 * AvMain.hd)
				{
					this.isHide = true;
				}
				else if (num4 > 3L && num4 < 8L && this.isFire && !this.isG)
				{
					this.isHide = false;
				}
				this.test = this.pa + "    " + num5;
				ServerListScr.cmtoY = this.pa + num5;
				if (ServerListScr.cmtoY < 0 || ServerListScr.cmtoY > ServerListScr.cmyLim)
				{
					ServerListScr.cmtoY = this.pa + num5 / 2;
				}
				this.pa = ServerListScr.cmtoY;
				ServerListScr.cmy = ServerListScr.cmtoY;
			}
			if (Canvas.isPointerRelease && Canvas.isPoint(ServerListScr.x, ServerListScr.y, ServerListScr.w, ServerListScr.h))
			{
				this.isG = false;
				int num6 = (int)(this.count - this.timePoint);
				int num7 = this.dyTran - Canvas.py;
				if (CRes.abs(num7) > 40 && num6 < 10 && ServerListScr.cmtoY > 0 && ServerListScr.cmtoY < ServerListScr.cmyLim)
				{
					this.vY = num7 / num6 * 10;
				}
				this.timePoint = -1L;
				if (global::Math.abs(Canvas.dy()) < 10 * AvMain.hd && this.isFire)
				{
					if (num4 <= 4L)
					{
						if (this.isFire)
						{
							this.isHide = false;
						}
						this.timeOpen = 5;
					}
					else if (!this.isHide)
					{
						this.click();
					}
					this.isFire = false;
				}
			}
		}
		if (Canvas.isPointerRelease)
		{
			this.transY = false;
		}
		base.updateKey();
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0006810E File Offset: 0x0006650E
	private void chans()
	{
		ServerListScr.cmtoY = 0;
		if (ServerListScr.cmtoY < 0)
		{
			ServerListScr.cmtoY = 0;
		}
		if (ServerListScr.cmtoY > ServerListScr.cmyLim)
		{
			ServerListScr.cmtoY = ServerListScr.cmyLim;
		}
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x00068140 File Offset: 0x00066540
	public override void paintMain(MyGraphics g)
	{
		GUIUtility.ScaleAroundPivot(new Vector2(AvMain.zoom, AvMain.zoom), Vector2.zero);
		Canvas.loadMap.paint(g);
		Canvas.loadMap.paintObject(g);
		GUIUtility.ScaleAroundPivot(new Vector2(1f / AvMain.zoom, 1f / AvMain.zoom), Vector2.zero);
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x000681A4 File Offset: 0x000665A4
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		Canvas.resetTrans(g);
		if (Canvas.currentDialog == null && Canvas.menuMain == null)
		{
			Canvas.paint.paintPopupBack(g, ServerListScr.x, ServerListScr.y, ServerListScr.w, ServerListScr.h, (int)this.countClose / 3, false);
			g.translate((float)ServerListScr.x, (float)(ServerListScr.y + 10));
			g.setClip(0f, 0f, (float)ServerListScr.w, (float)ServerListScr.hDis);
			g.translate(0f, (float)(-(float)ServerListScr.cmy));
			if (!this.isHide)
			{
				g.setColor(16777215);
				g.fillRect((float)(12 * AvMain.hd), (float)(ServerListScr.selected * MyScreen.hText), (float)(ServerListScr.w - 24 * AvMain.hd), (float)MyScreen.hText);
			}
			int num = (MyScreen.hText - (int)AvMain.hNormal) / 2;
			for (int i = 0; i < GameMidlet.nameSV[OptionScr.gI().mapFocus[4]].Length; i++)
			{
				Canvas.normalFont.drawString(g, GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][i][0], 24 * AvMain.hd, num, 0);
				g.drawImage(this.imgArr, (float)(14 * AvMain.hd), (float)(num + (int)AvMain.hNormal / 2), 3);
				num += MyScreen.hText;
				for (int j = 1; j < GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][i].Length; j++)
				{
					Canvas.normalFont.drawString(g, GameMidlet.nameSV[OptionScr.gI().mapFocus[4]][i][j], 36 * AvMain.hd, num, 0);
					g.drawImage(this.imgF, (float)(24 * AvMain.hd), (float)(num + (int)AvMain.hNormal / 2), 3);
					num += MyScreen.hText;
				}
			}
			base.paint(g);
		}
		else
		{
			LoginScr.gI().paintLogo(g);
		}
		Canvas.paintPlus(g);
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x000683A4 File Offset: 0x000667A4
	public void onGetText(string s)
	{
		if (s.Equals(string.Empty))
		{
			this.indexUSer++;
			this.doUpdateServer();
			return;
		}
		bool flag = false;
		if (s == null || s == string.Empty)
		{
			flag = true;
		}
		string[][][] array = null;
		int[][][] array2 = null;
		string[][][] array3 = null;
		try
		{
			string[] array4 = s.Split(new char[]
			{
				'*'
			});
			if (array4.Length == 0 || array4.Length == 1)
			{
				flag = true;
			}
			array2 = new int[2][][];
			array = new string[2][][];
			array3 = new string[2][][];
			array2[OptionScr.gI().mapFocus[4]] = new int[array4.Length - 1][];
			array[OptionScr.gI().mapFocus[4]] = new string[array4.Length - 1][];
			array3[OptionScr.gI().mapFocus[4]] = new string[array4.Length - 1][];
			for (int i = 1; i < array4.Length; i++)
			{
				string[] array5 = array4[i].Split(new char[]
				{
					'\n'
				});
				array3[OptionScr.gI().mapFocus[4]][i - 1] = new string[array5.Length - 1];
				array[OptionScr.gI().mapFocus[4]][i - 1] = new string[array5.Length - 2];
				array2[OptionScr.gI().mapFocus[4]][i - 1] = new int[array5.Length - 2];
				array3[OptionScr.gI().mapFocus[4]][i - 1][0] = array5[0];
				for (int j = 1; j < array5.Length - 1; j++)
				{
					string[] array6 = array5[j].Split(new char[]
					{
						':'
					});
					array3[OptionScr.gI().mapFocus[4]][i - 1][j] = array6[0];
					array[OptionScr.gI().mapFocus[4]][i - 1][j - 1] = array6[1];
					array6[2] = array6[2].Substring(0, array6[2].Length - 1);
					array2[OptionScr.gI().mapFocus[4]][i - 1][j - 1] = int.Parse(array6[2]);
				}
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
			flag = true;
		}
		if (flag)
		{
			this.indexUSer++;
			this.doUpdateServer();
			return;
		}
		GameMidlet.IP = array;
		GameMidlet.PORT = array2;
		GameMidlet.nameSV = array3;
		AvatarData.saveIP();
		Canvas.endDlg();
		if (LoginScr.isLoadIP)
		{
			LoginScr.isLoadIP = false;
			LoginScr.gI().regRequest();
		}
		else
		{
			this.init();
			this.switchToMe();
		}
	}

	// Token: 0x04000D9E RID: 3486
	public static ServerListScr me;

	// Token: 0x04000D9F RID: 3487
	public static int indexSV;

	// Token: 0x04000DA0 RID: 3488
	public static int index;

	// Token: 0x04000DA1 RID: 3489
	public new static int selected;

	// Token: 0x04000DA2 RID: 3490
	public Image imgF;

	// Token: 0x04000DA3 RID: 3491
	public Image imgArr;

	// Token: 0x04000DA4 RID: 3492
	private bool isSelected;

	// Token: 0x04000DA5 RID: 3493
	public static int cmtoY;

	// Token: 0x04000DA6 RID: 3494
	public static int cmy;

	// Token: 0x04000DA7 RID: 3495
	public static int cmdy;

	// Token: 0x04000DA8 RID: 3496
	public static int cmvy;

	// Token: 0x04000DA9 RID: 3497
	public static int cmyLim;

	// Token: 0x04000DAA RID: 3498
	public static int w;

	// Token: 0x04000DAB RID: 3499
	public static int h;

	// Token: 0x04000DAC RID: 3500
	public static int hDis;

	// Token: 0x04000DAD RID: 3501
	public static int x;

	// Token: 0x04000DAE RID: 3502
	public static int y;

	// Token: 0x04000DAF RID: 3503
	private sbyte countClose;

	// Token: 0x04000DB0 RID: 3504
	private int indexUSer;

	// Token: 0x04000DB1 RID: 3505
	private long timeDelay;

	// Token: 0x04000DB2 RID: 3506
	private int vY;

	// Token: 0x04000DB3 RID: 3507
	private bool transY;

	// Token: 0x04000DB4 RID: 3508
	private int pa;

	// Token: 0x04000DB5 RID: 3509
	private string test = string.Empty;

	// Token: 0x04000DB6 RID: 3510
	private long count;

	// Token: 0x04000DB7 RID: 3511
	private long timePoint;

	// Token: 0x04000DB8 RID: 3512
	private int dyTran;

	// Token: 0x04000DB9 RID: 3513
	private int timeOpen;

	// Token: 0x04000DBA RID: 3514
	private int pyLast;

	// Token: 0x04000DBB RID: 3515
	private bool isFire;

	// Token: 0x04000DBC RID: 3516
	private bool isG;

	// Token: 0x04000DBD RID: 3517
	private bool isTranKey;

	// Token: 0x0200018E RID: 398
	private class actDoSettingPassword : IKbAction
	{
		// Token: 0x06000A89 RID: 2697 RVA: 0x00068664 File Offset: 0x00066A64
		public void perform(string text)
		{
			string text2 = Canvas.inputDlg.getText();
			if (text.Equals(string.Empty))
			{
				return;
			}
			LoginScr.gI().doForgetPass(text);
		}
	}
}
