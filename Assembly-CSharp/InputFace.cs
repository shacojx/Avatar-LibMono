using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class InputFace : Face
{
	// Token: 0x060002E7 RID: 743 RVA: 0x00017FBB File Offset: 0x000163BB
	public InputFace()
	{
		this.w = 190 * AvMain.hd;
		this.x = (Canvas.w - this.w) / 2;
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00017FE8 File Offset: 0x000163E8
	public static InputFace gI()
	{
		return (InputFace.me != null) ? InputFace.me : (InputFace.me = new InputFace());
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x00018009 File Offset: 0x00016409
	public override void commandTab(int index)
	{
		if (index != 0)
		{
			Canvas.currentMyScreen.commandTab(index);
			return;
		}
		if (this.iAcClose != null)
		{
			this.iAcClose.perform();
		}
		base.close();
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0001803E File Offset: 0x0001643E
	public override void init(int h)
	{
		h = Canvas.h;
		this.setInfo(this.list, this.title, this.nameChangePass, this.center, h);
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00018068 File Offset: 0x00016468
	public void setInfo(TField[] list, string title, string[][] subName, Command cmd, int hh)
	{
		this.center = cmd;
		this.title = title;
		this.list = list;
		this.nameChangePass = subName;
		this.h = list[0].height * list.Length + 20 * AvMain.hd * (list.Length + 1) + 6 * AvMain.hd;
		if (!TouchScreenKeyboard.visible)
		{
			this.y = (hh - Canvas.transTab - ((AvMain.hFillTab == 0) ? Canvas.hTab : AvMain.hFillTab) - this.h) / 2;
		}
		else
		{
			this.y = (hh - ((AvMain.hFillTab == 0) ? Canvas.hTab : AvMain.hFillTab) - this.h) / 2;
		}
		int num = this.y + 3 * AvMain.hd;
		int num2 = 0;
		for (int i = 0; i < list.Length; i++)
		{
			for (int j = 0; j < subName[i].Length; j++)
			{
				if (num2 < Canvas.normalFont.getWidth(subName[i][j]))
				{
					num2 = Canvas.normalFont.getWidth(subName[i][j]);
				}
			}
		}
		for (int k = 0; k < list.Length; k++)
		{
			list[k].autoScaleScreen = true;
			list[k].showSubTextField = false;
			list[k].width = this.w - 30 * AvMain.hd - num2;
			list[k].x = this.x + this.w - list[k].width - 10 * AvMain.hd;
			num += 20 * AvMain.hd;
			list[k].y = num;
			num += list[k].height;
		}
		this.wTab = Canvas.normalFont.getWidth(title) + 50 * AvMain.hd;
		if (this.wTab < 80 * AvMain.hd)
		{
			this.wTab = 80 * AvMain.hd;
		}
		this.setFocus();
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00018258 File Offset: 0x00016658
	public override void updateKey()
	{
		base.updateKey();
		if (Canvas.isPointerClick && Canvas.isPointer(this.x + this.w - 24 * AvMain.hd, this.y - 19 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
		{
			this.countClose = 5;
			Canvas.isPointerClick = false;
			this.isTranKey = true;
		}
		if (this.isTranKey)
		{
			if (Canvas.isPointerDown && (int)this.countClose == 5 && !Canvas.isPoint(this.x + this.w - 24 * AvMain.hd, this.y - 19 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
			{
				this.countClose = 0;
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranKey = false;
				if ((int)this.countClose == 5)
				{
					this.countClose = 0;
					if (this.iAcClose != null)
					{
						this.iAcClose.perform();
					}
					base.close();
				}
			}
		}
		for (int i = 0; i < this.list.Length; i++)
		{
			this.list[i].update();
		}
		if (Canvas.isPointerClick && ipKeyboard.tk != null)
		{
			ipKeyboard.close();
			Canvas.isPointerClick = false;
		}
	}

	// Token: 0x060002ED RID: 749 RVA: 0x000183BC File Offset: 0x000167BC
	private void setFocus()
	{
		for (int i = 0; i < this.list.Length; i++)
		{
			this.list[i].setFocus(false);
		}
		this.list[this.focus].setFocus(true);
		this.right = null;
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0001840C File Offset: 0x0001680C
	public override void keyPress(int keyCode)
	{
		for (int i = 0; i < this.list.Length; i++)
		{
			if (this.list[i].isFocused())
			{
				this.list[i].keyPressed(keyCode);
			}
		}
		base.keyPress(keyCode);
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0001845C File Offset: 0x0001685C
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		Canvas.paint.paintPopupBack(g, this.x, this.y, this.w, this.h, (int)this.countClose / 3, false);
		for (int i = 0; i < this.list.Length; i++)
		{
			g.setClip((float)(this.x + 4 * AvMain.hd), (float)this.y, (float)(this.w - 8 * AvMain.hd), (float)this.h);
			int num = this.list[i].x - Canvas.normalFont.getWidth(this.nameChangePass[i][0]) - 5;
			if (num > this.x + 10 * AvMain.hd)
			{
				num = this.x + 10 * AvMain.hd;
			}
			int num2 = 2;
			if (this.nameChangePass[i][1].Equals(string.Empty))
			{
				num2 = 1;
			}
			for (int j = 0; j < num2; j++)
			{
				Canvas.normalFont.drawString(g, this.nameChangePass[i][j], num, this.list[i].y + this.list[i].height / 2 - (int)AvMain.hNormal * num2 / 2 + (int)AvMain.hNormal * j, 0);
			}
			this.list[i].paint(g);
		}
		base.paint(g);
	}

	// Token: 0x0400036D RID: 877
	public static InputFace me;

	// Token: 0x0400036E RID: 878
	public TField[] list;

	// Token: 0x0400036F RID: 879
	private string title;

	// Token: 0x04000370 RID: 880
	private int x;

	// Token: 0x04000371 RID: 881
	private int y;

	// Token: 0x04000372 RID: 882
	private int w;

	// Token: 0x04000373 RID: 883
	private int h;

	// Token: 0x04000374 RID: 884
	private int focus;

	// Token: 0x04000375 RID: 885
	private sbyte countClose;

	// Token: 0x04000376 RID: 886
	public IAction iAcClose;

	// Token: 0x04000377 RID: 887
	private int wTab;

	// Token: 0x04000378 RID: 888
	private string[][] nameChangePass;

	// Token: 0x04000379 RID: 889
	private bool isTranKey;
}
