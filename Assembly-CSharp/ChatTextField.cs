using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class ChatTextField : AvMain
{
	// Token: 0x060005FC RID: 1532 RVA: 0x000380D4 File Offset: 0x000364D4
	protected ChatTextField()
	{
		this.center = new Command(T.chat, new ChatTextField.IActionChat2(this));
		this.tfChat = new TField("chat", MapScr.instance, new ChatTextField.IActionChat2(this));
		this.tfChat.setFocus(true);
		this.tfChat.showSubTextField = false;
		this.tfChat.autoScaleScreen = true;
		this.tfChat.setIputType(ipKeyboard.TEXT);
		this.init(Canvas.hCan);
		this.tfChat.x = 5 * AvMain.hd;
		this.tfChat.setMaxTextLenght(40);
		this.tfChat.action = new ChatTextField.IActionChat2(this);
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x00038188 File Offset: 0x00036588
	public void init(int hc)
	{
		this.tfChat.y = hc - this.tfChat.height - 7 * AvMain.hd;
		this.center = new Command(T.chat, new ChatTextField.IActionChat2(this));
		if (onMainMenu.isOngame)
		{
			this.center.y = -200;
			this.tfChat.y = hc - this.tfChat.height - 7 * AvMain.hd - PaintPopup.hButtonSmall;
			this.tfChat.width = Canvas.w - 10 * AvMain.hd;
		}
		else
		{
			this.center.x = Canvas.w - MyScreen.wTab / 2 - 2 * AvMain.hd;
			this.center.y = this.tfChat.y + this.tfChat.height / 2 - PaintPopup.hButtonSmall / 2;
			this.tfChat.width = this.center.x - MyScreen.wTab / 2 - 10 * AvMain.hd;
		}
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0003829C File Offset: 0x0003669C
	public void keyPressed(int keyCode)
	{
		if (ChatTextField.isShow)
		{
			this.tfChat.keyPressed(keyCode);
		}
		if (this.tfChat.getText().Equals(string.Empty))
		{
			ChatTextField.isShow = false;
		}
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x000382D5 File Offset: 0x000366D5
	public static ChatTextField gI()
	{
		return (ChatTextField.instance != null) ? ChatTextField.instance : (ChatTextField.instance = new ChatTextField());
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x000382F8 File Offset: 0x000366F8
	public void startChat(int firstCharacter, IChatable parentMyScreen)
	{
		if (Canvas.currentFace != null)
		{
			return;
		}
		if (!ipKeyboard.tk.text.Equals(string.Empty))
		{
			this.tfChat.keyPressed(firstCharacter);
			this.parentMyScreen = parentMyScreen;
			ChatTextField.isShow = true;
			this.tfChat.setFocusWithKb(true);
		}
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x00038350 File Offset: 0x00036750
	public override void updateKey()
	{
		TField.currentTField = this.tfChat;
		if (onMainMenu.isOngame)
		{
			Canvas.paint.updateKeyOn(this.left, this.center, this.right);
		}
		else
		{
			base.updateKey();
		}
		if (this.chatButtonLight > 0)
		{
			this.chatButtonLight--;
		}
		this.tfChat.update();
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x000383C0 File Offset: 0x000367C0
	public override void paint(MyGraphics g)
	{
		if (onMainMenu.isOngame)
		{
			Canvas.resetTrans(g);
			Canvas.paint.paintCmdBar(g, this.left, this.center, this.right);
		}
		else
		{
			base.paint(g);
		}
		this.tfChat.paint(g);
		g.setClip(0f, 0f, (float)Canvas.w, (float)Canvas.h);
		Canvas.resetTrans(g);
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x00038434 File Offset: 0x00036834
	public void showTF()
	{
		if (!ChatTextField.isShow)
		{
			this.tfChat.setFocus(true);
			try
			{
				this.parentMyScreen = (IChatable)Canvas.currentMyScreen;
				ChatTextField.isShow = true;
				this.tfChat.parent = Canvas.currentMyScreen;
				this.tfChat.setFocusWithKb(true);
				Canvas.isPointerClick = false;
				Canvas.isPointerRelease = false;
			}
			catch (Exception)
			{
				try
				{
					this.parentMyScreen = MapScr.gI();
					ChatTextField.isShow = true;
					this.tfChat.parent = Canvas.currentMyScreen;
					this.tfChat.setFocusWithKb(true);
					Canvas.isPointerClick = false;
					Canvas.isPointerRelease = false;
				}
				catch (Exception)
				{
				}
			}
		}
		Out.println("showTF: " + ChatTextField.isShow);
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0003851C File Offset: 0x0003691C
	public void showTF(string text)
	{
		if (!ChatTextField.isShow)
		{
			this.tfChat.setFocus(true);
			try
			{
				this.parentMyScreen = (IChatable)Canvas.currentMyScreen;
				ChatTextField.isShow = true;
				this.tfChat.parent = Canvas.currentMyScreen;
				this.tfChat.setText(text);
				this.tfChat.setFocusWithKb(true);
				Canvas.isPointerClick = false;
				Canvas.isPointerRelease = false;
			}
			catch (Exception)
			{
				try
				{
					this.parentMyScreen = MapScr.gI();
					ChatTextField.isShow = true;
					this.tfChat.parent = Canvas.currentMyScreen;
					this.tfChat.setText(text);
					this.tfChat.setFocusWithKb(true);
					Canvas.isPointerClick = false;
					Canvas.isPointerRelease = false;
				}
				catch (Exception)
				{
				}
			}
		}
		Out.println("showTF: " + ChatTextField.isShow);
	}

	// Token: 0x04000829 RID: 2089
	public static ChatTextField instance;

	// Token: 0x0400082A RID: 2090
	public TField tfChat;

	// Token: 0x0400082B RID: 2091
	public static bool isShow;

	// Token: 0x0400082C RID: 2092
	public IChatable parentMyScreen;

	// Token: 0x0400082D RID: 2093
	private long lastTimeChat;

	// Token: 0x0400082E RID: 2094
	private int chatButtonLight;

	// Token: 0x020000BC RID: 188
	private class IActionChat2 : IAction
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x0003861E File Offset: 0x00036A1E
		public IActionChat2(ChatTextField me)
		{
			this.me = me;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00038630 File Offset: 0x00036A30
		public void perform()
		{
			if (this.me != null && this.me.parentMyScreen != null)
			{
				string text = this.me.tfChat.getText();
				this.me.parentMyScreen.onChatFromMe(text);
				this.me.tfChat.setText(string.Empty);
				if (ipKeyboard.tk != null)
				{
					ipKeyboard.tk.text = string.Empty;
				}
				if (Screen.orientation == 1)
				{
					ChatTextField.isShow = false;
				}
				if (text.ToLower().Equals("cauca") && TouchScreenKeyboard.visible)
				{
					ipKeyboard.close();
					ipKeyboard.tk = null;
				}
			}
		}

		// Token: 0x0400082F RID: 2095
		public ChatTextField me;
	}
}
