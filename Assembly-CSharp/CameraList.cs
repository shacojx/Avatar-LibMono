using System;

// Token: 0x02000030 RID: 48
public class CameraList
{
	// Token: 0x060001FA RID: 506 RVA: 0x0000F7A0 File Offset: 0x0000DBA0
	public void setInfo(int x, int y, int wOne, int hOne, int w, int h, int limX, int limY, int size)
	{
		this.isQuaTrang = false;
		this.x = x;
		this.y = y + Canvas.transTab;
		this.sizeH = h / hOne;
		this.sizeW = w / wOne;
		this.size = size;
		this.wOne = wOne;
		this.hOne = hOne;
		this.h = h;
		this.w = w;
		this.disY = limY;
		this.disX = limX;
		this.selected = 0;
		CameraList.cmy = (CameraList.cmtoY = 0f);
		this.cmyLim = h - this.disY;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		CameraList.cmx = (CameraList.cmtoX = 0f);
		this.cmxLim = w - this.disX;
		if (this.cmxLim < 0)
		{
			this.cmxLim = 0;
		}
		this.isShow = true;
		this.count = 0L;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0000F88E File Offset: 0x0000DC8E
	public void close()
	{
		this.isShow = false;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000F897 File Offset: 0x0000DC97
	public void setSelect(int se)
	{
		this.selected = se;
		this.setCam();
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000F8A8 File Offset: 0x0000DCA8
	public void updateKey()
	{
		this.count += 1L;
		if (Canvas.menuMain != null || Canvas.currentDialog != null)
		{
			return;
		}
		if (this.timeOpen > 0f)
		{
			this.timeOpen -= 1f;
			if (this.timeOpen == 0f && Canvas.currentMyScreen != PopupShop.me)
			{
				Canvas.currentMyScreen.setSelected(this.selected, true);
			}
			return;
		}
		if (Canvas.isPointerClick && Canvas.isPointer(this.x, this.y, this.w, this.disY))
		{
			this.pyLast = Canvas.pyLast;
			this.pxLast = Canvas.pxLast;
			Canvas.isPointerClick = false;
			this.timeDelay = this.count;
			this.pa = CameraList.cmy;
			this.pb = CameraList.cmx;
			this.xCamLast = CameraList.cmx;
			this.transY = true;
			this.isG = false;
			if (this.vY != 0f || this.vX != 0f)
			{
				this.isG = true;
			}
			this.vY = 0f;
			this.vX = 0f;
		}
		if (this.transY)
		{
			long num = this.count - this.timeDelay;
			int num2 = this.pyLast - Canvas.py;
			this.pyLast = Canvas.py;
			int num3 = this.pxLast - Canvas.px;
			this.pxLast = Canvas.px;
			if (Canvas.isPointerDown)
			{
				if (this.count % 2L == 0L)
				{
					this.dyTran = (float)Canvas.py;
					this.dxTran = (float)Canvas.px;
					this.timePointY = this.count;
					this.timePointX = this.count;
				}
				this.vY = 0f;
				this.vX = 0f;
				if (CameraList.cmtoY <= 0f || CameraList.cmtoY >= (float)this.cmyLim)
				{
					CameraList.cmtoY = this.pa + (float)(num2 / 2);
					this.pa = CameraList.cmtoY;
				}
				else
				{
					CameraList.cmtoY = this.pa + (float)num2;
					this.pa = CameraList.cmtoY;
				}
				if (CameraList.cmtoX <= 0f || CameraList.cmtoX >= (float)this.cmxLim)
				{
					CameraList.cmtoX = this.pb + (float)(num3 / 2);
					this.pb = CameraList.cmtoX;
				}
				else
				{
					CameraList.cmtoX = this.pb + (float)num3;
					this.pb = CameraList.cmtoX;
				}
				CameraList.cmy = CameraList.cmtoY;
				CameraList.cmx = CameraList.cmtoX;
				if (num < 20L)
				{
					int num4 = (int)(CameraList.cmtoY + (float)Canvas.py - (float)this.y) / this.hOne;
					int num5 = (int)((CameraList.cmtoX + (float)Canvas.px - (float)this.x) / (float)this.wOne);
					this.selected = num4 * this.sizeW + num5;
					if (this.selected < 0)
					{
						this.selected = 0;
					}
					if (this.selected >= this.sizeH * this.sizeW)
					{
						this.selected = this.sizeH * this.sizeW - 1;
					}
					this.isSel = true;
					Canvas.currentMyScreen.setSelected(this.selected, false);
				}
				if (CRes.abs(Canvas.dy()) >= 10 * AvMain.hd || CRes.abs(Canvas.dx()) >= 10 * AvMain.hd)
				{
					Canvas.currentMyScreen.setHidePointer(true);
				}
				else if (num > 3L && num < 8L && !this.isG)
				{
					this.isSel = false;
					Canvas.currentMyScreen.setHidePointer(false);
				}
			}
			if (Canvas.isPointerRelease)
			{
				this.isG = false;
				this.transY = false;
				int num6 = (int)(this.count - this.timePointY);
				float num7 = this.dyTran - (float)Canvas.py;
				float num8 = this.dxTran - (float)Canvas.px;
				if (CRes.abs((int)num7) > 40 && num6 < 20 && CameraList.cmtoY > 0f && CameraList.cmtoY < (float)this.cmyLim)
				{
					this.vY = num7 / (float)num6 * 10f;
				}
				int num9 = (int)(this.count - this.timePointX);
				if (CRes.abs((int)num8) > 40 && num9 < 20 && CameraList.cmtoX > 0f && CameraList.cmtoX < (float)this.cmxLim)
				{
					this.vX = num8 / (float)num9 * 10f;
				}
				if (this.isQuaTrang)
				{
					if (Canvas.dx() > 20 * AvMain.hd)
					{
						if (Canvas.dx() > this.disX / 3)
						{
							int num10 = (int)(CameraList.cmx / (float)this.disX) + 1;
							CameraList.cmtoX = (float)(num10 * this.disX);
							this.vX = 0f;
						}
						else
						{
							int num11 = (int)(CameraList.cmx / (float)this.disX);
							CameraList.cmtoX = (float)(num11 * this.disX);
							this.vX = 0f;
						}
					}
					if (Canvas.dx() < -20 * AvMain.hd)
					{
						if (CRes.abs(Canvas.dx()) > this.disX / 3)
						{
							int num12 = (int)(CameraList.cmx / (float)this.disX);
							CameraList.cmtoX = (float)(num12 * this.disX);
							this.vX = 0f;
						}
						else
						{
							int num13 = (int)(CameraList.cmx / (float)this.disX) + 1;
							CameraList.cmtoX = (float)(num13 * this.disX);
							this.vX = 0f;
						}
					}
				}
				this.timePointY = -1L;
				this.timePointX = -1L;
				if (CRes.abs(Canvas.dy()) < 10 * AvMain.hd && CRes.abs(Canvas.dx()) < 10 * AvMain.hd)
				{
					if (num <= 4L)
					{
						this.timeOpen = 5f;
						Canvas.currentMyScreen.setHidePointer(false);
					}
					else
					{
						Canvas.currentMyScreen.setSelected(this.selected, true);
						if (Canvas.currentMyScreen != PopupShop.me)
						{
							Canvas.currentMyScreen.setHidePointer(true);
						}
					}
					this.isSel = false;
					Canvas.paint.clickSound();
				}
				this.transX = false;
			}
		}
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000FF10 File Offset: 0x0000E310
	private void setCam()
	{
		if (!Canvas.isPointerDown)
		{
			CameraList.cmtoY = (float)(this.selected / this.sizeW * this.hOne - this.disY / 2 + this.hOne / 2);
			if (CameraList.cmtoY < 0f)
			{
				CameraList.cmtoY = 0f;
			}
			if (CameraList.cmtoY > (float)this.cmyLim)
			{
				CameraList.cmtoY = (float)this.cmyLim;
			}
			if (this.selected / this.sizeW > this.sizeH - 1 || this.selected / this.sizeW == 0)
			{
				CameraList.cmy = CameraList.cmtoY;
			}
			CameraList.cmtoX = (float)(this.selected % this.sizeW * this.wOne - this.disX / 2 + this.wOne / 2);
			if (CameraList.cmtoX < 0f)
			{
				CameraList.cmtoX = 0f;
			}
			if (CameraList.cmtoX > (float)this.cmxLim)
			{
				CameraList.cmtoX = (float)this.cmxLim;
			}
			if (this.selected % this.sizeW > this.sizeW - 1 || this.selected % this.sizeW == 0)
			{
				CameraList.cmx = CameraList.cmtoX;
			}
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00010058 File Offset: 0x0000E458
	public void moveCamera()
	{
		if (Canvas.menuMain != null || Canvas.currentDialog != null)
		{
			return;
		}
		if (this.vY != 0f)
		{
			if (CameraList.cmy < 0f || CameraList.cmy > (float)this.cmyLim)
			{
				if (this.vY > 500f)
				{
					this.vY = 500f;
				}
				else if (this.vY < -500f)
				{
					this.vY = -500f;
				}
				this.vY -= this.vY / 5f;
				if (CRes.abs((int)(this.vY / 10f)) <= 10)
				{
					this.vY = 0f;
				}
			}
			CameraList.cmy += this.vY / 15f;
			CameraList.cmtoY = CameraList.cmy;
			this.vY -= this.vY / 20f;
		}
		else if (CameraList.cmy < 0f)
		{
			CameraList.cmtoY = 0f;
		}
		else if (CameraList.cmy > (float)this.cmyLim)
		{
			CameraList.cmtoY = (float)this.cmyLim;
		}
		if (this.vX != 0f)
		{
			if (CameraList.cmx < 0f || CameraList.cmx > (float)this.cmxLim)
			{
				if (this.vX > 500f)
				{
					this.vX = 500f;
				}
				else if (this.vX < -500f)
				{
					this.vX = -500f;
				}
				this.vX -= this.vX / 5f;
				if (CRes.abs((int)(this.vX / 10f)) <= 10)
				{
					this.vX = 0f;
				}
			}
			CameraList.cmx += this.vX / 15f;
			CameraList.cmtoX = CameraList.cmx;
			this.vX -= this.vX / 20f;
		}
		else if (CameraList.cmx < 0f)
		{
			CameraList.cmtoX = 0f;
		}
		else if (CameraList.cmx > (float)this.cmxLim)
		{
			CameraList.cmtoX = (float)this.cmxLim;
		}
		if (CameraList.cmy != CameraList.cmtoY)
		{
			this.cmvy = (int)(CameraList.cmtoY - CameraList.cmy) << 2;
			this.cmdy += this.cmvy;
			CameraList.cmy += (float)(this.cmdy >> 4);
			this.cmdy &= 15;
		}
		if (CameraList.cmx != CameraList.cmtoX)
		{
			this.cmvx = (int)(CameraList.cmtoX - CameraList.cmx) << 2;
			this.cmdx += this.cmvx;
			CameraList.cmx += (float)(this.cmdx >> 4);
			this.cmdx &= 15;
		}
	}

	// Token: 0x0400022E RID: 558
	public int cmdy;

	// Token: 0x0400022F RID: 559
	public int cmvy;

	// Token: 0x04000230 RID: 560
	public int cmyLim;

	// Token: 0x04000231 RID: 561
	public int xL;

	// Token: 0x04000232 RID: 562
	public int h;

	// Token: 0x04000233 RID: 563
	public int w;

	// Token: 0x04000234 RID: 564
	public int disY;

	// Token: 0x04000235 RID: 565
	public int disX;

	// Token: 0x04000236 RID: 566
	public int sizeW;

	// Token: 0x04000237 RID: 567
	public int sizeH;

	// Token: 0x04000238 RID: 568
	public int wOne;

	// Token: 0x04000239 RID: 569
	public int hOne;

	// Token: 0x0400023A RID: 570
	public int x;

	// Token: 0x0400023B RID: 571
	public int y;

	// Token: 0x0400023C RID: 572
	public int size;

	// Token: 0x0400023D RID: 573
	public int cmdx;

	// Token: 0x0400023E RID: 574
	public int cmvx;

	// Token: 0x0400023F RID: 575
	public int cmxLim;

	// Token: 0x04000240 RID: 576
	public static float cmy;

	// Token: 0x04000241 RID: 577
	public static float cmtoY;

	// Token: 0x04000242 RID: 578
	public static float cmx;

	// Token: 0x04000243 RID: 579
	public static float cmtoX;

	// Token: 0x04000244 RID: 580
	private int selected;

	// Token: 0x04000245 RID: 581
	public bool isShow;

	// Token: 0x04000246 RID: 582
	public bool isQuaTrang;

	// Token: 0x04000247 RID: 583
	private long timeDelay;

	// Token: 0x04000248 RID: 584
	private long count;

	// Token: 0x04000249 RID: 585
	public float pa;

	// Token: 0x0400024A RID: 586
	public float pb;

	// Token: 0x0400024B RID: 587
	public float vY;

	// Token: 0x0400024C RID: 588
	public float vX;

	// Token: 0x0400024D RID: 589
	public float dyTran;

	// Token: 0x0400024E RID: 590
	public float dxTran;

	// Token: 0x0400024F RID: 591
	public float timeOpen;

	// Token: 0x04000250 RID: 592
	public float xCamLast;

	// Token: 0x04000251 RID: 593
	public bool transY;

	// Token: 0x04000252 RID: 594
	public bool transX;

	// Token: 0x04000253 RID: 595
	public bool isSel;

	// Token: 0x04000254 RID: 596
	public bool isG;

	// Token: 0x04000255 RID: 597
	private long timePointY;

	// Token: 0x04000256 RID: 598
	private long timePointX;

	// Token: 0x04000257 RID: 599
	private int pxLast;

	// Token: 0x04000258 RID: 600
	private int pyLast;
}
