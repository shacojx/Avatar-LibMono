using System;

// Token: 0x0200018B RID: 395
public class ReportDlg : Dialog
{
	// Token: 0x06000A6A RID: 2666 RVA: 0x000673F1 File Offset: 0x000657F1
	public ReportDlg()
	{
		this.center = new Command(T.OK, new ReportDlg.IActionFinishReport());
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0006740E File Offset: 0x0006580E
	public static ReportDlg gI()
	{
		if (ReportDlg.instance == null)
		{
			ReportDlg.instance = new ReportDlg();
		}
		return ReportDlg.instance;
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x00067429 File Offset: 0x00065829
	public override void show()
	{
		Canvas.currentDialog = this;
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x00067431 File Offset: 0x00065831
	public void update()
	{
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x00067433 File Offset: 0x00065833
	public override void updateKey()
	{
		Canvas.paint.updateKeyOn(this.left, this.center, this.right);
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x00067454 File Offset: 0x00065854
	public override void paint(MyGraphics g)
	{
		this.y = Canvas.h - ((AvMain.hFillTab == 0) ? Canvas.hTab : AvMain.hFillTab) - this.h - 10;
		Canvas.paint.paintPopupBack(g, 8, this.y, Canvas.w - 16, this.h, -1, false);
		this.y += 5 + AvMain.hDuBox - Canvas.arialFont.getHeight() / 2;
		for (int i = 0; i < this.list.size(); i++)
		{
			string st = (string)this.list.elementAt(i);
			Canvas.arialFont.drawString(g, st, 40, this.y + 3, 0);
			this.y += Canvas.arialFont.getHeight();
		}
		Canvas.resetTrans(g);
		Canvas.paint.paintTabSoft(g);
		Canvas.paint.paintCmdBar(g, this.left, this.center, this.right);
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0006755D File Offset: 0x0006595D
	public void setInfo(MyVector matchResult)
	{
		this.list = matchResult;
		this.h = this.list.size() * Canvas.normalFont.getHeight() + AvMain.hDuBox * 2 + 10;
	}

	// Token: 0x04000D9A RID: 3482
	public MyVector list;

	// Token: 0x04000D9B RID: 3483
	private static ReportDlg instance;

	// Token: 0x04000D9C RID: 3484
	private int y;

	// Token: 0x04000D9D RID: 3485
	private int h;

	// Token: 0x0200018C RID: 396
	private class IActionFinishReport : IAction
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x00067595 File Offset: 0x00065995
		public void perform()
		{
			Canvas.endDlg();
			ReportDlg.instance = null;
		}
	}
}
