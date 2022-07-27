using System;

// Token: 0x02000038 RID: 56
public class EffectManager
{
	// Token: 0x0600022A RID: 554 RVA: 0x000126F9 File Offset: 0x00010AF9
	public EffectManager()
	{
		this.count = 0;
	}

	// Token: 0x0600022B RID: 555 RVA: 0x00012708 File Offset: 0x00010B08
	public void update()
	{
		if (AvatarData.getEffect(this.ID) == null)
		{
			return;
		}
		if ((int)this.style == 0)
		{
			Avatar avatar = LoadMap.getAvatar(this.idPlayer);
			if (avatar == null)
			{
				LoadMap.effManager.removeElement(this);
				return;
			}
			this.x = (short)avatar.x;
			this.y = (short)avatar.y;
		}
		if (this.count == this.loopLimit)
		{
			this.count = 0;
			EffectObj effectObj = new EffectObj();
			effectObj.ID = this.ID;
			effectObj.idPlayer = this.idPlayer;
			effectObj.style = this.style;
			switch (this.loopType)
			{
			case 0:
				effectObj.x = (int)this.x;
				effectObj.y = (int)this.y;
				break;
			case 1:
			{
				int num = CRes.rnd((int)this.radius);
				int angle = CRes.rnd(360);
				int num2 = num * CRes.cos(CRes.fixangle(angle)) >> 10;
				int num3 = -(num * CRes.sin(CRes.fixangle(angle))) >> 10;
				effectObj.x = (int)this.x;
				effectObj.y = (int)this.y;
				effectObj.dx = (short)num2;
				effectObj.dy = (short)num3;
				break;
			}
			case 2:
				effectObj.x = (int)this.x;
				effectObj.y = (int)this.y;
				if ((int)this.style == 0)
				{
					effectObj.dx = this.xLoop[(int)this.indexPos];
					effectObj.dy = this.yLoop[(int)this.indexPos];
				}
				else
				{
					effectObj.x += (int)this.xLoop[(int)this.indexPos];
					effectObj.y += (int)this.yLoop[(int)this.indexPos];
				}
				break;
			}
			this.indexLoop += 1;
			this.indexPos += 1;
			if (this.xLoop != null && (int)this.indexPos >= this.xLoop.Length)
			{
				this.indexPos = 0;
			}
			if (this.loop != -1 && this.indexLoop >= this.loop)
			{
				LoadMap.effManager.removeElement(this);
			}
			switch (this.style)
			{
			case 0:
				LoadMap.playerLists.addElement(effectObj);
				LoadMap.playerLists = LoadMap.orderVector(LoadMap.playerLists);
				break;
			case 1:
				LoadMap.treeLists.addElement(effectObj);
				LoadMap.treeLists = LoadMap.orderVector(LoadMap.treeLists);
				break;
			case 2:
				if (LoadMap.effBgList == null)
				{
					LoadMap.effBgList = new MyVector();
				}
				LoadMap.effBgList.addElement(effectObj);
				break;
			case 3:
				if (LoadMap.effCameraList == null)
				{
					LoadMap.effCameraList = new MyVector();
				}
				LoadMap.effCameraList.addElement(effectObj);
				break;
			}
		}
		this.count += 1;
	}

	// Token: 0x040002AE RID: 686
	public short ID;

	// Token: 0x040002AF RID: 687
	public short loop;

	// Token: 0x040002B0 RID: 688
	public short loopLimit;

	// Token: 0x040002B1 RID: 689
	public short radius;

	// Token: 0x040002B2 RID: 690
	public short x;

	// Token: 0x040002B3 RID: 691
	public short y;

	// Token: 0x040002B4 RID: 692
	public short count;

	// Token: 0x040002B5 RID: 693
	public short indexLoop;

	// Token: 0x040002B6 RID: 694
	public short indexPos;

	// Token: 0x040002B7 RID: 695
	public int idPlayer;

	// Token: 0x040002B8 RID: 696
	public sbyte style;

	// Token: 0x040002B9 RID: 697
	public sbyte loopType;

	// Token: 0x040002BA RID: 698
	public short[] xLoop;

	// Token: 0x040002BB RID: 699
	public short[] yLoop;
}
