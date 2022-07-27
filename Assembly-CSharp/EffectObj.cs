using System;

// Token: 0x02000039 RID: 57
public class EffectObj : Base
{
	// Token: 0x0600022C RID: 556 RVA: 0x00012A0C File Offset: 0x00010E0C
	public EffectObj()
	{
		this.dx = (this.dy = 0);
		this.catagory = 6;
		this.index = 0;
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00012A40 File Offset: 0x00010E40
	public override void update()
	{
		try
		{
			EffectData effect = AvatarData.getEffect(this.ID);
			if (effect != null)
			{
				this.index = (sbyte)((int)this.index + 1);
				if ((int)this.index >= effect.arrFrame.Length)
				{
					this.removee();
				}
			}
			else
			{
				this.removee();
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00012AB8 File Offset: 0x00010EB8
	public override void paint(MyGraphics g)
	{
		if (Canvas.currentMyScreen == MainMenu.gI())
		{
			return;
		}
		EffectData effect = AvatarData.getEffect(this.ID);
		if (effect != null)
		{
			if ((int)this.style == 0)
			{
				Avatar avatar = LoadMap.getAvatar(this.idPlayer);
				if (avatar == null)
				{
					this.removee();
					return;
				}
				this.x = avatar.x + (int)this.dx;
				this.y = avatar.y + (int)this.dy;
			}
			effect.paint(g, this.x, this.y, (int)this.index);
		}
	}

	// Token: 0x0600022F RID: 559 RVA: 0x00012B54 File Offset: 0x00010F54
	private void removee()
	{
		switch (this.style)
		{
		case 0:
			LoadMap.playerLists.removeElement(this);
			break;
		case 1:
			LoadMap.treeLists.removeElement(this);
			break;
		case 2:
			LoadMap.effBgList.removeElement(this);
			break;
		case 3:
			LoadMap.effCameraList.removeElement(this);
			break;
		}
	}

	// Token: 0x040002BC RID: 700
	public short ID;

	// Token: 0x040002BD RID: 701
	public short dx;

	// Token: 0x040002BE RID: 702
	public short dy;

	// Token: 0x040002BF RID: 703
	public int idPlayer;

	// Token: 0x040002C0 RID: 704
	public sbyte style;

	// Token: 0x040002C1 RID: 705
	public new sbyte index;
}
