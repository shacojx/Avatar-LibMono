using System;

// Token: 0x02000032 RID: 50
public class Command
{
	// Token: 0x06000202 RID: 514 RVA: 0x00010C80 File Offset: 0x0000F080
	public Command()
	{
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00010C96 File Offset: 0x0000F096
	public Command(string caption, IAction action)
	{
		this.caption = caption;
		this.action = action;
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00010CBA File Offset: 0x0000F0BA
	public Command(string caption, IKbAction action)
	{
		this.caption = caption;
		this.ipaction = action;
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00010CDE File Offset: 0x0000F0DE
	public Command(string caption, int type)
	{
		this.caption = caption;
		this.indexMenu = (sbyte)type;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00010D03 File Offset: 0x0000F103
	public Command(string caption, int type, AvMain pointer)
	{
		this.caption = caption;
		this.indexMenu = (sbyte)type;
		this.pointer = pointer;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00010D2F File Offset: 0x0000F12F
	public Command(string caption, int type, int sub)
	{
		this.caption = caption;
		this.indexMenu = (sbyte)type;
		this.subIndex = (sbyte)sub;
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00010D5C File Offset: 0x0000F15C
	public Command(string caption, int type, int subIndex, AvMain pointer)
	{
		this.caption = caption;
		this.indexMenu = (sbyte)type;
		this.pointer = pointer;
		this.subIndex = (sbyte)subIndex;
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00010D91 File Offset: 0x0000F191
	public Command(string caption, int type, int x, int y)
	{
		this.caption = caption;
		this.indexMenu = (sbyte)type;
		this.x = x;
		this.y = y;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00010DC5 File Offset: 0x0000F1C5
	public Command(string caption, IAction action, int x, int y)
	{
		this.caption = caption;
		this.action = action;
		this.x = x;
		this.y = y;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00010DF8 File Offset: 0x0000F1F8
	public void perform()
	{
		Out.println("command gettext: " + Canvas.inputDlg.tfInput.getText());
		if (this.action != null)
		{
			this.action.perform();
		}
		else if (this.ipaction != null)
		{
			this.ipaction.perform(Canvas.inputDlg.tfInput.getText());
		}
		else if (this.pointer != null)
		{
			this.pointer.commandActionPointer((int)this.indexMenu, (int)this.subIndex);
		}
		else if (Canvas.currentDialog != null)
		{
			Canvas.currentDialog.commandTab((int)this.indexMenu);
		}
		else
		{
			Canvas.currentMyScreen.commandTab((int)this.indexMenu);
		}
	}

	// Token: 0x0600020C RID: 524 RVA: 0x00010EC2 File Offset: 0x0000F2C2
	public virtual void update()
	{
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00010EC4 File Offset: 0x0000F2C4
	public virtual void paint(MyGraphics g, int x, int y)
	{
	}

	// Token: 0x04000267 RID: 615
	public string caption;

	// Token: 0x04000268 RID: 616
	public IAction action;

	// Token: 0x04000269 RID: 617
	public IKbAction ipaction;

	// Token: 0x0400026A RID: 618
	public sbyte indexMenu;

	// Token: 0x0400026B RID: 619
	public sbyte subIndex;

	// Token: 0x0400026C RID: 620
	public sbyte indexImage;

	// Token: 0x0400026D RID: 621
	public int x = -1;

	// Token: 0x0400026E RID: 622
	public int y = -1;

	// Token: 0x0400026F RID: 623
	public AvMain pointer;
}
