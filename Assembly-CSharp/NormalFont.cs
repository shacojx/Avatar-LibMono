using System;
using UnityEngine;

// Token: 0x0200017E RID: 382
public class NormalFont : FontX
{
	// Token: 0x06000A12 RID: 2578 RVA: 0x0006316C File Offset: 0x0006156C
	public NormalFont(int type)
	{
		this.fontB = new FontB(NormalFont.NAME[type], NormalFont.SIZE[type], NormalFont.STYLE[type], (sbyte)type, NormalFont.COLOR1[type], NormalFont.COLOR2[type]);
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x000631C1 File Offset: 0x000615C1
	public override void drawString(MyGraphics g, string st, int x, int y, int align)
	{
		this.fontB.drawString(g, st, x, y, align);
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x000631D5 File Offset: 0x000615D5
	public override int getWidth(string st)
	{
		return this.fontB.getWidthOf(st);
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x000631E3 File Offset: 0x000615E3
	public override string[] splitFontBStrInLine(string src, int lineWidth)
	{
		return this.fontB.splitStrInLine(src, lineWidth);
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x000631F4 File Offset: 0x000615F4
	public override MyVector splitFontBStrInLineV(string src, int lineWidth)
	{
		return this.fontB.splitStrInLineV(src, lineWidth);
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00063210 File Offset: 0x00061610
	public override string replace(string _text, string _searchStr, string _replacementStr)
	{
		if (_text.Equals(string.Empty) || _searchStr.Equals(string.Empty))
		{
			return _text;
		}
		string str = string.Empty;
		int num = _text.IndexOf(_searchStr);
		int num2 = 0;
		int length = _searchStr.Length;
		while (num != -1)
		{
			str = str + _text.Substring(num2, num - num2) + _replacementStr;
			num2 = num + length;
			num = _text.IndexOf(_searchStr, num2);
		}
		return str + _text.Substring(num2, _text.Length - num2);
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x00063298 File Offset: 0x00061698
	public override int getHeight()
	{
		return this.fontB.getHeight();
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x000632A5 File Offset: 0x000616A5
	public override int getWidthNotExact(string s)
	{
		return this.fontB.getWidthNotExactOf(s);
	}

	// Token: 0x04000CFB RID: 3323
	public new const sbyte LEFT = 0;

	// Token: 0x04000CFC RID: 3324
	public new const sbyte RIGHT = 1;

	// Token: 0x04000CFD RID: 3325
	public new const sbyte CENTER = 2;

	// Token: 0x04000CFE RID: 3326
	public Image imgFont;

	// Token: 0x04000CFF RID: 3327
	public string charList;

	// Token: 0x04000D00 RID: 3328
	public sbyte[] charWidth;

	// Token: 0x04000D01 RID: 3329
	private static string[] NAME = new string[]
	{
		"arial",
		"arial",
		"arial",
		"arial",
		"copper",
		"arial",
		"arial"
	};

	// Token: 0x04000D02 RID: 3330
	private static int[] STYLE = new int[]
	{
		0,
		2,
		0,
		0,
		1,
		2,
		2
	};

	// Token: 0x04000D03 RID: 3331
	private static Color[] COLOR1 = new Color[]
	{
		new Color(0f, 0.294f, 0.27f),
		Color.white,
		Color.white,
		Color.black,
		Color.yellow,
		Color.red,
		Color.yellow
	};

	// Token: 0x04000D04 RID: 3332
	private static Color[] COLOR2 = new Color[]
	{
		Color.black,
		Color.black,
		Color.black,
		Color.black,
		Color.black,
		Color.black,
		Color.black
	};

	// Token: 0x04000D05 RID: 3333
	private static int[] SIZE = new int[]
	{
		11,
		11,
		11,
		11,
		17,
		10,
		10
	};

	// Token: 0x04000D06 RID: 3334
	private FontB fontB;
}
