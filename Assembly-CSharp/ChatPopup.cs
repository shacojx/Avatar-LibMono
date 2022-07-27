using System;

// Token: 0x020000BA RID: 186
public class ChatPopup
{
	// Token: 0x060005F3 RID: 1523 RVA: 0x00037AF9 File Offset: 0x00035EF9
	public ChatPopup()
	{
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00037B04 File Offset: 0x00035F04
	public ChatPopup(int time, string chat, sbyte NPC)
	{
		this.iNPC = NPC;
		this.prepareData(time, chat);
		this.hCount = this.h - ChatPopup.imgPopup[0].frameHeight * 2;
		ChatPopup.hText = (short)(Canvas.fontChatB.getHeight() - 5);
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x00037B53 File Offset: 0x00035F53
	public void setPos(int x, int y)
	{
		this.xc = x;
		this.yc = y;
		if (onMainMenu.isOngame)
		{
			this.yc -= 20 * AvMain.hd;
		}
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x00037B82 File Offset: 0x00035F82
	public bool setOut()
	{
		if (this.countDown > 1)
		{
			this.countDown /= 2;
		}
		if (Canvas.getTick() / 1000L - (long)this.timeCur >= 5L)
		{
			return true;
		}
		this.recalculatePos();
		return false;
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x00037BC4 File Offset: 0x00035FC4
	public void prepareData(int time, string chat)
	{
		ChatPopup.hText = (short)(Canvas.fontChatB.getHeight() - 5);
		this.w = 80 * AvMain.hd;
		this.chats = Canvas.fontChatB.splitFontBStrInLine(chat, this.w - 25 * AvMain.hd);
		this.h = (int)ChatPopup.hText * this.chats.Length + 4 + 4;
		if (this.h < (int)(ChatPopup.wPop * 2) || this.chats.Length == 1)
		{
			this.h = (int)(ChatPopup.wPop * 2);
		}
		this.w = 0;
		for (int i = 0; i < this.chats.Length; i++)
		{
			int num = Canvas.fontChatB.getWidth(this.chats[i]) + 25 * AvMain.hd;
			if (num > this.w)
			{
				this.w = num;
			}
		}
		if (this.w < 30 * AvMain.hd)
		{
			this.w = 30 * AvMain.hd;
		}
		this.timeOut = time;
		this.timeCur = (int)(Canvas.getTick() / 1000L);
		if (this.countDown <= 1)
		{
			this.countDown = this.w / 4;
		}
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x00037CFC File Offset: 0x000360FC
	public void recalculatePos()
	{
		if (Canvas.currentMyScreen is BoardScr && (onMainMenu.isOngame || BoardScr.isStartGame || BoardScr.disableReady))
		{
			if (this.yc - this.h < 0)
			{
				this.yc = this.h + 10;
			}
			if (this.xc - this.w / 2 < 0)
			{
				this.xc = this.w / 2;
			}
			if (this.xc + this.w / 2 > Canvas.w)
			{
				this.xc = Canvas.w - this.w / 2;
			}
		}
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x00037DA8 File Offset: 0x000361A8
	public void paintAnimal(MyGraphics g)
	{
		int num = AvMain.hd;
		if (Canvas.currentMyScreen == BoardScr.me && (onMainMenu.isOngame || BoardScr.isStartGame || BoardScr.disableReady))
		{
			num = 1;
		}
		ChatPopup.paintRoundRect(g, this.xc * num - (this.w - this.countDown) / 2, this.yc * num - (this.h - this.countDown), this.w - this.countDown, this.h - this.countDown, ((int)this.iNPC != 1) ? 16777215 : 16768679, ((int)this.iNPC != 1) ? 14145495 : 13940870, this.iNPC);
		g.drawImage(ChatPopup.imgArrow[(int)this.iNPC], (float)(this.xc * num - (((int)this.iNPC != 0) ? 0 : (3 * AvMain.hd))), (float)(this.yc * num - 2), MyGraphics.TOP | MyGraphics.HCENTER);
		for (int i = 0; i < this.chats.Length; i++)
		{
			Canvas.fontChatB.drawString(g, this.chats[i], this.xc * num - this.w / 2 + this.w / 2, this.yc * num - this.h / 2 + i * ((int)ChatPopup.hText - this.countDown / 2) - this.chats.Length * (int)ChatPopup.hText / 2 - (int)(ChatPopup.hText / 4), 2);
		}
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x00037F44 File Offset: 0x00036344
	public static void paintRoundRect(MyGraphics g, int x, int y, int w, int h, int color1, int color2, sbyte iNPC)
	{
		ChatPopup.imgPopup[(int)iNPC].drawFrame(0, x, y, 0, g);
		ChatPopup.imgPopup[(int)iNPC].drawFrame(1, x + w - (int)ChatPopup.wPop, y, 0, g);
		ChatPopup.imgPopup[(int)iNPC].drawFrame(3, x, y + h - (int)ChatPopup.wPop, 0, g);
		ChatPopup.imgPopup[(int)iNPC].drawFrame(2, x + w - (int)ChatPopup.wPop, y + h - (int)ChatPopup.wPop, 0, g);
		g.setColor(color1);
		g.fillRect((float)(x + (int)ChatPopup.wPop), (float)y, (float)(w - (int)(ChatPopup.wPop * 2)), (float)ChatPopup.wPop);
		g.fillRect((float)(x + (int)ChatPopup.wPop), (float)(y + h - (int)ChatPopup.wPop), (float)(w - (int)(ChatPopup.wPop * 2)), (float)ChatPopup.wPop);
		g.fillRect((float)x, (float)(y + (int)ChatPopup.wPop), (float)w, (float)(h - (int)(ChatPopup.wPop * 2)));
		g.setColor(color2);
		g.fillRect((float)(x + (int)ChatPopup.wPop), (float)y, (float)(w - (int)(ChatPopup.wPop * 2)), 1f);
		g.fillRect((float)(x + (int)ChatPopup.wPop), (float)(y + h - 1), (float)(w - (int)(ChatPopup.wPop * 2)), 1f);
		g.fillRect((float)x, (float)(y + (int)ChatPopup.wPop), 1f, (float)(h - (int)(ChatPopup.wPop * 2)));
		g.fillRect((float)(x + w - 1), (float)(y + (int)ChatPopup.wPop), 1f, (float)(h - (int)(ChatPopup.wPop * 2)));
	}

	// Token: 0x0400081B RID: 2075
	public int timeOut;

	// Token: 0x0400081C RID: 2076
	public int xc;

	// Token: 0x0400081D RID: 2077
	public int yc;

	// Token: 0x0400081E RID: 2078
	public int h;

	// Token: 0x0400081F RID: 2079
	public int w;

	// Token: 0x04000820 RID: 2080
	public int hCount;

	// Token: 0x04000821 RID: 2081
	public static short wPop;

	// Token: 0x04000822 RID: 2082
	public static short hText;

	// Token: 0x04000823 RID: 2083
	public string[] chats;

	// Token: 0x04000824 RID: 2084
	public static FrameImage[] imgPopup = new FrameImage[3];

	// Token: 0x04000825 RID: 2085
	public static Image[] imgArrow = new Image[2];

	// Token: 0x04000826 RID: 2086
	private sbyte iNPC;

	// Token: 0x04000827 RID: 2087
	public int timeCur;

	// Token: 0x04000828 RID: 2088
	public int countDown;
}
