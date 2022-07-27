using System;
using System.IO;

// Token: 0x020001B1 RID: 433
public class OptionScr : MyScreen
{
	// Token: 0x06000B9B RID: 2971 RVA: 0x000754DD File Offset: 0x000738DD
	public OptionScr()
	{
		this.isPaint = new bool[this.max];
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x00075513 File Offset: 0x00073913
	public static OptionScr gI()
	{
		if (OptionScr.instance == null)
		{
			OptionScr.instance = new OptionScr();
		}
		return OptionScr.instance;
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x00075530 File Offset: 0x00073930
	public override void switchToMe()
	{
		this.initSize();
		this.lastScr = Canvas.currentMyScreen;
		base.switchToMe();
		this.load();
		this.mapFocus[2] = 1;
		if (!Main.isCompactDevice)
		{
			this.volume = 0;
			this.mapFocus[0] = 0;
			this.mapFocus[1] = 0;
			this.mapFocus[2] = 0;
		}
		else if (this.volume == 0)
		{
			this.mapFocus[2] = 0;
		}
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x000755A8 File Offset: 0x000739A8
	public override void commandActionPointer(int index, int subIndex)
	{
		if (index == 0)
		{
			this.doClose();
		}
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x000755C0 File Offset: 0x000739C0
	public void initSize()
	{
		this.center = new Command(T.complete, 0, this);
		this.hText = MyScreen.hText;
		this.xL = Canvas.h;
		int num = (int)PaintPopup.hTab + AvMain.hDuBox * 2 - 10;
		if (this.isPaint != null)
		{
			this.w = 200 * AvMain.hd;
			this.h = 200 * AvMain.hd;
			this.x = (Canvas.w - this.w) / 2;
			this.y = (Canvas.hCan - this.h) / 2;
			this.hCell = this.h / 4;
			for (int i = 0; i < 3; i++)
			{
				this.isPaint[i] = true;
			}
			this.isPaint[3] = true;
			this.mapFocus = new int[this.max];
		}
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x0007569F File Offset: 0x00073A9F
	protected void doClose()
	{
		this.save(this.volume);
		this.lastScr.switchToMe();
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x000756B8 File Offset: 0x00073AB8
	public void save(int volume)
	{
		this.volume = volume;
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeByte((sbyte)volume);
			for (int i = 0; i < this.max; i++)
			{
				dataOutputStream.writeByte((sbyte)this.mapFocus[i]);
			}
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
		try
		{
			RMS.saveRMS("avatarShowName", dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception e2)
		{
			Out.logError(e2);
		}
		this.init();
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x0007575C File Offset: 0x00073B5C
	public void load()
	{
		this.initSize();
		DataInputStream dataInputStream = AvatarData.initLoad("avatarShowName");
		OptionScr.isVirTualKey = false;
		if (dataInputStream == null)
		{
			return;
		}
		try
		{
			this.volume = (int)dataInputStream.readByte();
			this.mapFocus = new int[this.max];
			for (int i = 0; i < this.max; i++)
			{
				this.mapFocus[i] = (int)dataInputStream.readByte();
				if (this.mapFocus[i] > 1)
				{
					this.mapFocus[i] = 0;
				}
			}
			dataInputStream.close();
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
		this.init();
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00075810 File Offset: 0x00073C10
	private void init()
	{
		Canvas.paint.setLanguage();
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x0007581C File Offset: 0x00073C1C
	public override void updateKey()
	{
		base.updateKey();
		if (!Main.isCompactDevice)
		{
			return;
		}
		if (Canvas.isPointerClick)
		{
			int num = this.hCell;
			for (int i = 0; i < 3; i++)
			{
				if (Canvas.isPoint(this.x + 90 * AvMain.hd - 20 * AvMain.hd, this.y + (i + 1) * this.hCell - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
				{
					this.isTranKey = true;
					this.indexLeft = i;
					Canvas.isPointerClick = false;
				}
				else if (Canvas.isPoint(this.x + this.w - 30 * AvMain.hd - 20 * AvMain.hd, this.y + (i + 1) * this.hCell - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
				{
					this.isTranKey = true;
					this.indexRight = i;
					Canvas.isPointerClick = false;
				}
				num += this.hCell;
			}
		}
		if (this.isTranKey)
		{
			if (Canvas.isPointerDown)
			{
				if (this.indexLeft != -1 && !Canvas.isPoint(this.x + 90 * AvMain.hd - 20 * AvMain.hd, this.y + (this.indexLeft + 1) * this.hCell - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
				{
					this.indexLeft = -1;
				}
				else if (this.indexRight != -1 && !Canvas.isPoint(this.x + this.w - 30 * AvMain.hd - 20 * AvMain.hd, this.y + (this.indexRight + 1) * this.hCell - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
				{
					this.indexRight = -1;
				}
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranKey = false;
				if (this.indexLeft != -1)
				{
					this.setMapFocus(-1, this.indexLeft);
					this.indexLeft = -1;
				}
				else if (this.indexRight != -1)
				{
					this.setMapFocus(1, this.indexRight);
					this.indexRight = -1;
				}
			}
		}
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x00075A78 File Offset: 0x00073E78
	private void setMapFocus(int dir, int i)
	{
		if (i == 2)
		{
			this.volume += dir * 10;
			if (this.volume < 0)
			{
				this.volume = 0;
			}
			if (this.volume > 100)
			{
				this.volume = 100;
			}
			SoundManager.setVolume((float)(this.volume / 100));
		}
		else if (this.mapFocus[i] == 0)
		{
			this.mapFocus[i] = 1;
		}
		else
		{
			this.mapFocus[i] = 0;
		}
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00075B00 File Offset: 0x00073F00
	public override void update()
	{
		this.lastScr.update();
		if (this.xL != 0)
		{
			this.xL += -this.xL >> 1;
			if (this.xL < 0)
			{
				this.xL = 0;
			}
		}
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x00075B4C File Offset: 0x00073F4C
	public override void paint(MyGraphics g)
	{
		this.lastScr.paintMain(g);
		this.paintMain(g);
		base.paint(g);
		g.setColor(0);
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00075B70 File Offset: 0x00073F70
	public override void paintMain(MyGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.translate(0f, (float)this.xL);
		Canvas.paint.paintPopupBack(g, this.x, this.y + 10 * AvMain.hd, this.w, this.h - 20 * AvMain.hd, -1, false);
		g.translate((float)this.x, (float)this.y);
		if (this.point >= 4)
		{
			this.point = 0;
		}
		int num = this.hCell;
		for (int i = 0; i < 3; i++)
		{
			if (this.isPaint[i])
			{
				g.drawImage(PaintPopup.imgMuiIOS[(this.indexLeft != i) ? 0 : 1][2], (float)(90 * AvMain.hd), (float)num, 3);
				g.drawImage(PaintPopup.imgMuiIOS[(this.indexRight != i) ? 0 : 1][3], (float)(this.w - 30 * AvMain.hd), (float)num, 3);
				Canvas.normalFont.drawString(g, T.name[i][2], 10 * AvMain.hd, num - Canvas.normalFont.getHeight() / 2, 0);
				Canvas.normalFont.drawString(g, T.name[i][this.mapFocus[i]], 90 * AvMain.hd + (this.w - 30 * AvMain.hd - 90 * AvMain.hd) / 2, num - Canvas.normalFont.getHeight() / 2, 2);
				num += this.hCell;
			}
		}
		Canvas.normalFont.drawString(g, this.volume + string.Empty, 90 * AvMain.hd + (this.w - 30 * AvMain.hd - 90 * AvMain.hd) / 2, num - Canvas.normalFont.getHeight() / 2 - this.hCell, 2);
		this.point++;
	}

	// Token: 0x04000EDF RID: 3807
	public static OptionScr instance;

	// Token: 0x04000EE0 RID: 3808
	private int point;

	// Token: 0x04000EE1 RID: 3809
	private int focus;

	// Token: 0x04000EE2 RID: 3810
	private int max = 5;

	// Token: 0x04000EE3 RID: 3811
	public int[] mapFocus;

	// Token: 0x04000EE4 RID: 3812
	public int volume = 10;

	// Token: 0x04000EE5 RID: 3813
	private int xL;

	// Token: 0x04000EE6 RID: 3814
	private new int hText;

	// Token: 0x04000EE7 RID: 3815
	public const int SHOW_NAME = 0;

	// Token: 0x04000EE8 RID: 3816
	public const int SHOW_DIRECTION = 1;

	// Token: 0x04000EE9 RID: 3817
	public const int SOUND = 2;

	// Token: 0x04000EEA RID: 3818
	private MyScreen lastScr;

	// Token: 0x04000EEB RID: 3819
	public static bool isVirTualKey;

	// Token: 0x04000EEC RID: 3820
	private bool[] isPaint;

	// Token: 0x04000EED RID: 3821
	public int x;

	// Token: 0x04000EEE RID: 3822
	public int y;

	// Token: 0x04000EEF RID: 3823
	public int w;

	// Token: 0x04000EF0 RID: 3824
	public int h;

	// Token: 0x04000EF1 RID: 3825
	public int hCell;

	// Token: 0x04000EF2 RID: 3826
	private bool isTranKey;

	// Token: 0x04000EF3 RID: 3827
	private new int indexLeft = -1;

	// Token: 0x04000EF4 RID: 3828
	private new int indexRight = -1;
}
