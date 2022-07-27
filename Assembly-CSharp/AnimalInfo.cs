using System;

// Token: 0x02000026 RID: 38
public class AnimalInfo
{
	// Token: 0x06000184 RID: 388 RVA: 0x0000BB18 File Offset: 0x00009F18
	public AnimalInfo()
	{
		this.arrFrame = new sbyte[3][];
		for (int i = 0; i < 3; i++)
		{
			this.arrFrame[i] = new sbyte[12];
		}
	}

	// Token: 0x0400016F RID: 367
	public sbyte species;

	// Token: 0x04000170 RID: 368
	public sbyte frame;

	// Token: 0x04000171 RID: 369
	public sbyte area;

	// Token: 0x04000172 RID: 370
	public int harvestTime;

	// Token: 0x04000173 RID: 371
	public int[] price = new int[2];

	// Token: 0x04000174 RID: 372
	public short priceProduct;

	// Token: 0x04000175 RID: 373
	public short iconID;

	// Token: 0x04000176 RID: 374
	public short iconProduct = -1;

	// Token: 0x04000177 RID: 375
	public short iconO = -1;

	// Token: 0x04000178 RID: 376
	public short[] idImg = new short[3];

	// Token: 0x04000179 RID: 377
	public sbyte[][] arrFrame;

	// Token: 0x0400017A RID: 378
	public string name;

	// Token: 0x0400017B RID: 379
	public string des;
}
