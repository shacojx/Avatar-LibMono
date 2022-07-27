using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class ipKeyboard
{
	// Token: 0x060000C9 RID: 201 RVA: 0x00006D30 File Offset: 0x00005130
	public static void openKeyBoard(string caption, int type, string text, IKbAction action, bool hideInput)
	{
		Out.println("openKeyBoard: " + caption);
		ipKeyboard.act = action;
		ipKeyboard.typeInput = type;
		ipKeyboard.isInput = hideInput;
		TouchScreenKeyboardType touchScreenKeyboardType = (type != 0 && type != 2) ? 4 : 1;
		if (action == null)
		{
			TouchScreenKeyboard.hideInput = true;
		}
		else
		{
			TouchScreenKeyboard.hideInput = hideInput;
		}
		Out.println("openKeyBoard: " + text);
		ipKeyboard.tk = TouchScreenKeyboard.Open(text, touchScreenKeyboardType, false, false, type == 2, false, caption);
		ipKeyboard.isReset = true;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00006DB8 File Offset: 0x000051B8
	public static void update()
	{
		if (ipKeyboard.tk == null)
		{
			return;
		}
		if (Canvas.currentMyScreen != MessageScr.me && !ChatTextField.isShow && Canvas.isPaintIconVir() && !ipKeyboard.tk.text.Equals(string.Empty))
		{
			ChatTextField.gI().showTF(ipKeyboard.tk.text);
		}
		if (ipKeyboard.tk.done)
		{
			if (ipKeyboard.act != null)
			{
				ipKeyboard.act.perform(ipKeyboard.tk.text);
				ipKeyboard.act = null;
				if (ChatTextField.isShow)
				{
					ChatTextField.isShow = false;
				}
			}
			ipKeyboard.tk.text = string.Empty;
			if (Screen.orientation == 1)
			{
				ipKeyboard.isReset = true;
				ipKeyboard.tk.active = true;
				TouchScreenKeyboard.hideInput = ipKeyboard.isInput;
			}
			else
			{
				ipKeyboard.tk = null;
			}
		}
		if (ipKeyboard.tk != null && !ipKeyboard.tk.active && Screen.orientation == 1)
		{
			ipKeyboard.isReset = false;
			ipKeyboard.tk.active = true;
			TouchScreenKeyboard.hideInput = true;
			TouchScreenKeyboardType touchScreenKeyboardType = 1;
			ipKeyboard.tk = TouchScreenKeyboard.Open(string.Empty, touchScreenKeyboardType, false, false, false, false, string.Empty);
		}
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00006EF9 File Offset: 0x000052F9
	public static void close()
	{
		ipKeyboard.tk.active = false;
		Canvas.aTran = false;
	}

	// Token: 0x04000085 RID: 133
	public static TouchScreenKeyboard tk;

	// Token: 0x04000086 RID: 134
	public static int TEXT;

	// Token: 0x04000087 RID: 135
	public static int NUMBERIC = 1;

	// Token: 0x04000088 RID: 136
	public static int PASS = 2;

	// Token: 0x04000089 RID: 137
	public static IKbAction act;

	// Token: 0x0400008A RID: 138
	public static int typeInput;

	// Token: 0x0400008B RID: 139
	public static bool isReset;

	// Token: 0x0400008C RID: 140
	public static bool isInput;
}
