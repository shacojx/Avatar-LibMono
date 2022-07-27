using System;

// Token: 0x02000074 RID: 116
public class TransMoneyDlg : Dialog
{
	// Token: 0x060003D6 RID: 982 RVA: 0x0002474F File Offset: 0x00022B4F
	public static TransMoneyDlg gI()
	{
		return (TransMoneyDlg.me != null) ? TransMoneyDlg.me : (TransMoneyDlg.me = new TransMoneyDlg());
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00024770 File Offset: 0x00022B70
	public override void show()
	{
		this.init();
		Canvas.currentDialog = this;
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00024780 File Offset: 0x00022B80
	private void init()
	{
		if (this.imgButton != null)
		{
			return;
		}
		try
		{
			this.imgButton = new FrameImage(Image.createImagePNG(T.mode[AvMain.hd - 1] + "/hd/on/button"), 65 * AvMain.hd, (AvMain.hd != 2) ? 18 : 37);
		}
		catch (Exception ex)
		{
		}
		this.w = this.imgButton.frameWidth * 3 + 30 * AvMain.hd;
		this.h = this.imgButton.frameHeight * 3 + 60 * AvMain.hd;
		this.x = (Canvas.w - this.w) / 2;
		this.y = (Canvas.h - this.h) / 2;
		this.hItem = this.h / 3;
		this.wItem = this.w / 3;
		this.money = new int[]
		{
			100,
			1000,
			10000,
			50000,
			100000,
			500000,
			1000000,
			5000000,
			10000000
		};
		this.center = new Command(T.selectt, 0, this);
		this.right = new Command(T.close, 1, this);
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x000248B0 File Offset: 0x00022CB0
	public override void commandActionPointer(int index, int subIndex)
	{
		if (index != 0)
		{
			if (index == 1)
			{
				Canvas.currentDialog = null;
			}
		}
		else
		{
			Canvas.startOKDlg(T.doYouWantTransMoney, new TransMoneyDlg.IActionTransXeng());
		}
	}

	// Token: 0x060003DA RID: 986 RVA: 0x000248E3 File Offset: 0x00022CE3
	public void update()
	{
	}

	// Token: 0x060003DB RID: 987 RVA: 0x000248E8 File Offset: 0x00022CE8
	public override void updateKey()
	{
		Canvas.paint.updateKeyOn(this.left, this.center, this.right);
		if (Canvas.isKeyPressed(2))
		{
			if (this.focus / 3 > 0)
			{
				this.focus -= 3;
			}
		}
		else if (Canvas.isKeyPressed(4))
		{
			if (this.focus % 3 > 0)
			{
				this.focus--;
			}
		}
		else if (Canvas.isKeyPressed(6))
		{
			if (this.focus % 3 < 2)
			{
				this.focus++;
			}
		}
		else if (Canvas.isKeyPressed(8) && this.focus / 3 < 2)
		{
			this.focus += 3;
		}
		if (Canvas.isPointerClick)
		{
			for (int i = 0; i < this.money.Length; i++)
			{
				if (Canvas.isPoint(this.x + i % 3 * this.wItem, this.y + i / 3 * this.hItem, this.wItem, this.hItem))
				{
					Canvas.isPointerClick = false;
					this.focus = i;
					break;
				}
			}
		}
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00024A2C File Offset: 0x00022E2C
	public override void paint(MyGraphics g)
	{
		onMainMenu.gI().paintMain(g);
		Canvas.resetTrans(g);
		Canvas.paint.paintTransMoney(g, this.x, this.y, this.w, this.h);
		g.translate((float)this.x, (float)this.y);
		for (int i = 0; i < this.money.Length; i++)
		{
			this.imgButton.drawFrame((this.focus != i) ? 0 : 1, this.wItem / 2 + i % 3 * this.wItem, this.hItem / 2 + i / 3 * this.hItem, 0, 3, g);
			Canvas.smallFontYellow.drawString(g, this.money[i] + string.Empty, this.wItem / 2 + i % 3 * this.wItem, this.hItem / 2 + i / 3 * this.hItem - (int)AvMain.hSmall / 2, 2);
		}
		Canvas.resetTrans(g);
		OnScreen.paintBar(g, this.left, this.center, this.right, null);
	}

	// Token: 0x04000630 RID: 1584
	private FrameImage imgButton;

	// Token: 0x04000631 RID: 1585
	public static TransMoneyDlg me;

	// Token: 0x04000632 RID: 1586
	private int x;

	// Token: 0x04000633 RID: 1587
	private int y;

	// Token: 0x04000634 RID: 1588
	private int w;

	// Token: 0x04000635 RID: 1589
	private int h;

	// Token: 0x04000636 RID: 1590
	private int hItem;

	// Token: 0x04000637 RID: 1591
	private int wItem;

	// Token: 0x04000638 RID: 1592
	private int focus;

	// Token: 0x04000639 RID: 1593
	private int[] money;

	// Token: 0x02000075 RID: 117
	private class IActionTransXeng : IAction
	{
		// Token: 0x060003DE RID: 990 RVA: 0x00024B5A File Offset: 0x00022F5A
		public void perform()
		{
			GlobalService.gI().transXeng(TransMoneyDlg.gI().money[TransMoneyDlg.gI().focus]);
			Canvas.startWaitDlg();
		}
	}
}
