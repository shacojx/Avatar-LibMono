using System;

// Token: 0x0200003C RID: 60
public abstract class Face : AvMain
{
	// Token: 0x06000235 RID: 565 RVA: 0x00010ECE File Offset: 0x0000F2CE
	public void show()
	{
		Canvas.currentFace = this;
	}

	// Token: 0x06000236 RID: 566 RVA: 0x00010ED6 File Offset: 0x0000F2D6
	public virtual void init(int h)
	{
	}

	// Token: 0x06000237 RID: 567 RVA: 0x00010ED8 File Offset: 0x0000F2D8
	public void close()
	{
		Canvas.currentFace = null;
	}

	// Token: 0x06000238 RID: 568 RVA: 0x00010EE0 File Offset: 0x0000F2E0
	public override void updateKey()
	{
		if (Canvas.currentDialog == null)
		{
			base.updateKey();
		}
	}

	// Token: 0x06000239 RID: 569 RVA: 0x00010EF2 File Offset: 0x0000F2F2
	public override void paint(MyGraphics g)
	{
		if (Canvas.currentDialog == null)
		{
			base.paint(g);
		}
	}
}
