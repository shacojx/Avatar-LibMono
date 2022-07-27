using System;

// Token: 0x02000027 RID: 39
public class AnimateEffect : Effect
{
	// Token: 0x06000185 RID: 389 RVA: 0x0000BBAC File Offset: 0x00009FAC
	public AnimateEffect(sbyte type, bool isStart, int num)
	{
		this.type = type;
		this.number = num * 10;
		if (AvMain.hd == 1)
		{
			this.number = num * 5;
		}
		this.timeCur = (int)(Canvas.getTick() / 1000L);
		switch (type)
		{
		case 0:
			this.number = Canvas.w * Canvas.h / 1000 + 50;
			break;
		case 1:
			this.number = 30;
			if (AnimateEffect.img == null)
			{
				AnimateEffect.img = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/cobay"), 16 * AvMain.hd, 10 * AvMain.hd);
			}
			break;
		case 3:
			this.number = Canvas.w * Canvas.h / 1000;
			AnimateEffect.img = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/tuyet"), 5 * AvMain.hd, 5 * AvMain.hd);
			AnimateEffect.imgTemp = AnimateEffect.img;
			break;
		}
		for (int i = 0; i < this.number; i++)
		{
			Point point = new Point((int)((AvCamera.gI().xCam - (float)Canvas.hw + (float)CRes.rnd(Canvas.w * 2)) * 10f), (int)((AvCamera.gI().yCam - (float)(Canvas.h * 2) + (float)CRes.rnd(Canvas.h * 2)) * 10f));
			point.x = (-Canvas.w / 2 + CRes.rnd((int)LoadMap.wMap * LoadMap.w + Canvas.w)) * 10;
			if ((int)type == 3 || (int)this.type == 2)
			{
				point.h = CRes.rnd(3);
			}
			else
			{
				point.h = CRes.rnd(4);
			}
			point.limitY = 16 + CRes.rnd(3) * 4;
			point.v = CRes.rnd(-1, 1);
			point.color = CRes.rnd(point.limitY);
			point.dis = (sbyte)CRes.rnd(20);
			this.list.addElement(point);
		}
		if ((int)type == 2)
		{
			for (int j = 0; j < this.list.size() - 1; j++)
			{
				Point point2 = (Point)this.list.elementAt(j);
				for (int k = j + 1; k < this.list.size(); k++)
				{
					Point point3 = (Point)this.list.elementAt(k);
					if (point2.h > point3.h)
					{
						this.list.setElementAt(point2, k);
						this.list.setElementAt(point3, j);
						point2 = point3;
					}
				}
			}
		}
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0000BE8A File Offset: 0x0000A28A
	public override void close()
	{
		base.close();
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0000BE92 File Offset: 0x0000A292
	public void stop()
	{
		this.isStop = true;
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000BE9C File Offset: 0x0000A29C
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		g.translate(-AvCamera.gI().xCam, -AvCamera.gI().yCam);
		switch (this.type)
		{
		case 0:
			this.paintRain(g);
			break;
		case 1:
			this.paintFallingLeaves(g);
			break;
		case 2:
		{
			if (this.IDAction == -1)
			{
				return;
			}
			EffectData effect = AvatarData.getEffect(this.IDAction);
			for (int i = 0; i < this.number; i++)
			{
				Point point = (Point)this.list.elementAt(i);
				point.countFr++;
				if ((float)(point.x * AvMain.hd / 10) > AvCamera.gI().xCam && (float)(point.x * AvMain.hd / 10) < AvCamera.gI().xCam + (float)Canvas.w && (float)(point.y * AvMain.hd / 10) > AvCamera.gI().yCam && (float)(point.y * AvMain.hd / 10) < AvCamera.gI().yCam + (float)Canvas.h)
				{
					if (effect != null)
					{
						if (point.countFr >= effect.arrFrame.Length)
						{
							point.countFr = 0;
						}
						effect.paint(g, point.x / 10, point.y / 10, point.countFr);
					}
					Point point2 = point;
					point2.dis = (sbyte)((int)point2.dis + 1);
					if ((int)point.dis >= 20)
					{
						point.dis = 0;
					}
				}
			}
			break;
		}
		case 3:
			for (int j = 0; j < this.number; j++)
			{
				Point point3 = (Point)this.list.elementAt(j);
				if ((float)(point3.x * AvMain.hd / 10) > AvCamera.gI().xCam && (float)(point3.x * AvMain.hd / 10) < AvCamera.gI().xCam + (float)Canvas.w && (float)(point3.y * AvMain.hd / 10) > AvCamera.gI().yCam && (float)(point3.y * AvMain.hd / 10) < AvCamera.gI().yCam + (float)Canvas.h)
				{
					AnimateEffect.imgTemp.drawFrame(2 - point3.h, point3.x * AvMain.hd / 10, point3.y * AvMain.hd / 10, 0, g);
				}
			}
			break;
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0000C14F File Offset: 0x0000A54F
	public override void paintBack(MyGraphics g)
	{
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0000C154 File Offset: 0x0000A554
	private void paintFallingLeaves(MyGraphics g)
	{
		for (int i = 0; i < this.number; i++)
		{
			Point point = (Point)this.list.elementAt(i);
			if ((float)(point.x * AvMain.hd / 10) > AvCamera.gI().xCam && (float)(point.x * AvMain.hd / 10) < AvCamera.gI().xCam + (float)Canvas.w && (float)(point.y * AvMain.hd / 10) > AvCamera.gI().yCam && (float)(point.y * AvMain.hd / 10) < AvCamera.gI().yCam + (float)Canvas.h)
			{
				AnimateEffect.img.drawFrame(point.color / (point.limitY / 4), point.x * AvMain.hd / 10, point.y * AvMain.hd / 10, 0, 3, g);
			}
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0000C250 File Offset: 0x0000A650
	private void paintRain(MyGraphics g)
	{
		g.setColor(14540253);
		for (int i = 0; i < this.number; i++)
		{
			Point point = (Point)this.list.elementAt(i);
			if ((float)(point.x * AvMain.hd / 10) > AvCamera.gI().xCam && (float)(point.x * AvMain.hd / 10) < AvCamera.gI().xCam + (float)Canvas.w && (float)(point.y * AvMain.hd / 10) > AvCamera.gI().yCam && (float)(point.y * AvMain.hd / 10) < AvCamera.gI().yCam + (float)Canvas.h)
			{
				g.fillRect((float)(point.x * AvMain.hd / 10), (float)(point.y * AvMain.hd / 10), 1f, (float)(point.h + 1));
			}
		}
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000C350 File Offset: 0x0000A750
	public override void update()
	{
		AnimateEffect.updateWind();
		switch (this.type)
		{
		case 0:
			this.updateRain();
			break;
		case 1:
			this.updateFallingLeaves();
			break;
		case 2:
			this.updateFlower();
			break;
		case 3:
			this.updateSnow();
			break;
		}
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000C3B0 File Offset: 0x0000A7B0
	public static void updateWind()
	{
		int num = 1;
		if (Canvas.gameTick % 6 == 3)
		{
			num = CRes.rnd(15);
		}
		if (num == 0 && AnimateEffect.wind == 5)
		{
			AnimateEffect.wind = 5 + CRes.rnd(20);
			AnimateEffect.countWind = 50 + CRes.rnd(100);
		}
		if (AnimateEffect.countWind > 0)
		{
			AnimateEffect.countWind--;
		}
		if (AnimateEffect.countWind == 0 && AnimateEffect.wind > 5 && Canvas.gameTick % 4 == 2)
		{
			AnimateEffect.wind--;
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000C448 File Offset: 0x0000A848
	private void updateRain()
	{
		for (int i = 0; i < this.number; i++)
		{
			Point point = (Point)this.list.elementAt(i);
			point.y += (point.h + 1) * 15 + (3 - point.h) * 3;
			point.g++;
			point.x += (point.h + 1) * 4;
			if ((float)(point.y / 10) > AvCamera.gI().yCam + (float)Canvas.h - (float)((4 - point.h) * 50))
			{
				this.rndPos(point);
			}
			this.setLimitX(point);
		}
	}

	// Token: 0x0600018F RID: 399 RVA: 0x0000C504 File Offset: 0x0000A904
	private void setLimitX(Point pos)
	{
		int num = (int)(AvCamera.gI().xCam * (float)((2 - pos.h) * 20) / 120f);
		if ((float)(pos.x / 10 + num) < AvCamera.gI().xCam - 10f)
		{
			pos.x += (Canvas.w + 20) * 10;
		}
		if ((float)(pos.x / 10 + num) > AvCamera.gI().xCam + (float)Canvas.w + 10f)
		{
			pos.x -= (Canvas.w + 20) * 10;
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000C5AC File Offset: 0x0000A9AC
	private void updateFlower()
	{
		if (Canvas.getTick() / 1000L - (long)this.timeCur > (long)this.timeStop)
		{
			this.timeStop++;
			for (int i = 0; i < 5; i++)
			{
				this.list.removeElementAt(0);
				this.number = this.list.size();
				if (this.number == 0)
				{
					this.close();
					return;
				}
			}
		}
		for (int j = 0; j < this.number; j++)
		{
			Point point = (Point)this.list.elementAt(j);
			point.y += (point.h + 2) * 5;
			point.x += (point.h + 1) * 2 + AnimateEffect.wind * AnimateEffect.dirWind;
			if (point.y / 10 > (int)LoadMap.Hmap * LoadMap.w - (4 - point.h) * 20)
			{
				this.rndPos(point);
			}
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000C6B8 File Offset: 0x0000AAB8
	private void updateSnow()
	{
		for (int i = 0; i < this.number; i++)
		{
			Point point = (Point)this.list.elementAt(i);
			point.y += (point.h + 4) * 3;
			point.x += (point.h + 1) * 2 + AnimateEffect.wind * AnimateEffect.dirWind;
			if (point.y / 10 > (int)LoadMap.Hmap * LoadMap.w - (4 - point.h) * 20)
			{
				this.rndPos(point);
			}
		}
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000C754 File Offset: 0x0000AB54
	private void updateFallingLeaves()
	{
		for (int i = 0; i < this.number; i++)
		{
			Point point = (Point)this.list.elementAt(i);
			point.y += 10;
			point.x += point.v * 10 + AnimateEffect.wind * AnimateEffect.dirWind;
			point.color++;
			if (point.color >= point.limitY)
			{
				point.color = 0;
			}
			if (point.y / 10 > (int)LoadMap.Hmap * LoadMap.w - (4 - point.h) * 20)
			{
				this.rndPos(point);
			}
		}
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000C810 File Offset: 0x0000AC10
	private void setLeaveFall(int x0, int y0)
	{
		Point point = new Point(x0, y0);
		point.limitY = 200;
		point.g = CRes.rnd(4);
		point.layer = new AnimateEffect.Layer1(this, point, x0, y0);
		LoadMap.dynamicLists.addElement(point);
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000C858 File Offset: 0x0000AC58
	private void rndPos(Point pos)
	{
		if (this.isStop)
		{
			this.list.removeElement(pos);
			this.number = this.list.size();
			if (this.list.size() == 0)
			{
				this.close();
			}
		}
		else
		{
			pos.y = (int)(AvCamera.gI().yCam - (float)Canvas.hh + (float)CRes.rnd(Canvas.h * 2)) * 10;
			pos.x = (-Canvas.w / 2 + CRes.rnd((int)LoadMap.wMap * LoadMap.w + Canvas.w)) * 10;
		}
	}

	// Token: 0x0400017C RID: 380
	public static FrameImage img;

	// Token: 0x0400017D RID: 381
	public static FrameImage imgFlower;

	// Token: 0x0400017E RID: 382
	public static FrameImage imgSnow;

	// Token: 0x0400017F RID: 383
	public static FrameImage imgTemp;

	// Token: 0x04000180 RID: 384
	private const sbyte RAIN = 0;

	// Token: 0x04000181 RID: 385
	private const sbyte FALLING_LEAVES = 1;

	// Token: 0x04000182 RID: 386
	private const sbyte FALLING_FLOWER = 2;

	// Token: 0x04000183 RID: 387
	private const sbyte SNOW = 3;

	// Token: 0x04000184 RID: 388
	private sbyte type;

	// Token: 0x04000185 RID: 389
	public int number;

	// Token: 0x04000186 RID: 390
	public int timeStop;

	// Token: 0x04000187 RID: 391
	public int timeCur;

	// Token: 0x04000188 RID: 392
	private static int wind = 5;

	// Token: 0x04000189 RID: 393
	private static int countWind;

	// Token: 0x0400018A RID: 394
	private static int dirWind = CRes.rnd(1, -1);

	// Token: 0x0400018B RID: 395
	private MyVector list = new MyVector();

	// Token: 0x02000028 RID: 40
	private class Layer1 : Layer
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000C919 File Offset: 0x0000AD19
		public Layer1(AnimateEffect parent, Point po, int x0, int y0)
		{
			this.parent = parent;
			this.po = po;
			this.x0 = x0;
			this.y0 = y0;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000C93E File Offset: 0x0000AD3E
		public override void paint(MyGraphics g, int x, int y)
		{
			AnimateEffect.img.drawFrame(this.po.g, this.x0, this.y0, 0, 3, g);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C964 File Offset: 0x0000AD64
		public override void update()
		{
			this.po.limitY--;
			if (this.po.limitY <= 0)
			{
				LoadMap.dynamicLists.removeElement(this.po);
			}
		}

		// Token: 0x0400018C RID: 396
		private AnimateEffect parent;

		// Token: 0x0400018D RID: 397
		private readonly Point po;

		// Token: 0x0400018E RID: 398
		private readonly int x0;

		// Token: 0x0400018F RID: 399
		private readonly int y0;
	}
}
