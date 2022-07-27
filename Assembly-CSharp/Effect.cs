using System;

// Token: 0x02000036 RID: 54
public abstract class Effect
{
	// Token: 0x06000222 RID: 546
	public abstract void update();

	// Token: 0x06000223 RID: 547
	public abstract void paint(MyGraphics g);

	// Token: 0x06000224 RID: 548 RVA: 0x0000BB8D File Offset: 0x00009F8D
	public void show()
	{
		Canvas.currentEffect.addElement(this);
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000BB9A File Offset: 0x00009F9A
	public virtual void close()
	{
		Canvas.currentEffect.removeElement(this);
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000BBA7 File Offset: 0x00009FA7
	public virtual void paintBack(MyGraphics g)
	{
	}

	// Token: 0x040002A7 RID: 679
	public bool isStop;

	// Token: 0x040002A8 RID: 680
	public short IDAction = -1;
}
