using System;

// Token: 0x020000BD RID: 189
public class DialLuckyScr : MyScreen
{
	// Token: 0x06000608 RID: 1544 RVA: 0x000386E4 File Offset: 0x00036AE4
	public DialLuckyScr()
	{
		this.radius = 90 * AvMain.hd;
		this.posCenter = new AvPosition(Canvas.w, Canvas.hh);
		this.part = 30;
		this.num = 360 / this.part;
		this.cmdDial = new Command(T.quay, 0, this);
		this.cmdWait = new Command(T.pleaseWait, 1, this);
		this.cmdClose = new Command(T.close, 2, this);
		this.center = this.cmdDial;
		this.degreeKim = 90;
		this.isFireWork = new bool[3];
		this.listFireWork = new MyVector();
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x000387A0 File Offset: 0x00036BA0
	public static DialLuckyScr gI()
	{
		return (DialLuckyScr.me != null) ? DialLuckyScr.me : (DialLuckyScr.me = new DialLuckyScr());
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x000387C1 File Offset: 0x00036BC1
	public void switchToMe(MyScreen currentMyScreen, short idPart)
	{
		this.lastScr = currentMyScreen;
		this.idPart = idPart;
		Canvas.keyHold[5] = false;
		base.switchToMe();
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x000387E0 File Offset: 0x00036BE0
	public override void commandActionPointer(int index, int subIndex)
	{
		if (index != 0)
		{
			if (index != 1)
			{
				if (index == 2)
				{
					this.lastScr.switchToMe();
					this.doContinue();
				}
			}
		}
		else
		{
			this.isable = true;
			if (this.degreeKim > 90)
			{
				this.start();
			}
		}
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x00038840 File Offset: 0x00036C40
	protected void doContinue()
	{
		this.isPaint = false;
		this.center = this.cmdDial;
		for (int i = 0; i < 3; i++)
		{
			this.isFireWork[i] = false;
		}
		this.listFireWork.removeAllElements();
		this.setItemBay(this.listGift, GameMidlet.avatar, 0);
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x00038898 File Offset: 0x00036C98
	private void setItemBay(MyVector list, Avatar ava, int delay)
	{
		int num = delay;
		for (int i = 0; i < list.size(); i++)
		{
			Gift gift = (Gift)list.elementAt(i);
			string text = string.Empty;
			switch (gift.type)
			{
			case 1:
			{
				Part part = AvatarData.getPart(gift.idPart);
				ImageInfo imageInfo = AvatarData.listImgInfo[(int)part.idIcon];
				Canvas.addFlyText(0, ava.x, ava.y - 50, -1, CRes.createImgByImg((int)imageInfo.x0 * AvMain.hd, (int)imageInfo.y0 * AvMain.hd, (int)imageInfo.w * AvMain.hd, (int)imageInfo.h * AvMain.hd, AvatarData.getBigImgInfo((int)imageInfo.bigID).img), num);
				break;
			}
			case 2:
				text = "+" + gift.xu + T.xu;
				ava.setMoney(ava.money[0] + gift.xu);
				num += 20;
				break;
			case 3:
				text = "+" + gift.xp + " xp";
				ava.setExp(ava.exp + gift.xp);
				num += 20;
				break;
			case 4:
				text = "+" + gift.luong + T.gold;
				ava.money[2] += gift.luong;
				num += 20;
				break;
			}
			if (!text.Equals(string.Empty))
			{
				Canvas.addFlyTextSmall(text, ava.x, ava.y - 50, -1, 1, num);
			}
		}
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x00038A50 File Offset: 0x00036E50
	public override void update()
	{
		this.lastScr.update();
		if (this.g > 0)
		{
			this.degree -= this.g;
			if (this.degree < 0)
			{
				this.degree = 7200 + this.degree;
			}
			if (this.g < 10)
			{
				if (this.degree / 20 % 30 == 0)
				{
					this.g = 0;
				}
			}
			else
			{
				this.g--;
			}
			if (Canvas.gameTick % 8 == 4)
			{
				int num = CRes.rnd(this.num);
				int num2 = this.degree / 20 + num * this.part;
				if (num2 > 360)
				{
					num2 -= 360;
				}
				int num3 = this.radius * CRes.cos(CRes.fixangle(num2)) >> 10;
				int num4 = -(this.radius * CRes.sin(CRes.fixangle(num2))) >> 10;
				this.addFire(this.posCenter.x + num3, this.posCenter.y + num4);
			}
		}
		else if (this.isTurn)
		{
			this.stop();
		}
		if (this.center == this.cmdWait)
		{
			int num5 = 0;
			for (int i = 0; i < this.isFireWork.Length; i++)
			{
				if (this.isFireWork[i])
				{
					num5++;
				}
			}
			if (num5 == 3)
			{
				this.center = this.cmdClose;
			}
		}
		for (int j = 0; j < this.listFireWork.size(); j++)
		{
			Point point = (Point)this.listFireWork.elementAt(j);
			point.x += point.g;
			if (point.g > 1 || point.g < -1)
			{
				point.g -= point.g / CRes.abs(point.g);
			}
			point.y += point.h;
			point.h++;
			point.color++;
			if (point.color > 20)
			{
				this.listFireWork.removeElement(point);
			}
		}
		if (this.isPaint)
		{
			for (int k = 0; k < this.listGift.size(); k++)
			{
				if (!this.isFireWork[k] && (long)(Environment.TickCount / 100) - this.timePaint > (long)((k + 1) * 5))
				{
					this.isFireWork[k] = true;
					Gift gift = (Gift)this.listGift.elementAt(k);
					this.addFire(gift.x, gift.y);
				}
			}
		}
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x00038D34 File Offset: 0x00037134
	private void addFire(int x, int y)
	{
		for (int i = 0; i < 10; i++)
		{
			int num = 1;
			if (i % 2 == 0)
			{
				num = -1;
			}
			Point point = new Point(x, y);
			point.color = 0;
			point.g = num * (CRes.rnd(80) / 10);
			point.h = -CRes.rnd(70) / 10;
			this.listFireWork.addElement(point);
		}
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x00038DA0 File Offset: 0x000371A0
	private void stop()
	{
		this.isTurn = false;
		this.isPaint = true;
		this.isable = false;
		this.timePaint = (long)(Environment.TickCount / 100);
		for (int i = 0; i < this.listGift.size(); i++)
		{
			Gift gift = (Gift)this.listGift.elementAt(i);
			int angle;
			if (i == 0)
			{
				angle = 150;
			}
			else if (i == 1)
			{
				angle = 180;
			}
			else
			{
				angle = 210;
			}
			int num = this.radius * CRes.cos(CRes.fixangle(angle)) >> 10;
			int num2 = -(this.radius * CRes.sin(CRes.fixangle(angle))) >> 10;
			gift.x = this.posCenter.x + num;
			gift.y = this.posCenter.y + num2;
		}
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x00038E80 File Offset: 0x00037280
	public override void updateKey()
	{
		if (!this.isPaint)
		{
			if (AvMain.indexCenter == 4)
			{
				if (this.degreeKim < 270)
				{
					this.degreeKim += 3;
				}
			}
			else if (this.degreeKim > 90)
			{
				this.degreeKim -= 3;
			}
		}
		base.updateKey();
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x00038EE8 File Offset: 0x000372E8
	private void start()
	{
		if (this.isTurn || !this.isable)
		{
			return;
		}
		this.selectedNumber = this.degreeKim;
		GlobalService.gI().doDialLucky(this.idPart, this.selectedNumber - 90);
		Canvas.startWaitDlg();
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x00038F38 File Offset: 0x00037338
	public void onStart(int idUser, int degree, MyVector listGift1)
	{
		if (idUser != GameMidlet.avatar.IDDB)
		{
			Avatar avatar = LoadMap.getAvatar(idUser);
			if (avatar != null)
			{
				this.setItemBay(listGift1, avatar, 100 + degree + 20);
			}
		}
		else
		{
			this.center = this.cmdWait;
			this.listGift = listGift1;
			this.g = 100 + (this.selectedNumber - 90);
			this.isTurn = true;
			Canvas.endDlg();
		}
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x00038FA8 File Offset: 0x000373A8
	public override void paint(MyGraphics g)
	{
		this.lastScr.paintMain(g);
		Canvas.resetTrans(g);
		int num = this.degree / 20;
		for (int i = 0; i < this.num; i++)
		{
			int num2 = num + i * this.part;
			if (num2 > 360)
			{
				num2 -= 360;
			}
			if (num2 >= 82 && num2 <= 278)
			{
				int num3 = this.radius * CRes.cos(CRes.fixangle(num2)) >> 10;
				int num4 = -(this.radius * CRes.sin(CRes.fixangle(num2))) >> 10;
				g.drawImage(DialLuckyScr.imgCau_back, (float)(this.posCenter.x + num3), (float)(this.posCenter.y + num4), 3);
			}
		}
		if (this.isPaint)
		{
			this.paintGift(g);
		}
		int num5 = 0;
		for (int j = 0; j < this.num; j++)
		{
			int num6 = num + j * this.part;
			if (num6 > 360)
			{
				num6 -= 360;
			}
			if (num6 >= 82 && num6 <= 278)
			{
				int num7 = this.radius * CRes.cos(CRes.fixangle(num6)) >> 10;
				int num8 = -(this.radius * CRes.sin(CRes.fixangle(num6))) >> 10;
				long num9 = (long)(Environment.TickCount / 100) - this.timePaint;
				if (!this.isPaint || num6 < 150 || num6 > 210 || (num9 <= (long)((num5 + 1) * 5) && num9 > (long)((num5 + 1) * 5 - 5)))
				{
					g.drawImage(DialLuckyScr.imgDauHoi, (float)(this.posCenter.x + num7), (float)(this.posCenter.y + num8), 3);
				}
				else
				{
					num5++;
				}
				g.drawImage(DialLuckyScr.imgCau, (float)(this.posCenter.x + num7), (float)(this.posCenter.y + num8), 3);
			}
		}
		g.drawImage(DialLuckyScr.imgDo, (float)(this.posCenter.x - DialLuckyScr.imgDo.w / 2), (float)this.posCenter.y, 3);
		this.paintKim(g);
		if (this.isPaint || this.g > 0)
		{
			this.paintFireWork(g);
		}
		base.paint(g);
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x00039224 File Offset: 0x00037624
	private void paintFireWork(MyGraphics g)
	{
		for (int i = 0; i < this.listFireWork.size(); i++)
		{
			Point point = (Point)this.listFireWork.elementAt(i);
			DialLuckyScr.imgFireWork.drawFrame(point.color / 5, point.x, point.y, 0, 3, g);
		}
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x00039280 File Offset: 0x00037680
	private void paintKim(MyGraphics g)
	{
		int num = this.radius / 2 * CRes.cos(CRes.fixangle(this.degreeKim)) >> 10;
		int num2 = -(this.radius / 2 * CRes.sin(CRes.fixangle(this.degreeKim))) >> 10;
		g.drawImage(DialLuckyScr.imgDot, (float)(this.posCenter.x + num), (float)(this.posCenter.y + num2), 3);
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x000392F0 File Offset: 0x000376F0
	private void paintGift(MyGraphics g)
	{
		for (int i = 0; i < this.listGift.size(); i++)
		{
			if ((long)(Environment.TickCount / 100) - this.timePaint > (long)((i + 1) * 5))
			{
				Gift gift = (Gift)this.listGift.elementAt(i);
				switch (gift.type)
				{
				case 1:
				{
					Part part = AvatarData.getPart(gift.idPart);
					part.paint(g, gift.x, gift.y, 3);
					Canvas.borderFont.drawString(g, gift.expire, gift.x - 17, gift.y - 7, 1);
					break;
				}
				case 2:
					Canvas.borderFont.drawString(g, T.xu, gift.x, gift.y - Canvas.borderFont.getHeight() / 2, 2);
					Canvas.borderFont.drawString(g, gift.xu + string.Empty, gift.x - 17, gift.y - 8, 1);
					break;
				case 3:
					Canvas.borderFont.drawString(g, "xp", gift.x, gift.y - Canvas.borderFont.getHeight() / 2, 2);
					Canvas.borderFont.drawString(g, gift.xp + string.Empty, gift.x - 17, gift.y - 8, 1);
					break;
				case 4:
					Canvas.borderFont.drawString(g, T.gold, gift.x, gift.y - Canvas.borderFont.getHeight() / 2, 2);
					Canvas.borderFont.drawString(g, gift.luong + string.Empty, gift.x - 17, gift.y - 8, 1);
					break;
				}
			}
		}
	}

	// Token: 0x04000830 RID: 2096
	private static DialLuckyScr me;

	// Token: 0x04000831 RID: 2097
	public static Image imgCau;

	// Token: 0x04000832 RID: 2098
	public static Image imgCau_back;

	// Token: 0x04000833 RID: 2099
	public static Image imgDo;

	// Token: 0x04000834 RID: 2100
	public static Image imgDauHoi;

	// Token: 0x04000835 RID: 2101
	public static Image imgDot;

	// Token: 0x04000836 RID: 2102
	public static FrameImage imgFireWork;

	// Token: 0x04000837 RID: 2103
	private int radius;

	// Token: 0x04000838 RID: 2104
	private int degree;

	// Token: 0x04000839 RID: 2105
	private int part;

	// Token: 0x0400083A RID: 2106
	private int g;

	// Token: 0x0400083B RID: 2107
	private int degreeKim;

	// Token: 0x0400083C RID: 2108
	private int num;

	// Token: 0x0400083D RID: 2109
	private int selectedNumber;

	// Token: 0x0400083E RID: 2110
	private AvPosition posCenter;

	// Token: 0x0400083F RID: 2111
	private bool isTurn;

	// Token: 0x04000840 RID: 2112
	private bool isPaint;

	// Token: 0x04000841 RID: 2113
	private bool isable;

	// Token: 0x04000842 RID: 2114
	private MyScreen lastScr;

	// Token: 0x04000843 RID: 2115
	private short idPart;

	// Token: 0x04000844 RID: 2116
	private Command cmdDial;

	// Token: 0x04000845 RID: 2117
	private Command cmdWait;

	// Token: 0x04000846 RID: 2118
	private Command cmdClose;

	// Token: 0x04000847 RID: 2119
	private MyVector listGift = new MyVector();

	// Token: 0x04000848 RID: 2120
	private long timePaint;

	// Token: 0x04000849 RID: 2121
	private bool[] isFireWork;

	// Token: 0x0400084A RID: 2122
	private MyVector listFireWork;
}
