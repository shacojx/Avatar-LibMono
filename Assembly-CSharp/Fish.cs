using System;

// Token: 0x0200010A RID: 266
public class Fish
{
	// Token: 0x06000742 RID: 1858 RVA: 0x000425C8 File Offset: 0x000409C8
	public Fish()
	{
		this.size = (sbyte)(7 + CRes.rnd(4));
		this.waves = new AvPosition[2];
		for (int i = 0; i < 2; i++)
		{
			this.waves[i] = new AvPosition(-10, 0, i * 15);
		}
		this.posGoc = new AvPosition[2];
		this.posGoc[0] = new AvPosition();
		this.posGoc[1] = new AvPosition();
		this.posDay = new AvPosition[(int)this.size];
		this.posTemp = new AvPosition[(int)this.size];
		for (int j = 0; j < (int)this.size; j++)
		{
			this.posDay[j] = new AvPosition();
			this.posTemp[j] = new AvPosition();
		}
		this.posMoi = new AvPosition(0, 0, -1);
		this.posMoitran = new AvPosition(0, 0, -1);
		this.posMoiTemp = new AvPosition();
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00042700 File Offset: 0x00040B00
	public void doSetDayCau()
	{
		this.indexQuan = 0;
		this.isQuan = 0;
		this.g = -(10 + CRes.rnd(4));
		this.countQuan = -1;
		this.isCanCau = false;
		this.isSuccess = false;
		this.yTemp = -1;
		this.isLac = false;
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00042750 File Offset: 0x00040B50
	public void doQuanCau(Avatar ava)
	{
		this.ava = ava;
		if ((int)ava.direct == (int)Base.RIGHT)
		{
			this.direct = 1;
		}
		else
		{
			this.direct = -1;
		}
		this.doSetDayCau();
		this.doSetPos(ava);
		if (ava.IDDB == GameMidlet.avatar.IDDB)
		{
			MapScr.doAction(13);
		}
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x000427B4 File Offset: 0x00040BB4
	public void doSetPos(Avatar ava)
	{
		this.countQuan = 0;
		this.idFish = 0;
		Part part = AvatarData.getPartByZ(ava.seriPart, 70);
		if (part.follow >= 0)
		{
			part = AvatarData.getPart(part.follow);
		}
		APartInfo apartInfo = (APartInfo)part;
		ImageInfo imageInfo = AvatarData.listImgInfo[(int)apartInfo.imgID[3]];
		ImageInfo imageInfo2 = AvatarData.listImgInfo[(int)apartInfo.imgID[14]];
		int x = ava.x;
		int num = ava.y + (int)ava.ySat;
		this.posGoc[0].x = x + (int)apartInfo.dx[3] * AvMain.hd + (int)imageInfo.w * AvMain.hd;
		this.posGoc[0].y = num + (int)apartInfo.dy[3] * AvMain.hd - 5 * (AvMain.hd - 1);
		this.posGoc[1].x = x + (int)apartInfo.dx[14] * AvMain.hd + (int)imageInfo2.w * AvMain.hd;
		this.posGoc[1].y = num + (int)apartInfo.dy[14] * AvMain.hd - 5 * (AvMain.hd - 1);
		this.posMoi.anchor = -1;
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x000428EC File Offset: 0x00040CEC
	public void doQuanDay()
	{
		this.indexQuan++;
		this.distant = Fish.dis;
		for (int i = 0; i < (int)this.size; i++)
		{
			this.posDay[i].x = this.posGoc[1].x;
			this.posDay[i].y = this.posGoc[1].y;
		}
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00042960 File Offset: 0x00040D60
	public void setPosDay(int index)
	{
		this.posDay[0].x = this.posGoc[index].x;
		this.posDay[0].y = this.posGoc[index].y;
		if (index == 1)
		{
			this.ava.action = 13;
		}
		else
		{
			this.ava.action = 2;
		}
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x000429C8 File Offset: 0x00040DC8
	public void update()
	{
		if (this.ava == null)
		{
			return;
		}
		this.count++;
		if (this.count >= 6)
		{
			this.count = 0;
		}
		this.updateDayCau();
		this.updateQuanCau();
		this.updateCanCau();
		if (this.isWait)
		{
			this.updateWait();
		}
		if (this.isQuan != 0)
		{
			this.updateWave();
		}
		if (!this.isSuccess)
		{
			this.updatePosMoi();
		}
		if ((int)this.ava.direct == (int)Base.RIGHT)
		{
			this.direct = 1;
		}
		else
		{
			this.direct = -1;
		}
		for (int i = 0; i < (int)this.size; i++)
		{
			int num = this.posDay[i].x - this.ava.x;
			if (i != (int)this.size - 2 || CRes.abs(this.posTemp[i].x - (this.ava.x + this.direct * num)) > 1)
			{
				this.posTemp[i].x = this.ava.x * AvMain.hd + this.direct * num;
			}
			this.posTemp[i].y = this.posDay[i].y;
		}
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00042B20 File Offset: 0x00040F20
	private void updatePosMoi()
	{
		if (this.isQuan == 1)
		{
			if (this.posMoi.anchor == -1)
			{
				this.posMoi.x = (this.posMoitran.x = (this.posMoiTemp.x = this.posDay[(int)this.size - 1].x));
				this.posMoi.y = (this.posMoitran.y = (this.posMoiTemp.y = this.posDay[(int)this.size - 1].y));
				this.posMoi.anchor = 0;
				this.iRnd = -1;
			}
			int num = this.posMoiTemp.x - this.posMoitran.x;
			int num2 = this.posMoiTemp.y - this.posMoitran.y;
			if (this.iRnd > 0)
			{
				this.iRnd--;
			}
			if ((this.iRnd <= 0 || this.isCanCau) && Canvas.gameTick % 2 == 1)
			{
				if (CRes.abs(num) > 0)
				{
					if (num > 0)
					{
						this.posMoiTemp.x--;
					}
					else
					{
						this.posMoiTemp.x++;
					}
					this.posDay[(int)this.size - 1].x = this.posMoiTemp.x;
				}
				if (CRes.abs(num2) > 0)
				{
					if (num2 > 0)
					{
						this.posMoiTemp.y--;
					}
					else
					{
						this.posMoiTemp.y++;
					}
					this.posDay[(int)this.size - 1].y = this.posMoiTemp.y;
				}
			}
			if (CRes.abs(num) <= 0 && CRes.abs(num2) <= 0)
			{
				this.iRnd = 50 + CRes.rnd(100);
				this.posMoitran.x = this.posMoi.x + 10 - CRes.rnd(20);
				this.posMoitran.y = this.posMoi.y + CRes.rnd(6);
			}
		}
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00042D68 File Offset: 0x00041168
	private void updateWave()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.waves[i].anchor == 0 || this.waves[i].x == -10)
			{
				this.waves[i].x = this.posTemp[(int)this.size - 2].x;
				this.waves[i].y = this.posTemp[(int)this.size - 2].y;
			}
			if (this.isCanCau)
			{
				this.waves[i].anchor += 2;
			}
			else
			{
				this.waves[i].anchor++;
			}
			if (this.waves[i].anchor > this.radius + ((!this.isCanCau) ? 0 : 10))
			{
				this.waves[i].anchor = 0;
			}
		}
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00042E65 File Offset: 0x00041265
	private void updateWait()
	{
		if (this.isAble())
		{
			this.doQuanCau(this.ava);
			this.isWait = false;
		}
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x00042E88 File Offset: 0x00041288
	private void updateCanCau()
	{
		if (!this.isCanCau)
		{
			return;
		}
		if (this.distant > 4 && Canvas.gameTick % 6 == 3)
		{
			this.distant--;
		}
		if (!this.isSuccess && Canvas.gameTick % 6 == 3 && this.ava != GameMidlet.avatar)
		{
			if ((int)this.ava.action == 2)
			{
				this.setPosDay(1);
			}
			else
			{
				this.setPosDay(0);
			}
		}
		if (this.isSuccess && this.distant <= 4)
		{
			this.distant = 2;
			int num = 0;
			if (!this.isLac)
			{
				for (int i = 0; i < (int)this.size - 1; i++)
				{
					if (!this.posDay[i].setDetectX(this.posDay[i + 1].x, 1))
					{
						num++;
					}
				}
			}
			if (num == 0 && !this.isLac)
			{
				this.posMoi.anchor = -2;
				this.isLac = true;
			}
		}
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x00042FA8 File Offset: 0x000413A8
	private void updateQuanCau()
	{
		if (this.countQuan != -1)
		{
			this.countQuan++;
			if (Canvas.gameTick % 4 == 2)
			{
				if ((int)this.ava.action == 2)
				{
					this.ava.action = 13;
					if (this.countQuan > 16)
					{
						this.doQuanDay();
						this.countQuan = -1;
					}
				}
				else
				{
					this.ava.action = 2;
				}
			}
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x00043028 File Offset: 0x00041428
	private void setLacMoi()
	{
		if (this.isLac && this.idFish > 0)
		{
			this.aa++;
			if (this.aa < 2)
			{
				for (int i = 1; i < (int)this.size; i++)
				{
					this.posDay[i].x -= 6;
				}
			}
			else if (this.aa > 4 && this.aa < 8)
			{
				for (int j = 1; j < (int)this.size; j++)
				{
					this.posDay[j].x += 6;
				}
			}
			else if (this.aa > 14)
			{
				this.rLac--;
				if (this.rLac < 0)
				{
					this.aa = 0;
					this.rLac = CRes.rnd(20);
				}
			}
		}
		else
		{
			this.rLac = CRes.rnd(20);
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00043130 File Offset: 0x00041530
	private void updateDayCau()
	{
		if (this.indexQuan == 0)
		{
			return;
		}
		if (this.isQuan == 1)
		{
			for (int i = 1; i < (int)this.size - 2; i++)
			{
				this.posDay[i].y += 6;
			}
			this.setLacMoi();
			if (this.distant == Fish.dis)
			{
				this.distant = 7;
			}
		}
		bool flag = false;
		int num = (int)this.size - 1;
		int num2 = 1;
		if (this.isSuccess)
		{
			num2 = 0;
		}
		for (int j = 1; j < (int)this.size - this.isQuan * num2; j++)
		{
			int num3 = CRes.distance(this.posDay[j].x, this.posDay[j].y, this.posDay[j - 1].x, this.posDay[j - 1].y);
			if (num3 > this.distant + 1)
			{
				flag = true;
				int num4 = num3 - this.distant;
				int angle = CRes.angle(this.posDay[j - 1].x - this.posDay[j].x, -(this.posDay[j - 1].y - Fish.force - this.posDay[j].y));
				int num5 = num4 * CRes.cos(CRes.fixangle(angle)) >> 10;
				int num6 = -(num4 * CRes.sin(CRes.fixangle(angle))) >> 10;
				this.posDay[j].x += num5;
				this.posDay[j].y += num6;
			}
		}
		if (this.posDay[num].y < this.ava.y + (int)this.ava.ySat + 5)
		{
			this.posDay[num].x += Fish.v;
			this.posDay[num].y += this.g;
			this.g++;
		}
		if (!this.isSuccess)
		{
			for (int k = num - 1; k > 0; k--)
			{
				int num7 = CRes.distance(this.posDay[k].x, this.posDay[k].y, this.posDay[k + 1].x, this.posDay[k + 1].y);
				if (num7 > this.distant + 1)
				{
					flag = true;
					int angle2 = CRes.angle(this.posDay[k + 1].x - this.posDay[k].x, -(this.posDay[k + 1].y - this.posDay[k].y));
					int num8 = num7 - this.distant;
					int num9 = num8 * CRes.cos(CRes.fixangle(angle2)) >> 10;
					int num10 = -(num8 * CRes.sin(CRes.fixangle(angle2))) >> 10;
					this.posDay[k].x += num9;
					this.posDay[k].y += num10;
				}
			}
		}
		if (!flag)
		{
			this.isQuan = 1;
		}
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00043480 File Offset: 0x00041880
	public void paint(MyGraphics g)
	{
		if (this.isWait || this.countQuan != -1)
		{
			return;
		}
		if (AvMain.hd > 1)
		{
			g.translate(0f, (float)this.ava.y);
		}
		if (this.isQuan != 0 && !this.isSuccess && (float)this.waves[0].x > AvCamera.gI().xCam && (float)this.waves[0].x < AvCamera.gI().xCam + (float)Canvas.w)
		{
			g.setColor(Fish.color[(int)LoadMap.status]);
		}
		g.setColor(8685448);
		if (((float)this.posTemp[0].x > AvCamera.gI().xCam && (float)this.posTemp[0].x < AvCamera.gI().xCam + (float)Canvas.w) || ((float)this.posTemp[(int)this.size - 1].x > AvCamera.gI().xCam && (float)this.posTemp[(int)this.size - 1].x < AvCamera.gI().xCam + (float)Canvas.w))
		{
			for (int i = 0; i < (int)this.size - 1 - this.isQuan; i++)
			{
				if (this.posTemp[i + 1].y < this.ava.y + (int)this.ava.ySat + 20)
				{
					g.drawLine((float)this.posTemp[i].x, (float)this.posTemp[i].y, (float)this.posTemp[i + 1].x, (float)this.posTemp[i + 1].y);
				}
			}
			if (this.isQuan == 0 && this.posTemp[(int)this.size - 1].y < this.ava.y + (int)this.ava.ySat + 10)
			{
				PaintPopup.fill(this.posTemp[(int)this.size - 1].x, this.posTemp[(int)this.size - 1].y, 2, 2, 0, g);
			}
			g.drawImage(FishingScr.imgPhao, (float)this.posTemp[(int)this.size - 2].x, (float)this.posTemp[(int)this.size - 2].y, 3);
			if (this.isSuccess && this.idFish > 0)
			{
				FishingScr.imgCa.drawFrame(this.count / 3, this.posTemp[(int)this.size - 2].x + 2, this.posTemp[(int)this.size - 2].y + 4, 0, MyGraphics.RIGHT | MyGraphics.TOP, g);
				if (Canvas.gameTick % 10 > 5)
				{
					PartSmall partSmall = (PartSmall)AvatarData.getPart((short)this.idFish);
					if (partSmall != null)
					{
						partSmall.paint(g, this.ava.x * AvMain.hd, this.ava.y - 55 * AvMain.hd, 3);
					}
				}
			}
		}
		if (AvMain.hd > 1)
		{
			g.translate(0f, (float)(-(float)this.ava.y));
		}
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x000437E4 File Offset: 0x00041BE4
	public bool isAble()
	{
		return (int)this.ava.action == 2 || (int)this.ava.action == 13;
	}

	// Token: 0x0400092E RID: 2350
	public static int CAN = 322;

	// Token: 0x0400092F RID: 2351
	public static int CUOC = 0;

	// Token: 0x04000930 RID: 2352
	public static int LUOI = 1;

	// Token: 0x04000931 RID: 2353
	public static int MOI = 2;

	// Token: 0x04000932 RID: 2354
	public static int dis = 10;

	// Token: 0x04000933 RID: 2355
	public int direct = 1;

	// Token: 0x04000934 RID: 2356
	public sbyte size = 9;

	// Token: 0x04000935 RID: 2357
	public Avatar ava;

	// Token: 0x04000936 RID: 2358
	public AvPosition[] posDay;

	// Token: 0x04000937 RID: 2359
	public AvPosition[] posTemp;

	// Token: 0x04000938 RID: 2360
	private AvPosition[] posGoc;

	// Token: 0x04000939 RID: 2361
	private int indexQuan;

	// Token: 0x0400093A RID: 2362
	public int distant = Fish.dis;

	// Token: 0x0400093B RID: 2363
	private static int force = 0;

	// Token: 0x0400093C RID: 2364
	private static int v = 10;

	// Token: 0x0400093D RID: 2365
	public int g = -8;

	// Token: 0x0400093E RID: 2366
	public int isQuan;

	// Token: 0x0400093F RID: 2367
	public int countQuan = -1;

	// Token: 0x04000940 RID: 2368
	private int radius = 25;

	// Token: 0x04000941 RID: 2369
	private int rnd;

	// Token: 0x04000942 RID: 2370
	public int idFish = -1;

	// Token: 0x04000943 RID: 2371
	private int yTemp = -1;

	// Token: 0x04000944 RID: 2372
	private int iRnd;

	// Token: 0x04000945 RID: 2373
	public bool isCanCau;

	// Token: 0x04000946 RID: 2374
	public bool isSuccess;

	// Token: 0x04000947 RID: 2375
	public bool isWait;

	// Token: 0x04000948 RID: 2376
	private bool isLac;

	// Token: 0x04000949 RID: 2377
	private AvPosition[] waves;

	// Token: 0x0400094A RID: 2378
	private AvPosition posMoi;

	// Token: 0x0400094B RID: 2379
	private AvPosition posMoitran;

	// Token: 0x0400094C RID: 2380
	private AvPosition posMoiTemp;

	// Token: 0x0400094D RID: 2381
	public static int[] color = new int[]
	{
		12577266,
		10341591
	};

	// Token: 0x0400094E RID: 2382
	private int aa;

	// Token: 0x0400094F RID: 2383
	private int rLac;

	// Token: 0x04000950 RID: 2384
	private int count;
}
