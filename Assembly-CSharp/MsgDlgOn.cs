using System;

// Token: 0x0200017A RID: 378
public class MsgDlgOn : Dialog
{
	// Token: 0x060009EE RID: 2542 RVA: 0x000619F4 File Offset: 0x0005FDF4
	public override void show()
	{
		Canvas.currentDialog = this;
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x000619FC File Offset: 0x0005FDFC
	public void setInfo(string info, Command left, Command center, Command right)
	{
		this.info = Canvas.normalWhiteFont.splitFontBStrInLine(info, Canvas.w - 200 * ScaleGUI.numScale);
		this.left = left;
		this.center = center;
		this.right = right;
		if (info.Equals(T.pleaseWait))
		{
			this.isPaint = false;
			right = center;
			center = null;
		}
		else
		{
			this.isPaint = true;
		}
		this.firstMills = Environment.TickCount;
		this.h = 90 * ScaleGUI.numScale;
		if (this.info.Length >= 5)
		{
			this.h = this.info.Length * Canvas.normalWhiteFont.getHeight() + 20 * ScaleGUI.numScale;
		}
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x00061AB3 File Offset: 0x0005FEB3
	public override void updateKey()
	{
		Canvas.paint.updateKeyOn(this.left, this.center, this.right);
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x00061AD4 File Offset: 0x0005FED4
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		if (!this.isPaint)
		{
			this.currMills = Environment.TickCount;
			if (this.currMills - this.firstMills > 500)
			{
				this.isPaint = true;
			}
			return;
		}
		int num = Canvas.h - this.h - 30 + 5 * AvMain.hd;
		Canvas.paint.paintDefaultPopup(g, 80 * ScaleGUI.numScale, num, Canvas.w - 160 * ScaleGUI.numScale, this.h);
		int num2 = num + (this.h - this.info.Length * Canvas.normalWhiteFont.getHeight()) / 2;
		if (this.isBusy)
		{
			MsgDlgOn.imgBusy.drawFrame(Canvas.gameTick % 24 / 2, Canvas.hw - 90 * ScaleGUI.numScale, num2 + 10 * ScaleGUI.numScale, 0, 3, g);
		}
		int i = 0;
		int num3 = num2;
		while (i < this.info.Length)
		{
			Canvas.normalWhiteFont.drawString(g, this.info[i], Canvas.hw, num3, 2);
			i++;
			num3 += Canvas.normalWhiteFont.getHeight();
		}
		Canvas.resetTrans(g);
		Canvas.paint.paintTabSoft(g);
		if (this.isPaint)
		{
			Canvas.paint.paintCmdBar(g, this.left, this.center, this.right);
		}
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x00061C33 File Offset: 0x00060033
	public override void commandTab(int index)
	{
		if (index != -2)
		{
			if (index != -1)
			{
				Canvas.currentMyScreen.commandTab(index);
			}
			else
			{
				Canvas.currentDialog = null;
			}
		}
		else
		{
			MapScr.gI().doExitGame();
		}
	}

	// Token: 0x04000CCC RID: 3276
	public string[] info;

	// Token: 0x04000CCD RID: 3277
	public bool isBusy;

	// Token: 0x04000CCE RID: 3278
	private int h;

	// Token: 0x04000CCF RID: 3279
	private int firstMills;

	// Token: 0x04000CD0 RID: 3280
	private int currMills;

	// Token: 0x04000CD1 RID: 3281
	private bool isPaint;

	// Token: 0x04000CD2 RID: 3282
	public static FrameImage imgBusy;
}
