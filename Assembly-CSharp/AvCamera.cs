using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class AvCamera
{
	// Token: 0x06000AB6 RID: 2742 RVA: 0x00069FA8 File Offset: 0x000683A8
	public static AvCamera gI()
	{
		if (AvCamera.instance == null)
		{
			AvCamera.instance = new AvCamera();
		}
		return AvCamera.instance;
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x00069FC3 File Offset: 0x000683C3
	public static void setDistance(int dis)
	{
		AvCamera.distance = dis;
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x00069FCC File Offset: 0x000683CC
	public void init(int index)
	{
		if (this.followPlayer == null)
		{
			return;
		}
		this.hdFarm = 1f;
		AvCamera.isFollow = false;
		this.hdFarm = AvMain.zoom;
		AvCamera.w = (int)((float)(LoadMap.w * AvMain.hd) * this.hdFarm);
		AvCamera.distance = Canvas.w / 20;
		if (AvCamera.distance < 20)
		{
			AvCamera.distance = 20;
		}
		if ((float)(this.followPlayer.x * AvMain.hd) * this.hdFarm > (float)Canvas.hw)
		{
			if ((float)(this.followPlayer.x * AvMain.hd) * this.hdFarm < (float)((int)LoadMap.wMap * AvCamera.w - Canvas.hw - AvCamera.w))
			{
				this.xTo = (float)((int)((float)(this.followPlayer.x * AvMain.hd) * this.hdFarm - (float)Canvas.hw));
			}
			else
			{
				this.xTo = (float)((int)LoadMap.wMap * AvCamera.w - Canvas.w);
				if (this.xTo < 0f)
				{
					this.xTo = 0f;
				}
			}
		}
		else
		{
			this.xTo = 0f;
		}
		int hCan = Canvas.hCan;
		if (Canvas.w > (int)LoadMap.wMap * AvCamera.w)
		{
			this.xTo = (float)(-(float)(Canvas.w - (int)LoadMap.wMap * AvCamera.w) / 2);
		}
		if (hCan > (int)LoadMap.Hmap * AvCamera.w && (index - 1 == 57 || index - 1 == 58 || index - 1 == 59 || index - 1 == 108))
		{
			this.yTo = (float)(-(float)(hCan - (int)LoadMap.Hmap * AvCamera.w) / 2);
		}
		else
		{
			this.yTo = (float)((int)LoadMap.Hmap * AvCamera.w - hCan);
		}
		this.xLimit = (float)((int)((float)((int)LoadMap.wMap * AvCamera.w - Canvas.w) / this.hdFarm));
		this.yLimit = (float)((int)((float)((int)LoadMap.Hmap * AvCamera.w - hCan) / this.hdFarm));
		this.xCam = this.xTo;
		if (this.yTo > this.yLimit)
		{
			this.yTo = this.yLimit;
		}
		this.setPosFollowPlayer();
		this.xCam = this.xTo;
		this.yCam = this.yTo;
		if (this.xCam < 0f)
		{
			this.xCam = 0f;
		}
		if (this.xCam > this.xLimit)
		{
			this.xCam = this.xLimit;
		}
		if (this.yCam > this.yLimit)
		{
			this.yCam = this.yLimit;
		}
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0006A27D File Offset: 0x0006867D
	public void notTrans()
	{
		this.xCam = this.xTo;
		this.yCam = this.yTo;
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0006A298 File Offset: 0x00068698
	public void updateTo()
	{
		float zoom = AvMain.zoom;
		if (!AvCamera.disable)
		{
			if (this.xCam != this.xTo)
			{
				this.cmvx = (float)((int)(this.xTo - this.xCam) << 1);
				this.cmdx += this.cmvx;
				this.xCam += (float)((int)this.cmdx >> 4);
				this.cmdx = (float)((int)this.cmdx & 15);
				if (this.xCam < 0f)
				{
					this.xCam = 0f;
				}
				if (this.xCam > this.xLimit)
				{
					this.xCam = this.xLimit;
				}
			}
		}
		else
		{
			if (this.xCam < 0f)
			{
				this.xCam = 0f;
			}
			if (this.xCam > (float)((int)LoadMap.wMap * LoadMap.w * AvMain.hd) * zoom - (float)Canvas.w)
			{
				this.xCam = (float)((int)((float)((int)LoadMap.wMap * LoadMap.w * AvMain.hd) * zoom - (float)Canvas.w));
			}
		}
		if (this.yCam != this.yTo)
		{
			this.cmvy = (float)((int)(this.yTo - this.yCam) << 1);
			this.cmdy += this.cmvy;
			this.yCam += (float)((int)this.cmdy >> 4);
			this.cmdy = (float)((int)this.cmdy & 15);
			if (this.yCam > this.yLimit)
			{
				this.yCam = this.yLimit;
			}
		}
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0006A43C File Offset: 0x0006883C
	public void setToPos(float x, float y)
	{
		float zoom = AvMain.zoom;
		this.timeDelay = 0L;
		this.xTo = x - (float)Canvas.hw;
		this.yTo = y - (float)(Canvas.hCan / 2);
		if (this.xTo < 0f)
		{
			this.xTo = 0f;
		}
		if (this.xTo > (float)((int)LoadMap.wMap * AvCamera.w - Canvas.w))
		{
			this.xTo = (float)((int)LoadMap.wMap * AvCamera.w - Canvas.w);
		}
		if (this.yTo < 0f)
		{
			this.yTo = 0f;
		}
		int num = Canvas.hCan;
		if (TouchScreenKeyboard.visible)
		{
			num = Canvas.h - MyScreen.hTab - 17 * AvMain.hd;
		}
		if (this.yTo > (float)((int)LoadMap.Hmap * AvCamera.w - num))
		{
			this.yTo = (float)((int)LoadMap.Hmap * AvCamera.w - num);
		}
		this.setLimit();
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x0006A538 File Offset: 0x00068938
	public void setToPos(float x, float y, int ih)
	{
		this.timeDelay = 0L;
		this.xTo = x - (float)Canvas.hw;
		this.yTo = y - (float)(Canvas.hCan / 2);
		if (this.xTo < 0f)
		{
			this.xTo = 0f;
		}
		if (this.xTo > (float)((int)LoadMap.wMap * AvCamera.w - Canvas.w))
		{
			this.xTo = (float)((int)LoadMap.wMap * AvCamera.w - Canvas.w);
		}
		if (this.yTo < 0f)
		{
			this.yTo = 0f;
		}
		int num = ih - MyScreen.hTab - 17 * AvMain.hd;
		if (this.yTo > (float)((int)LoadMap.Hmap * AvCamera.w - num))
		{
			this.yTo = (float)((int)LoadMap.Hmap * AvCamera.w - num);
		}
		this.setLimit();
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x0006A61C File Offset: 0x00068A1C
	public void setPos(int x, int y)
	{
		this.xCam = (this.xTo = (float)x);
		this.yCam = (this.yTo = (float)y);
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x0006A64C File Offset: 0x00068A4C
	public void update()
	{
		if (Canvas.isZoom)
		{
			return;
		}
		if (Canvas.currentMyScreen != RaceScr.me && Canvas.welcome == null)
		{
			AvCamera.isFollow = true;
		}
		if (this.followPlayer != null && MapScr.isWedding)
		{
			AvCamera.isFollow = false;
		}
		if (AvCamera.isMove && CRes.abs((int)this.vX) <= 1 && CRes.abs((int)this.vY) <= 1)
		{
			AvCamera.isMove = false;
			this.timeDelay = (long)(Environment.TickCount / 100);
			this.vX = (this.vY = 0f);
		}
		this.moveCamera();
		if (this.vX != 0f || this.vY != 0f)
		{
			return;
		}
		this.updateTo();
		if (!AvCamera.isFollow)
		{
			int num;
			if ((int)this.followPlayer.direct == (int)Base.RIGHT)
			{
				num = this.followPlayer.x * AvMain.hd + AvCamera.distance;
			}
			else
			{
				num = this.followPlayer.x * AvMain.hd - AvCamera.distance;
			}
			this.xTo = (float)((int)((float)num - (float)Canvas.hw / this.hdFarm));
			int hCan = Canvas.hCan;
			this.yTo = (float)((int)((float)(this.followPlayer.y * AvMain.hd) - ((float)hCan / this.hdFarm - ((float)(hCan / 2) / this.hdFarm - (float)(AvCamera.w / 3)))));
			if ((int)this.followPlayer.direct == (int)Base.LEFT)
			{
				if ((float)this.followPlayer.x < (float)Canvas.hw / ((float)AvMain.hd * this.hdFarm))
				{
					this.xTo = 0f;
				}
			}
			else if (this.xTo > this.xLimit)
			{
				this.xTo = this.xLimit;
			}
		}
		this.setLimit();
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x0006A840 File Offset: 0x00068C40
	public void setPosFollowPlayer()
	{
		int num;
		if ((int)this.followPlayer.direct == (int)Base.RIGHT)
		{
			num = this.followPlayer.x * AvMain.hd + AvCamera.distance;
		}
		else
		{
			num = this.followPlayer.x * AvMain.hd - AvCamera.distance;
		}
		this.xTo = (float)((int)((float)num - (float)Canvas.hw / this.hdFarm));
		int hCan = Canvas.hCan;
		this.yTo = (float)((int)((float)(this.followPlayer.y * AvMain.hd) - ((float)hCan / this.hdFarm - ((float)(hCan / 2) / this.hdFarm - (float)(AvCamera.w / 3)))));
		if ((int)this.followPlayer.direct == (int)Base.LEFT)
		{
			if ((float)this.followPlayer.x < (float)Canvas.hw / ((float)AvMain.hd * this.hdFarm))
			{
				this.xTo = 0f;
			}
		}
		else if (this.xTo > this.xLimit)
		{
			this.xTo = this.xLimit;
		}
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x0006A95C File Offset: 0x00068D5C
	public void setLimit()
	{
		int hCan = Canvas.hCan;
		if (LoadMap.TYPEMAP >= 0 && LoadMap.TYPEMAP < LoadMap.bg.Length && (int)LoadMap.bg[LoadMap.TYPEMAP] == -1 && LoadMap.imgBG == null)
		{
			if (hCan > (int)LoadMap.Hmap * AvCamera.w)
			{
				this.yCam = (this.yTo = (float)(-(float)(hCan - (int)LoadMap.Hmap * AvCamera.w) / 2));
			}
		}
		else if (this.yCam > this.yLimit)
		{
			this.yCam = (this.yTo = this.yLimit);
		}
		if (Canvas.w > (int)LoadMap.wMap * AvCamera.w)
		{
			this.xCam = (this.xTo = (float)(-(float)(Canvas.w - (int)LoadMap.wMap * AvCamera.w) / 2));
		}
		else if (this.xCam < 0f)
		{
			this.xCam = (this.xTo = 0f);
		}
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x0006AA68 File Offset: 0x00068E68
	public void moveCamera()
	{
		if (Canvas.menuMain != null || Canvas.currentDialog != null)
		{
			return;
		}
		if (this.vY != 0f)
		{
			float num = (float)(-(float)Canvas.hCan);
			if (this.yCam + this.vY / 15f < num + 100f)
			{
				if (this.yCam + this.vY / 15f < num)
				{
					this.yCam = (this.yTo = (float)(-(float)Canvas.hCan));
					this.vY /= 6f;
					this.vY *= -1f;
				}
				else
				{
					this.vY -= this.vY / 20f;
				}
			}
			if (this.yCam + this.vY / 15f > this.yLimit - (float)(100 * AvMain.hd))
			{
				if (this.yCam + this.vY / 15f >= this.yLimit)
				{
					this.yCam = (this.yTo = this.yLimit);
					this.vY /= 6f;
					this.vY *= -1f;
				}
				else
				{
					this.vY -= this.vY / 20f;
				}
			}
			this.yCam += this.vY / 15f;
			this.yTo = this.yCam;
			this.vY -= this.vY / 20f;
		}
		if (this.vX != 0f)
		{
			if (this.xCam + this.vX / 15f < (float)(100 * AvMain.hd))
			{
				if (this.xCam + this.vX / 15f < 0f)
				{
					this.xCam = (this.xTo = 0f);
					this.vX /= 6f;
					this.vX *= -1f;
				}
				else
				{
					this.vX -= this.vX / 20f;
				}
			}
			if (this.xCam + this.vX / 15f > this.xLimit - (float)(100 * AvMain.hd))
			{
				if (this.xCam + this.vX / 15f >= this.xLimit)
				{
					this.xCam = (this.xTo = this.xLimit);
					this.vX /= 6f;
					this.vX *= -1f;
				}
				else
				{
					this.vX -= this.vX / 20f;
				}
			}
			this.xCam += this.vX / 15f;
			this.xTo = this.xCam;
			this.vX -= this.vX / 20f;
		}
	}

	// Token: 0x04000E15 RID: 3605
	public static AvCamera instance;

	// Token: 0x04000E16 RID: 3606
	public float xCam;

	// Token: 0x04000E17 RID: 3607
	public float yCam;

	// Token: 0x04000E18 RID: 3608
	public float xTo;

	// Token: 0x04000E19 RID: 3609
	public float yTo;

	// Token: 0x04000E1A RID: 3610
	public float xLimit;

	// Token: 0x04000E1B RID: 3611
	public float yLimit;

	// Token: 0x04000E1C RID: 3612
	public long timeDelay;

	// Token: 0x04000E1D RID: 3613
	private float cmvx;

	// Token: 0x04000E1E RID: 3614
	private float cmdx;

	// Token: 0x04000E1F RID: 3615
	private float cmvy;

	// Token: 0x04000E20 RID: 3616
	private float cmdy;

	// Token: 0x04000E21 RID: 3617
	public static int distance;

	// Token: 0x04000E22 RID: 3618
	public static int w;

	// Token: 0x04000E23 RID: 3619
	public static bool disable;

	// Token: 0x04000E24 RID: 3620
	public static bool isFollow;

	// Token: 0x04000E25 RID: 3621
	public static bool isMove;

	// Token: 0x04000E26 RID: 3622
	public Base followPlayer;

	// Token: 0x04000E27 RID: 3623
	public float hdFarm = 1f;

	// Token: 0x04000E28 RID: 3624
	public float vY;

	// Token: 0x04000E29 RID: 3625
	public float vX;
}
