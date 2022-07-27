using System;

// Token: 0x02000065 RID: 101
public class PictureObj
{
	// Token: 0x0600037E RID: 894 RVA: 0x0001F83E File Offset: 0x0001DC3E
	public PictureObj(int id, int x, int y, int or, bool line)
	{
		this.ID = id;
		this.x = x;
		this.y = y;
		this.orthor = or;
		this.linebreak = line;
	}

	// Token: 0x04000467 RID: 1127
	public int x;

	// Token: 0x04000468 RID: 1128
	public int y;

	// Token: 0x04000469 RID: 1129
	public int orthor;

	// Token: 0x0400046A RID: 1130
	public int ID;

	// Token: 0x0400046B RID: 1131
	public int h;

	// Token: 0x0400046C RID: 1132
	public int w;

	// Token: 0x0400046D RID: 1133
	public bool linebreak;
}
