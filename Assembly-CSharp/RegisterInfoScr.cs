using System;

// Token: 0x02000186 RID: 390
public class RegisterInfoScr : MyScreen
{
	// Token: 0x06000A48 RID: 2632 RVA: 0x00065C2D File Offset: 0x0006402D
	public static RegisterInfoScr gI()
	{
		return (RegisterInfoScr.me != null) ? RegisterInfoScr.me : (RegisterInfoScr.me = new RegisterInfoScr());
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x00065C50 File Offset: 0x00064050
	public void start(bool isTrue)
	{
		this.w = 240 * AvMain.hd;
		if (this.w > Canvas.w)
		{
			this.w = Canvas.w;
		}
		this.h = 210 * AvMain.hd;
		this.x = (Canvas.w - this.w) / 2;
		this.y = (Canvas.h - this.h) / 2 - PaintPopup.hButtonSmall / 2 + 10 * AvMain.hd;
		int num = this.y + 5 * AvMain.hd;
		this.tfUser = new TField(string.Empty, null, new RegisterInfoScr.IActionOk());
		this.tfUser.name = "tfUser";
		this.tfUser.sDefaust = "Họ và tên";
		this.tfUser.isChangeFocus = false;
		this.tfUser.width = this.w - 22 * AvMain.hd;
		if (isTrue)
		{
			this.tfUser.setText("Nguyển Văn A");
		}
		else
		{
			this.tfUser.setText(string.Empty);
		}
		this.tfUser.setIputType(3);
		this.tfUser.x = this.x + 14 * AvMain.hd;
		this.tfUser.y = num;
		num += this.tfUser.height + 2 * AvMain.hd;
		int width = (this.w - 40 * AvMain.hd) / 3;
		this.tfnam = new TField(string.Empty, null, new RegisterInfoScr.IActionOk());
		this.tfnam.name = "tfnam";
		this.tfnam.sDefaust = "Năm";
		this.tfnam.isChangeFocus = false;
		this.tfnam.width = width;
		if (isTrue)
		{
			this.tfnam.setText("1987");
		}
		else
		{
			this.tfnam.setText(string.Empty);
		}
		this.tfnam.setIputType(1);
		this.tfnam.x = this.x + this.w - 8 * AvMain.hd - this.tfnam.width;
		this.tfnam.y = num;
		this.tfThang = new TField(string.Empty, null, new RegisterInfoScr.IActionOk());
		this.tfThang.name = "tfThang";
		this.tfThang.sDefaust = "Tháng";
		this.tfThang.isChangeFocus = false;
		this.tfThang.width = width;
		if (isTrue)
		{
			this.tfThang.setText("1");
		}
		else
		{
			this.tfThang.setText(string.Empty);
		}
		this.tfThang.setIputType(1);
		this.tfThang.x = this.x + this.w / 2 - this.tfThang.width / 2 + AvMain.hd;
		this.tfThang.y = num;
		this.tfNgay = new TField(string.Empty, null, new RegisterInfoScr.IActionOk());
		this.tfNgay.name = "tfNgay";
		this.tfNgay.sDefaust = "Ngày";
		this.tfNgay.isChangeFocus = false;
		this.tfNgay.width = width;
		if (isTrue)
		{
			this.tfNgay.setText("1");
		}
		else
		{
			this.tfNgay.setText(string.Empty);
		}
		this.tfNgay.setIputType(1);
		this.tfNgay.x = this.x + 13 * AvMain.hd;
		this.tfNgay.y = num;
		num += this.tfUser.height + 2 * AvMain.hd;
		this.tfAddress = new TField(string.Empty, null, new RegisterInfoScr.IActionOk());
		this.tfAddress.name = "tfAddress";
		this.tfAddress.sDefaust = "Địa chỉ";
		this.tfAddress.isChangeFocus = false;
		this.tfAddress.width = this.w - 22 * AvMain.hd;
		if (isTrue)
		{
			this.tfAddress.setText("Ho Chi Minh");
		}
		else
		{
			this.tfAddress.setText(string.Empty);
		}
		this.tfAddress.setIputType(0);
		this.tfAddress.x = this.x + 14 * AvMain.hd;
		this.tfAddress.y = num;
		num += this.tfUser.height + 2 * AvMain.hd;
		this.tfCMND = new TField(string.Empty, null, new RegisterInfoScr.IActionOk());
		this.tfCMND.name = "tfCMND";
		this.tfCMND.sDefaust = "Số CMND hoặc hộ chiếu";
		this.tfCMND.isChangeFocus = false;
		this.tfCMND.width = this.w - 22 * AvMain.hd;
		if (isTrue)
		{
			this.tfCMND.setText("0123456789");
		}
		else
		{
			this.tfCMND.setText(string.Empty);
		}
		this.tfCMND.setIputType(1);
		this.tfCMND.x = this.x + 14 * AvMain.hd;
		this.tfCMND.y = num;
		num += this.tfUser.height + 2 * AvMain.hd;
		this.tfNgayCap = new TField(string.Empty, null, new RegisterInfoScr.IActionOk());
		this.tfNgayCap.name = "tfNgayCap";
		this.tfNgayCap.sDefaust = "Ngày cấp";
		this.tfNgayCap.isChangeFocus = false;
		this.tfNgayCap.width = (this.w - 42 * AvMain.hd) / 2;
		if (isTrue)
		{
			this.tfNgayCap.setText("1/1/2010");
		}
		else
		{
			this.tfNgayCap.setText(string.Empty);
		}
		this.tfNgayCap.setIputType(0);
		this.tfNgayCap.x = this.x + 14 * AvMain.hd;
		this.tfNgayCap.y = num;
		this.tfNoiCap = new TField(string.Empty, null, new RegisterInfoScr.IActionOk());
		this.tfNoiCap.name = "tfNoiCap";
		this.tfNoiCap.sDefaust = "Nơi cấp";
		this.tfNoiCap.isChangeFocus = false;
		this.tfNoiCap.width = (this.w - 22 * AvMain.hd) / 2;
		if (isTrue)
		{
			this.tfNoiCap.setText("Ho Chi Minh");
		}
		else
		{
			this.tfNoiCap.setText(string.Empty);
		}
		this.tfNoiCap.setIputType(0);
		this.tfNoiCap.x = this.x + this.w - (this.w - 22 * AvMain.hd) / 2 - 10 * AvMain.hd;
		this.tfNoiCap.y = num;
		num += this.tfUser.height + 2 * AvMain.hd;
		this.tfNumberPhone = new TField(string.Empty, null, new RegisterInfoScr.IActionOk());
		this.tfNumberPhone.name = "tfNumberPhone";
		this.tfNumberPhone.sDefaust = "Số điện thoại";
		this.tfNumberPhone.isChangeFocus = false;
		this.tfNumberPhone.width = this.w - 22 * AvMain.hd;
		if (isTrue)
		{
			this.tfNumberPhone.setText("0123456789");
		}
		else
		{
			this.tfNumberPhone.setText(string.Empty);
		}
		this.tfNumberPhone.setIputType(0);
		this.tfNumberPhone.x = this.x + 14 * AvMain.hd;
		this.tfNumberPhone.y = num;
		this.strInfo = Canvas.fontChatB.splitFontBStrInLine("Dưới 18 tuổi chỉ có thể chơi 180 phút 1 ngày.", this.w - 20 * AvMain.hd);
		this.h = num + Canvas.fontChatB.getHeight() * this.strInfo.Length + 5 * AvMain.hd;
		if (this.h > Canvas.hCan - PaintPopup.hButtonSmall)
		{
			this.h = Canvas.hCan - PaintPopup.hButtonSmall;
		}
		this.switchToMe();
		this.cmyLim = num - this.w;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmtoY = 0;
		this.cmy = 0;
		this.center = new Command("Tạo", new RegisterInfoScr.IActionTao());
		this.center.x = this.x + this.w / 2;
		this.center.y = this.y + this.h + 2 * AvMain.hd;
		Canvas.endDlg();
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x000664F3 File Offset: 0x000648F3
	public override void update()
	{
		this.updateText();
		this.moveCamera();
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x00066504 File Offset: 0x00064904
	private void updateText()
	{
		if (!this.tfNgay.getText().Equals(string.Empty))
		{
			string text = this.tfNgay.getText();
			try
			{
				int num = int.Parse(text);
			}
			catch (Exception ex)
			{
				this.tfNgay.setText("1");
			}
		}
		if (!this.tfThang.getText().Equals(string.Empty))
		{
			string text2 = this.tfThang.getText();
			try
			{
				int num2 = int.Parse(text2);
			}
			catch (Exception ex2)
			{
				this.tfThang.setText("1");
			}
		}
		if (!this.tfnam.getText().Equals(string.Empty))
		{
			string text3 = this.tfnam.getText();
			try
			{
				int num3 = int.Parse(text3);
			}
			catch (Exception ex3)
			{
				this.tfnam.setText("1987");
			}
		}
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x00066624 File Offset: 0x00064A24
	public void create()
	{
		if (this.tfUser.Equals(string.Empty))
		{
			Canvas.startOKDlg("Bạn chưa nhập họ và tên");
			return;
		}
		if (this.tfNgay.Equals(string.Empty))
		{
			Canvas.startOKDlg("Bạn chưa nhập ngày sinh");
			return;
		}
		if (this.tfThang.Equals(string.Empty))
		{
			Canvas.startOKDlg("Bạn chưa nhập tháng sinh");
			return;
		}
		if (this.tfnam.Equals(string.Empty))
		{
			Canvas.startOKDlg("Bạn chưa nhập năm sinh");
			return;
		}
		if (this.tfAddress.Equals(string.Empty))
		{
			Canvas.startOKDlg("Bạn chưa nhập địa chỉ");
			return;
		}
		if (this.tfCMND.Equals(string.Empty))
		{
			Canvas.startOKDlg("Bạn chưa nhập CMND");
			return;
		}
		if (this.tfNgayCap.Equals(string.Empty))
		{
			Canvas.startOKDlg("Bạn chưa nhập ngày cấp CMND");
			return;
		}
		if (this.tfNoiCap.Equals(string.Empty))
		{
			Canvas.startOKDlg("Bạn chưa nhập nơi cấp CMND");
			return;
		}
		Canvas.startWaitDlg();
		GlobalService.gI().createCharInfo(this.tfUser.getText(), int.Parse(this.tfNgay.getText()), int.Parse(this.tfThang.getText()), int.Parse(this.tfnam.getText()), this.tfAddress.getText(), this.tfCMND.getText(), this.tfNgayCap.getText(), this.tfNoiCap.getText(), this.tfNumberPhone.getText());
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x000667B4 File Offset: 0x00064BB4
	public void moveCamera()
	{
		if (this.timeOpen > 0)
		{
			this.timeOpen--;
		}
		if (this.vY != 0)
		{
			if (this.cmy < 0 || this.cmy > this.cmyLim)
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
			this.cmy += this.vY / 15;
			this.cmtoY = this.cmy;
			if (this.vY != 0)
			{
				this.vY -= this.vY / 20 + 1;
			}
		}
		else if (this.cmy < 0)
		{
			this.cmtoY = 0;
		}
		else if (this.cmy > this.cmyLim)
		{
			this.cmtoY = this.cmyLim;
		}
		if (this.cmy != this.cmtoY)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0006694C File Offset: 0x00064D4C
	public override void keyPress(int keyCode)
	{
		if (this.tfUser.isFocused())
		{
			this.tfUser.keyPressed(keyCode);
		}
		else if (this.tfnam.isFocused())
		{
			this.tfnam.keyPressed(keyCode);
		}
		else if (this.tfThang.isFocused())
		{
			this.tfThang.keyPressed(keyCode);
		}
		else if (this.tfNgay.isFocused())
		{
			this.tfNgay.keyPressed(keyCode);
		}
		else if (this.tfAddress.isFocused())
		{
			this.tfAddress.keyPressed(keyCode);
		}
		else if (this.tfCMND.isFocused())
		{
			this.tfCMND.keyPressed(keyCode);
		}
		if (this.tfNgayCap.isFocused())
		{
			this.tfNgayCap.keyPressed(keyCode);
		}
		else if (this.tfNoiCap.isFocused())
		{
			this.tfNoiCap.keyPressed(keyCode);
		}
		else if (this.tfNumberPhone.isFocused())
		{
			this.tfNumberPhone.keyPressed(keyCode);
		}
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x00066A84 File Offset: 0x00064E84
	public override void updateKey()
	{
		base.updateKey();
		this.tfUser.update();
		this.tfnam.update();
		this.tfThang.update();
		this.tfNgay.update();
		this.tfAddress.update();
		this.tfCMND.update();
		this.tfNgayCap.update();
		this.tfNoiCap.update();
		this.tfNumberPhone.update();
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x00066AFC File Offset: 0x00064EFC
	public override void paint(MyGraphics g)
	{
		Canvas.loadMap.paint(g);
		Canvas.loadMap.paintObject(g);
		Canvas.resetTrans(g);
		if (Canvas.currentDialog != null)
		{
			return;
		}
		Canvas.paint.paintPopupBack(g, this.x, this.y, this.w, this.h, -1, false);
		for (int i = 0; i < this.strInfo.Length; i++)
		{
			Canvas.fontChatB.drawString(g, this.strInfo[i], this.x + this.w / 2, this.y + this.h - 5 * AvMain.hd - (Canvas.fontChatB.getHeight() - 2 * AvMain.hd) * this.strInfo.Length + i * (Canvas.fontChatB.getHeight() - 2), 2);
		}
		g.setClip((float)this.x, (float)(this.y + 4 * AvMain.hd), (float)this.w, (float)(this.h - 8 * AvMain.hd));
		g.translate(0f, (float)(-(float)this.cmy));
		this.tfUser.paint(g);
		this.tfnam.paint(g);
		this.tfThang.paint(g);
		this.tfNgay.paint(g);
		this.tfAddress.paint(g);
		this.tfCMND.paint(g);
		this.tfNgayCap.paint(g);
		this.tfNoiCap.paint(g);
		this.tfNumberPhone.paint(g);
		Canvas.resetTrans(g);
		base.paint(g);
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x00066C8F File Offset: 0x0006508F
	public void onCreate()
	{
		Out.println("onCreate");
		Canvas.endDlg();
		MapScr.gI().joinCitymap();
	}

	// Token: 0x04000D68 RID: 3432
	public static RegisterInfoScr me;

	// Token: 0x04000D69 RID: 3433
	private int x;

	// Token: 0x04000D6A RID: 3434
	private int y;

	// Token: 0x04000D6B RID: 3435
	private int w;

	// Token: 0x04000D6C RID: 3436
	private int h;

	// Token: 0x04000D6D RID: 3437
	public TField tfUser;

	// Token: 0x04000D6E RID: 3438
	public TField tfNgay;

	// Token: 0x04000D6F RID: 3439
	public TField tfThang;

	// Token: 0x04000D70 RID: 3440
	public TField tfnam;

	// Token: 0x04000D71 RID: 3441
	public TField tfAddress;

	// Token: 0x04000D72 RID: 3442
	public TField tfCMND;

	// Token: 0x04000D73 RID: 3443
	public TField tfNgayCap;

	// Token: 0x04000D74 RID: 3444
	public TField tfNoiCap;

	// Token: 0x04000D75 RID: 3445
	public TField tfNumberPhone;

	// Token: 0x04000D76 RID: 3446
	private int cmtoY;

	// Token: 0x04000D77 RID: 3447
	private int cmy;

	// Token: 0x04000D78 RID: 3448
	private int cmdy;

	// Token: 0x04000D79 RID: 3449
	private int cmvy;

	// Token: 0x04000D7A RID: 3450
	private int cmyLim;

	// Token: 0x04000D7B RID: 3451
	public static bool isCreate;

	// Token: 0x04000D7C RID: 3452
	public static bool isTrue;

	// Token: 0x04000D7D RID: 3453
	private string[] strInfo;

	// Token: 0x04000D7E RID: 3454
	private int pa;

	// Token: 0x04000D7F RID: 3455
	private int dyTran;

	// Token: 0x04000D80 RID: 3456
	private int timeOpen;

	// Token: 0x04000D81 RID: 3457
	private int vY;

	// Token: 0x04000D82 RID: 3458
	private int pyLast;

	// Token: 0x04000D83 RID: 3459
	private int yCamLast;

	// Token: 0x04000D84 RID: 3460
	private bool trans;

	// Token: 0x04000D85 RID: 3461
	private bool isClick;

	// Token: 0x04000D86 RID: 3462
	private new bool isHide;

	// Token: 0x04000D87 RID: 3463
	private bool changeFocus;

	// Token: 0x04000D88 RID: 3464
	private bool changeIndex;

	// Token: 0x04000D89 RID: 3465
	private bool transY;

	// Token: 0x04000D8A RID: 3466
	private bool tranImage;

	// Token: 0x04000D8B RID: 3467
	private long timeDelay;

	// Token: 0x04000D8C RID: 3468
	private long count;

	// Token: 0x04000D8D RID: 3469
	private long timePoint;

	// Token: 0x04000D8E RID: 3470
	private long timePointY;

	// Token: 0x02000187 RID: 391
	private class IActionTao : IAction
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x00066CB2 File Offset: 0x000650B2
		public void perform()
		{
			RegisterInfoScr.gI().create();
		}
	}

	// Token: 0x02000188 RID: 392
	private class IActionOk : IAction
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x00066CC6 File Offset: 0x000650C6
		public void perform()
		{
			Out.println("IActionOk");
		}
	}
}
