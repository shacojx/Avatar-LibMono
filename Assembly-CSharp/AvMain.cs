using System;

// Token: 0x02000029 RID: 41
public class AvMain
{
	// Token: 0x06000199 RID: 409 RVA: 0x0000C99A File Offset: 0x0000AD9A
	static AvMain()
	{
		if (Main.hdtype == 0)
		{
			AvMain.hd = 1;
		}
		if (Main.hdtype == 1)
		{
			AvMain.hd = 1;
		}
		if (Main.hdtype == 2)
		{
			AvMain.hd = 2;
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000C9E0 File Offset: 0x0000ADE0
	public virtual void keyPress(int keyCode)
	{
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000C9E2 File Offset: 0x0000ADE2
	public virtual void paint(MyGraphics g)
	{
		Canvas.resetTransNotZoom(g);
		Canvas.paint.paintCmd(g, this.left, this.center, this.right);
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0000CA07 File Offset: 0x0000AE07
	public virtual void commandAction(int index)
	{
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000CA09 File Offset: 0x0000AE09
	public virtual void commandActionPointer(int index, int subIndex)
	{
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000CA0B File Offset: 0x0000AE0B
	public virtual void commandTab(int index)
	{
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000CA0D File Offset: 0x0000AE0D
	public virtual void closeTabAll()
	{
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000CA0F File Offset: 0x0000AE0F
	public virtual void initTabTrans()
	{
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000CA11 File Offset: 0x0000AE11
	private void click(Command cmd)
	{
		if (cmd != null)
		{
			Canvas.isPointerClick = false;
			Canvas.isPointerRelease = false;
			Canvas.endDlg();
			this.perform(cmd);
			SoundManager.playSound(7);
			AvMain.indexRight = (AvMain.indexCenter = (AvMain.indexLeft = 0));
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x0000CA4C File Offset: 0x0000AE4C
	public virtual void updateKey()
	{
		if (AvMain.lsplash > 0)
		{
			AvMain.lsplash--;
		}
		if (AvMain.csplash > 0)
		{
			AvMain.csplash--;
		}
		if (AvMain.rsplash > 0)
		{
			AvMain.rsplash--;
		}
		if (Canvas.currentDialog != null || Canvas.welcome == null || !Welcome.isPaintArrow || Welcome.lastScr != Canvas.currentMyScreen)
		{
			Canvas.paint.setDrawPointer(this.left, this.center, this.right);
			if (Canvas.currentMyScreen != MessageScr.me)
			{
				Canvas.paint.setBack();
			}
		}
		else if (Canvas.currentMyScreen != MessageScr.me && (Canvas.welcome == null || !Welcome.isPaintArrow || Welcome.lastScr != Canvas.currentMyScreen))
		{
			Canvas.paint.setBack();
		}
		if (Canvas.isPointerRelease)
		{
			int num = Canvas.paint.collisionCmdBar(this.left, this.center, this.right);
			if (num != 0)
			{
				if (num != 1)
				{
					if (num == 2)
					{
						if (AvMain.indexRight == 4 || Canvas.stypeInt == 0)
						{
							this.click(this.right);
						}
					}
				}
				else if (AvMain.indexCenter == 4 || Canvas.stypeInt == 0)
				{
					this.click(this.center);
				}
			}
			else if (AvMain.indexLeft == 4 || Canvas.stypeInt == 0)
			{
				this.click(this.left);
			}
			AvMain.indexRight = (AvMain.indexCenter = (AvMain.indexLeft = 0));
		}
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x0000CC08 File Offset: 0x0000B008
	public virtual void perform(Command cmd)
	{
		if (cmd != null)
		{
			if (cmd.action != null || cmd.ipaction != null)
			{
				cmd.perform();
			}
			else if (cmd.pointer != null)
			{
				cmd.pointer.commandActionPointer((int)cmd.indexMenu, (int)cmd.subIndex);
			}
			else
			{
				this.commandTab((int)cmd.indexMenu);
			}
		}
	}

	// Token: 0x04000190 RID: 400
	public static int hd;

	// Token: 0x04000191 RID: 401
	public static int hDuBox;

	// Token: 0x04000192 RID: 402
	public static int duPopup;

	// Token: 0x04000193 RID: 403
	public static int hFillTab;

	// Token: 0x04000194 RID: 404
	public static int hCmd;

	// Token: 0x04000195 RID: 405
	public static sbyte hBlack;

	// Token: 0x04000196 RID: 406
	public static sbyte hBorder;

	// Token: 0x04000197 RID: 407
	public static sbyte hNormal;

	// Token: 0x04000198 RID: 408
	public static sbyte hSmall;

	// Token: 0x04000199 RID: 409
	public static sbyte hBlack2;

	// Token: 0x0400019A RID: 410
	public static float zoom = 1f;

	// Token: 0x0400019B RID: 411
	public static byte indexLeft;

	// Token: 0x0400019C RID: 412
	public static byte indexCenter;

	// Token: 0x0400019D RID: 413
	public static byte indexRight;

	// Token: 0x0400019E RID: 414
	public Command left;

	// Token: 0x0400019F RID: 415
	public Command center;

	// Token: 0x040001A0 RID: 416
	public Command right;

	// Token: 0x040001A1 RID: 417
	public bool isHide;

	// Token: 0x040001A2 RID: 418
	public bool isTran;

	// Token: 0x040001A3 RID: 419
	public static bool isQwerty;

	// Token: 0x040001A4 RID: 420
	public static int lsplash;

	// Token: 0x040001A5 RID: 421
	public static int csplash;

	// Token: 0x040001A6 RID: 422
	public static int rsplash;

	// Token: 0x040001A7 RID: 423
	public static string lsplashs;

	// Token: 0x040001A8 RID: 424
	public static string csplashs;

	// Token: 0x040001A9 RID: 425
	public static string rsplashs;
}
