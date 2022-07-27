using System;

// Token: 0x020001B7 RID: 439
public class Xingau
{
	// Token: 0x06000BF5 RID: 3061 RVA: 0x00078518 File Offset: 0x00076918
	public Xingau(int x, int y, int type, int typeStop, bool stopHere)
	{
		this.x = x;
		this.y = y;
		this.type = type;
		this.typeStop = typeStop;
		this.stopHere = stopHere;
		Xingau.hImg = 50;
		Xingau.wImg = 54;
		if (AvMain.hd == 2)
		{
			Xingau.wImg = (Xingau.hImg = 108);
		}
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x00078578 File Offset: 0x00076978
	public void paint(MyGraphics g)
	{
		if (AvatarData.getImgIcon(874).count != -1)
		{
			g.drawRegion(AvatarData.getImgIcon(874).img, 0f, (float)(this.idFrame * (int)Xingau.hImg), (int)Xingau.wImg, (int)Xingau.hImg, 0, (float)this.x, (float)this.y, MyGraphics.TOP | MyGraphics.HCENTER);
		}
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x000785E8 File Offset: 0x000769E8
	public void update()
	{
		if (!this.stopHere)
		{
			if (Canvas.gameTick % 2 == 0)
			{
				this.index++;
				if (this.index > Xingau.array[this.type].Length - 1)
				{
					this.index = 0;
				}
			}
			this.idFrame = Xingau.array[this.type][this.index];
		}
		else
		{
			this.idFrame = this.typeStop;
		}
	}

	// Token: 0x04000F43 RID: 3907
	public int x;

	// Token: 0x04000F44 RID: 3908
	public int index;

	// Token: 0x04000F45 RID: 3909
	public int idFrame;

	// Token: 0x04000F46 RID: 3910
	public int y;

	// Token: 0x04000F47 RID: 3911
	public int type;

	// Token: 0x04000F48 RID: 3912
	public int typeStop;

	// Token: 0x04000F49 RID: 3913
	public static int[][] array = new int[3][];

	// Token: 0x04000F4A RID: 3914
	public bool stopHere;

	// Token: 0x04000F4B RID: 3915
	public static sbyte wImg;

	// Token: 0x04000F4C RID: 3916
	public static sbyte hImg;
}
