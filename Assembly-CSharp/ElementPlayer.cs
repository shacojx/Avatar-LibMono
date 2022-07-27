using System;

// Token: 0x0200003A RID: 58
public class ElementPlayer
{
	// Token: 0x06000230 RID: 560 RVA: 0x00012BC8 File Offset: 0x00010FC8
	public ElementPlayer(int ID, string name, string text)
	{
		this.IDPlayer = ID;
		this.name = name;
		this.subText = text;
		this.text = new MyVector();
		this.addText(text, false);
	}

	// Token: 0x06000231 RID: 561 RVA: 0x00012BF8 File Offset: 0x00010FF8
	public ElementPlayer(string name, string text)
	{
		this.IDPlayer = -1;
		this.name = name;
		this.subText = text;
	}

	// Token: 0x06000232 RID: 562 RVA: 0x00012C18 File Offset: 0x00011018
	public void addText(string te, bool isO)
	{
		this.subText = te;
		if (Canvas.fontChat.getWidth(this.subText) > Canvas.w / 3 * 2)
		{
			string[] array = Canvas.fontChat.splitFontBStrInLine(this.subText, Canvas.w / 3 * 2);
			this.subText = array[0];
		}
		if (!te.Equals(string.Empty))
		{
			TextMsg o = new TextMsg(te, isO);
			this.text.addElement(o);
		}
		if (this.numSMS < 99)
		{
			this.numSMS += 1;
		}
	}

	// Token: 0x040002C2 RID: 706
	public int IDPlayer;

	// Token: 0x040002C3 RID: 707
	public string name;

	// Token: 0x040002C4 RID: 708
	public string subText;

	// Token: 0x040002C5 RID: 709
	public MyVector text;

	// Token: 0x040002C6 RID: 710
	public IAction action;

	// Token: 0x040002C7 RID: 711
	public short numSMS;
}
