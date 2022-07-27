using System;

// Token: 0x02000166 RID: 358
public class MenuMain : AvMain
{
	// Token: 0x0600095E RID: 2398 RVA: 0x0001D35C File Offset: 0x0001B75C
	public virtual void update()
	{
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x0001D35E File Offset: 0x0001B75E
	public new virtual void updateKey()
	{
		base.updateKey();
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x0001D366 File Offset: 0x0001B766
	public new virtual void paint(MyGraphics g)
	{
		base.paint(g);
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0001D36F File Offset: 0x0001B76F
	public void show()
	{
		Canvas.menuMain = this;
	}
}
