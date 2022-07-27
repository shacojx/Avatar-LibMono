using System;

// Token: 0x0200004B RID: 75
public class ImageIcon
{
	// Token: 0x060002DD RID: 733 RVA: 0x00017B33 File Offset: 0x00015F33
	public ImageIcon()
	{
	}

	// Token: 0x060002DE RID: 734 RVA: 0x00017B42 File Offset: 0x00015F42
	public ImageIcon(Image im)
	{
		this.img = im;
		this.count = 0;
		this.w = (short)im.getWidth();
		this.h = (short)im.getHeight();
	}

	// Token: 0x0400035A RID: 858
	public Image img;

	// Token: 0x0400035B RID: 859
	public short w;

	// Token: 0x0400035C RID: 860
	public short h;

	// Token: 0x0400035D RID: 861
	public int count = -1;
}
