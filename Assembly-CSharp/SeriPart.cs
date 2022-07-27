using System;

// Token: 0x0200006C RID: 108
public class SeriPart
{
	// Token: 0x060003AC RID: 940 RVA: 0x000219EF File Offset: 0x0001FDEF
	public SeriPart()
	{
	}

	// Token: 0x060003AD RID: 941 RVA: 0x000219F7 File Offset: 0x0001FDF7
	public SeriPart(short idP)
	{
		this.idPart = idP;
	}

	// Token: 0x040004BA RID: 1210
	public short idPart;

	// Token: 0x040004BB RID: 1211
	public sbyte time;

	// Token: 0x040004BC RID: 1212
	public string expireString;
}
