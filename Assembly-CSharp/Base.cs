using System;

// Token: 0x020000B2 RID: 178
public class Base : MyObject
{
	// Token: 0x06000584 RID: 1412 RVA: 0x0000ABE4 File Offset: 0x00008FE4
	protected void getChat()
	{
		if (this.chat == null && this.listChat.size() > 0)
		{
			this.chat = (ChatPopup)this.listChat.elementAt(0);
			this.chat.setPos(this.x, this.y - 45);
			this.listChat.removeElementAt(0);
		}
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x0000AC4A File Offset: 0x0000904A
	public void addChat(int time, string text, sbyte boss)
	{
		this.listChat.addElement(new ChatPopup(time, text, boss));
		this.getChat();
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0000AC65 File Offset: 0x00009065
	public override void paint(MyGraphics g)
	{
		if (this.chat != null && Canvas.currentMyScreen != MainMenu.me)
		{
			this.chat.paintAnimal(g);
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x0000AC90 File Offset: 0x00009090
	public void setPos(int x, int y)
	{
		this.xCur = x;
		this.x = x;
		this.yCur = y;
		this.y = y;
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0000ACC0 File Offset: 0x000090C0
	public override void update()
	{
		if (this.chat != null)
		{
			this.chat.setPos(this.x, this.y - 45);
			if (this.chat.setOut())
			{
				this.chat = null;
				this.getChat();
			}
		}
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0000AD10 File Offset: 0x00009110
	public virtual bool detectCollision(int vX, int vY)
	{
		if ((int)this.action == -1 || (int)this.action == 14)
		{
			this.vx = 0;
			this.vy = 0;
			return true;
		}
		if ((int)this.action != 10 && (int)this.action != 2 && (int)this.action != 4)
		{
			this.action = 0;
		}
		if ((int)this.action != 0 && (int)this.action != 1)
		{
			this.vx = 0;
			this.vy = 0;
			return true;
		}
		this.action = 1;
		int x = this.x;
		int y = this.y;
		if ((int)this.catagory == 2)
		{
			x = this.xCur;
			y = this.yCur;
		}
		if (LoadMap.isTrans(x + vX, y + vY))
		{
			if (vX != 0)
			{
				if (vX > 0)
				{
					this.vx = this.v;
				}
				else
				{
					this.vx = -this.v;
				}
			}
			if (vY != 0)
			{
				if (vY > 0)
				{
					this.vy = this.v;
				}
				else
				{
					this.vy = -this.v;
				}
			}
			return false;
		}
		this.vx = 0;
		this.vy = 0;
		return true;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0000AE48 File Offset: 0x00009248
	public bool setWay(int vX, int vY)
	{
		if ((int)this.action != 0 && (int)this.action != 1)
		{
			return false;
		}
		int num = this.x;
		if ((int)this.catagory == 0)
		{
			num += ((vX >= 0) ? 7 : -7);
		}
		if (vX != 0)
		{
			int typeMap = LoadMap.getTypeMap(num + vX, this.y - 24);
			int typeMap2 = LoadMap.getTypeMap(num, this.y - 24);
			if (typeMap == 80 && typeMap2 == 80)
			{
				this.vy = -this.v;
				this.xCur = num;
				MapScr.gI().move();
				return true;
			}
			int typeMap3 = LoadMap.getTypeMap(num + vX, this.y + 24);
			int typeMap4 = LoadMap.getTypeMap(num, this.y + 24);
			if (typeMap3 == 80 && typeMap4 == 80)
			{
				this.vy = this.v;
				this.xCur = num;
				MapScr.gI().move();
				return true;
			}
		}
		else if (vY != 0)
		{
			int typeMap5 = LoadMap.getTypeMap(num - 24, this.y + vY);
			int typeMap6 = LoadMap.getTypeMap(num - 24, this.y);
			if (typeMap5 == 80 && typeMap6 == 80)
			{
				this.vx = -this.v;
				this.yCur = this.y;
				MapScr.gI().move();
				return true;
			}
			int typeMap7 = LoadMap.getTypeMap(num + 24, this.y + vY);
			int typeMap8 = LoadMap.getTypeMap(num + 24, this.y);
			if (typeMap7 == 80 && typeMap8 == 80)
			{
				this.vx = this.v;
				this.yCur = this.y;
				MapScr.gI().move();
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0000B001 File Offset: 0x00009401
	public virtual void paintIcon(MyGraphics g2, int x, int y, bool isName)
	{
	}

	// Token: 0x040007BB RID: 1979
	public const sbyte DOING = -1;

	// Token: 0x040007BC RID: 1980
	public const sbyte STAND = 0;

	// Token: 0x040007BD RID: 1981
	public const sbyte RUN = 1;

	// Token: 0x040007BE RID: 1982
	public const sbyte SAT_UP_STAND = 2;

	// Token: 0x040007BF RID: 1983
	public const sbyte JUMPS = 10;

	// Token: 0x040007C0 RID: 1984
	public const sbyte AvatarRace = 11;

	// Token: 0x040007C1 RID: 1985
	public const sbyte TAM = 14;

	// Token: 0x040007C2 RID: 1986
	public int IDDB;

	// Token: 0x040007C3 RID: 1987
	public string name = string.Empty;

	// Token: 0x040007C4 RID: 1988
	public int frame;

	// Token: 0x040007C5 RID: 1989
	public sbyte g = 7;

	// Token: 0x040007C6 RID: 1990
	public sbyte vhy;

	// Token: 0x040007C7 RID: 1991
	public sbyte vh;

	// Token: 0x040007C8 RID: 1992
	public int xCur;

	// Token: 0x040007C9 RID: 1993
	public int yCur;

	// Token: 0x040007CA RID: 1994
	public int vx;

	// Token: 0x040007CB RID: 1995
	public int vy;

	// Token: 0x040007CC RID: 1996
	public int v = 4;

	// Token: 0x040007CD RID: 1997
	public sbyte action;

	// Token: 0x040007CE RID: 1998
	public static sbyte RIGHT;

	// Token: 0x040007CF RID: 1999
	public static sbyte LEFT = 2;

	// Token: 0x040007D0 RID: 2000
	public sbyte direct = Base.LEFT;

	// Token: 0x040007D1 RID: 2001
	public ChatPopup chat;

	// Token: 0x040007D2 RID: 2002
	public MyVector listChat = new MyVector();

	// Token: 0x040007D3 RID: 2003
	public bool ableShow;
}
