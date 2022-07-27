using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class FontB
{
	// Token: 0x06000021 RID: 33 RVA: 0x00002A30 File Offset: 0x00000E30
	public FontB(string name, int size, int style, sbyte type, Color color1, Color color2)
	{
		this.type = type;
		this.myfont = (Font)Resources.Load(name);
		this.size = size;
		this.color1 = color1;
		this.color2 = color2;
		this.fstyle = style;
		this.wO = this.getWidthExactOf("O");
		this.setW();
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002AB0 File Offset: 0x00000EB0
	public void setW()
	{
		this.wChar = new sbyte[FontB.cCharDown.Length + FontB.cCharUp.Length];
		for (int i = 0; i < FontB.cCharDown.Length; i++)
		{
			int num = (int)FontB.cCharDown[i];
			if (FontB.cCharDown[i] == '/' || FontB.cCharDown[i] == '!')
			{
				this.wChar[i] = (sbyte)(3 * AvMain.hd);
			}
			else if (FontB.cCharDown[i] == '*' || FontB.cCharDown[i] == '/')
			{
				this.wChar[i] = (sbyte)this.getWidthExactOf("*");
			}
			else if (FontB.cCharDown[i] == ' ')
			{
				this.wChar[i] = (sbyte)(this.getWidthExactOf("a") - 4);
			}
			else if (num >= 65 && num <= 90)
			{
				this.wChar[i] = (sbyte)this.getWidthExactOf(FontB.cCharDown[i] + string.Empty);
			}
			else if (i > 16 && (num < 97 || num > 122))
			{
				this.wChar[i] = (sbyte)this.getWidthExactOf("a");
			}
			else
			{
				this.wChar[i] = (sbyte)this.getWidthExactOf(FontB.cCharDown[i] + string.Empty);
			}
		}
		for (int j = 0; j < FontB.cCharUp.Length; j++)
		{
			int num = (int)FontB.cCharUp[j];
			if (FontB.cCharDown[j] != ' ' && (num < 65 || num > 90))
			{
				this.wChar[FontB.cCharDown.Length + j] = (sbyte)this.getWidthExactOf("A");
			}
			else
			{
				this.wChar[FontB.cCharDown.Length + j] = (sbyte)this.getWidthExactOf(FontB.cCharUp[j] + string.Empty);
			}
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002CAC File Offset: 0x000010AC
	public int getWidth(string str)
	{
		char[] array = str.ToCharArray();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < array.Length; i++)
		{
			for (int j = 0; j < FontB.cCharDown.Length; j++)
			{
				if (array[i] == FontB.cCharDown[j])
				{
					num += (int)this.wChar[j];
					num2 = 1;
					break;
				}
			}
			if (num2 == 0)
			{
				for (int k = 0; k < FontB.cCharUp.Length; k++)
				{
					if (array[i] == FontB.cCharUp[k])
					{
						num += (int)this.wChar[k + FontB.cCharDown.Length];
						break;
					}
				}
			}
		}
		return num;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002D65 File Offset: 0x00001165
	public int getWidthOf(string str)
	{
		return this.getWidthExactOf(str);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002D70 File Offset: 0x00001170
	public int getWidthExactOf(string s)
	{
		int result;
		try
		{
			result = (int)new GUIStyle
			{
				font = this.myfont,
				fontSize = this.size
			}.CalcSize(new GUIContent(s)).x;
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"GET WIDTH OF ",
				s,
				" FAIL.\n",
				ex.Message,
				"\n",
				ex.StackTrace
			}));
			result = this.getWidthNotExactOf(s);
		}
		return result;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002E14 File Offset: 0x00001214
	public int getHeight()
	{
		if (this.height > 0)
		{
			return this.height;
		}
		GUIStyle guistyle = new GUIStyle();
		guistyle.font = this.myfont;
		guistyle.fontSize = this.size;
		try
		{
			this.height = (int)guistyle.CalcSize(new GUIContent("Adg")).y + 2;
		}
		catch (Exception ex)
		{
			Debug.LogError("FAIL GET HEIGHT " + ex.StackTrace);
			this.height = 20;
		}
		return this.height;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002EB4 File Offset: 0x000012B4
	public void drawString(MyGraphics g, string st, int x0, int y0, int align)
	{
		if ((int)this.type != 14 && (int)this.type != 10 && (int)this.type != 18 && (int)this.type != 15 && (int)this.type != 16 && (int)this.type != 17 && ((int)this.type != 4 || AvMain.hd == 1))
		{
			char[] array = st.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				char c = array[i];
				for (int j = 0; j < FontB.st1.Length; j++)
				{
					if (FontB.st1[j] == c)
					{
						array[i] = FontB.st2[j];
						break;
					}
				}
			}
			st = new string(array);
		}
		GUIStyle guistyle = new GUIStyle(GUI.skin.label);
		guistyle.font = this.myfont;
		guistyle.fontSize = this.size;
		float num = 0f;
		float num2 = 0f;
		int num3 = this.getWidthNotExactOf(st) + 10;
		if (num3 < Canvas.w)
		{
			num3 = Canvas.w;
		}
		if (align != 0)
		{
			if (align != 1)
			{
				if (align == 2)
				{
					num = (float)(x0 - num3 / 2);
					num2 = (float)y0;
					guistyle.alignment = 1;
				}
			}
			else
			{
				num = (float)(x0 - num3);
				num2 = (float)y0;
				guistyle.alignment = 2;
			}
		}
		else
		{
			num = (float)x0;
			num2 = (float)y0;
			guistyle.alignment = 0;
		}
		if (this.fstyle == 2)
		{
			guistyle.normal.textColor = this.color2;
			if ((int)this.type != 4)
			{
				g.drawString(st, (float)((int)num - 1), (float)((int)num2 - 1), guistyle, num3);
				g.drawString(st, (float)((int)num + 1), (float)((int)num2 + 1), guistyle, num3);
				g.drawString(st, (float)((int)num + 1), (float)((int)num2 - 1), guistyle, num3);
			}
			g.drawString(st, (float)((int)num - 1), (float)((int)num2 + 1), guistyle, num3);
			if (Main.isCompactDevice)
			{
				if ((int)this.type != 4)
				{
					g.drawString(st, (float)((int)num - 1), (float)((int)num2), guistyle, num3);
					g.drawString(st, (float)((int)num + 1), (float)((int)num2), guistyle, num3);
					g.drawString(st, (float)((int)num), (float)((int)num2 - 1), guistyle, num3);
				}
				g.drawString(st, (float)((int)num), (float)((int)num2 + 1), guistyle, num3);
			}
			guistyle.normal.textColor = this.color1;
			g.drawString(st, (float)((int)num), (float)((int)num2), guistyle, num3);
		}
		else if (this.fstyle == 1)
		{
			guistyle.normal.textColor = this.color2;
			g.drawString(st, (float)((int)num - 1), (float)((int)num2 - 1), guistyle, num3);
			guistyle.normal.textColor = this.color1;
			g.drawString(st, (float)((int)num), (float)((int)num2), guistyle, num3);
		}
		else if (this.fstyle == 0)
		{
			guistyle.normal.textColor = this.color1;
			g.drawString(st, (float)((int)num), (float)((int)num2), guistyle, num3);
		}
		else if (this.fstyle == 3)
		{
			guistyle.normal.textColor = this.color2;
			g.drawString(st, (float)((int)num), (float)((int)num2 - 1), guistyle, num3);
			guistyle.normal.textColor = this.color1;
			g.drawString(st, (float)((int)num), (float)((int)num2), guistyle, num3);
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x0000325C File Offset: 0x0000165C
	public string[] splitStrInLine(string src, int lineWidth)
	{
		ArrayList arrayList = this.splitStrInLineA(src, lineWidth);
		string[] array = new string[arrayList.Count];
		for (int i = 0; i < arrayList.Count; i++)
		{
			array[i] = (string)arrayList[i];
		}
		return array;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000032A5 File Offset: 0x000016A5
	public MyVector splitStrInLineV(string src, int lineWidth)
	{
		return new MyVector(this.splitStrInLineA(src, lineWidth));
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000032B4 File Offset: 0x000016B4
	public ArrayList splitStrInLineA(string src, int lineWidth)
	{
		ArrayList arrayList = new ArrayList();
		int num = 0;
		int num2 = 0;
		int length = src.Length;
		if (length < 5)
		{
			arrayList.Add(src);
			return arrayList;
		}
		string text = string.Empty;
		try
		{
			for (;;)
			{
				while (this.getWidth(text) < lineWidth)
				{
					text += src[num2];
					num2++;
					if (src[num2] == '\n')
					{
						break;
					}
					if (num2 >= length - 1)
					{
						num2 = length - 1;
						break;
					}
				}
				if (num2 != length - 1 && src[num2 + 1] != ' ')
				{
					int num3 = num2;
					while (src[num2 + 1] != '\n')
					{
						if (src[num2 + 1] != ' ' || src[num2] == ' ')
						{
							if (num2 != num)
							{
								num2--;
								continue;
							}
						}
						IL_E3:
						if (num2 == num)
						{
							num2 = num3;
							goto IL_ED;
						}
						goto IL_ED;
					}
					goto IL_E3;
				}
				IL_ED:
				string text2 = src.Substring(num, num2 + 1 - num);
				if (text2[0] == '\n')
				{
					text2 = text2.Substring(1, text2.Length - 1);
				}
				if (text2[text2.Length - 1] == '\n')
				{
					text2 = text2.Substring(0, text2.Length - 1);
				}
				arrayList.Add(text2);
				if (num2 == length - 1)
				{
					break;
				}
				num = num2 + 1;
				while (num != length - 1 && src[num] == ' ')
				{
					num++;
				}
				if (num == length - 1)
				{
					break;
				}
				num2 = num;
				text = string.Empty;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"EXCEPTION WHEN REAL SPLIT ",
				src,
				"\nend=",
				num2,
				"\n",
				ex.Message,
				"\n",
				ex.StackTrace
			}));
			arrayList.Add(src);
		}
		return arrayList;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000034E4 File Offset: 0x000018E4
	public int getWidthNotExactOf(string s)
	{
		return s.Length * this.wO;
	}

	// Token: 0x04000016 RID: 22
	public Font myfont;

	// Token: 0x04000017 RID: 23
	public int size = 15;

	// Token: 0x04000018 RID: 24
	private int height;

	// Token: 0x04000019 RID: 25
	private int wO;

	// Token: 0x0400001A RID: 26
	public Color color1 = Color.white;

	// Token: 0x0400001B RID: 27
	public Color color2 = Color.gray;

	// Token: 0x0400001C RID: 28
	private int fstyle;

	// Token: 0x0400001D RID: 29
	public static string st1 = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";

	// Token: 0x0400001E RID: 30
	public static string st2 = "¸µ¶·¹¨¾»¼½Æ©ÊÇÈÉËÐÌÎÏÑªÕÒÓÔÖÝ×ØÜÞãßáâä«èåæçé¬íêëìîóïñòô­øõö÷ùýúûüþ®¸µ¶·¹¡¾»¼½Æ¢ÊÇÈÉËÐÌÎÏÑ£ÕÒÓÔÖÝ×ØÜÞãßáâä¤èåæçé¥íêëìîóïñòô¦øõö÷ùýúûüþ§";

	// Token: 0x0400001F RID: 31
	public sbyte[][] wStr;

	// Token: 0x04000020 RID: 32
	public sbyte type;

	// Token: 0x04000021 RID: 33
	public static string stSetWDown = " +-%:,.0123456789aáàảãạăắằẳẵặâấầẩẫậbcdđeéèẻẽẹêếềểễệfghiíìỉĩịklmnoóòỏõọôốồổỗộơớờởỡợpquúùủũụưứừửữựyýỳỷỹỵrvjxtszw*/!AÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQUÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴRVJXTSZW";

	// Token: 0x04000022 RID: 34
	public static string stSetWUp = "*";

	// Token: 0x04000023 RID: 35
	public sbyte[] wChar;

	// Token: 0x04000024 RID: 36
	public static char[] cCharDown = FontB.stSetWDown.ToCharArray();

	// Token: 0x04000025 RID: 37
	public static char[] cCharUp = FontB.stSetWUp.ToCharArray();
}
