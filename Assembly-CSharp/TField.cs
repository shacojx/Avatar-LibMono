using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class TField
{
	// Token: 0x06000A9B RID: 2715 RVA: 0x00068C88 File Offset: 0x00067088
	public TField(string name, MyScreen parent, IAction ac)
	{
		this.action = ac;
		this.name = name;
		this.parent = parent;
		this.text = string.Empty;
		this.init();
		this.setFocus(false);
		this.height = TField.tfframe.frameHeight;
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x00068D4D File Offset: 0x0006714D
	public static void setVendorTypeMode(int mode)
	{
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x00068D50 File Offset: 0x00067150
	public void setFocus(bool isFocus)
	{
		if (this.isFocus != isFocus)
		{
			TField.mode = 0;
		}
		TField.lastKey = -1984;
		TField.timeChangeMode = (int)(DateTime.Now.Ticks / 1000L);
		this.isFocus = false;
		if (isFocus)
		{
		}
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x00068DA0 File Offset: 0x000671A0
	public void setFocusWithKb(bool isFocus)
	{
		Out.println(string.Concat(new object[]
		{
			"setFocusWithKb: ",
			this,
			"    ",
			this.text
		}));
		if (this.isFocus != isFocus)
		{
			TField.mode = 0;
		}
		TField.lastKey = -1984;
		TField.timeChangeMode = (int)(DateTime.Now.Ticks / 1000L);
		this.isFocus = false;
		if (isFocus)
		{
			TField.currentTField = this;
		}
		else if (TField.currentTField == this)
		{
			TField.currentTField = null;
		}
		if (Thread.CurrentThread.Name == Main.mainThreadName && TField.currentTField != null && Canvas.currentMyScreen == TField.currentTField.parent)
		{
			Debug.LogWarning("SHOW KEYBOARD FOR " + TField.currentTField.name);
			if (Screen.orientation != 1 || ipKeyboard.tk == null)
			{
				ipKeyboard.openKeyBoard(T.nameAcc, this.inputType, this.text, new TField.IActionChat(this), true);
				ipKeyboard.tk.text = this.text;
			}
			else
			{
				ipKeyboard.tk.text = this.text;
				ipKeyboard.act = new TField.IActionChat(this);
			}
		}
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x00068EEB File Offset: 0x000672EB
	private void init()
	{
		TField.CARET_HEIGHT = Canvas.blackF.getHeight() + 1;
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x00068EFE File Offset: 0x000672FE
	public static void close()
	{
		if (TouchScreenKeyboard.visible)
		{
			Canvas.isPointerRelease = false;
			Canvas.isKeyBoard = false;
			ipKeyboard.tk = null;
		}
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x00068F1C File Offset: 0x0006731C
	public void clear()
	{
		if (this.caretPos > 0 && this.text.Length > 0)
		{
			this.text = this.text.Substring(0, this.caretPos - 1);
			this.caretPos--;
			this.setOffset(0);
			this.setPasswordTest();
			if (ipKeyboard.tk != null)
			{
				ipKeyboard.tk.text = string.Empty;
			}
		}
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x00068F94 File Offset: 0x00067394
	public void setOffset(int index)
	{
		if (this.inputType == ipKeyboard.PASS)
		{
			this.paintedText = this.passwordText;
		}
		else
		{
			this.paintedText = this.text;
		}
		int num = Canvas.fontChatB.getWidth(this.paintedText.Substring(0, this.caretPos));
		if (index == -1)
		{
			if (num + this.offsetX < 15 && this.caretPos > 0 && this.caretPos < this.paintedText.Length)
			{
				this.offsetX += Canvas.fontChatB.getWidth(this.paintedText.Substring(this.caretPos, 1));
			}
		}
		else if (index == 1)
		{
			if (num + this.offsetX > this.width - 25 && this.caretPos < this.paintedText.Length && this.caretPos > 0)
			{
				this.offsetX -= Canvas.fontChatB.getWidth(this.paintedText.Substring(this.caretPos - 1, 1));
			}
		}
		else
		{
			this.offsetX = -(num - (this.width - 12 - 20 * AvMain.hd));
		}
		if (this.offsetX > 0)
		{
			this.offsetX = 0;
		}
		else if (this.offsetX < 0)
		{
			int num2 = Canvas.fontChatB.getWidth(this.paintedText) - (this.width - 12 - 20 * AvMain.hd);
			if (this.offsetX < -num2)
			{
				this.offsetX = -num2;
			}
		}
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x00069138 File Offset: 0x00067538
	private void keyPressedAny(int keyCode)
	{
		string[] array;
		if (this.inputType == 2 || this.inputType == 3)
		{
			array = TField.printA;
		}
		else
		{
			array = TField.print;
		}
		if (keyCode == TField.lastKey)
		{
			this.indexOfActiveChar = (this.indexOfActiveChar + 1) % array[keyCode - 48].Length;
			char c = array[keyCode - 48][this.indexOfActiveChar];
			if (TField.mode == 0)
			{
				c = char.ToLower(c);
			}
			else if (TField.mode == 1)
			{
				c = char.ToUpper(c);
			}
			else if (TField.mode == 2)
			{
				c = char.ToUpper(c);
			}
			else
			{
				c = array[keyCode - 48][array[keyCode - 48].Length - 1];
			}
			string str = this.text.Substring(0, this.caretPos - 1) + c;
			if (this.caretPos < this.text.Length)
			{
				str += this.text.Substring(this.caretPos, this.text.Length - this.caretPos);
			}
			this.text = str;
			this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
			this.setPasswordTest();
		}
		else if (this.text.Length < this.maxTextLenght)
		{
			if (TField.mode == 1 && TField.lastKey != -1984)
			{
				TField.mode = 0;
			}
			this.indexOfActiveChar = 0;
			char c2 = array[keyCode - 48][this.indexOfActiveChar];
			if (TField.mode == 0)
			{
				c2 = char.ToLower(c2);
			}
			else if (TField.mode == 1)
			{
				c2 = char.ToUpper(c2);
			}
			else if (TField.mode == 2)
			{
				c2 = char.ToUpper(c2);
			}
			else
			{
				c2 = array[keyCode - 48][array[keyCode - 48].Length - 1];
			}
			string str2 = this.text.Substring(0, this.caretPos) + c2;
			if (this.caretPos < this.text.Length)
			{
				str2 += this.text.Substring(this.caretPos, this.text.Length - this.caretPos);
			}
			this.text = str2;
			this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
			this.caretPos++;
			this.setPasswordTest();
			this.setOffset(0);
		}
		TField.lastKey = keyCode;
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x000693D0 File Offset: 0x000677D0
	private void keyPressedAscii(int keyCode)
	{
		if ((this.inputType == 2 || this.inputType == 3) && (keyCode < 48 || keyCode > 57) && (keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 122))
		{
			return;
		}
		if (this.text.Length < this.maxTextLenght)
		{
			string str = this.text.Substring(0, this.caretPos) + (char)keyCode;
			if (this.caretPos < this.text.Length)
			{
				str += this.text.Substring(this.caretPos, this.text.Length - this.caretPos);
			}
			this.text = str;
			this.caretPos++;
			this.setPasswordTest();
			this.setOffset(0);
		}
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x000694BD File Offset: 0x000678BD
	public static void setMode()
	{
		TField.mode++;
		if (TField.mode > 3)
		{
			TField.mode = 0;
		}
		TField.lastKey = TField.changeModeKey;
		TField.timeChangeMode = Environment.TickCount / 1000;
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x000694F8 File Offset: 0x000678F8
	public bool keyPressed(int keyCode)
	{
		if (keyCode == 8 || keyCode == -8 || keyCode == 204)
		{
			this.clear();
			return true;
		}
		if (TField.isQwerty && keyCode >= 32)
		{
			this.keyPressedAscii(keyCode);
			return false;
		}
		if (keyCode == TField.changeDau && this.inputType == 0)
		{
			this.setDau();
			return false;
		}
		if (keyCode == 42)
		{
			keyCode = 58;
		}
		if (keyCode == 35)
		{
			keyCode = 59;
		}
		if (keyCode >= 48 && keyCode <= 59)
		{
			if (this.inputType == 0 || this.inputType == 2 || this.inputType == 3)
			{
				this.keyPressedAny(keyCode);
			}
			else if (this.inputType == 1)
			{
				this.keyPressedAscii(keyCode);
				this.keyInActiveState = 1;
			}
		}
		else
		{
			this.indexOfActiveChar = 0;
			TField.lastKey = -1984;
			if (keyCode == 14 && !this.lockArrow)
			{
				if (this.caretPos > 0)
				{
					this.caretPos--;
					this.setOffset(0);
					this.showCaretCounter = 10;
					return false;
				}
			}
			else if (keyCode == 15 && !this.lockArrow)
			{
				if (this.caretPos < this.text.Length)
				{
					this.caretPos++;
					this.setOffset(0);
					this.showCaretCounter = 10;
					return false;
				}
			}
			else
			{
				if (keyCode == 19)
				{
					this.clear();
					return false;
				}
				TField.lastKey = keyCode;
			}
		}
		return true;
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x00069690 File Offset: 0x00067A90
	private void setDau()
	{
		this.timeDau = (long)(Environment.TickCount / 100);
		if (this.indexDau == -1)
		{
			for (int i = this.caretPos; i > 0; i--)
			{
				char c = this.text[i - 1];
				for (int j = 0; j < TField.printDau.Length; j++)
				{
					char c2 = TField.printDau[j];
					if (c == c2)
					{
						this.indexTemplate = j;
						this.indexCong = 0;
						this.indexDau = i - 1;
						return;
					}
				}
			}
			this.indexDau = -1;
		}
		else
		{
			this.indexCong++;
			if (this.indexCong >= 6)
			{
				this.indexCong = 0;
			}
			string str = this.text.Substring(0, this.indexDau);
			string str2 = this.text.Substring(this.indexDau + 1);
			string str3 = TField.printDau.Substring(this.indexTemplate + this.indexCong, 1);
			this.text = str + str3 + str2;
		}
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x000697A8 File Offset: 0x00067BA8
	public void paint(MyGraphics g)
	{
		bool flag = this.isFocused();
		if (this.inputType == ipKeyboard.PASS)
		{
			this.paintedText = this.passwordText;
		}
		else
		{
			if (!this.UpperCaseEnable)
			{
				this.text = this.text.ToLower();
			}
			this.paintedText = this.text;
		}
		g.setClip(0f, 0f, (float)(Canvas.w + 20), (float)Canvas.hCan);
		Canvas.paint.paintTextBox(g, this.x, this.y, this.width, this.height, this, flag, this.indexEraser);
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0006984F File Offset: 0x00067C4F
	public bool isFocused()
	{
		return TField.currentTField == this;
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x00069860 File Offset: 0x00067C60
	private void setPasswordTest()
	{
		if (this.inputType == ipKeyboard.PASS)
		{
			this.passwordText = string.Empty;
			for (int i = 0; i < this.text.Length; i++)
			{
				this.passwordText += "*";
			}
			if (this.keyInActiveState > 0 && this.caretPos > 0)
			{
				this.passwordText = this.passwordText.Substring(0, this.caretPos - 1) + this.text[this.caretPos - 1] + this.passwordText.Substring(this.caretPos, this.passwordText.Length - this.caretPos);
			}
		}
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0006992C File Offset: 0x00067D2C
	public void update()
	{
		if (ipKeyboard.tk != null && ipKeyboard.tk.active && TField.currentTField == this && ipKeyboard.tk.text != this.text)
		{
			this.tempScr = ipKeyboard.tk.text;
			if (this.tempScr.Length > this.text.Length)
			{
				int num = (int)this.tempScr.Substring(this.tempScr.Length - 1).ToCharArray()[0];
				if (this.isUser && (num < 48 || num > 57) && (num < 65 || num > 90) && (num < 97 || num > 122))
				{
					ipKeyboard.tk.text = this.text;
				}
				else if (this.tempScr.Length > this.maxTextLenght)
				{
					ipKeyboard.tk.text = this.text;
				}
				else
				{
					this.setText(ipKeyboard.tk.text);
				}
			}
			else
			{
				this.setText(ipKeyboard.tk.text);
			}
		}
		this.counter++;
		if (this.showCaretCounter > 0)
		{
			this.showCaretCounter--;
		}
		if (Canvas.currentDialog == null && Canvas.menuMain == null)
		{
			this.setTextBox();
		}
		else if (Canvas.currentDialog == Canvas.inputDlg && this == Canvas.inputDlg.tfInput)
		{
			this.setTextBox();
		}
		if (this.indexDau != -1 && (long)(Environment.TickCount / 100) - this.timeDau > 5L)
		{
			this.indexDau = -1;
		}
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x00069AF8 File Offset: 0x00067EF8
	public void setTextBox()
	{
		if (Canvas.isPointerClick)
		{
			if (Canvas.isPoint(this.x, this.y + 1, this.width, this.height - 2))
			{
				this.isTransTF = true;
				Canvas.isPointerClick = false;
				Out.println("name: " + this.name);
			}
			if (TField.currentTField == this && Canvas.isPoint(this.x + this.width - 22 * AvMain.hd, this.y + 1, 24 * AvMain.hd, this.height - 2))
			{
				this.indexEraser = 1;
				this.isTransTF = true;
				Canvas.isPointerClick = false;
			}
		}
		if (this.isTransTF)
		{
			if (Canvas.isPointerDown && !Canvas.isPoint(this.x + this.width - 22 * AvMain.hd, this.y + 1, 24 * AvMain.hd, this.height - 2))
			{
				this.indexEraser = 0;
			}
			if (Canvas.isPointerRelease)
			{
				this.isTransTF = false;
				if ((int)this.indexEraser == 1)
				{
					Canvas.isPointerRelease = false;
					this.indexEraser = 0;
					this.setText(string.Empty);
					if (TField.currentTField == this && ipKeyboard.tk != null)
					{
						ipKeyboard.tk.text = string.Empty;
					}
				}
				else if (Canvas.isPoint(this.x, this.y + 1, this.width, this.height - 2))
				{
					Canvas.isPointerRelease = false;
					if (TField.currentTField != this || ipKeyboard.tk == null)
					{
						this.setFocusWithKb(true);
					}
				}
			}
		}
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x00069CA9 File Offset: 0x000680A9
	public string getText()
	{
		return this.text;
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x00069CB4 File Offset: 0x000680B4
	public void setText(string text)
	{
		if (text == null)
		{
			return;
		}
		TField.lastKey = -1984;
		this.keyInActiveState = 0;
		this.indexOfActiveChar = 0;
		this.text = text;
		if (Thread.CurrentThread.Name == Main.mainThreadName && ipKeyboard.tk != null)
		{
			ipKeyboard.tk.text = text;
		}
		this.paintedText = text;
		this.setPasswordTest();
		this.caretPos = text.Length;
		this.setOffset(0);
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x00069D35 File Offset: 0x00068135
	public void setMaxTextLenght(int maxTextLenght)
	{
		this.maxTextLenght = maxTextLenght;
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x00069D3E File Offset: 0x0006813E
	public void setIputType(int iputType)
	{
		this.inputType = iputType;
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x00069D47 File Offset: 0x00068147
	public void setFocus2(bool b)
	{
		this.isFocus = false;
	}

	// Token: 0x04000DCB RID: 3531
	public bool UpperCaseEnable = true;

	// Token: 0x04000DCC RID: 3532
	public string name;

	// Token: 0x04000DCD RID: 3533
	public static TField currentTField;

	// Token: 0x04000DCE RID: 3534
	public const sbyte KEY_LEFT = 14;

	// Token: 0x04000DCF RID: 3535
	public const sbyte KEY_RIGHT = 15;

	// Token: 0x04000DD0 RID: 3536
	public const sbyte KEY_CLEAR = 19;

	// Token: 0x04000DD1 RID: 3537
	public int x;

	// Token: 0x04000DD2 RID: 3538
	public int y;

	// Token: 0x04000DD3 RID: 3539
	public int width;

	// Token: 0x04000DD4 RID: 3540
	public int height;

	// Token: 0x04000DD5 RID: 3541
	private bool isFocus;

	// Token: 0x04000DD6 RID: 3542
	public bool lockArrow;

	// Token: 0x04000DD7 RID: 3543
	public bool paintFocus = true;

	// Token: 0x04000DD8 RID: 3544
	public bool isChangeFocus = true;

	// Token: 0x04000DD9 RID: 3545
	public static int typeXpeed = 1;

	// Token: 0x04000DDA RID: 3546
	public static int[] MAX_TIME_TO_CONFIRM_KEY = new int[]
	{
		18,
		14,
		11,
		9,
		6,
		4,
		2
	};

	// Token: 0x04000DDB RID: 3547
	public static int CARET_HEIGHT = 0;

	// Token: 0x04000DDC RID: 3548
	public const int CARET_WIDTH = 1;

	// Token: 0x04000DDD RID: 3549
	public const int CARET_SHOWING_TIME = 5;

	// Token: 0x04000DDE RID: 3550
	public static int TEXT_GAP_X = 5;

	// Token: 0x04000DDF RID: 3551
	public const int MAX_SHOW_CARET_COUNER = 10;

	// Token: 0x04000DE0 RID: 3552
	public const int INPUT_TYPE_ANY = 0;

	// Token: 0x04000DE1 RID: 3553
	public const int INPUT_TYPE_NUMERIC = 1;

	// Token: 0x04000DE2 RID: 3554
	public const int INPUT_TYPE_PASSWORD = 2;

	// Token: 0x04000DE3 RID: 3555
	public const int INPUT_ALPHA_NUMBER_ONLY = 3;

	// Token: 0x04000DE4 RID: 3556
	public bool isUser;

	// Token: 0x04000DE5 RID: 3557
	public static string[] modeNotify = new string[]
	{
		"abc",
		"Abc",
		"ABC",
		"123"
	};

	// Token: 0x04000DE6 RID: 3558
	public IAction action;

	// Token: 0x04000DE7 RID: 3559
	private static string[] print = new string[]
	{
		" 0",
		".,@?!_1\"/$-():*+<=>;%&~#%^&*{}[];'/1",
		"abc2âă",
		"def3đê",
		"ghi4",
		"jkl5",
		"mno6ôơ",
		"pqrs7",
		"tuv8ư",
		"wxyz9",
		"*",
		"#"
	};

	// Token: 0x04000DE8 RID: 3560
	private static string[] printA = new string[]
	{
		"0",
		"1",
		"abc2",
		"def3",
		"ghi4",
		"jkl5",
		"mno6",
		"pqrs7",
		"tuv8",
		"wxyz9",
		"0",
		"0"
	};

	// Token: 0x04000DE9 RID: 3561
	private static string[] printBB = new string[]
	{
		" 0",
		"er1",
		"ty2",
		"ui3",
		"df4",
		"gh5",
		"jk6",
		"cv7",
		"bn8",
		"m9",
		"0",
		"0",
		"qw!",
		"as?",
		"zx",
		"op.",
		"l,"
	};

	// Token: 0x04000DEA RID: 3562
	private string text = string.Empty;

	// Token: 0x04000DEB RID: 3563
	private string passwordText = string.Empty;

	// Token: 0x04000DEC RID: 3564
	public string paintedText = string.Empty;

	// Token: 0x04000DED RID: 3565
	public int caretPos;

	// Token: 0x04000DEE RID: 3566
	public int counter;

	// Token: 0x04000DEF RID: 3567
	private int maxTextLenght = 40;

	// Token: 0x04000DF0 RID: 3568
	public int offsetX;

	// Token: 0x04000DF1 RID: 3569
	private static int lastKey = -1984;

	// Token: 0x04000DF2 RID: 3570
	public int keyInActiveState;

	// Token: 0x04000DF3 RID: 3571
	private int indexOfActiveChar;

	// Token: 0x04000DF4 RID: 3572
	public int showCaretCounter = 10;

	// Token: 0x04000DF5 RID: 3573
	public int inputType = ipKeyboard.TEXT;

	// Token: 0x04000DF6 RID: 3574
	public static bool isQwerty = true;

	// Token: 0x04000DF7 RID: 3575
	public static int typingModeAreaWidth;

	// Token: 0x04000DF8 RID: 3576
	public const sbyte abc = 0;

	// Token: 0x04000DF9 RID: 3577
	public const sbyte Abc = 1;

	// Token: 0x04000DFA RID: 3578
	public const sbyte ABC = 2;

	// Token: 0x04000DFB RID: 3579
	public const sbyte number123 = 3;

	// Token: 0x04000DFC RID: 3580
	public static int mode = 0;

	// Token: 0x04000DFD RID: 3581
	public static int timeChangeMode;

	// Token: 0x04000DFE RID: 3582
	public static FrameImage frame;

	// Token: 0x04000DFF RID: 3583
	public static FrameImage tfframe;

	// Token: 0x04000E00 RID: 3584
	private sbyte indexEraser;

	// Token: 0x04000E01 RID: 3585
	public static int changeModeKey = 11;

	// Token: 0x04000E02 RID: 3586
	public static int changeDau;

	// Token: 0x04000E03 RID: 3587
	public string sDefaust = string.Empty;

	// Token: 0x04000E04 RID: 3588
	public static int xDu;

	// Token: 0x04000E05 RID: 3589
	public static int yDu;

	// Token: 0x04000E06 RID: 3590
	public AvMain parent;

	// Token: 0x04000E07 RID: 3591
	public static IAction acClear;

	// Token: 0x04000E08 RID: 3592
	public Command cmdClear;

	// Token: 0x04000E09 RID: 3593
	public static bool isOpenTextBox = false;

	// Token: 0x04000E0A RID: 3594
	private int indexDau = -1;

	// Token: 0x04000E0B RID: 3595
	private int indexTemplate;

	// Token: 0x04000E0C RID: 3596
	private int indexCong;

	// Token: 0x04000E0D RID: 3597
	private long timeDau;

	// Token: 0x04000E0E RID: 3598
	private static string printDau = "aáàảãạâấầẩẫậăắằẳẵặeéèẻẽẹêếềểễệiíìỉĩịoóòỏõọôốồổỗộơớờởỡợuúùủũụưứừửữựyýỳỷỹỵ";

	// Token: 0x04000E0F RID: 3599
	private string tempScr = string.Empty;

	// Token: 0x04000E10 RID: 3600
	private bool openVirtual;

	// Token: 0x04000E11 RID: 3601
	public bool autoScaleScreen;

	// Token: 0x04000E12 RID: 3602
	public bool showSubTextField = true;

	// Token: 0x04000E13 RID: 3603
	private bool isTransTF;

	// Token: 0x02000192 RID: 402
	private class IActionChat : IKbAction
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x00069F57 File Offset: 0x00068357
		public IActionChat(TField me)
		{
			this.me = me;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00069F66 File Offset: 0x00068366
		public void perform(string text)
		{
			if (text.Equals(string.Empty))
			{
				return;
			}
			this.me.setText(text);
			this.me.action.perform();
		}

		// Token: 0x04000E14 RID: 3604
		private TField me;
	}
}
