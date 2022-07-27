using System;

// Token: 0x02000072 RID: 114
public class TextMsg
{
	// Token: 0x060003D3 RID: 979 RVA: 0x00024698 File Offset: 0x00022A98
	public TextMsg(string t, bool isO)
	{
		this.text = Canvas.fontChat.splitFontBStrInLine(t, Canvas.w / 2);
		for (int i = 0; i < this.text.Length; i++)
		{
			int num = Canvas.fontChat.getWidth(this.text[i]) + 10 * AvMain.hd;
			if ((int)this.wPopup < num)
			{
				this.wPopup = (short)num;
			}
		}
		if ((int)this.wPopup < 30 * AvMain.hd)
		{
			this.wPopup = (short)(40 * AvMain.hd);
		}
		this.isOwner = isO;
	}

	// Token: 0x0400062A RID: 1578
	public string[] text;

	// Token: 0x0400062B RID: 1579
	public bool isOwner;

	// Token: 0x0400062C RID: 1580
	public short wPopup;
}
