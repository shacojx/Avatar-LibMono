using System;
using UnityEngine;

// Token: 0x0200017D RID: 381
public abstract class MyScreen : AvMain
{
	// Token: 0x060009FF RID: 2559 RVA: 0x0001FA28 File Offset: 0x0001DE28
	public virtual void setSelected(int se, bool iss)
	{
		this.selected = se;
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x0001FA31 File Offset: 0x0001DE31
	public void action(int sel)
	{
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0001FA33 File Offset: 0x0001DE33
	public virtual void doMenu()
	{
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x0001FA35 File Offset: 0x0001DE35
	public virtual void doSetting()
	{
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x0001FA37 File Offset: 0x0001DE37
	public virtual void doMenuTab()
	{
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x0001FA39 File Offset: 0x0001DE39
	public virtual void close()
	{
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x0001FA3B File Offset: 0x0001DE3B
	public static int getHTF()
	{
		return Canvas.h - MyScreen.hTab - 17 * AvMain.hd;
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0001FA51 File Offset: 0x0001DE51
	public virtual void setHidePointer(bool iss)
	{
		this.isHide = iss;
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0001FA5A File Offset: 0x0001DE5A
	public virtual void switchToMe()
	{
		Canvas.currentMyScreen = this;
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0001FA62 File Offset: 0x0001DE62
	public virtual void initZoom()
	{
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0001FA64 File Offset: 0x0001DE64
	public override void paint(MyGraphics g)
	{
		if (Canvas.menuMain != null)
		{
			return;
		}
		if (Canvas.currentDialog == null && Canvas.currentFace == null && !ChatTextField.isShow)
		{
			base.paint(g);
		}
		else
		{
			Canvas.resetTransNotZoom(g);
		}
		if (!Session_ME.gI().isConnected())
		{
			Canvas.arialFont.drawString(g, "2.5.8", Canvas.posByteCOunt.x, Canvas.posByteCOunt.y, Canvas.posByteCOunt.anchor);
		}
		else if (Canvas.currentMyScreen == ServerListScr.me)
		{
			Canvas.arialFont.drawString(g, Session_ME.strRecvByteCount + string.Empty, Canvas.posByteCOunt.x, Canvas.posByteCOunt.y, Canvas.posByteCOunt.anchor);
		}
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0001FB35 File Offset: 0x0001DF35
	public virtual void paintMain(MyGraphics g)
	{
	}

	// Token: 0x06000A0B RID: 2571
	public abstract void update();

	// Token: 0x06000A0C RID: 2572 RVA: 0x0001FB37 File Offset: 0x0001DF37
	public override void keyPress(int keyCode)
	{
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0001FB39 File Offset: 0x0001DF39
	public override void commandTab(int index)
	{
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0001FB3B File Offset: 0x0001DF3B
	public override void commandAction(int index)
	{
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0001FB3D File Offset: 0x0001DF3D
	public virtual void doChat(string text)
	{
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0001FB3F File Offset: 0x0001DF3F
	public void repaint()
	{
	}

	// Token: 0x04000CED RID: 3309
	public static int ITEM_HEIGHT = 20;

	// Token: 0x04000CEE RID: 3310
	public static Image imgLogo;

	// Token: 0x04000CEF RID: 3311
	public static Image[] imgChat;

	// Token: 0x04000CF0 RID: 3312
	public int selected;

	// Token: 0x04000CF1 RID: 3313
	public static int nMsg = 0;

	// Token: 0x04000CF2 RID: 3314
	public static int hTab = 20;

	// Token: 0x04000CF3 RID: 3315
	public static int wTab;

	// Token: 0x04000CF4 RID: 3316
	public static int hText;

	// Token: 0x04000CF5 RID: 3317
	public static int colorBar = 0;

	// Token: 0x04000CF6 RID: 3318
	public static Color color;

	// Token: 0x04000CF7 RID: 3319
	public static int colorMiniMap = 0;

	// Token: 0x04000CF8 RID: 3320
	public static int colorPark = 8705740;

	// Token: 0x04000CF9 RID: 3321
	public static int[] colorCity = new int[]
	{
		4802889,
		3092271
	};

	// Token: 0x04000CFA RID: 3322
	public static int[] colorFarmPath = new int[]
	{
		14400144,
		12689526
	};
}
