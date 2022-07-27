using System;

// Token: 0x0200003D RID: 61
public class FarmItem
{
	// Token: 0x0600023B RID: 571 RVA: 0x00012CC0 File Offset: 0x000110C0
	public void paint(MyGraphics g, int x, int y, int dir, int anthor)
	{
		ImageIcon imgIcon = FarmData.getImgIcon(this.IDImg);
		if (imgIcon.count != -1)
		{
			g.drawRegion(imgIcon.img, 0f, 0f, (int)imgIcon.w, (int)imgIcon.h, dir, (float)x, (float)y, anthor);
		}
	}

	// Token: 0x040002CB RID: 715
	public short ID;

	// Token: 0x040002CC RID: 716
	public short IDImg;

	// Token: 0x040002CD RID: 717
	public bool isItem;

	// Token: 0x040002CE RID: 718
	public const sbyte T_TREE = 0;

	// Token: 0x040002CF RID: 719
	public const sbyte T_POULTRY = 1;

	// Token: 0x040002D0 RID: 720
	public const sbyte T_CATTLE = 2;

	// Token: 0x040002D1 RID: 721
	public const sbyte T_DOG = 3;

	// Token: 0x040002D2 RID: 722
	public const sbyte T_FISH = 4;

	// Token: 0x040002D3 RID: 723
	public const sbyte T_NOT_FISH = 100;

	// Token: 0x040002D4 RID: 724
	public const sbyte T_PUBLIC = 101;

	// Token: 0x040002D5 RID: 725
	public sbyte type;

	// Token: 0x040002D6 RID: 726
	public const sbyte A_FEEDING = 5;

	// Token: 0x040002D7 RID: 727
	public const sbyte A_INJECTION = 4;

	// Token: 0x040002D8 RID: 728
	public const sbyte A_TONIC = 6;

	// Token: 0x040002D9 RID: 729
	public const sbyte A_BON_PHAN = 2;

	// Token: 0x040002DA RID: 730
	public const sbyte A_DIET_CO = 3;

	// Token: 0x040002DB RID: 731
	public const sbyte A_TRU_SAU = 7;

	// Token: 0x040002DC RID: 732
	public sbyte action;

	// Token: 0x040002DD RID: 733
	public string des;

	// Token: 0x040002DE RID: 734
	public int priceXu;

	// Token: 0x040002DF RID: 735
	public int priceLuong;
}
