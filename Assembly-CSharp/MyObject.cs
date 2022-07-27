using System;

// Token: 0x0200017F RID: 383
public abstract class MyObject
{
	// Token: 0x06000A1C RID: 2588 RVA: 0x0000205F File Offset: 0x0000045F
	public virtual void update()
	{
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x00002061 File Offset: 0x00000461
	public virtual void paint(MyGraphics g)
	{
	}

	// Token: 0x04000D07 RID: 3335
	public int x;

	// Token: 0x04000D08 RID: 3336
	public int y;

	// Token: 0x04000D09 RID: 3337
	public static int hd = AvMain.hd;

	// Token: 0x04000D0A RID: 3338
	public const sbyte AVATAR = 0;

	// Token: 0x04000D0B RID: 3339
	public const sbyte TREE = 1;

	// Token: 0x04000D0C RID: 3340
	public const sbyte ANIMAL = 2;

	// Token: 0x04000D0D RID: 3341
	public const sbyte PET = 4;

	// Token: 0x04000D0E RID: 3342
	public const sbyte DROP = 5;

	// Token: 0x04000D0F RID: 3343
	public const sbyte EFFECT = 6;

	// Token: 0x04000D10 RID: 3344
	public const sbyte FISH = 7;

	// Token: 0x04000D11 RID: 3345
	public const sbyte STAR_FRUIT = 8;

	// Token: 0x04000D12 RID: 3346
	public const sbyte POPUPNAME = 9;

	// Token: 0x04000D13 RID: 3347
	public const sbyte PETRACE = 10;

	// Token: 0x04000D14 RID: 3348
	public sbyte catagory;

	// Token: 0x04000D15 RID: 3349
	public short height;

	// Token: 0x04000D16 RID: 3350
	public short w;

	// Token: 0x04000D17 RID: 3351
	public short h;

	// Token: 0x04000D18 RID: 3352
	public short index = -1;
}
