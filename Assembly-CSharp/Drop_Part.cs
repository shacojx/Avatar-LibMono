using System;

// Token: 0x02000035 RID: 53
public class Drop_Part : Base
{
	// Token: 0x0600021B RID: 539 RVA: 0x0001224D File Offset: 0x0001064D
	public Drop_Part()
	{
		this.catagory = 5;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0001225C File Offset: 0x0001065C
	public Drop_Part(sbyte typeDrop, short idDrop1, int id)
	{
		this.ID = id;
		this.catagory = 5;
		this.type = typeDrop;
		this.idDrop = idDrop1;
		this.dir = 0;
	}

	// Token: 0x0600021D RID: 541 RVA: 0x00012288 File Offset: 0x00010688
	public override void update()
	{
		switch (this.state)
		{
		case 0:
		case 1:
			this.x += (int)((short)(this.x0 - this.x >> 2));
			this.y += (int)((short)(this.y0 - this.y >> 2));
			if ((int)this.g >= -6)
			{
				this.deltaH = (short)((int)this.deltaH + (int)this.g);
				this.g = (sbyte)((int)this.g - 1);
			}
			if ((CRes.abs(this.x - this.x0) < 4 || CRes.abs(this.y - this.y0) < 4) && this.deltaH <= 1)
			{
				this.x = this.x0;
				this.y = this.y0;
				this.deltaH = 0;
				this.g = 0;
				if ((int)this.state == 1)
				{
					LoadMap.removePlayer(this);
				}
				this.state = 2;
			}
			break;
		case 3:
			this.deltaH += 3;
			if (this.deltaH > 50)
			{
				LoadMap.removePlayer(this);
			}
			break;
		case 4:
			if (this.deltaH > 0)
			{
				this.deltaH = (short)((int)this.deltaH - (int)this.g);
				this.g = (sbyte)((int)this.g + 1);
			}
			else
			{
				this.deltaH = 0;
				this.state = 2;
			}
			break;
		}
	}

	// Token: 0x0600021E RID: 542 RVA: 0x00012420 File Offset: 0x00010820
	public override void paint(MyGraphics g)
	{
		g.drawImage(LoadMap.imgShadow, (float)this.x, (float)(this.y + 1), MyGraphics.BOTTOM | MyGraphics.HCENTER);
		if ((int)this.type == 0)
		{
			Part part = AvatarData.getPart(this.idDrop);
			part.paintIcon(g, this.x, this.y + (int)this.yRung / 20 - (int)this.deltaH, 0, MyGraphics.BOTTOM | MyGraphics.HCENTER);
		}
		else
		{
			AvatarData.paintImg(g, (int)this.idDrop, this.x, this.y + (int)this.yRung / 20 - (int)this.deltaH, MyGraphics.BOTTOM | MyGraphics.HCENTER);
		}
		this.yRung = (sbyte)((int)this.yRung - 2);
		if ((int)this.yRung < -20)
		{
			this.yRung = 0;
		}
	}

	// Token: 0x0600021F RID: 543 RVA: 0x000124FC File Offset: 0x000108FC
	public void startFlyTo(int idUser)
	{
		Avatar avatar = LoadMap.getAvatar(idUser);
		if (avatar != null)
		{
			this.x0 = avatar.x;
			this.y0 = avatar.y;
			this.state = 1;
			this.deltaH = 0;
		}
		else
		{
			this.deltaH = 0;
			this.state = 3;
		}
		this.g = 6;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00012558 File Offset: 0x00010958
	public void startDropFrom(int idPlayer, short xTo, short yTo)
	{
		if (idPlayer == -2)
		{
			this.x = (int)xTo;
			this.y = (int)yTo;
			this.state = 2;
		}
		else
		{
			Avatar avatar = LoadMap.getAvatar(idPlayer);
			if (avatar != null)
			{
				this.x = avatar.x;
				this.y = avatar.y;
				this.state = 0;
				this.g = 6;
				this.deltaH = 0;
			}
			else
			{
				this.state = 4;
				this.x = (int)xTo;
				this.y = (int)yTo;
				this.deltaH = 100;
				this.g = 0;
			}
		}
		this.x0 = (int)xTo;
		this.y0 = (int)yTo;
	}

	// Token: 0x04000296 RID: 662
	public short idDrop;

	// Token: 0x04000297 RID: 663
	public short deltaH;

	// Token: 0x04000298 RID: 664
	public int x0;

	// Token: 0x04000299 RID: 665
	public int y0;

	// Token: 0x0400029A RID: 666
	public int ID;

	// Token: 0x0400029B RID: 667
	private new sbyte g;

	// Token: 0x0400029C RID: 668
	private sbyte dir;

	// Token: 0x0400029D RID: 669
	private sbyte yDrop;

	// Token: 0x0400029E RID: 670
	private sbyte yRung;

	// Token: 0x0400029F RID: 671
	public sbyte type;

	// Token: 0x040002A0 RID: 672
	public sbyte state;

	// Token: 0x040002A1 RID: 673
	public const sbyte STATE_DROP = 0;

	// Token: 0x040002A2 RID: 674
	public const sbyte STATE_FLY = 1;

	// Token: 0x040002A3 RID: 675
	public const sbyte STATE_STAND = 2;

	// Token: 0x040002A4 RID: 676
	public const sbyte STATE_FLY_UP = 3;

	// Token: 0x040002A5 RID: 677
	public const sbyte STATE_DOWN = 4;

	// Token: 0x040002A6 RID: 678
	public const sbyte DROP_POWER = 6;
}
