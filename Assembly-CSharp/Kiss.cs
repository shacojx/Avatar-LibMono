using System;

// Token: 0x02000052 RID: 82
public class Kiss
{
	// Token: 0x060002F3 RID: 755 RVA: 0x00018628 File Offset: 0x00016A28
	public Kiss(int xC, int yC)
	{
		this.xCur = xC;
		this.yCur = yC;
		this.x = new int[3];
		this.y = new int[3];
		this.index = new sbyte[3];
		this.dir = new sbyte[3];
		this.dis = new sbyte[3];
		for (int i = 0; i < 3; i++)
		{
			this.index[i] = (sbyte)CRes.rnd(8);
			this.y[i] = -i * 20;
			this.dir[i] = ((CRes.rnd(2) != 0) ? -1 : 1);
			this.dis[i] = 6;
		}
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x000186D8 File Offset: 0x00016AD8
	public void update()
	{
		for (int i = 0; i < 3; i++)
		{
			this.y[i]--;
			if (this.y[i] < -60)
			{
				this.y[i] = 0;
				this.dis[i] = 6;
			}
			sbyte[] array = this.index;
			int num = i;
			array[num] = (sbyte)((int)array[num] + 1);
			if ((int)this.index[i] == 6)
			{
				this.index[i] = 0;
			}
			this.x[i] += (int)this.dir[i] * 2;
			if ((int)this.dir[i] == 1)
			{
				if (this.x[i] > 10 - CRes.abs(this.y[i] / 10))
				{
					this.dir[i] = -1;
					if ((int)this.dis[i] > 0)
					{
						sbyte[] array2 = this.dis;
						int num2 = i;
						array2[num2] = (sbyte)((int)array2[num2] - 1);
					}
				}
			}
			else
			{
				if (this.x[i] < -(10 - CRes.abs(this.y[i] / 10)))
				{
					this.dir[i] = 1;
				}
				if ((int)this.dis[i] > 0)
				{
					sbyte[] array3 = this.dis;
					int num3 = i;
					array3[num3] = (sbyte)((int)array3[num3] - 1);
				}
			}
		}
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x0001881C File Offset: 0x00016C1C
	public void paint(MyGraphics g)
	{
		for (int i = 0; i < 3; i++)
		{
			Avatar.imgKiss.drawFrame((int)this.index[i] / 3, (this.xCur + this.x[i]) * AvMain.hd, (this.yCur + this.y[i]) * AvMain.hd, 0, 3, g);
		}
	}

	// Token: 0x04000386 RID: 902
	private int xCur;

	// Token: 0x04000387 RID: 903
	private int yCur;

	// Token: 0x04000388 RID: 904
	private int[] x;

	// Token: 0x04000389 RID: 905
	private int[] y;

	// Token: 0x0400038A RID: 906
	private sbyte[] index;

	// Token: 0x0400038B RID: 907
	private sbyte[] dir;

	// Token: 0x0400038C RID: 908
	private sbyte[] dis;
}
