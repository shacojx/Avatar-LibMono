using System;

// Token: 0x02000111 RID: 273
public class InputDlg : Dialog
{
	// Token: 0x06000781 RID: 1921 RVA: 0x00044DC8 File Offset: 0x000431C8
	public InputDlg()
	{
		this.tfInput = new TField(string.Empty, null, new InputDlg.IActionOk());
		this.tfInput.isChangeFocus = false;
		this.tfInput.showSubTextField = false;
		this.tfInput.autoScaleScreen = true;
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x00044E15 File Offset: 0x00043215
	public override void commandTab(int index)
	{
		Canvas.currentMyScreen.commandTab(index);
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00044E2C File Offset: 0x0004322C
	public string getText()
	{
		return this.tfInput.getText();
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x00044E3C File Offset: 0x0004323C
	public override void init(int hCan)
	{
		this.y = hCan - this.h - 50;
		this.tfInput.x = Canvas.hw - this.tfInput.width / 2;
		this.tfInput.y = this.y + this.h - this.tfInput.height - AvMain.hCmd / 2 - 10;
		if (this.center != null)
		{
			this.center.x = Canvas.w / 2;
			this.center.y = this.y + this.h - PaintPopup.hButtonSmall / 2;
		}
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x00044EE4 File Offset: 0x000432E4
	public void setImg(Image img)
	{
		this.img = img;
		this.h += img.getHeight();
		this.init(Canvas.hCan);
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x00044F0C File Offset: 0x0004330C
	public void setInfo(string info, int index, int type, MyScreen parent)
	{
		this.initInfo(info, type);
		this.show();
		this.tfInput.parent = parent;
		this.tfInput.setFocus(true);
		this.tfInput.showSubTextField = false;
		this.center = new Command(T.OK, index, Canvas.hw, this.y + this.h - AvMain.hCmd / 2);
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x00044F78 File Offset: 0x00043378
	public void initInfo(string info, int type)
	{
		this.img = null;
		this.w = Canvas.w - 40;
		if (Canvas.normalFont.getWidth(info) + 20 < this.w)
		{
			this.w = Canvas.normalFont.getWidth(info) + 20;
		}
		if (this.w < Canvas.w / 2)
		{
			this.w = Canvas.w / 2;
		}
		this.info = Canvas.normalFont.splitFontBStrInLine(info, this.w - 20);
		this.h = AvMain.hCmd / 2 + 10 + this.tfInput.height + this.info.Length * (int)AvMain.hNormal + 60;
		this.x = (Canvas.w - this.w) / 2;
		this.y = Canvas.hCan - this.h - 50;
		if (this.center != null)
		{
			this.center.x = Canvas.w / 2;
			this.center.y = this.y + this.h - PaintPopup.hButtonSmall / 2;
		}
		this.tfInput.isChangeFocus = false;
		this.tfInput.width = this.w - 20 * AvMain.hd;
		this.init(Canvas.hCan);
		this.tfInput.setText(string.Empty);
		this.tfInput.setIputType(ipKeyboard.TEXT);
		this.show();
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x000450EC File Offset: 0x000434EC
	public void setInfoIA(string info, IAction ok, int type, MyScreen me)
	{
		this.initInfo(info, type);
		this.okAction = ok;
		this.tfInput.parent = me;
		this.center = new Command(T.OK, this.okAction);
		this.tfInput.showSubTextField = false;
		this.show();
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x00045140 File Offset: 0x00043540
	public void setInfoIkb(string info, IKbAction ac, int type, MyScreen me)
	{
		this.initInfo(info, type);
		this.tfInput.parent = me;
		this.center = new Command(T.OK, ac);
		this.init(Canvas.hCan);
		this.tfInput.showSubTextField = false;
		this.show();
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x00045190 File Offset: 0x00043590
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		Canvas.paint.paintPopupBack(g, this.x, this.y, this.w, this.h, -1, false);
		int num = this.y + (this.tfInput.y - this.y) / 2 - this.info.Length * (int)AvMain.hNormal / 2 + 4 * AvMain.hd;
		if (this.img != null)
		{
			g.drawImage(this.img, (float)(this.x + this.w / 2), (float)(this.tfInput.y - this.img.getHeight() / 2 - 5 * AvMain.hd), 3);
			num -= this.img.getHeight() / 2;
		}
		int i = 0;
		int num2 = num;
		while (i < this.info.Length)
		{
			Canvas.normalFont.drawString(g, this.info[i], Canvas.hw, num2, 2);
			i++;
			num2 += (int)AvMain.hNormal;
		}
		this.tfInput.paint(g);
		base.paint(g);
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x000452A9 File Offset: 0x000436A9
	public override void keyPress(int keyCode)
	{
		this.tfInput.keyPressed(keyCode);
		if (keyCode == -5)
		{
			this.tfInput.action.perform();
		}
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x000452D0 File Offset: 0x000436D0
	public override void updateKey()
	{
		this.tfInput.update();
		if (this.tfInput.isFocused())
		{
			this.right = null;
		}
		base.updateKey();
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x000452FA File Offset: 0x000436FA
	public override void show()
	{
		Canvas.currentDialog = this;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00045302 File Offset: 0x00043702
	public void showWithCaption(string text)
	{
		this.tfInput.name = text;
		this.show();
	}

	// Token: 0x04000977 RID: 2423
	protected string[] info;

	// Token: 0x04000978 RID: 2424
	public TField tfInput;

	// Token: 0x04000979 RID: 2425
	public IAction okAction;

	// Token: 0x0400097A RID: 2426
	private Image img;

	// Token: 0x0400097B RID: 2427
	private int x;

	// Token: 0x0400097C RID: 2428
	private int y;

	// Token: 0x0400097D RID: 2429
	private int w;

	// Token: 0x0400097E RID: 2430
	private int h;

	// Token: 0x02000112 RID: 274
	private class IActionOk : IAction
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x0004531E File Offset: 0x0004371E
		public void perform()
		{
			Canvas.inputDlg.center.perform();
		}
	}
}
