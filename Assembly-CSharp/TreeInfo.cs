using System;

// Token: 0x02000076 RID: 118
public class TreeInfo
{
	// Token: 0x060003E0 RID: 992 RVA: 0x00024BA2 File Offset: 0x00022FA2
	public void paint(MyGraphics g, int id, int x, int y, int arthor)
	{
		if (this.isDynamic)
		{
			FarmData.paintImg(g, (int)this.idImg[id], x, y, arthor);
		}
		else
		{
			FarmData.listImgInfo[(int)this.idImg[id]].paintFarm(g, x, y, arthor);
		}
	}

	// Token: 0x0400063A RID: 1594
	public string name;

	// Token: 0x0400063B RID: 1595
	public short ID;

	// Token: 0x0400063C RID: 1596
	public short[] idImg;

	// Token: 0x0400063D RID: 1597
	public sbyte[] Phase;

	// Token: 0x0400063E RID: 1598
	public short harvestTime;

	// Token: 0x0400063F RID: 1599
	public short dieTime = -1;

	// Token: 0x04000640 RID: 1600
	public short[] priceSeed = new short[2];

	// Token: 0x04000641 RID: 1601
	public short priceProduct;

	// Token: 0x04000642 RID: 1602
	public short numProduct;

	// Token: 0x04000643 RID: 1603
	public short productID;

	// Token: 0x04000644 RID: 1604
	public string name1;

	// Token: 0x04000645 RID: 1605
	public short idIcon;

	// Token: 0x04000646 RID: 1606
	public bool isDynamic;

	// Token: 0x04000647 RID: 1607
	public sbyte lv = 1;
}
